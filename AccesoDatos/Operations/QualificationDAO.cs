using AccesoDatos.Context;
using AccesoDatos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Operations
{
    public class QualificationDAO
    {
        public DbEscuelaContext context = new DbEscuelaContext();

        public List<Calificacion> GetByIdQualification(int id)
        {
            var qualification = context.Calificacions.Where(c => c.MatriculaId == id).ToList();
            return qualification;
        }

        public bool InsertQualification(Calificacion qua)
        {
            try
            {
                context.Calificacions.Add(qua);
                context.SaveChanges();

                return true;

            }catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteQualification(int id)
        {
            try
            {
                var quaDelete = context.Calificacions.Where(c => c.Id == id).FirstOrDefault();
                if (quaDelete != null)
                {
                    context.Calificacions.Remove(quaDelete);
                    context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
