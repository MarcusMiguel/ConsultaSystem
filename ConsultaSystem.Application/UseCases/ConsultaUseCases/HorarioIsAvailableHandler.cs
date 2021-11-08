using System.Threading;
using System.Threading.Tasks;
using ConsultaSystem.Domain.Interfaces.Repositories;
using MediatR;

namespace ConsultaSystem.Application.UseCases
{
    public class HorarioIsAvailableHandler : IRequestHandler<HorarioIsAvailable, bool>
    {
        IConsultaRepository _repository;
        public HorarioIsAvailableHandler(IConsultaRepository repository)
        {
            _repository = repository;
        }

        public Task<bool> Handle(HorarioIsAvailable request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_repository.HorarioIsAvaliable(request.Horario));
        }

    }
}
