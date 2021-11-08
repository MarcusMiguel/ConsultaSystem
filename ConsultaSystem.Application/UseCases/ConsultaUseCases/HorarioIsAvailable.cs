using MediatR;
using System;

namespace ConsultaSystem.Application.UseCases
{
    public class HorarioIsAvailable : IRequest<bool>
    {
        public DateTime Horario;

        public HorarioIsAvailable(DateTime horario)
        {
            Horario = horario;
        }
    }
}
