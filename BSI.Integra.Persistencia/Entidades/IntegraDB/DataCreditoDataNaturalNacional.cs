using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class DataCreditoDataNaturalNacional : BaseIntegraEntity
    {
        public int IdDataCreditoBusqueda { get; set; }
        [StringLength(30)]
        public string NroDocumento { get; set; } = null!;
        [StringLength(150)]
        public string? Nombres { get; set; }
        [StringLength(150)]
        public string? PrimerApellido { get; set; }
        [StringLength(150)]
        public string? SegundoApellido { get; set; }
        [StringLength(150)]
        public string? NombreCompleto { get; set; }
        public bool? Validada { get; set; }
        public bool? Rut { get; set; }
        [StringLength(10)]
        public string? Genero { get; set; }
        [StringLength(20)]
        public string? IdentificacionEstado { get; set; }
        public DateTime? IdentificacionFechaExpedicion { get; set; }
        [StringLength(50)]
        public string? IdentificacionCiudad { get; set; }
        [StringLength(50)]
        public string? IdentificacionDepartamento { get; set; }
        [StringLength(50)]
        public string? IdentificacionNumero { get; set; }
        public int? EdadMinima { get; set; }
        public int? EdadMaxima { get; set; }
        public DataCreditoBusquedum IdDataCreditoBusquedaNavigation { get; set; } = null!;
    }
}
