using ParkingHere.Dominio;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ParkingHere.ORM
{
    public class Contexto : DbContext
    {
        public Contexto()
        : base("DbTrainee2015")
        { }

        public DbSet<Veiculo> Veiculos { get; set; }
        public DbSet<Vaga> Vagas { get; set; }
        //public DbSet<VagaVeiculo> VagaVeiculos { get; set; }
        public DbSet<Estacionamento> Estacionamentos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //Mapeamento
            //modelBuilder.Configurations.Add(new AlunoMap());
            //modelBuilder.Configurations.Add(new TurmaMap());
            //modelBuilder.Configurations.Add(new CursoMap());
            //modelBuilder.Configurations.Add(new ProfessorMap());
        }
    }
}
