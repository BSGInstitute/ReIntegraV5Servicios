namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class CourierDetalleDTO
    {
     
        public int Id { get; set; }
        public int IdCourier { get; set; }
        public int IdPais { get; set; }
        public int IdCiudad { get; set; }
        public string Direccion { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public int TiempoEnvio { get; set; }
        public string? Pais { get; set; } = null!;
        public string? Ciudad { get; set; } = null!;
 
    }
}