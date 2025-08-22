using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Operaciones
{
    public class ProcesoPagoIvr : BaseIntegraEntity
    {
        public int IdAlumno { get; set; }
        public int? IdTransaccionAuditoriaPago { get; set; }
        public int? IdPersonal { get; set; }
        public string? Celular { get; set; }
        public string? Anexo { get; set; }
    }
}
