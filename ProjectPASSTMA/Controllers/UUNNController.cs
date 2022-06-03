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
    public class UUNNController : Controller
    {
        // GET: UUNN
        public ActionResult Index()
        {
            var uns = UUNNCN.ListarUUNN();
            return View(uns);
        }

        public ActionResult Detalle(int id)
        {
            var un = UUNNCN.DetalleUUNN(id);
            return View(un);
        }


        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Crear(UUNN un)
        {
            try
            {
                UUNNCN.AgregarUUNN(un);
                return Json(new { ok = true, toRedirect = Url.Action("Index") }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ok = false, toRedirect = Url.Action("Crear"), msg = ex.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult Editar(int id)
        {
            var un = UUNNCN.DetalleUUNN(id);
            return View(un);
        }

        [HttpPost]
        public ActionResult Editar(UUNN un)
        {
            try
            {
                UUNNCN.EditarUUNN(un);
                return Json(new { ok = true, toRedirect = Url.Action("Index") }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ok = false, toRedirect = Url.Action("Editar", new { id = un.IdUUNN }), msg = ex.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult Eliminar(int? id) //Para diferenciarlo del HttpPost
        {
            if (id == null)//Junto a esto
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var un = UUNNCN.DetalleUUNN(id.Value);
            return View(un);
        }

        [HttpPost]
        public ActionResult Eliminar(int id)
        {
            try
            {
                UUNNCN.EliminarUUNN(id);
                return Json(new { ok = true, toRedirect = Url.Action("Index") }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ok = false, toRedirect = Url.Action("Eliminar", new { id = id }), msg = ex.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetUUNN()
        {
            var lista = UUNNCN.ListarUUNN();
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }
    }
}