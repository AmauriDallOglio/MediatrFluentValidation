using MediatR;

namespace MediatrFluentValidation.Notificacao
{
    public record UsuarioRegistradoNotification(string UsuarioNome) : INotification;
}
