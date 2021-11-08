using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ConsultaSystem.Domain.Entities;
using ConsultaSystem.Domain.Interfaces.Repositories;
using MediatR;

namespace ConsultaSystem.Application.UseCases
{
    public class GetAllTiposDeExamesHandler : IRequestHandler<GetAllTiposDeExames, IEnumerable<TipoDeExame>>
    {
        ITipoDeExameRepository _repository;
        public GetAllTiposDeExamesHandler(ITipoDeExameRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<TipoDeExame>> Handle(GetAllTiposDeExames request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_repository.GetAll());
        }
    }
}


