using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class MedioPagoMatriculaCronograma : BaseIntegraEntity
    {
        public int IdMatriculaCabecera { get; set; }
        public int IdMedioPago { get; set; }
        public bool Activo { get; set; }
    }
}
