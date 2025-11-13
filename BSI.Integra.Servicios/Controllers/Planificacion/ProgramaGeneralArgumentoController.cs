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
        [HttpGet("[Action]/{idProgramaGeneralArgumento}")]
        public IActionResult ObtenerProgramaGeneralArgumentoById(int idProgramaGeneralArgumento)
        {
            try
            {
                var result = _programaGeneralArgumentoService.ObtenerInformacionProgramaGeneralArgumento(idProgramaGeneralArgumento);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("[action]/{idPGeneral}")]
        public IActionResult ObtenerProgramaGeneralArgumentoTodo(int idPGeneral)
        {
            try
            {
                var result = _programaGeneralArgumentoService.ObtenerInformacionProgramaGeneralArgumentoTodo(idPGeneral);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Jose Vega
        /// Fecha: 30-10-2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los argumentos de motivación
        /// </summary>
        [HttpGet("[action]/{idOportunidad}")]
        public async Task<IActionResult> ObtenerArgumentoMotivacion(int idOportunidad)
        {
            try
            {
                var motivaciones = await _programaGeneralArgumentoService.ObtenerArgumentoMotivacion(idOportunidad);
                if (motivaciones == null || motivaciones.Count == 0)
                {
                    motivaciones = new List<MotivacionSalidaDTO> { null };
                }
                var response = new ObtenerArgumentoMotivacionResponseDTO
                {
                    Motivaciones = motivaciones,
                    Error = null
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                var responseError = new ObtenerArgumentoMotivacionResponseDTO
                {
                    Motivaciones = new List<MotivacionSalidaDTO>(),
                    Error = ex.Message
                };
                return BadRequest(responseError);
            }
        }


        /// Tipo Función: GET
        /// Autor: Jose Vega
        /// Fecha: 30-10-2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los problemas de cliente
        /// </summary>
        [HttpGet("[action]/{idPGeneral}")]
        public async Task<IActionResult> ObtenerProblemaCliente(int idPGeneral, int? idAlumno = null)
        {
            try
            {
                var data = await _programaGeneralArgumentoService.ObtenerProblemaCliente(idPGeneral, idAlumno);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("[action]/{idPGeneral}")]
        public IActionResult ObtenerProgramaGeneralArgumentoMotivacionByIdPGeneral(int idPGeneral)
        {
            try
            {
                var result = _programaGeneralArgumentoService.ObtenerArgumentoMotivacionByIdPGeneral(idPGeneral);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("InsertarArgumentoMotivacionSeleccion")]
        public IActionResult InsertarArgumentoMotivacionSeleccion([FromBody] ProgramaArgumentoMotivacionSeleccionDTO dto)
        {
            try
            {
                var resultado = _programaGeneralArgumentoService.InsertarArgumentoMotivacionSeleccion(dto, _tokenManager.UserName);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Insertar")]
        public IActionResult Insertar([FromBody] ProgramaGeneralArgumentoDTO dto)
        {
            try
            {
                var resultado = _programaGeneralArgumentoService.Insertar(dto, _tokenManager.UserName);
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
                var resultado = _programaGeneralArgumentoService.Actualizar(dto, _tokenManager.UserName);
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

        [HttpDelete("[action]/{IdProgramaGeneralArgumento}")]
        public IActionResult Eliminar(int IdProgramaGeneralArgumento)
        {
            var respuesta = _programaGeneralArgumentoService.Eliminar(IdProgramaGeneralArgumento, _tokenManager.UserName);
            return Ok(respuesta);
        }
    }
}
