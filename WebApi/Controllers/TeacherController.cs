using AccesoDatos.Models;
using AccesoDatos.Operations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private TeacherDAO teacherDAO = new TeacherDAO();

        [HttpPost("authentication")]
        public string Login([FromBody] Profesor teacher )
        {
            var teach = teacherDAO.Login(teacher.Usuario, teacher.Pass);

            if (teach != null)
            {
                return teach.Usuario;
            }
            else
            {
                return null;
            }
        }

        [HttpGet("TeacherAndStudent")]
        public List<TeacherStudent> GetStudentJoinTeache(string usuario)
        {
           return teacherDAO.teacherStudents(usuario);
        }

        [HttpGet("IdStudent")]
        public Alumno GetByIdStudent(int id)
        {
            return teacherDAO.GetByIdStudent(id);
        }

        [HttpPost("InsertStudent")]
        public bool GetInsertStudent([FromBody] Alumno student, int id_asig)
        {
            return teacherDAO.InsertStudentTuition(student.Dni, student.Nombre, student.Direccion, student.Edad, student.Email, id_asig);
        }

        [HttpPut("UpdateStudent")]
        public bool GetUpdateStudent([FromBody] Alumno upStudent)
        {
            return teacherDAO.UpdateStudent(upStudent.Id, upStudent.Dni, upStudent.Nombre, upStudent.Direccion, upStudent.Edad, upStudent.Email);
        }

        [HttpDelete("DeleteStudent")]
        public bool DeleteByIdStudent(int id)
        {
            return teacherDAO.DeleteStudent(id);
        }
    }
}
