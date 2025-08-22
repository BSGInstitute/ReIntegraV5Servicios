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
    /// Controlador: MoodleCategoriaController
    /// Autor Creación: Gilmer Qm.
    /// Fecha: 02/05/2023
    /// <summary>
    /// Gestión de Moodle Categoria
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    //[AllowAnonymous]
    public class MoodleCategoriaController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private IMoodleCategoriaService _moodleCategoriaService;
        public MoodleCategoriaController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _moodleCategoriaService = new MoodleCategoriaService(_unitOfWork);
        }
        /// Tipo Función: GET
        /// Autor: Gilmer Quispe.
        /// Fecha: 02/05/2023
        /// Version: 1.0
        /// <summary>
        /// obtiene el combo de T_MoodleCategoriaTipo
        /// </summary> 
        /// <returns> List<ComboDTO> </returns>
        [HttpGet]
        [Route("[action]")]
        public ActionResult ObtenerCombos()
        {
            var resutlado = _unitOfWork.MoodleCategoriaTipoRepository.ObtenerCombo();
            return Ok(resutlado);
        }
        /// Tipo Función: GET
        /// Autor: Gilmer Quispe.
        /// Fecha: 02/05/2023
        /// Version: 1.0
        /// <summary>
        /// Este método obtiene una lista de categorias Moodle registrados en la base de datos
        /// </summary>
        /// <returns> List<CategoriaMoodleDTO> </returns>
        [HttpGet]
        [Route("[action]")]
        public ActionResult Obtener()
        {
            var resultado = _unitOfWork.MoodleCategoriaRepository.Obtener();
            return Ok(resultado);
        }
        /// Tipo Función: POST
        /// Autor: Gilmer Quispe.
        /// Fecha: 02/05/2023
        /// Version: 1.0
        /// <summary>
        /// inserta un nuedo dato a T_MoodleCategoria
        /// </summary>
        /// <returns> bool </returns>
        [HttpPost]
        [Route("[action]")]
        [Authorize]
        public ActionResult Insertar([FromBody] MoodleCategoriaDTO moodleCategoriaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
            bool existeCategoriaMoodle = _unitOfWork.MoodleCategoriaRepository.ExisteCategoriaMoodle(moodleCategoriaDTO.IdCategoriaMoodle);
            if (!existeCategoriaMoodle)
            {
                var resultado = _moodleCategoriaService.Insertar(moodleCategoriaDTO, registroClaimToken.UserName);
                return Ok(resultado);
            }
            return BadRequest("La Categoria Moodle Curso con ID " + moodleCategoriaDTO.IdCategoriaMoodle+ " existe y está vigente.");
        }
        /// Tipo Función: POST
        /// Autor: Gilmer Quispe.
        /// Fecha: 02/05/2023
        /// Version: 1.0
        /// <summary>
        /// Actializa un dato a T_MoodleCategoria
        /// </summary>
        /// <returns> bool </returns>
        [HttpPut]
        [Route("[action]")]
        [Authorize]
        public ActionResult Actualizar([FromBody] MoodleCategoriaDTO moodleCategoriaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
            bool existeCategoriaMoodle = _unitOfWork.MoodleCategoriaRepository.ExisteCategoriaMoodle(moodleCategoriaDTO.IdCategoriaMoodle);
            if (!existeCategoriaMoodle)
            {
                var resultado = _moodleCategoriaService.Actualizar(moodleCategoriaDTO, registroClaimToken.UserName);
                return Ok(resultado);
            }
            return BadRequest("La Categoria Moodle Curso con ID " + moodleCategoriaDTO.IdCategoriaMoodle + " existe y está vigente.");
        }
        /// Tipo Función: DELETE 
        /// Autor: Gretel Canasa.
        /// Fecha: 03/05/2023
        /// Version: 1.0
        /// <summary>
        /// Elimina un dato a T_MoodleCategoria
        /// </summary>
        /// <returns> bool </returns>
        [HttpDelete]
        [Route("[action]/{idMoodleCategoria}")]
        [Authorize]
        public ActionResult Eliminar(int idMoodleCategoria)
        {
            var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
            var resultado = _moodleCategoriaService.EliminarMoodleCategoria(idMoodleCategoria, registroClaimToken.UserName);
            return Ok(resultado);
        }
    }
}
