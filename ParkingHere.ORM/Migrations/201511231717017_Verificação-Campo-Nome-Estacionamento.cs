namespace ParkingHere.ORM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VerificaçãoCampoNomeEstacionamento : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Estacionamento", "Nome", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Estacionamento", "Nome", c => c.String());
        }
    }
}
