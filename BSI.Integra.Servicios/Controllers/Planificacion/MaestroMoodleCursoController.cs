using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
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
    /// Controlador: MoodleCursoController
    /// Autor Creación: Gilmer Qm.
    /// Fecha: 02/05/2023
    /// <summary>
    /// Gestión de Moodle Curso
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    //[AllowAnonymous]
    public class MaestroMoodleCursoController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private IMaestroCursoMoodleService _maestroMoodleCursoService;
        public MaestroMoodleCursoController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _maestroMoodleCursoService = new MaestroCursoMoodleService(_unitOfWork);
        }
        /// Tipo Función: GET
        /// Autor: Gilmer Quispe.
        /// Fecha: 02/05/2023
        /// Version: 1.0
        /// <summary>
        /// obtiene el combo de T_MoodleCursoTipo
        /// </summary> 
        /// <returns> List<ComboDTO> </returns>
        [HttpGet]
        [Route("[action]")]
        public ActionResult ObtenerComboMoodleCategoria()
        {
            try
            {
                var comboDTOs = _maestroMoodleCursoService.ObtenerComboMoodleCategoria();
                return Ok(comboDTOs);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Gilmer Quispe.
        /// Fecha: 02/05/2023
        /// Version: 1.0
        /// <summary>
        /// Este método obtiene una lista de categorias Moodle registrados en la base de datos
        /// </summary>
        /// <returns> List<CursoMoodleDTO> </returns>
        [HttpGet]
        [Route("[action]")]
        public ActionResult ObtenerCursosRegistradas()
        {
            try
            {
                var categoriaMoodleDTOs = _maestroMoodleCursoService.ObtenerCursosMoodleRegistradas();
                return Ok(categoriaMoodleDTOs);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Gilmer Quispe.
        /// Fecha: 02/05/2023
        /// Version: 1.0
        /// <summary>
        /// inserta un nuedo dato a T_MoodleCurso
        /// </summary>
        /// <returns> bool </returns>
        [HttpPost]
        [Route("[action]")]
        [Authorize]
        public ActionResult InsertarMoodleCurso([FromBody] MoodleCursoDTO moodleCursoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                bool existeCursoMoodle = _maestroMoodleCursoService.ExisteCursoMoodle(moodleCursoDTO.IdCursoMoodle ?? 0);
                if (!existeCursoMoodle)
                {
                    var insercion = _maestroMoodleCursoService.InsertarMoodleCurso(moodleCursoDTO, registroClaimToken.UserName);
                    return Ok(insercion);
                }
                return BadRequest("El Moodle Curso con ID " + moodleCursoDTO.IdCursoMoodle + " existe y está vigente.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Gilmer Quispe.
        /// Fecha: 02/05/2023
        /// Version: 1.0
        /// <summary>
        /// Actializa un dato a T_MoodleCurso
        /// </summary>
        /// <returns> bool </returns>
        [HttpPut]
        [Route("[action]")]
        [Authorize]
        public ActionResult ActualizarMoodleCurso([FromBody] MoodleCursoDTO moodleCursoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                bool existeCursoMoodle = _maestroMoodleCursoService.ExisteCursoMoodle(moodleCursoDTO.IdCursoMoodle ?? 0);
                if (!existeCursoMoodle) { 
                    var actualizacion = _maestroMoodleCursoService.ActualizarMoodleCurso(moodleCursoDTO, registroClaimToken.UserName);
                    return Ok(actualizacion);
                }
                return BadRequest("El Moodle Curso con ID "+moodleCursoDTO.IdCursoMoodle +" existe y está vigente.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: DELETE 
        /// Autor: Gretel Canasa.
        /// Fecha: 03/05/2023
        /// Version: 1.0
        /// <summary>
        /// Elimina un dato a T_MoodleCurso
        /// </summary>
        /// <returns> bool </returns>
        [HttpDelete]
        [Route("[action]/{idMoodleCurso}")]
        [Authorize]
        public ActionResult EliminarMoodleCurso(int idMoodleCurso)
        {

            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var eliminacion = _maestroMoodleCursoService.EliminarMoodleCurso(idMoodleCurso, registroClaimToken.UserName);
                return Ok(eliminacion);
            }
            catch
            {
                throw;
            }
        }
    }
}
