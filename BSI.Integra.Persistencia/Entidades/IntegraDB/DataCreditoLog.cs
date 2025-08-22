using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class DataCreditoLog : BaseIntegraEntity
    {
        [StringLength(20)]
        public string? NumeroDocumento { get; set; }
        [StringLength(20)]
        public string? PrimerApellido { get; set; }
        public int? TipoIdentificacion { get; set; }
        public string RespuestaXml { get; set; } = null!;
    }
}
