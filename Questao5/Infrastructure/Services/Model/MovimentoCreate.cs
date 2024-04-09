using System.ComponentModel.DataAnnotations;

namespace Questao5.Infrastructure.Services.Model;
public class MovimentoCreate
{
    [Required]
    public Guid IdRequisicao { get; set; }
    [Required]
    public double Valor { get; set; }
    [Required]
    public string TipoMovimento { get; set; }
}
