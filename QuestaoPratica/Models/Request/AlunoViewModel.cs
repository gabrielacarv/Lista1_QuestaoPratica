using QuestaoPratica.Models.Validations;
using System.ComponentModel.DataAnnotations;

namespace QuestaoPratica.Models.Request
{
    public class AlunoViewModel
    {
        public int Codigo { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório!")]
        public string Nome { get; set; }


        [RaValidationsAttribute]
        public string Ra { get; set; }


        [Required(ErrorMessage = "Email é obrigatório!")]
        public string Email { get; set; }


        [CpfValidationAttribute]
        public string Cpf { get; set; }


        [Required(ErrorMessage = "O campo Ativo é obrigatório.")]
        public bool Ativo { get; set; }
    }
}