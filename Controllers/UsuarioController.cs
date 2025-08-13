using MediatR;
using MediatrFluentValidation.Command;
using MediatrFluentValidation.UsuarioCriado;
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


        [HttpGet("GeraExceptionDeTeste")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GeraExceptionDeTeste()
        {
            throw new Exception("Gerando uma Exception no controller");
        }

        [HttpPost("GeraErroString")]
        public async Task<IActionResult> GeraErroString([FromBody] string nome)
        {
            // Validação de entrada (exemplo simplificado)
            if (nome == null || string.IsNullOrEmpty(nome))
            {
                return BadRequest("Dados inválidos.");
            }
            return Ok("Usuário inserido com sucesso!");
            // Chama o serviço para criar o produto e obtém o resultado
            // var resultado = await _produtoService.CriarProdutoAsync(produtoDto);

            //// Retorna a resposta com o ID do produto criado ou erro, conforme o caso
            //return resultado.Match<IActionResult>(
            //    sucesso => Ok(sucesso),
            //    erro => StatusCode(erro.Codigo, erro.Descricao)

        }





        [HttpPost("UsuarioCriadoNotificacao")]
        public async Task<IActionResult> UsuarioCriadoNotificacao([FromBody] UsuarioDto usuarioDto)
        {
            // Simulando criação do usuário
            var novoId = new Random().Next(1, 1000);

            // Publica a notificação para todos os handlers registrados
            await _mediator.Publish(new UsuarioCriadoNotification(novoId, usuarioDto.Nome));

            return Ok(new { Id = novoId, Mensagem = "Usuário criado com sucesso!" });
        }

        public class UsuarioDto
        {
            public string Nome { get; set; }
        }

    }
    
}
