using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
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
    /// Controlador: NivelEstudioController
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
    public class NivelEstudioController : ControllerBase
    {

        private ITokenManager _tokenManager;
        private INivelEstudioService _NivelEstudioService;
        public NivelEstudioController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _NivelEstudioService = new NivelEstudioService(unitOfWork);
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
        public ActionResult<IEnumerable<NivelEstudioDTO>> Obtener()
        {
            var resultado = _NivelEstudioService.Obtener();
            return Ok(resultado);
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
        public ActionResult<IEnumerable<TipoFormacionDTO>> ObtenerFormacion()
        {
            var resultado = _NivelEstudioService.ObtenerFormacion();
            return Ok(resultado);
        }


        /// Tipo Función: POST
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 04/30/2024
        /// Versión: 1.0    
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="dto">Entidad NivelEstudioDTO</param>
        /// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error </returns>
        [HttpPost("[action]")]
        public ActionResult<NivelEstudioComboDTO> Insertar([FromBody] NivelEstudioComboDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = _NivelEstudioService.Insertar(dto, _tokenManager.UserName);
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
        public ActionResult<NivelEstudioComboDTO> Actualizar([FromBody] NivelEstudioComboDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = _NivelEstudioService.Actualizar(dto, _tokenManager.UserName);
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
            var respuesta = _NivelEstudioService.Eliminar(id, _tokenManager.UserName);
            return Ok(respuesta);
        }
    }
}
