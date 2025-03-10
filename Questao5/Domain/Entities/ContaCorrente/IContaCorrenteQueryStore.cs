using Questao5.Infrastructure.Database.QueryStore.Requests;
using Questao5.Infrastructure.Database.QueryStore.Responses;

namespace Questao5.Domain.Entities.ContaCorrente
{
    public interface IContaCorrenteQueryStore
    {
        Task<BuscarContaCorrenteResponse> BuscarContaCorrente(BuscarContaCorrenteRequest request);
    }
}
