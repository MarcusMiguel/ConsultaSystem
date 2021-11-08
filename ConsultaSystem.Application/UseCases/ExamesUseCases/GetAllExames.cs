using System.Collections.Generic;
using MediatR;
using ConsultaSystem.Domain.Entities;


namespace ConsultaSystem.Application.UseCases
{
    public class GetAllExames : IRequest<IEnumerable<Exame>>
    {
    }
}
