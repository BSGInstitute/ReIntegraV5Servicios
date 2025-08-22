using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class OportunidadLogOpor : BaseIntegraEntity
    {
        public int Id { get; set; }
        public int? IdOportunidad { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? IdPersonalAsignado { get; set; }
        public int? IdTipoDato { get; set; }
        public int? IdFaseOportunidadAnt { get; set; }
        public int? IdFaseOportunidad { get; set; }
        public int? IdOrigen { get; set; }
        public int? IdContacto { get; set; }
        public DateTime? FechaLog { get; set; }
        public int? IdActividadDetalle { get; set; }
        public int? IdOcurrencia { get; set; }
        public int? IdOcurrenciaActividad { get; set; }
        public string Comentario { get; set; }
        public int? IdCategoriaOrigen { get; set; }
        public int? IdConjuntoAnuncio { get; set; }
        public int? IdFaseOportunidadIp { get; set; }
        public int? IdFaseOportunidadIc { get; set; }
        public DateTime? FechaEnvioFaseOportunidadPf { get; set; }
        public DateTime? FechaPagoFaseOportunidadPf { get; set; }
        public DateTime? FechaPagoFaseOportunidadIc { get; set; }
        public bool? FasesActivas { get; set; }
        public DateTime? FechaRegistroCampania { get; set; }
        public int? IdFaseOportunidadPf { get; set; }
        public string CodigoPagoIc { get; set; }
        public int? IdAsesorAnt { get; set; }
        public int? IdCentroCostoAnt { get; set; }
        public DateTime? FechaFinLog { get; set; }
        public DateTime? FechaCambioFase { get; set; }
        public bool? CambioFase { get; set; }
        public DateTime? FechaCambioFaseIs { get; set; }
        public bool? CambioFaseIs { get; set; }
        public DateTime? FechaCambioFaseAnt { get; set; }
        public DateTime? FechaCambioAsesor { get; set; }
        public DateTime? FechaCambioAsesorAnt { get; set; }
        public int? CambioFaseAsesor { get; set; }
        public int? CicloRn2 { get; set; }
        public int? IdSubCategoriaDato { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
        public int? IdClasificacionPersona { get; set; }
        public int? IdPersonalAreaTrabajo { get; set; }
        public int? IdOcurrenciaAlterno { get; set; }
        public int? IdOcurrenciaActividadAlterno { get; set; }
        public OportunidadLogOpor()
        {
        }
    }
}
