using ConsultaSystem.Domain.Entities;

namespace ConsultaSystem.Domain.Interfaces.Repositories
{
    public interface IPacienteRepository : IGenericRepository<Paciente>
    {
        bool ExistsCPF(string cpf);
    }
}
