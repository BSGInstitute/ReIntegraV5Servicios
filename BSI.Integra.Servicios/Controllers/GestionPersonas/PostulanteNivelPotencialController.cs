using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Implementacion;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Configurations;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers.GestionPersonas
{
    /// Controlador: PostulanteNivelPotencialController
    /// Autor: Flavio R. Mamani Fabian
    /// Fecha: 30/04/2024
    /// <summary>
    /// Estado Etapa Proceso Seleccion
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    [Authorize]
    [JwtExpirationValidation]
    public class PostulanteNivelPotencialController : ControllerBase
    {

        private ITokenManager _tokenManager;
        private IPostulanteNivelPotencialService _PostulanteNivelPotencialServic;
        public PostulanteNivelPotencialController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _PostulanteNivelPotencialServic = new PostulanteNivelPotencialService(unitOfWork);
            _tokenManager = tokenManager;
        }

        /// Tipo Función: GET
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 04/30/2024
        /// Versión: 1.0    
        /// <summary>
        /// Obtiene datos de Criterio Evaluacion Proceso
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [HttpGet("[action]")]
        public ActionResult<IEnumerable<PostulanteNivelPotencialDTO>> Obtener()
        {
            var resultado = _PostulanteNivelPotencialServic.Obtener();
            return Ok(resultado);
        }

        /// Tipo Función: POST
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 04/30/2024
        /// Versión: 1.0    
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="dto">Entidad PostulanteNivelPotencialDTO</param>
        /// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error </returns>
        [HttpPost("[action]")]
        public ActionResult<PostulanteNivelPotencialDTO> Insertar([FromBody] PostulanteNivelPotencialDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = _PostulanteNivelPotencialServic.Insertar(dto, _tokenManager.UserName);
            return Ok(respuesta);
        }

        /// Tipo Función: PUT
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 04/30/2024
        /// Versión: 1.0    
        /// <summary>
        /// Realiza una actualizacion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a modificar</param>
        /// <returns>Retorna 200 y objeto actualizado o 400 y mensaje de error</returns>
        [HttpPut("[action]")]
        public ActionResult<PostulanteNivelPotencialDTO> Actualizar([FromBody] PostulanteNivelPotencialDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = _PostulanteNivelPotencialServic.Actualizar(dto, _tokenManager.UserName);
            return Ok(respuesta);
        }

        /// Tipo Función: DELETE
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 30/04/2024
        /// Versión: 1.0    
        /// <summary>
        /// Realiza una Eliminacion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a eliminar</param>
        /// <returns>Retorna 200 y objeto eliminado o 400 y mensaje de error</returns>
        [HttpDelete("[action]/{id}")]
        public ActionResult<bool> Eliminar(int id)
        {
            var respuesta = _PostulanteNivelPotencialServic.Eliminar(id, _tokenManager.UserName);
            return Ok(respuesta);
        }
    }
}
