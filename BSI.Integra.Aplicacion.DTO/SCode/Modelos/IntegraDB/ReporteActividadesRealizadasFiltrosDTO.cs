namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ReporteActividadesRealizadasFiltrosDTO
    {
        public int? IdEstadoOcurrencia { get; set; }
        public int? IdAlumno { get; set; }
        public int IdAsesor { get; set; }
        public List<int>? IdFasesOportunidadOrigen { get; set; }
        public List<int>? IdFasesOportunidadDestino { get; set; }
        public int? IdTipoDato { get; set; }
        public DateTime Fecha { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? EstadoLlamada { get; set; }
        public int? IdTipoCategoriaOrigen { get; set; }
        public int? IdProbabilidadActual { get; set; }
        public int HoraInicio { get; set; }
        public int MinutosInicio { get; set; }
        public int HoraFin { get; set; }
        public int MinutosFin { get; set; }
        public bool EstadoFiltroHora { get; set; }
        public int? EstadoPersonal { get; set; }
        public List<int>? IdEstadoMatricula { get; set; }
        public List<int>? IdSubestadoMatricula { get; set; }
    }

    public class ProcesadoDataActividadesRealizadasDTO
    {
        public int IdActividad { get; set; }
        public string NombreCentroCosto { get; set; }
        public string NombreCompletoContacto { get; set; }
        public string CodigoFaseFinal { get; set; }
        public string NombreTipoDato { get; set; }
        public string NombreOrigen { get; set; }
        public DateTime? FechaProgramada { get; set; }
        public DateTime FechaReal { get; set; }
        public string NombreActividadCabecera { get; set; }
        public string NombreOcurrencia { get; set; }
        public string OcurrenciaPadre { get; set; }
        public string ComentarioActividad { get; set; }
        public string NombreCompletoAsesor { get; set; }
        public int IdAlumno { get; set; }
        public int IdOportunidad { get; set; }
        public string ProbabilidadActual { get; set; }
        public string CodigoFaseOrigen { get; set; }
        public string NombreCategoriaOrigen { get; set; }
        public string EstadoOcurrencia { get; set; }
        public string NombreGrupo { get; set; }

        // Informacion de LLamadas
        public string TiemposDuracionLlamadas { get; set; }
        public double MinutosTotalIntervaleLlamadas { get; set; }
        public double MinutosIntervale { get; set; }
        public double MinutosTotalTimbrado { get; set; }
        public double MinutosTotalContesto { get; set; }
        public double MinutosTotalPerdido { get; set; }
        public double MayorTiempo { get; set; }
        public string TiemposTresCX { get; set; }
        public string EstadosTresCX { get; set; }
        public string FechaLlamada { get; set; }
        public int TotalEjecutadas { get; set; }
        public int TotalNoEjecutadas { get; set; }
        public int TotalAsignacionManual { get; set; }
        public int? IdFaseOportunidadInicial { get; set; }
        public List<NombreGrabacionDTO> NombreGrabacionTresCX { get; set; } = new List<NombreGrabacionDTO>();
        public List<NombreGrabacionDTO> NombreGrabacionIntegra { get; set; } = new List<NombreGrabacionDTO>();
        public bool ExisteLlamadaExitosa { get; set; }

    }
    public class ProcesadoDataActividadesRealizadasAlternoDTO
    {
        public int IdActividad { get; set; }
        public string NombreCentroCosto { get; set; }
        public string NombreCompletoContacto { get; set; }
        public string CodigoFaseFinal { get; set; }
        public string NombreTipoDato { get; set; }
        public string NombreOrigen { get; set; }
        public DateTime? FechaProgramada { get; set; }
        public DateTime FechaReal { get; set; }
        public string NombreActividadCabecera { get; set; }
        public string NombreOcurrencia { get; set; }
        public string ComentarioActividad { get; set; }
        public string NombreCompletoAsesor { get; set; }
        public int IdAlumno { get; set; }
        public int IdOportunidad { get; set; }
        public string ProbabilidadActual { get; set; }
        public string CodigoFaseOrigen { get; set; }
        public string NombreCategoriaOrigen { get; set; }
        public string EstadoOcurrencia { get; set; }
        public string NombreGrupo { get; set; }

        public List<TiempoTresCXDTO> TiemposDuracionLlamadas { get; set; }
        public double MinutosTotalIntervaleLlamadas { get; set; }
        public double MinutosIntervalo { get; set; }
        public double MinutosTotalTimbrado { get; set; }
        public double MinutosTotalContesto { get; set; }
        public double MinutosTotalPerdido { get; set; }
        public double MayorTiempo { get; set; }
        public List<TiempoTresCXDTO> TiemposTresCX { get; set; }
        public List<EstadoTresCXDTO> EstadosTresCX { get; set; }
        public List<FechaLlamadaDTO> FechaLlamada { get; set; }
        public int TotalEjecutadas { get; set; }
        public int TotalNoEjecutadas { get; set; }
        public int TotalAsignacionManual { get; set; }
        public double MinutosPerdidosOcurrencia { get; set; }
        public int? IdFaseOportunidadInicial { get; set; }
        public List<NombreGrabacionAlternoDTO> NombreGrabacionTresCX { get; set; } = new List<NombreGrabacionAlternoDTO>();
        public List<NombreGrabacionAlternoDTO> NombreGrabacionIntegra { get; set; } = new List<NombreGrabacionAlternoDTO>();
        public bool ExisteLlamadaExitosa { get; set; }

    }
    public class TiempoTresCXDTO
    {
        public string? Webphone { get; set; }
        public string TT { get; set; }
        public string TC { get; set; }
        public string OrigenLlamada { get; set; }
    }
    public class EstadoTresCXDTO
    {
        public string Tipo { get; set; }
        public string SubTipo { get; set; }
        public string OrigenLlamada { get; set; }
    }
    public class FechaLlamadaDTO
    {
        public double? MinutosPerdidos { get; set; }
        public string Inicio { get; set; }
        public string Termino { get; set; }
        public string OrigenLlamada { get; set; }
    }
    public class NombreGrabacionDTO
    {
        public string Funcion { get; set; }
        public string NombreGrabacion { get; set; }
        public string OrigenLlamada { get; set; }
        public string Webphone { get; set; }
    }
    public class NombreGrabacionAlternoDTO
    {
        public string Tipo { get; set; }
        public string OrigenLlamada { get; set; }
        public string Webphone { get; set; }
        public string NombreGrabacion { get; set; }
    }
    public class ReporteRealizadaDataDTO
    {
        public int IdActividad { get; set; }
        public string NombreCentroCosto { get; set; }
        public string NombreCompletoContacto { get; set; }
        public string CodigoFaseFinal { get; set; }
        public string NombreTipoDato { get; set; }
        public string NombreOrigen { get; set; }
        public DateTime? FechaProgramada { get; set; }
        public DateTime FechaReal { get; set; }
        public string NombreActividadCabecera { get; set; }
        public string OcurrenciaPadre { get; set; }
        public string NombreOcurrencia { get; set; }
        public string ComentarioActividad { get; set; }
        public string NombreCompletoAsesor { get; set; }
        public int IdAlumno { get; set; }
        public int IdOportunidad { get; set; }
        public string ProbabilidadActual { get; set; }
        public string CodigoFaseOrigen { get; set; }
        public int IdFaseOportunidadInicial { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string NombreCategoriaOrigen { get; set; }
        public string EstadoOcurrencia { get; set; }
        public string NombreGrupo { get; set; }
        public int? IdLlamadaWebphone { get; set; }
        public int? DuracionTimbrado { get; set; }
        public int? DuracionContesto { get; set; }
        public DateTime? FechaInicioLlamada { get; set; }
        public DateTime? FechaFinLlamada { get; set; }
        public string NombreGrabacionIntegra { get; set; }
        public string Webphone { get; set; }
        public int? IdTresCX { get; set; }
        public int? DuracionContestoTresCx { get; set; }
        public int? DuracionTimbradoTresCx { get; set; }
        public DateTime? FechaInicioLlamadaTresCX { get; set; }
        public DateTime? FechaFinLlamadaTresCX { get; set; }
        public string EstadoLlamadaTresCX { get; set; }
        public string SubEstadoLlamadaTresCX { get; set; }
        public string NombreGrabacionTresCX { get; set; }
        public string OrigenLlamada { get; set; }
    }

    public class CompuestoActividadesRealizadasDTO
    {
        public int IdActividad { get; set; }
        public string NombreCentroCosto { get; set; }
        public string NombreCompletoContacto { get; set; }
        public string CodigoFaseFinal { get; set; }
        public string NombreTipoDato { get; set; }
        public string NombreOrigen { get; set; }
        public DateTime? FechaProgramada { get; set; }
        public DateTime FechaReal { get; set; }
        public string NombreActividadCabecera { get; set; }
        public string NombreOcurrencia { get; set; }
        public string OcurrenciaPadre { get; set; }
        public string ComentarioActividad { get; set; }
        public string NombreCompletoAsesor { get; set; }
        public int IdAlumno { get; set; }
        public int IdOportunidad { get; set; }
        public string ProbabilidadActual { get; set; }
        public string CodigoFaseOrigen { get; set; }
        public int IdFaseOportunidadInicial { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string NombreCategoriaOrigen { get; set; }
        public string EstadoOcurrencia { get; set; }
        public string NombreGrupo { get; set; }
        public List<InformacionLlamadaDTO> LlamadasIntegra { get; set; }
        public List<InformacionLlamadaDTO> LlamadasCentral { get; set; }
    }
    public class CompuestoActividadesRealizadasAlternoDTO
    {
        public int IdActividad { get; set; }
        public string NombreCentroCosto { get; set; }
        public string NombreCompletoContacto { get; set; }
        public string CodigoFaseFinal { get; set; }
        public string NombreTipoDato { get; set; }
        public string NombreOrigen { get; set; }
        public DateTime? FechaProgramada { get; set; }
        public DateTime FechaReal { get; set; }
        public string NombreActividadCabecera { get; set; }
        public string NombreOcurrencia { get; set; }
        public string ComentarioActividad { get; set; }
        public string NombreCompletoAsesor { get; set; }
        public int IdAlumno { get; set; }
        public int IdOportunidad { get; set; }
        public string ProbabilidadActual { get; set; }
        public string CodigoFaseOrigen { get; set; }
        public int IdFaseOportunidadInicial { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string NombreCategoriaOrigen { get; set; }
        public string EstadoOcurrencia { get; set; }
        public string NombreGrupo { get; set; }
        public List<InformacionLlamadaAlternoDTO> LlamadasIntegra { get; set; }
        public List<InformacionLlamadaAlternoDTO> LlamadasCentral { get; set; }
    }
    public class InformacionLlamadaDTO
    {
        public int? Id { get; set; }
        public int? DuracionTimbrado { get; set; }
        public int? DuracionContesto { get; set; }
        public DateTime? FechaInicioLlamada { get; set; }
        public DateTime? FechaFinLlamada { get; set; }
        public string EstadoLlamada { get; set; }
        public string SubEstadoLlamada { get; set; }
        public string NombreGrabacion { get; set; }
        public string MinutosPerdidos { get; set; }
        public string Webphone { get; set; }
        public string OrigenLlamada { get; set; }
    }
    public class InformacionLlamadaAlternoDTO
    {
        public int? Id { get; set; }
        public int? DuracionTimbrado { get; set; }
        public int? DuracionContesto { get; set; }
        public DateTime? FechaInicioLlamada { get; set; }
        public DateTime? FechaFinLlamada { get; set; }
        public string EstadoLlamada { get; set; }
        public string SubEstadoLlamada { get; set; }
        public string? NombreGrabacion { get; set; }
        public double? MinutosPerdidos { get; set; }
        public string Webphone { get; set; }
        public string AnexoCentral { get; set; }
        public string TelefonoDestinoReal { get; set; }
        public string TelefonoDestino { get; set; }
        public string OrigenLlamada { get; set; }
    }
    public class FiltroReporteActividadRealizadaDTO
    {
        public IEnumerable<DTO.ComboDTO> EstadoOcurrencia { get; set; }
        public List<PersonalAsignadoDTO> Asesores { get; set; }
        public IEnumerable<FaseOportunidadComboDTO> FaseOportunidad { get; set; }
        public IEnumerable<ComboDTO> TipoDato { get; set; }
        public IEnumerable<ComboDTO> Probabilidad { get; set; }
        public IEnumerable<ComboDTO> CategoriaOrigen { get; set; }
        public List<PersonalAsignadoDTO> AsistentesActivos { get; set; }
        public List<PersonalAsignadoDTO> AsistentesTotales { get; set; }
        public List<PersonalAsignadoDTO> AsistentesInactivos { get; set; }
        public List<FiltroConfiguracionCoordinadoraEstadoMatriculaDTO> EstadoMatricula { get; set; }
        public List<SubEstadoMatriculaFiltroDTO> SubEstadoMatricula { get; set; }
    }
    public class FiltroReporteActividadRealizadaAlternoDTO
    {
        public IEnumerable<DTO.ComboDTO> EstadoOcurrencia { get; set; }
        public List<PersonalAsignadoDTO> Asesores { get; set; }
        public IEnumerable<FaseOportunidadComboDTO> FaseOportunidad { get; set; }
        public IEnumerable<ComboDTO> TipoDato { get; set; }
        public IEnumerable<ComboDTO> Probabilidad { get; set; }
        public IEnumerable<ComboDTO> CategoriaOrigen { get; set; }
    }
    public class FiltroConfiguracionCoordinadoraEstadoMatriculaDTO
    {
        public int IdEstadoMatricula { get; set; }
        public string EstadoMatricula { get; set; }
    }


}
