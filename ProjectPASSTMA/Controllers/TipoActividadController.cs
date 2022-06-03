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
    public class TipoActividadController : Controller
    {
        // GET: TipoActividad
        public ActionResult Index()
        {
            var rs = TipoActividadCN.ListarTipoActividad();
            return View(rs);
        }


        public ActionResult Detalle(int id)
        {
            var rs = TipoActividadCN.DetalleTipoActividad(id);
            return View(rs);
        }


        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] /*el AntiForgeryToken agregado para no permitir el envio de codigo malicioso por usuarios no registrados*/
        public ActionResult Crear(TIPOACTIVIDAD rs)
        {
            try
            {

                TipoActividadCN.AgregarTipoActividad(rs);
                return Json(new { ok = true, toRedirect = Url.Action("Index") }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ok = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult Editar(int id)
        {
            var rs = TipoActividadCN.DetalleTipoActividad(id);
            return View(rs);
        }

        [HttpPost]
        public ActionResult Editar(TIPOACTIVIDAD rs)
        {
            try
            {
                TipoActividadCN.EditarTipoActividad(rs);
                return Json(new { ok = true, toRedirect = Url.Action("Index") }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ok = false, toRedirect = Url.Action("Editar", new { id = rs.IdTipo }), msg = ex.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult Eliminar(int? id) //Para diferenciarlo del HttpPost
        {
            if (id == null)//Junto a esto
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var rs = TipoActividadCN.DetalleTipoActividad(id.Value);
            return View(rs);
        }

        [HttpPost]
        public ActionResult Eliminar(int id)
        {
            try
            {
                TipoActividadCN.EliminarTipoActividad(id);
                return Json(new { ok = true, toRedirect = Url.Action("Index") }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ok = false, toRedirect = Url.Action("Eliminar", new { id = id }), msg = ex.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetTipoActividad()
        {
            var lista = TipoActividadCN.ListarTipoActividad();
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }
    }
}