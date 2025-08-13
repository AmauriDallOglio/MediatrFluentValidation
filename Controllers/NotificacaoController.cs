using MediatR;
using MediatrFluentValidation.Notificacao;
using Microsoft.AspNetCore.Mvc;

namespace MediatrFluentValidation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificacaoController : ControllerBase
    {
        private readonly IMediator _mediator;
        public NotificacaoController(IMediator mediator) => _mediator = mediator;

        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar(string nome)
        {
            // Lógica de registro de usuário (persistência...)

            // Publica a notificação
            await _mediator.Publish(new UsuarioRegistradoNotification(nome));
            return Ok();
        }
    }
}
