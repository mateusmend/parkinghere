namespace ParkingHere.Dominio
{
    public class Vaga
    {
        public int Id { get; set; }
        public bool Livre { get; set; }
        public string Endereco { get; set; }
        public int Tipo { get; set; }
        public int EstacionamentoId { get; set; }
        public virtual Estacionamento Estacionamento { get; set; }
    }
}
