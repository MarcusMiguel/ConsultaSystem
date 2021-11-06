namespace ConsultaSystem.Migrations
{
    using ConsultaSystem.Infra.Context;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<ConsultaSystemContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ConsultaSystemContext context)
        {
        }
    }
}
