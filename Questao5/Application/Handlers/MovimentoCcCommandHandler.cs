using MediatR;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Domain.Entities.ContaCorrente;
using Questao5.Domain.Entities.Idempotencia;
using Questao5.Domain.Entities.Movimento;
using Questao5.Domain.Enumerators;
using Questao5.Infrastructure.Database.QueryStore.Requests;
using System.Text.Json;

namespace Questao5.Application.Handlers
{
    public class MovimentoCcCommandHandler : IRequestHandler<MovimentoCcCommand, MovimentoCcResponse>
    {
        private readonly IIdempotenciaQueryStore _idempotenciaQueryStore;
        private readonly IContaCorrenteQueryStore _contaCorrenteQueryStore;
        private readonly IMovimentoCommandStore _movimentoCommandStore;
        private readonly IIdempotenciaCommandStore _idempotenciaCommandStore;

        public MovimentoCcCommandHandler(IIdempotenciaQueryStore idempotenciaQueryStore, 
            IContaCorrenteQueryStore contaCorrenteQueryStore, 
            IMovimentoCommandStore movimentoCommandStore, 
            IIdempotenciaCommandStore idempotenciaCommandStore)
        {
            _idempotenciaQueryStore = idempotenciaQueryStore;
            _contaCorrenteQueryStore = contaCorrenteQueryStore;
            _movimentoCommandStore = movimentoCommandStore;
            _idempotenciaCommandStore = idempotenciaCommandStore;
        }

        public async Task<MovimentoCcResponse> Handle(MovimentoCcCommand request, CancellationToken cancellationToken)
        {
            Movimento movimento = request;
            var idempotenciaRequest = new BuscarIdempotenciaRequest { ChaveIdempotencia = request.ChaveIdempotencia };
            var idCcRequest = new BuscarContaCorrenteRequest { IdContaCorrente = request.IdContaCorrente };
            var idempotencia = await _idempotenciaQueryStore.BuscarIdempotencia(idempotenciaRequest);

            if (idempotencia != null)
            {
                var teste = JsonSerializer.Deserialize<MovimentoCcResponse>(idempotencia.Resultado);
                return JsonSerializer.Deserialize<MovimentoCcResponse>(idempotencia.Resultado); //Verificar
            }

            var cc = await _contaCorrenteQueryStore.BuscarContaCorrente(idCcRequest);

            if (cc == null) 
                return new MovimentoCcResponse 
                { 
                    Sucesso = false,
                    Mensagem = "Conta corrente não encontrada.", 
                    TipoErro = "INVALID_ACCOUNT" 
                };

            if (!cc.Ativo)
                return new MovimentoCcResponse 
                { 
                    Sucesso = false, 
                    Mensagem = "Conta corrente inativa.", 
                    TipoErro = "INACTIVE_ACCOUNT" 
                };

            if (request.Valor <= 0)
                return new MovimentoCcResponse
                {
                    Sucesso = false,
                    Mensagem = "Valor inválido.",
                    TipoErro = "INVALID_VALUE"
                };

            if(!Enum.IsDefined(typeof(TipoMovimento), request.TipoMovimento))
                return new MovimentoCcResponse
                {
                    Sucesso = false,
                    Mensagem = "Tipo da conta inválido.",
                    TipoErro = "INVALID_TYPE"
                };

            await _movimentoCommandStore.RegistrarMovimento(movimento);

            var response = new MovimentoCcResponse
            {
                Sucesso = true,
                Mensagem = "Movimento registrado com sucesso!",
                IdMovimento = movimento.IdMovimento
            };

            await _idempotenciaCommandStore.RegistrarIdempotencia(request.ChaveIdempotencia, request, response);

            return response;
        }
    }
}
