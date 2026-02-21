using BSI.Integra.Aplicacion.Planificacion.SCode.Service.Interface;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BSI.Integra.Servicios.Controllers.Planificacion.AgendaPlanificacion
{
    /// Controlador: GestionDocenteAgendaController
    /// Autor: Jose Vega
    /// Fecha: 21/02/2026
    /// <summary>
    /// Gestión de tabs y actividades de la agenda de planificación docente.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class GestionDocenteAgendaController : ControllerBase
    {
        private readonly IGestionDocenteAgendaService _gestionDocenteAgendaService;

        public GestionDocenteAgendaController(IGestionDocenteAgendaService gestionDocenteAgendaService)
        {
            _gestionDocenteAgendaService = gestionDocenteAgendaService;
        }

        /// Tipo Función: GET
        /// Autor: Jose Vega
        /// Fecha: 21/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las configuraciones de tabs de agenda para el área de trabajo indicada.
        /// </summary>
        /// <param name="codigoAreaTrabajo">Código del área de trabajo</param>
        /// <returns>Lista de configuraciones de tabs</returns>
        [HttpGet("ObtenerTabsConfigurados/{codigoAreaTrabajo}")]
        public IActionResult ObtenerTabsConfigurados(string codigoAreaTrabajo)
        {
            try
            {
                var tabs = _gestionDocenteAgendaService.ObtenerTabsConfigurados(codigoAreaTrabajo);
                return Ok(tabs);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }

        /// Tipo Función: GET
        /// Autor: Jose Vega
        /// Fecha: 21/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todas las actividades de todos los tabs configurados para el asesor.
        /// </summary>
        /// <param name="codigoAreaTrabajo">Código del área de trabajo</param>
        /// <param name="idAsesor">Id del personal asignado (0 para todos)</param>
        /// <returns>Diccionario de actividades agrupadas por nombre del tab</returns>
        [HttpGet("ObtenerActividades/{codigoAreaTrabajo}/{idAsesor}")]
        public IActionResult ObtenerActividades(string codigoAreaTrabajo, int idAsesor)
        {
            try
            {
                var actividades = _gestionDocenteAgendaService.ObtenerActividades(idAsesor, codigoAreaTrabajo);
                return Ok(actividades);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }

        /// Tipo Función: GET
        /// Autor: Jose Vega
        /// Fecha: 21/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Carga las actividades de un tab específico.
        /// </summary>
        /// <param name="idTab">Id del AgendaTab</param>
        /// <param name="codigoAreaTrabajo">Código del área de trabajo</param>
        /// <param name="idAsesor">Id del personal asignado (0 para todos)</param>
        /// <returns>Actividades del tab seleccionado y cantidad</returns>
        [HttpGet("CargarActividadPorTab/{idTab}/{codigoAreaTrabajo}/{idAsesor}")]
        public IActionResult CargarActividadPorTab(int idTab, string codigoAreaTrabajo, int idAsesor)
        {
            try
            {
                var resultado = _gestionDocenteAgendaService.CargarActividadSeleccionadaPorFiltro(idTab, codigoAreaTrabajo, idAsesor);
                return Ok(new { ActividadesAgenda = resultado.ActividadesAgenda, Cantidad = resultado.Cantidad });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }
    }
}
