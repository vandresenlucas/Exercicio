using MediatR;
using Questao5.Application.Commands.Responses;
using Questao5.Domain.Entities.Movimento;
using Questao5.Domain.Enumerators;

namespace Questao5.Application.Commands.Requests
{
    public class MovimentoCcCommand : IRequest<MovimentoCcResponse>
    {
        public string ChaveIdempotencia { get; set; }
        public string IdContaCorrente { get; set; }
        public decimal Valor { get; set; }
        public TipoMovimento TipoMovimento { get; set; }

        public static implicit operator Movimento(MovimentoCcCommand command)
        {
            if (command == null)
                return null;

            return new(
                Guid.NewGuid().ToString(),
                command.IdContaCorrente,
                DateTime.Now,
                command.TipoMovimento.ToString(),
                command.Valor);
        }
    }
}
