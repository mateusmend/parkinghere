using ParkingHere.ORM;

namespace ParkingHere
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new Contexto();

            //Inclusao de registro
            //Veiculo veic = new Veiculo();
            //veic.Placa = "ABC1234";

            //db.Veiculos.Add(veic);
            //db.SaveChanges();

            //Correção do registro
            //var veiculos = db.Veiculos.FirstOrDefault(x => x.Id == 1);

            //Exclusao do registro            
            //var veiculos = db.Veiculos.FirstOrDefault(x => x.Id == 1);
            //db.Veiculos.Remove(veiculos);
            //db.SaveChanges();
        }
    }
}
