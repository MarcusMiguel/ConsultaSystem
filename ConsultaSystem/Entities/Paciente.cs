using System;
using System.ComponentModel.DataAnnotations;

namespace ConsultaSystem.Entities
{
    public class Paciente
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [CustomValidationCPF(ErrorMessage = "CPF inválido")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public Sexo Sexo { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^[2-9]\d{1}-\d{4,5}-\d{4}$", ErrorMessage = "Você deve digitar um telefone no formato ##-#####-#### ou ##-####-####")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; }
    }
}