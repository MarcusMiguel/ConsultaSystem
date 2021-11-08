using System.Collections.Generic;
using MediatR;
using ConsultaSystem.Domain.Entities;


namespace ConsultaSystem.Application.UseCases
{
    public class GetAllTiposDeExames : IRequest<IEnumerable<TipoDeExame>>
    {
    }
}
