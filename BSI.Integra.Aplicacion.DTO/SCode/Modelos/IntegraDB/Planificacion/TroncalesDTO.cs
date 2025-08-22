namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class TroncalDTO
    {
        public int Id { get; set; }
        public int IdCategoriaPrograma { get; set; }
        public int IdRegionCiudad { get; set; }
        public string TroncalCompleto { get; set; }
        public string NombreRegionCiudad { get; set; }
        public string NombreCategoriaPrograma { get; set; }
    }
    public class TroncalEntidadDTO
    {
        public int Id { get; set; }
        public int IdCategoriaPrograma { get; set; }
        public int IdRegionCiudad { get; set; }
        public string TroncalCompleto { get; set; }
    }
}
