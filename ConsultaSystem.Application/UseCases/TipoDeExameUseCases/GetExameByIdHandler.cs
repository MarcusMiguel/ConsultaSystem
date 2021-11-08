using System.Threading;
using System.Threading.Tasks;
using ConsultaSystem.Domain.Entities;
using ConsultaSystem.Domain.Interfaces.Repositories;
using MediatR;

namespace ConsultaSystem.Application.UseCases
{
    public class GetTipoDeExameByIdHandler : IRequestHandler<GetTipoDeExameById, TipoDeExame>
    {
        ITipoDeExameRepository _repository;
        public GetTipoDeExameByIdHandler(ITipoDeExameRepository repository)
        {
            _repository = repository;
        }

        public Task<TipoDeExame> Handle(GetTipoDeExameById request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_repository.GetById(request.Id));
        }
    }
}


