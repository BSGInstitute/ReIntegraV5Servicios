using BSI.Integra.Aplicacion.Operaciones.Service.Implementacion;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: FormularioProgresivoSeccionPortalController
    /// Autor: Jorge Gamero.
    /// Fecha: 27/02/2025
    /// <summary>
    /// Gestión de Secciones de Portal para Formulario Progresivo
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class FormularioProgresivoSeccionPortalController : Controller
    {
        private IUnitOfWork unitOfWork;
        public FormularioProgresivoSeccionPortalController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// Tipo Función: GET
        /// Autor: Jorge Gamero
        /// Fecha: 27/02/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerRegistros()
        {
            try
            {
                var FormularioProgresivoSeccionPortalService = new FormularioProgresivoSeccionPortalService(unitOfWork);
                var resultado = FormularioProgresivoSeccionPortalService.ObtenerRegistros();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
