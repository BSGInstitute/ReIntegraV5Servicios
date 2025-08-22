using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: VersionProgramaController
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 09/08/2022
    /// <summary>
    /// Gestión de VersionPrograma
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class VersionProgramaController : Controller
    {
        private IVersionProgramaService _versionProgramaService;
        public VersionProgramaController(IUnitOfWork unitOfWork)
        {
            _versionProgramaService = new VersionProgramaService(unitOfWork);
        }
        /// Tipo Función: POST
        /// Autor: Gretel Canasa
        /// Fecha: 18/04/2023
        /// Versión: 1.0
        /// <summary>
        /// Registra un nuevo Material de Accion
        /// </summary>
        /// <param name="dto">Material de Accion</param>
        /// <returns>MaterialAccionDTO</returns>
        /// 
        [Authorize]
        [HttpPost("[Action]")]
        public IActionResult Insertar([FromBody] VersionProgramaDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var resultado = _versionProgramaService.Insertar(dto, registroClaimToken.UserName);
                return Ok(resultado);
            }
            catch
            {
                throw;
            }
        }
        /// Tipo Función: PUT
        /// Autor: Gretel Canasa
        /// Fecha: 25/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a modificar</param>
        /// <returns>Retorna 200 y objeto actualizado o 400 y mensaje de error</returns>
        [HttpPut("Actualizar")]
        public IActionResult Actualizar([FromBody] VersionProgramaDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var resultado = _versionProgramaService.Actualizar(dto, registroClaimToken.UserName);
                return Ok(resultado);
            }
            catch
            {
                throw;
            }
        }
        /// Tipo Función: DELETE
        /// Autor: Gretel Canasa.
        /// Fecha: 09/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Realiza una eliminacion logica basica a la tabla
        /// </summary>
        /// <param name="id">Id de la entidad a eliminar</param>
        /// <param name="usuario">Nombre del usuario que realiza la eliminacion</param>
        /// <returns>Retorna 200 y bandera de eliminacion realizada o 400 y mensaje de error</returns>
        [HttpDelete("[Action]/{id}")]
        public IActionResult Eliminar(int id)
        {
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var resultado = _versionProgramaService.Eliminar(id, registroClaimToken.UserName);
                return Ok(resultado);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Tipo Función: GET
        /// Autor: Gretel Canasa
        /// Fecha: 09/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_VersionPrograma
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [HttpGet("[Action]")]
        public IActionResult ObtenerVersionPrograma()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var resultado = _versionProgramaService.ObtenerVersionPrograma();
                return Ok(resultado);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }

}
