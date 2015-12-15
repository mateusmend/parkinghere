namespace ParkingHere.ORM.Migrations
{
    using Dominio;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<ParkingHere.ORM.Contexto>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "ParkingHere.ORM.Contexto";
        }

        protected override void Seed(ParkingHere.ORM.Contexto context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            //context.Estacionamentos.AddOrUpdate(
            //    p => p.Nome,
            //         new Estacionamento { Nome = "Renomear", NumeroVagas = 200 }
            //    );

        }
    }
}
