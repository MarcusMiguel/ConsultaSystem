using ConsultaSystem.Domain.Entities;
using ConsultaSystem.Domain.Interfaces.Repositories;
using ConsultaSystem.Domain.Interfaces.Services;
using System;

namespace ConsultaSystem.Domain.Services
{
    public class ConsultaService : GenericService<Consulta>, IConsultaService
    {
        private readonly IConsultaRepository _repository;

        public ConsultaService(IConsultaRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public bool HorarioIsAvaliable(DateTime horario)
        {
           return _repository.HorarioIsAvaliable(horario);
        }
    }
}
