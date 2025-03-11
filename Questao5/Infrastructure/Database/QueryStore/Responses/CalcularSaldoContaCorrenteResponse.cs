using Questao5.Application.Queries.Responses;

namespace Questao5.Infrastructure.Database.QueryStore.Responses
{
    public class CalcularSaldoContaCorrenteResponse
    {
        public int NumeroCc { get; set; }
        public string TitularCc { get; set; }
        public DateTime DataHora { get; set; }
        public decimal Saldo { get; set; }

        public static implicit operator ConsultaSaldoCcQueryResponse(CalcularSaldoContaCorrenteResponse response)
        {
            if (response == null)
                return null;

            return new ConsultaSaldoCcQueryResponse
            {
                NumeroCc = response.NumeroCc,
                TitularCc = response.TitularCc,
                DataHora = response.DataHora,
                Saldo = response.Saldo
            };
        }
    }
}
