namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class PagoBancoDTO
    {
        public string Codigousuario { get; set; }
        public string Codigoespecial { get; set; }
        public string Fechavencimiento { get; set; }
        public string Fechapago { get; set; }
        public string Montopago { get; set; }
        public string Montomora { get; set; }
        public string Montototal { get; set; }
        public string Observaciones { get; set; }
        public string Moneda { get; set; }
        public string Cuenta { get; set; }
    }

    public class ImoprtarCrepLogDTO
    {
        public string IdMatricula { get; set; }
        public int NumeroCuota { get; set; }
        public int NumeroSubCuota { get; set; }
        public int IdPeriodo { get; set; }
        public string NumeroDocumento { get; set; }
        public string Codigousuario { get; set; }
        public string Codigoespecial { get; set; }
        public string Fechavencimiento { get; set; }
        public string Fechapago { get; set; }
        public string Montopago { get; set; }
        public string Montomora { get; set; }
        public string Montototal { get; set; }
        public string Observaciones { get; set; }
        public string Moneda { get; set; }
        public string Cuenta { get; set; }
        public string Excepcion { get; set; }
    }
}
