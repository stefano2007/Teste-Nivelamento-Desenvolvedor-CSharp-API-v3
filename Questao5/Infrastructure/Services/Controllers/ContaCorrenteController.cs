using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;
using Questao5.Infrastructure.Services.Model;
using System.Net;

namespace Questao5.Infrastructure.Services.Controllers;

[ApiController]
[Route("[controller]")]
public class ContaCorrenteController : ControllerBase
{
    private readonly IMediator _mediator;
    public ContaCorrenteController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Movimentação de uma conta corrente
    /// </summary>
    /// <param name="idContaCorrente">Identificação da conta corrente</param>
    /// <param name="movimentoCreate">Objeto de criação da movimentação: Identificação da requisição, Valor a ser movimentado e Tipo de movimento (C = Credito, D = Débito)</param>
    /// <returns></returns>
    [HttpPost("{idContaCorrente}/movimentar")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(MovimentoCreateResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(CustomErro), (int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult> CriarMovimentoContaCorrente([FromRoute]Guid idContaCorrente, [FromBody] MovimentoCreate movimentoCreate)
    {
        var command = new MovimentoCreateCommand(
                    idRequisicao: movimentoCreate.IdRequisicao, 
                    idContaCorrente: idContaCorrente, 
                    tipoMovimento: movimentoCreate.TipoMovimento, 
                    valor: movimentoCreate.Valor
                );
        var result = await _mediator.Send(command);

        return Ok(result);
    }

    /// <summary>
    /// Saldo da conta corrente
    /// </summary>
    /// <param name="idContaCorrente">Identificação da Conta Corrente</param>
    /// <returns></returns>
    [HttpGet("saldo")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(SaldoContaCorrenteResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(CustomErro), (int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult> GetSaldoContaCorrente(Guid idContaCorrente)
    {
        var query = new SaldoContaCorrenteQuery(idContaCorrente: idContaCorrente);
        var result = await _mediator.Send(query);
        
        return Ok(result);
    }

    /// <summary>
    /// Saldo da conta corrente (Usando View SQL)
    /// </summary>
    /// <param name="idContaCorrente">Identificação da Conta Corrente</param>
    /// <returns></returns>
    [HttpGet("saldo/ViewSQL")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(SaldoContaCorrenteResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(CustomErro), (int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult> GetSaldoContaCorrenteViewSQL(Guid idContaCorrente)
    {
        //criado um endpoint usando o calculo através de uma view no SQLite, apenas para mostrar uma outra forma de ser feito 
        var query = new SaldoContaCorrenteQuery(idContaCorrente: idContaCorrente);
        var result = await _mediator.Send(query);

        return Ok(result);
    }
}