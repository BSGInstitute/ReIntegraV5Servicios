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
        public List<ReporteDashboardCentroCostoItemDTO> CentrosCosto { get; set; } = new List<ReporteDashboardCentroCostoItemDTO>();
    }

    /// <summary>
    /// DTO para item de combo de programas especificos
    /// </summary>
    public class ReporteDashboardProgramaEspecificoItemDTO
    {
        public int? Id { get; set; }
        public string? Nombre { get; set; }
    }

    /// <summary>
    /// DTO para item de combo de centros de costo
    /// </summary>
    public class ReporteDashboardCentroCostoItemDTO
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
    }

    /// <summary>
    /// DTO para resumen semanal de sesiones
    /// </summary>
    public class ReporteDashboardSemanalDTO
    {
        public int Anio { get; set; }
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
        public int? IdProgramaEspecificoPadre { get; set; }
        public int? IdCentroCostoPadre { get; set; }
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

    /// <summary>
    /// DTO para resumen de sesiones por estado (grafico pie/donut)
    /// Estados: Ejecutada, Cancelada, Por-Reprogramar, Adicional, Por Ejecutar, No Aplica, Recuperada
    /// </summary>
    public class ReporteDashboardEstadoSesionDTO
    {
        public int IdEstadoSesion { get; set; }
        public string? EstadoSesion { get; set; }
        public int CantidadSesiones { get; set; }
        public decimal Porcentaje { get; set; }
    }

    /// <summary>
    /// DTO para detalle de sesiones filtradas por estado
    /// </summary>
    public class ReporteDashboardSesionDetalleDTO
    {
        public int IdProgramaEspecifico { get; set; }
        public string? ProgramaEspecifico { get; set; }
        public string? EstadoPrograma { get; set; }
        public string? CentroCosto { get; set; }
        public int IdPEspecificoSesion { get; set; }
        public DateTime? Fecha { get; set; }
        public string? DiaSemana { get; set; }
        public string? Horario { get; set; }
        public decimal Duracion { get; set; }
        public int IdEstadoSesion { get; set; }
        public string? EstadoSesion { get; set; }
        public string? Docente { get; set; }
        public string? Sede { get; set; }
        public string? Aula { get; set; }
        public string? Modalidad { get; set; }
        public int NumeroSesion { get; set; }
    }

    /// <summary>
    /// DTO para evolucion mensual de estados de sesion (grafico de lineas)
    /// </summary>
    public class ReporteDashboardEvolucionEstadoSesionDTO
    {
        public int Mes { get; set; }
        public string? NombreMes { get; set; }
        public int IdEstadoSesion { get; set; }
        public string? EstadoSesion { get; set; }
        public int CantidadSesiones { get; set; }
    }

    /// <summary>
    /// DTO para KPIs de estados de sesion
    /// </summary>
    public class ReporteDashboardKPIsEstadoSesionDTO
    {
        public int TotalSesiones { get; set; }
        public int SesionesEjecutadas { get; set; }
        public int SesionesCanceladas { get; set; }
        public int SesionesPorReprogramar { get; set; }
        public int SesionesAdicionales { get; set; }
        public int SesionesPorEjecutar { get; set; }
        public int SesionesNoAplica { get; set; }
        public int SesionesRecuperadas { get; set; }
        public decimal PorcentajeEjecutadas { get; set; }
        public decimal PorcentajeCanceladas { get; set; }
    }

    /// <summary>
    /// DTO para grafico de cambios de estado basados en log
    /// Transiciones: Lanzamiento->Ejecucion, Ejecucion->Concluido, *->Cancelado
    /// </summary>
    public class ReporteDashboardCambioEstadoDTO
    {
        public int Anio { get; set; }
        public int NumeroSemana { get; set; }
        public DateTime? FechaInicioSemana { get; set; }
        public DateTime? FechaFinSemana { get; set; }
        public int LanzamientoAEjecucion { get; set; }
        public int EjecucionAConcluido { get; set; }
        public int ACancelado { get; set; }
        public int EsSemanaActual { get; set; }
    }

    /// <summary>
    /// DTO para grafico de estados de programas hijo por dia o semana
    /// </summary>
    public class ReporteDashboardEstadoPorDiaDTO
    {
        public int? Anio { get; set; }
        public int? Mes { get; set; }
        public string? NombreMes { get; set; }
        public int? NumeroSemana { get; set; }
        public DateTime? FechaReferencia { get; set; }
        public DateTime? Fecha { get; set; }
        public string? DiaSemana { get; set; }
        public string? Estado { get; set; }
        public int CantidadProgramas { get; set; }
        public int CantidadSesiones { get; set; }
    }

    /// <summary>
    /// DTO para detalle de cursos V3 con modalidad clasificada (Inhouse/Presencial/Online)
    /// </summary>
    public class ReporteDashboardCursoV3DTO
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
        public string? ModalidadClasificada { get; set; }
        public string? Observacion { get; set; }
    }

    /// <summary>
    /// DTO para seguimiento de clases por dia de semana (Lunes a Sabado)
    /// Estados: Programada(5), Ejecutada(1), Cancelada(2), Reprogramada(3)
    /// </summary>
    public class ReporteDashboardSeguimientoClaseDTO
    {
        public int NroDiaSemana { get; set; }
        public string? DiaSemana { get; set; }
        public DateTime? Fecha { get; set; }
        public int Programadas { get; set; }
        public int Ejecutadas { get; set; }
        public int Canceladas { get; set; }
        public int Reprogramadas { get; set; }
        public int TotalSesiones { get; set; }
    }

    /// <summary>
    /// DTO para request del seguimiento de clases (filtro propio)
    /// </summary>
    public class ReporteDashboardSeguimientoFiltroRequestDTO
    {
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string? EstadoCurso { get; set; }
        public int? Anio { get; set; }
        public int? SemanaInicio { get; set; }
        public int? SemanaFin { get; set; }
        public string? Modalidad { get; set; }
        public int? IdProgramaPadre { get; set; }
        public int? IdCurso { get; set; }
    }

    // ── Dashboard 2: Seguimiento por Docente ─────────────────────────────────

    /// <summary>
    /// DTO para item de docente en el filtro desplegable (Dashboard 2)
    /// </summary>
    public class ReporteDashboardDocenteFiltroDTO
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? RazonSocial { get; set; }
    }

    /// <summary>
    /// DTO para item de PEspecifico en el filtro de busqueda (Dashboard 2)
    /// </summary>
    public class ReporteDashboardPEspecificoFiltroDTO
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Estado { get; set; }
        public string? Tipo { get; set; }
    }

    /// <summary>
    /// DTO para cursos (programas especificos hijo) en el filtro de seguimiento
    /// </summary>
    public class ReporteDashboardCursoFiltroDTO
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
    }

    /// <summary>
    /// DTO para item de pais en el filtro del dashboard
    /// </summary>
    public class ReporteDashboardPaisFiltroDTO
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
    }

    /// <summary>
    /// DTO para KPIs generales del seguimiento de docente (RS1 del SP22)
    /// </summary>
    public class ReporteDashboardSeguimientoDocenteKPIsDTO
    {
        public int? IdDocente { get; set; }
        public string? Docente { get; set; }
        public int TotalProgramas { get; set; }
        public int TotalSesiones { get; set; }
        public int SesionesEjecutadas { get; set; }
        public int SesionesCanceladas { get; set; }
        public int SesionesReprogramadas { get; set; }
        public int SesionesProgramadas { get; set; }
        public decimal PorcentajeEjecutadas { get; set; }
    }

    /// <summary>
    /// DTO para resumen por programa del seguimiento de docente (RS2 del SP22)
    /// </summary>
    public class ReporteDashboardSeguimientoDocenteProgramaDTO
    {
        public int IdPEspecifico { get; set; }
        public string? ProgramaGeneral { get; set; }
        public string? Programa { get; set; }
        public string? CentroCosto { get; set; }
        public string? EstadoPrograma { get; set; }
        public int TotalSesiones { get; set; }
        public int SesionesEjecutadas { get; set; }
        public int SesionesCanceladas { get; set; }
        public int SesionesReprogramadas { get; set; }
        public int SesionesProgramadas { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public decimal PorcentajeEjecutadas { get; set; }
    }

    /// <summary>
    /// DTO para detalle de sesiones del seguimiento de docente (RS3 del SP22)
    /// </summary>
    public class ReporteDashboardSeguimientoDocenteSesionDTO
    {
        public int IdPEspecificoSesion { get; set; }
        public int IdPEspecifico { get; set; }
        public string? ProgramaGeneral { get; set; }
        public string? Programa { get; set; }
        public string? CentroCosto { get; set; }
        public string? EstadoPrograma { get; set; }
        public DateTime? Fecha { get; set; }
        public string? DiaSemana { get; set; }
        public TimeSpan? HoraInicio { get; set; }
        public TimeSpan? HoraFin { get; set; }
        public string? EstadoSesion { get; set; }
        public int IdPEspecificoSesionEstado { get; set; }
        public int NroSesion { get; set; }
        public string? Docente { get; set; }
        public string? Sede { get; set; }
        public string? Aula { get; set; }
    }

    /// <summary>
    /// DTO compuesto para retornar los 3 result sets del seguimiento de docente
    /// </summary>
    public class ReporteDashboardSeguimientoDocenteDTO
    {
        public ReporteDashboardSeguimientoDocenteKPIsDTO KPIs { get; set; } = new();
        public List<ReporteDashboardSeguimientoDocenteProgramaDTO> Programas { get; set; } = new();
        public List<ReporteDashboardSeguimientoDocenteSesionDTO> Sesiones { get; set; } = new();
    }

    // ── PEspecifico filtrado por Docente (IdProveedor) ───────────────────────

    /// <summary>
    /// DTO para programas específicos filtrados por docente (idProveedor)
    /// </summary>
    public class ReporteDashboardPEspecificoPorDocenteDTO
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
    }

    // ── Notas por PEspecifico (SP_PW_ListadoNotaProcesarOnline) ─────────────

    /// <summary>Raw row from SP RS1: evaluaciones/criterios</summary>
    public class ReporteDashboardNotaEvaluacionRawDTO
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public decimal Porcentaje { get; set; }
    }

    /// <summary>Raw row from SP RS2: notas agregadas por alumno y criterio</summary>
    public class ReporteDashboardNotaRawDTO
    {
        public int IdMatriculaCabecera { get; set; }
        public int IdCriterioEvaluacion { get; set; }
        public decimal Nota { get; set; }
    }

    /// <summary>Raw row from SP RS3: detalle de notas (por entregable)</summary>
    public class ReporteDashboardNotaDetalleRawDTO
    {
        public int IdMatriculaCabecera { get; set; }
        public int IdCriterioEvaluacion { get; set; }
        public decimal Nota { get; set; }
    }

    /// <summary>Raw row from SP RS4: matriculas</summary>
    public class ReporteDashboardMatriculaRawDTO
    {
        public int IdMatriculaCabecera { get; set; }
        public string? CodigoMatricula { get; set; }
        public string? Alumno { get; set; }
        public int GrupoCurso { get; set; }
    }

    /// <summary>Raw row from SP RS5: sesiones presenciales</summary>
    public class ReporteDashboardSesionRawDTO
    {
        public int IdPEspecificoSesion { get; set; }
    }

    /// <summary>Raw row from SP RS6: asistencias</summary>
    public class ReporteDashboardAsistenciaRawDTO
    {
        public int IdMatriculaCabecera { get; set; }
        public int IdPEspecificoSesion { get; set; }
        public bool Asistio { get; set; }
    }

    /// <summary>Raw row from SP RS7: escala de calificacion</summary>
    public class ReporteDashboardEscalaRawDTO
    {
        public decimal EscalaCalificacion { get; set; }
        public bool EsOnline { get; set; }
    }

    /// <summary>Criterio de evaluacion con su nota calculada (columna dinamica)</summary>
    public class ReporteDashboardNotaCriterioDTO
    {
        public int IdEvaluacion { get; set; }
        public string? NombreCriterio { get; set; }
        public decimal Porcentaje { get; set; }
        public decimal Nota { get; set; }
    }

    /// <summary>Fila de alumno con sus notas por criterio, estado y observacion chatbot</summary>
    public class ReporteDashboardNotaAlumnoDTO
    {
        public int IdMatriculaCabecera { get; set; }
        public string? CodigoMatricula { get; set; }
        public string? Alumno { get; set; }
        public string? CoordinadoraAcademica { get; set; }
        public string? CentroCosto { get; set; }
        public string? Curso { get; set; }
        public string? EstadoMatricula { get; set; }
        public List<ReporteDashboardNotaCriterioDTO> Notas { get; set; } = new();
        public decimal PromedioFinal { get; set; }
        public string? EstadoNotaAlumno { get; set; }
        public string? ObservacionChatbot { get; set; }
    }

    /// <summary>Criterio de evaluacion para encabezado de columna</summary>
    public class ReporteDashboardNotaEvaluacionDTO
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public decimal Porcentaje { get; set; }
    }

    /// <summary>Fila padre — datos del programa para la tabla anidada de notas</summary>
    public class ReporteDashboardNotaProgramaDTO
    {
        public int IdPEspecifico { get; set; }
        public string? Programa { get; set; }
        public string? EstadoPrograma { get; set; }
        public string? ModalidadPrograma { get; set; }
        public string? CentroCostoProg { get; set; }
        public string? Curso { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaTermino { get; set; }
        public string? EstadoRegistroNotas { get; set; }
        public string? SubEstadoRegistroNotas { get; set; }
        public string? Docente { get; set; }
        public string? Coordinadora { get; set; }
        public string? ObservacionChatbot { get; set; }
        public int ConteoChatbot { get; set; }
        public DateTime? FechaRegistroNota { get; set; }
        public int? PlazoCalificacion { get; set; }
        public int TotalAlumnos { get; set; }
        public int TotalCriterios { get; set; }
        public int AlumnosConNotaCompleta { get; set; }
    }

    /// <summary>Raw row del SP RS1 (datos del programa)</summary>
    public class ReporteDashboardNotaProgramaRawDTO
    {
        public int IdPEspecifico { get; set; }
        public string? Programa { get; set; }
        public string? EstadoPrograma { get; set; }
        public string? ModalidadPrograma { get; set; }
        public string? CentroCostoProg { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaTermino { get; set; }
        public string? EstadoRegistroNotas { get; set; }
        public string? SubEstadoRegistroNotas { get; set; }
        public string? Docente { get; set; }
        public string? Coordinadora { get; set; }
        public string? ObservacionChatbot { get; set; }
        public int ConteoChatbot { get; set; }
        public DateTime? FechaRegistroNota { get; set; }
        public int? PlazoCalificacion { get; set; }
        public int TotalAlumnos { get; set; }
        public int TotalCriterios { get; set; }
        public int AlumnosConNotaCompleta { get; set; }
    }

    /// <summary>Raw row del SP RS2 (alumno con notas pivoteadas)</summary>
    public class ReporteDashboardNotaAlumnoRawDTO
    {
        public int IdMatriculaCabecera { get; set; }
        public string? CodigoMatricula { get; set; }
        public string? NombreAlumno { get; set; }
        public string? CoordinadoraAcademica { get; set; }
        public string? CentroCosto { get; set; }
        public string? Curso { get; set; }
        public string? EstadoMatricula { get; set; }
        public decimal? NotaCriterio1 { get; set; }
        public decimal? NotaCriterio2 { get; set; }
        public decimal? NotaCriterio3 { get; set; }
        public decimal? NotaCriterio4 { get; set; }
        public decimal? NotaCriterio5 { get; set; }
        public string? EstadoNotaAlumno { get; set; }
        public string? ObservacionChatbot { get; set; }
    }

    /// <summary>Respuesta completa de notas por PEspecifico (estructura anidada)</summary>
    public class ReporteDashboardNotasPorPEspecificoDTO
    {
        public ReporteDashboardNotaProgramaDTO? Programa { get; set; }
        public List<ReporteDashboardNotaEvaluacionDTO> Evaluaciones { get; set; } = new();
        public List<ReporteDashboardNotaAlumnoDTO> Alumnos { get; set; } = new();
        public bool EsOnline { get; set; }
    }

    /// <summary>
    /// DTO para KPIs de programas agrupados por estado
    /// Usado por SP_ReporteDashboardObtenerResumenProgramas
    /// </summary>
    public class ReporteDashboardResumenProgramasDTO
    {
        public string? Estado { get; set; }
        public int CantidadProgramas { get; set; }
    }

    /// <summary>
    /// DTO para KPIs de cursos agrupados por estado
    /// Usado por SP_ReporteDashboardObtenerResumenCursos
    /// </summary>
    public class ReporteDashboardResumenCursosDTO
    {
        public string? Estado { get; set; }
        public int CantidadCursos { get; set; }
    }

    /// <summary>
    /// DTO para distribucion de programas por modalidad
    /// Usado por SP_ReporteDashboardObtenerModalidadProgramas
    /// </summary>
    public class ReporteDashboardModalidadProgramasDTO
    {
        public string? Modalidad { get; set; }
        public int CantidadProgramas { get; set; }
        public int CantidadSesiones { get; set; }
        public decimal Porcentaje { get; set; }
    }

    /// <summary>
    /// DTO para distribucion de cursos por modalidad
    /// Usado por SP_ReporteDashboardObtenerModalidadCursos
    /// </summary>
    public class ReporteDashboardModalidadCursosDTO
    {
        public string? Modalidad { get; set; }
        public int CantidadCursos { get; set; }
        public int CantidadSesiones { get; set; }
        public decimal Porcentaje { get; set; }
    }

    /// <summary>
    /// DTO para grafico de evolucion mensual de programas
    /// Usado por SP_ReporteDashboardObtenerGraficoPorMesProgramas
    /// </summary>
    public class ReporteDashboardGraficoPorMesProgramasDTO
    {
        public int Anio { get; set; }
        public int Mes { get; set; }
        public string? NombreMes { get; set; }
        public string? EstadoPadre { get; set; }
        public int CantidadProgramas { get; set; }
        public int CantidadSesiones { get; set; }
    }

    /// <summary>
    /// DTO para grafico de evolucion mensual de cursos
    /// Usado por SP_ReporteDashboardObtenerGraficoPorMesCursos
    /// </summary>
    public class ReporteDashboardGraficoPorMesCursosDTO
    {
        public int Anio { get; set; }
        public int Mes { get; set; }
        public string? NombreMes { get; set; }
        public string? EstadoCurso { get; set; }
        public int CantidadCursos { get; set; }
        public int CantidadSesiones { get; set; }
    }

    /// <summary>Programa General para el filtro en cascada del Dashboard Seguimiento Docente</summary>
    public class ReporteDashboardPGeneralFiltroDTO
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
    }

    /// <summary>Fila del RS2 del SP1 FiltrarProgramasCalificacion (IdPEspecifico + FechaTermino)</summary>
    public class ReporteDashboardCalificacionFiltroResultDTO
    {
        public int IdPEspecifico { get; set; }
        public DateTime? FechaTermino { get; set; }
    }

    /// <summary>Respuesta paginada de Calificación de Alumnos</summary>
    public class ReporteDashboardCalificacionAlumnosDTO
    {
        public int TotalRegistros { get; set; }
        public int Pagina { get; set; }
        public int TamanoPagina { get; set; }
        public int TotalPaginas => TamanoPagina > 0 ? (int)Math.Ceiling((double)TotalRegistros / TamanoPagina) : 0;
        public List<ReporteDashboardNotasPorPEspecificoDTO> Programas { get; set; } = new();
    }

    /// <summary>Raw row RS2 del SP unificado (programa con docente y coordinadora ya resueltos)</summary>
    public class ReporteDashboardCalificacionProgramaRawDTO
    {
        public int IdPEspecifico { get; set; }
        public string? Programa { get; set; }
        public string? EstadoPrograma { get; set; }
        public string? ModalidadPrograma { get; set; }
        public string? CentroCostoProg { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaTermino { get; set; }
        public string? EstadoRegistroNotas { get; set; }
        public string? SubEstadoRegistroNotas { get; set; }
        public string? Docente { get; set; }
        public string? Coordinadora { get; set; }
        public int TotalAlumnos { get; set; }
        public int TotalCriterios { get; set; }
        public int AlumnosConNota { get; set; }
        public DateTime? FechaRegistroNota { get; set; }
        public int? PlazoCalificacion { get; set; }
        public string? ObservacionChatbot { get; set; }
        public int? ConteoChatbot { get; set; }
    }

    /// <summary>Raw row RS3 del SP unificado (alumno con notas)</summary>
    public class ReporteDashboardCalificacionAlumnoRawDTO
    {
        public int IdPEspecifico { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public string? CodigoMatricula { get; set; }
        public string? NombreAlumno { get; set; }
        public string? CoordinadoraAcademica { get; set; }
        public string? CentroCosto { get; set; }
        public string? Curso { get; set; }
        public string? EstadoMatricula { get; set; }
        public decimal? NotaCriterio1 { get; set; }
        public decimal? NotaCriterio2 { get; set; }
        public decimal? NotaCriterio3 { get; set; }
        public decimal? NotaCriterio4 { get; set; }
        public decimal? NotaCriterio5 { get; set; }
        public string? EstadoNotaAlumno { get; set; }
        public string? ObservacionChatbot { get; set; }
    }

    /// <summary>Raw row RS4 del SP unificado (criterio de evaluación)</summary>
    public class ReporteDashboardCalificacionCriterioRawDTO
    {
        public int IdPEspecifico { get; set; }
        public int IdEvaluacion { get; set; }
        public string? NombreCriterio { get; set; }
        public decimal Porcentaje { get; set; }
        public int Orden { get; set; }
    }
}
