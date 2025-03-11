using FluentAssertions;
using NSubstitute;
using Questao5.Application;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Handlers;
using Questao5.Application.Services;
using Questao5.Domain.Entities.ContaCorrente;
using Questao5.Domain.Entities.Idempotencia;
using Questao5.Domain.Entities.Movimento;
using Questao5.Infrastructure.Database.QueryStore.Requests;
using Questao5.Infrastructure.Database.QueryStore.Responses;
using Questao5.Tests.Builders;
using System.Text.Json;
using Xunit;

namespace Questao5.Tests
{
    public class MovimentoCcCommandHandlerTests
    {
        private readonly IIdempotenciaQueryStore _idempotenciaQueryStore;
        private readonly IMovimentoCommandStore _movimentoCommandStore;
        private readonly IIdempotenciaCommandStore _idempotenciaCommandStore;
        private readonly IContaCorrenteService _movimentoService;
        private readonly MovimentoCcCommandHandler _handler;

        public MovimentoCcCommandHandlerTests()
        {
            _idempotenciaQueryStore = Substitute.For<IIdempotenciaQueryStore>();
            _movimentoCommandStore = Substitute.For<IMovimentoCommandStore>();
            _idempotenciaCommandStore = Substitute.For<IIdempotenciaCommandStore>();
            _movimentoService = Substitute.For<IContaCorrenteService>();

            _handler = new MovimentoCcCommandHandler(_idempotenciaQueryStore,
                                                     Substitute.For<IContaCorrenteQueryStore>(),
                                                     _movimentoCommandStore,
                                                     _idempotenciaCommandStore,
                                                     _movimentoService);
        }

        [Fact]
        public async Task When_Idempotency_Key_Exists_Returns_Existing_Result()
        {
            // Arrange
            var command = new MovimentoCcCommandBuilder().Build();
            var idempotencia = new IdempotenciaResponse 
            { 
                Resultado = JsonSerializer.Serialize(new Result { Sucesso = true, Mensagem = "Existe idempotencia" }) 
            };

            _idempotenciaQueryStore
                .BuscarIdempotenciaAsync(Arg.Any<BuscarIdempotenciaRequest>())
                .Returns(Task.FromResult(idempotencia));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.Sucesso);
            Assert.Equivalent(new Result { Sucesso = true, Mensagem = "Existe idempotencia" }, result);
        }

        [Fact]
        public async Task When_Account_Is_Invalid_Returns_Error()
        {
            // Arrange
            var command = new MovimentoCcCommandBuilder().Build();

            _movimentoService
                .ValidarContaCorrente(Arg.Any<string>())
                .Returns(Task.FromResult(new Result { Sucesso = false, Mensagem = "Conta corrente inválida" }));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.Sucesso);
            result.Should().BeEquivalentTo(
                new Result { Sucesso = false, Mensagem = "Conta corrente inválida" }, 
                options => options.Excluding(r => r.Response));
        }

        [Fact]
        public async Task When_Valid_Request_Returns_Success_And_Registers_Movement()
        {
            // Arrange
            var command = new MovimentoCcCommandBuilder().Build();

            _idempotenciaQueryStore
                .BuscarIdempotenciaAsync(Arg.Any<BuscarIdempotenciaRequest>())
                .Returns(Task.FromResult<IdempotenciaResponse>(null));  

            _movimentoService
                .ValidarContaCorrente(Arg.Any<string>())
                .Returns(Task.FromResult<Result>(null)); 

            _movimentoCommandStore
                .RegistrarMovimentoAsync(Arg.Any<Movimento>())
                .Returns(Task.CompletedTask);
            
            _idempotenciaCommandStore.RegistrarIdempotenciaAsync(
                    Arg.Any<string>(), 
                    Arg.Any<MovimentoCcCommand>(), 
                    Arg.Any<Result>())
                .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.Sucesso);
            result.Should().BeEquivalentTo(new Result { Mensagem = "Movimento registrado com sucesso!" }, options => options.Excluding(r => r.Response));
            await _movimentoCommandStore.Received(1).RegistrarMovimentoAsync(Arg.Any<Movimento>());
            await _idempotenciaCommandStore.Received(1).RegistrarIdempotenciaAsync(command.ChaveIdempotencia, command, Arg.Any<Result>());
        }
    }
}
