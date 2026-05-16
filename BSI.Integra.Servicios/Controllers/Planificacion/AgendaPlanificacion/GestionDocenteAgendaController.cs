using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.SCode.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BSI.Integra.Servicios.Controllers.Planificacion.AgendaPlanificacion
{
    /// Autor: Joseph Llanque
    /// Fecha: 20/02/2026
    /// Versión: 1.0
    /// <summary>
    /// Controlador para la gestión de la agenda de docentes.
    /// Provee endpoints GET para: lista de docentes con cursos, detalle de docente,
    /// tabs configurados desde BD, actividades de todos los tabs y actividades por tab específico.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("CorsVista")]
    public class GestionDocenteAgendaController : ControllerBase
    {
        private readonly IGestionDocenteAgendaService _gestionDocenteAgendaService;
        private readonly IUnitOfWork _unitOfWork;

        public GestionDocenteAgendaController(IGestionDocenteAgendaService gestionDocenteAgendaService, IUnitOfWork unitOfWork)
        {
            _gestionDocenteAgendaService = gestionDocenteAgendaService;
            _unitOfWork = unitOfWork;
        }


        /// Tipo Función: GET
        /// Autor: Jose Vega
        /// Fecha: 19/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Endpoint que obtiene el detalle completo de un docente: datos personales (nombre, celular,
        /// correo, personal asignado, país, ciudad), flujo asignado y todos sus cronogramas con sesiones.
        /// El curso indicado por idPEspecifico se lista en primer lugar.
        /// </summary>
        /// <param name="idProveedor">Identificador del docente/proveedor.</param>
        /// <param name="idPEspecifico">Identificador del curso a priorizar en la lista de cronogramas.</param>
        /// <param name="idGestionContacto">Identificador opcional del GestionContacto para obtener el flujo asignado.</param>
        /// <returns>ActionResult con DocenteAgendaDetalleDTO.</returns>
        [HttpGet("ObtenerDetalleDocente/{idProveedor}/{idPEspecifico}")]
        public IActionResult ObtenerDetalleDocente(int idProveedor, int idPEspecifico, [FromQuery] int? idGestionContacto)
        {
            try
            {
                var detalle = _gestionDocenteAgendaService.ObtenerDetalleDocente(idProveedor, idPEspecifico, idGestionContacto);
                if (detalle == null) return NotFound(new { Exito = false, Mensaje = "No se encontró el docente." });
                return Ok(detalle);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }

        /// Tipo Función: GET
        /// Autor: Joseph Llanque
        /// Fecha: 24/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la configuración de todos los tabs de agenda activos para un área de trabajo.
        /// </summary>
        /// <param name="codigoAreaTrabajo">Código del área de trabajo (ej: "PLA").</param>
        /// <returns>ActionResult con lista de AgendaTabConfiguracionPlanificacionAlternoDTO.</returns>
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
        /// Autor: Joseph Llanque
        /// Fecha: 24/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las actividades de todos los tabs configurados, agrupadas por nombre de tab.
        /// Si idAsesor = 0 no filtra por personal asignado.
        /// </summary>
        /// <param name="codigoAreaTrabajo">Código del área de trabajo.</param>
        /// <param name="idAsesor">ID del personal asignado; 0 para obtener todos.</param>
        /// <returns>ActionResult con Dictionary NombreTab → lista de ActividadAgendaPlanificacionDTO.</returns>
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
        /// Autor: Joseph Llanque
        /// Fecha: 24/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Carga las actividades de un tab específico por su ID y retorna la cantidad total.
        /// Si idAsesor = 0 no filtra por personal asignado.
        /// </summary>
        /// <param name="idTab">ID del tab (T_AgendaTabConfiguracionPlanificacion.Id).</param>
        /// <param name="codigoAreaTrabajo">Código del área de trabajo.</param>
        /// <param name="idAsesor">ID del personal asignado; 0 para obtener todos.</param>
        /// <returns>ActionResult con CargarActividadPorTabResultadoDTO (ActividadesAgenda + Cantidad).</returns>
        [HttpGet("CargarActividadPorTab/{idTab}/{codigoAreaTrabajo}/{idAsesor}")]
        public IActionResult CargarActividadPorTab(int idTab, string codigoAreaTrabajo, int idAsesor)
        {
            try
            {
                var resultado = _gestionDocenteAgendaService.CargarActividadSeleccionadaPorFiltro(idTab, codigoAreaTrabajo, idAsesor);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }

        /// Tipo Función: GET
        /// Autor: Joseph Llanque
        /// Fecha: 07/05/2026
        /// Versión: 1.0
        /// <summary>
        /// Endpoint dedicado del tab "Mensajes Recibidos". Ejecuta directamente
        /// pla.SP_GestionDocenteMensajesRecibidosObtener con filter opcional por
        /// personal asignado. NO pasa por la pipeline genérica de tabs porque el
        /// shape del SP es distinto (una fila por canal, columnas TipoMensaje,
        /// AsuntoMensaje, etc.) y necesita parámetro propio.
        /// </summary>
        /// <param name="idPersonalAsignado">ID del personal asignado; 0 para sin filtro.</param>
        /// <returns>Lista de MensajeRecibidoAgendaDTO (hasta 2 filas por docente).</returns>
        [HttpGet("ObtenerMensajesRecibidos/{idPersonalAsignado}")]
        public IActionResult ObtenerMensajesRecibidos(int idPersonalAsignado)
        {
            try
            {
                var lista = _gestionDocenteAgendaService.ObtenerMensajesRecibidos(idPersonalAsignado);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }

        /// Tipo Función: GET
        /// Autor: Jose Vega
        /// Fecha: 03/03/2026
        /// Versión: 1.1
        /// <summary>
        /// Obtiene la lista de docentes que comparten el mismo centro de costo que la gestión de contacto proporcionada.
        /// Útil para alternar entre docentes vinculados al mismo contexto de trabajo/centro de costo.
        /// </summary>
        /// <param name="idGestionContacto">Identificador de la gestión de contacto.</param>
        /// <returns>ActionResult con lista de DocenteConCursoDTO.</returns>
        [HttpGet("ObtenerDocentesPorGestionContacto/{idGestionContacto}")]
        public IActionResult ObtenerDocentesPorGestionContacto(int idGestionContacto)
        {
            try
            {
                var lista = _gestionDocenteAgendaService.ObtenerDocentesPorGestionContacto(idGestionContacto);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }

        /// Tipo Función: GET
        /// Autor: Jose Vega
        /// Fecha: 03/03/2026
        /// Versión: 1.0
        /// <summary>
        /// Obtiene información adicional faltante del docente: email, historial WhatsApp completo,
        /// historial de correos (asuntos), resumen de última comunicación, encuestas y cantidad de alumnos.
        /// </summary>
        /// <param name="idProveedor">Identificador del docente.</param>
        /// <param name="idPEspecifico">Identificador del curso.</param>
        /// <returns>ActionResult con InformacionFaltanteDocenteDTO.</returns>
        [HttpGet("ObtenerInformacionFaltante/{idProveedor}/{idPEspecifico}")]
        public IActionResult ObtenerInformacionFaltante(int idProveedor, int idPEspecifico)
        {
            try
            {
                var info = _gestionDocenteAgendaService.ObtenerInformacionFaltanteDocente(idProveedor, idPEspecifico);
                return Ok(info);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }

        /// Tipo Función: GET
        /// Autor: Joseph Llanque
        /// Fecha: 23/03/2026
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el historial de WhatsApp del docente bajo demanda.
        /// Separado de ObtenerInformacionFaltante para evitar N+1 queries en la carga inicial.
        /// Se llama únicamente cuando el usuario abre el tab de WhatsApp en la ficha del docente.
        /// </summary>
        /// <param name="idProveedor">Identificador del docente.</param>
        /// <returns>ActionResult con lista de WhatsAppHistorialDocenteDTO.</returns>
        [HttpGet("ObtenerHistorialWhatsApp/{idProveedor}")]
        public IActionResult ObtenerHistorialWhatsApp(int idProveedor)
        {
            try
            {
                var historial = _gestionDocenteAgendaService.ObtenerHistorialWhatsApp(idProveedor);
                return Ok(historial);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }

        /// Tipo Función: GET
        /// Autor: Joseph Llanque
        /// Fecha: 23/03/2026
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el historial de correos del docente para un curso bajo demanda.
        /// Separado de ObtenerInformacionFaltante para evitar N+1 queries en la carga inicial.
        /// Se llama únicamente cuando el usuario abre el tab de correo en la ficha del docente.
        /// </summary>
        /// <param name="idProveedor">Identificador del docente.</param>
        /// <param name="idPEspecifico">Identificador del curso.</param>
        /// <returns>ActionResult con lista de CorreoResumenDocenteDTO.</returns>
        [HttpGet("ObtenerHistorialCorreos/{idProveedor}/{idPEspecifico}")]
        public IActionResult ObtenerHistorialCorreos(int idProveedor, int idPEspecifico)
        {
            try
            {
                var historial = _gestionDocenteAgendaService.ObtenerHistorialCorreos(idProveedor, idPEspecifico);
                return Ok(historial);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }

        /// Tipo Función: GET
        /// Autor: Jose Vega
        /// Fecha: 03/03/2026
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el detalle de un correo enviado al docente, incluyendo asunto, fecha de envío,
        /// personal remitente, correo destinatario y estado de envío.
        /// </summary>
        /// <param name="idCorreo">Identificador del correo en T_MandrilEnvioCorreoGestion.</param>
        /// <returns>ActionResult con CorreoDetalleDocenteDTO.</returns>
        [HttpGet("ObtenerDetalleCorreo/{idCorreo}")]
        public IActionResult ObtenerDetalleCorreo(int idCorreo)
        {
            try
            {
                var detalle = _gestionDocenteAgendaService.ObtenerDetalleCorreo(idCorreo);
                if (detalle == null) return NotFound(new { Exito = false, Mensaje = "No se encontró el correo." });
                return Ok(detalle);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }

        /// Tipo Función: GET
        /// Autor: Jose Vega
        /// Fecha: 05/03/2026
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el body HTML de un correo enviado al docente, conectándose via IMAP
        /// con las credenciales del asesor (personal remitente).
        /// </summary>
        /// <param name="idAsesor">ID del personal que envió el correo (para obtener credenciales IMAP).</param>
        /// <param name="idCorreo">ID del mensaje en el servidor IMAP.</param>
        /// <param name="folder">Carpeta IMAP (ej: "INBOX", "[Gmail]/Sent Mail"). Por defecto "INBOX".</param>
        /// <returns>ActionResult con CorreoBodyDTO (EmailBody + ArchivosAdjuntos).</returns>
        [HttpGet("ObtenerCorreoBody/{idAsesor}/{idCorreo}")]
        public IActionResult ObtenerCorreoBody(int idAsesor, int idCorreo, [FromQuery] string folder)
        {
            try
            {
                var servicioGmailCliente = new GmailClienteService(_unitOfWork);
                var resultado = servicioGmailCliente.ObtenerCorreoBody(idAsesor, idCorreo, folder);
                if (resultado == null) return NotFound(new { Exito = false, Mensaje = "No se encontró el correo." });
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }

        /// Tipo Función: GET
        /// Autor: Joseph Llanque
        /// Fecha: 11/03/2026
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el body HTML y archivos adjuntos de un correo desde la base de datos (mkt.T_GmailCorreo),
        /// sin conectarse a IMAP. Usa el IdCorreo como PK de la tabla.
        /// </summary>
        /// <param name="idCorreo">PK de mkt.T_GmailCorreo.</param>
        /// <returns>ActionResult con CorreoBodyDTO (EmailBody + ArchivosAdjuntos).</returns>
        [HttpGet("ObtenerCorreoBodyDB/{idCorreo}")]
        public IActionResult ObtenerCorreoBodyDB(int idCorreo)
        {
            try
            {
                var resultado = _unitOfWork.GestionDocenteAgendaRepository.ObtenerCorreoBodyDB(idCorreo);
                if (resultado == null || string.IsNullOrEmpty(resultado.EmailBody))
                    return NotFound(new { Exito = false, Mensaje = "No se encontró el correo." });
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }

        /// Tipo Función: GET
        /// Autor: Joseph Llanque
        /// Fecha: 19/03/2026
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el contador de alertas del docente ejecutando pla.SP_GestionDocenteAlertasContador.
        /// </summary>
        /// <returns>ActionResult con ContadorAlertasDTO.</returns>
        [HttpGet("ObtenerContadorAlertas")]
        public IActionResult ObtenerContadorAlertas()
        {
            try
            {
                var contador = _gestionDocenteAgendaService.ObtenerContadorAlertas();
                return Ok(contador);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }

        /// Tipo Función: POST
        /// Autor: Joseph Llanque
        /// Fecha: 11/03/2026
        /// Versión: 1.0
        /// <summary>
        /// Obtiene correos recibidos desde IMAP filtrados por los criterios del FiltroKendo
        /// (ej. remitente = email del docente). Replica la lógica de Correo/ObtenerCorreoRecibido
        /// para uso exclusivo del módulo de planificación.
        /// </summary>
        /// <param name="filtro">Filtros de bandeja: IdAsesor, Folder, paginación y FiltroKendo.</param>
        /// <returns>ActionResult con BandejaCorreoDTO (ListaCorreos + TotalEnviados).</returns>
        [HttpPost("ObtenerCorreoRecibido")]
        public IActionResult ObtenerCorreoRecibido([FromBody] FiltroBandejaCorreoDTO filtro)
        {
            try
            {
                if (filtro.IdAsesor <= 0)
                    return BadRequest(new { Exito = false, Mensaje = "El IdAsesor es requerido." });

                var bandejaCorreoService = new BandejaCorreoService(_unitOfWork);
                var resultado = bandejaCorreoService.ObtenerCorreoRecibido(filtro);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }
    }
}
