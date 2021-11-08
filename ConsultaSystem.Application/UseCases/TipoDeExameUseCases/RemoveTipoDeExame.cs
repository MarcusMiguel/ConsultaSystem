using MediatR;

namespace ConsultaSystem.Application.UseCases
{
    public class RemoveTipoDeExame : IRequest<int>
    {
        public int Id;

       public RemoveTipoDeExame(int id)
        {
            Id = id;
        } 
    }
}
