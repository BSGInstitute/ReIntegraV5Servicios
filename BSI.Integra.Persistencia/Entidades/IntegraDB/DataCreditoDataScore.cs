using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class DataCreditoDataScore : BaseIntegraEntity
    {
        public int IdDataCreditoBusqueda { get; set; }
        [StringLength(20)]
        public string? Tipo { get; set; }
        [StringLength(40)]
        public string? Puntaje { get; set; }
        public DateTime? Fecha { get; set; }
        [StringLength(40)]
        public string? Poblacion { get; set; }
        public DataCreditoBusquedum IdDataCreditoBusquedaNavigation { get; set; } = null!;
    }
}
