using MediatR;
using ConsultaSystem.Domain.Entities;


namespace ConsultaSystem.Application.UseCases
{
    public class GetPacienteById : IRequest<Paciente>
    {
        public int Id;

        public GetPacienteById(int id)
        {
            Id = id;
        }
    }
}
