using ENTIDAD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATOS
{
    public class EstadoDAL
    {
        public List<ESTADO> ListEstado()
        {
            using (var db = new PASSTMAContext())
            {
                db.Configuration.LazyLoadingEnabled = false; /*  LazyLoadingEnabled = false permite obtener datos de tablas relacionadas, las mismas que Json los lee como nulas  */
                return db.ESTADO.ToList();
            }
        }

        public void AddEstado(ESTADO rs)
        {
            using (var db = new PASSTMAContext())
            {
                db.ESTADO.Add(rs);
                db.SaveChanges();
            }
        }

        public ESTADO DetailEstado(int id)
        {
            using (var db = new PASSTMAContext())
            {
                return db.ESTADO.Find(id);
                //o tambien esta linea de codigo
                //return db.Departamento.Where(d => d.IdDepartamento == id).FirstOrDefault();
            }
        }


        public void EditEstado(ESTADO rs)
        {
            using (var db = new PASSTMAContext())
            {
                var d = db.ESTADO.Find(rs.IdEstado);
                d.NombreEstado = rs.NombreEstado;
                db.SaveChanges();
            }
        }

        public void DeleteEstado(int id)
        {
            using (var db = new PASSTMAContext())
            {
                var d = db.ESTADO.Find(id);
                db.ESTADO.Remove(d);
                db.SaveChanges();
            }
        }
    }
}
