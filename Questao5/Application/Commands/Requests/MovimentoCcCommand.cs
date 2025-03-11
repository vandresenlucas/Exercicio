using MediatR;
using Questao5.Domain.Entities.Movimento;
using Swashbuckle.AspNetCore.Annotations;

namespace Questao5.Application.Commands.Requests
{
    [SwaggerSchema(Description = "Representa os dados para adicionar uma movimentação.")]
    public class MovimentoCcCommand : IRequest<Result>
    {
        [SwaggerSchema("Identificação da requisição.")]
        public string ChaveIdempotencia { get; set; }

        [SwaggerSchema("Identificação da conta corrente.")]
        public string IdContaCorrente { get; set; }

        [SwaggerSchema("Valor a ser movimentado.")]
        public decimal Valor { get; set; }

        [SwaggerSchema("tipo da movimentação (C = Credito, D = Débito)")]
        public string TipoMovimento { get; set; }

        public static implicit operator Movimento(MovimentoCcCommand command)
        {
            if (command == null)
                return null;

            return new(
                Guid.NewGuid().ToString(),
                command.IdContaCorrente,
                DateTime.Now,
                command.TipoMovimento,
                command.Valor);
        }
    }
}
