﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
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
        [ProducesResponseType(typeof(MovimentoCcResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(MovimentoCcResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> MovimentarConta([FromBody] MovimentoCcCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);

                if (result.Sucesso)
                    return Ok(new { movimentoId = result.IdMovimento });

                return BadRequest(new { mensagem = result.Mensagem, tipo = result.TipoErro });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
