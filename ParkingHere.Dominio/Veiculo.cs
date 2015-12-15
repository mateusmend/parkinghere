using System;
using System.ComponentModel.DataAnnotations;

namespace ParkingHere.Dominio
{
    public class Veiculo
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo Placa é obrigatório!")]
        public string Placa { get; set; }
        public string Tipo { get; set; }
        public DateTime DataInicial { get; set; }
        public DateTime DataFinal { get; set; }
        public int VagaId { get; set; }
        public virtual Vaga Vaga { get; set; }
    }
}

