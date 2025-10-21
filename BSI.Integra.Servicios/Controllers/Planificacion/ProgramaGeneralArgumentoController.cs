using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers.Planificacion
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ProgramaGeneralArgumentoController : Controller
    {
        private IUnitOfWork _unitOfWork;
        //private IAgendaInformacionActividadService _servicioPrincipal;
        public ProgramaGeneralArgumentoController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            //_servicioPrincipal = new AgendaInformacionActividadService(unitOfWork);
        }
        [HttpGet("ObtenerProgramaGeneralArgumentos")]
        public IActionResult ObtenerProgramaGeneralArgumentos()
        {
            try
            {
                return Ok("get obtener progrmas generales argumentos");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("ObtenerProgramaGeneralArgumentoById")]
        public IActionResult ObtenerProgramaGeneralArgumentoById([FromBody] Dictionary<string, string> filtro)
        {
            try
            {
                return Ok("post obtener programa general argumento by id");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
