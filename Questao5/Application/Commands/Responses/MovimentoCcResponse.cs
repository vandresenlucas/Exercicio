namespace Questao5.Application.Commands.Responses
{
    public class MovimentoCcResponse
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public string TipoErro { get; set; }
        public string IdMovimento { get; set; }
    }
}
