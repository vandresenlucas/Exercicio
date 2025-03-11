using FluentValidation;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Errors;
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
                .WithMessage(BusinessErrors.EMPTY_ID_IDEMPOTENCIA);

            RuleFor(command => command.IdContaCorrente)
               .Cascade(CascadeMode.Stop)
               .NotEmpty()
               .WithMessage(BusinessErrors.EMPTY_ID_CC);

            RuleFor(command => command.TipoMovimento)
               .Cascade(CascadeMode.Stop)
               .Must(tipoMovimento => !Enum.IsDefined(typeof(TipoMovimento), tipoMovimento))
               .WithMessage(BusinessErrors.INVALID_MOVIMENT_TYPE);

            RuleFor(command => command.Valor)
               .Cascade(CascadeMode.Stop)
               .GreaterThan(0)
               .WithMessage(BusinessErrors.INVALID_AMOUNT);

        }
    }
}
