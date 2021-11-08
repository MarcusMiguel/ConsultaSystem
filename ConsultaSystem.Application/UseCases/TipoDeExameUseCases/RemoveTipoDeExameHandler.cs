using System.Threading;
using System.Threading.Tasks;
using ConsultaSystem.Domain.Interfaces.Repositories;
using MediatR;

namespace ConsultaSystem.Application.UseCases
{
    public class RemoveTipoDeExameHandler : IRequestHandler<RemoveTipoDeExame, int>
    {
        ITipoDeExameRepository _repository;
        public RemoveTipoDeExameHandler(ITipoDeExameRepository repository)
        {
            _repository = repository;
        }

        public Task<int> Handle(RemoveTipoDeExame request, CancellationToken cancellationToken)
        {
            _repository.Remove(request.Id);
            return Task.FromResult(request.Id);
        }
    }
}

