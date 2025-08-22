using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Repositorio.Repository.Implementation.Planificacion;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers.Planificacion
{
    /// Controlador: CriterioEvaluacionController
    /// Autor Creacion: Gilmer Qm.
    /// Fecha: 25/05/2023
    /// <summary>
    /// Gestión de Criterio evaluacion
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class CriterioEvaluacionController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;
        private ICriterioEvaluacionService _criterioEvaluacionService;


        public CriterioEvaluacionController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _criterioEvaluacionService = new CriterioEvaluacionService(_unitOfWork);
        }
        [Route("[action]")]
        [Authorize]
        [HttpPost]
        public IActionResult Insertar([FromBody] CriterioEvaluacionDTO criterioEvaluacionDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var resultado = _criterioEvaluacionService.InsertarCriterio(criterioEvaluacionDTO, registroClaimToken.UserName);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("[action]")]
        [Authorize]
        [HttpPut]
        public IActionResult Actualizar([FromBody] CriterioEvaluacionDTO criterioEvaluacionDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var resultado = _criterioEvaluacionService.ActualizarCriterio(criterioEvaluacionDTO, registroClaimToken.UserName);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Autor: Gilmer Quispe.
        /// Fecha: 01/06/2023
        /// Version: 1.0
        /// <summary>
        /// Realiza una eliminacion logica a la tabla CriterioEvaluacion y sus tablas detalles
        /// </summary>
        /// <returns> bool </returns>  
        [Route("[action]/{id}")]
        [HttpDelete]
        [Authorize]
        public ActionResult Eliminar(int id)
        {
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var resultado = _criterioEvaluacionService.EliminarCriterio(id, registroClaimToken.UserName);
                return Ok(resultado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: GET 
        /// Autor: Gilmer Quispe.
        /// Fecha: 31/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la lista de CriterioEvaluacion y su detalles  
        /// </summary> 
        /// <returns> List<ComboDTO> </returns>
        [Route("[action]")]
        [HttpGet]
        public IActionResult Obtener()
        {
            var resultado = new { criterioEvaluacion = _criterioEvaluacionService.ObtenerCriteriosEvaluacion() };
            return Ok(resultado);
        }

        /// Tipo Función: GET 
        /// Autor: Gilmer Quispe.
        /// Fecha: 31/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la lista de CriterioEvaluacion y su detalles  
        /// </summary> 
        /// <returns> List<ComboDTO> </returns>
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> ObtenerCombosModulo()
        {
            var resultado = await _criterioEvaluacionService.ObtenerCombosModulo();
            return Ok(resultado);
        }
        /// Tipo Función: GET 
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 16/07/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el combo de criterios de evaluacion
        /// </summary> 
        /// <returns> List<ComboDTO> </returns>
        [Route("[action]")]
        [HttpGet]
        public IActionResult ObtenerCombo()
        {
            var resultado = _criterioEvaluacionService.ObtenerCombo();
            return Ok(resultado);
        }
        /// Tipo Función: GET 
        /// Autor: Gilmer Quispe.
        /// Fecha: 31/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la lista de CriterioEvaluacion y su detalles  
        /// </summary> 
        /// <returns> List<ComboDTO> </returns>
        [Route("[action]/{id}")]
        [HttpGet]
        public IActionResult ObtenerPorId(int id)
        {
            var resultado = _criterioEvaluacionService.ObtenerCriterioEvaluacionPorId(id);
            return Ok(resultado);
        }

        [Route("[action]/{tipoprograma}/{modalidadprograma}")]
        [HttpGet]
        public IActionResult ObtenerPGCriteriosEvaluacion(int tipoprograma, int modalidadprograma)
        {
            var lista = _criterioEvaluacionService.ObtenerCriterio(tipoprograma, modalidadprograma);
            return Ok(lista);
        }
    }
}
