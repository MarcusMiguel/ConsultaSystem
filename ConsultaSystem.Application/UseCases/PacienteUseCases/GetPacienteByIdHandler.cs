using System.Threading;
using System.Threading.Tasks;
using ConsultaSystem.Domain.Entities;
using ConsultaSystem.Domain.Interfaces.Repositories;
using MediatR;

namespace ConsultaSystem.Application.UseCases
{
    public class GetPacienteByIdHandler : IRequestHandler<GetPacienteById, Paciente>
    {
        IPacienteRepository _repository;
        public GetPacienteByIdHandler(IPacienteRepository repository)
        {
            _repository = repository;
        }

        public Task<Paciente> Handle(GetPacienteById request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_repository.GetById(request.Id));
        }
    }
}


