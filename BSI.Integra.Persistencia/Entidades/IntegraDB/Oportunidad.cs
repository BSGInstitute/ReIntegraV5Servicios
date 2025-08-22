using BSI.Integra.Aplicacion.Base;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class Oportunidad : BaseIntegraEntity
    {
        public int? IdCentroCosto { get; set; }
        public int? IdPersonalAsignado { get; set; }
        public int IdTipoDato { get; set; }
        public int IdFaseOportunidad { get; set; }
        public int? IdOrigen { get; set; }
        public int? IdAlumno { get; set; }
        [StringLength(500)]
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
        [StringLength(50)]
        public string? CodigoPagoIc { get; set; }
        public int? FlagVentaCruzada { get; set; }
        public int? IdTiempoCapacitacion { get; set; }
        public int? IdTiempoCapacitacionValidacion { get; set; }
        public int? IdSubCategoriaDato { get; set; }
        public int? IdInteraccionFormulario { get; set; }
        public string? UrlOrigen { get; set; }
        public DateTime? FechaPaso2 { get; set; }
        public bool? Paso2 { get; set; }
        [StringLength(300)]
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
        public int? IdPersonal_CoordinadorSeguimiento { get; set; }
        public Guid? IdMigracion { get; set; }
        public ICollection<ActividadDetalle> ActividadDetalles { get; set; }
        public ICollection<AsignacionOportunidad> AsignacionOportunidads { get; set; }
        public virtual ICollection<CalidadProcesamientoAlterno> CalidadProcesamientoAlternos { get; set; }
        public ICollection<CalidadProcesamiento> CalidadProcesamientos { get; set; }
        public ICollection<ComprobantePagoOportunidad> ComprobantePagoOportunidads { get; set; }
        public ICollection<ModeloDataMining> ModeloDataMinings { get; set; }
        public ICollection<OportunidadClasificacionOperaciones> OportunidadClasificacionOperaciones { get; set; }
        public ICollection<OportunidadCompetidor> OportunidadCompetidors { get; set; }
        public ICollection<OportunidadLog> OportunidadLogs { get; set; }
        public ICollection<SolucionClienteByActividad> SolucionClienteByActividads { get; set; }

    }
}