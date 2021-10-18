using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ConsultaSystem.Data
{
    public class ConsultaSystemContext : DbContext
    {
        public ConsultaSystemContext() : base("name=ConsultaSystemContext")
        {
        }

        public System.Data.Entity.DbSet<ConsultaSystem.Entities.Paciente> Pacientes { get; set; }

        public System.Data.Entity.DbSet<ConsultaSystem.Entities.Exame> Exames { get; set; }

        public System.Data.Entity.DbSet<ConsultaSystem.Entities.Consulta> Consultas { get; set; }

        public System.Data.Entity.DbSet<ConsultaSystem.Entities.TipoDeExame> TiposDeExames { get; set; }
    }
}
