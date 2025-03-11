using Questao5.Infrastructure.Database.QueryStore.Requests;

namespace Questao5.Tests.Builders
{
    public class BuscarContaCorrenteRequestBuilder
    {
        private string _idContaCorrente = Guid.NewGuid().ToString();

        public BuscarContaCorrenteRequest Build()
        {
            return new BuscarContaCorrenteRequest
            {
                IdContaCorrente = _idContaCorrente,
            };
        }
    }
}
