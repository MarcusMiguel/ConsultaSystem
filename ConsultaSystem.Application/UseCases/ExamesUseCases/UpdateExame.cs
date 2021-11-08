using MediatR;
using ConsultaSystem.Domain.Entities;

namespace ConsultaSystem.Application.UseCases
{
    public class UpdateExame : IRequest<Exame>
    {
        public Exame Exame;

        public UpdateExame(Exame exame)
        {
            Exame = exame;
        }
    }
}
