using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class PlanContable : BaseIntegraEntity
    {
        public long Cuenta { get; set; }
        [StringLength(400)]
        public string Descripcion { get; set; } = null!;
        public int Padre { get; set; }
        public bool? Univel { get; set; }
        [StringLength(50)]
        public string Cbal { get; set; } = null!;
        [StringLength(50)]
        public string Debe { get; set; } = null!;
        [StringLength(50)]
        public string Haber { get; set; } = null!;
        public int? IdPlanContableTipoCuenta { get; set; }
        [StringLength(50)]
        public string? Analisis { get; set; }
        [StringLength(50)]
        public string? CentroCosto { get; set; }
        public int? IdFurTipoSolicitud { get; set; }
    }
}
