using System.ComponentModel.DataAnnotations.Schema;

namespace Questao5.Infrastructure.Database.CommandStore.Requests
{
    public class RegistrarMovimentoRequest
    {
        public string? ChaveIdempotencia { get; set; }
        public string? IdContaCorrente { get; set; }
        public string? TipoMovimento { get; set; }
        public decimal Valor { get; set; }
    }
}
