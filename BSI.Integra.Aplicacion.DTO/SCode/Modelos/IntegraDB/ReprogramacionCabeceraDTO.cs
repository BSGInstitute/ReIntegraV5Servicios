namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ReprogramacionCabeceraDTO
    {
        public int Id { get; set; }
        public int IdActividadCabecera { get; set; }
        public int? IdCategoriaOrigen { get; set; }
        public int MaxReproPorDia { get; set; }
        public int IntervaloSigProgramacionMin { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
    public class ReprogramacionCabeceraRADTO
    {
        public int IntervaloSigProgramacionMin { get; set; }
        public int MaxReproPorDia { get; set; }
    }
    public class ReprogramacionCabeceraPersonalRADTO
    {
        public int ReproDia { get; set; }
    }
    public class ReprogramacionCabeceraSinAuditoriaDTO
    {
        public int Id { get; set; }
        public int IdActividadCabecera { get; set; }
        public int? IdCategoriaOrigen { get; set; }
        public int MaxReproPorDia { get; set; }
        public int IntervaloSigProgramacionMin { get; set; }
    }
}
