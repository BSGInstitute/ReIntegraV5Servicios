using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using System;
using System.Collections.Generic;

namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion
{
    /// <summary>
    /// DTO para un �tem de la lista de docentes con cursos asignados.
    /// </summary>
    public class DocenteConCursoDTO
    {
        public int IdProveedor { get; set; }
        public string NombreDocente { get; set; }
        public int IdPEspecifico { get; set; }
        public string NombreCurso { get; set; }
        public string CodigoCurso { get; set; }
        public int? IdPersonalAsignado { get; set; }
        public string PersonalAsignado { get; set; }
        public int IdGestionContacto { get; set; }
        public int IdFlujo { get; set; }
        public string NombreFlujo { get; set; }
    }

    public class DocenteAgendaDetalleDTO
    {
        public DocenteAgendaCabeceraDTO Cabecera { get; set; }
        public DocenteAgendaFlujoDTO Flujo { get; set; }
        public List<DocenteAgendaCronogramaDTO> Cronogramas { get; set; }
    }
    public class DocenteAgendaCronogramaDTO
    {
        public int IdPEspecifico { get; set; }
        public string NombreCurso { get; set; }
        public string CodigoCurso { get; set; }
        public string EstadoCurso { get; set; }
        public string TipoCurso { get; set; }
        public string CategoriaCurso { get; set; }
        public string CiudadCurso { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaTermino { get; set; }
        public bool EsPriorizado { get; set; }
        public List<DocenteAgendaSesionDTO> Sesiones { get; set; }
    }

    public class DocenteAgendaCabeceraDTO
    {
        public int IdProveedor { get; set; }
        public string NombreCompleto { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public int? IdPersonalAsignado { get; set; }
        public string PersonalAsignado { get; set; }
        public string Pais { get; set; }
        public string Ciudad { get; set; }
    }

    public class DocenteAgendaFlujoDTO
    {
        public int IdFlujo { get; set; }
        public string NombreFlujo { get; set; }
        public string DescripcionFlujo { get; set; }
    }

    /// <summary>
    /// DTO para una sesión dentro de un cronograma de curso.
    /// </summary>
    public class SesionCronogramaDTO
    {
        public int IdSesion { get; set; }
        public DateTime FechaHoraInicio { get; set; }
        public decimal Duracion { get; set; }
        public int Grupo { get; set; }
        public string Comentario { get; set; }
    }

    /// <summary>
    /// DTO para un cronograma (programa específico) del docente con sus sesiones.
    /// </summary>
    public class CronogramaDocenteDTO
    {
        public int IdPEspecifico { get; set; }
        public string NombreCurso { get; set; }
        public string CodigoCurso { get; set; }
        public string EstadoCurso { get; set; }
        public string TipoCurso { get; set; }
        public string CategoriaCurso { get; set; }
        public string CiudadCurso { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaTermino { get; set; }
        public bool EsPriorizado { get; set; }
        public List<DocenteAgendaSesionDTO> Sesiones { get; set; }
    }

   


    public class DocenteAgendaSesionDTO
    {
        public int IdSesion { get; set; }
        public DateTime FechaHoraInicio { get; set; }
        public decimal Duracion { get; set; }
        public int Grupo { get; set; }
        public string Comentario { get; set; }
    }

    /// Autor: Joseph Llanque
    /// Fecha: 24/02/2026
    /// Versión: 1.0
    /// <summary>
    /// DTO de configuración de un tab de agenda.
    /// Resultado del JOIN entre com.T_AgendaTab y com.T_AgendaTabConfiguracionPlanificacion.
    /// </summary>
    public class AgendaTabConfiguracionPlanificacionAlternoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool VisualizarActividad { get; set; }
        public bool CargarInformacionInicial { get; set; }
        public string VistaBaseDatos { get; set; }
        public string VistaCampos { get; set; }
        public string IdFaseGestionContacto { get; set; }
        public string IdEstadoGestionContacto { get; set; }
        public string CodigoAreaTrabajo { get; set; }
        public int Numeracion { get; set; }
        public bool ValidarFecha { get; set; }
    }

    /// Autor: Joseph Llanque
    /// Fecha: 24/02/2026
    /// Versión: 1.0
    /// <summary>
    /// DTO unificado de actividad/docente retornado por los SPs dinámicos de agenda.
    /// Los campos no retornados por un SP específico quedan null.
    /// </summary>
    public class ActividadAgendaPlanificacionDTO
    {
        public int IdProveedor { get; set; }
        public string NombreDocente { get; set; }
        public string Celular { get; set; }
        public string Celular2 { get; set; }
        public string Contacto1 { get; set; }
        public string Contacto2 { get; set; }
        public string Correo { get; set; }
        public int IdGestionContacto { get; set; }
        public int? IdPersonal_Asignado { get; set; }
        public string PersonalAsignado { get; set; }
        public int? IdGestionDocenteFlujo { get; set; }
        public string NombreFlujo { get; set; }
        
        public int? IdCentroCosto { get; set; }
        public string NombreCentroCosto { get; set; }
        public string CodigoCentroCosto { get; set; }
        
        public int? IdActividadCabecera { get; set; }
        public string NombreActividadCabecera { get; set; }
        public int? IdActividadDetalle { get; set; }
        public string NombreActividadDetalle { get; set; }

        public string ActividadesCabecera { get; set; }
        public int? NumeroActividades { get; set; }
        public string Pais { get; set; }
        public string Ciudad { get; set; }
        public int? IdPEspecifico { get; set; }
        public string NombreCurso { get; set; }
        public string CodigoCurso { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaTermino { get; set; }
        public int? IdProgramaGeneral { get; set; }
        public string ProgramaGeneral { get; set; }
        public DateTime? ProximaClase { get; set; }
        
        public decimal? EncuestaPromedio { get; set; }
        public int? EncuestaCantidad { get; set; }
        public string EncuestaUltimoComentario { get; set; }

        public string UsuarioWeb { get; set; }
        public string ContraseniaWeb { get; set; }
        public DateTime? FechaInscritoWeb { get; set; }
    }

    /// Autor: Joseph Llanque
    /// Fecha: 24/02/2026
    /// Versión: 1.0
    /// <summary>
    /// DTO resultado del endpoint CargarActividadPorTab.
    /// Contiene las actividades agrupadas por nombre de tab y la cantidad total.
    /// </summary>
    public class CargarActividadPorTabResultadoDTO
    {
        public Dictionary<string, List<ActividadAgendaPlanificacionDTO>> ActividadesAgenda { get; set; }
        public int Cantidad { get; set; }
    }

    public class InformacionFaltanteDocenteDTO
    {
        public string Email { get; set; }
        public decimal? EncuestaPromedio { get; set; }
        public int? EncuestaCantidad { get; set; }
        public string EncuestaUltimoComentario { get; set; }
        public int CantidadEstudiantes { get; set; }
        public List<WhatsAppHistorialDocenteDTO> HistorialWhatsApp { get; set; }
        public List<CorreoResumenDocenteDTO> HistorialCorreos { get; set; }
        public UltimaComunicacionResumenDTO UltimaComunicacion { get; set; }
    }

    public class WhatsAppHistorialDocenteDTO
    {
        public string Numero { get; set; }
        public int Tipo { get; set; }
        public int SubTipo { get; set; }
        public int IdPais { get; set; }
        public string Mensaje { get; set; }
        public int? IdPersonal { get; set; }
        public int? IdProveedor { get; set; }
        public int? Registro { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string NombrePersonal { get; set; }
        public int? EstadoMensaje { get; set; }
    }

    public class UltimaComunicacionResumenDTO
    {
        public DateTime? Fecha { get; set; }
        public string Tipo { get; set; }
        public string Direccion { get; set; }
    }

    public class CorreoResumenDocenteDTO
    {
        public int IdMandrilEnvioCorreoGestion { get; set; }
        public string Asunto { get; set; }
        public DateTime? FechaEnvio { get; set; }
        public string PersonalRemitente { get; set; }
        public int? IdCentroCosto { get; set; }
        public bool EsRelacionadoAlCurso { get; set; }
    }

    public class CorreoDetalleDocenteDTO
    {
        public int IdMandrilEnvioCorreoGestion { get; set; }
        public string Asunto { get; set; }
        public DateTime? FechaEnvio { get; set; }
        public int? EstadoEnvio { get; set; }
        public string PersonalRemitente { get; set; }
        public string CorreoDestinatario { get; set; }
        public int? IdCentroCosto { get; set; }
    }
}
