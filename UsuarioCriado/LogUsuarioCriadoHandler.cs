using MediatR;

namespace MediatrFluentValidation.UsuarioCriado
{
    public class LogUsuarioCriadoHandler : INotificationHandler<UsuarioCriadoNotificacao>
    {
        public Task Handle(UsuarioCriadoNotificacao notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"[LOG] Usuário criado: {notification.Nome} (ID: {notification.UsuarioId})");
            return Task.CompletedTask;
        }
    }
}
