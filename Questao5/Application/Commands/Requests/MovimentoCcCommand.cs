using MediatR;
using Questao5.Application.Commands.Responses;
using Questao5.Domain.Enumerators;

namespace Questao5.Application.Commands.Requests
{
    public class MovimentoCcCommand : IRequest<MovimentoCcResponse>
    {
        public string ChaveIdempotencia { get; set; }
        public string IdContaCorrente { get; set; }
        public double Valor { get; set; }
        public TipoMovimento TipoMovimento { get; set; }
    }
}
