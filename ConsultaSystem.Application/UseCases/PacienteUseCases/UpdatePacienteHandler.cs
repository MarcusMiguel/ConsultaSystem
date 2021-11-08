using System.Threading;
using System.Threading.Tasks;
using ConsultaSystem.Domain.Entities;
using ConsultaSystem.Domain.Interfaces.Repositories;
using MediatR;

namespace ConsultaSystem.Application.UseCases
{
    public class UpdatePacienteHandler : IRequestHandler<UpdatePaciente, Paciente>
    {
        IPacienteRepository _repository;
        public UpdatePacienteHandler(IPacienteRepository repository)
        {
            _repository = repository;
        }
        public Task<Paciente> Handle(UpdatePaciente request, CancellationToken cancellationToken)
        {
            _repository.Update(request.Paciente);
            return Task.FromResult(request.Paciente);
        }

    }
}
