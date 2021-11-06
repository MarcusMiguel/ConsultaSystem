using ConsultaSystem.Domain.Entities;
using ConsultaSystem.Domain.Interfaces.Repositories;
using ConsultaSystem.Domain.Interfaces.Services;

namespace ConsultaSystem.Domain.Services
{
    public class ExameService : GenericService<Exame>, IExameService
    {
        private readonly IExameRepository _repository;

        public ExameService(IExameRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
