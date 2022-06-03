using ENTIDAD;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATOS
{
    public class SEMDAL
    {
        public List<SEMCE> ListSEM()
        {
            string sql = @"SELECT s.IdSEM, s.NombreSEM, s.IdUUNN, u.NombreUUNN, sd.NombreSEDE, r.NombreResponsable, s.EsPrincipal
                            FROM SEM s
                            inner join (UUNN u inner join SEDE sd on u.IdSEDE = sd.IdSEDE) on s.IdUUNN = u.IdUUNN
                            inner join RESPONSABLE r on s.IdResponsable = r.IdResponsable
                            WHERE s.NombreSEM != ''";

            using (var db = new PASSTMAContext())
            {
                return db.Database.SqlQuery<SEMCE>(sql).ToList();
            }
        }

        public void AddSEM(SEM sm)
        {
            using (var db = new PASSTMAContext())
            {
                db.SEM.Add(sm);
                db.SaveChanges();
            }
        }

        public SEMCE DetailSEM(int id)
        {
            string sql = @"SELECT s.IdSEM, s.NombreSEM, s.IdUUNN, u.NombreUUNN, sd.IdSEDE, sd.NombreSEDE, r.IdResponsable, r.NombreResponsable, s.EsPrincipal
                            FROM SEM s
                            inner join (UUNN u inner join SEDE sd on u.IdSEDE = sd.IdSEDE) on s.IdUUNN = u.IdUUNN
                            inner join RESPONSABLE r on s.IdResponsable = r.IdResponsable
                            WHERE s.IdSEM = @IdSEM";
            using (var db = new PASSTMAContext())
            {
                return db.Database.SqlQuery<SEMCE>(sql,
                    new SqlParameter("@IdSEM", id)).FirstOrDefault();
            }
        }


        public void EditSEM(SEM sm)
        {
            using (var db = new PASSTMAContext())
            {
                var d = db.SEM.Find(sm.IdSEM);
                d.NombreSEM = sm.NombreSEM;
                d.IdUUNN = sm.IdUUNN;
                d.IdResponsable = sm.IdResponsable;
                d.EsPrincipal = sm.EsPrincipal;
                db.SaveChanges();
            }
        }

        public void DeleteSEM(int id)
        {
            using (var db = new PASSTMAContext())
            {
                var e = db.SEM.Find(id);
                db.SEM.Remove(e);
                db.SaveChanges();
            }
        }

        public List<SEM> ListCMBSEM()
        {
            string sql = @"SELECT * FROM SEM WHERE NombreSEM != ''";
            using (var db = new PASSTMAContext())
            {
                return db.Database.SqlQuery<SEM>(sql).ToList();
            }
        }

        public List<SEM> ListCMBSEMByIdUUNN(int iduunn)
        {
            if (iduunn == 0)
            {
                string sql = @"SELECT * FROM SEM WHERE NombreSEM != ''";
                using (var db = new PASSTMAContext())
                {
                    return db.Database.SqlQuery<SEM>(sql).ToList();
                }
            }
            else
            {
                string sql = @"SELECT * FROM SEM WHERE IdUUNN = @IdUUNN and NombreSEM != ''";
                using (var db = new PASSTMAContext())
                {
                    return db.Database.SqlQuery<SEM>(sql,
                        new SqlParameter("@IdUUNN", iduunn)).ToList();
                }
            }

        }

        public List<SEM> ListCMBSEMByUser(string user)
        {
            string sql = @"ListarSEMByUser @respons";
            using (var db = new PASSTMAContext())
            {
                return db.Database.SqlQuery<SEM>(sql,
                    new SqlParameter("@respons", user)).ToList();
            }
        }
    }
}
