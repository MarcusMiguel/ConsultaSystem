using System.Threading;
using System.Threading.Tasks;
using ConsultaSystem.Domain.Entities;
using ConsultaSystem.Domain.Interfaces.Repositories;
using MediatR;

namespace ConsultaSystem.Application.UseCases
{
    public class AddTipoDeExameHandler : IRequestHandler<AddTipoDeExame, TipoDeExame>
    {
        ITipoDeExameRepository _repository;
        public AddTipoDeExameHandler(ITipoDeExameRepository repository)
        {
            _repository = repository;
        }

        public Task<TipoDeExame> Handle(AddTipoDeExame request, CancellationToken cancellationToken)
        {
            _repository.Add(request.TipoDeExame);
            return Task.FromResult(request.TipoDeExame); 
        }

    }
}
