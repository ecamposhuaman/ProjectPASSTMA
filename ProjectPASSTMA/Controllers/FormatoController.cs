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
    public class FormatoController : Controller
    {
        // GET: Formato
        public ActionResult Index()
        {
            var fm = FormatoCN.ListarFormato();
            return View(fm);
        }

        public ActionResult Detalle(int id)
        {
            var fm = FormatoCN.DetalleFormato(id);
            return View(fm);
        }

        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Crear(FORMATO fm, HttpPostedFileBase ArchivoFormato)
        {
            try
            {
                if (ArchivoFormato != null && ArchivoFormato.ContentLength > 0)
                    try
                    {
                        string path = Path.Combine(Server.MapPath("~/FILES/FORMATOS"), Path.GetFileName(ArchivoFormato.FileName));
                        if (!(System.IO.File.Exists(path)))
                        {
                            ArchivoFormato.SaveAs(path);
                            fm.NombreFormato = ArchivoFormato.FileName;
                        }
                        else
                        {
                            ViewBag.Message = "Ya se tiene registrado un archivo con el nombre: " + ArchivoFormato.FileName;
                            return View("Crear");
                        }
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = "Error al guardar archivo: " + ex.ToString();
                        return View("Crear");
                    }
                else
                {
                    ViewBag.Message = "Seleccionó un archivo inválido.";
                    return View("Crear");
                }
                FormatoCN.AgregarFormato(fm);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Error General: " + ex.ToString();
                return View("Crear");
            }
        }


        public ActionResult Editar(int id)
        {
            var un = FormatoCN.DetalleFormato(id);
            return View(un);
        }

        [HttpPost]
        public ActionResult Editar(FORMATO fm, HttpPostedFileBase ArchivoFormato)
        {
            try
            {
                if (ArchivoFormato != null && ArchivoFormato.ContentLength > 0)
                    try
                    {
                        

                        string path = Path.Combine(Server.MapPath("~/FILES/FORMATOS"), Path.GetFileName(ArchivoFormato.FileName));
                        if (!(System.IO.File.Exists(path)))//Si no existe un archivo de igual nombre
                        {
                            //ELIMINAMOS EL ARCHIVO EXISTENTE
                            if (fm.NombreFormato != null)
                            {
                                string oldpath = Path.Combine(Server.MapPath("~/FILES/FORMATOS"), Path.GetFileName(fm.NombreFormato));
                                System.IO.File.Delete(oldpath);
                            }

                            ArchivoFormato.SaveAs(path);
                            fm.NombreFormato = ArchivoFormato.FileName;
                        }
                        else
                        {
                            ViewBag.Message = "Ya se tiene registrado un archivo con el nombre: " + ArchivoFormato.FileName;
                            return RedirectToAction("Editar", new { id = fm.IdFormato });
                        }
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = "Error al guardar archivo: " + ex.ToString();
                        return RedirectToAction("Editar", new { id = fm.IdFormato });
                    }
                else
                {
                    ViewBag.Message = "Seleccionó un archivo inválido.";
                    return RedirectToAction("Editar", new { id = fm.IdFormato });
                }
                FormatoCN.EditarFormato(fm);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Error General: " + ex.ToString();
                return RedirectToAction("Editar", new { id = fm.IdFormato });
            }
        }


        public ActionResult Eliminar(int? id) //Para diferenciarlo del HttpPost
        {
            if (id == null)//Junto a esto
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var un = FormatoCN.DetalleFormato(id.Value);
            return View(un);
        }

        [HttpPost]
        public ActionResult Eliminar(int id)
        {
            try
            {
                var fm = FormatoCN.DetalleFormato(id);
                string nmfile = fm.NombreFormato;

                FormatoCN.EliminarFormato(id);
                //Eliminar Archivo Físico
                if (nmfile != "")
                {
                    string path = Path.Combine(Server.MapPath("~/FILES/FORMATOS"),Path.GetFileName(nmfile));
                    System.IO.File.Delete(path);
                }
                return Json(new { ok = true, toRedirect = Url.Action("Index") }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ok = false, toRedirect = Url.Action("Eliminar", new { id = id }), msg = ex.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetFormato()
        {
            var lista = FormatoCN.ListarFormato();
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }

        public FileResult DescargarFormato(string NomArchivo)
        {
            var FileVirtualPath = "~/FILES/FORMATOS/" + NomArchivo;
            return File(FileVirtualPath, Path.GetFileName(FileVirtualPath));
        }
    }
}