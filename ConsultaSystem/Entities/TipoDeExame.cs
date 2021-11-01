using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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