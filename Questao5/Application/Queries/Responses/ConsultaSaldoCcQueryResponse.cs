namespace Questao5.Application.Queries.Responses
{
    public class ConsultaSaldoCcQueryResponse
    {
        public int NumeroCc { get; set; }
        public string TitularCc { get; set; }
        public DateTime DataHora { get; set; }
        public decimal Saldo { get; set; }
    }
}
