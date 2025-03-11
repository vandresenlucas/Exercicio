namespace Questao5.Infrastructure.Database.QueryStore.Responses
{
    public class BuscarContaCorrenteResponse
    {
        public string IdContaCorrente { get; set; }
        public bool Ativo { get; set; }
        public int Numero { get; set; }
        public string Nome { get; set; }
    }
}
