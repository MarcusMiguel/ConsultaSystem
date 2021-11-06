using System.ComponentModel.DataAnnotations;

namespace ConsultaSystem.Domain.Entities
{
    public class TipoDeExame : Entity
    {
        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required]
        [StringLength(256)]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
    }
}