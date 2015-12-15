using ParkingHere.BO.Interface;
using ParkingHere.Dominio;

namespace ParkingHere.BO
{
    public interface IEstacionamentoBo : Ivalidator<Estacionamento>
    {
        void Adicionar(Estacionamento estacionamento);

        bool ValidarEstacionamento();

        void RemoverEstacionamento();

        void AlterarEstacionamento(Estacionamento estacionamentoAtualizado);
    }
}