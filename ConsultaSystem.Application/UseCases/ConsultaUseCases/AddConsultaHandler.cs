using System.Threading;
using System.Threading.Tasks;
using ConsultaSystem.Domain.Entities;
using ConsultaSystem.Domain.Interfaces.Repositories;
using MediatR;

namespace ConsultaSystem.Application.UseCases
{
    public class AddConsultaHandler : IRequestHandler<AddConsulta, Consulta>
    {
        IConsultaRepository _repository;
        public AddConsultaHandler(IConsultaRepository repository)
        {
            _repository = repository;
        }

        public Task<Consulta> Handle(AddConsulta request, CancellationToken cancellationToken)
        {
            _repository.Add(request.Consulta);
            return Task.FromResult(request.Consulta); 
        }

    }
}
