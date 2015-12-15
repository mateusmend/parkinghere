using System.Collections.Generic;
using System.Linq;
using ParkingHere.Dominio;
using ParkingHere.ORM;
using System.Data.Entity;
using System;

namespace ParkingHere.BO
{
    public class EstacionamentoBO : IEstacionamentoBo
    {
        private Contexto db = new Contexto();
        public Estacionamento Ordernar()
        {
            var estacionamentoOrdenado = db.Estacionamentos.FirstOrDefault();
            estacionamentoOrdenado.Vagas = estacionamentoOrdenado.Vagas.OrderBy(x=>x.Tipo).ToList();

            return estacionamentoOrdenado;
        }
        #region Metodo Adicionar
        public void Adicionar(Estacionamento estacionamento)
        {
            estacionamento.Vagas = new List<Vaga>();

            for (int i = 1; i <= estacionamento.NumeroVagas; i++)
            {
                estacionamento.Vagas.Add(new Vaga() {Endereco = "Vaga " + i, Tipo = 1 });
            }
            for (int i = 1; i <= estacionamento.NumeroVagasMoto; i++)
            {
                estacionamento.Vagas.Add(new Vaga() {Endereco = "Vaga " + i, Tipo= 2 });
            }
            for (int i = 1; i <= estacionamento.NumeroVagasDef; i++)
            {
                estacionamento.Vagas.Add(new Vaga() {Endereco = "Vaga " + i, Tipo = 3 });
            }
            db.Estacionamentos.Add(estacionamento);
            db.SaveChanges();
        }
        #endregion

        #region Metodo AlterarEstacionamento
        public void AlterarEstacionamento(Estacionamento estacionamentoAtualizado)
        {
            var alterar = db.Estacionamentos.FirstOrDefault(x => x.Id == estacionamentoAtualizado.Id);
            var numeroVagas = estacionamentoAtualizado.NumeroVagas - alterar.NumeroVagas;
            var numeroVagasMoto = estacionamentoAtualizado.NumeroVagasMoto - alterar.NumeroVagasMoto;
            var numeroVagasDef = estacionamentoAtualizado.NumeroVagasDef - alterar.NumeroVagasDef;
            var total = alterar.NumeroVagas;
            var totalmoto = alterar.NumeroVagasMoto;
            var totaldef = alterar.NumeroVagasMoto;
            if (total < estacionamentoAtualizado.NumeroVagas)
            {
                AdicionarVagas(estacionamentoAtualizado, alterar, numeroVagas);
            }
            else
            {
                RemoverVagas(estacionamentoAtualizado, alterar);
            }
            if (totaldef < estacionamentoAtualizado.NumeroVagasDef)
            {
                AdicionarVagasDef(estacionamentoAtualizado, alterar, numeroVagasDef);
            }
            else
            {
                RemoverVagasDef(estacionamentoAtualizado, alterar);
            }
            if (totalmoto < estacionamentoAtualizado.NumeroVagasMoto)
            {
                AdicionarVagasMoto(estacionamentoAtualizado, alterar, numeroVagasMoto);
            }
            else
            {
                RemoverVagasMoto(estacionamentoAtualizado, alterar);
            }

        }

        private void RemoverVagasMoto(Estacionamento estacionamentoAtualizado, Estacionamento alterar)
        {
            var numeroVagas = alterar.NumeroVagasMoto - estacionamentoAtualizado.NumeroVagasMoto;
            AtribuirPropriedadesNovasMoto(estacionamentoAtualizado, alterar);
            for (int i = 1; i <= numeroVagas; i++)
            {
                var remover = alterar.Vagas.LastOrDefault(x => x.Tipo == 2);
                db.Vagas.Remove(remover);
                alterar.Vagas.Remove(remover);

            }
            EditarSalvandoNoBanco(alterar);
        }

        private void RemoverVagasDef(Estacionamento estacionamentoAtualizado, Estacionamento alterar)
        {
            var numeroVagas = alterar.NumeroVagasDef - estacionamentoAtualizado.NumeroVagasDef;
            AtribuirPropriedadesNovasDef(estacionamentoAtualizado, alterar);
            for (int i = 1; i <= numeroVagas; i++)
            {
                var remover = alterar.Vagas.LastOrDefault(x => x.Tipo == 3);
                db.Vagas.Remove(remover);
                alterar.Vagas.Remove(remover);

            }
            EditarSalvandoNoBanco(alterar);
        }

        private void AdicionarVagasDef(Estacionamento estacionamentoAtualizado, Estacionamento alterar, int numeroVagasDef)
        {
            numeroVagasDef = alterar.NumeroVagasDef;
            var numeroVagas = estacionamentoAtualizado.NumeroVagasDef - alterar.NumeroVagasDef;
            AtribuirPropriedadesNovasDef(estacionamentoAtualizado, alterar);
            for (int i = 1; i <= numeroVagas; i++)
            {
                alterar.Vagas.Add(new Vaga() { Endereco = "Vaga " + (numeroVagasDef + i), Tipo = 3 });
            }
            EditarSalvandoNoBanco(alterar);
        }

        private void AdicionarVagasMoto(Estacionamento estacionamentoAtualizado, Estacionamento alterar, int numeroVagasMoto)
        {
            numeroVagasMoto = alterar.NumeroVagasMoto;
            var numeroVagas = estacionamentoAtualizado.NumeroVagasMoto - alterar.NumeroVagasMoto ;
            AtribuirPropriedadesNovasMoto(estacionamentoAtualizado, alterar);
            for (int i = 1; i <= numeroVagas; i++)
            {
                alterar.Vagas.Add(new Vaga() { Endereco = "Vaga " + (numeroVagasMoto + i), Tipo= 2 });
            }
            EditarSalvandoNoBanco(alterar);
        }

        /// <summary>
        /// Método Privado para inserção de novas vagas quando solicitado a edição do estacionamento
        /// </summary>
        /// <param name="estacionamentoAtualizado">receberá as propriedades novas</param>
        /// <param name="alterar">referencia do estacionamentoAtualizado utilizada para alterar no banco</param>
        /// <param name="quantidadeVagas">Quantidade de vagas</param>
        private void AdicionarVagas(Estacionamento estacionamentoAtualizado, Estacionamento alterar, int quantidadeVagas)
        {
            quantidadeVagas = alterar.NumeroVagas;
            var numeroVagas = estacionamentoAtualizado.NumeroVagas - alterar.NumeroVagas;
            AtribuirPropriedadesNovasCarro(estacionamentoAtualizado, alterar);
            for (int i = 1; i <= numeroVagas; i++)
            {
                alterar.Vagas.Add(new Vaga() { Endereco = "Vaga " + (quantidadeVagas + i), Tipo=1 });
            }
            EditarSalvandoNoBanco(alterar);
        }

        private void AtribuirPropriedadesNovasCarro(Estacionamento estacionamentoAtualizado, Estacionamento alterar)
        {
            alterar.Nome = estacionamentoAtualizado.Nome;
            alterar.NumeroVagas = estacionamentoAtualizado.NumeroVagas;
        }
        /// <summary>
        /// método privado para atribuir as propriedades novas no estacionamento !
        /// </summary>
        /// <param name="estacionamentoAtualizado"></param>
        /// <param name="alterar"></param>
        private static void AtribuirPropriedadesNovasMoto(Estacionamento estacionamentoAtualizado, Estacionamento alterar)
        {
            alterar.Nome = estacionamentoAtualizado.Nome;
            alterar.NumeroVagasMoto = estacionamentoAtualizado.NumeroVagasMoto;
        }
        private static void AtribuirPropriedadesNovasDef(Estacionamento estacionamentoAtualizado, Estacionamento alterar)
        {
            alterar.Nome = estacionamentoAtualizado.Nome;
            alterar.NumeroVagasDef = estacionamentoAtualizado.NumeroVagasDef;
        }
        /// <summary>
        /// Método privado para remover vagas na edição do estacionamento
        /// </summary>
        /// <param name="estacionamentoAtualizado">receberá as propriedades novas</param>
        /// <param name="alterar">referencia do estacionamentoAtualizado utilizada para alterar no banco</param>
        private void RemoverVagas(Estacionamento estacionamentoAtualizado, Estacionamento alterar)
        {
            var numeroVagas = alterar.NumeroVagas - estacionamentoAtualizado.NumeroVagas;
            AtribuirPropriedadesNovasCarro(estacionamentoAtualizado, alterar);
            for (int i = 1; i <= numeroVagas; i++)
            {
                var remover = alterar.Vagas.LastOrDefault(x => x.Tipo == 1);
                db.Vagas.Remove(remover);
                alterar.Vagas.Remove(remover);
              
            }
            EditarSalvandoNoBanco(alterar);
        }

        private void EditarSalvandoNoBanco(Estacionamento alterar)
        {
            db.Entry(alterar).State = EntityState.Modified;
            db.SaveChanges();
        }
        #endregion

        #region Metodo ValidarEstacionamento
        public bool ValidarEstacionamento()
        {
            if (db.Estacionamentos.ToList().Count > 0)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Metodo RemoverEstacionamento
        public void RemoverEstacionamento()
        {
            var remove = db.Estacionamentos.FirstOrDefault();
            db.Estacionamentos.Remove(remove);
            db.SaveChanges();
        }
        #endregion

        #region Metodo de Verificação (Ivalidador)
        //Metodo de validação da Ivalidator
        public bool IsValidBase(Estacionamento domain)
        {
            return true;
        }
        //Metodo que verifica se o Estacionamento foi inserido
        public bool ValidateInsert(Estacionamento domain)
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
