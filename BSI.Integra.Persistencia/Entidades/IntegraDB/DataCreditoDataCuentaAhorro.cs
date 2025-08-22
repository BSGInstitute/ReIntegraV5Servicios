using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class DataCreditoDataCuentaAhorro : BaseIntegraEntity
    {
        public int IdDataCreditoBusqueda { get; set; }
        public bool? Bloqueada { get; set; }
        [StringLength(50)]
        public string? Entidad { get; set; }
        [StringLength(25)]
        public string? Numero { get; set; }
        public DateTime? FechaApertura { get; set; }
        [StringLength(20)]
        public string? Calificacion { get; set; }
        [StringLength(30)]
        public string? SituacionTitular { get; set; }
        [StringLength(50)]
        public string? Oficina { get; set; }
        [StringLength(50)]
        public string? Ciudad { get; set; }
        [StringLength(30)]
        public string? CodigoDaneCiudad { get; set; }
        public int? TipoIdentificacion { get; set; }
        [StringLength(30)]
        public string? Identificacion { get; set; }
        [StringLength(30)]
        public string? Sector { get; set; }
        [StringLength(30)]
        public string? CaracteristicaClase { get; set; }
        [StringLength(30)]
        public string? ValorMoneda { get; set; }
        public DateTime? ValorFecha { get; set; }
        [StringLength(30)]
        public string? ValorCalificacion { get; set; }
        [StringLength(30)]
        public string? EstadoCodigo { get; set; }
        public DateTime? EstadoFecha { get; set; }
        [StringLength(50)]
        public string? Llave { get; set; }

        //  public DataCreditoBusquedum IdDataCreditoBusquedaNavigation { get; set; } = null!;
    }
}
