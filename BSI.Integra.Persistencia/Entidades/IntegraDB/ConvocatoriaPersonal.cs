using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class ConvocatoriaPersonal : BaseIntegraEntity
    {
        [StringLength(200)]
        public string Nombre { get; set; } = null!;
        [StringLength(50)]
        public string Codigo { get; set; } = null!;
        public int IdProcesoSeleccion { get; set; }
        public int IdProveedor { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string CuerpoConvocatoria { get; set; } = null!;
        public int? IdSedeTrabajo { get; set; }
        public int? IdArea { get; set; }
        [StringLength(350)]
        public string? UrlAviso { get; set; }
        public int? IdPersonal { get; set; }
        public int? NroVacantes { get; set; }
        public int? IdModalidadTrabajo { get; set; }
        public int? IdCategoriaAsignacion { get; set; }
        public bool? VerEnPortal { get; set; }
        public bool? SoloMatriculado { get; set; }
        public string? InformacionAdicional { get; set; }
        public int? IdTipoContrato { get; set; }
        public string? TipoJornada { get; set; }
        public int? HoraSemanal { get; set; }
        public int? RemIdMoneda { get; set; }
        public decimal? MontoRemBruta { get; set; }
        public bool? VisualizarRem { get; set; }
        public bool? AplicaBono { get; set; }
        public int? BonoIdMoneda { get; set; }
        public decimal? MontoDesdeBono { get; set; }
        public decimal? MontoHastaBono { get; set; }
        public bool? AplicaComision { get; set; }
        public int? ComisionIdMoneda { get; set; }

        public decimal? MontoDesdeComision { get; set; }
        public decimal? MontoHastaComision { get; set; }

        public int? IdEstadoConvocatoria { get; set; }
    }
}
