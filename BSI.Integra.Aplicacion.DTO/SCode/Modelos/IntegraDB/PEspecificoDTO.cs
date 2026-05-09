using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class PEspecificoDTO
    {
        public int Id { get; set; }
        [StringLength(300)]
        public string Nombre { get; set; } = null!;
        [StringLength(100)]
        public string? Codigo { get; set; }
        public int? IdCentroCosto { get; set; }
        [StringLength(100)]
        public string? Frecuencia { get; set; }
        [StringLength(100)]
        public string EstadoP { get; set; } = null!;
        [StringLength(100)]
        public string Tipo { get; set; } = null!;
        [StringLength(50)]
        public string? TipoAmbiente { get; set; }
        [StringLength(50)]
        public string? Categoria { get; set; } = null!;
        public int? IdProgramaGeneral { get; set; }
        [StringLength(50)]
        public string Ciudad { get; set; } = null!;
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaTermino { get; set; }
        [StringLength(50)]
        public string? FechaInicioV { get; set; }
        [StringLength(50)]
        public string? FechaTerminoV { get; set; }
        [StringLength(20)]
        public string? CodigoBanco { get; set; }
        [StringLength(50)]
        public string? FechaInicioP { get; set; }
        [StringLength(50)]
        public string? FechaTerminoP { get; set; }
        public int? FrecuenciaId { get; set; }
        public int? EstadoPid { get; set; }
        public int? TipoId { get; set; }
        public int? CategoriaId { get; set; }
        public short? OrigenPrograma { get; set; }
        public int? IdCiudad { get; set; }
        [StringLength(20)]
        public string? CoordinadoraAcademica { get; set; }
        [StringLength(20)]
        public string? CoordinadoraCobranza { get; set; }
        [StringLength(10)]
        public string? Duracion { get; set; }
        [StringLength(1)]
        public string? ActualizacionAutomatica { get; set; }
        public int? IdCursoMoodle { get; set; }
        public bool? CursoIndividual { get; set; }
        public int? IdSesionInicio { get; set; }
        public int? IdExpositorReferencia { get; set; }
        public int? IdAmbiente { get; set; }
        [StringLength(255)]
        public string? UrlDocumentoCronograma { get; set; }
        public int? IdEstadoPespecifico { get; set; }
        [StringLength(255)]
        public string? UrlDocumentoCronogramaGrupos { get; set; }
        public int? IdTroncalPartner { get; set; }
        public int? IdCursoMoodlePrueba { get; set; }
        public int? IdEstadoCupos { get; set; }
        public int? IdCursoRa { get; set; }
        public int? IdProveedor { get; set; }
        public int? IdProveedorCalificaProyecto { get; set; }
        public string? ObservacionCursoFinalizado { get; set; }
        public int? IdTipoProgramaCarrera { get; set; }
        public bool? ResumenClaseActivo { get; set; }
        public bool? TutorVirtualActivo { get; set; } 
    }
    public class PEspecificoPGeneralFiltroDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public int? IdProgramaGeneral { get; set; }
    }
    public class PEspecificoRelacionadoFiltroDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Modalidad { get; set; }
        public string Codigo { get; set; }
    }
    public class PEspecificoPorIdCentroCostoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string EstadoP { get; set; } = null!;
        public string Tipo { get; set; } = null!;
        public string? TipoAmbiente { get; set; }
        public string Categoria { get; set; } = null!;
        public int? IdProgramaGeneral { get; set; }
        public string Ciudad { get; set; } = null!;
        public int? EstadoPid { get; set; }
        public int? TipoId { get; set; }
        public int? IdCiudad { get; set; }
        public string? Duracion { get; set; }
        public bool? CursoIndividual { get; set; }
        public int? IdSesionInicio { get; set; }
        public int? IdExpositorReferencia { get; set; }
        public int? IdAmbiente { get; set; }
        public string? UrlDocumentoCronograma { get; set; }
        public DateTime? FechaHoraInicio { get; set; }
        public string? UrlDocumentoCronogramaGrupos { get; set; }
    }
    public class PEspecificoPorIdPGeneral
    {
        public int Id { get; set; }
        public int IdCentroCosto { get; set; }
        public string Nombre { get; set; }
        public string CentroCosto { get; set; }
        public string Ciudad { get; set; }
        public string Tipo { get; set; }
        public string Duracion { get; set; }
        public int? EstadoPId { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaInicio { get; set; }
        public string FechaInicioTexto { get; set; }
        public string Frecuencia { get; set; }
        public int IdCategoria { get; set; }
        public string CodigoBanco { get; set; }
    }
    public class FechaInicioProgramaEspecificoDTO
    {
        public int IdPEspecifico { get; set; }
        public string NombrePGeneral { get; set; }
        public string NombrePEspecifico { get; set; }
        public string Ciudad { get; set; }
        public string Tipo { get; set; }
        public string TipoCiudad { get; set; }
        public string Duracion { get; set; }
        public string Tiempo { get; set; }
        public string FechaHoraInicio { get; set; }
        public int IdEstadoPEspecifico { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int IdCategoria { get; set; }
    }
    public class SeccionEtiquetaDTO
    {
        public string Valor { get; set; }
        public Guid? IdPlantillaPW { get; set; }
        public Guid? IdSeccionPW { get; set; }
        public int? IdCentroCosto { get; set; }
    }
    public class PEspecificoInformacionDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public int? IdCentroCosto { get; set; }
        public string EstadoP { get; set; }
        public string Tipo { get; set; }
        public string Categoria { get; set; }
        public string CodigoBanco { get; set; }
        public string Ciudad { get; set; }
        public int IdProgramaGeneral { get; set; }
    }
    public class PEspecificoIdTipoDTO
    {
        public int TipoId { get; set; }
    }
    public class PEspecificoValorDTO
    {
        public int IdModalidadCurso { get; set; }
        public int IdCiudad { get; set; }
        public string MonedaCronograma { get; set; }
    }
    public class ProgramaEspecificoSesionDetalleDTO
    {
        public int IdPEspecifico { get; set; }
        public int IdPEspecificoSesion { get; set; }
        public string NombrePEspecifico { get; set; }
        public DateTime FechaSesion { get; set; }
        public string HorarioSesion { get; set; }
        public int DuracionSesionHoras { get; set; }
    }
    public class PEspecificoNuevoAulaVirtualDTO
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
        public bool Estado { get; set; }
        public int TipoPEspecifico { get; set; }
        public int? IdPEspecificoPadre { get; set; }
        public int IdPEspecificoHijo { get; set; }
        public string NombrePEspecificoHijo { get; set; }
        public string NombrePEspecificoPadre { get; set; }
    }
    public class CongelamientoPEspecificoMatriculaAlumnoDTO
    {
        public int Id { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public int IdPEspecifico { get; set; }
        public bool EstadoPadre { get; set; }
        public string Nombre { get; set; }
        public int IdProgramaGeneral { get; set; }
        public List<EsquemaEvaluacionCongeladoListadoDTO> EsquemasEvaluacion { get; set; }
    }
    public class PEspecificoComboDTO
    {
        public int IdPEspecifico { get; set; }
        public string Nombre { get; set; }
    }
    public class CursosCentroCostoDTO
    {
        public int Id { get; set; }
        public int IdPEspecifico { get; set; }
        public string NombreCursoEspecifico { get; set; }
        public int Duracion { get; set; }
        public int Orden { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string NombrePEspecifico { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public int Estado { get; set; }

    }
    public class PEspecificoProgramaGeneralFiltroDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int? IdPGeneral { get; set; }
    }

    public class PEspecificoFiltroSPDTO
    {
        public string? IdProgramaEspecifico { get; set; }
        public string? IdCentroCosto { get; set; }
        public string? CodigoBs { get; set; }
        public string? IdEstadoPEspecifico { get; set; }
        public string? IdModalidadCurso { get; set; }
        public string? IdPGeneral { get; set; }
        public string? IdArea { get; set; }
        public string? IdSubArea { get; set; }
        public int IdCentroCostoD { get; set; }
    }
    public class ProgramaEspecificoPadreIndividualDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string CodigoBanco { get; set; }
        public string Codigo { get; set; }
        public int? IdCentroCosto { get; set; }
        public string EstadoP { get; set; }
        public string Tipo { get; set; }
        public int? IdProgramaGeneral { get; set; }
        public string Ciudad { get; set; }
        public int? EstadoPId { get; set; }
        public int IdEstadoCupos { get; set; }
        public int? TipoId { get; set; }
        public short? OrigenPrograma { get; set; }
        public int? IdCiudad { get; set; }
        public string Duracion { get; set; }
        public string ActualizacionAutomatica { get; set; }
        public int? IdCursoMoodle { get; set; }
        public bool? CursoIndividual { get; set; }
        public int? IdExpositor_Referencia { get; set; }
        public int? IdAmbiente { get; set; }
        public string TipoAmbiente { get; set; }
        public string UrlDocumentoCronograma { get; set; }
        public string UrlDocumentoCronogramaM { get; set; }
        public string UrlDocumentoCronogramaB { get; set; }
        public string UrlDocumentoCronogramaI { get; set; }
        public string UrlDocumentoCronogramaGrupos { get; set; }
        public string UrlDocumentoCronogramaGruposM { get; set; }
        public string UrlDocumentoCronogramaGruposB { get; set; }
        public string UrlDocumentoCronogramaGruposI { get; set; }
        public string TipoSesion { get; set; }
        public int? IdCursoMoodlePrueba { get; set; }
        public string TipoProgramaGeneral { get; set; }
        public int? IdTipoProgramaCarrera { get; set; }
        public bool? ResumenClaseActivo { get; set; }
        public bool? TutorVirtualActivo { get; set; }

    }
    public class PEspecificoModuloComboDTO
    {
        public IEnumerable<ComboDTO> Producto { get; set; }
        public IEnumerable<ProveedorProductoDTO> Proveedor { get; set; }
        public IEnumerable<ComboDTO> ProveedorCurso { get; set; }
        public IEnumerable<ComboDTO> ProductoPresentacion { get; set; }
        public IEnumerable<ComboDTO> ProgramaGeneral { get; set; }
        public IEnumerable<ComboDTO> CentroCosto { get; set; }
        public IEnumerable<ComboDTO> Modalidad { get; set; }
        public IEnumerable<LocacionTroncalDTO> LocacionTroncal { get; set; }
        public IEnumerable<AmbienteCiudadFiltroDTO> Ambiente { get; set; }
        public IEnumerable<ComboDTO> Origen { get; set; }
        public IEnumerable<LocacionComboDTO> Locacion { get; set; }
        public IEnumerable<ComboDTO> Expositor { get; set; }
        public IEnumerable<ComboDTO> Frecuencia { get; set; }
        public IEnumerable<ComboDTO> EstadoPEspecifico { get; set; }
        public IEnumerable<ComboDTO> PersonalAreaTrabajo { get; set; }
        public IEnumerable<ComboDTO> Ciudad { get; set; }
        public IEnumerable<ComboDTO> Ciclo { get; set; }
        public IEnumerable<ComboDTO> PeriodoLectivo { get; set; }
        public IEnumerable<ComboDTO> CiudadBS { get; set; }
        public IEnumerable<AreaCapacitacionFiltroDTO> AreaCapacitacion { get; set; }
        public IEnumerable<SubAreaCapacitacionFiltroDTO> SubAreaCapacitacion { get; set; }
        public IEnumerable<PGeneralSubAreaFiltroDTO> ProgramaGeneralP { get; set; }
        public IEnumerable<PEspecificoPGeneralFiltroDTO> ProgramaEspecifico { get; set; }
        public IEnumerable<PEspecificoPGeneralFiltroDTO> ProgramaEspecificoHijos { get; set; }
        public IEnumerable<CentroCostoPEspecificoFiltroDTO> CentroCostoP { get; set; }
        public IEnumerable<PEspecificoRelacionadoFiltroDTO> ProgramaEspecificoWebinar { get; set; }
        public IEnumerable<PlantillaTipoEnvioDTO> PlantillaCorreo { get; set; }
        public IEnumerable<PlantillaTipoEnvioDTO> PlantillaWhatsApp { get; set; }
        public IEnumerable<TiempoFrecuenciaDTO> TiempoFrecuencia { get; set; }
        public IEnumerable<ComboDTO> Dias { get; set; }
    }
    public class DatosConfiguracionProgramasWebexDTO
    {
        public int IdPEspecifico { get; set; }
        public int IdTiempoFrecuencia { get; set; }
        public int Valor { get; set; }
        public int IdTiempoFrecuenciaCorreo { get; set; }
        public int ValorFrecuenciaCorreo { get; set; }
        public int IdPlantillaFrecuenciaCorreo { get; set; }
        public int IdTiempoFrecuenciaWhatsapp { get; set; }
        public int ValorFrecuenciaWhatsapp { get; set; }
        public int IdPlantillaFrecuenciaWhatsapp { get; set; }
        public int IdTiempoFrecuenciaCorreoConfirmacion { get; set; }
        public int ValorFrecuenciaCorreoConfirmacion { get; set; }
        public int IdPlantillaCorreoConfirmacion { get; set; }
        public int IdTiempoFrecuenciaCorreoDocente { get; set; }
        public int ValorFrecuenciaDocente { get; set; }
        public int IdPlantillaDocente { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int IdFrecuencia { get; set; }
    }
    public class DatosProgramaEspecificoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Duracion { get; set; }
        public int? IdCiudad { get; set; }
        public string Tipo { get; set; }
        public string TipoAmbiente { get; set; }
        public int? IdSesion_Inicio { get; set; }
    }
    public class RegistroProgramaEspecificoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public int? IdCentroCosto { get; set; }
        public string EstadoP { get; set; }
        public string Tipo { get; set; }
        public int? IdProgramageneral { get; set; }
        public string Ciudad { get; set; }
        public string UrlDocumentoCronograma { get; set; }
        public bool? CursoIndividual { get; set; }
    }
    public class PEspecificoGeneracionAutomaticaDTO
    {
        public int IdProgramaGeneral { get; set; }
        public string Modalidad { get; set; }
        public int? IdCiudad { get; set; }
        public string NombreCiudad { get; set; }
        public int Anio { get; set; }
    }
    public class DatosPGeneralDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public int IdArea { get; set; }
        public int IdSubArea { get; set; }
        public int IdCategoria { get; set; }
        public int IdTipoPrograma { get; set; }
    }
    public class CategoriaCiudadDTO
    {
        public int Id { get; set; }
        public int IdCategoriaPrograma { get; set; }
        public int? IdCiudad { get; set; }
        public string TroncalCompleto { get; set; }
        public int IdRegionCiudad { get; set; }
    }
    public class FiltroInsertarPEspecificoDTO
    {
        public PEspecificoDTO Pespecifico { get; set; }
        public CentroCostoDTO? CentroCosto { get; set; }
        public int IdCiudad { get; set; }
        public int? idPespecificoAdicional { get; set; }
    }
    public class InsertarActualizarModuloWebinaDTO
    {
        public int Id { get; set; }
        public int IdPespecifico { get; set; }
        public int IdPespecificoPadre { get; set; }
    }
    public class DatosProgramaEspecificoDuracionDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Duracion { get; set; }
        public int? IdProgramaGeneral { get; set; }
    }
    public class ParametrosInsertaFrecuenciaDTO
    {
        public List<PespecificoFrecuenciaDetalleDTO> listaDetalles { get; set; }
        public List<PespecificoFrecuenciaDetalleDTO> listaDetallesWebinar { get; set; }
        public int IdPespecifico { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int IdFrecuencia { get; set; }
        public DateTime? FechaInicioWebinar { get; set; }
        public int? IdFrecuenciaWebinar { get; set; }
        public List<int> ListaPEspecificos { get; set; }
        public int? IdTiempoFrecuencia { get; set; }
        public int? ValorTiempoFrecuencia { get; set; }
        public int? IdTiempoFrecuenciaCorreoConfirmacion { get; set; }
        public int? ValorFrecuenciaCorreoConfirmacion { get; set; }
        public int? IdTiempoFrecuenciaCorreo { get; set; }
        public int? ValorFrecuenciaCorreo { get; set; }
        public int? IdTiempoFrecuenciaWhatsapp { get; set; }
        public int? ValorFrecuenciaWhatsapp { get; set; }
        public int? IdPlantillaFrecuenciaCorreo { get; set; }
        public int? IdPlantillaFrecuenciaWhatsapp { get; set; }
        public int? IdPlantillaCorreoConfirmacion { get; set; }
        public int? IdTiempoFrecuenciaCorreoDocente { get; set; }
        public int? ValorFrecuenciaDocente { get; set; }
        public int? IdPlantillaDocente { get; set; }
        public bool? CheckTiempoFrecuencia { get; set; }
        public bool? CheckEnvioCorreo { get; set; }
        public bool? CheckEnvioWhatsApp { get; set; }
        public bool? CheckEnvioCorreoConfirmacion { get; set; }
        public bool? CheckEnvioCorreoDocente { get; set; }
    }
    public class FiltroObtenerPDFDTO
    {
        public int IdPespecifico { get; set; }
        public bool CursoIndividual { get; set; }
        public string CursoNombre { get; set; }
        public int Grupo { get; set; }
        public List<PespecificoSesionCompuestoDTO> Sesion { get; set; }
    }
    public class ReporteAmbienteDTO
    {
        public string CentroCostoPadre { get; set; }
        public string ProgramaEspecificoPadre { get; set; }
        public string EstadoPadre { get; set; }
        public string CentroCostoHijo { get; set; }
        public string ProgramaEspecificoHijo { get; set; }
        public string EstadoHijo { get; set; }
        public string ModalidadHijo { get; set; }
        public string Anio { get; set; }
        public string SemanaCalendario { get; set; }
        public string Fecha { get; set; }
        public string Horario { get; set; }
        public string Sede { get; set; }
        public string Aula { get; set; }
        public string NroSesión { get; set; }
        public string Docente { get; set; }
        public string Coordinador { get; set; }
        public string NroAmbientesProgramados { get; set; }
        public string NroAmbientesDisponibles { get; set; }
    }
    public class FiltroReporteAmbienteDTO
    {
        public string IdProgramaEspecifico { get; set; }
        public string IdCentroCosto { get; set; }
        public string CodigoBS { get; set; }
        public string IdEstadoPEspecifico { get; set; }
        public string IdModalidadCurso { get; set; }
        public string IdPGeneral { get; set; }
        public string IdArea { get; set; }
        public string IdSubArea { get; set; }
    }
    public class DatosListaPespecificoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string CodigoBanco { get; set; }
        public string Codigo { get; set; }
        public int? IdCentroCosto { get; set; }
        public string EstadoP { get; set; }
        public string Tipo { get; set; }
        public int? IdProgramaGeneral { get; set; }
        public string Ciudad { get; set; }
        public int? EstadoPId { get; set; }
        public int? TipoId { get; set; }
        public short? OrigenPrograma { get; set; }
        public int? IdCiudad { get; set; }
        public string Duracion { get; set; }
        public string ActualizacionAutomatica { get; set; }
        public int? IdCursoMoodle { get; set; }
        public bool? CursoIndividual { get; set; }
        public int? IdExpositor_Referencia { get; set; }
        public int? IdAmbiente { get; set; }
        public string TipoAmbiente { get; set; }
        public string UrlDocumentoCronograma { get; set; }
        public string UrlDocumentoCronogramaM { get; set; }
        public string UrlDocumentoCronogramaB { get; set; }
        public string UrlDocumentoCronogramaI { get; set; }
        public string UrlDocumentoCronogramaGrupos { get; set; }
        public string UrlDocumentoCronogramaGruposM { get; set; }
        public string UrlDocumentoCronogramaGruposB { get; set; }
        public string UrlDocumentoCronogramaGruposI { get; set; }
        public string TipoSesion { get; set; }
        public int? IdCursoMoodlePrueba { get; set; }
        public string TipoProgramaGeneral { get; set; }
    }
    public class FiltroSesionEspecialDTO
    {
        public int PEspecificoPadreId { get; set; }
        public string Nombre { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Duracion { get; set; }
        public int Grupo { get; set; }
    }
    public class DocenteAmbientePEspecificoDTO
    {
        public int Id { get; set; }
        public int? IdExpositor_Referencia { get; set; }
        public int? IdProveedor { get; set; }
        public string Duracion { get; set; }
        public int? IdAmbiente { get; set; }
        public int? IdEstadoPEspecifico { get; set; }
        public int? IdEstadoCupos { get; set; }
        public int? IdModalidadCurso { get; set; }
        public int? IdCursoMoodle { get; set; }
        public int? IdCursoMoodlePrueba { get; set; }
        public int? IdCiclo { get; set; }
        public int? IdPeriodoLectivo { get; set; }
    }
    public class PEspecificoSesionFechasDTO
    {
        public int Id { get; set; }
        public int IdPEspecifico { get; set; }
        public DateTime FechaHoraInicio { get; set; }
        public DateTime FechaHoraFin { get; set; }
    }
    public class PEspecificoGrupoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdPEspecificoPadre { get; set; }
    }
    public class ProgramaEspecificoMaterialDTO
    {
        public int IdProgramaEspecifico { get; set; }
        public string ProgramaEspecifico { get; set; }
        public string EstadoProgramaEspecifico { get; set; }
        public string Modalidad { get; set; }
        public string Ciudad { get; set; }
    }
    public class ProgramaEspecificoMaterialFiltroDTO
    {
        public string? IdProgramaEspecifico { get; set; }
        public string? IdCentroCosto { get; set; }
        public string? CodigoBs { get; set; }
        public string? IdEstadoPEspecifico { get; set; }
        public string? IdModalidadCurso { get; set; }
        public string? IdPGeneral { get; set; }
        public string? IdArea { get; set; }
        public string? IdSubArea { get; set; }
        public int? IdCentroCostoD { get; set; }
    }
    public class PEspecificoFurDetalleDTO
    {
        public int IdPEspecifico { get; set; }
        public int Id { get; set; }
        public string Codigo { get; set; }
        public int IdProducto { get; set; }
        public int IdProveedor { get; set; }
        public decimal? Monto { get; set; }
        public decimal? Cantidad { get; set; }
    }
    public class CiudadSesionDTO
    {
        public int IdPais { get; set; }
        public string NombrePais { get; set; }
        public string? ZonaHoraria { get; set; }
        public string Ciudad { get; set; }
    }

    public class PespecificoCentroCostoDTO
    {
        public int? IdCentroCosto { get; set; }
    }



    public class ProgramasAsignadosDTO
    {
        public int? IdPEspecificoPadre { get; set; }
        public string NombrePEspecificoPadre { get; set; }
    }
    public class CursosAsignadosDTO
    {
        public int? IdPEspecificoPadre { get; set; }
        public int IdPEspecifico { get; set; }
        public string NombrePEspecifico { get; set; }
    }

    public class accesoPortalDTO
    {
        public List<ProgramasAsignadosDTO> ProgramasAsignados { get; set; }
        public List<CursosAsignadosDTO> CursosAsignados { get; set; }
    }

    public class PEspecificoDetalleFechaByPGeneral
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdPGeneral { get; set; }
        public DateTime FechaInicio { get; set; }
    }
	public class PEspecificoByPGeneral
	{
		public int IdPEspecifico { get; set; }
		public string NombrePEspecifico { get; set; }
		public int IdPGeneral { get; set; }
	}

}

