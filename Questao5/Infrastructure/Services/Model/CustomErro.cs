using Questao5.Domain.Enumerators;

namespace Questao5.Infrastructure.Services.Model
{
    public class CustomErro
    {
        public CustomErro(string mensagem, TipoException tipoException)
        {
            Mensagem = mensagem;
            TipoException = tipoException.ToString();
        }

        public string Mensagem { get; set; }
        public string TipoException { get; set; }
    }
}
