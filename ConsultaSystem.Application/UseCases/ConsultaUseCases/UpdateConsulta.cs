using MediatR;
using ConsultaSystem.Domain.Entities;

namespace ConsultaSystem.Application.UseCases
{
    public class UpdateConsulta : IRequest<Consulta>
    {
        public Consulta Consulta;

        public UpdateConsulta(Consulta consulta)
        {
            Consulta = consulta;
        }
    }
}
