using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class PGeneralDTO
    {
        public int? Id { get; set; }
        public int? IdPgeneral { get; set; }
        [StringLength(600)]
        public string? Nombre { get; set; }
        [StringLength(300)]
        public string? PwImgPortada { get; set; }
        [StringLength(700)]
        public string? PwImgPortadaAlf { get; set; }
        [StringLength(300)]
        public string? PwImgSecundaria { get; set; }
        [StringLength(700)]
        public string? PwImgSecundariaAlf { get; set; }
        public int? IdPartner { get; set; }
        public int? IdArea { get; set; }
        public int? IdSubArea { get; set; }
        public int? IdCategoria { get; set; }
        [StringLength(1)]
        public string? PwEstado { get; set; }
        [StringLength(1)]
        public string? PwMostrarBsplay { get; set; }
        [StringLength(250)]
        public string? PwDuracion { get; set; }
        public int? IdBusqueda { get; set; }
        public int? IdChatZopim { get; set; }
        public string? PgTitulo { get; set; }
        [StringLength(300)]
        public string? Codigo { get; set; }
        [StringLength(255)]
        public string? UrlImagenPortadaFr { get; set; }
        [StringLength(255)]
        public string? UrlBrochurePrograma { get; set; }
        [StringLength(255)]
        public string? UrlPartner { get; set; }
        [StringLength(255)]
        public string? UrlVersion { get; set; }
        [StringLength(500)]
        public string? PwTituloHtml { get; set; }
        public bool? EsModulo { get; set; }
        [StringLength(150)]
        public string? NombreCorto { get; set; }
        public int? IdPagina { get; set; }
        public int? ChatActivo { get; set; }
        [StringLength(500)]
        public string? PwDescripcionGeneral { get; set; }
        public bool? TieneProyectoDeAplicacion { get; set; }
        public int? IdTipoPrograma { get; set; }
        [StringLength(50)]
        public string? CodigoPartner { get; set; }
        [StringLength(150)]
        public string? LogoPrograma { get; set; }
        public string? UrlLogoPrograma { get; set; }
        public DateTime? FechaInicioAsincronico { get; set; }
        public bool? AsignaVenta { get; set; }
        public bool? TieneCertificadoModular { get; set; }
        public bool? CertificadoRequierePago { get; set; }
        public int? IdPgeneralBase { get; set; }
        public int? IdPgeneralPeriodoAsincronico { get; set; }
        public float? CreditosTeoricos { get; set; }
        public float? CreditosPracticos { get; set; }
        public float? CreditosTotales { get; set; }
        public int? HorasTeoricas { get; set; }
        public int? HorasPracticas { get; set; }
        public int? HorasTotales { get; set; }
        public int? IdTipoProgramaCarrera { get; set; }
    }
    public class PGeneralAlternoDTO
    {
        public int Id { get; set; }
        public int? IdPgeneral { get; set; }
        public string? Nombre { get; set; }
        public string? Pw_ImgPortada { get; set; }
        public string? Pw_ImgPortadaAlf { get; set; }
        public string? Pw_ImgSecundaria { get; set; }
        public string? Pw_ImgSecundariaAlf { get; set; }
        public int? IdPartner { get; set; }
        public int? IdArea { get; set; }
        public int? IdSubArea { get; set; }
        public int? IdCategoria { get; set; }
        public string? Pw_estado { get; set; }
        public string? Pw_mostrarBSPlay { get; set; }
        public string? pw_duracion { get; set; }
        public int? IdBusqueda { get; set; }
        public int? IdChatZopim { get; set; }
        public string? Pg_titulo { get; set; }
        public string? Codigo { get; set; }
        public string? UrlImagenPortadaFr { get; set; }
        public string? UrlBrochurePrograma { get; set; }
        public string? UrlPartner { get; set; }
        public string? UrlVersion { get; set; }
        public string? Pw_tituloHtml { get; set; }
        public bool? EsModulo { get; set; }
        public string? NombreCorto { get; set; }
        public int? IdPagina { get; set; }
        public int? ChatActivo { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public string? Pw_DescripcionGeneral { get; set; }
        public bool? TieneProyectoDeAplicacion { get; set; }
        public int? IdTipoPrograma { get; set; }
        public string? CodigoPartner { get; set; }
        public string? LogoPrograma { get; set; }
        public string? UrlLogoPrograma { get; set; }
    }
    public class PGeneralAlternoV2DTO
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? pw_duracion { get; set; }
    }
    public class PGeneralCabeceraSpeechAgendaDTO
    {
        public string? ProgramaGeneral { get; set; }
        public string? Texto { get; set; }
        public string? Color { get; set; }
    }
    public class PGeneralPublicoObjetivoParaAgendaDTO
    {
        public int Id { get; set; }
        public string? Titulo { get; set; }
        public string? Contenido { get; set; }
        public int IdPGeneral { get; set; }
        public int Respuesta { get; set; }
    }
    public class PGeneralAtributosPrincipalesDTO
    {
        public int Id { get; set; }
        public int? IdPgeneral { get; set; }
        public string? Nombre { get; set; }
        public string? PwImgPortada { get; set; }
        public string? PwImgPortadaAlf { get; set; }
        public string? PwImgSecundaria { get; set; }
        public string? PwImgSecundariaAlf { get; set; }
        public int? IdPartner { get; set; }
        public int? IdArea { get; set; }
        public int? IdSubArea { get; set; }
        public int? IdCategoria { get; set; }
        public string? PwEstado { get; set; }
        public string? PwMostrarBSPlay { get; set; }
        public string? PwDuracion { get; set; }
        public int? IdBusqueda { get; set; }
        public int? IdChatZopim { get; set; }
        public string? PgTitulo { get; set; }
        public string? Codigo { get; set; }
        public string? UrlImagenPortadaFr { get; set; }
        public string? UrlBrochurePrograma { get; set; }
        public string? UrlPartner { get; set; }
        public string? UrlVersion { get; set; }
        public string? PwTituloHtml { get; set; }
        public bool? EsModulo { get; set; }
        public string? NombreCorto { get; set; }
        public int? IdPagina { get; set; }
        public int? ChatActivo { get; set; }
    }
    public class PgeneralDocumentoSeccionDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string pw_duracion { get; set; }
        public List<SeccionDocumentoDTO> ListaSeccion { get; set; }
        public List<ProgramaGeneralSeccionAnexosHTMLDTO> ListaSeccionV2 { get; set; }
    }
    public class PGeneralNombreDTO
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
    }
    public class PGeneralCursoRelacionadoDTO
    {
        public string? Nombre { get; set; }
        public string UrlPagina { get; set; } = null!;
    }
    public class PGeneralAreaSubAreaDTO
    {
        public int IdProgramaGeneral { get; set; }
        public int IdArea { get; set; }
        public int IdSubArea { get; set; }
    }
    public class PadrePespecificoHijoDTO
    {
        public int Id { get; set; }
        public int IdPespecificoPadre { get; set; }
        public int IdPespecificoHijo { get; set; }
    }
    public class FrecuenciaProgramaGeneralDTO
    {
        public int IdPEspecifico { get; set; }
        public string Nombre { get; set; }
    }
    public class CursosRelacionadosDTO
    {
        public int IdPgeneral { get; set; }
        public string Nombre { get; set; }
        public string Duracion { get; set; }
        public string Modalidad { get; set; }
        public string Url_Video { get; set; }
        public string Inversion { get; set; }
    }
    public class InformacionProgramaDTO
    {
        public string Titulo { get; set; }
        public string Contenido { get; set; }
    }
    public class ProgramaGeneralDatoDTO
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public int? IdPgeneral { get; set; }
    }
    public class PGeneralPrincipalDTO
    {
        public int Id { get; set; }
        public int? IdPgeneral { get; set; }
        public string Nombre { get; set; }
        public string PwImgPortada { get; set; }
        public string PwImgPortadaAlf { get; set; }
        public string PwImgSecundaria { get; set; }
        public string PwImgSecundariaAlf { get; set; }
        public int? IdPartner { get; set; }
        public int? IdArea { get; set; }
        public int? IdSubArea { get; set; }
        public int? IdCategoria { get; set; }
        public string PwEstado { get; set; }
        public string PwMostrarBsplay { get; set; }
        public string PwDuracion { get; set; }
        public int? IdBusqueda { get; set; }
        public int? IdChatZopim { get; set; }
        public string PgTitulo { get; set; }
        public string Codigo { get; set; }
        public string UrlImagenPortadaFr { get; set; }
        public string UrlBrochurePrograma { get; set; }
        public string UrlPartner { get; set; }
        public string UrlVersion { get; set; }
        public string PwTituloHtml { get; set; }
        public bool? EsModulo { get; set; }
        public string NombreCorto { get; set; }
        public int? IdPagina { get; set; }
        public int? ChatActivo { get; set; }
        public string Usuario { get; set; }
    }

    public class PGeneralPuntoCorteComboDTO
    {
        public IEnumerable<ComboDTO> ListaAreaCapacitacion { get; set; }
        public IEnumerable<SubAreaCapacitacionFiltroDTO> ListaSubAreaCapacitacion { get; set; }
        public List<PGeneralSubAreaCapacitacionFiltroDTO> ListaProgramaGeneral { get; set; }
        public List<ComboDTO> ListaPuntoCorte { get; set; }
    }

    public class PGeneralSubAreaCapacitacionFiltroDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int? IdSubAreaCapacitacion { get; set; }
    }
    public class PGeneralSubAreaFiltroDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdSubArea { get; set; }
    }
    public class PgeneralIdDTO
    {
        public int Id { get; set; }
    }
    public class PGeneralCursoIrcaDTO
    {
        public string NombreCurso { get; set; }
        public string EstadoCurso { get; set; }
    }
    public class PGeneralModeloCertificadoDTO
    {
        public int IdModelo { get; set; }
        public string NombreModelo { get; set; }
        public string UrlModelo { get; set; }
    }
    public class ProgramaSeccionIndividualDTO
    {
        public string Titulo { get; set; }
        public string Contenido { get; set; }
        public int? IdSeccionTipoDetallePW { get; set; }
        public int? NumeroFila { get; set; }
        public string Cabecera { get; set; }
        public string PiePagina { get; set; }
        public int? OrdenWeb { get; set; }
    }
    public class ArchivosAlumnoDTO
    {
        public byte[] registroMemoria { get; set; }
        public string NombreArchivo { get; set; }
    }
    public class InformacionProgramaDocumentosDTO
    {
        public int IdProgramaGeneral { get; set; }
        public string Nombre { get; set; }
        public string UrlBrochurePrograma { get; set; }
        public string Tipo { get; set; }
        public string Ciudad { get; set; }
        public string UrlDocumentoCronograma { get; set; }
        public int IdOportunidad { get; set; }
        public int IdAlumno { get; set; }
        public string CodigoFase { get; set; }
        public int IdActividadDetalle { get; set; }
    }
    public class EstructuraProgramaPortalDTO
    {
        public int IdPGeneral { get; set; }
        public List<ContenidoTituloDTO> EstructuraCurso { get; set; }
    }
    public class ContenidoTituloDTO
    {
        public string Titulo { get; set; }
        public int Duracion { get; set; }
        public List<ContenidoHijoDTO> SubTitulo { get; set; }
    }
    public class ContenidoHijoDTO
    {
        public string Contenido { get; set; }
        public int NumeroFila { get; set; }
        public int Documento { get; set; }
    }
    public class ListaCursosPorProgramaDTO
    {
        public int Id { get; set; }
        public int IdHijo { get; set; }
        public string Curso { get; set; }
    }
    public class PGeneralProblemaDTO
    {
        public int IdProblema { get; set; }
        public string NombreProblema { get; set; }
        public int Respuesta { get; set; }
        public string Completado { get; set; }
        public List<PGeneralArgumentoProblemaDTO> Argumentos { get; set; }
    }
    public class PGeneralArgumentoProblemaDTO
    {
        public int Id { get; set; }
        public int IdProgramaGeneralProblema { get; set; }
        public string Detalle { get; set; }
        public string Solucion { get; set; }
        public bool Solucionado { get; set; }
        public bool Seleccionado { get; set; }
    }
    public class PGeneralHijoDTO
    {
        public int Id { get; set; }
        public int IdPGeneral_Hijo { get; set; }
    }

    public class PgeneralWebinarDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdTipoPrograma { get; set; }
    }
    public class PGeneralDatosDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public int IdArea { get; set; }
        public int IdSubArea { get; set; }
        public int? IdCategoria { get; set; }
    }
    public class ProgramaGeneralTroncalDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public int IdTroncalPatner { get; set; }
        public int Duracion { get; set; }
        public int IdArea { get; set; }
        public int IdSubArea { get; set; }
        public int IdCategoria { get; set; }
        public string NombreCategoria { get; set; }
    }
    public class FiltroProgramaGeneralDTO
    {
        public string? IdPgeneral { get; set; }
        public string? IdArea { get; set; }
        public string? IdSubArea { get; set; }
        public string? IdPartner { get; set; }
    }
    public class CompuestoPerfilContactoProgramaDTO
    {
        public CoeficienteScoringCargoDTO Cargo { get; set; }
        public CoeficienteScoringCiudadDTO Ciudad { get; set; }
        public CoeficienteScoringCategoriaDTO Categoria { get; set; }
        public CoeficienteScoringATrabajoDTO Trabajo { get; set; }
        public CoeficienteScoringAFormacionDTO Formacion { get; set; }
        public CoeficienteScoringModalidadDTO Modalidad { get; set; }
        public CoeficienteScoringIndustriaDTO Industria { get; set; }
        public List<TipoDatoPerFilContactoProgramaDTO> TipoDato { get; set; }
        public List<EscalaProbabilidadDTO> Escala { get; set; }
        public PerfilContactoInterceptoDTO Intercepto { get; set; }
        public int IdPGeneral { get; set; }
    }
    public class CoeficienteScoringCargoDTO
    {
        public List<ScoringCargoProgramaDTO> CargoScoring { get; set; }
        public List<ProgramaGeneralPerfilCoeficienteDTO> CargoCoefiente { get; set; }
    }
    public class CoeficienteScoringCiudadDTO
    {
        public List<ScoringCiudadProgramaDTO> CiudadScoring { get; set; }
        public List<ProgramaGeneralPerfilCoeficienteDTO> CiudadCoefiente { get; set; }
    }
    public class CoeficienteScoringCategoriaDTO
    {
        public List<ScoringCategoriaProgramaDTO> CategoriaScoring { get; set; }
        public List<ProgramaGeneralPerfilCoeficienteDTO> CategoriaCoefiente { get; set; }
    }
    public class CoeficienteScoringATrabajoDTO
    {
        public List<ScoringTrabajoProgramaDTO> TrabajoScoring { get; set; }
        public List<ProgramaGeneralPerfilCoeficienteDTO> TrabajoCoefiente { get; set; }
    }
    public class CoeficienteScoringAFormacionDTO
    {
        public List<ScoringAFormacionProgramaDTO> FormacionScoring { get; set; }
        public List<ProgramaGeneralPerfilCoeficienteDTO> FormacionCoefiente { get; set; }
    }
    public class CoeficienteScoringModalidadDTO
    {
        public List<ScoringModalidadProgramaDTO> ModalidadScoring { get; set; }
        public List<ProgramaGeneralPerfilCoeficienteDTO> ModalidadCoefiente { get; set; }
    }
    public class CoeficienteScoringIndustriaDTO
    {
        public List<ScoringIndustriaProgramaDTO> IndustriaScoring { get; set; }
        public List<ProgramaGeneralPerfilCoeficienteDTO> IndustriaCoefiente { get; set; }
    }
    public class TipoDatoPerFilContactoProgramaDTO
    {
        public int Id { get; set; }
        public int IdTipoDato { get; set; }
    }
    public class ScoringCargoProgramaDTO
    {
        public int Id { get; set; }
        public int IdPGeneral { get; set; }
        public string Nombre { get; set; }
        public int IdCargo { get; set; }
        public int IdSelect { get; set; }
        public int Valor { get; set; }
        public int Fila { get; set; }
        public int Columna { get; set; }
        public bool Validar { get; set; }
    }
    public class ScoringCiudadProgramaDTO
    {
        public int Id { get; set; }
        public int IdPGeneral { get; set; }
        public string Nombre { get; set; }
        public int IdCiudad { get; set; }
        public int IdSelect { get; set; }
        public int Valor { get; set; }
        public int Fila { get; set; }
        public int Columna { get; set; }
        public bool Validar { get; set; }
    }
    public class ScoringCategoriaProgramaDTO
    {
        public int Id { get; set; }
        public int IdPGeneral { get; set; }
        public string Nombre { get; set; }
        public int IdCategoriaOrigen { get; set; }
        public int IdSelect { get; set; }
        public int Valor { get; set; }
        public int Fila { get; set; }
        public int Columna { get; set; }
        public bool Validar { get; set; }
    }
    public class ScoringTrabajoProgramaDTO
    {
        public int Id { get; set; }
        public int IdPGeneral { get; set; }
        public string Nombre { get; set; }
        public int IdAreaTrabajo { get; set; }
        public int IdSelect { get; set; }
        public int Valor { get; set; }
        public int Fila { get; set; }
        public int Columna { get; set; }
        public bool Validar { get; set; }
    }
    public class ScoringAFormacionProgramaDTO
    {
        public int Id { get; set; }
        public int IdPGeneral { get; set; }
        public string Nombre { get; set; }
        public int IdAreaFormacion { get; set; }
        public int IdSelect { get; set; }
        public int Valor { get; set; }
        public int Fila { get; set; }
        public int Columna { get; set; }
        public bool Validar { get; set; }
    }
    public class ScoringModalidadProgramaDTO
    {
        public int Id { get; set; }
        public int IdPGeneral { get; set; }
        public string Nombre { get; set; }
        public int IdModalidad { get; set; }
        public int IdSelect { get; set; }
        public int Valor { get; set; }
        public int Fila { get; set; }
        public int Columna { get; set; }
        public bool Validar { get; set; }
    }
    public class ScoringIndustriaProgramaDTO
    {
        public int Id { get; set; }
        public int IdPGeneral { get; set; }
        public string Nombre { get; set; }
        public int IdIndustria { get; set; }
        public int IdSelect { get; set; }
        public int Valor { get; set; }
        public int Fila { get; set; }
        public int Columna { get; set; }
        public bool Validar { get; set; }
    }
    public class ProgramaGeneralPerfilCoeficienteDTO
    {
        public int Id { get; set; }
        public int IdPGeneral { get; set; }
        public string Nombre { get; set; }
        public double Coeficiente { get; set; }
        public int IdSelect { get; set; }
        public int IdColumna { get; set; }
    }
    public class EscalaProbabilidadDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public double ProbabilidadActual { get; set; }
        public double ProbabilidadInicial { get; set; }
        public int Orden { get; set; }
    }
    public class PerfilContactoInterceptoDTO
    {
        public int Id { get; set; }
        public string PerfilEstado { get; set; }
        public double PerfilIntercepto { get; set; }
    }

    public class ProgramaGeneralDTO
    {
        public PGeneralDTO PGeneral { get; set; }
        public DetallesProgramasDTO DetallesProgramaGeneral { get; set; }
        public int FechaAsincronicaNueva { get; set; }
    }
    public class CompuestoModeloPredictivoProgramaDTO
    {
        public List<ModeloPredictivoIndustriaDTO> Industria { get; set; }
        public List<ModeloPredictivoCargoDTO> Cargo { get; set; }
        public List<ModeloPredictivoFormacionDTO> Formacion { get; set; }
        public List<ModeloPredictivoTrabajoDTO> Trabajo { get; set; }
        public List<ModeloPredictivoCategoriaDatoDTO> CategoriaDato { get; set; }
        public List<ModeloPredictivoEscalaDTO> Escala { get; set; }
        public List<ModeloPredictivoTipoDatoDTO> TipoDato { get; set; }
        public ModeloPredictivoInterceptoDTO Intercepto { get; set; }
        public int IdPGeneral { get; set; }
    }
    public class ModeloPredictivoInterceptoDTO
    {
        public int Id { get; set; }
        public int IdPGeneral { get; set; }
        public decimal PeIntercepto { get; set; }
        public int PeEstado { get; set; }
    }
    public class CompuestoPreRequisitoModalidadAlternaDTO
    {
        public int IdPreRequisito { get; set; }
        public int IdPGeneral { get; set; }
        public string? NombrePreRequisito { get; set; }
        public int Orden { get; set; }
        public int Tipo { get; set; }
        public List<ModalidadCursoProblemaDTO> Modalidades { get; set; }
    }
    public class ProgramaRelacionadoDTO
    {
        public List<CursoRelacionadoProgramaDTO> Cursos { get; set; }
        public int IdPGeneral { get; set; }
    }
    public class CursoRelacionadoProgramaDTO
    {
        public int Id { get; set; }
        public int IdRelacionado { get; set; }
        public string Nombre { get; set; }
    }
    public class PGeneralCursoCriterioHijoVistaDTO
    {
        public int Id { get; set; }
        public int IdProgramaGeneralHijo { get; set; }
        public string Nombre { get; set; }
        public bool ConsiderarNota { get; set; }
        public int IdPGeneral_Padre { get; set; }
        public int Porcentaje { get; set; }
        public int IdModalidadCurso { get; set; }
        public int IdCriterioEvaluacion { get; set; }
        public int EsCurso { get; set; }
    }
    public class ProgramaGeneralSubAreaFiltroDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int? IdSubArea { get; set; }
    }
    public class PgeneralIdPaginaDTO
    {
        public int Id { get; set; }
        public int IdPagina { get; set; }
    }
}
