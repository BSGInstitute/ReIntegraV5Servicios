using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class DataCreditoDataInfMicroAnalisisVector : BaseIntegraEntity
    {
        public int IdDataCreditoBusqueda { get; set; }
        [StringLength(50)]
        public string? NombreSector { get; set; }
        [StringLength(50)]
        public string? CuentaEntidad { get; set; }
        [StringLength(50)]
        public string? CuentaNumeroCuenta { get; set; }
        [StringLength(50)]
        public string? CuentaTipoCuenta { get; set; }
        [StringLength(50)]
        public string? CuentaEstado { get; set; }
        public bool? ContieneDatos { get; set; }
        public DateTime? Fecha { get; set; }
        [StringLength(50)]
        public string? SaldoDeudaTotalMora { get; set; }
        public DataCreditoBusquedum IdDataCreditoBusquedaNavigation { get; set; } = null!;
    }
}
