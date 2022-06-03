using NEGOCIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectPASSTMA.Controllers
{
    public class EstadoController : Controller
    {
        // GET: Estado
        public ActionResult Index()
        {
            var rs = EstadoCN.ListarEstado();
            return View(rs);
        }


        //public ActionResult Detalle(int id)
        //{
        //    var rs = ResponsableCN.DetalleResponsable(id);
        //    return View(rs);
        //}


        //public ActionResult Crear()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken] /*el AntiForgeryToken agregado para no permitir el envio de codigo malicioso por usuarios no registrados*/
        //public ActionResult Crear(RESPONSABLE rs)
        //{
        //    try
        //    {
        //        /*VALIDACIONES DEL LADO DEL SERVIDOR*/
        //        if (rs.NombreResponsable == null)
        //            return Json(new { ok = false, msg = "Ingrese un Nombre de Responsable" }, JsonRequestBehavior.AllowGet);
        //        if (rs.EmailResponsable == null)
        //            return Json(new { ok = false, msg = "Ingrese un Correo Electrónico" }, JsonRequestBehavior.AllowGet);

        //        ResponsableCN.AgregarResponsable(rs);
        //        return Json(new { ok = true, toRedirect = Url.Action("Index") }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { ok = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
        //    }
        //}


        //public ActionResult Editar(int id)
        //{
        //    var rs = ResponsableCN.DetalleResponsable(id);
        //    return View(rs);
        //}

        //[HttpPost]
        //public ActionResult Editar(RESPONSABLE rs)
        //{
        //    try
        //    {
        //        if (rs.NombreResponsable == null)
        //        {
        //            ModelState.AddModelError("", "Debe ingresar un Nombre de Responsable");
        //            return View(rs);
        //        }
        //        ResponsableCN.EditarResponsable(rs);
        //        return RedirectToAction("Index");
        //    }
        //    catch (Exception ex)
        //    {
        //        ModelState.AddModelError("", "Ocurrió un error al Editar Responsable: " + ex);
        //        return View(rs);
        //    }
        //}


        //public ActionResult Eliminar(int? id) //Para diferenciarlo del HttpPost
        //{
        //    if (id == null)//Junto a esto
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

        //    var rs = ResponsableCN.DetalleResponsable(id.Value);
        //    return View(rs);
        //}

        //[HttpPost]
        //public ActionResult Eliminar(int id)
        //{
        //    try
        //    {
        //        ResponsableCN.EliminarResponsable(id);
        //        return RedirectToAction("Index");
        //    }
        //    catch (Exception ex)
        //    {
        //        ModelState.AddModelError("", "Ocurrió un error al Eliminar Responsable: " + ex);
        //        return View();
        //    }
        //}

        public JsonResult GetEstado()
        {
            var lista = EstadoCN.ListarEstado();
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }
    }
}