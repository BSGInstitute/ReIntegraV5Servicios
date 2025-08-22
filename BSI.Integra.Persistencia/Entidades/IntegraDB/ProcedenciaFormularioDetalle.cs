using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class ProcedenciaFormularioDetalle : BaseIntegraEntity
    {
        public int IdProcedenciaFormulario { get; set; }
        public int IdTipoInteraccion { get; set; }
    }
}
