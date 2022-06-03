using ENTIDAD;
using NEGOCIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ProjectPASSTMA.Controllers
{
    public class ActividadController : Controller
    {
        // GET: Actividad
        public ActionResult Index()
        {
            var act = ActividadCN.ListarActividad();
            return View(act);
        }


        public ActionResult Detalle(int id)
        {
            var ac = ActividadCN.DetalleActividad(id);
            return View(ac);
        }


        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] /*el AntiForgeryToken agregado para no permitir el envio de codigo malicioso por usuarios no registrados*/
        public ActionResult Crear(ACTIVIDAD ac)
        {
            try
            {
                ActividadCN.AgregarActividad(ac);
                return Json(new { ok = true, toRedirect = Url.Action("Index") }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ok = false, msg = ex.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult Editar(int id)
        {
            var ac = ActividadCN.DetalleActividad(id);
            return View(ac);
        }

        [HttpPost]
        public ActionResult Editar(ACTIVIDADCE ac)
        {
            try
            {
                ActividadCN.EditarActividad(ac);
                return Json(new { ok = true, toRedirect = Url.Action("Index") }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ok = false, msg = ex.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Eliminar(int? id) //Para diferenciarlo del HttpPost
        {
            if (id == null)//Junto a esto
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var ac = ActividadCN.DetalleActividad(id.Value);
            return View(ac);
        }

        [HttpPost]
        public ActionResult Eliminar(int id)
        {
            try
            {
                ActividadCN.EliminarActividad(id);
                return Json(new { ok = true, toRedirect = Url.Action("Index") }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ok = false, toRedirect = Url.Action("Eliminar", new { id = id }) , msg = ex.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult GetActividad()
        {
            var lista = ActividadCN.ListarCMBActividad();
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetActividadByTipo(int idtipo)
        {
            var lista = ActividadCN.ListarCMBActividadByTipo(idtipo);
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }
    }
}