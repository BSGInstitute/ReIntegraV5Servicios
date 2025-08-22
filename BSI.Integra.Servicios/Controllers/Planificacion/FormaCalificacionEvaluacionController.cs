using BSI.Integra.Aplicacion.Marketing.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers.Planificacion
{
    /// Controlador: FormaCalificacionEvaluacionController
    /// Autor Creación: Gilmer Qm.
    /// Fecha: 31/05/2023
    /// <summary>
    /// Gestión de FormaCalificacionEvaluacion
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    //[AllowAnonymous]
    public class FormaCalificacionEvaluacionController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private IFormaCalificacionEvaluacionService _formaCalificacionEvaluacionService;

        public FormaCalificacionEvaluacionController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _formaCalificacionEvaluacionService = new FormaCalificacionEvaluacionService(_unitOfWork);
        }
        /// Tipo Función: GET
        /// Autor: Gilmer  Quispe.
        /// Fecha: 31/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene combo
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        [HttpGet("[Action]")]
        public IActionResult ObtenerCombo()
        {
            try
            {
                var resultado = _formaCalificacionEvaluacionService.ObtenerCombo();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
