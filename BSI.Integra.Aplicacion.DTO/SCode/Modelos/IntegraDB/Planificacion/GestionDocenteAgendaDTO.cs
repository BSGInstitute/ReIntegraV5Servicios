using System;
using System.Collections.Generic;

namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion
{
    /// <summary>
    /// Configuración de tab de agenda de planificación.
    /// Resultado del JOIN entre T_AgendaTab y T_AgendaTabConfiguracionPlanificacion.
    /// </summary>
    public class AgendaTabConfiguracionPlanificacionAlternoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool VisualizarActividad { get; set; }
        public bool CargarInformacionInicial { get; set; }
        public string VistaBaseDatos { get; set; }
        public string CamposVista { get; set; }
        public string IdFaseGestionContacto { get; set; }
        public string IdEstadoGestionContacto { get; set; }
        public string CodigoAreaTrabajo { get; set; }
        public int Numeracion { get; set; }
        public bool ValidarFecha { get; set; }
    }

    /// <summary>
    /// DTO unificado para las actividades de la agenda de planificación.
    /// Contiene todos los campos posibles de los SPs configurados en VistaBaseDatos.
    /// Los campos no devueltos por un SP específico quedan null/default.
    /// </summary>
    public class ActividadAgendaPlanificacionDTO
    {
        // Campos comunes (todos los SPs)
        public int IdProveedor { get; set; }
        public string NombreDocente { get; set; }
        public string Celular { get; set; }
        public string Correo { get; set; }
        public int IdGestionContacto { get; set; }
        public int? IdPersonalAsignado { get; set; }
        public string PersonalAsignado { get; set; }

        // Campos de Flujo General (SP_GestionDocenteFlujoGeneralObtener)
        public int? IdFlujo { get; set; }
        public string NombreFlujo { get; set; }
        public string ActividadesCabecera { get; set; }
        public int? NumeroActividades { get; set; }

        // Campos de Todos los Docentes (SP_GestionDocenteTodosObtener)
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
        public decimal? PromedioPuntajeEncuesta { get; set; }
        public int? CantidadEncuestas { get; set; }
        public string UltimoComentarioAlumno { get; set; }
    }

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

    public class DocenteAgendaSesionDTO
    {
        public int IdSesion { get; set; }
        public DateTime FechaHoraInicio { get; set; }
        public decimal Duracion { get; set; }
        public int Grupo { get; set; }
        public string Comentario { get; set; }
    }

    /// <summary>
    /// Tab "Todos los Docentes": docente, curso, programa general, próxima clase,
    /// coordinador, celular, correo, país, ciudad y datos de encuesta.
    /// </summary>
    public class DocenteTodosDTO
    {
        public int IdProveedor { get; set; }
        public string NombreDocente { get; set; }
        public string Celular { get; set; }
        public string Correo { get; set; }
        public string Pais { get; set; }
        public string Ciudad { get; set; }
        public int IdGestionContacto { get; set; }
        public int? IdPersonalAsignado { get; set; }
        public string PersonalAsignado { get; set; }
        public int IdPEspecifico { get; set; }
        public string NombreCurso { get; set; }
        public string CodigoCurso { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaTermino { get; set; }
        public int? IdProgramaGeneral { get; set; }
        public string ProgramaGeneral { get; set; }
        public DateTime? ProximaClase { get; set; }
        public decimal? PromedioPuntajeEncuesta { get; set; }
        public int? CantidadEncuestas { get; set; }
        public string UltimoComentarioAlumno { get; set; }
    }

    /// <summary>
    /// Tab "Flujos Generales": docente, flujo asignado, actividades cabecera (nombres y conteo),
    /// celular, correo y coordinador.
    /// </summary>
    public class DocenteFlujoGeneralDTO
    {
        public int IdProveedor { get; set; }
        public string NombreDocente { get; set; }
        public string Celular { get; set; }
        public string Correo { get; set; }
        public int IdGestionContacto { get; set; }
        public int IdFlujo { get; set; }
        public string NombreFlujo { get; set; }
        public string ActividadesCabecera { get; set; }
        public int NumeroActividades { get; set; }
        public int? IdPersonalAsignado { get; set; }
        public string PersonalAsignado { get; set; }
    }
}
