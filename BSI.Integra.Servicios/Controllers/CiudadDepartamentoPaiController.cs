using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Operaciones.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: CiudadDepartamentoPaiController
    /// Autor: Jorge Gamero.
    /// Fecha: 20/09/2024
    /// <summary>
    /// Gestión de CiudadDepartamentoPai
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class CiudadDepartamentoPaiController : Controller
    {
        private IUnitOfWork unitOfWork;
        public CiudadDepartamentoPaiController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        
        /// Tipo Función: GET
        /// Autor: Jorge Gamero
        /// Fecha: 20/09/2024
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
                var ciudadDepartamentoPaiService = new CiudadDepartamentoPaiService(unitOfWork);
                var resultado = ciudadDepartamentoPaiService.ObtenerCombo();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Jorge Gamero
        /// Fecha: 20/09/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Id y Nombre de todos los registros de la tabla para combos
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        [Route("[action]/{idDepartamentoPais}")]
        [HttpGet]
        public ActionResult ObtenerPorId(int idDepartamentoPais)
        {
            try
            {
                var ciudadDepartamentoPaiService = new CiudadDepartamentoPaiService(unitOfWork);
                var resultado = ciudadDepartamentoPaiService.ObtenerPorId(idDepartamentoPais);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Jorge Gamero
        /// Fecha: 25/09/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene código de ciudad por Id
        /// </summary>
        /// <returns>  </returns>
        [Route("[action]/{id}")]
        [HttpGet]
        public ActionResult ObtenerCodigoPorId(int id)
        {
            try
            {
                var ciudadDepartamentoPaiService = new CiudadDepartamentoPaiService(unitOfWork);
                var resultado = ciudadDepartamentoPaiService.ObtenerCodigoPorId(id);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
