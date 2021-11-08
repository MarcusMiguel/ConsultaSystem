using MediatR;
using ConsultaSystem.Domain.Entities;

namespace ConsultaSystem.Application.UseCases
{
    public class GetTipoDeExameById : IRequest<TipoDeExame>
    {
        public int Id;

        public GetTipoDeExameById(int id)
        {
            Id = id;
        }
    }
}
