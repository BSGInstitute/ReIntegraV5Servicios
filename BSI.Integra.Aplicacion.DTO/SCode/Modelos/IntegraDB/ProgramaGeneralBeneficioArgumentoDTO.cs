namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ProgramaGeneralBeneficioArgumentoDTO
    {
        public int Id { get; set; }
        public int IdProgramaGeneralBeneficio { get; set; }
        public string Nombre { get; set; } = null!;
        public int IdPgeneral { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
    public class ProgramaGeneralBeneficioArgumentoComboDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
    }
    public class ProgramaGeneralBeneficioArgumentoAgendaDTO
    {
        public string Id { get; set; }
        public string IdProgramaGeneralBeneficio { get; set; }
        public string IdPGeneral { get; set; }
        public string Nombre { get; set; }
        public string PreNombre { get; set; } 
    }
}
