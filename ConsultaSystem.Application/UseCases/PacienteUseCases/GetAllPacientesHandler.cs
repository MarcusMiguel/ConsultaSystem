using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ConsultaSystem.Domain.Entities;
using ConsultaSystem.Domain.Interfaces.Repositories;
using MediatR;

namespace ConsultaSystem.Application.UseCases
{
    public class GetAllPacientesHandler : IRequestHandler<GetAllPacientes, IEnumerable<Paciente>>
    {
        IPacienteRepository _repository;
        public GetAllPacientesHandler(IPacienteRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<Paciente>> Handle(GetAllPacientes request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_repository.GetAll());
        }
    }
}


