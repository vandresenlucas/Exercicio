using NSubstitute;
using Questao5.Application;
using Questao5.Application.Handlers;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;
using Questao5.Application.Services;
using Questao5.Domain.Entities.ContaCorrente;
using Questao5.Domain.Entities.Movimento;
using Questao5.Infrastructure.Database.QueryStore.Requests;
using Questao5.Infrastructure.Database.QueryStore.Responses;
using Xunit;

namespace Questao5.Tests
{
    public class ConsultaSaldoCcQueryHandlerTests
    {
        private readonly IMovimentoQueryStore _movimentoQueryStore;
        private readonly IContaCorrenteService _contaCorrenteService;
        private readonly IContaCorrenteQueryStore _contaCorrenteQueryStore;
        private readonly ConsultaSaldoCcQueryHandler _handler;

        public ConsultaSaldoCcQueryHandlerTests()
        {
            // Criando mocks
            _movimentoQueryStore = Substitute.For<IMovimentoQueryStore>();
            _contaCorrenteService = Substitute.For<IContaCorrenteService>();
            _contaCorrenteQueryStore = Substitute.For<IContaCorrenteQueryStore>();

            // Criando o handler com os mocks
            _handler = new ConsultaSaldoCcQueryHandler(
                _movimentoQueryStore,
                _contaCorrenteService,
                _contaCorrenteQueryStore
            );
        }

        [Fact]
        public async Task When_Whithout_Transactions_Return_Amount_Zero()
        {
            // Arrange
            var query = new ConsultaSaldoCcQuery { IdContaCorrente = Guid.NewGuid().ToString() };
            var numeroCc = 12345;
            var titularCc = "João";

            _contaCorrenteService.ValidarContaCorrente(Arg.Any<string>()).Returns((Result)null);

            _movimentoQueryStore.BuscarMovimentosPorContaCorrente(Arg.Any<BuscarMovimentosPorCcIdRequest>())
                .Returns((BuscarMovimentosPorCcIdResponse)null);

            _contaCorrenteQueryStore.BuscarContaCorrenteAsync(Arg.Any<BuscarContaCorrenteRequest>())
                .Returns(Task.FromResult(new BuscarContaCorrenteResponse { Numero = numeroCc, Nome = titularCc }));

            _movimentoQueryStore.CalcularSaldoContaCorrente(query).Returns(
                new CalcularSaldoContaCorrenteResponse
                {
                    NumeroCc = numeroCc,
                    TitularCc = titularCc
                });

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Cálculo finalizado com sucesso!!", result.Mensagem);
            var response = Assert.IsType<ConsultaSaldoCcQueryResponse>(result.Response);
            Assert.Equal(12345, response.NumeroCc);
            Assert.Equal("João", response.TitularCc);
            Assert.Equal(0.00m, response.Saldo);
        }

        [Fact]
        public async Task When_Invalid_Account_Return_Error()
        {
            // Arrange
            var query = new ConsultaSaldoCcQuery { IdContaCorrente = Guid.NewGuid().ToString() };
            var expectedError = new Result { Mensagem = "Conta não encontrada." };

            _contaCorrenteService.ValidarContaCorrente(query.IdContaCorrente).Returns(expectedError); 

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Equal("Conta não encontrada.", result.Mensagem);
        }

        [Fact]
        public async Task Handle_ComMovimentos_RetornaSaldoCalculado()
        {
            // Arrange
            var query = new ConsultaSaldoCcQuery { IdContaCorrente = Guid.NewGuid().ToString() };

            // Simulando uma conta válida
            _contaCorrenteService.ValidarContaCorrente(Arg.Any<string>()).Returns((Result)null);

            _movimentoQueryStore.BuscarMovimentosPorContaCorrente(Arg.Any<BuscarMovimentosPorCcIdRequest>())
                .Returns(new BuscarMovimentosPorCcIdResponse { Valor = 100m, TipoMovimento = "C" });

            var saldoCalculado = new CalcularSaldoContaCorrenteResponse
            {
                NumeroCc = 12345,
                TitularCc = "João",
                Saldo = 50m,
                DataHora = DateTime.Now
            };

            _movimentoQueryStore.CalcularSaldoContaCorrente(Arg.Any<CalcularSaldoContaCorrenteRequest>())
                .Returns(saldoCalculado);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Cálculo finalizado com sucesso!!", result.Mensagem);
            var response = Assert.IsType<CalcularSaldoContaCorrenteResponse>(result.Response);
            Assert.Equal(50m, response.Saldo);
        }
    }
}
