using ENTIDAD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATOS
{
    public class SEDEDAL
    {
        public List<SEDE> ListSEDE()
        {
            using (var db = new PASSTMAContext())
            {
                db.Configuration.LazyLoadingEnabled = false; /*  LazyLoadingEnabled = false permite obtener datos de tablas relacionadas, las mismas que Json los lee como nulas  */
                return db.SEDE.ToList();
            }
        }

        public void AddSEDE(SEDE sd)
        {
            using (var db = new PASSTMAContext())
            {
                db.SEDE.Add(sd);
                db.SaveChanges();
            }
        }

        public SEDE DetailSEDE(int id)
        {
            using (var db = new PASSTMAContext())
            {
                return db.SEDE.Find(id);
                //o tambien esta linea de codigo
                //return db.Departamento.Where(d => d.IdDepartamento == id).FirstOrDefault();
            }
        }


        public void EditSEDE(SEDE sd)
        {
            using (var db = new PASSTMAContext())
            {
                var d = db.SEDE.Find(sd.IdSEDE);
                d.NombreSEDE = sd.NombreSEDE;
                db.SaveChanges();
            }
        }

        public void DeleteSEDE(int id)
        {
            using (var db = new PASSTMAContext())
            {
                var d = db.SEDE.Find(id);
                db.SEDE.Remove(d);
                db.SaveChanges();
            }
        }
    }
}
