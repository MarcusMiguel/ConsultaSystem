using ConsultaSystem.Domain.Entities;
using System;

namespace ConsultaSystem.Domain.Interfaces.Repositories
{
    public interface IConsultaRepository : IGenericRepository<Consulta>
    {
        bool HorarioIsAvaliable(DateTime horario);
    }
}
