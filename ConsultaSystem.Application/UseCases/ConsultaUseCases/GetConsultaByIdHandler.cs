using System.Threading;
using System.Threading.Tasks;
using ConsultaSystem.Domain.Entities;
using ConsultaSystem.Domain.Interfaces.Repositories;
using MediatR;

namespace ConsultaSystem.Application.UseCases
{
    public class GetConsultaByIdHandler : IRequestHandler<GetConsultaById, Consulta>
    {
        IConsultaRepository _repository;
        public GetConsultaByIdHandler(IConsultaRepository repository)
        {
            _repository = repository;
        }

        public Task<Consulta> Handle(GetConsultaById request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_repository.GetById(request.Id));
        }
    }
}


