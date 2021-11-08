using ConsultaSystem.Domain.Entities;
using ConsultaSystem.Domain.Interfaces.Repositories;
using System;
using System.Linq;

namespace ConsultaSystem.Infra.Repositories
{
    public class ConsultaRepository : GenericRepository<Consulta>, IConsultaRepository
    {
        public bool HorarioIsAvaliable(DateTime horario)
        {
            var conflict = _db.Consultas.Where(o => o.Horario == horario);
            if (conflict.Count() > 0)
            {
                return false;
            }
            return true;
        }
    }
}
