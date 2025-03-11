using Swashbuckle.AspNetCore.Annotations;

namespace Questao5.Application
{
    [SwaggerSchema(Description = "Representa o resultado de uma operação.")]
    public class Result
    {
        [SwaggerSchema(Description = "Indica se a operação foi bem-sucedida.")]
        public bool Sucesso { get; set; }

        [SwaggerSchema(Description = "Mensagem associada ao resultado da operação, se houver.")]
        public string? Mensagem { get; set; }

        [SwaggerSchema(Description = "Tipo do erro ocorrido, se houver.")]
        public string? TipoErro { get; set; }

        [SwaggerSchema(Description = "Objeto que contém a resposta da operação, se houver.")]
        public object? Response { get; set; }

        public Result(bool sucesso = true, string? menssagem = null, string? tipoErro = null, object? response = null)
        {
            Sucesso = sucesso;
            Mensagem = menssagem;
            Response = response;
            TipoErro = tipoErro;
        }
    }
}
