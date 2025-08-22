namespace BSI.Integra.Aplicacion.DTOs
{
    public class NotaIngresoCajaDatosPdfDTO
    {
        public int IdNotaIngresoCaja { get; set; }
        public string CodigoNic { get; set; }
        public string CodigoCaja { get; set; }
        public string RazonSocial { get; set; }
        public string Direccion { get; set; }
        public string Ruc { get; set; }
        public string Central { get; set; }
        public string CuentaCaja { get; set; }
        public string Origen { get; set; }
        public string FechaGiro { get; set; }
        public string PersonalEmitido { get; set; }
        public string Concepto { get; set; }
        public decimal Monto { get; set; }
        public string Moneda { get; set; }
        public string PersonalResponsable { get; set; }
        public string DniResponsable { get; set; }
    }
}
