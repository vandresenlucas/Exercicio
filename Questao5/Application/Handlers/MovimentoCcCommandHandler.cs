using MediatR;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Application.Services;
using Questao5.Domain.Entities.ContaCorrente;
using Questao5.Domain.Entities.Idempotencia;
using Questao5.Domain.Entities.Movimento;
using Questao5.Infrastructure.Database.QueryStore.Requests;
using System.Text.Json;

namespace Questao5.Application.Handlers
{
    public class MovimentoCcCommandHandler : IRequestHandler<MovimentoCcCommand, Result>
    {
        private readonly IIdempotenciaQueryStore _idempotenciaQueryStore;
        private readonly IMovimentoCommandStore _movimentoCommandStore;
        private readonly IIdempotenciaCommandStore _idempotenciaCommandStore;
        private readonly IContaCorrenteService _contaCorrenteService;

        public MovimentoCcCommandHandler(IIdempotenciaQueryStore idempotenciaQueryStore,
            IContaCorrenteQueryStore contaCorrenteQueryStore,
            IMovimentoCommandStore movimentoCommandStore,
            IIdempotenciaCommandStore idempotenciaCommandStore,
            IContaCorrenteService contaCorrenteService)
        {
            _idempotenciaQueryStore = idempotenciaQueryStore;
            _movimentoCommandStore = movimentoCommandStore;
            _idempotenciaCommandStore = idempotenciaCommandStore;
            _contaCorrenteService = contaCorrenteService;
        }

        public async Task<Result> Handle(MovimentoCcCommand request, CancellationToken cancellationToken)
        {
            Movimento movimento = request;
            var idempotenciaRequest = new BuscarIdempotenciaRequest { ChaveIdempotencia = request.ChaveIdempotencia };
            var idempotencia = await _idempotenciaQueryStore.BuscarIdempotenciaAsync(idempotenciaRequest);

            if (idempotencia != null)
                return JsonSerializer.Deserialize<Result>(idempotencia.Resultado); 

            var contaValida = await _contaCorrenteService.ValidarContaCorrente(request.IdContaCorrente);

            if (contaValida != null)
                return contaValida;

            await _movimentoCommandStore.RegistrarMovimentoAsync(movimento);

            var response = new Result
            {
                Mensagem = "Movimento registrado com sucesso!",
                Response = new MovimentoCcCommandResponse { IdMovimento = movimento.IdMovimento }
            };

            await _idempotenciaCommandStore.RegistrarIdempotenciaAsync(request.ChaveIdempotencia, request, response);

            return response;
        }
    }
}
