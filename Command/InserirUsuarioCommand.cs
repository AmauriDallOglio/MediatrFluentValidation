using MediatR;
using System.ComponentModel.DataAnnotations;

namespace MediatrFluentValidation.Command
{
    public class InserirUsuarioCommand : IRequest<bool>
    {
        [Required(ErrorMessage = "Nome é obrigatório")]
        [MaxLength(300, ErrorMessage = "Nome não pode ter mais de 300 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "O e-mail deve ser válido!")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Senha é obrigatório")]
        [MinLength(3, ErrorMessage = "A senha deve ter no mínimo 3 caracteres.")]
        public string Senha { get; set; } = string.Empty;


        //[JsonConverter(typeof(JsonStringEnumConverter))]
        [Required(ErrorMessage = "Sexo é obrigatório")]
        public string Sexo { get; set; }

    }


    public enum SexoEnum
    {
        [Display(Name = "Feminino")]
        F = 1,

        [Display(Name = "Masculino")]
        M = 2
    }



}
