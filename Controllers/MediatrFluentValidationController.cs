using MediatR;
using MediatrFluentValidation.Aplicacao.Command;
using Microsoft.AspNetCore.Mvc;

namespace MediatrFluentValidation.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsuarioController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> InserirUsuario([FromBody] InserirUsuarioCommand command)
        {
            var resultado = await _mediator.Send(command);
            if (resultado)
            {
                //var mensagensDeErro = resultado.Errors.Select(e => e.ErrorMessage).ToList();
                return Ok("Usuário inserido com sucesso!");
            }

   


            return BadRequest("Falha ao inserir o usuário.");
        }

   



    }
    
}
