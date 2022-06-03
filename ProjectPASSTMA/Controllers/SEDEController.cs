using ENTIDAD;
using NEGOCIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ProjectPASSTMA.Controllers
{
    public class SEDEController : Controller
    {
        // GET: SEDE
        public ActionResult Index()
        {
            var rs = SEDECN.ListarSEDE();
            return View(rs);
        }


        public ActionResult Detalle(int id)
        {
            var rs = SEDECN.DetalleSEDE(id);
            return View(rs);
        }


        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] /*el AntiForgeryToken agregado para no permitir el envio de codigo malicioso por usuarios no registrados*/
        public ActionResult Crear(SEDE sd)
        {
            try
            {
                SEDECN.AgregarSEDE(sd);
                return Json(new { ok = true, toRedirect = Url.Action("Index") }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ok = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult Editar(int id)
        {
            var rs = SEDECN.DetalleSEDE(id);
            return View(rs);
        }

        [HttpPost]
        public ActionResult Editar(SEDE sd)
        {
            try
            {
                SEDECN.EditarSEDE(sd);
                return Json(new { ok = true, toRedirect = Url.Action("Index") }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ok = false, toRedirect = Url.Action("Editar", new { id = sd.IdSEDE }), msg = ex.ToString() }, JsonRequestBehavior.AllowGet);

            }
        }


        public ActionResult Eliminar(int? id) //Para diferenciarlo del HttpPost
        {
            if (id == null)//Junto a esto
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var rs = SEDECN.DetalleSEDE(id.Value);
            return View(rs);
        }

        [HttpPost]
        public ActionResult Eliminar(int id)
        {
            try
            {
                SEDECN.EliminarSEDE(id);
                return Json(new { ok = true, toRedirect = Url.Action("Index") }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ok = false, toRedirect = Url.Action("Eliminar", new { id = id }), msg = ex.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetSEDE()
        {
            var lista = SEDECN.ListarSEDE();
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }
    }
}