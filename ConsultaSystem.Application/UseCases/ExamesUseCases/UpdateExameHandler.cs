using System.Threading;
using System.Threading.Tasks;
using ConsultaSystem.Domain.Entities;
using ConsultaSystem.Domain.Interfaces.Repositories;
using MediatR;

namespace ConsultaSystem.Application.UseCases
{
    public class UpdateExameHandler : IRequestHandler<UpdateExame, Exame>
    {
        IExameRepository _repository;
        public UpdateExameHandler(IExameRepository repository)
        {
            _repository = repository;
        }
        public Task<Exame> Handle(UpdateExame request, CancellationToken cancellationToken)
        {
            _repository.Update(request.Exame);
            return Task.FromResult(request.Exame);
        }

    }
}
