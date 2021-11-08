using MediatR;
using ConsultaSystem.Domain.Entities;

namespace ConsultaSystem.Application.UseCases
{
    public class UpdateTipoDeExame : IRequest<TipoDeExame>
    {
        public TipoDeExame TipoDeExame;

        public UpdateTipoDeExame(TipoDeExame tipoDeExame)
        {
            TipoDeExame = tipoDeExame;
        }
    }
}
