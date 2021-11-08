using System.Threading;
using System.Threading.Tasks;
using ConsultaSystem.Domain.Entities;
using ConsultaSystem.Domain.Interfaces.Repositories;
using MediatR;

namespace ConsultaSystem.Application.UseCases
{
    public class UpdateTipoDeExameHandler : IRequestHandler<UpdateTipoDeExame, TipoDeExame>
    {
        ITipoDeExameRepository _repository;
        public UpdateTipoDeExameHandler(ITipoDeExameRepository repository)
        {
            _repository = repository;
        }
        public Task<TipoDeExame> Handle(UpdateTipoDeExame request, CancellationToken cancellationToken)
        {
            _repository.Update(request.TipoDeExame);
            return Task.FromResult(request.TipoDeExame);
        }

    }
}
