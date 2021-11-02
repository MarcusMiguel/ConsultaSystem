using System.ComponentModel.DataAnnotations;

namespace ConsultaSystem.Entities
{
    public class TipoDeExame
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required]
        [StringLength(256)]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
    }
}