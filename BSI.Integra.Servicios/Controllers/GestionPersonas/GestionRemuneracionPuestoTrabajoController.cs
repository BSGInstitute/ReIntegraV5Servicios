using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Interface;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Configurations;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers.GestionPersonas
{
    /// Controlador: GestionRemuneracionPuestoTrabajoController
    /// Autor: Sergio Yepez P.
    /// Fecha: 17/12/2024
    /// <summary>
    /// Compensación por Puesto
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    [Authorize]
    [JwtExpirationValidation]
    public class GestionRemuneracionPuestoTrabajoController : Controller
    {
        private IUnitOfWork unitOfWork;
        private ITokenManager _tokenManager;
        private IGestionRemuneracionPuestoTrabajoService _gestionRemuneracionPuestoTrabajoService;
        public GestionRemuneracionPuestoTrabajoController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _gestionRemuneracionPuestoTrabajoService = new GestionRemuneracionPuestoTrabajoService(unitOfWork);
            _tokenManager = tokenManager;
        }

        [HttpGet("[action]")]
        public IActionResult Obtener()
        {
            try 
            { 
                var resultado = _gestionRemuneracionPuestoTrabajoService.Obtener();
                return Ok(resultado);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        /// Tipo Funcion: Get
        /// Autor: Sergio Yepez Pillco.
        /// Fecha: 17/12/2024
        /// <summary>
        /// Función que trae data para llenar los combos Area, Puesto Trabajo, Pais y Categoria
        /// </summary>
        /// <returns>Retorma una lista</returns>
        [Route("[action]")]
        [HttpGet]
        public IActionResult ObtenerCombosModulo()
        {
            try
            {
                var reporteCombo = _gestionRemuneracionPuestoTrabajoService.ObtenerCombosModulo();
                return Ok(reporteCombo);
            }
            catch
            {
                throw;
            }
        }

        /// Tipo Función: POST
        /// Autor: Sergio Yepez Pillco.
        /// Fecha: 17/12/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene detalle de los puestos de trabajo
        /// </summary>
        /// <param name="IdRemuneracionPuestoTrabajo">DTO enviado desde la interfaz<</param>
        /// <returns>Response 200 o 400, dependiendo del flujo</returns>
        [HttpPost]
        [Route("[action]")]
        public ActionResult ObtenerRemuneracionPuestoTrabajoVariablePorPuesto([FromBody] int IdRemuneracionPuestoTrabajo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                var listaRemuneracionPuestoTrabajoVariable = _gestionRemuneracionPuestoTrabajoService.ObtenerPuestoTrabajoRemuneracionVariableRegistrado(IdRemuneracionPuestoTrabajo);
                return Ok(listaRemuneracionPuestoTrabajoVariable);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Sergio Yepez P.
        /// Fecha: 17/12/2024
        /// Versión: 1.0    
        /// <summary>
        /// Realiza una insercion de Remuneracion por Puesto de Trabajo y sus Detalles
        /// </summary>
        /// <param name="dto">Entidad CriterioEvaluacionProcesoDTO</param>
        /// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error </returns>
        [Route("[action]")]
        [Authorize]
        [HttpPost]
        public IActionResult Insertar([FromBody] GestionRemuneracionPuestoTrabajoDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var resultado = _gestionRemuneracionPuestoTrabajoService.Insertar(dto, _tokenManager.UserName);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: PUT
        /// Autor: Sergio Yepez P.
        /// Fecha: 17/12/2024
        /// Versión: 1.0    
        /// <summary>
        /// Realiza una actualizacion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a modificar</param>
        /// <returns>Retorna 200 y objeto actualizado o 400 y mensaje de error</returns>
        [Route("[action]")]
        [HttpPut]
        public IActionResult Actualizar([FromBody] GestionRemuneracionPuestoTrabajoDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var resultado = _gestionRemuneracionPuestoTrabajoService.Actualizar(dto, registroClaimToken.UserName);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: DELETE
        /// Autor: Sergio Yepez P.
        /// Fecha: 17/12/2024
        /// Versión: 1.0    
        /// <summary>
        /// Realiza una Eliminacion lógica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a eliminar</param>
        /// <returns>Retorna 200 y objeto eliminado o 400 y mensaje de error</returns>
        [HttpDelete("[action]/{id}")]
        public IActionResult Eliminar(int id)
        {
            var respuesta = _gestionRemuneracionPuestoTrabajoService.Eliminar(id, _tokenManager.UserName);
            return Ok(respuesta);
        }

        /// <summary>
        /// Procesa el formulario nuevo del portal
        /// </summary>
        /// <returns>Response 200 con el id de la asignacion automatica temporal</returns>
        [Authorize]
        [JwtExpirationValidation]
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ProcesarArchivo(IFormFile file)
        {
            try
            {
                var resultado = _gestionRemuneracionPuestoTrabajoService.ProcesarPuestoTrabajoRemuneracionExcel(file, _tokenManager.UserName);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}