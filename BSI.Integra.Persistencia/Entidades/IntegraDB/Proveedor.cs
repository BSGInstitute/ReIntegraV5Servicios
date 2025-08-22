using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class Proveedor : BaseIntegraEntity
    {
        public int IdTipoContribuyente { get; set; }
        public int IdDocumentoIdentidad { get; set; }
        [StringLength(20)]
        public string NroDocIdentidad { get; set; } = null!;
        [StringLength(1000)]
        public string RazonSocial { get; set; } = null!;
        [StringLength(500)]
        public string Nombre1 { get; set; } = null!;
        [StringLength(500)]
        public string Nombre2 { get; set; } = null!;
        [StringLength(500)]
        public string ApePaterno { get; set; } = null!;
        [StringLength(500)]
        public string ApeMaterno { get; set; } = null!;
        [StringLength(500)]
        public string Direccion { get; set; } = null!;
        [StringLength(500)]
        public string Descripcion { get; set; } = null!;
        public int? IdCiudad { get; set; }
        [StringLength(500)]
        public string? Telefono { get; set; }
        [StringLength(500)]
        public string Email { get; set; } = null!;
        [StringLength(50)]
        public string? Celular1 { get; set; }
        [StringLength(50)]
        public string? Celular2 { get; set; }
        [StringLength(200)]
        public string? Contacto1 { get; set; }
        [StringLength(200)]
        public string? Contacto2 { get; set; }
        public int? IdPrestacionRegistro { get; set; }
        public bool? EsPersonaValida { get; set; }
        public int? IdTipoImpuestoPreferido { get; set; }
        public int? IdRetencionPreferido { get; set; }
        public int? IdDetraccionPreferido { get; set; }
        public int? IdPersonalAsignado { get; set; }
        [StringLength(100)]
        public string? Alias { get; set; }
    }
}
