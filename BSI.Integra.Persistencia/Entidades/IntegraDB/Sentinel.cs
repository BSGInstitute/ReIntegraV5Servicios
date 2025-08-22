using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class Sentinel : BaseIntegraEntity
    {
        [StringLength(8)]
        public string Dni { get; set; } = null!;
        public IEnumerable<SentinelRepLegItem> SentinelRepLegItems { get; set; } = new List<SentinelRepLegItem>();
        public IEnumerable<SentinelSdtEstandarItem> SentinelSdtEstandarItems { get; set; } = new List<SentinelSdtEstandarItem>();
        public IEnumerable<SentinelSdtInfGen> SentinelSdtInfGens { get; set; } = new List<SentinelSdtInfGen>();
        public IEnumerable<SentinelSdtLincreItem> SentinelSdtLincreItems { get; set; } = new List<SentinelSdtLincreItem>();
        public IEnumerable<SentinelSdtPoshisItem> SentinelSdtPoshisItems { get; set; } = new List<SentinelSdtPoshisItem>();
        public IEnumerable<SentinelSdtRepSbsitem> SentinelSdtRepSbsitems { get; set; } = new List<SentinelSdtRepSbsitem>();
        public IEnumerable<SentinelSdtResVenItem> SentinelSdtResVenItems { get; set; } = new List<SentinelSdtResVenItem>();
    }
}
