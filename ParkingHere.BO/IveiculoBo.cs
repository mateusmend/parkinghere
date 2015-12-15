using ParkingHere.BO.Interface;
using ParkingHere.Dominio;

namespace ParkingHere.BO
{
    public interface IveiculoBo : Ivalidator<Veiculo>
    {
        bool ValidarPlaca(string Placa);

        bool Adicionar(Veiculo v);

        Veiculo Remover(int vagaId);

        void ConfirmarRemocao(int VagaId);

    }
}