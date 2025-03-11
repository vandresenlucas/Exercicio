using FluentValidation;
using Questao5.Application.Commands.Requests;
using Questao5.Domain.Enumerators;

namespace Questao5.Application.Validators
{
    public class MovimentoCcCommandValidator : AbstractValidator<MovimentoCcCommand>
    {
        public MovimentoCcCommandValidator() 
        {
            RuleFor(command => command.ChaveIdempotencia)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage(BusinessErrorsEnum.EMPTY_ID_IDEMPOTENCIA.ToString());

            RuleFor(command => command.IdContaCorrente)
               .Cascade(CascadeMode.Stop)
               .NotEmpty()
               .WithMessage(BusinessErrorsEnum.EMPTY_ID_CC.ToString());

            RuleFor(command => command.TipoMovimento)
               .Cascade(CascadeMode.Stop)
               .Must(tipoMovimento => tipoMovimento.ToUpper() == "C" || tipoMovimento.ToUpper() == "D")
               .WithMessage(BusinessErrorsEnum.INVALID_TYPE.ToString());

            RuleFor(command => command.Valor)
               .Cascade(CascadeMode.Stop)
               .GreaterThan(0)
               .WithMessage(BusinessErrorsEnum.INVALID_VALUE.ToString());

        }
    }
}
