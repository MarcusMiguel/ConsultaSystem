using MediatR;
using ConsultaSystem.Domain.Entities;

namespace ConsultaSystem.Application.UseCases
{
    public class AddTipoDeExame : IRequest<TipoDeExame>
    {
        public TipoDeExame TipoDeExame;

        public AddTipoDeExame(TipoDeExame tipoDeExame)
        {
            TipoDeExame = tipoDeExame;
        }
    }
}
