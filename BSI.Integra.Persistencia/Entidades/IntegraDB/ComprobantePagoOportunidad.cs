using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class ComprobantePagoOportunidad : BaseIntegraEntity
    {
        public int? IdContacto { get; set; }
        [StringLength(200)]
        public string Nombres { get; set; } = null!;
        [StringLength(200)]
        public string Apellidos { get; set; } = null!;
        [StringLength(50)]
        public string Celular { get; set; } = null!;
        [StringLength(20)]
        public string Dni { get; set; } = null!;
        [StringLength(100)]
        public string Correo { get; set; } = null!;
        [StringLength(100)]
        public string NombrePais { get; set; } = null!;
        public int IdPais { get; set; }
        [StringLength(100)]
        public string NombreCiudad { get; set; } = null!;
        [StringLength(50)]
        public string TipoComprobante { get; set; } = null!;
        [StringLength(100)]
        public string NroDocumento { get; set; } = null!;
        [StringLength(500)]
        public string NombreRazonSocial { get; set; } = null!;
        [StringLength(500)]
        public string Direccion { get; set; } = null!;
        public int BitComprobante { get; set; }
        public string? Observacion { get; set; }
        public int? IdOcurrencia { get; set; }
        public int IdAsesor { get; set; }
        public int? IdOportunidad { get; set; }
        public string? Comentario { get; set; }
        public int? IdClasificacionPersona { get; set; }
        public byte[] RowVersion { get; set; } = null!;
    }
}
