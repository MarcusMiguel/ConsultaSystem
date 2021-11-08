using MediatR;

namespace ConsultaSystem.Application.UseCases
{
    public class RemovePaciente : IRequest<int>
    {
        public int Id;

       public RemovePaciente(int id)
        {
            Id = id;
        } 
    }
}
