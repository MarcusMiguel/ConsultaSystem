using System.Threading;
using System.Threading.Tasks;
using ConsultaSystem.Domain.Interfaces.Repositories;
using MediatR;

namespace ConsultaSystem.Application.UseCases
{
    public class RemoveExameHandler : IRequestHandler<RemoveExame, int>
    {
        IExameRepository _repository;
        public RemoveExameHandler(IExameRepository repository)
        {
            _repository = repository;
        }

        public Task<int> Handle(RemoveExame request, CancellationToken cancellationToken)
        {
            _repository.Remove(request.Id);
            return Task.FromResult(request.Id);
        }
    }
}

