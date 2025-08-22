using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Comercial
{
    public class ReprogramacionDTO
    {
        public Oportunidad? Oportunidad { get; set; }
        public List<ActividadDetalle>? Actividades { get; set; }
        public ActividadDetalle? ActividadTemp { get; set; }
        public ActividadDetalle? ActividadTrabajada { get; set; }
        public ActividadDetalle? ActividadNuevaProgramada { get; set; }
        public ActividadDetalle? ActividadNueva { get; set; }
        public OportunidadLog? OportunidadLogTemp { get; set; }
        public OportunidadLog? OportunidadLogNueva { get; set; }
        public OportunidadCompetidor? OportunidadCompetidor { get; set; }
        public CalidadProcesamiento? CalidadProcesamiento { get; set; }
        public CalidadProcesamientoAlterno? CalidadProcesamientoAlterno { get; set; }
        public List<SolucionClienteByActividad>? ListaSolucionClienteByActividad { get; set; }
        public ComprobantePagoOportunidad? ComprobantePago { get; set; }
        public PreCalculadaCambioFase? PreCalculadaCambioFase { get; set; }
        public ModeloDataMining? ModeloDataMining { get; set; }
        public AsignacionOportunidad? AsignacionOportunidad { get; set; }
        public AsignacionOportunidadLog? AsignacionOportunidadLog { get; set; }
        public string? Usuario { get; set; }
        public int? IdTipoInteraccion { get; set; }
    }
    public class FinalizarProgramarActividadAlternoDTO
    {
        public DatosFiltroFinalizarActividadDTO? filtro { get; set; }
        public DatosOportunidadDTO DatosOportunidad { get; set; }
        public ActividadAntiguaDTO ActividadAntigua { get; set; }
        public OportunidadCompetidorFinalizarActividadDTO OportunidadCompetidor { get; set; }
        public CalidadProcesamientoAlternoDTO? CalidadProcesamientoAlterno { get; set; }
        public List<int> ListaCompetidor { get; set; }
        public ComprobantePagoOportunidadDTO? ComprobantePago { get; set; }
        public CalidadLlamadaDTO? CalidadLlamada { get; set; }
        public string Usuario { get; set; }
        public int IdFaseOportunidad { get; set; }
        public string? TipoProgramacion { get; set; }//manual o automatica
        public bool? EstadoSeguimientoWhatsApp { get; set; }
    }

    public class RespuestaFinalizarYProgramarActividadAlterno
    {
        public CompuestoActividadEjecutadaDTO Realizada { get; set; }
        public int IdHoraBloqueada { get; set; }
        public int IdOportunidad { get; set; }
        public int IdReprogramacionCabecera { get; set; }
    }
    public class DatosOportunidadDTO
    {
        public bool? FasesActivas { get; set; }
        public int? IdFaseOportunidadIp { get; set; }
        public int? IdFaseOportunidadPf { get; set; }
        public DateTime? FechaEnvioFaseOportunidadPf { get; set; }
        public DateTime? FechaPagoFaseOportunidadPf { get; set; }
        public int? IdFaseOportunidadIc { get; set; }
        public DateTime? FechaPagoFaseOportunidadIc { get; set; }
        public string? CodigoPagoIc { get; set; }
        public string? UltimaFechaProgramada { get; set; }
        public int? IdFaseOportunidad { get; set; }
        public int? IdTipoDato { get; set; }
        public int IdOrigen { get; set; }
        public int? IdEstadoOportunidad { get; set; }
        public int? IdPersonalAsignado { get; set; }
        public int? IdAlumno { get; set; }
        public int? IdTipoInteraccion { get; set; }
        public int? IdSubCategoriaDato { get; set; }
        public int? IdCentroCosto { get; set; }
        public string? UltimoComentario { get; set; }
    }

    public class ActividadAntiguaDTO
    {
        public int Id { get; set; }
        public string? Comentario { get; set; }
        public int IdOcurrencia { get; set; }
        public int IdOcurrenciaActividad { get; set; }
        public int IdAlumno { get; set; }
        public int IdOportunidad { get; set; }
        public int IdCentralLlamada { get; set; }
    }

    public class CerrarActividadDTO
    {
        public DatosOportunidadDTO DatosOportunidad { get; set; }
        public string Usuario { get; set; }
        public ActividadAntiguaDTO ActividadAntigua { get; set; }
        public OportunidadCompetidorFinalizarActividadDTO OportunidadCompetidor { get; set; }
        public CalidadProcesamientoAlternoDTO? CalidadProcesamientoAlterno { get; set; }
        public List<int> ListaCompetidor { get; set; }
    }

    public class VentaCruzadaDTO
    {
        public DatosOportunidadDTO DatosOportunidad { get; set; }
        public ActividadAntiguaDTO ActividadAntigua { get; set; }
        public OportunidadCompetidorFinalizarActividadDTO OportunidadCompetidor { get; set; }
        public CalidadProcesamientoAlternoDTO? CalidadProcesamientoAlterno { get; set; }
        public List<int> ListaCompetidor { get; set; }
        public string Usuario { get; set; }
        public int IdFaseOportunidad { get; set; }
    }

    public class ValidacionDiasSinContactoDTO
    {
        public int idOportunidad { get; set; }
        public int idPais { get; set; }
        public int idPersonal { get; set; }
        public int idCategoriaOrigen { get; set; }

    }

}
