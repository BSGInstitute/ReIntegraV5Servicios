namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class FlujoDTO
    {
        public int Id { get; set; }
        public int IdModalidadCurso { get; set; }
        public int IdClasificacionUbicacionDocente { get; set; }
        public string Nombre { get; set; } = null!;
    }
    public class FlujoDetalleDTO
    {
        public int Id { get; set; }
        public int IdModalidadCurso { get; set; }
        public string NombreModalidadCurso { get; set; }
        public int IdClasificacionUbicacionDocente { get; set; }
        public string NombreClasificacionUbicacionDocente { get; set; }
        public string NombreFlujo { get; set; }
    }
    public class FlujoCombosDTO
    {
        public IEnumerable<ComboDTO> ComboClasificacionUbicacion { get; set; }
        public IEnumerable<ComboDTO> ComboModalidad { get; set; }
    }
}