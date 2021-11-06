using ConsultaSystem.Domain.Entities;
using ConsultaSystem.Domain.Interfaces.Repositories;
using System.Linq;

namespace ConsultaSystem.Infra.Repositories
{
    public class PacienteRepository : GenericRepository<Paciente>, IPacienteRepository
    {
        public bool ExistsCPF(string cpf)
        {
            var cpfIgual = _db.Pacientes.Where(o => o.CPF == cpf);
            if (cpfIgual.Count() >0)
            {
                return true;
            }
                return false;
        }
    }
}
