using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class CentroCosto : BaseIntegraEntity
    {
        public int? IdArea { get; set; }
        public int? IdSubArea { get; set; }
        [StringLength(50)]
        public string IdPgeneral { get; set; } = null!;
        [StringLength(150)]
        public string Nombre { get; set; } = null!; 
        [StringLength(50)]
        public string Codigo { get; set; } = null!;
        [StringLength(10)]
        public string? IdAreaCc { get; set; }
        public int? Ismtotales { get; set; }
        public int? Icpftotales { get; set; }
        public ICollection<CampaniaGeneralDetalle> CampaniaGeneralDetalles { get; set; }
        public ICollection<PEspecifico> Pespecificos { get; set; }
    }
}
