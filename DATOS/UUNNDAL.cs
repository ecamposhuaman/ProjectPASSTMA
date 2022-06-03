using ENTIDAD;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATOS
{
    public class UUNNDAL
    {
        public List<UUNNCE> ListUUNN()
        {
            string sql = @"SELECT u.IdUUNN, u.NombreUUNN, u.IdSEDE, s.NombreSEDE  
                            from UUNN u
                            inner join SEDE s on u.IdSEDE = s.IdSEDE
                            WHERE u.NombreUUNN != ''";

            using (var db = new PASSTMAContext())
            {
                return db.Database.SqlQuery<UUNNCE>(sql).ToList();
            }
        }

        public void AddUUNN(UUNN un)
        {
            using (var db = new PASSTMAContext())
            {
                db.UUNN.Add(un);
                db.SaveChanges();
            }
        }

        public UUNNCE DetailUUNN(int id)
        {
            string sql = @"SELECT u.IdUUNN, u.NombreUUNN, u.IdSEDE, s.NombreSEDE  
                            from UUNN u
                            inner join SEDE s on u.IdSEDE = s.IdSEDE
                            WHERE u.IdUUNN = @IdUUNN";
            using (var db = new PASSTMAContext())
            {
                return db.Database.SqlQuery<UUNNCE>(sql,
                    new SqlParameter("@IdUUNN", id)).FirstOrDefault();
            }
        }

        public void EditUUNN(UUNN un)
        {
            using (var db = new PASSTMAContext())
            {
                var d = db.UUNN.Find(un.IdUUNN);
                d.NombreUUNN = un.NombreUUNN;
                d.IdSEDE = un.IdSEDE;
                db.SaveChanges();
            }
        }

        public void DeleteUUNN(int id)
        {
            using (var db = new PASSTMAContext())
            {
                var e = db.UUNN.Find(id);
                db.UUNN.Remove(e);
                db.SaveChanges();
            }
        }
    }
}
