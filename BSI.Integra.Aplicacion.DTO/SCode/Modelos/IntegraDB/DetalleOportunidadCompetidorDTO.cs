namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class DetalleOportunidadCompetidorDTO
    {
        public int Id { get; set; }
        public int IdOportunidadCompetidor { get; set; }
        public int IdCompetidor { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
    public class DetalleOportunidadCompetidorComboDTO
    {
        public int Id { get; set; }
        public string Competidor { get; set; } = null!;
        public int IdOportunidad { get; set; }
    }
    public class DetalleOportunidadCompetidorEmpresaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
    }
    public class DetalleOportunidadOperacionesDTO
    {
        public int IdOportunidad { get; set; }
        public int IdCentroCosto { get; set; }
        public string NombreCentroCosto { get; set; }
        public int IdProgramaGeneral { get; set; }
        public string NombreProgramaGeneral { get; set; }
        public string NombreCiudad { get; set; }
        public int EscalaCalificacion { get; set; }
    }
}
