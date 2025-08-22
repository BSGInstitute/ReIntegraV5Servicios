using BSI.Integra.Persistencia.Entidades.IntegraDB;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class OportunidadDTO
    {
        public int? Id { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? IdPersonalAsignado { get; set; }
        public int? IdTipoDato { get; set; }
        public int? IdFaseOportunidad { get; set; }
        public int? IdOrigen { get; set; }
        public int? IdAlumno { get; set; }
        [StringLength(500)]
        public string? UltimoComentario { get; set; }
        public int? IdActividadDetalleUltima { get; set; }
        public int? IdActividadCabeceraUltima { get; set; }
        public int? IdEstadoActividadDetalleUltimoEstado { get; set; }
        public string? UltimaFechaProgramada { get; set; }
        public string? UltimaHoraProgramada { get; set; }
        public int? IdEstadoOportunidad { get; set; }
        public int? IdEstadoOcurrenciaUltimo { get; set; }
        public int? IdFaseOportunidadMaxima { get; set; }
        public int? IdFaseOportunidadInicial { get; set; }
        public int? IdCategoriaOrigen { get; set; }
        public int? IdConjuntoAnuncio { get; set; }
        public int? IdCampaniaScoring { get; set; }
        public int? IdFaseOportunidadIp { get; set; }
        public int? IdFaseOportunidadIc { get; set; }
        public DateTime? FechaEnvioFaseOportunidadPf { get; set; }
        public DateTime? FechaPagoFaseOportunidadPf { get; set; }
        public DateTime? FechaPagoFaseOportunidadIc { get; set; }
        public DateTime? FechaRegistroCampania { get; set; }
        public Guid? IdFaseOportunidadPortal { get; set; }
        public int? IdFaseOportunidadPf { get; set; }
        public string? CodigoPagoIc { get; set; }
        public int? FlagVentaCruzada { get; set; }
        public int? IdTiempoCapacitacion { get; set; }
        public int? IdTiempoCapacitacionValidacion { get; set; }
        public int? IdSubCategoriaDato { get; set; }
        public int? IdInteraccionFormulario { get; set; }
        public string? UrlOrigen { get; set; }
        public DateTime? FechaPaso2 { get; set; }
        public bool? Paso2 { get; set; }
        public string? CodMailing { get; set; }
        public int? IdPagina { get; set; }
        public bool? FasesActivas { get; set; }
        public int? IdTipoInteraccion { get; set; }
        public int? IdClasificacionPersona { get; set; }
        public int? IdPersonalAreaTrabajo { get; set; }
    }
    public class OportunidadBoDTO
    {
        public int Id { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? IdPersonalAsignado { get; set; }
        public int IdTipoDato { get; set; }
        public int IdFaseOportunidad { get; set; }
        public int? IdOrigen { get; set; }
        public int? IdAlumno { get; set; }
        public string? UltimoComentario { get; set; }
        public int? IdActividadDetalleUltima { get; set; }
        public int? IdActividadCabeceraUltima { get; set; }
        public int? IdEstadoActividadDetalleUltimoEstado { get; set; }
        public DateTime? UltimaFechaProgramada { get; set; }
        public int IdEstadoOportunidad { get; set; }
        public int? IdEstadoOcurrenciaUltimo { get; set; }
        public int IdFaseOportunidadMaxima { get; set; }
        public int? IdFaseOportunidadInicial { get; set; }
        public int? IdCategoriaOrigen { get; set; }
        public int? IdConjuntoAnuncio { get; set; }
        public int? IdCampaniaScoring { get; set; }
        public int? IdFaseOportunidadIp { get; set; }
        public int? IdFaseOportunidadIc { get; set; }
        public DateTime? FechaEnvioFaseOportunidadPf { get; set; }
        public DateTime? FechaPagoFaseOportunidadPf { get; set; }
        public DateTime? FechaPagoFaseOportunidadIc { get; set; }
        public DateTime FechaRegistroCampania { get; set; }
        public Guid? IdFaseOportunidadPortal { get; set; }
        public int? IdFaseOportunidadPf { get; set; }
        public string? CodigoPagoIc { get; set; }
        public int? FlagVentaCruzada { get; set; }
        public int? IdTiempoCapacitacion { get; set; }
        public int? IdTiempoCapacitacionValidacion { get; set; }
        public int? IdSubCategoriaDato { get; set; }
        public int? IdInteraccionFormulario { get; set; }
        public string? UrlOrigen { get; set; }
        public DateTime? FechaPaso2 { get; set; }
        public bool? Paso2 { get; set; }
        public string? CodMailing { get; set; }
        public int? IdPagina { get; set; }
        public int? NroSolicitud { get; set; }
        public int? NroSolicitudPorArea { get; set; }
        public int? NroSolicitudPorSubArea { get; set; }
        public int? NroSolicitudPorProgramaGeneral { get; set; }
        public int? NroSolicitudPorProgramaEspecifico { get; set; }
        public int? IdClasificacionPersona { get; set; }
        public int? IdPersonalAreaTrabajo { get; set; }
        public int? IdPadre { get; set; }
        public int? IdAnuncioFacebook { get; set; }
        public bool? ValidacionCorrecta { get; set; }
        public bool? EnLlamada { get; set; }
        public int? NumeroIntentoLlamada { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public int? IdTipoInteraccion { get; set; }
        public decimal? ValorProbabilidad { get; set; }
        public string Usuario { get; set; }

        public List<ActividadDetalle>? Actividades;
        public ActividadDetalle? ActividadAntigua;
        public ActividadDetalle? ActividadNueva;
        public ActividadDetalle? ActividadNuevaProgramarActividad;
        public OportunidadLog? OportunidadLogAntigua;
        public OportunidadLog? OportunidadLogNueva;
        public OportunidadCompetidor? OportunidadCompetidor;
        public CalidadProcesamiento? CalidadProcesamiento;
        public List<SolucionClienteByActividad>? ListaSoluciones;
        public ComprobantePagoOportunidad? ComprobantePago;
        public PreCalculadaCambioFase? PreCalculadaCambioFase;
        public ModeloDataMining? ModeloDataMining;
        public AsignacionOportunidad? AsignacionOportunidad;
        public AsignacionOportunidadLog? AsignacionOportunidadLog;
        public Alumno? Alumno;
        public Expositor? Expositor;
    }

    public class OportunidadVentaCurzadaResultadoDTO
    {
        public int OportunidadNuevoId { get; set; }
        public int ActividadDetalleNuevoId { get; set; }
    }
    public class OportunidadComboDTO
    {
        public int Id { get; set; }
        public string Alumno { get; set; } = null!;
        public string CentroCosto { get; set; } = null!;
    }
    public class OportunidadVentaCruzadaAgendaDTO
    {
        public int IdOportunidad { get; set; }
        public string? Programa { get; set; }
        public string? Probabilidad { get; set; }
        public string? Precio { get; set; }
        public string? Matricula { get; set; }
        public string? Comision { get; set; }
        public string? Contado { get; set; }
        public int Orden { get; set; }
        public decimal Costo { get; set; }
    }
    public class OportunidadHistorialAgendaDTO
    {
        public int IdOportunidad { get; set; }
        public string? Programa { get; set; }
        public string? Area { get; set; }
        public string? Grupo { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public string? Probabilidad { get; set; }
        public string? FaseMaxima { get; set; }
        public string? FaseOportunidad { get; set; }
        public string? Precio { get; set; }
        public string? Comision { get; set; }
        public string? MontoTotal { get; set; }
        public string? IdBusqueda { get; set; }
        public string? Asesor { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
    public class OportunidadInformacionDTO
    {
        public int IdAlumno { get; set; }
        public int IdClasificacionPersona { get; set; }
        public List<OportunidadVentaCruzadaAgendaDTO> ListaVentaCruzada { get; set; } = new List<OportunidadVentaCruzadaAgendaDTO>();
        public List<OportunidadHistorialAgendaDTO> ListaHistorial { get; set; } = new List<OportunidadHistorialAgendaDTO>();
        public ProgramaGeneralPreBenCompuestoDTO ProgramaGeneralPreBen { get; set; } = new ProgramaGeneralPreBenCompuestoDTO();
        public List<ActividadOportunidadDTO> ActividadesOportunidad { get; set; } = new List<ActividadOportunidadDTO>();
    }
    public class OportunidadTiempoCapacitacionDTO
    {
        public int Id { get; set; }
        public int? IdTiempoCapacitacion { get; set; }
        public int? IdTiempoCapacitacionValidacion { get; set; }
        public string Usuario { get; set; }
    }
    public class TiempoCapacitacionPorIdOportunidadDTO
    {
        public List<TiempoCapacitacionComboDTO> Records { get; set; } = new List<TiempoCapacitacionComboDTO>();
        public int? Record { get; set; }
        public bool Lista { get; set; }
        public bool ListaValidacion { get; set; }
        public int? RecordValidado { get; set; }
    }
    public class OportunidadPrerequisitoBeneficioCompetidorDTO
    {
        public OportunidadCompetidorAgendaDTO? OportunidadCompetidor { get; set; }
        public List<DetalleOportunidadCompetidorEmpresaDTO> Competidores { get; set; } = new List<DetalleOportunidadCompetidorEmpresaDTO>();
        public List<ProgramaGeneralPrerequisitoOportunidadDTO> PrerequisitosGenerales { get; set; } = new List<ProgramaGeneralPrerequisitoOportunidadDTO>();
        public List<ProgramaGeneralPrerequisitoOportunidadDTO> PrerequisitosEspecificos { get; set; } = new List<ProgramaGeneralPrerequisitoOportunidadDTO>();
        public List<ProgramaGeneralBeneficioOportunidadDetalleDTO> Beneficios { get; set; } = new List<ProgramaGeneralBeneficioOportunidadDetalleDTO>();
    }
    public class OportunidadCodigoFaseDTO
    {
        public string? FaseInicio { get; set; }
        public DateTime? FechaSiguienteLlamada { get; set; }
        public int? IdFaseOportunidad { get; set; }
    }
    public class OportunidadCompuestoDTO
    {
        public int Id { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? IdPespecifico { get; set; }
        public int? IdPersonalAsignado { get; set; }
        public int IdTipoDato { get; set; }
        public int IdFaseOportunidad { get; set; }
        public int? IdOrigen { get; set; }
        public string? CodigoPagoIC { get; set; }
        public int? IdAlumno { get; set; }
        public string? UltimoComentario { get; set; }
        public int? IdActividadDetalleUltima { get; set; }
        public int? IdActividadCabeceraUltima { get; set; }
        public int? IdEstadoActividadDetalleUltimoEstado { get; set; }
        public DateTime? UltimaFechaProgramada { get; set; }
        public int IdEstadoOportunidad { get; set; }
        public int? IdEstadoOcurrenciaUltimo { get; set; }
        public int IdFaseOportunidadMaxima { get; set; }
        public int? IdFaseOportunidadInicial { get; set; }
        public int? IdCategoriaOrigen { get; set; }
        public string? NombrePartner { get; set; }
        public string? EncabezadoCorreoPartner { get; set; }
        public int? IdActividadDetalle { get; set; }
        public string? PrecioContado { get; set; }
        public string? NombreProgramaGeneral { get; set; }
        public string? PwDuracion { get; set; }
        public string? IdCategoriaPrograma { get; set; }
        public string? UrlVersion { get; set; }
        public string? UrlBrochurePrograma { get; set; }
        public DateTime? FechaEnvio { get; set; }
        public string? Central { get; set; }
        public string? Anexo3CX { get; set; }
        public string? UrlFirmaCorreos { get; set; }
        public string? Email { get; set; }
        public int? IdPgeneral { get; set; }
        // Atributos Adicionales
        public string? CostoTotalConDescuento { get; set; }
        public bool? Promocion25 { get; set; }
    }
    public class ResultadoVisualizarOportunidadDTO
    {
        public int Id { get; set; }
        public DateTime? FechaVisibleHasta { get; set; }
        public int? Valor { get; set; }
    }
    public class OportunidadBandejaCorreoDTO
    {
        public int? IdOportunidad { get; set; }
        public int? IdContacto { get; set; }
        public string? Nombre1 { get; set; }
        public string? Nombre2 { get; set; }
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public string? NombreCentroCosto { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public string? Celular { get; set; }
        public string? Email1 { get; set; }
        public string? Email2 { get; set; }
        public int? IdCodigoPais { get; set; }
        public int? IdCiudad { get; set; }
        public int? IdCargo { get; set; }
        public int? IdAFormacion { get; set; }
        public int? IdATrabajo { get; set; }
        public int? IdIndustria { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? IdTipoDato { get; set; }
        public int? IdFaseOportunidad { get; set; }
        public int? IdOrigen { get; set; }
        public int? IdEmpresa { get; set; }
        public string? Codigo { get; set; }
        public string Asesor { get; set; } = null!;
        public bool? EnSeguimiento { get; set; }

    }
    public class OportunidadAlumnoDTO
    {
        public string DiaFechaActual { get; set; }
        public string NombreMesFechaActual { get; set; }
        public string AnioFechaActual { get; set; }
        public string MontoTotal { get; set; }
        public string DuracionMesesPGeneral { get; set; }
        public string NombrePGeneral { get; set; }
        public string CronogramaPagoCompleto { get; set; }
        public string CronogramaPagoCompletoChile { get; set; }
        public string Anexo1EstructuraCurricular { get; set; }
        public string Anexo2Certificacion { get; set; }
        public string Version { get; set; }
        public int IdPEspecifico { get; set; }
        public OportunidadAlumnoDetalleDTO OportunidadAlumno { get; set; }
    }
    public class DatosAlumnoOportunidadDTO
    {
        public int IdOportunidad { get; set; }
        public string CodigoMatricula { get; set; }
        public int Id { get; set; }
        public int IdCodigoPais { get; set; }
        public string Celular { get; set; }
        public string Nombre1 { get; set; }
        public string Nombre2 { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
    }
    public class OportunidadAlumnoDetalleDTO
    {
        public string Nombre1 { get; set; }
        public string NombreCompleto { get; set; }
        public string NroDocumento { get; set; }
        public string Direccion { get; set; }
        public int? IdCodigoPais { get; set; }
        public string NombreCiudad { get; set; }
        public string NombrePais { get; set; }
        public string Email1 { get; set; }
    }
    public class BoolRN2DTO
    {
        public bool Estado { get; set; }
    }
    public class OportunidadFinalizarActividadDTO
    {
        public int Id { get; set; }
        public int IdCentroCosto { get; set; }
        public int IdPersonalAsignado { get; set; }
        public int IdTipoDato { get; set; }
        public int IdFaseOportunidad { get; set; }
        public int IdOrigen { get; set; }
        public int IdAlumno { get; set; }
        public string? UltimoComentario { get; set; }
        public int IdActividadDetalleUltima { get; set; }
        public int IdActividadCabeceraUltima { get; set; }
        public int IdEstadoActividadDetalleUltimoEstado { get; set; }
        public string? UltimaFechaProgramada { get; set; }
        public string? UltimaHoraProgramada { get; set; }
        public int IdEstadoOportunidad { get; set; }
        public int IdEstadoOcurrenciaUltimo { get; set; }
        public int IdFaseOportunidadMaxima { get; set; }
        public int IdFaseOportunidadInicial { get; set; }
        public int IdCategoriaOrigen { get; set; }
        public int IdConjuntoAnuncio { get; set; }
        public int IdCampaniaScoring { get; set; }
        public int IdFaseOportunidadIp { get; set; }
        public int IdFaseOportunidadIc { get; set; }
        public DateTime? FechaEnvioFaseOportunidadPf { get; set; }
        public DateTime? FechaPagoFaseOportunidadPf { get; set; }
        public DateTime? FechaPagoFaseOportunidadIc { get; set; }
        public DateTime? FechaRegistroCampania { get; set; }
        public Guid? IdFaseOportunidadPortal { get; set; }
        public int IdFaseOportunidadPf { get; set; }
        public string? CodigoPagoIc { get; set; }
        public int FlagVentaCruzada { get; set; }
        public int IdTiempoCapacitacion { get; set; }
        public int IdTiempoCapacitacionValidacion { get; set; }
        public int IdSubCategoriaDato { get; set; }
        public int IdInteraccionFormulario { get; set; }
        public string? UrlOrigen { get; set; }
        public DateTime? FechaPaso2 { get; set; }
        public bool? Paso2 { get; set; }
        public string? CodMailing { get; set; }
        public int IdPagina { get; set; }
        public bool FasesActivas { get; set; }
        public int? IdTipoInteraccion { get; set; }
        public int? IdClasificacionPersona { get; set; }
        public int? IdPersonalAreaTrabajo { get; set; }
    }
    public class FinalizarActividadRespuestaDTO
    {
        public ActividadDetalleDTO ActividadNueva { get; set; }
        public OportunidadDTO Oportunidad { get; set; }
        public OportunidadLogFinalizarActividadDTO OportunidadLogNueva { get; set; }
        public PreCalculadaCambioFaseDTO? PreCalculadaCambioFase { get; set; }
    }
    public class ProgramaActividadAlternoRespuestaDTO
    {
        public ActividadDetalleDTO ActividadNuevaProgramarActividad { get; set; }
        public OportunidadDTO Oportunidad { get; set; }
    }
    public class OportunidadDatosDTO
    {
        public int? Id { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? IdPersonalAsignado { get; set; }
        public int? IdTipoDato { get; set; }
        public int? IdFaseOportunidad { get; set; }
        public int? IdOrigen { get; set; }
        public int? IdAlumno { get; set; }
        public string UltimoComentario { get; set; }
        public int? IdActividadDetalleUltima { get; set; }
        public int? IdActividadCabeceraUltima { get; set; }
        public int? IdEstadoActividadDetalleUltimoEstado { get; set; }
        public string? UltimaFechaProgramada { get; set; }
        public string? UltimaHoraProgramada { get; set; }
        public int? IdEstadoOportunidad { get; set; }
        public int? IdEstadoOcurrenciaUltimo { get; set; }
        public int? IdFaseOportunidadMaxima { get; set; }
        public int? IdFaseOportunidadInicial { get; set; }
        public int? IdCategoriaOrigen { get; set; }
        public int? IdConjuntoAnuncio { get; set; }
        public int? IdCampaniaScoring { get; set; }
        public int? IdFaseOportunidadIp { get; set; }
        public int? IdFaseOportunidadIc { get; set; }
        public DateTime? FechaEnvioFaseOportunidadPf { get; set; }
        public DateTime? FechaPagoFaseOportunidadPf { get; set; }
        public DateTime? FechaPagoFaseOportunidadIc { get; set; }
        public DateTime? FechaRegistroCampania { get; set; }
        public Guid? IdFaseOportunidadPortal { get; set; }
        public int? IdFaseOportunidadPf { get; set; }
        public string? CodigoPagoIc { get; set; }
        public int? FlagVentaCruzada { get; set; }
        public int? IdTiempoCapacitacion { get; set; }
        public int? IdTiempoCapacitacionValidacion { get; set; }
        public int? IdSubCategoriaDato { get; set; }
        public int? IdInteraccionFormulario { get; set; }
        public string? UrlOrigen { get; set; }
        public DateTime? FechaPaso2 { get; set; }
        public bool? Paso2 { get; set; }
        public string? CodMailing { get; set; }
        public int? IdPagina { get; set; }
        public bool? FasesActivas { get; set; }

        public int? IdTipoInteraccion { get; set; }
        public int? IdClasificacionPersona { get; set; }
        public int? IdPersonalAreaTrabajo { get; set; }
        public AlumnoHoraDTO? alumnoHoraDTO { get; set; } = new AlumnoHoraDTO();
    }
    public class OportunidadReasignarDTO
    {
        public int IdAsesor { get; set; }
        public string NombreCompletoAsesor { get; set; }
        public string EmailAsesor { get; set; }
        public int IdJefe { get; set; }
        public string NombreCompletoJefe { get; set; }
        public int IdOportunidad { get; set; }
        public string CodigoFaseOportunidad { get; set; }
        public string Nombre1 { get; set; }
        public string Nombre2 { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
    }
    public class ProbabilidadOportunidadResumenDTO
    {
        public int IdOportunidad { get; set; }
        public decimal Probabilidad { get; set; }
    }
    public class OportunidadFormularioDTO
    {
        public int Id { get; set; }
        public int IdAlumno { get; set; }
        public int IdCentroCosto { get; set; }
        public int IdFaseOportunidad { get; set; }
        public int IdOrigen { get; set; }
        public int IdPersonal_Asignado { get; set; }
        public int IdTipoDato { get; set; }
        [StringLength(500)]
        public string UltimoComentario { get; set; }
        public int fk_id_tipointeraccion { get; set; }
    }
    public class OportunidadCodigoMatriculaDTO
    {
        public int IdOportunidad { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public bool Verificado { get; set; }
        public string Usuario { get; set; }
        public string? CodigoMatricula { get; set; }
    }

    public class OportunidadOperacionesFiltroDTO
    {
        public int Id { get; set; }
        public int IdPersonal_Asignado { get; set; }
        public int IdAlumno { get; set; }
        public int IdCentroCosto { get; set; }
        public int? IdPadre { get; set; }
        public int IdFaseOportunidad { get; set; }
        public int IdPersonalAreaTrabajo { get; set; }
        public int? IdOportunidadClasificacionOperaciones { get; set; }
    }
    public class OportunidadISMDTO
    {
        public int IdOportunidad { get; set; }
        public int IdProgramaGeneral { get; set; }
        public int IdAlumno { get; set; }
    }
    public class OportunidadProbabilidadDTO
    {
        public int IdOportunidad { get; set; }
        public int IdProgramaGeneral { get; set; }
        public int IdAlumno { get; set; }
        public int PesoProbabilidad { get; set; }
    }
    public class OportunidadCategoriaDTO
    {
        public int IdOportunidad { get; set; }
        public int IdFaseOportunidad { get; set; }
        public int IdPersonalAsignado { get; set; }
        public int IdProgramaGeneral { get; set; }
        public int IdAlumno { get; set; }
        public int PesoCategoria { get; set; }
    }
    public class OportunidadVentaCruzadaDTO
    {
        public int IdOportunidad { get; set; }
        public string Programa { get; set; }
        public string Probabilidad { get; set; }
        public string Precio { get; set; }
        public string Matricula { get; set; }
        public string Comision { get; set; }
        public string Contado { get; set; }
        public int Orden { get; set; }
        public decimal Costo { get; set; }
    }
    public class OportunidadHistorialDTO
    {
        public int IdOportunidad { get; set; }
        public string Programa { get; set; }
        public string Area { get; set; }
        public string Grupo { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public string Probabilidad { get; set; }
        public string FaseMaxima { get; set; }
        public string FaseOportunidad { get; set; }
        public string Precio { get; set; }
        public string Comision { get; set; }
        public string MontoTotal { get; set; }
        public string IdBusqueda { get; set; }
        public string Asesor { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
    public class InformacionOportunidadDTO
    {
        public int IdAlumno { get; set; }
        public int IdClasificacionPersona { get; set; }
        public List<OportunidadVentaCruzadaDTO> VentasCruzadas { get; set; } = new List<OportunidadVentaCruzadaDTO>();
        public List<OportunidadHistorialDTO> HistorialOportunidades { get; set; } = new List<OportunidadHistorialDTO>();
    }
    public class RetornoFinalizarCrearOportunidadDTO
    {
        public CompuestoActividadEjecutadaDTO ActividadEjecutada { get; set; }
        public int IdOportunidad { get; set; }
        public string Error { get; set; }
    }
    public class OportunidadTabAgendaDTO
    {
        public int Id { get; set; }
        public string CodigoMatricula { get; set; }
        public string AgendaTab { get; set; }
        public string NombreAlumno { get; set; }
        public string NombreAsignado { get; set; }
    }

    public class OportunidadTabAgendaFichaDTO
    {
        public int Id { get; set; }
        public string CodigoMatricula { get; set; }
        public int AgendaTab { get; set; }
        public string NombreAlumno { get; set; }
        public string NombreAsignado { get; set; }
    }
    public class CerrarOportunidadDTO
    {
        public int Id { get; set; }
        public string CodigoMatricula { get; set; }
        public string AgendaTab { get; set; }
        public string NombreAlumno { get; set; }
        public string NombreAsignado { get; set; }
    }
    public class OportunidadDatosChatDTO
    {
        public int IdOportunidad { get; set; }
        public int IdContacto { get; set; }
        public string Nombre1 { get; set; }
        public string Nombre2 { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string Email1 { get; set; }
        public string Email2 { get; set; }
        public int? IdCodigoPais { get; set; }
        public int? IdCiudad { get; set; }
        public int? IdCargo { get; set; }
        public int? IdAFormacion { get; set; }
        public int? IdATrabajo { get; set; }
        public int? IdIndustria { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? IdTipoDato { get; set; }
        public int? IdFaseOportunidad { get; set; }
        public int? IdOrigen { get; set; }
        public int? IdEmpresa { get; set; }
        public string Error { get; set; }
        public string NombreCentroCosto { get; set; }
    }
    public class OportunidadAnteriorDTO
    {
        public int IdOportunidad { get; set; }
        public string FaseOportunidad { get; set; }
        //  public string FaseMaxima { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string CentroCosto { get; set; }
        // public string Programa { get; set; }
        // public string Probabilidad { get; set; }
        // public string Grupo { get; set; }
        // public Nullable<DateTime> FechaSolicitud { get; set; }
        //  public string Asesor { get; set; }
        public string TipoDato { get; set; }
        public string CategoriaOrigen { get; set; }
    }
    public class OportunidadPendienteWhatsappDTO
    {
        public int IdOportunidad { get; set; }
        public int IdPersonal { get; set; }
        public int IdCategoriaOrigen { get; set; }
    }
    public class OportunidadAnteriorAlternoDTO
    {
        public int IdOportunidad { get; set; }
        public string FaseOportunidad { get; set; }
        //  public string FaseMaxima { get; set; }
        public DateTime FechaCreacion { get; set; }

        public string CentroCosto { get; set; }

        public string TipoDato { get; set; }
        public string CategoriaOrigen { get; set; }

        public int IdAlumno { get; set; }
        public bool EstadoOportunidad { get; set; }

    }




    public class ActividadTrabajadaDTO
    {
        public int IdOportunidad { get; set; }
        public int IdFaseOportunidad { get; set; }
        public int IdActividadDetalle { get; set; }
        public string CodigoFaseOportunidad { get; set; }
        public bool Existe { get; set; }
    }
    public class FichaAlumnoDTO
    {
        public int IdOportunidad { get; set; }
        public int IdAlumno { get; set; }
        public string NombreCentroCosto { get; set; }
    }
    public class ValorEtiquetaWhatsAppDTO
    {
        public string AlumnoNombre1 { get; set; }
        public string PersonalNombreCompleto { get; set; }
        public string PgeneralNombre { get; set; }
    }
    public class InformacionBaseOportunidad
    {
        public string? GuidLinkedInLead { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public string Celular { get; set; }
        public string Pais { get; set; }
        public string Cargo { get; set; }
        public string AreaFormacion { get; set; }
        public string AreaTrabajo { get; set; }
        public string Industria { get; set; }
        public string CentroCosto { get; set; }
        public string Origen { get; set; }
        public string Asesor { get; set; }
        public string TipoDato { get; set; }
        public string FaseOportunidad { get; set; }
        public DateTime? FechaRegistroCampania { get; set; }
    }
    public class CorreoNotificacionDTO
    {
        public int IdPersonal { get; set; }
        public string Email { get; set; }
        public int? IdPais { get; set; }
        public int IdCorreoNotificacionTipo { get; set; }
        public string NombreNotificacionTipo { get; set; }
        public string DescripcionNotificacionTipo { get; set; }
        public bool? Principal { get; set; }
    }
    public class ArchivoOportunidadDTO
    {
        public int Id { get; set; }
        public string NombreArchivo { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
    public class ArchivoMasivoDTO
    {
        public string NombreArchivo { get; set; }
    }


    public class VerificarOportunidadLead
    {
        public string? GuidLinkedInLead { get; set; }
        public int? IdOportunidad { get; set; }
        public int? IdAlumno { get; set; }
    }

}
