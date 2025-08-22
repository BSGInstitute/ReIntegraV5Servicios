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
    /// Controlador: TipoDocumentoAlumnoController
    /// Autor: Christian Quispe Mamani.
    /// Fecha: 16/05/2023
    /// <summary>
    /// Gestión de TipoDocumento
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class TipoDocumentoAlumnoController : ControllerBase
    {
        private ITipoDocumentoAlumnoService _tipoDocumentoAlumnoService;
        private IUnitOfWork _unitOfWork;
        public TipoDocumentoAlumnoController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _tipoDocumentoAlumnoService = new TipoDocumentoAlumnoService(_unitOfWork);
        }
        /// Tipo Función: GET
        /// Autor: Christian Quispe Mamani.
        /// Fecha: 16/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_TipoDocumentoAlumno
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [HttpGet("[action]")]
        public IActionResult Obtener()
        {
            try
            {
                var resultado = _tipoDocumentoAlumnoService.Obtener();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Christian Quispe Mamani.
        /// Fecha: 16/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en V_TPlantilla_CertificadoConstancia
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [HttpGet("[action]")]
        public IActionResult ObtenerPlantillaCertificadoConstancia()
        {
            try
            {
                var resultado = _tipoDocumentoAlumnoService.ObtenerPlantillaCertificadoConstancia();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Christian Quispe Mamani.
        /// Fecha: 16/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en V_TPlantilla_CertificadoConstancia
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [HttpGet("[Action]/{id}")]
        public IActionResult ObtenerDetalleConfiguracionCerficicado(int id)
        {
            try
            {
                var resultado = _tipoDocumentoAlumnoService.ObtenerDetalleConfiguracionCerficicado(id);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Autor: Christian A. Quispe Mamani
        /// Fecha: 16/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los ids de las siguientes tablas:
        /// T_TipoDocumentoAlumnoPGeneral,
        /// T_TipoDocumentoAlumnoSubEstadoMatricula,
        /// T_TipoDocumentoAlumnoEstadoMatricula,
        /// T_TipoDocumentoAlumnoModalidadCurso
        /// </summary>
        /// <returns> Lista ComboPGDTO </returns>
        [HttpGet("[Action]/{id}")]
        public IActionResult ObtenerDetalleTipoDocumento(int id)
        {
            try
            {
                var resultado = _tipoDocumentoAlumnoService.ObtenerIdsDetalleTipoDocumento(id);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
        /// Autor: Christian A. Quispe Mamani
        /// Fecha: 16/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los Id y Nombres
        /// </summary>
        /// <returns> Lista TipoDocumentoAlumnoCombosDTO </returns>
        [HttpGet("[Action]")]
        public IActionResult ObtenerCombos()
        {
            try
            {
                var resultado = _tipoDocumentoAlumnoService.ObtenerCombosTipoDocumento();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
        /// Autor: Christian A. Quispe Mamani
        /// Fecha: 16/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los Id y Nombres
        /// </summary>
        /// <returns> Lista TipoDocumentoAlumnoCombosDTO </returns>
        [Authorize]
        [HttpPost("[Action]")]
        public IActionResult Insertar([FromBody] TipoDocumentoAlumnoEntidadDTO dto)
        {
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var resultado = _tipoDocumentoAlumnoService.InsertarTipoDocumentoAlumno(dto, registroClaimToken.UserName);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
        /// Autor: Christian A. Quispe Mamani
        /// Fecha: 16/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los Id y Nombres
        /// </summary>
        /// <returns> Lista TipoDocumentoAlumnoCombosDTO </returns>
        [Authorize]
        [HttpPut("[Action]")]
        public IActionResult Actualizar([FromBody] TipoDocumentoAlumnoEntidadDTO dto)
        {
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var resultado = _tipoDocumentoAlumnoService.ActualizarTipoDocumentoAlumno(dto, registroClaimToken.UserName);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
        /// Autor: Christian A. Quispe Mamani
        /// Fecha: 16/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los Id y Nombres
        /// </summary>
        /// <returns> Lista TipoDocumentoAlumnoCombosDTO </returns>
        [Authorize]
        [HttpDelete("[Action]/{id}")]
        public IActionResult Eliminar(int id)
        {
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var resultado = _tipoDocumentoAlumnoService.EliminarTipoDocumentoAlumno(id, registroClaimToken.UserName);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
    }
}
