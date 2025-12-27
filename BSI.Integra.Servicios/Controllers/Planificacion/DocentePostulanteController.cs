using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.SCode.Service.Interface;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers.Planificacion
{
    /// Autor: Lolo Zaa
    /// Fecha: 26/12/2025
    /// <summary>
    /// Controlador para el manejo de docentes postulantes
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class DocentePostulanteController : Controller
    {
        private IDocentePostulanteService _docentePostulanteService;

        public DocentePostulanteController(IDocentePostulanteService docentePostulanteService)
        {
            _docentePostulanteService = docentePostulanteService;
        }
        /// Autor: Lolo Zaa
        /// Fecha: 26/12/2025
        /// <summary>
        /// Obtiene todos los docentes postulantes activos
        /// </summary>
        /// <returns>Lista de docentes postulantes</returns>
        [HttpGet("[Action]")]
        public IActionResult Obtener()
        {
            try
            {
                var resultado = _docentePostulanteService.Obtener();
                return Ok(resultado);
            }
            catch
            {
                throw;
            }
        }
        /// Autor: Lolo Zaa
        /// Fecha: 26/12/2025
        /// <summary>
        /// Obtiene un docente postulante por su ID
        /// </summary>
        /// <param name="id">ID del docente postulante</param>
        /// <returns>Datos del docente postulante</returns>
        [HttpGet("[Action]/{id}")]
        public IActionResult Obtener(int id)
        {
            try
            {
                var resultado = _docentePostulanteService.ObtenerPorId(id);
                return Ok(resultado);
            }
            catch
            {
                throw;
            }
        }
        /// Autor: Lolo Zaa
        /// Fecha: 26/12/2025
        /// <summary>
        /// Inserta un nuevo docente postulante
        /// </summary>
        /// <param name="dto">Datos del docente postulante a insertar</param>
        /// <returns>Docente postulante creado</returns>
        //[Authorize] // Comentado temporalmente para pruebas
        [HttpPost("[Action]")]
        public IActionResult Insertar([FromBody] DocentePostulanteDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                //var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                //var resultado = _docentePostulanteService.Insertar(dto, registroClaimToken.UserName);
                var resultado = _docentePostulanteService.Insertar(dto, "TestUser"); // Usuario temporal para pruebas
                return Ok(resultado);
            }
            catch
            {
                throw;
            }
        }
        /// Autor: Lolo Zaa
        /// Fecha: 26/12/2025
        /// <summary>
        /// Actualiza un docente postulante existente
        /// </summary>
        /// <param name="dto">Datos del docente postulante a actualizar</param>
        /// <returns>Docente postulante actualizado</returns>
        //[Authorize] // Comentado temporalmente para pruebas
        [HttpPut("[Action]")]
        public IActionResult Actualizar([FromBody] DocentePostulanteDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                //var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                //var resultado = _docentePostulanteService.Actualizar(dto, registroClaimToken.UserName);
                var resultado = _docentePostulanteService.Actualizar(dto, "TestUser"); // Usuario temporal para pruebas
                return Ok(resultado);
            }
            catch
            {
                throw;
            }
        }
        /// Autor: Lolo Zaa
        /// Fecha: 26/12/2025
        /// <summary>
        /// Elimina lógicamente un docente postulante
        /// </summary>
        /// <param name="id">ID del docente postulante a eliminar</param>
        /// <returns>Resultado de la operación</returns>
        //[Authorize] // Comentado temporalmente para pruebas
        [HttpDelete("[Action]/{id}")]
        public IActionResult Eliminar(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                //var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                //var resultado = _docentePostulanteService.Eliminar(id, registroClaimToken.UserName);
                var resultado = _docentePostulanteService.Eliminar(id, "TestUser"); // Usuario temporal para pruebas
                return Ok(resultado);
            }
            catch
            {
                throw;
            }
        }
    }
}
