using AccesoDatos.Models;
using AccesoDatos.Operations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class QualificationController : ControllerBase
    {
        private QualificationDAO qualificationDAO = new QualificationDAO();

        [HttpGet("IdQualification")]
        public List<Calificacion> GetByIdQualification(int id)
        {
            return qualificationDAO.GetByIdQualification(id);
        }

        [HttpPost("InsertQualification")]
        public bool GetInsertQualification([FromBody] Calificacion qua)
        {
            return qualificationDAO.InsertQualification(qua);
        }

        [HttpDelete("DeleteQualification")]
        public bool GetDeleteQualification(int id)
        {
            return qualificationDAO.DeleteQualification(id);
        }
    }
}
