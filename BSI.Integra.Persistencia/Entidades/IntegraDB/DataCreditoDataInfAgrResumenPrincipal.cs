using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class DataCreditoDataInfAgrResumenPrincipal : BaseIntegraEntity
    {
        public int IdDataCreditoBusqueda { get; set; }
        [StringLength(30)]
        public string? CreditosVigentes { get; set; }
        [StringLength(30)]
        public string? CreditosCerrados { get; set; }
        [StringLength(30)]
        public string? CreditosActualesNegativos { get; set; }
        [StringLength(30)]
        public string? HistNegUlt12Meses { get; set; }
        [StringLength(30)]
        public string? CuentasAbiertasAhoccb { get; set; }
        [StringLength(30)]
        public string? CuentasCerradasAhoccb { get; set; }
        [StringLength(30)]
        public string? ConsultadasUlt6meses { get; set; }
        [StringLength(30)]
        public string? DesacuerdosAlaFecha { get; set; }
        [StringLength(30)]
        public string? ReclamosVigentes { get; set; }
        public DataCreditoBusquedum IdDataCreditoBusquedaNavigation { get; set; } = null!;
    }
}
