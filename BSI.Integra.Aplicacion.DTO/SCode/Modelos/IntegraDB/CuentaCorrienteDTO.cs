namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class CuentaCorrientesDTO
    {
        public int IdCta { get; set; }
        public string NumeroCuenta { get; set; }
        public int IdCiudad { get; set; }
        public string Ciudad { get; set; }
        public string NombreEntidadFinanciera { get; set; }
    }
    public class CuentaCorrienteComboDTO
    {
        public int Id { get; set; }
        public string NumeroCuenta { get; set; } = null!;
        public int IdMoneda { get; set; }
        public int IdBanco { get; set; }
    }

    public class DatosCuentaCorrienteDTO
    {
        public int IdCta { get; set; }
        public string Id { get; set; }
        public string Cuenta { get; set; }
    }
    public class CuentasCorrienteDTO
    {
        public int IdCta { get; set; }
        public string NumeroCuenta { get; set; }
        public int IdCiudad { get; set; }
        public string Ciudad { get; set; }
        public string NombreEntidadFinanciera { get; set; }
    }
}
