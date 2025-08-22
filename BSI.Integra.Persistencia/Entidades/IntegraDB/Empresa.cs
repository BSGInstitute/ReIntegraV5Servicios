using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class Empresa : BaseIntegraEntity
    {
        [StringLength(600)]
        public string Nombre { get; set; } = null!;
        [StringLength(600)]
        public string? Ruc { get; set; }
        public int? IdTipoIdentificador { get; set; }
        [StringLength(8000)]
        public string? Direccion { get; set; }
        [StringLength(100)]
        public string? Telefono { get; set; }
        [StringLength(600)]
        public string? PaginaWeb { get; set; }
        [StringLength(600)]
        public string? Email { get; set; }
        public int? Trabajadores { get; set; }
        public double? NivelFacturacion { get; set; }
        public int? IdPais { get; set; }
        public int? IdRegion { get; set; }
        public int? IdCiudad { get; set; }
        public int? IdIndustria { get; set; }
        [StringLength(1)]
        public string? IdTipoEmpresa { get; set; }
        public int? IdTamanio { get; set; }
        public int? Ciiu { get; set; }
        public int? IdCodigoCiiuIndustria { get; set; }
        public string? Municipio { get; set; }
        public int? IdMunicipioMexico { get; set; }
        public int? IdAsentamientoMexico { get; set; }
        public int? IdCiudadMexico { get; set; }
        public string? EstadoLugar { get; set; }
        public string? CodigoPostal { get; set; }
        public string? Colonia { get; set; }
    }
}
