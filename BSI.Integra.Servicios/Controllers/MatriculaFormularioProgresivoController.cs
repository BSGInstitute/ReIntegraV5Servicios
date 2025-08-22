using BSI.Integra.Aplicacion.Operaciones.Service.Implementacion;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: MatriculaFormularioProgresivoController
    /// Autor: Max Mantilla.
    /// Fecha: 26/03/2025
    /// <summary>
    /// Gestión de Formulario Progresivo
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class MatriculaFormularioProgresivoController: Controller
    {
        private IUnitOfWork unitOfWork;
        public MatriculaFormularioProgresivoController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// Tipo Función: GET
        /// Autor: Max Mantilla
        /// Fecha: 07/11/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene información del cupón de descuento profiling a aplicar para generar su cronograma en la matrícula
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        [Route("[action]/{emailUsuario}/")]
        [HttpGet]
        public ActionResult ObtenerDescuentoProfiling(string emailUsuario)
        {
            try
            {
                var MatriculaFormularioProgresivoService = new MatriculaFormularioProgresivoService(unitOfWork);
                var resultado = MatriculaFormularioProgresivoService.ObtenerDescuentoProfiling(emailUsuario);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
