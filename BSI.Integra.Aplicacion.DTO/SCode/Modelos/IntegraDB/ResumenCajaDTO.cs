namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ResumenCajaDTO
    {
        public int IdCaja { get; set; }
        public string CodigoCaja { get; set; } = null!;
        public int IdEmpresaAutorizada { get; set; }
        public string EmpresaAutorizada { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public string Central { get; set; } = null!;
        public string Ruc { get; set; } = null!;
        public int IdEntidadFinanciera { get; set; }
        public string EntidadFinanciera { get; set; } = null!;
        public int IdCuentaCorriente { get; set; }
        public string CuentaCorriente { get; set; } = null!;
        public int IdMoneda { get; set; }
        public string Moneda { get; set; } = null!;
        public int IdCiudad { get; set; }
        public string Ciudad { get; set; } = null!;
        public int IdPais { get; set; }
        public string Pais { get; set; } = null!;
        public string PersonalResponsable { get; set; } = null!;
    }


}
