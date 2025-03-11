using Questao5.Application.Commands.Requests;

namespace Questao5.Tests.Builders
{
    public class MovimentoCcCommandBuilder
    {
        private string _chaveIdempotencia = Guid.NewGuid().ToString();
        private string _idContaCorrente = Guid.NewGuid().ToString();
        private string _tipoMovimento = "C"; //Crédito
        private decimal _valor = 0;

        public MovimentoCcCommandBuilder WhithoutChaveIdempotencia()
        {
            _chaveIdempotencia = string.Empty;
            return this;
        }

        public MovimentoCcCommandBuilder WhithoutIdContaCorrente()
        {
            _idContaCorrente = string.Empty;
            return this;
        }

        public MovimentoCcCommandBuilder WhithTipoMovimento(string tipoMovimento)
        {
            _tipoMovimento = tipoMovimento;
            return this;
        }

        public MovimentoCcCommandBuilder WhithValor(decimal valor)
        {
            _valor = valor;
            return this;
        }

        public MovimentoCcCommand Build()
        {
            return new MovimentoCcCommand
            {
                ChaveIdempotencia = _chaveIdempotencia,
                IdContaCorrente = _idContaCorrente,
                TipoMovimento = _tipoMovimento,
                Valor = _valor
            };
        }
    }
}
