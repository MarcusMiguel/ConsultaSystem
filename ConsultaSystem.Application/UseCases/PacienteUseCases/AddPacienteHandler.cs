using System.Threading;
using System.Threading.Tasks;
using ConsultaSystem.Domain.Entities;
using ConsultaSystem.Domain.Interfaces.Repositories;
using MediatR;

namespace ConsultaSystem.Application.UseCases
{
    public class AddPacienteHandler : IRequestHandler<AddPaciente, Paciente>
    {
        IPacienteRepository _repository;
        public AddPacienteHandler(IPacienteRepository repository)
        {
            _repository = repository;
        }

        public Task<Paciente> Handle(AddPaciente request, CancellationToken cancellationToken)
        {
            _repository.Add(request.Paciente);
            return Task.FromResult(request.Paciente); 
        }

    }
}
