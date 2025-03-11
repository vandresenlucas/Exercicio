using Questao5.Application.Commands.Responses;
using Questao5.Application.Errors;
using Questao5.Domain.Entities.ContaCorrente;
using Questao5.Infrastructure.Database.QueryStore.Requests;

namespace Questao5.Application.Services
{
    public class MovimentoService : IMovimentoService
    {
        private readonly IContaCorrenteQueryStore _contaCorrenteQueryStore;

        public MovimentoService(IContaCorrenteQueryStore contaCorrenteQueryStore)
        {
            _contaCorrenteQueryStore = contaCorrenteQueryStore;
        }

        public async Task<MovimentoCcResponse> ValidarMovimento(string idContaCorrente)
        {
            var idCcRequest = new BuscarContaCorrenteRequest { IdContaCorrente = idContaCorrente };

            var cc = await _contaCorrenteQueryStore.BuscarContaCorrenteAsync(idCcRequest);

            if (cc == null)
                return new MovimentoCcResponse
                {
                    Sucesso = false,
                    Mensagem = BusinessErrors.INVALID_ACCOUNT,
                    TipoErro = "INVALID_ACCOUNT"
                };

            if (!cc.Ativo)
                return new MovimentoCcResponse
                {
                    Sucesso = false,
                    Mensagem = BusinessErrors.INACTIVE_ACCOUNT,
                    TipoErro = "INACTIVE_ACCOUNT"
                };

            return null;
        }
    }
}
