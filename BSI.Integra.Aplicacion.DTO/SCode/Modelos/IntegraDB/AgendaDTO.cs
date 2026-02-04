namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{



    public class ActividadAgendaDTO
    {
        public int Id { get; set; }
        public string EstadoHoja { get; set; }
        public string CentroCosto { get; set; }
        public string PEspecifico { get; set; }
        public string Modalidad { get; set; }
        public DateTime? FechaPrimeraSesion { get; set; }
        public int? ValidoAccesoTemporal { get; set; }
        public string Contacto { get; set; }
        public string CodigoFase { get; set; }
        public string NombreTipoDato { get; set; }
        public string Origen { get; set; }
        public DateTime? UltimaFechaProgramada { get; set; }
        public int IdClasificacionPersona { get; set; }
        public int IdAlumno { get; set; }
        public string Celular { get; set; }
        public int? IdCodigoPais { get; set; }
        public string Email1 { get; set; }
        public int IdOportunidad { get; set; }
        public string UltimoComentario { get; set; }
        public int IdActividadCabecera { get; set; }
        public bool ReprogramacionManual { get; set; }
        public bool ReprogramacionAutomatica { get; set; }
        public string ActividadCabecera { get; set; }
        public string Asesor { get; set; }
        public int IdPersonal_Asignado { get; set; }
        public int IdCentroCosto { get; set; }
        public int? IdFaseOportunidad { get; set; }
        public int IdTipoDato { get; set; }
        public string ProbabilidadActualDesc { get; set; }
        public string CategoriaNombre { get; set; }
        public string CategoriaDescripcion { get; set; }
        public int? IdCategoriaOrigen { get; set; }
        public int? IdSubCategoriaDato { get; set; }
        public int IdEstadoOportunidad { get; set; }
        public bool ValidaLlamada { get; set; }
        public int? DiasSinContactoManhana { get; set; }
        public int? DiasSinContactoTarde { get; set; }
        public int? ActividadesManhana { get; set; }
        public int? ActividadesTarde { get; set; }
        public int? IdPadre { get; set; }
        public int? IdMatriculaCabecera { get; set; }
        public int? IdEstadoMatricula { get; set; }
        public string CodigoMatricula { get; set; }
        public string DNI { get; set; }
        public int? DiasAtrasoCuotaPago { get; set; }
        public string EstadoMatricula { get; set; }
        public int? GrupoCurso { get; set; }
        public string SubEstadoMatricula { get; set; }
        public int? DiasSeguimiento { get; set; }
        public int? DiasActividadesEjecutadas { get; set; }
        public string Tarifario { get; set; }
        public DateTime? FechaGrabacion { get; set; }
        public DateTime? FechaVerificacion { get; set; }
        public DateTime? FechaSolicitud { get; set; }
        public int? ActividadTotalUltimos7Dias { get; set; }
        public int? ActividadEjecutadaUltimos7Dias { get; set; }
        public int? NumeroDiasActividadesReprogramadas { get; set; }
        public int? TotalDiaActual { get; set; }
        public int? EjecutadasDiaActual { get; set; }
        public string TipoSolicitudOperaciones { get; set; }
        public string? Color { get; set; }
        public int? TipoRegistro { get; set; }
        public bool? EsOnline { get; set; }
        public Dictionary<string, List<ActividadAgendaDTO>> ActividadesAgenda { get; set; }

        // --- Campos adicionales para Solicitudes Agrupadas ---
        // Campos de Programa
        public int? IdPEspecifico { get; set; }
        public int? IdPGeneral { get; set; }
        public string? PGeneral { get; set; }

        // Campos de Solicitud
        public string? Prioridad { get; set; }
        public string? NombreSolicitud { get; set; }
        public int? IdTipoReporte { get; set; }
        public string? TipoReporte { get; set; }
        public int? IdSolicitudCategoria { get; set; }
        public int? IdSubCategoria { get; set; }
        public int? IdEstadoSolicitud { get; set; }

        // Campos de Solicitante
        public int? IdSolicitante { get; set; }
        public string? NombreSolicitante { get; set; }
        public int? IdAreaSolicitante { get; set; }
        public string? AreaSolicitante { get; set; }

        // Campos de Revisión
        public int? IdAreaRevision { get; set; }
        public string? AreaRevision { get; set; }
        public string? NombreArchivoSolicitante { get; set; }

        // Campos de Solución
        public int? IdAreaSolucion { get; set; }
        public string? AreaSolucion { get; set; }
        public int? IdPersonalSolucion { get; set; }
        public string? PersonalSolucion { get; set; }
        public string? ComentarioSolucion { get; set; }
        public string? NombreArchivoSolucion { get; set; }

        // Otros campos de solicitud
        public DateTime? FechaModificacionSolicitud { get; set; }
        public int? IdControlSolicitudOrigen { get; set; }
        public string? ControlSolicitudOrigen { get; set; }
        //GrabacionCentral
        public string? GrabacionCentral { get; set; }
    }

    public class ActividadAgendaV2DTO
    {
        public string EstadoHoja { get; set; }
        public string CentroCosto { get; set; }
        public string PEspecifico { get; set; }
        public string Modalidad { get; set; }
        public DateTime? FechaPrimeraSesion { get; set; }
        public int? ValidoAccesoTemporal { get; set; }
        public string Contacto { get; set; }
        public string CodigoFase { get; set; }
        public string NombreTipoDato { get; set; }
        public string Origen { get; set; }
        public DateTime? UltimaFechaProgramada { get; set; }
        public string Celular { get; set; }
        public int? IdCodigoPais { get; set; }
        public string Email1 { get; set; }
        public string UltimoComentario { get; set; }
        public bool ReprogramacionManual { get; set; }
        public bool ReprogramacionAutomatica { get; set; }
        public string ActividadCabecera { get; set; }
        public string Asesor { get; set; }
        public string ProbabilidadActualDesc { get; set; }
        public string CategoriaNombre { get; set; }
        public string CategoriaDescripcion { get; set; }
        public bool ValidaLlamada { get; set; }
        public int? DiasSinContactoManhana { get; set; }
        public int? DiasSinContactoTarde { get; set; }
        public int? ActividadesManhana { get; set; }
        public int? ActividadesTarde { get; set; }
        public string CodigoMatricula { get; set; }
        public string DNI { get; set; }
        public int? DiasAtrasoCuotaPago { get; set; }
        public string EstadoMatricula { get; set; }
        public int? GrupoCurso { get; set; }
        public string SubEstadoMatricula { get; set; }
        public int? DiasSeguimiento { get; set; }
        public int? DiasActividadesEjecutadas { get; set; }
        public string Tarifario { get; set; }
        public DateTime? FechaGrabacion { get; set; }
        public DateTime? FechaVerificacion { get; set; }
        public DateTime? FechaSolicitud { get; set; }
        public int? ActividadTotalUltimos7Dias { get; set; }
        public int? ActividadEjecutadaUltimos7Dias { get; set; }
        public int? NumeroDiasActividadesReprogramadas { get; set; }
        public int? TotalDiaActual { get; set; }
        public int? EjecutadasDiaActual { get; set; }
        public string TipoSolicitudOperaciones { get; set; }
        public string? Color { get; set; }
        public int? TipoRegistro { get; set; }
        public bool? EsOnline { get; set; }
        public Dictionary<string, List<ActividadAgendaV2DTO>> ActividadesAgendaV2 { get; set; }
    }
    public class ActividadesPorFiltroRespuestaDTO
    {
        public Dictionary<string, List<ActividadAgendaDTO>> ActividadesAgenda { get; set; } = new Dictionary<string, List<ActividadAgendaDTO>>();
        public int CantidadRN2 { get; set; }
    }
    public class CompuestoActividadesEjecutadasTempDTO
    {
        public int TotalOportunidades { get; set; }
        public int Id { get; set; }
        public string CentroCosto { get; set; }
        public string Contacto { get; set; }
        public string CodigoFase { get; set; }
        public string NombreTipoDato { get; set; }
        public string Origen { get; set; }
        public DateTime? FechaProgramada { get; set; }
        public DateTime? FechaReal { get; set; }
        public Nullable<int> Duracion { get; set; }
        public string Actividad { get; set; }
        public string Ocurrencia { get; set; }
        public string Comentario { get; set; }
        public string Asesor { get; set; }
        public int IdContacto { get; set; }
        public int IdOportunidad { get; set; }
        public string ProbActual { get; set; }
        public string Ca_nombre { get; set; }
        public int IdCategoria { get; set; }
        public string TiempoLlamadas { get; set; }
        public string FaseMaxima { get; set; }
        public string FaseInicial { get; set; }
        public string NumeroLlamadas { get; set; }
        public string DuracionTimbrado { get; set; }
        public string DuracionContesto { get; set; }
        public string EstadoLlamada { get; set; }
        public string UnicoTimbrado { get; set; }
        public string UnicoContesto { get; set; }
        public string UnicoEstadoLlamada { get; set; }
        public string Estado { get; set; }
        public string EstadoClasificacion { get; set; }
        public string UnicoClasificacion { get; set; }
        public DateTime? UnicoFechaLlamada { get; set; }
        public List<CompuestoActividadesEjecutadasTemp_DetalleDTO> lista { get; set; }
        public List<CompuestoActividadesEjecutadasTemp_DetalleDTO> llamadasTresCX { get; set; }
        public double MinutosIntervale { get; set; }
        public double MinutosTotalTimbrado { get; set; }
        public double MinutosTotalContesto { get; set; }
        public double MinutosTotalPerdido { get; set; }
        public double MayorTiempo { get; set; }
        public string TiemposTresCX { get; set; }
        public string EstadosTresCX { get; set; }
        public string NombreGrupo { get; set; }
        public string FechaLlamada { get; set; }
        public int TotalEjecutadas { get; set; }
        public int TotalNoEjecutadas { get; set; }
        public int TotalAsignacionManual { get; set; }
        public int? IdFaseOportunidadInicial { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string NombreGrabacionTresCX { get; set; }
        public string NombreGrabacionIntegra { get; set; }
    }
    public class CompuestoActividadesEjecutadasTemp_DetalleDTO
    {
        public int? Id { get; set; }
        public string DuracionTimbrado { get; set; }
        public string DuracionContesto { get; set; }
        public string EstadoLlamada { get; set; }
        public DateTime? FechaLlamada { get; set; }
        public string EstadoClasificacion { get; set; }
        public DateTime? FechaLlamadaFin { get; set; }
        public string SubEstadoLlamada { get; set; }
        public string NombreGrabacion { get; set; }
    }
    public class ObtenerRealizadasRespuestaDTO
    {
        public List<CompuestoActividadesEjecutadasTempDTO> Records { get; set; }
        public int Total { get; set; }
    }
    public class AgendaDTO
    {
        //public int IdAsesor { get; set; }
        public int? DiferenciaHoraria { get; set; }
        public int IdTab { get; set; }
        public bool ValidacionTabs { get; set; }
        //public string AreaTrabajo { get; set; }
        //public Dictionary<string, string> Filtros { get; set; } = new Dictionary<string, string>();
        //public int CantidadRN2 { get; set; }
        public List<CompuestoActividadEjecutadaDTO> ActividadesRealizadas { get; set; } = new List<CompuestoActividadEjecutadaDTO>();
        //public Dictionary<string, List<ActividadAgendaDTO>> ActividadesAgenda { get; set; } = new Dictionary<string, List<ActividadAgendaDTO>>();
        public Dictionary<string, List<ActividadAgendaDTO>> ActividadesAgendaOperaciones { get; set; } = new Dictionary<string, List<ActividadAgendaDTO>>();
        public Dictionary<string, bool> HabilitarEstados { get; set; } = new Dictionary<string, bool>();
        public string LogCarlos { get; set; }
    }
    public class CursoAsignadoDTO
    {
        public int IdPEspecificoPadre { get; set; }
        public int IdPEspecifico { get; set; }
        public string NombrePEspecifico { get; set; }
    }
    public class ProgramaAsignadoDTO
    {
        public int IdPEspecifico { get; set; }
        public string NombrePEspecifico { get; set; }
        public int? IdCentroCosto { get; set; }
        public string EstadoP { get; set; }
        public string Modalidad { get; set; }
        public int? IdPGeneral { get; set; }
        public string Ciudad { get; set; }
        public int? IdCursoMoodle { get; set; }
        public int? IdCursoMoodlePrueba { get; set; }
        public int TipoPEspecifico { get; set; }
    }
    public class ControlActividadAgendaDTO
    {
        public int Totales { get; set; }
        public int Ejecutadas { get; set; }
        public int ItsGenerados { get; set; }
        public int IpsGenerados { get; set; }
    }

    public class ActividadesAsesorHistoricoDTO
    {
        public int Id { get; set; }
        public int IdAsesor { get; set; }
        public DateTime Fecha { get; set; }
        public int TotalActividades { get; set; }
        public int Ejecutadas { get; set; }
        public int ItsGenerados { get; set; }
        public int IpsGenerados { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }

    public class ResultadoBusquedaFichaAlumnoDTO
    {
        public string? TabAgenda { get; set; }
        public int IdOportunidad { get; set; }
        public int IdActividadDetalle { get; set; }
        public int IdAlumno { get; set; }
        public string Alumno { get; set; }
        public int IdFaseOportunidad { get; set; }
        public string FaseOportunidad { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int IdPersonalAsignado { get; set; }
        public string PersonalAsignado { get; set; }
        public int? IdAgente { get; set; }
        public int? IdSkill { get; set; }
    }
    public class ColorPerfilProgramaDTO
    {
        public int IdRegistro { get; set; }
        public string TipoRegistro { get; set; }
        public string ColorHex { get; set; }
        public string ColorDescripcion { get; set; }
    }

    public class MetricaComparativaDTO
    {
        public int Hoy { get; set; }
        public int Ayer { get; set; }
        public int Porcentaje { get; set; }
        public string Estado { get; set; }
    }

    public class MetricasDTO
    {
        public MetricaComparativaDTO TotalActividades { get; set; }
        public MetricaComparativaDTO Ejecutadas { get; set; }
        public MetricaComparativaDTO ItsGenerados { get; set; }
        public MetricaComparativaDTO IpsGenerados { get; set; }
    }

    public class MetricasComparativasDiariasDTO
    {
        public bool Success { get; set; }
        public string Fecha { get; set; }
        public string FechaComparacion { get; set; }
        public int IdAsesor { get; set; }
        public MetricasDTO Metricas { get; set; }
    }
}
