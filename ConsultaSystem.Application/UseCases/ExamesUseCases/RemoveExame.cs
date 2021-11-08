using MediatR;

namespace ConsultaSystem.Application.UseCases
{
    public class RemoveExame : IRequest<int>
    {
        public int Id;

       public RemoveExame(int id)
        {
            Id = id;
        } 
    }
}
