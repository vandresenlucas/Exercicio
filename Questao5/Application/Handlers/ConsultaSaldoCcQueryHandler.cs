using MediatR;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;
using Questao5.Application.Services;
using Questao5.Domain.Entities.ContaCorrente;
using Questao5.Domain.Entities.Movimento;

namespace Questao5.Application.Handlers
{
    public class ConsultaSaldoCcQueryHandler : IRequestHandler<ConsultaSaldoCcQuery, Result>
    {
        private readonly IMovimentoQueryStore _movimentoQueryStore;
        private readonly IContaCorrenteService _contaCorrenteService;
        private readonly IContaCorrenteQueryStore _contaCorrenteQueryStore;

        public ConsultaSaldoCcQueryHandler(IMovimentoQueryStore moivmentoQueryStore,
            IContaCorrenteService contaCorrenteService,
            IContaCorrenteQueryStore contaCorrenteQueryStore)
        {
            _movimentoQueryStore = moivmentoQueryStore;
            _contaCorrenteService = contaCorrenteService;
            _contaCorrenteQueryStore = contaCorrenteQueryStore;
        }

        public async Task<Result> Handle(ConsultaSaldoCcQuery request, CancellationToken cancellationToken)
        {
            var contaValida = await _contaCorrenteService.ValidarContaCorrente(request.IdContaCorrente);

            if (contaValida != null)
                return contaValida;

            var movimentos = await _movimentoQueryStore.BuscarMovimentosPorContaCorrente(request);

            if (movimentos == null)
            {
                var dadosCc = await _contaCorrenteQueryStore.BuscarContaCorrenteAsync(request);

                return new Result
                {
                    Mensagem = "Cálculo finalizado com sucesso!!",
                    Response = new ConsultaSaldoCcQueryResponse
                    {
                        NumeroCc = dadosCc.Numero,
                        TitularCc = dadosCc.Nome,
                        DataHora = DateTime.Now,
                        Saldo = Math.Round(0.00m, 2),
                    }
                };
            }

            var result = await _movimentoQueryStore.CalcularSaldoContaCorrente(request);

            return new Result
            {
                Mensagem = "Cálculo finalizado com sucesso!!",
                Response = result
            };
        }
    }
}
