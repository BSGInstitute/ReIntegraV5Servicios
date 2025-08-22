namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ProveedorCuentaBancoDTO
    {
        public int Id { get; set; }
        public int IdProveedor { get; set; }
        public int IdEntidadFinanciera { get; set; }
        public string NombreBanco { get; set; }
        public int IdTipoCuentaBanco { get; set; }
        public string TipoCuenta { get; set; }
        public string NroCuenta { get; set; }
        public string CuentaInterbancaria { get; set; }
        public int IdMoneda { get; set; }
        public string Moneda { get; set; }
        public string UsuarioModificacion { get; set; }
    }
    public class ProveedorCuentaBancoEnvioDTO
    {
        public int Id { get; set; }
        public int IdProveedor { get; set; }
        public int IdEntidadFinanciera { get; set; }
        public int IdTipoCuentaBanco { get; set; }
        public string NroCuenta { get; set; }
        public string CuentaInterbancaria { get; set; }
        public int IdMoneda { get; set; }
        public string UsuarioModificacion { get; set; }
    }

}
