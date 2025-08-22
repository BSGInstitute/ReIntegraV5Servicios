namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class OtroMovimientoCajaDTO
    {
        public int Id { get; set; }
        public int IdTipoMovimientoCaja { get; set; }
        public string? NombreTipoMovimientoCaja { get; set; }
        public int? IdSubTipoMovimientoCaja { get; set; }
        public string? NombreSubTipoMovimientoCaja { get; set; }
        public decimal Precio { get; set; }
        public int IdMoneda { get; set; }
        public string? NombreMoneda { get; set; }
        public DateTime FechaPago { get; set; }
        public int? IdCentroCosto { get; set; }
        public string? NombreCentroCosto { get; set; }
        public int? IdPlanContable { get; set; }
        public string? NombrePlanContable { get; set; }
        public int IdCuentaCorriente { get; set; }
        public string? NombreCuentaCorriente { get; set; }
        public int? IdFormaPago { get; set; }
        public string? NombreFormaPago { get; set; }
        public int? IdAlumno { get; set; }
        public string? NombreAlumno { get; set; }
        public string Observaciones { get; set; }

        public string Usuario { get; set; }
    }

    public class OtroMovimientoEnvioDTO
    {
             
        
        
    }
        


       

}



