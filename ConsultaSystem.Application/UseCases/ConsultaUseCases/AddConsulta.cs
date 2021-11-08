using MediatR;
using ConsultaSystem.Domain.Entities;

namespace ConsultaSystem.Application.UseCases
{
    public class AddConsulta : IRequest<Consulta>
    {
        public Consulta Consulta;

        public AddConsulta(Consulta consulta)
        {
            Consulta = consulta;
        }
    }
}
