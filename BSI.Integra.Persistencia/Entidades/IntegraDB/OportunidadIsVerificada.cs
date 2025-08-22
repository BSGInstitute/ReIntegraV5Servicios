using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class OportunidadIsVerificada : BaseIntegraEntity
    {
        public int IdOportunidad { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public bool Verificado { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
