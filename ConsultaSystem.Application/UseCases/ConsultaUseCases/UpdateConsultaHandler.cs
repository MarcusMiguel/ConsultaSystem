using System.Threading;
using System.Threading.Tasks;
using ConsultaSystem.Domain.Entities;
using ConsultaSystem.Domain.Interfaces.Repositories;
using MediatR;

namespace ConsultaSystem.Application.UseCases
{
    public class UpdateConsultaHandler : IRequestHandler<UpdateConsulta, Consulta>
    {
        IConsultaRepository _repository;
        public UpdateConsultaHandler(IConsultaRepository repository)
        {
            _repository = repository;
        }
        public Task<Consulta> Handle(UpdateConsulta request, CancellationToken cancellationToken)
        {
            _repository.Update(request.Consulta);
            return Task.FromResult(request.Consulta);
        }

    }
}
