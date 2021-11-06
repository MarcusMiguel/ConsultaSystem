using ConsultaSystem.Domain.Entities;

namespace ConsultaSystem.Domain.Interfaces.Services
{
    public interface IPacienteService : IGenericService<Paciente>
    {
        bool ExistsCPF(string cpf);
    }
}
