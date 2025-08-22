using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.Repository.Implementation.Planificacion;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Configurations;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: ProgramaGeneralPrerequisitoController
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 26/07/2022
    /// <summary>
    /// Gestión de ProgramaGeneralPrerequisito
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ProgramaGeneralPrerequisitoController : ControllerBase
    {
        private IProgramaGeneralPrerequisitoService _programaGeneralPrerequisitoService;
        private ITokenManager _tokenManager;
        public ProgramaGeneralPrerequisitoController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _programaGeneralPrerequisitoService = new ProgramaGeneralPrerequisitoService(unitOfWork);
            _tokenManager = tokenManager;
        }
        /// Tipo Función: DELETE
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 27/07/2023
        /// Versión: 1.0
        /// <summary>
        /// </summary>
        /// <param name="idPrerequisito"></param>
        /// <param name="listaModalidades"></param>
        /// <returns>Retorna 200 y estado o 400 y mensaje de error </returns>
        [Authorize]
        [JwtExpirationValidation]
        [Route("[action]/{idPrerequisito}")]
        [HttpDelete]
        public ActionResult<bool> EliminarPreRequisitos(int idPrerequisito)
        {
            var resultado = _programaGeneralPrerequisitoService.EliminarPreRequisitos(idPrerequisito, _tokenManager.UserName);
            return Ok(resultado);
        }
        /// Tipo Función: DELETE
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 27/07/2023
        /// Versión: 1.0
        /// <summary>
        /// </summary>
        /// <param name="jsonDTO"></param>
        /// <returns>Retorna 200 y estado o 400 y mensaje de error </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult GuardarPreRequisitos([FromBody] CompuestoPreRequisitoModalidadAlternaDTO jsonDTO)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var resultado = _programaGeneralPrerequisitoService.GuardarPreRequisitos(jsonDTO, _tokenManager.UserName);
            return Ok(resultado);
        }
    }
}
