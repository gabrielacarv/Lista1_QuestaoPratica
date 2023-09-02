using System.ComponentModel.DataAnnotations;

namespace QuestaoPratica.Models.Validations
{
    public class RaValidationsAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("CPF é obrigatório!");
            }

            if(value != null)
            {
                string ra = value.ToString();

                if (!ra.StartsWith("RA") || ra.Length != 8 || !ra.Substring(2).All(char.IsDigit))
                {
                    return new ValidationResult("O campo RA deve começar com as letras RA seguidas por 6 dígitos.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
