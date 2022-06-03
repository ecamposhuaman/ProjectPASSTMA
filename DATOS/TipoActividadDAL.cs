using ENTIDAD;
using System.Collections.Generic;
using System.Linq;

namespace DATOS
{
    public class TipoActividadDAL
    {
        public List<TIPOACTIVIDAD> ListTipoActividad()
        {
            using (var db = new PASSTMAContext())
            {
                db.Configuration.LazyLoadingEnabled = false; /*  LazyLoadingEnabled = false permite obtener datos de tablas relacionadas, las mismas que Json los lee como nulas  */
                return db.TIPOACTIVIDAD.ToList();
            }
        }

        public void AddTipoActividad(TIPOACTIVIDAD rs)
        {
            using (var db = new PASSTMAContext())
            {
                db.TIPOACTIVIDAD.Add(rs);
                db.SaveChanges();
            }
        }

        public TIPOACTIVIDAD DetailTipoActividad(int id)
        {
            using (var db = new PASSTMAContext())
            {
                return db.TIPOACTIVIDAD.Find(id);
                //o tambien esta linea de codigo
                //return db.Departamento.Where(d => d.IdDepartamento == id).FirstOrDefault();
            }
        }


        public void EditTipoActividad(TIPOACTIVIDAD rs)
        {
            using (var db = new PASSTMAContext())
            {
                var d = db.TIPOACTIVIDAD.Find(rs.IdTipo);
                d.NombreTipo = rs.NombreTipo;
                db.SaveChanges();
            }
        }

        public void DeleteTipoActividad(int id)
        {
            using (var db = new PASSTMAContext())
            {
                var d = db.TIPOACTIVIDAD.Find(id);
                db.TIPOACTIVIDAD.Remove(d);
                db.SaveChanges();
            }
        }
    }
}
