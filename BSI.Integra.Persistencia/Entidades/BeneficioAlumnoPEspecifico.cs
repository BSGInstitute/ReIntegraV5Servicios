using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades
{
    public class BeneficioAlumnoPEspecifico : BaseIntegraEntity
    {
        public int IdAlumno { get; set; }
        public int IdPgeneral { get; set; }
        public int IdPespecifico { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public string Beneficios { get; set; }
        public int? IdMigracion { get; set; }
    }
}
