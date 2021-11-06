using ConsultaSystem.Domain.Entities;
using System;

namespace ConsultaSystem.Domain.Interfaces.Services
{
    public interface IConsultaService : IGenericService<Consulta>
    {
        bool HorarioIsAvaliable(DateTime horario);
    }
}
