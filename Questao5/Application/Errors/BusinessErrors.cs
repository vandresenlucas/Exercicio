namespace Questao5.Application.Errors
{
    public static class BusinessErrors 
    {
        public const string INVALID_ACCOUNT = "Apenas contas correntes cadastradas podem receber movimentação!!";
        public const string INACTIVE_ACCOUNT = "Apenas contas correntes ativas podem receber movimentação!!";
        public const string INVALID_VALUE = "Apenas valores positivos podem ser recebidos!!";
        public const string INVALID_TYPE = "Apenas os tipos “débito” ou “crédito” podem ser aceitos!!";
        public const string EMPTY_ID_IDEMPOTENCIA = "Chave de idempotencia é obrigatória!!";
        public const string EMPTY_ID_CC = "Id da conta corrente é obrigatório!!";
        public const string INVALID_MOVIMENT_TYPE = "Tipo de movimento inválido!!";
        public const string INVALID_AMOUNT = "Valor inválido!!";
    }
}
