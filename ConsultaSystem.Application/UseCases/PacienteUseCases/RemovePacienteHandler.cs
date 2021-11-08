using System.Threading;
using System.Threading.Tasks;
using ConsultaSystem.Domain.Interfaces.Repositories;
using MediatR;

namespace ConsultaSystem.Application.UseCases
{
    public class RemovePacienteHandler : IRequestHandler<RemovePaciente, int>
    {
        IPacienteRepository _repository;
        public RemovePacienteHandler(IPacienteRepository repository)
        {
            _repository = repository;
        }

        public Task<int> Handle(RemovePaciente request, CancellationToken cancellationToken)
        {
            _repository.Remove(request.Id);
            return Task.FromResult(request.Id);
        }
    }
}

