using MediatR;

namespace ConsultaSystem.Application.UseCases
{
    public class RemoveConsulta : IRequest<int>
    {
        public int Id;

       public RemoveConsulta(int id)
        {
            Id = id;
        } 
    }
}
