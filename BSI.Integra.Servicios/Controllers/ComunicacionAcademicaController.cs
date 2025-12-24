using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ComunicacionAcademicaController : ControllerBase
    {
        private IComunicacionAcademicaService _ComunicacionAcademicaService;
        public ComunicacionAcademicaController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _ComunicacionAcademicaService = new ComunicacionAcademicaService(unitOfWork);
        }

        [HttpGet("[action]/{IdAlumno}")]
        public IActionResult ObtenerPreferenciaComunicacionAlumno(int IdAlumno)
        {
            try
            {
                var resultado = _ComunicacionAcademicaService.ObtenerPreferenciaComunicacionAlumno(IdAlumno);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpPost("[action]")]
        public IActionResult ActualizarPreferenciaComunicacionAlumno([FromBody] PreferenciaConfiguracionDTO dto)
        {
            try
            {
                var resultado = _ComunicacionAcademicaService.ActualizarPreferenciaComunicacionAlumno(dto);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("[action]")]
        public IActionResult ObtenerOpcionesPreferenciaComunicacion()
        {
            try
            {
                var resultado = _ComunicacionAcademicaService.ObtenerOpcionesPreferenciaComunicacion();
                return Ok(resultado);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
