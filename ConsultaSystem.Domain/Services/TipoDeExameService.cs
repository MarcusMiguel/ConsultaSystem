using ConsultaSystem.Domain.Entities;
using ConsultaSystem.Domain.Interfaces.Repositories;
using ConsultaSystem.Domain.Interfaces.Services;

namespace ConsultaSystem.Domain.Services
{
    public class TipoDeExameService : GenericService<TipoDeExame>, ITipoDeExameService
    {
        private readonly ITipoDeExameRepository _repository;

        public TipoDeExameService(ITipoDeExameRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
