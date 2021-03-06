using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsultaSystem.Entities
{
    public class Exame
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }
        
        [Required]
        [StringLength(1000)]
        public string Observacoes { get; set; }

        [Required]
        public int IDTipoDeExame { get; set; }

        [ForeignKey("IDTipoDeExame")]
        public  virtual TipoDeExame TipoDeExame { get; set; }

    }
}