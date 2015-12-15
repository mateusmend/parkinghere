using ParkingHere.Dominio;
using ParkingHere.ORM;
using System.Web.Mvc;
using System.Linq;
using ParkingHere.BO;

namespace ParkingHere.Web.UI.Controllers
{
    public class EstacionamentoController : Controller
    {
        private Contexto db = new Contexto();
        private EstacionamentoBO EstacionamentoBO = new EstacionamentoBO();
        [HttpPost]
        public ActionResult ValidarEstacionamento()
        {
            if (EstacionamentoBO.ValidarEstacionamento())
            {
                return Json(new { Id = 1 }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Id = 0 }, JsonRequestBehavior.AllowGet);
        }
        [Route("configuracoes")]
        public ActionResult Configuracao()
        {
            return View(db.Estacionamentos.FirstOrDefault());
        }

        public ActionResult deletar()
        {
            EstacionamentoBO.RemoverEstacionamento();
            return Json(Url.Action("CriarEstacionamento", "Estacionamento"));  
        }

        // GET: Estacionamento
        public ActionResult CriarEstacionamento() {
            if (EstacionamentoBO.ValidarEstacionamento())
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Create");                
            }
        }

        //Index
        [Route("estacionamento")]
        public ActionResult Index()
        {
            EstacionamentoBO.Ordernar();
            return View(EstacionamentoBO.Ordernar());
        }

        // GET: Estacionamento/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        #region CREATE: Get/Post
        // GET: Estacionamento/Create
        [Route("criarestacionamento")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Estacionamento/Create
        [HttpPost]
        [Route("criarestacionamento")]
        public ActionResult Create(Estacionamento estacionamento)
        {
            TempData["EstacionamentoCriado"] = "Estacionamento criado com sucesso!";

            EstacionamentoBO.Adicionar(estacionamento);
            return RedirectToAction("Index");
        }
        #endregion

        #region EDIT: Get/Post
        // GET: Estacionamento/Edit/5
        [Route("editarestacionamento")]
        public ActionResult Edit(int id)
        {
            TempData["EstacionamentoEditado"] = "Estacionamento editado com sucesso!";

            return View(db.Estacionamentos.FirstOrDefault(x => x.Id == id));
        }

        // POST: Estacionamento/Edit/5
        [HttpPost]
        [Route("editarestacionamento")]
        public ActionResult Edit(Estacionamento e)
        {
            EstacionamentoBO.AlterarEstacionamento(e);
                return RedirectToAction("CriarEstacionamento");
        }
        #endregion

        #region DELETE: Get/Post
        // GET: Estacionamento/Delete/5
        [Route("excluirestacionamento")]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Estacionamento/Delete/5
        [HttpPost]
        [Route("excluirestacionamento")]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        #endregion
    }
}
