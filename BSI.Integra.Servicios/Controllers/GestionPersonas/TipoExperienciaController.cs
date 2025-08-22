using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.GestionPersonas.SCode.Service.Implementacion;
using BSI.Integra.Aplicacion.GestionPersonas.SCode.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Configurations;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers.GestionPersonas
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    [Authorize]
    [JwtExpirationValidation]
    public class TipoExperienciaController : ControllerBase
    {
        private ITokenManager _tokenManager;
        private ITipoExperienciaService _tipoExperienciaService;

        public TipoExperienciaController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _tipoExperienciaService = new TipoExperienciaService(unitOfWork);
            _tokenManager = tokenManager;
        }

        /// Método HTTP: GET
        /// Autor:Juan D. Huanaco Quispe
        /// Fecha: 09/04/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los tipos de experiencia.
        /// </summary>
        /// <returns>Retorna 200 con la lista de objetos o 400 con un mensaje de error</returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult<IEnumerable<TipoExperienciaDTO>> Obtener()
        {
            return Ok(_tipoExperienciaService.Obtener());
        }

        /// Método HTTP: POST
        /// Autor:Juan D. Huanaco Quispe
        /// Fecha: 09/04/2024
        /// Versión: 1.0
        /// <summary>
        /// Realiza una inserción básica a la tabla.
        /// </summary>
        /// <param name="dto">Entidad TipoExperienciaDTO a insertar</param>
        /// <returns>Retorna 200 y el objeto ingresado o 400 y un mensaje de error </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult<TipoExperienciaDTO> Insertar([FromBody] TipoExperienciaDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_tipoExperienciaService.Insertar(dto, _tokenManager.UserName));
        }

        /// Método HTTP: PUT
        /// Autor:Juan D. Huanaco Quispe
        /// Fecha: 09/04/2024
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualización básica a la tabla
        /// </summary>
        /// <param name="dto">Entidad TipoExperienciaDTO a actualizar</param>
        /// <returns>Retorna 200 con el objeto actualizado o 400 con un mensaje de error</returns>
        [Route("[action]")]
        [HttpPut]
        public ActionResult<TipoExperienciaDTO> Actualizar([FromBody] TipoExperienciaDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_tipoExperienciaService.Actualizar(dto, _tokenManager.UserName));
        }

        /// Método HTTP: DELETE
        /// Autor:Juan D. Huanaco Quispe
        /// Fecha: 09/04/2024
        /// Versión: 1.0
        /// <summary>
        /// Realiza una eliminación básica a la tabla
        /// </summary>
        /// <param name="idTipoExperiencia">Id del tipo de experiencia a eliminar</param>
        /// <returns>Retorna 200 con el objeto actualizado o 400 con un mensaje de error</returns>
        [Route("[action]/{idTipoExperiencia}")]
        [HttpDelete]
        public ActionResult<bool> Eliminar(int idTipoExperiencia)
        {
            return Ok(_tipoExperienciaService.Eliminar(idTipoExperiencia, _tokenManager.UserName));
        }
    }
}
