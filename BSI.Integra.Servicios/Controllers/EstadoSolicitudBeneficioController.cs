using BSI.Integra.Aplicacion.Operaciones.Service.Implementacion;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: EstadoSolicitudBeneficioController
    /// Autor: Jorge Gamero.
    /// Fecha: 02/09/2024
    /// <summary>
    /// Gestión de Estado Solicitud Beneficio
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class EstadoSolicitudBeneficioController : Controller
    {
        private IUnitOfWork unitOfWork;
        public EstadoSolicitudBeneficioController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// Tipo Función: GET
        /// Autor: Jorge Gamero
        /// Fecha: 02/09/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Id y Nombre de todos los registros de la tabla para combos
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerCombo()
        {
            try
            {
                var estadoSolicitudBeneficioService = new EstadoSolicitudBeneficioService(unitOfWork);
                var resultado = estadoSolicitudBeneficioService.ObtenerCombo();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
