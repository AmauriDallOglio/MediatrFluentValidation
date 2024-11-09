using MediatR;

namespace MediatrFluentValidation.Command
{
    public class InserirUsuarioCommandHandler : IRequestHandler<InserirUsuarioCommand, bool>
    {
        public Task<bool> Handle(InserirUsuarioCommand request, CancellationToken cancellationToken)
        {
            // Lógica para inserir o usuário (exemplo fictício)
            if (string.IsNullOrWhiteSpace(request.Nome) || string.IsNullOrWhiteSpace(request.Email))
            {
                return Task.FromResult(false);
            }

            // Código de inserção do usuário no banco de dados aqui
            return Task.FromResult(true);
        }
    }
}
