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
    /// Controlador: NivelCompetenciaTecnicaController
    /// Autor: Juan D. Huanaco Quispe
    /// Fecha: 06/04/2024
    /// <summary>
    /// Gestión de Nivel Competencia Tecnica para el Modulo (M) Nivel Curso Complementario
    /// Interactua con la tabla 'gp.T_NivelCompetenciaTecnica'
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    [Authorize]
    [JwtExpirationValidation]
    public class NivelCompetenciaTecnicaController : ControllerBase
    {
        private ITokenManager _tokenManager;
        private INivelCompetenciaTecnicaService _nivelCompetenciaTecnicaService;
        public NivelCompetenciaTecnicaController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _nivelCompetenciaTecnicaService = new NivelCompetenciaTecnicaService(unitOfWork);
            _tokenManager = tokenManager;
        }

        /// Método HTTP: GET
        /// Autor:Juan D. Huanaco Quispe
        /// Fecha: 06/04/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los niveles de competencia tecnicos.
        /// </summary>
        /// <returns>Retorna 200 con la lista de objetos o 400 con un mensaje de error</returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult<IEnumerable<NivelCompetenciaTecnicaDTO>> Obtener()
        {
            return Ok(_nivelCompetenciaTecnicaService.Obtener());
        }

        /// Método HTTP: POST
        /// Autor:Juan D. Huanaco Quispe
        /// Fecha: 06/04/2024
        /// Versión: 1.0
        /// <summary>
        /// Realiza una inserción básica a la tabla.
        /// </summary>
        /// <param name="dto">Entidad NivelCompetenciaTecnicaDTO a insertar</param>
        /// <returns>Retorna 200 y el objeto ingresado o 400 y un mensaje de error </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult<NivelCompetenciaTecnicaDTO> Insertar([FromBody] NivelCompetenciaTecnicaDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_nivelCompetenciaTecnicaService.Insertar(dto, _tokenManager.UserName));
        }

        /// Método HTTP: PUT
        /// Autor:Juan D. Huanaco Quispe
        /// Fecha: 06/04/2024
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualización básica a la tabla
        /// </summary>
        /// <param name="dto">Entidad NivelCompetenciaTecnicaDTO a actualizar</param>
        /// <returns>Retorna 200 con el objeto actualizado o 400 con un mensaje de error</returns>
        [Route("[action]")]
        [HttpPut]
        public ActionResult<NivelCompetenciaTecnicaDTO> Actualizar([FromBody] NivelCompetenciaTecnicaDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_nivelCompetenciaTecnicaService.Actualizar(dto, _tokenManager.UserName));
        }

        /// Método HTTP: DELETE
        /// Autor:Juan D. Huanaco Quispe
        /// Fecha: 04/04/2024
        /// Versión: 1.0
        /// <summary>
        /// Realiza una eliminación básica a la tabla
        /// </summary>
        /// <param name="idNivelCompetenciaTecnica">Id del nivel de competencia tecnica a eliminar</param>
        /// <returns>Retorna 200 con el objeto actualizado o 400 con un mensaje de error</returns>
        [Route("[action]/{idNivelCompetenciaTecnica}")]
        [HttpDelete]
        public ActionResult<bool> Eliminar(int idNivelCompetenciaTecnica)
        {
            return Ok(_nivelCompetenciaTecnicaService.Eliminar(idNivelCompetenciaTecnica, _tokenManager.UserName));
        }
    }
}
