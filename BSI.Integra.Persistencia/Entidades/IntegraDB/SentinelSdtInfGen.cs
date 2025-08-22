using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class SentinelSdtInfGen : BaseIntegraEntity
    {
        public int? IdSentinel { get; set; }
        [StringLength(10)]
        public string? Dni { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        [StringLength(20)]
        public string? Sexo { get; set; }
        [StringLength(2)]
        public string? Digito { get; set; }
        [StringLength(2)]
        public string? DigitoAnterior { get; set; }
        [StringLength(20)]
        public string? Ruc { get; set; }
        [StringLength(200)]
        public string? RazonSocial { get; set; }
        [StringLength(200)]
        public string? NombreComercial { get; set; }
        public DateTime? FechaBaja { get; set; }
        [StringLength(200)]
        public string? TipoContribuyente { get; set; }
        [StringLength(4)]
        public string? CodigoTipoContribuyente { get; set; }
        [StringLength(30)]
        public string? EstadoContribuyente { get; set; }
        [StringLength(20)]
        public string? CodigoEstadoContribuyente { get; set; }
        [StringLength(40)]
        public string? CondicionContribuyente { get; set; }
        [StringLength(20)]
        public string? CodigoCondicionContribuyente { get; set; }
        [StringLength(200)]
        public string? ActividadEconomica { get; set; }
        [StringLength(20)]
        public string? Ciiu { get; set; }
        [StringLength(200)]
        public string? ActividadEconomica2 { get; set; }
        [StringLength(20)]
        public string? Ciiu2 { get; set; }
        [StringLength(200)]
        public string? ActividadEconomica3 { get; set; }
        [StringLength(20)]
        public string? Ciiu3 { get; set; }
        public DateTime? FechaActividad { get; set; }
        [StringLength(500)]
        public string? Direccion { get; set; }
        [StringLength(200)]
        public string? Referencia { get; set; }
        [StringLength(50)]
        public string? Departamento { get; set; }
        [StringLength(50)]
        public string? Provincia { get; set; }
        [StringLength(50)]
        public string? Distrito { get; set; }
        [StringLength(10)]
        public string? Ubigeo { get; set; }
        public DateTime? FechaConstitucion { get; set; }
        [StringLength(50)]
        public string? ActvidadComercioExterior { get; set; }
        [StringLength(4)]
        public string? CodigoActividadComerExt { get; set; }
        [StringLength(8)]
        public string? CodigoDependencia { get; set; }
        [StringLength(100)]
        public string? Dependencia { get; set; }
        [StringLength(10)]
        public string? Folio { get; set; }
        [StringLength(10)]
        public string? Asiento { get; set; }
        [StringLength(10)]
        public string? Tomo { get; set; }
        [StringLength(10)]
        public string? PartidaReg { get; set; }
        [StringLength(13)]
        public string? Patron { get; set; }
    }
}
