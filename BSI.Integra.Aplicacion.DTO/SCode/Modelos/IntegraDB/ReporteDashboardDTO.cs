using System;
using System.Collections.Generic;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    /// <summary>
    /// DTO para los KPIs principales del dashboard
    /// </summary>
    public class ReporteDashboardResumenDTO
    {
        public int TotalProgramasPadre { get; set; }
        public int TotalProgramasHijo { get; set; }
        public int ProgramasLanzamiento { get; set; }
        public int ProgramasEjecucion { get; set; }
        public int ProgramasFinalizados { get; set; }
        public int TotalDocentes { get; set; }
        public int DocentesActivos { get; set; }
        public int TotalCoordinadores { get; set; }
        public int TotalSesiones { get; set; }
    }

    /// <summary>
    /// DTO para grafico de distribucion por estado
    /// </summary>
    public class ReporteDashboardEstadoDTO
    {
        public string? Estado { get; set; }
        public int CantidadProgramas { get; set; }
    }

    /// <summary>
    /// DTO para grafico de distribucion por modalidad
    /// </summary>
    public class ReporteDashboardModalidadDTO
    {
        public string? Modalidad { get; set; }
        public int CantidadProgramas { get; set; }
        public int CantidadSesiones { get; set; }
        public decimal Porcentaje { get; set; }
    }

    /// <summary>
    /// DTO para listado de programas en grilla
    /// </summary>
    public class ReporteDashboardProgramaDTO
    {
        public int? IdProgramaEspecificoPadre { get; set; }
        public string? CentroCostoPadre { get; set; }
        public string? ProgramaEspecificoPadre { get; set; }
        public string? EstadoPadre { get; set; }
        public string? Modalidad { get; set; }
        public DateTime? FechaInicio { get; set; }
        public string? Ciudad { get; set; }
        public string? AreaCapacitacion { get; set; }
        public string? SubAreaCapacitacion { get; set; }
    }

    /// <summary>
    /// DTO para detalle de cursos/sesiones
    /// </summary>
    public class ReporteDashboardCursoDTO
    {
        public string? CentroCostoPadre { get; set; }
        public string? ProgramaEspecificoPadre { get; set; }
        public string? CentroCostoHijo { get; set; }
        public string? Curso { get; set; }
        public string? EstadoSesion { get; set; }
        public string? NroSesion { get; set; }
        public DateTime? Fecha { get; set; }
        public string? DiaSemana { get; set; }
        public string? Horario { get; set; }
        public string? Docente { get; set; }
        public string? Sede { get; set; }
        public string? Aula { get; set; }
        public string? Coordinador { get; set; }
    }

    /// <summary>
    /// DTO para asignacion de docentes
    /// </summary>
    public class ReporteDashboardDocenteDTO
    {
        public string? Docente { get; set; }
        public int? IdDocente { get; set; }
        public int? DocenteActivo { get; set; }
        public int ProgramasAsignados { get; set; }
        public int CursosAsignados { get; set; }
        public int TotalSesiones { get; set; }
        public DateTime? PrimeraClase { get; set; }
        public DateTime? UltimaClase { get; set; }
    }

    /// <summary>
    /// DTO para grafico de programas por mes
    /// </summary>
    public class ReporteDashboardGraficoPorMesDTO
    {
        public int Mes { get; set; }
        public string? NombreMes { get; set; }
        public string? EstadoPadre { get; set; }
        public int CantidadProgramas { get; set; }
        public int CantidadSesiones { get; set; }
    }

    /// <summary>
    /// DTO para los valores de los combos de filtros
    /// </summary>
    public class ReporteDashboardFiltrosDTO
    {
        public List<int> Anios { get; set; } = new List<int>();
        public List<string> Estados { get; set; } = new List<string>();
        public List<string> Modalidades { get; set; } = new List<string>();
        public List<string> Areas { get; set; } = new List<string>();
        public List<string> Ciudades { get; set; } = new List<string>();
        public List<ReporteDashboardProgramaEspecificoItemDTO> ProgramasEspecificos { get; set; } = new List<ReporteDashboardProgramaEspecificoItemDTO>();
        public List<string> CentrosCosto { get; set; } = new List<string>();
    }

    /// <summary>
    /// DTO para item de combo de programas especificos
    /// </summary>
    public class ReporteDashboardProgramaEspecificoItemDTO
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
    }

    /// <summary>
    /// DTO para resumen semanal de sesiones
    /// </summary>
    public class ReporteDashboardSemanalDTO
    {
        public int Semana { get; set; }
        public DateTime? FechaInicioSemana { get; set; }
        public DateTime? FechaFinSemana { get; set; }
        public int TotalSesiones { get; set; }
        public int ProgramasActivos { get; set; }
        public int DocentesActivos { get; set; }
        public int SesionesPendientes { get; set; }
        public int SesionesRealizadas { get; set; }
        public int SesionesCanceladas { get; set; }
        public int SesionesReprogramadas { get; set; }
    }

    /// <summary>
    /// DTO para datos completos del dashboard (exportacion)
    /// </summary>
    public class ReporteDashboardCompletoDTO
    {
        public string? CentroCostoPadre { get; set; }
        public string? ProgramaEspecificoPadre { get; set; }
        public string? EstadoPadre { get; set; }
        public string? CentroCostoHijo { get; set; }
        public string? ProgramaEspecificoHijo { get; set; }
        public string? EstadoHijo { get; set; }
        public string? ModalidadHijo { get; set; }
        public int? Anio { get; set; }
        public string? SemanaCalendario { get; set; }
        public DateTime? Fecha { get; set; }
        public string? Horario { get; set; }
        public string? Sede { get; set; }
        public string? Aula { get; set; }
        public string? NroSesion { get; set; }
        public string? Docente { get; set; }
        public string? Coordinador { get; set; }
        public string? NroAmbientesProgramados { get; set; }
        public int? NroAmbientesDisponibles { get; set; }
        public string? NombreModalidad { get; set; }
        public string? AreaCapacitacion { get; set; }
        public string? SubAreaCapacitacion { get; set; }
        public string? Ciudad { get; set; }
        public string? DiaSemana { get; set; }
    }

    /// <summary>
    /// DTO para recibir los filtros desde el frontend
    /// </summary>
    public class ReporteDashboardFiltroRequestDTO
    {
        public int? Anio { get; set; }
        public string? Estado { get; set; }
        public string? Modalidad { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string? Area { get; set; }
        public string? Ciudad { get; set; }
        public string? ProgramaPadre { get; set; }
        public int? IdProgramaEspecificoPadre { get; set; }
        public string? CentroCostoPadre { get; set; }
    }

    /// <summary>
    /// DTO para datos de sesiones en calendario
    /// </summary>
    public class ReporteDashboardCalendarioDTO
    {
        public DateTime? Fecha { get; set; }
        public string? DiaSemana { get; set; }
        public int SemanaCalendario { get; set; }
        public int CantidadSesiones { get; set; }
        public string? Programas { get; set; }
    }
}
