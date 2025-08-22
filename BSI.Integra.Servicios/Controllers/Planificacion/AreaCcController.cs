using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers.Planificacion
{
    /// Controlador: AreaCentroCostoController
    /// Autor Modificacion: Klebert Layme.
    /// Fecha: 26/04/2023
    /// <summary>
    /// Gestión de AreaCentroCosto
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class AreaCcController : Controller
    {
        private IUnitOfWork unitOfWork;
        private IAreaCcService _areaCcService;

        public AreaCcController(IUnitOfWork unitOfWork, IAreaCcService areaCcService)
        {
            this.unitOfWork = unitOfWork;
            _areaCcService = areaCcService;
        }
        /// Tipo Función: GET
        /// Autor: Klebert Layme
        /// Fecha: 26/04/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los Areas centro de costo
        /// </summary>
        /// <returns> ListaAreaCC </returns>
        //[AllowAnonymous]
        [HttpGet("[Action]")]
        public IActionResult Obtener()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new AreaCcService(unitOfWork);
                return Ok(servicio.Obtener());
            }
            catch
            {
                throw;
            }
        }
        /// Tipo Función: GET
        /// Autor: Klebert Layme
        /// Fecha: 24/04/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Area centro costo de accion por Id
        /// </summary>
        /// <param name="id">Id del Material de accion</param>
        /// <returns> MaterialAccionDTO </returns>
        [HttpGet("[Action]/{id}")]
        public IActionResult Obtener(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new AreaCcService(unitOfWork);
                return Ok(servicio.ObtenerPorId(id));
            }
            catch
            {
                throw;
            }
        }
        /// Tipo Función: POST
        /// Autor Modificacion: Klebert Layme
        /// Fecha: 24/04/2023
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="dto">Entidad a insertar</param>
        /// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error </returns>
        [HttpPost("[Action]")]
        [Authorize]
        public IActionResult Insertar([FromBody] AreaCcDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var resultado = _areaCcService.Insertar(dto, registroClaimToken.UserName);
                return Ok(resultado);
            }
            catch
            {
                throw;
            }
        }

        /// Tipo Función: PUT
        /// Autor Modificacion: Klebert Layme
        /// Fecha: 15/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla
        /// </summary>
        /// <param name="dto">Entidad a modificar</param>
        /// <returns>Retorna 200 y objeto actualizado o 400 y mensaje de error</returns>
        [HttpPut("[Action]")]
        [Authorize]
        public IActionResult Actualizar([FromBody] AreaCcDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var resultado = _areaCcService.Actualizar(dto, registroClaimToken.UserName);
                return Ok(resultado);
            }
            catch
            {
                throw;
            }
        }
        /// Tipo Función: DELETE
        /// Autor Modificacion: Klebert Layme
        /// Fecha: 15/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una eliminacion logica basica a la tabla
        /// </summary>
        /// <param name="id">Id de la entidad a eliminar</param>
        /// <param name="usuario">Nombre del usuario que realiza la eliminacion</param>
        /// <returns>Retorna 200 y bandera de eliminacion realizada o 400 y mensaje de error</returns>
        [HttpDelete("Eliminar/{id}")]
        [Authorize]
        public IActionResult Eliminar(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var resultado = _areaCcService.Eliminar(id, registroClaimToken.UserName);
                return Ok(resultado);
            }
            catch
            {
                throw;
            }
        }

    }

}
