using AccesoDatos.Context;
using AccesoDatos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Operations
{
    public class TeacherDAO
    {
        public DbEscuelaContext context = new DbEscuelaContext();

        public Profesor Login(string username, string password)
        {
            var teacher = context.Profesors.Where(t => t.Usuario == username && t.Pass == password).FirstOrDefault();
            return teacher;
        }

        public List<TeacherStudent> teacherStudents(string usuario)
        {
            var query = from a in context.Alumnos
                        join m in context.Matriculas on a.Id equals m.AlumnoId
                        join asig in context.Asignaturas on m.AsignaturaId equals asig.Id
                        where asig.Profesor == usuario
                        select new TeacherStudent
                        {
                            Id = a.Id,
                            Dni = a.Dni,
                            Nombre = a.Nombre,
                            Direccion = a.Direccion,
                            Edad = a.Edad,
                            Email = a.Email,
                            Asignatura = asig.Nombre
                        };
            return query.ToList();
        }

        public Alumno GetByIdStudent(int id)
        {
            var idStudent = context.Alumnos.Where(a => a.Id == id).FirstOrDefault();
            return idStudent;
        }

        public bool InsertStudent(string dni, string nombre, string direccion, int edad, string email)
        {
            try
            {
                Alumno student = new Alumno();
                student.Dni = dni;
                student.Nombre = nombre;
                student.Direccion = direccion;
                student.Edad = edad;
                student.Email = email;
                
                context.Alumnos.Add(student);
                context.SaveChanges();

                return true;

            }catch(Exception e)
            {
                return false;
            }
        }

        public Alumno GetTuitionStudent(string dni)
        {
            var dniStudent = context.Alumnos.Where(n => n.Dni == dni).FirstOrDefault();
            return dniStudent;
        }

        public bool InsertStudentTuition(string dni, string nombre, string direccion, int edad, string email, int id_asig)
        {
            try
            {
               var exist = GetTuitionStudent(dni);

                if (exist == null)
                {
                    InsertStudent(dni, nombre, direccion, edad, email);
                    var insertStudent = GetTuitionStudent(dni);
                    Matricula m = new Matricula();
                    m.AlumnoId = insertStudent.Id;
                    m.AsignaturaId = id_asig;
                    context.Matriculas.Add(m);
                    context.SaveChanges();
                }
                else
                {
                    Matricula m = new Matricula();
                    m.AlumnoId = exist.Id;
                    m.AsignaturaId = id_asig;
                    context.Matriculas.Add(m);
                    context.SaveChanges();  
                }
                return true;


            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateStudent(int id, string dni, string nombre, string direccion, int edad, string email)
        {
            try
            {
                var upStudent = GetByIdStudent(id);

                if (upStudent == null)
                {
                    return false;
                }
                else
                {
                    upStudent.Dni = dni;
                    upStudent.Nombre = nombre;
                    upStudent.Direccion = direccion;
                    upStudent.Edad = edad;
                    upStudent.Email = email;

                    context.SaveChanges();
                }
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteStudent(int id)
        {
            try
            {
               var student = context.Alumnos.Where(a => a.Id == id).FirstOrDefault();
                if (student != null)
                {
                    var tuition = context.Matriculas.Where(m => m.AlumnoId == id);
                    foreach (Matricula m in tuition)
                    {
                        var qualification = context.Calificacions.Where(c => c.MatriculaId == m.Id);
                        context.Calificacions.RemoveRange(qualification);
                    }
                    context.Matriculas.RemoveRange(tuition);
                    context.Alumnos.RemoveRange(student);
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
