namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class CuentaBancariaDTO
    {
        public int Id { get; set; }
        public string NumeroCuenta { get; set; }
        public string Moneda { get; set; }
        public int IdMoneda { get; set; }
        public string Ciudad { get; set; }
        public int IdCiudad { get; set; }
        public string NombreBanco { get; set; }
        public int IdBanco { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool EstadoCuenta { get; set; }
    }

    public class CuentaBancariaRecibidoDTO
    {
        public int Id { get; set; }
        public string NumeroCuenta { get; set; }
        public int IdMoneda { get; set; }
        public int IdCiudad { get; set; }
        public string Sucursal { get; set; }
        public string Cuenta { get; set; }
        public int IdBanco { get; set;}

    }
}
