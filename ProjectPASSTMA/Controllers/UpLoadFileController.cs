using ProjectPASSTMA.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectPASSTMA.Controllers
{
    public class UpLoadFileController : Controller
    {
        // GET: UpLoadFile
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Index(HttpPostedFileBase Archivo)
        {
            if (Archivo != null && Archivo.ContentLength > 0)
                try
                {
                    string path = Path.Combine(Server.MapPath("~/FILES"), 
                        Path.GetFileName(Archivo.FileName));

                    Archivo.SaveAs(path);
                    ViewBag.Message = "Archivo enviado exitosamente";
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR: " + ex.Message.ToString();
                }
            else
            {
                ViewBag.Message = "Ningún archivo especificado";
            }
            return View();
            //var items = GetFiles();
            //return View(items);
        }
        

        public ActionResult ListaArchivos()
        {
            var archvs = ObtenerArchivos();
            return View(archvs);
        }
        
        private List<string> ObtenerArchivos()
        {
            var dir = new System.IO.DirectoryInfo(Server.MapPath("~/FILES"));
            System.IO.FileInfo[] fileNames = dir.GetFiles("*.*");

            List<string> items = new List<string>();
            foreach (var file in fileNames)
            {
                items.Add(file.Name);
            }
            return items;
        }

        public FileResult Descargar(string filename)
        {
            var FileVirtualPath = "~/FILES/" + filename;
            return File(FileVirtualPath, "application/force- download", Path.GetFileName(FileVirtualPath));
        }
    }
}