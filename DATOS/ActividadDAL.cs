using ENTIDAD;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DATOS
{
    public class ActividadDAL
    {
        public List<ACTIVIDADCE> ListActividad()
        {
            string sql = @"Select ac.IdActividad, ac.NombreActividad, ac.IdTipo, tp.NombreTipo, 
							isnull(ac.IdFormato, '') as IdFormato, isnull(ar.NombreFormato, '') as NombreFormato
                            from ACTIVIDAD ac
                            left join FORMATO ar on ac.IdFormato = ar.IdFormato
                            inner join TIPOACTIVIDAD tp on ac.IdTipo = tp.IdTipo
                            WHERE ac.NombreActividad != ''";
            using (var db = new PASSTMAContext())
            {
                return db.Database.SqlQuery<ACTIVIDADCE>(sql).ToList();
            }
        }

        public void AddActividad(ACTIVIDAD ac)
        {
            using (var db = new PASSTMAContext())
            {
                db.ACTIVIDAD.Add(ac);
                db.SaveChanges();
            }
        }

        public ACTIVIDADCE DetailActividad(int id)
        {
            string sql = @"Select ac.IdActividad, ac.NombreActividad, ac.IdTipo, tp.NombreTipo,
                            isnull(ac.IdFormato, '') as IdFormato, isnull(ar.NombreFormato, '') as NombreFormato
                            from ACTIVIDAD ac
                            left join FORMATO ar on ac.IdFormato = ar.IdFormato
                            inner join TIPOACTIVIDAD tp on ac.IdTipo = tp.IdTipo
                            where ac.IdActividad = @IdActividad";
            using (var db = new PASSTMAContext())
            {
                return db.Database.SqlQuery<ACTIVIDADCE>(sql,
                    new SqlParameter("@IdActividad", id)).FirstOrDefault();
            }
        }


        public void EditActividad(ACTIVIDADCE ac)
        {
            using (var db = new PASSTMAContext())
            {
                var e = db.ACTIVIDAD.Find(ac.IdActividad);
                e.NombreActividad = ac.NombreActividad;
                e.IdTipo = ac.IdTipo;
                e.IdFormato = ac.IdFormato;
                db.SaveChanges();
            }
        }

        public void DeleteActividad(int id)
        {
            using (var db = new PASSTMAContext())
            {
                var d = db.ACTIVIDAD.Find(id);
                db.ACTIVIDAD.Remove(d);
                db.SaveChanges();
            }
        }

        public List<ACTIVIDAD> ListCMBActividad()
        {
            string sql = @"SELECT * FROM ACTIVIDAD WHERE NombreActividad != ''";
            using (var db = new PASSTMAContext())
            {
                return db.Database.SqlQuery<ACTIVIDAD>(sql).ToList();
            }
        }

        public List<ACTIVIDAD> ListCMBActividadByTipo(int idtipo)
        {
            
            string sql = @"SELECT * FROM ACTIVIDAD WHERE IdTipo = @IdTipo and NombreActividad != ''";
            using (var db = new PASSTMAContext())
            {
                return db.Database.SqlQuery<ACTIVIDAD>(sql,
                    new SqlParameter("@IdTipo", idtipo)).ToList();
            }
        }
    } 
}
