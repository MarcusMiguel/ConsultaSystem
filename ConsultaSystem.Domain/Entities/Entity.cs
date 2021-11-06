using System.ComponentModel.DataAnnotations;

namespace ConsultaSystem.Domain.Entities
{
    public abstract class Entity
    {
        [Key]
        public int ID { get; set; }
    }
}
