using System.Threading;
using System.Threading.Tasks;
using ConsultaSystem.Domain.Interfaces.Repositories;
using MediatR;

namespace ConsultaSystem.Application.UseCases
{
    public class RemoveConsultaHandler : IRequestHandler<RemoveConsulta, int>
    {
        IConsultaRepository _repository;
        public RemoveConsultaHandler(IConsultaRepository repository)
        {
            _repository = repository;
        }

        public Task<int> Handle(RemoveConsulta request, CancellationToken cancellationToken)
        {
            _repository.Remove(request.Id);
            return Task.FromResult(request.Id);
        }
    }
}

