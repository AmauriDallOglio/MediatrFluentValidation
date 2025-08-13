using MediatR;

namespace MediatrFluentValidation.Notificacao
{
    public class EmailBoasVindasHandler : INotificationHandler<UsuarioRegistradoNotification>
    {
        private readonly ILogger<EmailBoasVindasHandler> _logger;
        public EmailBoasVindasHandler(ILogger<EmailBoasVindasHandler> logger) => _logger = logger;

        public Task Handle(UsuarioRegistradoNotification notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Email de boas-vindas enviado para: {Nome}", notification.UsuarioNome);
            return Task.CompletedTask;
        }
    }
}
