using FluentValidation;

namespace MediatrFluentValidation.Command
{
    public class InserirUsuarioCommandValidator : AbstractValidator<InserirUsuarioCommand>
    {
        public InserirUsuarioCommandValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("Validator - O nome é obrigatório.")
                .MinimumLength(3).WithMessage("Validator - O nome deve ter pelo menos 3 caracteres.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Validator - O e-mail é obrigatório.")
                .EmailAddress().WithMessage("Validator - O e-mail deve ser válido.");


            RuleFor(x => x.Senha)
                .NotEmpty().WithMessage("Validator - A senha é obrigatória.")
                .MinimumLength(6).WithMessage("Validator - A senha deve ter no mínimo 6 caracteres.");


            // Validação do enum SexoEnum
            RuleFor(x => x.Sexo)
                .IsInEnum().WithMessage("Validator - O campo Sexo deve ser 'Feminino' ou 'Masculino'.");



            RuleFor(x => x.Sexo)
                .Must(value => value == "F" || value == "M")
                .WithMessage("Validator - O campo Sexo deve ser 'F' (Feminino) ou 'M' (Masculino).");


        }



    }
}
