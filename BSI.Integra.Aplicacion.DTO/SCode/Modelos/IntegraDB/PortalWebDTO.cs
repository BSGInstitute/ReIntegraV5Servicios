namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class AccesoPortalWebDTO
    {
        public string UserName { get; set; }
        public string Clave { get; set; }
    }
    public class NumeroOrdenDTO
    {
        public int Id { get; set; }
        public long ContadorActual { get; set; }
        public string NombreServicio { get; set; }
    }
    public class TransaccionAuditoriaPagoDTO
    {
        public int Id { get; set; }
        public int IdAlumno { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public string IdentificadorTransaccion { get; set; }
        public string RegistroTransaccionJson { get; set; }
        public int IdPasarelaPago { get; set; }
        public string MedioPago { get; set; }
        public string? Anexo { get; set; }
        public decimal MontoTotal { get; set; }
        public string Moneda { get; set; }
    }
    public class TransaccionAuditoriaPagoRespuestaDTO
    {
        public int Id { get; set; }
        public int IdAlumno { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public string IdentificadorTransaccion { get; set; }
        public int IdPasarelaPagoPw { get; set; }
        public string MedioPago { get; set; }
        public string Anexo { get; set; }
        public decimal MontoTotal { get; set; }
        public string Moneda { get; set; }
    }
}
