using Microsoft.Extensions.Localization;
using Moq;
using NSubstitute;
using Questao5.Application.Services;
using Questao5.Domain.Entities.ContaCorrente;
using Questao5.Domain.Enumerators;
using Questao5.Domain.Language;
using Questao5.Infrastructure.Database.QueryStore.Requests;
using Questao5.Infrastructure.Database.QueryStore.Responses;
using Questao5.Tests.Builders;
using Xunit;

namespace Questao5.Tests
{
    public class MovimentoServiceTests
    {
        private readonly IContaCorrenteQueryStore _contaCorrenteQueryStore;
        private readonly Mock<IStringLocalizer<ManagerResources>> _localizerMock;
        private readonly ContaCorrenteService _movimentoService;

        public MovimentoServiceTests()
        {
            // Criando mocks
            _contaCorrenteQueryStore = Substitute.For<IContaCorrenteQueryStore>();

            _localizerMock = new Mock<IStringLocalizer<ManagerResources>>();

            _localizerMock.Setup(l => l[BusinessErrorsEnum.INVALID_ACCOUNT.ToString()])
                .Returns(new LocalizedString("INVALID_ACCOUNT", "Apenas contas correntes cadastradas podem receber movimentação!!"));

            _localizerMock.Setup(l => l[BusinessErrorsEnum.INACTIVE_ACCOUNT.ToString()])
                .Returns(new LocalizedString("INACTIVE_ACCOUNT", "Apenas contas correntes ativas podem receber movimentação!!"));

            // Inicializando o serviço com os mocks
            _movimentoService = new ContaCorrenteService(_contaCorrenteQueryStore, _localizerMock.Object);
        }

        [Fact]
        public async Task ValidarMovimento_Should_Return_Error_When_Account_Not_Found()
        {
            // Arrange
            string idContaCorrente = "12345";
            _contaCorrenteQueryStore
                .BuscarContaCorrenteAsync(Arg.Is<BuscarContaCorrenteRequest>(x => x.IdContaCorrente == idContaCorrente))
                .Returns(Task.FromResult<BuscarContaCorrenteResponse>(null));

            // Act
            var result = await _movimentoService.ValidarContaCorrente(idContaCorrente);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Sucesso);
            Assert.Equal("Apenas contas correntes cadastradas podem receber movimentação!!", result.Mensagem);
            Assert.Equal("INVALID_ACCOUNT", result.TipoErro);
        }

        [Fact]
        public async Task ValidarMovimento_Should_Return_Error_When_Account_Is_Inactive()
        {
            var contaCorrenteInativa = new BuscarContaCorrenteRequestBuilder().Build();

            _contaCorrenteQueryStore
                .BuscarContaCorrenteAsync(Arg.Is<BuscarContaCorrenteRequest>(x => x.IdContaCorrente == contaCorrenteInativa.IdContaCorrente))
                .Returns(Task.FromResult(new BuscarContaCorrenteResponse 
                { 
                    Ativo = false, 
                    IdContaCorrente = contaCorrenteInativa.IdContaCorrente
                })); 

            //Act
            var result = await _movimentoService.ValidarContaCorrente(contaCorrenteInativa.IdContaCorrente);

            Assert.NotNull(result);
            Assert.False(result.Sucesso);
            Assert.Equal("Apenas contas correntes ativas podem receber movimentação!!", result.Mensagem);
            Assert.Equal("INACTIVE_ACCOUNT", result.TipoErro);
        }

        [Fact]
        public async Task ValidarMovimento_ShouldReturnNull_WhenAccountIsActive()
        {
            //Arrange
            string idContaCorrente = Guid.NewGuid().ToString();
            var contaCorrenteAtiva = new BuscarContaCorrenteResponse { IdContaCorrente = idContaCorrente, Ativo = true };

            _contaCorrenteQueryStore
                .BuscarContaCorrenteAsync(Arg.Is<BuscarContaCorrenteRequest>(x => x.IdContaCorrente == idContaCorrente))
                .Returns(Task.FromResult(contaCorrenteAtiva));

            //Act
            var result = await _movimentoService.ValidarContaCorrente(idContaCorrente);

            Assert.Null(result);
        }
    }
}
