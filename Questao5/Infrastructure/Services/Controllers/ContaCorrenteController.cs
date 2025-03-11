using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Queries.Requests;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Questao5.Infrastructure.Services.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContaCorrenteController : Controller
    {
        private readonly IMediator _mediator;

        public ContaCorrenteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("movimentar")]
        [SwaggerOperation(
            Summary = "Responsável por fazer a movimentação de conta corrente",
            Description = "Realiza o a movimentação de conta corrente fazendo as validações necessárias.",
            OperationId = "movimentar",
            Tags = new[] { "Movimentação de conta corrente" }
        )]
        [ProducesResponseType(typeof(Result), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Result), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> MovimentarConta([FromBody] MovimentoCcCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);

                if (result.Sucesso)
                    return Ok(result);

                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new Result(false, ex.Message));
            }
        }

        [HttpGet("saldo/{idContaCorrente}")]
        [SwaggerOperation(
            Summary = "Saldo da conta corrente",
            Description = "Responsável por calcular o saldo da conta corrente.",
            OperationId = "saldo",
            Tags = new[] { "Cálculo de saldo da conta corrente" }
        )]
        [ProducesResponseType(typeof(Result), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Result), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CalcularSaldo(
            [FromRoute, SwaggerParameter(Description = "Identificado da conta corrente")] string idContaCorrente)
        {
            try
            {
                var query = new ConsultaSaldoCcQuery { IdContaCorrente = idContaCorrente };
                var result = await _mediator.Send(query);

                if (result.Sucesso)
                    return Ok(result);

                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new Result(false, ex.Message));
            }
        }
    }
}
