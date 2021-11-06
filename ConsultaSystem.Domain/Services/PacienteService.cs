using ConsultaSystem.Domain.Entities;
using ConsultaSystem.Domain.Interfaces.Repositories;
using ConsultaSystem.Domain.Interfaces.Services;

namespace ConsultaSystem.Domain.Services
{
    public class PacienteService : GenericService<Paciente>, IPacienteService
    {
        private readonly IPacienteRepository _repository;

        public PacienteService(IPacienteRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public bool ExistsCPF(string cpf)
        {
            return _repository.ExistsCPF(cpf);
        }
    }
}
