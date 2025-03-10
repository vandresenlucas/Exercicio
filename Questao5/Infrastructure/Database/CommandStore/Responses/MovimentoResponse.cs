using Questao5.Domain.Enumerators;

namespace Questao5.Infrastructure.Database.CommandStore.Responses
{
    public class MovimentoResponse
    {
        public string IdMovimento { get; set; }
        public string IdContaCorrente { get; set; }
        public DateTime DataMovimento { get; set; }
        public TipoMovimento TipoMovimento { get; set; }
        public double Valor { get; set; }
    }
}
