using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ConsultaSystem.Domain.Entities;
using ConsultaSystem.Domain.Interfaces.Repositories;
using MediatR;

namespace ConsultaSystem.Application.UseCases
{
    public class GetAllExamesHandler : IRequestHandler<GetAllExames, IEnumerable<Exame>>
    {
        IExameRepository _repository;
        public GetAllExamesHandler(IExameRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<Exame>> Handle(GetAllExames request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_repository.GetAll());
        }
    }
}


