using FluentValidation.TestHelper;
using Questao5.Application.Validators;
using Questao5.Tests.Builders;
using Xunit;

namespace Questao5.Tests
{
    public class MovimentoCcCommandValidatorTests
    {
        private readonly MovimentoCcCommandValidator _validator;

        public MovimentoCcCommandValidatorTests()
        {
            _validator = new MovimentoCcCommandValidator();
        }

        [Fact]
        public void Validator_Should_Have_Error_When_ChaveIdempotencia_Is_Empty()
        {
            // Arrange
            var command = new MovimentoCcCommandBuilder().WhithoutChaveIdempotencia().Build();

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.ChaveIdempotencia)
                  .WithErrorMessage("EMPTY_ID_IDEMPOTENCIA");
        }

        [Fact]
        public void Validator_Should_Have_Error_When_IdContaCorrente_Is_Empty()
        {
            // Arrange
            var command = new MovimentoCcCommandBuilder().WhithoutIdContaCorrente().Build();

            // Act & Assert
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.IdContaCorrente)
                  .WithErrorMessage("EMPTY_ID_CC");
        }

        [Fact]
        public void Validator_Should_Have_Error_When_TipoMovimento_Is_Invalid()
        {
            // Arrange
            var command = new MovimentoCcCommandBuilder().WhithTipoMovimento("X").Build();

            // Act & Assert
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.TipoMovimento)
                  .WithErrorMessage("INVALID_TYPE");
        }

        [Fact]
        public void Validator_Should_Have_Error_When_Valor_Is_Zero_Or_Negative()
        {
            // Arrange
            var command = new MovimentoCcCommandBuilder().Build();

            // Act & Assert
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.Valor)
                  .WithErrorMessage("INVALID_VALUE");
        }

        [Fact]
        public void Validator_ShouldNotHaveError_WhenValidCommand()
        {
            // Arrange
            var command = new MovimentoCcCommandBuilder().WhithValor(100).Build();

            // Act & Assert
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
