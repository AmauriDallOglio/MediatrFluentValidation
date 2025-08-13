using MediatR;

namespace MediatrFluentValidation.UsuarioCriado
{
    public class EnviarEmailBoasVindasHandler : INotificationHandler<UsuarioCriadoNotificacao>
    {
        public Task Handle(UsuarioCriadoNotificacao notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"[EMAIL] Enviando boas-vindas para {notification.Nome}...");
            return Task.CompletedTask;
        }
    }
}
