using MediatR;
using ConsultaSystem.Domain.Entities;

namespace ConsultaSystem.Application.UseCases
{
    public class GetConsultaById : IRequest<Consulta>
    {
        public int Id;

        public GetConsultaById(int id)
        {
            Id = id;
        }
    }
}
