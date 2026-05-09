using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.Comercial.Service.Implementacion;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers.Comercial
{
    /// Controlador: HistorialOportunidadController
    /// Autor: Jose Vega
    /// Fecha: 22/04/2026
    /// Version: 1.0
    /// <summary>
    /// Endpoint de historial de oportunidades de un alumno con contadores de
    /// interacciones por canal (llamadas, whatsapp, correo, portal_web).
    /// </summary>
    [ApiController]
    [Route("api/Comercial/[controller]")]
    [EnableCors("CorsVista")]
    public class HistorialOportunidadController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public HistorialOportunidadController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("ObtenerHistorialPorIdAlumno/{idAlumno:int}")]
        public IActionResult ObtenerHistorialPorIdAlumno(int idAlumno)
        {
            try
            {
                IHistorialOportunidadService historialOportunidadService = new HistorialOportunidadService(_unitOfWork);
                var resultado = historialOportunidadService.ObtenerHistorialPorIdAlumno(idAlumno);
                return Ok(resultado);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
