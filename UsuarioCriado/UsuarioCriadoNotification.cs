using MediatR;

namespace MediatrFluentValidation.UsuarioCriado
{
    public class UsuarioCriadoNotificacao : INotification
    {
        public int UsuarioId { get; }
        public string Nome { get; }

        public UsuarioCriadoNotificacao(int usuarioId, string nome)
        {
            UsuarioId = usuarioId;
            Nome = nome;
        }
    }
}
