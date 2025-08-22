namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class CourierDTO
    {
     
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public int IdPais { get; set; }
        public int IdCiudad { get; set; }
        public string Direccion { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string Url { get; set; } = null!;
        public string? Pais { get; set; } = null!;
        public string? Ciudad { get; set; } = null!;

    }
}