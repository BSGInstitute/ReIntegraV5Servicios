namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class NotaIngresoCajaDTO
    {
        public int Id { get; set; }
        public string CodigoNic { get; set; }
        public int IdCaja { get; set; }
        public string CodigoCaja { get; set; }
        public string ResponsableCaja { get; set; }
        public int IdOrigenIngresoCaja { get; set; }
        public string OrigenIngresoCaja { get; set; }
        public int IdPersonalEmitido { get; set; }
        public string PersonalEmitido { get; set; }
        public decimal Monto { get; set; }
        public string FechaGiro { get; set; }
        public string Concepto { get; set; }
        public string FechaCobro { get; set; }
    }
    public class NotaIngresoCajaComboDTO
    {
        public int Id { get; set; }
        public string CodigoNic { get; set; }
    }

    public class NotaIngresoEnvioDTO
    {
        public int Id { get; set; }
        public string CodigoNic { get; set; }
        public int IdCaja { get; set; }
        public int IdOrigenIngresoCaja { get; set; }
        public int IdPersonalEmitido { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaGiro { get; set; }
        public string Concepto { get; set; }
        public DateTime FechaCobro { get; set; }
        public string Usuario { get; set; }
    }

}
