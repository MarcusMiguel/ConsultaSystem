using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ConsultaSystem.Application.UseCases
{
    public class ExistsCPF : IRequest<bool>
    {
        [Required]
        public string CPF { get; set; } = "";

        public ExistsCPF(string cpf)
        {
            CPF = cpf;
        }
    }
}
