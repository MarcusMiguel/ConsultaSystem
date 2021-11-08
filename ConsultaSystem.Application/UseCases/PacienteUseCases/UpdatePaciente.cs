using MediatR;
using ConsultaSystem.Domain.Entities;

namespace ConsultaSystem.Application.UseCases
{
    public class UpdatePaciente : IRequest<Paciente>
    {
        public Paciente Paciente;

        public UpdatePaciente(Paciente paciente)
        {
            Paciente = paciente;
        }
    }
}
