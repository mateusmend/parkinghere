using ParkingHere.BO;
using ParkingHere.Dominio;
using ParkingHere.ORM;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ParkingHere.Web.UI.Controllers
{
    [RoutePrefix("veiculo")]
    public class VeiculoController : Controller
    {
        private Contexto db = new Contexto();

        private VeiculoBO veiculoBO = new VeiculoBO();

        public ActionResult Filtro(int filtragem)
        {
            var veiculos = veiculoBO.Filtragem(filtragem);
            //var serializer = new JavaScriptSerializer();
            //var serializedResult = serializer.Serialize(veiculo);

            var result = (from item in veiculos
                         select new FiltroResult { Endereco = item.Vaga.Endereco, TipoV = item.Vaga.Tipo, Placa = item.Placa, TipoVe = item.Tipo, DataAt = item.DataInicial.ToShortDateString(), HoraInicial = item.DataInicial.ToShortTimeString()}).ToList();





            return Json(new {result}, JsonRequestBehavior.AllowGet); 





           //return Json(new { Endereco = item.Vaga.Endereco, TipoV = item.Vaga.Tipo, Placa = item.Placa, TipoVe = item.Tipo, DataAt = item.DataFinal.ToShortDateString(), HoraInicial = item.DataFinal.ToShortTimeString() }, JsonRequestBehavior.AllowGet); 
        }
        // GET: Veiculo
        [Route("listarveiculo")]
        public ActionResult Index()
        {
            return View(db.Veiculos.ToList());
        }

        // GET: Veiculo/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult ValidarLista()
        {
            if (db.Veiculos.Count() <= 0)
            {
                return Json(new { Id = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Id = 1 }, JsonRequestBehavior.AllowGet);
            }
        }

        #region Envio da validacao de PLACA para tela
        /// <summary>
        /// Este método vai realizar somente o envio para página,
        /// se a placa é repedita ou não
        /// </summary>
        /// <param name="Placa">Vai receber uma string placa</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ValidarPlacaDuplicada(string Placa)
        {
            if (veiculoBO.ValidarPlaca(Placa))
            {
               return Json(new { Id = 1 }, JsonRequestBehavior.AllowGet);
            }
                return Json(new { Id = 0 }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region CREATE Get/Post
        // GET: Veiculo/Create
        [Route("incluirveiculo/{vagaId}")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Veiculo/Create
        [HttpPost]
        [Route("incluirveiculo/{vagaId}")]
        public ActionResult Create(Veiculo v)
        {
            if(veiculoBO.Adicionar(v))
            {
                TempData["VeiculoAdicionado"] = "Vaga ocupada com sucesso!";

                return RedirectToAction("Index","Estacionamento");
            }

            return View();
        }
        #endregion

        #region EDIT: Get/Post
        // GET: Veiculo/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Veiculo/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        #endregion

        #region DELETE Get/Post
        // GET: Veiculo/Delete/5
        [Route("excluirveiculo/{vagaId}")]
        public ActionResult Delete(int vagaId)
        {
            Veiculo v = veiculoBO.Remover(vagaId);

            if (vagaId == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (v == null)
            {
                return HttpNotFound();
            }
            return View(v);
        }

        // POST: Tarefas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("excluirveiculo/{vagaId}")]
        public ActionResult DeleteConfirmed(int VagaId)
        {
            TempData["VeiculoRemovido"] = "Vaga desocupada com sucesso!";

            veiculoBO.ConfirmarRemocao(VagaId);
            return RedirectToAction("Index","Estacionamento");
        }

        #endregion
    }
}
