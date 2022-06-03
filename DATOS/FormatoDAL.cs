using ENTIDAD;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATOS
{
    public class FormatoDAL
    {
        public List<FORMATO> ListFormato()
        {
            string sql = @"SELECT * FROM FORMATO WHERE NombreFormato != ''";

            using (var db = new PASSTMAContext())
            {
                return db.Database.SqlQuery<FORMATO>(sql).ToList();
            }
        }

        public void AddFormato(FORMATO arch)
        {
            using (var db = new PASSTMAContext())
            {
                db.FORMATO.Add(arch);
                db.SaveChanges();
            }
        }

        public FORMATO DetailFormato(int id)
        {
            string sql = @"SELECT * FROM FORMATO WHERE IdFormato = @IdFormato";
            using (var db = new PASSTMAContext())
            {
                return db.Database.SqlQuery<FORMATO>(sql,
                    new SqlParameter("@IdFormato", id)).FirstOrDefault();
            }
        }


        public void EditFormato(FORMATO arch)
        {
            using (var db = new PASSTMAContext())
            {
                var d = db.FORMATO.Find(arch.IdFormato);
                d.NombreFormato = arch.NombreFormato;
                d.Descripcion = arch.Descripcion;
                db.SaveChanges();
            }
        }

        public void DeleteFormato(int id)
        {
            using (var db = new PASSTMAContext())
            {
                var e = db.FORMATO.Find(id);
                db.FORMATO.Remove(e);
                db.SaveChanges();
            }
        }
    }
}
