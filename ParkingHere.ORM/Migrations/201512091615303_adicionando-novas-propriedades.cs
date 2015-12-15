namespace ParkingHere.ORM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adicionandonovaspropriedades : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Estacionamento", "NumeroVagasMoto", c => c.Int(nullable: false));
            AddColumn("dbo.Estacionamento", "NumeroVagasDef", c => c.Int(nullable: false));
            AddColumn("dbo.Vaga", "Tipo", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Vaga", "Tipo");
            DropColumn("dbo.Estacionamento", "NumeroVagasDef");
            DropColumn("dbo.Estacionamento", "NumeroVagasMoto");
        }
    }
}
