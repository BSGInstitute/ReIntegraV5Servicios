using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion
{
    public class FinalizarProgramarGestionPlaDTO
    {
        public ActividadGestionAntiguaDTO ActividadAntigua { get; set; }
        public DatosGestionDocenteDTO DatosGestion { get; set; }
        public DatosFiltroFinalizarActividadDTO? Filtro { get; set; }
    }

    public class ActividadGestionAntiguaDTO
    {
        public int Id { get; set; }
        public string? Comentario { get; set; }
        public int IdOcurrencia { get; set; }
        public int IdOcurrenciaActividad { get; set; }
        public int IdProveedor { get; set; }
        public int IdGestionContacto { get; set; }
    }

    public class DatosGestionDocenteDTO
    {
        public int? IdCentroCosto { get; set; }
        public int? IdPersonalAsignado { get; set; }
        public int? IdClasificacionPersona { get; set; }
        public int? IdFaseGestionContacto { get; set; }
        public int? IdOrigen { get; set; }
        public string? UltimoComentario { get; set; }
        public int? IdEstadoGestionContacto { get; set; }
        public int? IdSubEstadoGestionContacto { get; set; }
        public bool? EstadoSeguimientoWhatsApp { get; set; }
        public string? UltimaFechaProgramada { get; set; }
    }
    public class CrearGestionContactoDTO
    {
        public int? IdCentroCosto { get; set; }
        public int IdPersonal_Asignado { get; set; }    // Asesor
        public int IdClasificacionPersona { get; set; } // Docente
        public int IdFaseGestionContacto { get; set; }
        public int IdOrigen { get; set; }
        public string UsuarioCreacion { get; set; }
        public string Comentario { get; set; }
        public int IdEstadoGestionContacto { get; set; }
    }

    public class PEspecificoSesionProveedorDTO
    {
        public int IdProveedor { get; set; }
        public string NombreProveedor { get; set; }
    }

    public class CrearOportunidadDocenteDTO
    {
        public int? IdCentroCosto { get; set; }
        /// <summary>
        /// ID de conf.T_ClasificacionPersona (cp.Id del combo ObtenerDocentes).
        /// Enviar este campo para flujo "General" (docente postulante o proveedor desde el combo).
        /// </summary>
        public int? IdClasificacionPersona { get; set; }
        /// <summary>
        /// ID de fin.T_Proveedor. Enviar este campo para flujo "Asignado al Curso"
        /// donde el proveedor viene de la cascada PE Específico → Proveedor.
        /// </summary>
        public int? IdProveedor { get; set; }
        public string UsuarioCreacion { get; set; }
    }

    public class GestionContactoSimpleDTO
    {
        public int Id { get; set; }
        public int? IdClasificacionPersona { get; set; }
        public int? IdPersonal_Asignado { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? IdFaseGestionContacto { get; set; }
        public int? IdEstadoGestionContacto { get; set; }
        public int? IdOrigen { get; set; }

        public string? UltimoComentario { get; set; }
        public string? UsuarioCreacion { get; set; }
        public string? UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }

    }
    public class ProveedorClasificacionDTO
    {
        public int IdClasificacionPersona { get; set; }
        public int IdProveedor { get; set; }
        public string RazonSocial { get; set; }
        public int IdTipoPersona { get; set; }
    }

    public class EstadoGestionContactoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }

    public class InsertarGestionContactoDocenteFlujoDTO
    {
        public int IdGestionContacto { get; set; }
        public int IdGestionDocenteFlujo { get; set; }
        public string UsuarioCreacion { get; set; }
    }

    public class DocenteComboDTO
    {
        /// <summary>ID de conf.T_ClasificacionPersona — único en el sistema independiente del tipo.</summary>
        public int Id { get; set; }
        public int IdTipoPersona { get; set; }
        public string NombreTipoPersona { get; set; }
        public string Nombre { get; set; }
        /// <summary>ID de la tabla de origen (fin.T_Proveedor.Id o pla.T_DocentePostulante.Id según IdTipoPersona).</summary>
        public int IdTablaOriginal { get; set; }
    }

    public class OportunidadDocenteListItemDTO
    {
        public int Id { get; set; }
        public int? DocenteId { get; set; }
        public string DocenteNombre { get; set; }
        public int? IdCategoria { get; set; }
        public string NombreCategoria { get; set; }
        public int? IdPais { get; set; }
        public string Curso { get; set; }
        public string FlujoAsignado { get; set; }
    }

    public class OportunidadDocenteListResponseDTO
    {
        public IEnumerable<OportunidadDocenteListItemDTO> Oportunidades { get; set; }
        public int Total { get; set; }
        public int Pagina { get; set; }
        public int PorPagina { get; set; }
    }

    // ===== DTOs para Actividades de Flujo por Categoría =====

    public class ActividadesFlujoPorCategoriaResponseDTO
    {
        public int IdGestionContactoFlujoCongelado { get; set; }
        public int IdCategoria { get; set; }
        public string NombreCategoria { get; set; }
        public int IdGestionDocenteActividadCabecera { get; set; }
        public IEnumerable<SesionConActividadesDTO> Sesiones { get; set; }
        public IEnumerable<ActividadCabeceraDTO> Actividades { get; set; }
    }

    public class SesionConActividadesDTO
    {
        public int IdSesion { get; set; }
        public int IdGestionDocenteActividadCabecera { get; set; }
        public int NumeroSesion { get; set; }
        public DateTime? FechaInicioSesion { get; set; }
        public int? IdPEspecifico { get; set; }
        public string NombrePEspecifico { get; set; }
        public int? IdProveedor { get; set; }
        public string RazonSocialDocente { get; set; }
        public IEnumerable<ActividadCabeceraDTO> Actividades { get; set; }
    }

    public class ActividadCabeceraDTO
    {
        public int IdGestionDocenteActividadCabecera { get; set; }
        public int IdGestionDocenteActividadCabeceraCongelada { get; set; }
        public string NombreCabecera { get; set; }
        public string DescripcionCabecera { get; set; }
        public IEnumerable<ActividadDetalleDTO> Detalles { get; set; }
    }

    public class ActividadDetalleDTO
    {
        public int IdGestionDocenteActividadCabecera { get; set; }
        public int IdGestionDocenteActividadDetalleCongelada { get; set; }
        public string NombreDetalle { get; set; }
        public string NombrePlantilla { get; set; }
        public string MedioComunicacion { get; set; }
        public string EstadoEjecucionDetalle { get; set; }
        public IEnumerable<DisparadorDTO> Disparadores { get; set; }
    }

    public class DisparadorDTO
    {
        public int IdDisparadorCongelado { get; set; }
        public string TipoDisparador { get; set; }
        public DateTime? FechaProgramada { get; set; }
        public DateTime? FechaFija { get; set; }
        public int? CantidadTiempoRelativo { get; set; }
        public string UnidadTiempo { get; set; }
        public string CodigoReferenciaTiempo { get; set; }
        public string NombreReferenciaTiempo { get; set; }
        public string NombreEvento { get; set; }
        public string OcurrenciaPrevia { get; set; }
        public string EstadoEjecucionDisparador { get; set; }
        public bool TieneFechaFija { get; set; }
        public bool TieneTiempoRelativo { get; set; }
        public bool TieneEvento { get; set; }
        public bool TieneOcurrenciaPrevia { get; set; }
    }

    // ===== DTOs para Hangfire - Ejecución de Actividades Congeladas =====

    /// <summary>
    /// DTO que representa una actividad pendiente de ejecución por Hangfire
    /// </summary>
    public class ActividadPendienteDTO
    {
        public int IdActividadDetalleCongelada { get; set; }
        public string NombreActividad { get; set; }
        public int IdTipoActividad { get; set; }
        public int IdPlantillaMedioComunicacion { get; set; }
        public int IdGestionContactoFlujoCongelado { get; set; }
        public int IdGestionContactoDocenteFlujo { get; set; }
        public int IdGestionContacto { get; set; }
        public int IdDisparadorCongelado { get; set; }
        public string TipoDisparador { get; set; }
        public DateTime FechaEjecucion { get; set; }
        public int? IdPEspecificoSesion { get; set; }
        public int IdPlantillaBase { get; set; }
        public int IdPlantilla { get; set; }
    }

    /// <summary>
    /// DTO para actualizar el estado de una actividad después de su ejecución
    /// </summary>
    public class ActualizarEstadoRequestDTO
    {
        public int IdActividadDetalleCongelada { get; set; }
        public int IdDisparadorCongelado { get; set; }
        public string CodigoNuevoEstado { get; set; }
        public string MensajeResultado { get; set; }
        public string MensajeError { get; set; }
        public string UsuarioModificacion { get; set; } = "HANGFIRE";
    }

    /// <summary>
    /// DTO para marcar una ocurrencia y activar disparadores dependientes
    /// </summary>
    public class MarcarOcurrenciaRequestDTO
    {
        public int IdGestionDocenteOcurrenciaCongelada { get; set; }
        public int IdGestionContacto { get; set; }
        public string Comentario { get; set; }
        public DateTime? FechaHoraOcurrencia { get; set; }
        public string UsuarioCreacion { get; set; }
    }

    /// <summary>
    /// DTO que representa el resultado de una operación de ejecución
    /// </summary>
    public class ResultadoEjecucionDTO
    {
        public bool Exitoso { get; set; }
        public int IdRegistro { get; set; }
        public string Mensaje { get; set; }
        public string Error { get; set; }
        public DateTime? NuevaFecha { get; set; }
        public ActividadPendienteDTO DatosActividad { get; set; }
    }

    /// <summary>
    /// DTO para ejecutar una actividad manualmente sin esperar a Hangfire
    /// </summary>
    public class EjecutarActividadManualDTO
    {
        public int IdActividadDetalleCongelada { get; set; }
        public int IdDisparadorCongelado { get; set; }
        public string UsuarioEjecucion { get; set; }
        public bool MarcarOcurrenciaAsociada { get; set; } = false;
        public int? IdGestionDocenteOcurrenciaCongelada { get; set; }
        public string ComentarioOcurrencia { get; set; }
    }

    /// <summary>
    /// DTO para consultar actividades dependientes de una actividad
    /// </summary>
    public class ActividadDependienteDTO
    {
        public int IdActividadDetalleCongelada { get; set; }
        public string NombreActividad { get; set; }
        public string TipoDisparador { get; set; }
        public string NombreOcurrencia { get; set; }
    }

    /// <summary>
    /// DTO para marcar una actividad manual como completada
    /// </summary>
    public class MarcarActividadCompletadaDTO
    {
        public int IdActividadDetalleCongelada { get; set; }
        public int IdDisparadorCongelado { get; set; }
        public string Comentario { get; set; }
        public string UsuarioEjecucion { get; set; }
    }

    /// <summary>
    /// DTO para adelantar la fecha de ejecución de una actividad
    /// </summary>
    public class AdelantarActividadDTO
    {
        public int IdDisparadorCongelado { get; set; }
        public string UsuarioModificacion { get; set; }
    }

    /// <summary>
    /// Body del endpoint CongelarFlujoDocente.
    /// La fecha es opcional: si es null el backend usa la fecha actual.
    /// </summary>
    public class CongelarFlujoDocenteBodyDTO
    {
        /// <summary>Fecha de inicio del flujo en formato ISO 8601. Null = fecha actual.</summary>
        public DateTime? FechaInicioFlujo { get; set; }
    }
    /// <summary>
    /// DTO para conversión de disparadores de OCURRENCIA_PREVIA a TIEMPO_FIJO
    /// Retornado por el SP simplificado pla.SP_GestionDocenteOcurrenciaMarcar
    /// </summary>
    public class DisparadorConversionDTO
    {
        public int IdGestionDocenteOcurrenciaMarcada { get; set; }
        public int IdGestionDocenteDisparadorCongelado { get; set; }
        public int IdGestionDocenteActividadDetalleCongelada { get; set; }
        public int IdGestionDocenteDisparadorDetalle { get; set; }
        public DateTime FechaCalculada { get; set; }
        public int IdGestionDocenteEstadoPorEjecutar { get; set; }
    }
}
