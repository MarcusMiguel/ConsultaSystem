using System.Threading;
using System.Threading.Tasks;
using ConsultaSystem.Domain.Entities;
using ConsultaSystem.Domain.Interfaces.Repositories;
using MediatR;

namespace ConsultaSystem.Application.UseCases
{
    public class GetExameByIdHandler : IRequestHandler<GetExameById, Exame>
    {
        IExameRepository _repository;
        public GetExameByIdHandler(IExameRepository repository)
        {
            _repository = repository;
        }

        public Task<Exame> Handle(GetExameById request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_repository.GetById(request.Id));
        }
    }
}


