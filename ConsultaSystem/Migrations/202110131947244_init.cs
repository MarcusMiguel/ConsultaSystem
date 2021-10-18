namespace ConsultaSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Consultas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Horario = c.DateTime(nullable: false),
                        Protocolo = c.String(nullable: false),
                        IDPaciente = c.Int(nullable: false),
                        IDTipoDeExame = c.Int(nullable: false),
                        IDExame = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Exames", t => t.IDExame, cascadeDelete: true)
                .ForeignKey("dbo.Pacientes", t => t.IDPaciente, cascadeDelete: true)
                .ForeignKey("dbo.TipoDeExames", t => t.IDTipoDeExame, cascadeDelete: true)
                .Index(t => t.IDPaciente)
                .Index(t => t.IDTipoDeExame)
                .Index(t => t.IDExame);
            
            CreateTable(
                "dbo.Exames",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 100),
                        Observacoes = c.String(nullable: false, maxLength: 1000),
                        IDTipoDeExame = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.TipoDeExames", t => t.IDTipoDeExame, cascadeDelete: false)
                .Index(t => t.IDTipoDeExame);
            
            CreateTable(
                "dbo.TipoDeExames",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 100),
                        Descricao = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Pacientes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 100),
                        CPF = c.String(nullable: false),
                        DataNascimento = c.DateTime(nullable: false),
                        Sexo = c.Int(nullable: false),
                        Telefone = c.String(nullable: false),
                        Email = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Consultas", "IDTipoDeExame", "dbo.TipoDeExames");
            DropForeignKey("dbo.Consultas", "IDPaciente", "dbo.Pacientes");
            DropForeignKey("dbo.Consultas", "IDExame", "dbo.Exames");
            DropForeignKey("dbo.Exames", "IDTipoDeExame", "dbo.TipoDeExames");
            DropIndex("dbo.Exames", new[] { "IDTipoDeExame" });
            DropIndex("dbo.Consultas", new[] { "IDExame" });
            DropIndex("dbo.Consultas", new[] { "IDTipoDeExame" });
            DropIndex("dbo.Consultas", new[] { "IDPaciente" });
            DropTable("dbo.Pacientes");
            DropTable("dbo.TipoDeExames");
            DropTable("dbo.Exames");
            DropTable("dbo.Consultas");
        }
    }
}
