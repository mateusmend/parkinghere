namespace ParkingHere.ORM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newatributesclassveiculo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Veiculo", "DataInicial", c => c.DateTime(nullable: false));
            AddColumn("dbo.Veiculo", "DataFinal", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Veiculo", "DataFinal");
            DropColumn("dbo.Veiculo", "DataInicial");
        }
    }
}
