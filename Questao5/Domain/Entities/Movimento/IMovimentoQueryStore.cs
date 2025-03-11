using Questao5.Infrastructure.Database.QueryStore.Requests;
using Questao5.Infrastructure.Database.QueryStore.Responses;

namespace Questao5.Domain.Entities.Movimento
{
    public interface IMovimentoQueryStore
    {
        Task<BuscarMovimentosPorCcIdResponse> BuscarMovimentosPorContaCorrente(BuscarMovimentosPorCcIdRequest request);
        Task<CalcularSaldoContaCorrenteResponse> CalcularSaldoContaCorrente(CalcularSaldoContaCorrenteRequest request);
    }
}
