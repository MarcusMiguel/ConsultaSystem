using System.Threading;
using System.Threading.Tasks;
using ConsultaSystem.Domain.Entities;
using ConsultaSystem.Domain.Interfaces.Repositories;
using MediatR;

namespace ConsultaSystem.Application.UseCases
{
    public class AddExameHandler : IRequestHandler<AddExame, Exame>
    {
        IExameRepository _repository;
        public AddExameHandler(IExameRepository repository)
        {
            _repository = repository;
        }

        public Task<Exame> Handle(AddExame request, CancellationToken cancellationToken)
        {
            _repository.Add(request.Exame);
            return Task.FromResult(request.Exame); 
        }

    }
}
