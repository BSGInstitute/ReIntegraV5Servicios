using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
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
    /// Controlador: FeedbackConfigurarGrupoPreguntaController
    /// Autor: Klebert Layme.
    /// Fecha: 15/07/2022
    /// <summary>
    /// Gestión de AreaCapacitacion
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class FeedbackConfigurarGrupoPreguntaController : ControllerBase
    {
        private IFeedbackConfigurarGrupoPreguntaService _feedbackConfigurarGrupoPreguntaService;
        private IUnitOfWork _unitOfWork;
        public FeedbackConfigurarGrupoPreguntaController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _feedbackConfigurarGrupoPreguntaService = new FeedbackConfigurarGrupoPreguntaService(unitOfWork);
        }

        /// Tipo Función: GET
        /// Autor: Klebert Layme.
        /// Fecha: 27/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en FeedbackConfigurarGrupoPreguntaDTO
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [HttpGet("[action]")]
        public IActionResult Obtener()
        {
            return Ok(_unitOfWork.FeedbackConfigurarGrupoPreguntaRepository.ObtenerFeedbackConfigurar());
        }

        /// Tipo Función: GET
        /// Autor: Klebert Layme.
        /// Fecha: 27/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros en combo de PGeneral ,PEspecifico , FeedbackConfigurar
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [HttpGet("[action]")]
        public IActionResult ObtenerCombo()
        {
            return Ok(_feedbackConfigurarGrupoPreguntaService.ObtenerCombos());
        }

        /// Tipo Función: GET
        /// Autor: Klebert Layme.
        /// Fecha: 27/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene por id los registros en combo de PGeneral ,PEspecifico.
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [HttpGet("[action]/{id}")]
        public IActionResult ObtenerProgramasSelecionados(int id)
        {
            try
            {
                var resultado = _feedbackConfigurarGrupoPreguntaService.ObtenerListaProgramasSelecionados(id);
                return Ok(new
                {
                    resultado.ProgramasGenerales,
                    resultado.ProgramasEspecificos
                });
            }
            catch
            {
                throw;
            }
        }

        /// Tipo Función: POST
        /// Autor: Klebert Layme.
        /// Fecha: 29/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="dto">Entidad a insertar</param>
        /// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error </returns>
        [Authorize]
        [HttpPost("[action]")]
        public IActionResult Insertar([FromBody] RegistroFeedbackConfigurarGrupoPreguntaDTO dto)
        {

            try
            {
                //var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var resultado = _feedbackConfigurarGrupoPreguntaService.Insertar(dto, "klaymea");
                return Ok(resultado);
            }
            catch
            {
                throw;
            }
        }
        /// Tipo Función: PUT
        /// Autor: Klebert Layme.
        /// Fecha: 29/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla
        /// </summary>
        /// <param name="dto">Entidad a insertar</param>
        /// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error </returns>
        [HttpPut("[action]")]
        [Authorize]
        public IActionResult Actualizar([FromBody] RegistroFeedbackConfigurarGrupoPreguntaDTO dto)
        {
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var resultado = _feedbackConfigurarGrupoPreguntaService.Actualizar(dto, registroClaimToken.UserName);
                return Ok(resultado);
            }
            catch
            {
                throw;
            }
        }

        /// Tipo Función: DELETE
        /// Autor: Klebert Layme.
        /// Fecha: 01/06/2023
        /// Versión: 1.0
        /// <summary>
        /// Realiza una eliminacion logica basica a la tabla
        /// </summary>
        /// <param name="id">Id de la entidad a eliminar</param>
        /// <param name="usuario">Nombre del usuario que realiza la eliminacion</param>
        /// <returns>Retorna 200 y bandera de eliminacion realizada o 400 y mensaje de error</returns>
        [HttpDelete("[action]/{id}")]
        [Authorize]
        public IActionResult Eliminar(int id)
        {
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var resultado = _feedbackConfigurarGrupoPreguntaService.Eliminar(id, registroClaimToken.UserName);
                return Ok(resultado);
            }
            catch
            {
                throw;
            }
        }
    }
}
