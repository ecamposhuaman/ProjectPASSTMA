using ClosedXML.Excel;
using ENTIDAD;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using NEGOCIO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ProjectPASSTMA.Controllers
{
    //[Authorize(Roles = "Administrador")]
    public class Sem_ActividadController : Controller
    {
        // GET: Sem_Actividad
        [Authorize(Roles = "Administrador, Responsable")]
        public ActionResult Index(int? IdSEM, int? cmbmeses)
        {
            TempData["IdSEM"] = IdSEM;
            TempData["mes"] = cmbmeses;
            string user_sha2 = User.Identity.GetUserId();
            int idus = Sem_ActividadCN.ObtenerUsuario(user_sha2);
            var usermanager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            if (usermanager.IsInRole(user_sha2, "Administrador"))
            {
                if (IdSEM == null)
                {
                    var sa = Sem_ActividadCN.ListarSem_Actividad_xSemMes(1, 0);
                    return View("IndexAdmin", sa);
                }
                else
                {
                    var sa = Sem_ActividadCN.ListarSem_Actividad_xSemMes(IdSEM.Value, cmbmeses.Value);
                    return View("IndexAdmin", sa);
                }
            }
            else
            {
                if (IdSEM == null)
                {
                    var sa = Sem_ActividadCN.ListarSem_Actividad_xUsuMes(idus, 0);
                    return View("Index", sa);
                }
                else
                {
                    var sa = Sem_ActividadCN.ListarSem_Actividad_xSemMes(IdSEM.Value, cmbmeses.Value);
                    return View("Index", sa);
                }
            }
        }

        public ActionResult Detalle(int id)
        {
            string user_sha2 = User.Identity.GetUserId();
            var usermanager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            if (usermanager.IsInRole(user_sha2, "Administrador"))
            {
                var sa = Sem_ActividadCN.DetalleSem_Actividad(id);
                return View("DetalleAdmin", sa);
            }
            else
            {
                var sa = Sem_ActividadCN.DetalleSem_Actividad(id);
                return View("Detalle", sa);
            }

        }

        [Authorize(Roles = "Administrador")]
        public ActionResult Crear()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(SEM_ACTIVIDAD sa, List<int> IdSEM, List<int> cmbmeses, INFORME Informe)
        {
            try
            {
                Informe.NombreInforme = "";
                sa.IdEstado = 1; //Estado Pendiente
                foreach (int mes in cmbmeses) //Para todas las fechas repetidas
                {
                    sa.Periodo = mes;
                    foreach (int id in IdSEM)//Para todo SEM selecionado
                    {
                        sa.IdSEM = id;
                        Sem_ActividadCN.AgregarSem_Actividad(sa, Informe);
                    }
                }
                return Json(new { ok = true, toRedirect = Url.Action("Index", "Sem_Actividad") }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ok = false, msg = ex.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult CerrarAsignacion(int id)
        {
            var sa = Sem_ActividadCN.DetalleSem_Actividad(id);
            return View(sa);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CerrarAsignacion(SEM_ACTIVIDADCE sa, INFORME Informe, HttpPostedFileBase ArchivoInforme)
        {
            try
            {
                DateTime dt = DateTime.Now;
                sa.FEjec = dt;
                sa.IdEstado = 2;

                Informe.NombreInforme = sa.NombreInforme;
                if (ArchivoInforme != null && ArchivoInforme.ContentLength > 0)
                {
                    try
                    {
                        if (sa.NombreInforme != null)
                        {
                            string oldpath = Path.Combine(Server.MapPath("~/FILES/INFORMES"), Path.GetFileName(sa.NombreInforme));
                            System.IO.File.Delete(oldpath);
                        }

                        //string cod = (DateTime.Now.ToString("yyyyMMddHHmmssF") + "_");
                        string cod = sa.IdSEMACT.ToString() + "_";
                        string path = Path.Combine(Server.MapPath("~/FILES/INFORMES"), Path.GetFileName(cod + ArchivoInforme.FileName));

                        ArchivoInforme.SaveAs(path);
                        Informe.NombreInforme = cod + ArchivoInforme.FileName;
                        //return Json(new { ok = false, msg = "Seleccione un Archivo de Informe" }, JsonRequestBehavior.AllowGet);
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = "Error al guardar archivo: " + ex.ToString();
                        return RedirectToAction("CerrarAsignacion", new { id = sa.IdSEMACT });
                        //return Json(new { ok = false, msg = ex.ToString() }, JsonRequestBehavior.AllowGet);
                    }
                }

                Sem_ActividadCN.EjecutarSem_Actividad(sa, Informe);
                return RedirectToAction("CerrarAsignacion", "Sem_Actividad", new { id = sa.IdSEMACT });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocurrió un error al cerrar Asignación: " + ex.ToString());
                return View(sa);
            }
        }

        [Authorize(Roles = "Administrador")]
        public ActionResult Editar(int id)
        {
            var sa = Sem_ActividadCN.DetalleSem_Actividad(id);
            return View(sa);
        }
        [HttpPost]
        public ActionResult Editar(SEM_ACTIVIDADCE sa, INFORME Informe, string DeleteArchivo)
        {
            try
            {
                //CÓDIGO PARA ELIMINAR LUEGO GUARDAR ARCHIVO
                if(DeleteArchivo == "Eliminar")
                {
                    Informe.NombreInforme = "";
                    if (sa.NombreInforme != null)
                    {
                        string oldpath = Path.Combine(Server.MapPath("~/FILES/INFORMES"), Path.GetFileName(sa.NombreInforme));
                        System.IO.File.Delete(oldpath);
                    }
                }
                Sem_ActividadCN.EditarSem_Actividad(sa, Informe);
                return Json(new { ok = true, toRedirect = Url.Action("Index") }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocurrió un error al modificar Asignación: " + ex.ToString());
                return View(sa);
            }
        }

        [Authorize(Roles = "Administrador")]
        public ActionResult Eliminar(int? id) //Para diferenciarlo del HttpPost
        {
            if (id == null)//Junto a esto
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var sa = Sem_ActividadCN.DetalleSem_Actividad(id.Value);
            return View(sa);
        }

        [HttpPost]
        public ActionResult Eliminar(int id)
        {
            try
            {
                var sa = Sem_ActividadCN.DetalleSem_Actividad(id);
                int idarch = sa.IdInforme;
                string nmfile = sa.NombreInforme;
                Sem_ActividadCN.EliminarSem_Actividad(id, idarch);
                //Eliminar Archivo Físico
                if (nmfile != "")
                {
                    string oldpath = Path.Combine(Server.MapPath("~/FILES/INFORMES"), Path.GetFileName(nmfile));
                    System.IO.File.Delete(oldpath);
                }
                return Json(new { ok = true, toRedirect = Url.Action("Index") }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ok = false, toRedirect = Url.Action("Eliminar", new { id = id }), msg = ex.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

        public FileResult DescargarInforme(string NomArchivo)
        {
            var FileVirtualPath = "~/FILES/INFORMES/" + NomArchivo;
            return File(FileVirtualPath, Path.GetFileName(FileVirtualPath)); // VER pdf en NUEVA PESTAÑA PARA DESCARGA
            /* return File(FileVirtualPath, "application/force- download", Path.GetFileName(FileVirtualPath));*/ // DESCARGAR PDF directamente
            // para VER el pdf en LA MISMA PESTAÑA se necesita eliminar "target = "_blank"" del ActionLink en la vista Index. 
        }


        public ActionResult Exportar()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Exportar(int IdUUNN, string Anio)
        {
            if (Anio != "")
                return ExportarExcel(IdUUNN, int.Parse(Anio));
            else
                return View();
        }
        public FileResult ExportarExcel(int IdUUNN, int Anio)
        {
            DateTime fecha = DateTime.Now.Date;
            var un = UUNNCN.DetalleUUNN(IdUUNN);
            string nomuunn = un.NombreUUNN;
            var sems = SEMCN.ListarCMBSEMByIdUUNN(IdUUNN);
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            List<String> nomsem = new List<String>();
            foreach (var s in sems)
            {
                var sa = Sem_ActividadCN.DataExportByIdSEM(s.IdSEM, Anio);
                dt = ConvertListToDataTable(sa);
                dt.TableName = s.NombreSEM.ToString();
                ds.Tables.Add(dt);
                //nomsem.Add(s.NombreSEM.ToString());
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                foreach (DataTable dat in ds.Tables)
                {
                    int longit = dat.Rows.Count + 7;
                    var ws = wb.Worksheets.Add(dat.TableName);
                    ws.Cell(1, 1).InsertTable(dat);

                    #region ESTILOS AL EXCEL
                    //ws.Columns("A", "AQ").Style.Fill.SetBackgroundColor(XLColor.Transparent);
                    ws.Columns("C", "AQ").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                    ws.Row(1).InsertRowsAbove(6);
                    ws.Rows(1, 5).Height = 25;
                    ws.Column("A").Width = 18;
                    ws.Column("B").Width = 84;
                    ws.Columns("D", "AO").Width = 6;
                    ws.Column("L").Width = 9;
                    ws.Column("U").Width = 9;
                    ws.Column("AD").Width = 9;
                    ws.Column("AM").Width = 9;
                    ws.Range("A1:B3").Merge();
                    ws.Range("D1:AM3").Merge();
                    ws.Range("AN1:AP1").Merge();
                    ws.Range("AN1:AP1").Value = "Código";
                    ws.Range("AN2:AP2").Merge();
                    ws.Range("AN2:AP2").Value = "Versión";
                    ws.Range("AN3:AP3").Merge();
                    ws.Range("AN3:AP3").Value = "Página";
                    ws.Range("AN1:AQ3").Style.Border.SetInsideBorder(XLBorderStyleValues.Thin);
                    ws.Range("AN1:AQ3").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin)
                                            .Alignment.SetVertical(XLAlignmentVerticalValues.Center);

                    ws.Range("AQ2").Value = fecha.ToShortDateString();

                    ws.Range("C1:AM3").Value = string.Format("{1}{0} {2}", Environment.NewLine, "PROGRAMA ANUAL DE SEGURIDAD, SALUD Y MEDIO AMBIENTE " + Anio, dat.TableName);
                    ws.Range("C1:AM3").Style
                        .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)
                        .Alignment.SetVertical(XLAlignmentVerticalValues.Center)
                        .Font.SetFontSize(20)
                        .Font.SetBold(true);

                    ws.Range("A4").Value = "Contenido";
                    ws.Range("A4:A6").Merge();
                    ws.Range("B4:B6").Value = ws.Range("A4:A6");
                    ws.Range("C4:C6").Value = ws.Range("A4:A6");
                    ws.Range("B4:B6").Value = "Actividades Programadas";
                    ws.Range("C4:C6").Value = "Formato a Utilizar";
                    ws.Column("C").Hide();
                    ws.Range("A4:C6").Style.Font.SetFontSize(14);

                    ws.Range("D4:K4").Merge();
                    ws.Range("D4:K4").Value = "PRIMER TRIMESTRE";
                    ws.Range("D4:K4").Style.Font.SetFontSize(14);
                    ws.Range("D5:E5").Merge();
                    ws.Range("D5:E5").Value = "ENERO";
                    ws.Range("F5:G5").Value = ws.Range("D5:E5");
                    ws.Range("H5:I5").Value = ws.Range("D5:E5");
                    ws.Range("J5:K5").Value = ws.Range("D5:E5");
                    ws.Range("F5:G5").Value = "FEBRERO";
                    ws.Range("H5:I5").Value = "MARZO";
                    ws.Range("J5:K5").Value = "TOTALES";
                    ws.Range("C6").Value = "Ejec";
                    ws.Range("E6").Value = "Ejec";
                    ws.Range("G6").Value = "Ejec";
                    ws.Range("I6").Value = "Ejec";
                    ws.Range("D6").Value = "Prog";
                    ws.Range("F6").Value = "Prog";
                    ws.Range("H6").Value = "Prog";
                    ws.Range("J6").Value = "Prog";
                    ws.Range("L4:L6").Merge();
                    ws.Range("L4:L6").Value = "Cumplimiento Trimestral (%)";
                    ws.Range("L4:L6").Style.Alignment.WrapText = true;
                    ws.Range("L4:L6").Style.Alignment.TextRotation = 90;
                    ws.Range("L4:L6").Style.Font.SetFontSize(10);

                    ws.Range("M4").Value = ws.Range("D4:L6");
                    ws.Range("V4").Value = ws.Range("D4:L6");
                    ws.Range("AE4").Value = ws.Range("D4:L6");
                    ws.Range("M4").Value = "SEGUNDO TRIMESTRE";
                    ws.Range("V4").Value = "TERCER TRIMESTRE";
                    ws.Range("AE4").Value = "CUARTO TRIMESTRE";
                    ws.Range("M5").Value = "ABRIL";
                    ws.Range("O5").Value = "MAYO";
                    ws.Range("Q5").Value = "JUNIO";
                    ws.Range("S5").Value = "TOTALES";
                    ws.Range("V5").Value = "JULIO";
                    ws.Range("X5").Value = "AGOSTO";
                    ws.Range("Z5").Value = "SEPTIEMBRE";
                    ws.Range("AB5").Value = "TOTALES";
                    ws.Range("AE5").Value = "OCTUBRE";
                    ws.Range("AG5").Value = "NOVIEMBRE";
                    ws.Range("AI5").Value = "DICIEMBRE";
                    ws.Range("AK5").Value = "TOTALES";

                    ws.Range("AN4:AO4").Merge();
                    ws.Range("AN4").Value = "ANUAL";
                    ws.Range("AN4:AO4").Style.Font.SetFontSize(14);
                    ws.Range("AN5:AO5").Merge();
                    ws.Range("AN5").Value = "TOTALES";
                    ws.Range("AN6").Value = "Prog";
                    ws.Range("AO6").Value = "Ejec";

                    ws.Range("AP4:AP6").Merge();
                    ws.Range("AP4:AP6").Value = "Avance Anual (%)";
                    ws.Range("AQ4:AQ6").Merge();
                    ws.Range("AQ4:AQ6").Value = "Avance por ITEM (%)";
                    ws.Range("AP4:AQ6").Style.Font.SetFontSize(10);
                    ws.Range("AP4:AQ6").Style.Alignment.WrapText = true;

                    ws.Range("A4:AQ6").Style.Fill.SetBackgroundColor(XLColor.Blue)
                            .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)
                            .Alignment.SetVertical(XLAlignmentVerticalValues.Center)
                            .Font.SetFontColor(XLColor.White);
                    ws.Range("A4:AQ6").Style.Border.SetInsideBorder(XLBorderStyleValues.Thin);
                    ws.Range("A4:AQ6").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);

                    ws.Range("L8:L" + longit).Style
                     .Font.SetBold(true)
                     .Font.SetFontColor(XLColor.Red);
                    ws.Range("U8:U" + longit).Style
                     .Font.SetBold(true)
                     .Font.SetFontColor(XLColor.Red);
                    ws.Range("AD8:AD" + longit).Style
                     .Font.SetBold(true)
                     .Font.SetFontColor(XLColor.Red);
                    ws.Range("AM8:AM" + longit).Style
                     .Font.SetBold(true)
                     .Font.SetFontColor(XLColor.Red);
                    ws.Range("AP8:AP" + longit).Style
                     .Font.SetBold(true)
                     .Font.SetFontColor(XLColor.Red);

                    ws.Range("J8:K" + longit).Style
                    .Font.SetBold(true)
                    .Font.SetFontColor(XLColor.Blue);
                    ws.Range("S8:T" + longit).Style
                    .Font.SetBold(true)
                    .Font.SetFontColor(XLColor.Blue);
                    ws.Range("AB8:AC" + longit).Style
                    .Font.SetBold(true)
                    .Font.SetFontColor(XLColor.Blue);
                    ws.Range("AK8:AL" + longit).Style
                    .Font.SetBold(true)
                    .Font.SetFontColor(XLColor.Blue);
                    ws.Range("AN8:AO" + longit).Style
                    .Font.SetBold(true)
                    .Font.SetFontColor(XLColor.Blue);

                    var imagePath = @"~/FILES/Logo_ELC.jpg";
                    ws.AddPicture(Server.MapPath(imagePath))
                        .MoveTo(ws.Cell("B1"))
                        //.WithSize(80,20)
                        .Scale(0.13); // optional: resize picture
                    ws.Row(7).Hide();
                    #endregion

                }

                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "PASST_" + Anio + " " + nomuunn + ".xlsx");
                }
            }
        }
        public static DataTable ConvertListToDataTable<T>(IList<T> sa)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in sa)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }

        public ActionResult MostrarContenido(string nombreInforme)
        {
            string embed = "<object data=\"{0}\" type =\"application/pdf\" width=\"600px\" height=\"500px\">";
            embed += "Si no puede ver el informe es porque no existe o puede descargarlo desde <a href = \"{0}\">Aquí</a>";
            embed += " o descargar <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> para ver el archivo.";
            embed += "</object>";

            TempData["Embed"] = string.Format(embed, VirtualPathUtility.ToAbsolute("~/FILES/INFORMES/" + nombreInforme));
            return PartialView("EditarPartial", TempData["Embed"]);
        }
        public ActionResult Revisar(int id)
        {
            var sa = Sem_ActividadCN.DetalleSem_Actividad(id);
            return View(sa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Revisar(SEM_ACTIVIDADCE sa) //OBSERVAR
        {
            int idsa = sa.IdSEMACT;
            string com = sa.Comunicado;
            try
            {
                string user = User.Identity.GetUserId();
                int us = Sem_ActividadCN.ObtenerUsuario(user);
                Sem_ActividadCN.ObservarSem_Actividad(idsa, 3, us, com);
                return RedirectToAction("Revisar", new { id = idsa });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Revisar", new { id = idsa });
            }
        }

        public ActionResult Aceptar(int id)
        {
            try
            {
                string user = User.Identity.GetUserId();
                int us = Sem_ActividadCN.ObtenerUsuario(user);

                Sem_ActividadCN.RevisaSem_Actividad(id, 4, us);
                
                return RedirectToAction("Revisar", new { id = id });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Revisar", new { id = id });
            }
        }
        
    }
}