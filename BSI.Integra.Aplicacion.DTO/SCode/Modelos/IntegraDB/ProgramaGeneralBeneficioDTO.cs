namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ProgramaGeneralBeneficioDTO
    {
        public int Id { get; set; }
        public int IdPgeneral { get; set; }
        public string Nombre { get; set; } = null!;
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
    public class ProgramaGeneralBeneficioComboDTO
    {
        public int Id { get; set; }
        public int IdPgeneral { get; set; }
        public string Nombre { get; set; } = null!;
    }
    public class ProgramaGeneralBeneficioOportunidadDTO
    {
        public int IdBeneficio { get; set; }
        /* TODO Deberia ser NombreBeneficio - Sugerencia de Cambio en SP */
        public string NombrePrerequisito { get; set; } = null!;
        public int Respuesta { get; set; }
        public string Completado { get; set; } = null!;
        public List<ProgramaGeneralBeneficioArgumentoAgendaDTO> Argumentos { get; set; }
    }
    public class ProgramaGeneralBeneficioOportunidadDetalleDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public int Respuesta { get; set; }
        public string Completado { get; set; } = null!;
        public List<ProgramaGeneralBeneficioArgumentoAgendaDTO> Argumentos { get; set; } = new List<ProgramaGeneralBeneficioArgumentoAgendaDTO>();
    }
}
