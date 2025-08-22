using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class AsignacionOportunidadManualDTO
    {
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
        public bool? VentaCruzadaMarketing { get; set; }
        public List<ActividadDetalle> Actividades { get; set; }
        public ActividadDetalle? ActividadAntigua { get; set; }
        public ActividadDetalle ActividadNueva { get; set; }
        public ActividadDetalle ActividadNuevaProgramarActividad { get; set; }
        public OportunidadLog? OportunidadLogAntigua { get; set; }
        public OportunidadLog OportunidadLogNueva { get; set; }
        public OportunidadCompetidor OportunidadCompetidor { get; set; }
        public List<SolucionClienteByActividad> ListaSoluciones { get; set; }
        public ComprobantePagoOportunidad ComprobantePago { get; set; }

        public PreCalculadaCambioFase PreCalculadaCambioFase { get; set; }
        public ModeloDataMining ModeloDataMining { get; set; }
        public AsignacionOportunidad AsignacionOportunidad { get; set; }
        public AsignacionOportunidadLog AsignacionOportunidadLog { get; set; }
        public Alumno Alumno { get; set; }
        public Expositor Expositor { get; set; }
        private string Usuario { get; set; }

        public Oportunidad OportunidadNueva { get; set; }
        public Oportunidad OportunidadAntigua { get; set; }

    }
}

