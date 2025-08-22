using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class SentinelSueldoPorIndustriaDataDinamico : BaseIntegraEntity
    {
        public int? IdCargo { get; set; }
        public int? IdIndustria { get; set; }
        public int? IdTamanioEmpresa { get; set; }
        [StringLength(300)]
        public string? Nombre { get; set; }
        public int? Tipo { get; set; }
        public int? Valor { get; set; }
    }
}
