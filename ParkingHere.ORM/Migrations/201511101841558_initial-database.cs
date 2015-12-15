namespace ParkingHere.ORM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialdatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Estacionamento",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        NumeroVagas = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Vaga",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Livre = c.Boolean(nullable: false),
                        Endereco = c.String(),
                        EstacionamentoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Estacionamento", t => t.EstacionamentoId, cascadeDelete: true)
                .Index(t => t.EstacionamentoId);
            
            CreateTable(
                "dbo.Veiculo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Placa = c.String(nullable: false),
                        Tipo = c.String(),
                        VagaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Vaga", t => t.VagaId, cascadeDelete: true)
                .Index(t => t.VagaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Veiculo", "VagaId", "dbo.Vaga");
            DropForeignKey("dbo.Vaga", "EstacionamentoId", "dbo.Estacionamento");
            DropIndex("dbo.Veiculo", new[] { "VagaId" });
            DropIndex("dbo.Vaga", new[] { "EstacionamentoId" });
            DropTable("dbo.Veiculo");
            DropTable("dbo.Vaga");
            DropTable("dbo.Estacionamento");
        }
    }
}
