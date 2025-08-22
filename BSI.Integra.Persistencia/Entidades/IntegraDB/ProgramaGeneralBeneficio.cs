using BSI.Integra.Aplicacion.Base;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class ProgramaGeneralBeneficio : BaseIntegraEntity
    {
        public int IdPgeneral { get; set; }
        [StringLength(500)]
        public string Nombre { get; set; } = null!;
        public virtual ICollection<ProgramaGeneralBeneficioArgumento> ProgramaGeneralBeneficioArgumentos { get; set; }
        public virtual ICollection<ProgramaGeneralBeneficioModalidad> ProgramaGeneralBeneficioModalidads { get; set; }
    }
}
