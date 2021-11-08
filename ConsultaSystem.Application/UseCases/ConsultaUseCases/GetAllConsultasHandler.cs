using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ConsultaSystem.Domain.Entities;
using ConsultaSystem.Domain.Interfaces.Repositories;
using MediatR;

namespace ConsultaSystem.Application.UseCases
{
    public class GetAllConsultasHandler : IRequestHandler<GetAllConsultas, IEnumerable<Consulta>>
    {
        IConsultaRepository _repository;
        public GetAllConsultasHandler(IConsultaRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<Consulta>> Handle(GetAllConsultas request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_repository.GetAll());
        }
    }
}


