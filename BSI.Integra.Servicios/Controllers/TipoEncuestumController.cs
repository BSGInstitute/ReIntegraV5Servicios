using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Operaciones.SCode.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{

    /// Controlador: TipoEncuestumController
    /// Autor: Jorge Gamero
    /// Fecha: 26/03/2025
    /// <summary>
    /// Gestión de Solicitud de Tipo de Encuesta
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class TipoEncuestumController : Controller
    {
        private IUnitOfWork unitOfWork;
        public TipoEncuestumController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// Tipo Función: GET
        /// Autor: Jorge Gamero
        /// Fecha: 26/03/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla T_TipoEncuesta
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerCombo()
        {
            try
            {
                var TipoEncuestumService = new TipoEncuestumService(unitOfWork);
                var resultado = TipoEncuestumService.ObtenerCombo();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Jorge Gamero
        /// Fecha: 26/03/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros de la vista V_TModalidadCurso_Filtro
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerComboTipoModalidad()
        {
            try
            {
                var TipoEncuestumService = new TipoEncuestumService(unitOfWork);
                var resultado = TipoEncuestumService.ObtenerComboTipoModalidad();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
