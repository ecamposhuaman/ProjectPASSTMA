using ENTIDAD;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using NEGOCIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ProjectPASSTMA.Controllers
{
    //[Authorize(Roles = "Administrador")]
    public class SEMController : Controller
    {
        // GET: SEM
        
        public ActionResult Index()
        {
            var sems = SEMCN.ListarSEM();
            return View(sems);
        }

        public ActionResult Detalle(int id)
        {
            var sm = SEMCN.DetalleSEM(id);
            return View(sm);
        }


        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] /*el AntiForgeryToken agregado para no permitir el envio de codigo malicioso por usuarios no registrados*/
        public ActionResult Crear(SEM sm)
        {
            try
            {
                if (sm.EsPrincipal == null)
                    sm.EsPrincipal = "N";
                else
                    sm.EsPrincipal = "S";

                SEMCN.AgregarSEM(sm);
                return Json(new { ok = true, toRedirect = Url.Action("Index") }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ok = false, msg = ex.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult Editar(int id)
        {
            var sm = SEMCN.DetalleSEM(id);
            return View(sm);
        }

        [HttpPost]
        public ActionResult Editar(SEM sm)
        {
            try
            {
                if (sm.EsPrincipal == null)
                    sm.EsPrincipal = "N";
                else
                    sm.EsPrincipal = "S";
                
                SEMCN.EditarSEM(sm);
                return Json(new { ok = true, toRedirect = Url.Action("Index") }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ok = false, toRedirect = Url.Action("Editar", new { id = sm.IdSEM }), msg = ex.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult Eliminar(int? id) //Para diferenciarlo del HttpPost
        {
            if (id == null)//Junto a esto
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var sm = SEMCN.DetalleSEM(id.Value);
            return View(sm);
        }

        [HttpPost]
        public ActionResult Eliminar(int id)
        {
            try
            {
                SEMCN.EliminarSEM(id);
                return Json(new { ok = true, toRedirect = Url.Action("Index") }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ok = false, toRedirect = Url.Action("Eliminar", new { id = id }), msg = ex.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetSEM()
        {
            var lista = SEMCN.ListarCMBSEM();
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSEMByIdUUNN(int iduunn)
        {
            var lista = SEMCN.ListarCMBSEMByIdUUNN(iduunn);
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetSEMByUser(string user)
        {
            var usermanager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            if (usermanager.IsInRole(User.Identity.GetUserId(), "Administrador"))
            {
                var lista = SEMCN.ListarCMBSEM();
                return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var lista = SEMCN.ListarCMBSEMByUser(user);
                return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}