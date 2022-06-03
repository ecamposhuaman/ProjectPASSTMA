using ENTIDAD;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATOS
{
    public class ResponsableDAL
    {
        public List<RESPONSABLE> ListResponsable()
        {
            using (var db = new PASSTMAContext())
            {
                db.Configuration.LazyLoadingEnabled = false; /*  LazyLoadingEnabled = false permite obtener datos de tablas relacionadas, las mismas que Json los lee como nulas  */
                return db.RESPONSABLE.ToList();
            }
        }

        public void AddResponsable(RESPONSABLE rs)
        {
            using (var db = new PASSTMAContext())
            {
                db.RESPONSABLE.Add(rs);
                db.SaveChanges();
            }
        }

        public RESPONSABLE DetailResponsable(int id)
        {
            using (var db = new PASSTMAContext())
            {
                return db.RESPONSABLE.Find(id);
                //o tambien esta linea de codigo
                //return db.Departamento.Where(d => d.IdDepartamento == id).FirstOrDefault();
            }
        }
        public RESPONSABLE DetailResponsableByEmail(string email)
        {
            string sql = @"SELECT * FROM RESPONSABLE WHERE EmailResponsable = @email";

            using (var db = new PASSTMAContext())
            {
                return db.Database.SqlQuery<RESPONSABLE>(sql,
                    new SqlParameter("@email", email)).FirstOrDefault();
            }
            //using (var db = new PASSTMAContext())
            //{
            //    return db.RESPONSABLE.Find(email);
            //}
        }


        public void EditResponsable(RESPONSABLE rs, int action)
        {
            if(action == 2)//Modificar datos y Restablecer cuenta
            {
                var idNetUser = "";
                using (var db = new PASSTMAContext())
                {
                    var d = db.RESPONSABLE.Find(rs.IdResponsable);
                    idNetUser = d.IdAspNetUser;
                    d.NombreResponsable = rs.NombreResponsable;
                    d.EmailResponsable = rs.EmailResponsable;
                    d.NombreRol = rs.NombreRol;
                    d.IdAspNetUser = null;//Quitamos la relación
                    db.SaveChanges();
                }
                using (var db = new PASSTMAContext())//Eliminamos objeto de AspNetUser (Registrados)
                {
                    var e = db.AspNetUsers.Find(idNetUser);
                    db.AspNetUsers.Remove(e);
                    db.SaveChanges();
                }
            }
            else
            {
                using (var db = new PASSTMAContext())
                {
                    var d = db.RESPONSABLE.Find(rs.IdResponsable);
                    d.NombreResponsable = rs.NombreResponsable;
                    d.EmailResponsable = rs.EmailResponsable;
                    d.NombreRol = rs.NombreRol;
                    db.SaveChanges();
                }
            }
            
            
        }

        public void DeleteResponsable(int id)
        {
            var idNetUser = "";
            using (var db = new PASSTMAContext())
            {
                var e = db.RESPONSABLE.Find(id);
                idNetUser = e.IdAspNetUser;
                db.RESPONSABLE.Remove(e);
                db.SaveChanges();
            }
            if (idNetUser != null)
            {
                using (var db = new PASSTMAContext())
                {
                    var e = db.AspNetUsers.Find(idNetUser);
                    db.AspNetUsers.Remove(e);
                    db.SaveChanges();
                }
            }
        }


        public RESPONSABLE VerificateUser(string email)
        {
            string sql = @"SELECT IdResponsable, NombreResponsable, EmailResponsable, IdAspNetUser, NombreRol
                            from RESPONSABLE 
                            WHERE EmailResponsable = @email";
            using (var db = new PASSTMAContext())
            {
                return db.Database.SqlQuery<RESPONSABLE>(sql,
                    new SqlParameter("@email", email)).FirstOrDefault();
            }
        }
        public void EditUser(int idresp, string key)
        {
            using (var db = new PASSTMAContext())
            {
                var d = db.RESPONSABLE.Find(idresp);
                d.IdAspNetUser = key;
                db.SaveChanges();
            }
        }


        public List<RESPONSABLE> ListCMBResponsable()
        {
            string sql = @"SELECT * from RESPONSABLE WHERE NombreResponsable != ''";

            using (var db = new PASSTMAContext())
            {
                return db.Database.SqlQuery<RESPONSABLE>(sql).ToList();
            }
        }
    }
}
