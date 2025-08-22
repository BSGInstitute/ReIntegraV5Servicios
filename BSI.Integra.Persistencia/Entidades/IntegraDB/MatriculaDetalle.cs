using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class MatriculaDetalle : BaseIntegraEntity
    {
        public int? IdMatriculaCabecera { get; set; }
        public int? IdCursoPespecifico { get; set; }
        public int? IdMigracion { get; set; }
    }
}
