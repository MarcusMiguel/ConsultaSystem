using ConsultaSystem.Domain.Entities;
using System.Data.Entity;

namespace ConsultaSystem.Infra.Context
{
    public class ConsultaSystemContext : DbContext
    {
        public ConsultaSystemContext() : base("name=ConsultaSystemContext")
        {
        }

        public System.Data.Entity.DbSet<Paciente> Pacientes { get; set; }

        public System.Data.Entity.DbSet<Exame> Exames { get; set; }

        public System.Data.Entity.DbSet<Consulta> Consultas { get; set; }

        public System.Data.Entity.DbSet<TipoDeExame> TiposDeExames { get; set; }
    }
}
