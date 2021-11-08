using MediatR;
using ConsultaSystem.Domain.Entities;

namespace ConsultaSystem.Application.UseCases
{
    public class GetExameById : IRequest<Exame>
    {
        public int Id;

        public GetExameById(int id)
        {
            Id = id;
        }
    }
}
