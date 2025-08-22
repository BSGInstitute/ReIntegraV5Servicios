using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class SentinelSueldoIndividual : BaseIntegraEntity
    {
        [StringLength(20)]
        public string? Dni { get; set; }
        [StringLength(300)]
        public string? Nombres { get; set; }
        [StringLength(300)]
        public string? ApellidoPaterno { get; set; }
        [StringLength(300)]
        public string? ApellidoMaterno { get; set; }
        [StringLength(300)]
        public string? Industria { get; set; }
        public int? IdIndustria { get; set; }
        [StringLength(300)]
        public string? TamanioEmpresa { get; set; }
        public int? IdTamanioEmpresa { get; set; }
        [StringLength(300)]
        public string? EmpresaNombre { get; set; }
        public int? IdEmpresa { get; set; }
        [StringLength(300)]
        public string? Cargo { get; set; }
        public int? IdCargo { get; set; }
        [StringLength(300)]
        public string? AreaTrabajo { get; set; }
        public int? IdAreaTrabajo { get; set; }
        [StringLength(300)]
        public string? AreaFormacion { get; set; }
        public int? IdAreaFormacion { get; set; }
        [StringLength(300)]
        public string? Ciudad { get; set; }
        public int? IdCodigoCiudad { get; set; }
        [StringLength(300)]
        public string? Pais { get; set; }
        public int? IdCodigoPais { get; set; }
        public int? SeLimiteInferior { get; set; }
        public int? SeLimiteSuperior { get; set; }
        public double? SePromedio { get; set; }
        [StringLength(300)]
        public string? OrigenInformacion { get; set; }
        public bool? Incluir { get; set; }
    }
}
