using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class SubEstadoMatricula : BaseIntegraEntity
    {
        [StringLength(100)]
        public string Nombre { get; set; } = null!;
        public int IdEstadoMatricula { get; set; }
        public int? IdMigracion { get; set; }
        public int? AvanceAcademicoValor1 { get; set; }
        public int? AvanceAcademicoValor2 { get; set; }
        public int? IdParametrocomparativoAvanceAcademico { get; set; }
        [StringLength(50)]
        public string? IdEstadoPago { get; set; }
        public int? NotaPromedioValor1 { get; set; }
        public int? NotaPromedioValor2 { get; set; }
        public int? IdParametrocomparativoNotaPromedio { get; set; }
        public bool? TieneDeuda { get; set; }
        public bool? ProyectoFinal { get; set; }
        public bool? RequiereVerificacionInformacion { get; set; }
    }
}
