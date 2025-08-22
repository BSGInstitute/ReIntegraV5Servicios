global using Microsoft.AspNetCore.Authorization;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Mvc;
using BSI.Integra.Aplicacion.GestionPersonas.SCode.Service.Interface;
using BSI.Integra.Aplicacion.GestionPersonas.SCode.Service.Implementacion;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Servicios.Configurations;
using Microsoft.AspNetCore.Cors;

namespace BSI.Integra.Servicios.Controllers.GestionPersonas
{
    /// Controlador: GradoEstudioController
    /// Autor: Juan D. Huanaco Quispe
    /// Fecha: 04/04/2024
    /// <summary>
    /// Gestión de Grado Estudio para el Modulo (M) Estado Formacion
    /// Interactua con la tabla 'gp.T_GradoEstudio'
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    [Authorize]
    [JwtExpirationValidation]
    public class GradoEstudioController : ControllerBase
    {

        private ITokenManager _tokenManager;
        private IGradoEstudioService _gradoEstudioService;

        public GradoEstudioController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _gradoEstudioService = new GradoEstudioService(unitOfWork);
            _tokenManager = tokenManager;
        }

        /// Método HTTP: GET
        /// Autor:Juan D. Huanaco Quispe
        /// Fecha: 04/04/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los grados de estudio.
        /// </summary>
        /// <returns>Retorna 200 con la lista de objetos o 400 con un mensaje de error</returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult<IEnumerable<GradoEstudioDTO>> Obtener()
        {
            return Ok(_gradoEstudioService.Obtener());
        }

        /// Método HTTP: POST
        /// Autor:Juan D. Huanaco Quispe
        /// Fecha: 04/04/2024
        /// Versión: 1.0
        /// <summary>
        /// Realiza una inserción básica a la tabla.
        /// </summary>
        /// <param name="dto">Entidad GradoEstudioDTO a insertar</param>
        /// <returns>Retorna 200 y el objeto ingresado o 400 y un mensaje de error </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult<GradoEstudioDTO> Insertar([FromBody] GradoEstudioDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            return Ok(_gradoEstudioService.Insertar(dto, _tokenManager.UserName));
        }

        /// Método HTTP: PUT
        /// Autor:Juan D. Huanaco Quispe
        /// Fecha: 04/04/2024
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualización básica a la tabla
        /// </summary>
        /// <param name="dto">Entidad GradoEstudioDTO a actualizar</param>
        /// <returns>Retorna 200 con el objeto actualizado o 400 con un mensaje de error</returns>
        [Route("[action]")]
        [HttpPut]
        public ActionResult<GradoEstudioDTO> Actualizar([FromBody] GradoEstudioDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            return Ok(_gradoEstudioService.Actualizar(dto, _tokenManager.UserName));
        }

        /// Método HTTP: DELETE
        /// Autor:Juan D. Huanaco Quispe
        /// Fecha: 04/04/2024
        /// Versión: 1.0
        /// <summary>
        /// Realiza una eliminación básica a la tabla
        /// </summary>
        /// <param name="idGradoEstudio">Id del grado de estudio a eliminar</param>
        /// <returns>Retorna 200 con el objeto actualizado o 400 con un mensaje de error</returns>
        [Route("[action]/{idGradoEstudio}")]
        [HttpDelete]
        public ActionResult<bool> Eliminar(int idGradoEstudio)
        {
            return Ok(_gradoEstudioService.Eliminar(idGradoEstudio, _tokenManager.UserName));
        }

    }
}
