using ParkingHere.ORM;
using System;
using ParkingHere.Dominio;
using System.Linq;
using System.Data.Entity;
using System.Collections;
using System.Collections.Generic;

namespace ParkingHere.BO
{
    public class VeiculoBO : IveiculoBo
    {
        private Contexto db = new Contexto();

        #region Metodo Remover
        public Veiculo Remover(int vagaId)
        {
            DateTime DataFinal = DateTime.Now;
            Veiculo veiculo = db.Veiculos.FirstOrDefault(x => x.VagaId == vagaId);
            TimeSpan Ts = DataFinal.Subtract(veiculo.DataInicial);

            veiculo.DataFinal = Convert.ToDateTime(Ts.ToString());
            return veiculo;
        }

        public List<Veiculo> Filtragem(int filtragem)
        {
            var veiculos = new List<Veiculo>();
            if(filtragem == 0)
            {
                veiculos = db.Veiculos.ToList();
            }
            else
            {
                veiculos = db.Veiculos.Where(x => x.Vaga.Tipo == filtragem).OrderBy(x => x.Vaga.Endereco).ToList();
            }

            return veiculos;
        }
        #endregion

        #region Metodo ValidarPlaca
        /// <summary>
        /// Este método faz a verificação se já exixte a placa cadastrada 
        /// </summary>
        /// <param name="Placa">Recebe uma placa do tipo string</param>
        /// <returns>V ou F, se a placa ja existe ou não</returns>

        public bool ValidarPlaca(string Placa)
        {
            var valorFiltrado = Placa.Trim();
            var veiculos = db.Veiculos.ToList();
            if (!String.IsNullOrEmpty(valorFiltrado))
            {
                var placa = veiculos.FirstOrDefault(x => x.Placa == valorFiltrado);

                if (placa != null && placa.Id > 0)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region Metodo Adicionar
        public bool Adicionar(Veiculo v)
        {
            DateTime DataInicio = DateTime.Now; ;
            DateTime DataFinal = DateTime.Now;

            v.DataInicial = DataInicio;
            v.DataFinal = DataFinal;
            v.Placa = v.Placa.ToUpper();
            
            try
            {
                Vaga vaga = db.Vagas.First(x => x.Id == v.VagaId);
                if (vaga.Tipo == 1)
                {
                    v.Tipo = "Carro";
                }
                else if (vaga.Tipo == 2)
                {
                    v.Tipo = "Moto";
                }
                else
                {
                    v.Tipo = "Carro";
                }
                db.Veiculos.Add(v);
                vaga.Livre = true;
                db.Entry(vaga).State = EntityState.Modified;
                db.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region Metodo ConfirmarRemocao
        public void ConfirmarRemocao(int VagaId)
        {
            Vaga vaga = db.Vagas.First(x => x.Id == VagaId);

            vaga.Livre = false;
            db.Entry(vaga).State = EntityState.Modified;

            Veiculo veiculo = db.Veiculos.FirstOrDefault(x => x.VagaId == VagaId);

            db.Veiculos.Remove(veiculo);
            db.SaveChanges();
        }
        #endregion

        #region Metodo de Verificação (Ivalidador)
        //Metodo de validação da Ivalidator
        public bool IsValidBase(Veiculo domain)
        {
            return true;
        }
        //Metodo que verifica de o veiculo foi incluido
        public bool ValidateInsert(Veiculo domain)
        {
            if (!IsValidBase(domain))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion
    }
}
