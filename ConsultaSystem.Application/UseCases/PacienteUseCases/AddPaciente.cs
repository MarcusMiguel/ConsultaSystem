using MediatR;
using ConsultaSystem.Domain.Entities;

namespace ConsultaSystem.Application.UseCases
{
    public class AddPaciente : IRequest<Paciente>
    {
        public Paciente Paciente;

        public AddPaciente(Paciente paciente)
        {
            Paciente = paciente;
        }
    }
}
