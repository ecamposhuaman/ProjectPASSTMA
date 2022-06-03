using ENTIDAD;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using NEGOCIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace ProjectPASSTMA.Controllers
{
    public class ResponsableController : Controller
    {
        // GET: Responsable
        public ActionResult Index()
        {
            var rs = ResponsableCN.ListarResponsable();
            return View(rs);
        }


        public ActionResult Detalle(int id)
        {
            var rs = ResponsableCN.DetalleResponsable(id);
            return View(rs);
        }



        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] /*el AntiForgeryToken agregado para no permitir el envio de codigo malicioso por usuarios no registrados*/
        public ActionResult Crear(RESPONSABLE rs, string EmailResponsable)
        {
            if (EsEmail(EmailResponsable))
            {
                try
                {
                    var rsbe = ResponsableCN.DetalleResponsableByEmail(EmailResponsable);
                    if (rsbe == null)
                    {
                        ResponsableCN.AgregarResponsable(rs);
                        return Json(new { ok = true, toRedirect = Url.Action("Index") }, JsonRequestBehavior.AllowGet);
                    }
                    else
                        return Json(new { ok = false, toRedirect = Url.Action("Crear"), msg = "El E-mail insertado ya existe" }, JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {
                    return Json(new { ok = false, msg = ex.ToString() }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { ok = false, toRedirect = Url.Action("Crear"), msg = "Email incorrecto" }, JsonRequestBehavior.AllowGet);
            }
        }

        public bool EsEmail(string email)
        {
            string expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public ActionResult Editar(int id)
        {
            var rs = ResponsableCN.DetalleResponsable(id);
            return View(rs);
        }

        [HttpPost]
        public ActionResult Editar(RESPONSABLE rs, string EmailResponsable, int action)
        {
            if (EsEmail(EmailResponsable))
            {
                try
                {
                    //Modificando la asociación a ROL
                    var IdAspNet = rs.IdAspNetUser;
                    var usermanager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                    if (IdAspNet != null) //Si se registraron e iniciaron sesión alguna vez
                    {
                        usermanager.RemoveFromRole(rs.IdAspNetUser, "Administrador");
                        usermanager.RemoveFromRole(rs.IdAspNetUser, "Responsable");
                        usermanager.AddToRole(rs.IdAspNetUser, rs.NombreRol);
                    }
                    ResponsableCN.EditarResponsable(rs, action);
                    return Json(new { ok = true, toRedirect = Url.Action("Index") }, JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {
                    return Json(new { ok = false, toRedirect = Url.Action("Editar", new { id = rs.IdResponsable }), msg = ex.ToString() }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { ok = false, toRedirect = Url.Action("Editar", new { id = rs.IdResponsable }), msg = "Email no válido" }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult Eliminar(int? id) //Para diferenciarlo del HttpPost
        {
            if (id == null)//Junto a esto
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var rs = ResponsableCN.DetalleResponsable(id.Value);
            return View(rs);
        }

        [HttpPost]
        public ActionResult Eliminar(int id)
        {
            try
            {
                ResponsableCN.EliminarResponsable(id);
                return Json(new { ok = true, toRedirect = Url.Action("Index") }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ok = false, toRedirect = Url.Action("Eliminar", new { id = id }), msg = ex.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetResponsable()
        {
            var lista = ResponsableCN.ListarCMBResponsable();
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }
    }
}