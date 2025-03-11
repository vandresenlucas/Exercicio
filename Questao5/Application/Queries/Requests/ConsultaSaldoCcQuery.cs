using MediatR;
using Questao5.Infrastructure.Database.QueryStore.Requests;
using Swashbuckle.AspNetCore.Annotations;

namespace Questao5.Application.Queries.Requests
{
    [SwaggerSchema(Description = "Representa os dados necessário para fazer a consulta de saldo da conta corrente.")]
    public class ConsultaSaldoCcQuery : IRequest<Result>
    {
        [SwaggerSchema("Identificação da conta corrente.")]
        public string IdContaCorrente { get; set; }

        public static implicit operator BuscarMovimentosPorCcIdRequest(ConsultaSaldoCcQuery query)
        {
            if (query == null)
                return null;

            return new BuscarMovimentosPorCcIdRequest { IdContaCorrente = query.IdContaCorrente };
        }

        public static implicit operator CalcularSaldoContaCorrenteRequest(ConsultaSaldoCcQuery query)
        {
            if (query == null)
                return null;

            return new CalcularSaldoContaCorrenteRequest { IdContaCorrente = query.IdContaCorrente };
        }

        public static implicit operator BuscarContaCorrenteRequest(ConsultaSaldoCcQuery query)
        {
            if (query == null)
                return null;

            return new BuscarContaCorrenteRequest { IdContaCorrente = query.IdContaCorrente };
        }
    }
}
