using Microsoft.Extensions.Localization;
using Questao5.Application.Errors;
using Questao5.Domain.Entities.ContaCorrente;
using Questao5.Domain.Enumerators;
using Questao5.Domain.Language;
using Questao5.Infrastructure.Database.QueryStore.Requests;

namespace Questao5.Application.Services
{
    public class MovimentoService : IMovimentoService
    {
        private readonly IContaCorrenteQueryStore _contaCorrenteQueryStore;
        private readonly IStringLocalizer<ManagerResources> _localizer;

        public MovimentoService(IContaCorrenteQueryStore contaCorrenteQueryStore, IStringLocalizer<ManagerResources> localizer)
        {
            _contaCorrenteQueryStore = contaCorrenteQueryStore;
            _localizer = localizer;
        }

        public async Task<Result> ValidarMovimento(string idContaCorrente)
        {
            var idCcRequest = new BuscarContaCorrenteRequest { IdContaCorrente = idContaCorrente };

            var cc = await _contaCorrenteQueryStore.BuscarContaCorrenteAsync(idCcRequest);

            if (cc == null)
                return new Result
                {
                    Sucesso = false,
                    Mensagem = _localizer[BusinessErrorsEnum.INVALID_ACCOUNT.ToString()],
                    TipoErro = "INVALID_ACCOUNT"
                };

            if (!cc.Ativo)
                return new Result
                {
                    Sucesso = false,
                    Mensagem = _localizer[BusinessErrorsEnum.INACTIVE_ACCOUNT.ToString()],
                    TipoErro = "INACTIVE_ACCOUNT"
                };

            return null;
        }
    }
}
