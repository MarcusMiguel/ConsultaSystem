using System.Threading;
using System.Threading.Tasks;
using ConsultaSystem.Domain.Interfaces.Repositories;
using MediatR;

namespace ConsultaSystem.Application.UseCases
{
    public class ExistsCPFHandler : IRequestHandler<ExistsCPF, bool>
    {
        IPacienteRepository _repository;
        public ExistsCPFHandler(IPacienteRepository repository) 
        {
            _repository = repository;
        }

        public Task<bool> Handle(ExistsCPF request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_repository.ExistsCPF(request.CPF));
        }
    }
}
