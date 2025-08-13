using MediatR;

namespace MediatrFluentValidation.Notificacao
{
    public class LogRegistroUsuarioHandler : INotificationHandler<UsuarioRegistradoNotification>
    {
        private readonly ILogger<LogRegistroUsuarioHandler> _logger;
        public LogRegistroUsuarioHandler(ILogger<LogRegistroUsuarioHandler> logger) => _logger = logger;

        public Task Handle(UsuarioRegistradoNotification notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Usuário registrado: {Nome}", notification.UsuarioNome);
            return Task.CompletedTask;
        }
    }

}
