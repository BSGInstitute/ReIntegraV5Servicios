using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Operaciones.SCode.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: GrabacionesClasesOnlineController
    /// Autor: Jorge Gamero
    /// Fecha: 13/01/2025
    /// <summary>
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class GrabacionesClasesOnlineController : Controller
    {
        private IUnitOfWork unitOfWork;
        public GrabacionesClasesOnlineController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// Tipo Función: POST
        /// Autor: Jorge Gamero
        /// Fecha: 14/01/2025
        /// Versión: 1.0
        /// <summary>
        /// Generar la vista para la grilla principal de la interfaz Grabaciones clases Online
        /// </summary>
        /// <param name="filtro"> GrabacionesClasesOnlineFiltroDTO </param>
        /// <returns> List<GrabacionesClasesOnlineDTO> </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarVistaProgramasOnline([FromBody] GrabacionesClasesOnlineFiltroDTO filtro)
        {
            try
            {
                var grabacionesClasesOnline = new GrabacionesClasesOnlineService(unitOfWork);
                var lista = grabacionesClasesOnline.GenerarVistaProgramasOnline(filtro);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Jorge Gamero
        /// Fecha: 15/01/2025
        /// Versión: 1.0
        /// <summary>
        /// Generar la vista para la grilla secundaria (modal) de la interfaz Grabaciones clases Online
        /// </summary>
        /// <param name="filtro"> SesionesFiltroDTO </param>
        /// <returns> List<SesionesClasesOnlineResumenDTO> </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerSesiones([FromBody] SesionesFiltroDTO filtro)
        {
            try
            {
                var grabacionesClasesOnline = new GrabacionesClasesOnlineService(unitOfWork);
                var lista = grabacionesClasesOnline.ObtenerSesiones(filtro);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Jorge Gamero
        /// Fecha: 31/01/2025
        /// Versión: 1.0
        /// <summary>
        /// Generar la vista para la grilla secundaria (modal) de la interfaz "Detalle Resúmenes" de grabaciones
        /// </summary>
        /// <param name="filtro"> SesionesFiltroDTO </param>
        /// <returns> List<SesionesClasesOnlineDetalleResumenDTO> </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerDetalleResumenGrabacionSesion([FromBody] SesionesFiltroDTO filtro)
        {
            try
            {
                var grabacionesClasesOnline = new GrabacionesClasesOnlineService(unitOfWork);
                var lista = grabacionesClasesOnline.ObtenerDetalleResumenGrabacionSesion(filtro);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Jorge Gamero
        /// Fecha: 01/02/2025
        /// Versión: 1.0
        /// <summary>
        /// Actualiza sesiones
        /// </summary>
        /// <param name="filtro"> SesionesClasesOnlineModificarFiltroDTO </param>
        /// <returns> bool </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarSesiones([FromBody] SesionesClasesOnlineModificarFiltroDTO filtro)
        {
            try
            {
                var grabacionesClasesOnline = new GrabacionesClasesOnlineService(unitOfWork);
                var consulta = grabacionesClasesOnline.ActualizarSesiones(filtro);
                return Ok(consulta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Jorge Gamero
        /// Fecha: 01/02/2025
        /// Versión: 1.0
        /// <summary>
        /// Modifica numeroDia de T_DisponibilidadProgramaSincronicoDefecto
        /// </summary>
        /// <param name="filtro"> DataDisponibilidadProgramaDefectoDTO </param>
        /// <returns> bool </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ModificarDisponibilidadProgramaDefecto([FromBody] DataDisponibilidadProgramaDefectoDTO filtro)
        {
            try
            {
                var grabacionesClasesOnline = new GrabacionesClasesOnlineService(unitOfWork);
                var consulta = grabacionesClasesOnline.ModificarDisponibilidadProgramaDefecto(filtro);
                return Ok(consulta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Jorge Gamero
        /// Fecha: 01/02/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene disponibilidad de programa
        /// </summary>
        /// <param>  </param>
        /// <returns> List<DataDisponibilidadProgramaDefectoDTO> </returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerDisponibilidadPrograma()
        {
            try
            {
                var grabacionesClasesOnline = new GrabacionesClasesOnlineService(unitOfWork);
                var lista = grabacionesClasesOnline.ObtenerDisponibilidadPrograma();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Marco Jose Villanueva Torres
        /// Fecha: 2026-04-09
        /// Versión: 1.0
        /// <summary>
        /// Calcula la fecha final de sesión basándose en la última sesión del PEspecifico
        /// </summary>
        /// <param name="filtro"> SesionesFiltroDTO </param>
        /// <returns> PEspecificoUltimaSesionDTO </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult CalcularFechaFinalSesion([FromBody] SesionesFiltroDTO filtro)
        {
            try
            {
                var grabacionesClasesOnline = new GrabacionesClasesOnlineService(unitOfWork);
                var resultado = grabacionesClasesOnline.ObtenerUltimaSesionPorIdPEspecifico(filtro.IdPEspecifico.Value);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
