namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Operaciones
{
    public class ProcesoPagoIvrDTO
    {
        public int? Id { get; set; }
        public int IdAlumno { get; set; }
        public int? IdTransaccionAuditoriaPago { get; set; }
        public int? IdPersonal { get; set; }
        public string? Celular { get; set; }
        public string? Anexo { get; set; }
    }
}
