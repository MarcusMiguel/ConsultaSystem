using MediatR;
using ConsultaSystem.Domain.Entities;

namespace ConsultaSystem.Application.UseCases
{
    public class AddExame : IRequest<Exame>
    {
        public Exame Exame;
        public AddExame(Exame exame)
        {
            Exame = exame;
        }
    }
}
