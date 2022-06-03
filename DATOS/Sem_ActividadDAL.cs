using ENTIDAD;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace DATOS
{
    public class Sem_ActividadDAL
    {

        public int ObtenerIdUsuario(string user)
        {
            string idsha = user;
            var resp = new RESPONSABLE();
            try
            {
                using (var context = new PASSTMAContext())
                {
                    resp = context.RESPONSABLE
                                    //.Include("Cursos")
                                    .Where(x => x.IdAspNetUser == idsha)
                                    .Single();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            int iduser = resp.IdResponsable;
            return iduser;
        }
        public List<SEM_ACTIVIDADCE> ListSem_Actividad()
        {
            string sql = @"SELECT sa.IdSEMACT, sa.IdSEM, s.NombreSEM, sa.IdActividad, a.NombreActividad, 
                            sa.Periodo, sa.IdEstado, e.NombreEstado, sa.IdInforme, arc.NombreInforme, 
                            sa.FEjec, sa.IdRevisor, r.NombreResponsable as NombreRevisor, sa.Cantidad, 
                            sa.Comunicado, sa.Observacion, frm.NombreFormato
                            from SEM_ACTIVIDAD sa
                            inner join SEM s on sa.IdSEM = s.IdSEM
                            inner join (ACTIVIDAD a left join FORMATO frm on a.IdFormato = frm.IdFormato) on sa.IdACTIVIDAD = a.IdACTIVIDAD
                            inner join ESTADO e on sa.IdEstado = e.IdEstado
                            left join INFORME arc on sa.IdInforme = arc.IdInforme
                            left join RESPONSABLE r on sa.IdRevisor = r.IdResponsable";

            using (var db = new PASSTMAContext())
            {
                return db.Database.SqlQuery<SEM_ACTIVIDADCE>(sql).ToList();
            }
        }

        public List<SEM_ACTIVIDADCE> ListSem_Actividad_xUsuMes(int idus, int cmbmeses)
        {
            string sql = @"ListarAsignaciones_xUsuMes @idus, @mes";

            using (var db = new PASSTMAContext())
            {
                return db.Database.SqlQuery<SEM_ACTIVIDADCE>(sql,
                    new SqlParameter("@idus", idus),
                    new SqlParameter("@mes", cmbmeses)).ToList();
            }
        }

        public List<SEM_ACTIVIDADCE> ListSem_Actividad_xSemMes(int idsem, int cmbmeses)
        {
            string sql = @"ListarAsignaciones_xSemMes @idsem, @mes";

            using (var db = new PASSTMAContext())
            {
                return db.Database.SqlQuery<SEM_ACTIVIDADCE>(sql,
                    new SqlParameter("@idsem", idsem),
                    new SqlParameter("@mes", cmbmeses)).ToList();
            }
        }

        public void AddSem_Actividad(SEM_ACTIVIDAD sa, INFORME Informe)
        {
            int idfile;
            using (var db = new PASSTMAContext())
            {
                db.INFORME.Add(Informe);
                db.SaveChanges();
                idfile = Informe.IdInforme;
            }
            using (var db = new PASSTMAContext())
            {
                idfile = Informe.IdInforme;
                sa.IdInforme = idfile;
                db.SEM_ACTIVIDAD.Add(sa);
                db.SaveChanges();
            }
        }

        public SEM_ACTIVIDAD DetailSem_Actividad_Asignacion(int id)
        {

            SEM_ACTIVIDAD dt = new SEM_ACTIVIDAD
            {
                IdActividad = id
            };
            return dt;
        }

        public SEM_ACTIVIDADCE DetailSem_Actividad(int id)
        {
            string sql = @"SELECT sa.IdSEMACT, sa.IdSEM, s.NombreSEM, sa.IdActividad, a.NombreActividad, 
                            sa.Periodo, sa.IdEstado, e.NombreEstado, sa.IdInforme, ISNULL(NULLIF(arc.NombreInforme,' '), 'Sin Informe') as NombreInforme, 
                            sa.FEjec, sa.IdRevisor, r.NombreResponsable as NombreRevisor, sa.Cantidad, sa.Comunicado, sa.Observacion
                            from SEM_ACTIVIDAD sa
                            inner join SEM s on sa.IdSEM = s.IdSEM
                            inner join ACTIVIDAD a on sa.IdACTIVIDAD = a.IdACTIVIDAD
                            inner join ESTADO e on sa.IdEstado = e.IdEstado
                            left join INFORME arc on sa.IdInforme = arc.IdInforme
                            left join RESPONSABLE r on sa.IdRevisor = r.IdResponsable
                            WHERE sa.IdSEMACT = @IdSEMACT";

            using (var db = new PASSTMAContext())
            {
                return db.Database.SqlQuery<SEM_ACTIVIDADCE>(sql,
                    new SqlParameter("@IdSEMACT", id)).FirstOrDefault();
            }
        }


        public void EditSem_Actividad(SEM_ACTIVIDADCE sa, INFORME Informe)
        {
            if(Informe.NombreInforme == "")
            {
                using (var db = new PASSTMAContext())
                {
                    var i = db.INFORME.Find(sa.IdInforme);
                    i.NombreInforme = "";
                    db.SaveChanges();
                }
            }
            using (var db = new PASSTMAContext())
            {
                var e = db.SEM_ACTIVIDAD.Find(sa.IdSEMACT);
                e.IdSEM = sa.IdSEM;
                e.IdActividad = sa.IdActividad;
                e.Periodo = sa.Periodo;
                e.IdEstado = sa.IdEstado;
                e.Cantidad = sa.Cantidad;
                e.Comunicado = sa.Comunicado;
                e.Observacion = sa.Observacion;
                db.SaveChanges();
            }
        }

        public void EjectSem_Actividad(SEM_ACTIVIDADCE sa, INFORME Informe)
        {
            using (var db = new PASSTMAContext())
            {
                var d = db.INFORME.Find(sa.IdInforme);
                d.NombreInforme = Informe.NombreInforme;
                db.SaveChanges();
                var e = db.SEM_ACTIVIDAD.Find(sa.IdSEMACT);
                e.IdEstado = sa.IdEstado;
                e.FEjec = sa.FEjec;
                e.Observacion = sa.Observacion;
                db.SaveChanges();
            }
        }

        public void RevSem_Actividad(int id, int idestado, int idrevisor)
        {
            using (var db = new PASSTMAContext())
            {
                var e = db.SEM_ACTIVIDAD.Find(id);
                e.IdEstado = idestado;
                e.IdRevisor = idrevisor;
                db.SaveChanges();
            }
        }

        public void ObsSem_Actividad(int id, int idestado, int idrevisor, string com)
        {
            if (com == null)
            {
                using (var db = new PASSTMAContext())
                {
                    var e = db.SEM_ACTIVIDAD.Find(id);
                    e.IdEstado = idestado;
                    e.IdRevisor = idrevisor;
                    db.SaveChanges();
                }
            }
            else
            {
                using (var db = new PASSTMAContext())
                {
                    var e = db.SEM_ACTIVIDAD.Find(id);

                    e.IdEstado = idestado;
                    e.IdRevisor = idrevisor;
                    e.Comunicado = com;
                    db.SaveChanges();
                }
            }
        }

        public void DeleteSem_Actividad(int id, int idarch)
        {
            using (var db = new PASSTMAContext())
            {
                var d = db.SEM_ACTIVIDAD.Find(id);
                db.SEM_ACTIVIDAD.Remove(d); // Se permite eliminar todo el registro porque la tabla es la ultima parte de la relación en la BBDD
                db.SaveChanges();

                var e = db.INFORME.Find(idarch);
                db.INFORME.Remove(e);
                db.SaveChanges();
            }
        }

        public List<SEMACTByIdSEM> DataExport_ByIdSEM(int idsem, int Anio)
        {
            string sql = @"ExportarPorIdSEM @idsem, @anio";

            using (var db = new PASSTMAContext())
            {
                return db.Database.SqlQuery<SEMACTByIdSEM>(sql,
                    new SqlParameter("@idsem", idsem),
                    new SqlParameter("@anio", Anio)).ToList();
            }
        }
    }
}
