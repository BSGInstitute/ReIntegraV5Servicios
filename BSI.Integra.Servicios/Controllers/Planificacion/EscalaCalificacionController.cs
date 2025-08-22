using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface.Planificacion;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers.Planificacion
{
    /// Controlador: EscalaCalificacionController
    /// Autor: Gretel Canasa
    /// Fecha: 11/05/2023
    /// <summary>
    /// Gestion de Materiales de Accion
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    //[ServiceFilter(typeof(JwtActionFilter))]
    public class EscalaCalificacionController : Controller
    {

        private IUnitOfWork _unitOfWork;
        private IEscalaCalificacionService _escalaCalificacionService;
        public EscalaCalificacionController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _escalaCalificacionService = new EscalaCalificacionService(_unitOfWork);
        }
        /// Tipo Función: GET
        /// Autor: Gretel Canasa
        /// Fecha: 11/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los materiales de accion
        /// </summary>
        /// <returns> Lista EscalaCalificacionDTO </returns>
        //[AllowAnonymous]
        [HttpGet("[Action]")]
        public IActionResult Obtener()
        {
            var resultado = _escalaCalificacionService.Obtener();
            return Ok(resultado);
        }
        /// Tipo Función: GET
        /// Autor: Gretel Canasa
        /// Fecha: 11/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Material de accion por Id
        /// </summary>
        /// <param name="id">Id del Material de accion</param>
        /// <returns> EscalaCalificacionDTO </returns>
        [HttpGet("[Action]/{id}")]
        public IActionResult Obtener(int id)
        {
            try
            {
                var resultado = _escalaCalificacionService.ObtenerPorId(id);
                return Ok(resultado);
            }
            catch
            {
                throw;
            }
        }
        /// Tipo Función: POST
        /// Autor: Gretel Canasa
        /// Fecha: 11/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Registra un nuevo Material de Accion
        /// </summary>
        /// <param name="dto">Material de Accion</param>
        /// <returns>EscalaCalificacionDTO</returns>
        /// 
        [Authorize]
        [HttpPost("[Action]")]
        public IActionResult Insertar([FromBody] EscalaCalificacionDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);

                var resultado = _escalaCalificacionService.Insertar(dto, registroClaimToken.UserName);


                return Ok(resultado);
            }
            catch
            {
                throw;
            }
        }
        /// Tipo Función: POST
        /// Autor: Gretel Canasa
        /// Fecha: 11/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Registra una lista de Materiales de Accion
        /// </summary>
        /// <param name="dtos">Lista de Materiales de Accion</param>
        /// <returns>Lista EscalaCalificacionDTO</returns>
        [Authorize]
        [HttpPost("[Action]")]
        public IActionResult InsertarLista([FromBody] List<EscalaCalificacionDTO> dtos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var registroClaimToken = HttpContext.Items["RegistroClaimToken"] as RegistroClaimTokenDTO;
                var resultado = _escalaCalificacionService.InsertarLista(dtos, registroClaimToken.UserName);
                return Ok(resultado);
            }
            catch
            {
                throw;
            }
        }
        /// Tipo Función: PUT
        /// Autor: Gretel Canasa
        /// Fecha: 11/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Modifica un Material de accion
        /// </summary>
        /// <param name="dto">Material de Accion</param>
        /// <returns> EscalaCalificacionDTO </returns>
        [Authorize]
        [HttpPut("[Action]")]
        public IActionResult Actualizar([FromBody] EscalaCalificacionDTO dto)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);

                var resultado = _escalaCalificacionService.Actualizar(dto, registroClaimToken.UserName);


                return Ok(resultado);
            }
            catch
            {
                throw;
            }
        }
        /// Tipo Función: PUT
        /// Autor: Gretel Canasa
        /// Fecha: 11/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Modifica una lista de Materiales de accion
        /// </summary>
        /// <param name="dto">Materiales de Accion</param>
        /// <returns> Lista EscalaCalificacionDTO </returns>
        [Authorize]
        [HttpPut("[Action]")]
        public IActionResult ActualizarLista([FromBody] List<EscalaCalificacionDTO> dtos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var registroClaimToken = HttpContext.Items["RegistroClaimToken"] as RegistroClaimTokenDTO;
                var resultado = _escalaCalificacionService.ActualizarLista(dtos, registroClaimToken.UserName);
                return Ok(resultado);
            }
            catch
            {
                throw;
            }
        }
        /// Tipo Función: DELETE
        /// Autor: Gretel Canasa
        /// Fecha: 11/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Elimina un registro de Material de Accion
        /// </summary>
        /// <param name="id">Id Material de Accion</param>
        /// <returns> true </returns>
        [Authorize]
        [HttpDelete("[Action]/{id}")]
        public IActionResult Eliminar(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var resultado = _escalaCalificacionService.Eliminar(id, registroClaimToken.UserName);
                return Ok(resultado);
            }
            catch
            {
                throw;
            }
        }
        /// Tipo Función: DELETE
        /// Autor: Gretel Canasa
        /// Fecha: 11/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Elimina registros de materiales de accion por ids
        /// </summary>
        /// <param name="ids">Lista de ids</param>
        /// <returns> true </returns>
        [Authorize]
        [HttpDelete("[Action]")]
        public IActionResult EliminarLista([FromBody] List<int> ids)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var registroClaimToken = HttpContext.Items["RegistroClaimToken"] as RegistroClaimTokenDTO;
                var resultado = _escalaCalificacionService.EliminarLista(ids, registroClaimToken.UserName);
                return Ok(resultado);
            }
            catch
            {
                throw;
            }
        }

    }
}
