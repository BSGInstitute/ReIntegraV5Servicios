using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers.Planificacion
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ProgramaGeneralArgumentoController : Controller
    {
        private ITokenManager _tokenManager;
        private IProgramaGeneralArgumentoService _programaGeneralArgumentoService;
        public ProgramaGeneralArgumentoController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _programaGeneralArgumentoService = new ProgramaGeneralArgumentoService(unitOfWork);
            _tokenManager = tokenManager;
        }
        /// Tipo Función: GET
        /// Autor: Marco Jose Villanueva
        /// Fecha: 21-10-2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_programaGeneralArgumento
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [HttpGet("[action]")]
        public IActionResult Obtener()
        {
            var resultado = _programaGeneralArgumentoService.Obtener();
            return Ok(resultado);

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

        [HttpPost("Insertar")]
        public IActionResult Insertar([FromBody] ProgramaGeneralArgumentoInsertDTO dto)
        {
            try
            {
                var resultado = _programaGeneralArgumentoService.Insertar(dto, "ctumir-sos");
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Actualizar")]
        public IActionResult Actualizar([FromBody] ProgramaGeneralArgumentoDTO dto)
        {
            try
            {
                var resultado = _programaGeneralArgumentoService.Actualizar(dto, "ctumir-sos");
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("[action]/{IdPGeneral}")]
        public IActionResult ObtenerMotivaciones(int  IdPGeneral)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = _programaGeneralArgumentoService.ObtenerMotivaciones(IdPGeneral);
            return Ok(respuesta);
        }


    }
}
