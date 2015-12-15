using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ParkingHere.Dominio
{
    public class Estacionamento
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo Nome é obrigatório!")]
        public string Nome { get; set; }
        [Required (ErrorMessage = "O campo Vagas para Carros é obrigatório!")]
        public int NumeroVagas { get; set; }
        [Required(ErrorMessage = "O campo Vagas para Motos é obrigatório!")]
        public int NumeroVagasMoto { get; set; }
        [Required(ErrorMessage = "O campo Vagas para PcD é obrigatório!")]
        public int NumeroVagasDef { get; set; }

        public virtual ICollection<Vaga> Vagas { get; set; }
        
    }
}
