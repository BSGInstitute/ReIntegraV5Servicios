using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Comercial;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ParametroFinalizarActividadDTO
    {
        public DatosFiltroFinalizarActividadDTO? filtro { get; set; }
        public OportunidadDTO? datosOportunidad { get; set; }
        public OportunidadDatosDTO? datosOportunidad2 { get; set; }
        public ActividadDetalleDTO? ActividadAntigua { get; set; }
        public OportunidadPreriquisitosBeneficiosSolucionesCompuestoDTO? DatosCompuesto { get; set; }
        public ComprobantePagoOportunidadDTO? ComprobantePago { get; set; }
        public CalidadLlamadaDTO? CalidadLlamada { get; set; }
        public string Usuario { get; set; }
        public int IdFaseOportunidad { get; set; }
        public string? tipoProgramacion { get; set; }//manual//automatica
    }
    public class FinalizarActividadAlternoDTO
    {
        public DatosOportunidadDTO DatosOportunidad { get; set; }
        public ActividadDetalleDTO ActividadAntigua { get; set; }
        //  public ComprobantePagoOportunidadDTO ComprobantePago { get; set; }
        public DatosCompuestoDTO DatosCompuesto { get; set; }
        // public DatosFiltroFinalizarActividadDTO Filtro { get; set; }
        public string Usuario { get; set; }
    }
}
