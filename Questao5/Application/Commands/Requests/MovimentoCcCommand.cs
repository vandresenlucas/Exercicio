using MediatR;
using Questao5.Domain.Entities.Movimento;

namespace Questao5.Application.Commands.Requests
{
    public class MovimentoCcCommand : IRequest<Result>
    {
        public string ChaveIdempotencia { get; set; }
        public string IdContaCorrente { get; set; }
        public decimal Valor { get; set; }
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
