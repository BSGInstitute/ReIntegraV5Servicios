namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion
{
    public class ProgramaGeneralPuntoCorteDTO
    {
        public int Id { get; set; }
        public int IdProgramaGeneral { get; set; }
        public decimal? PuntoCorteMedia { get; set; }
        public decimal? PuntoCorteAlta { get; set; }
        public decimal? PuntoCorteMuyAlta { get; set; }
        public int? IdPais { get; set; }
        public List<ProgramaGeneralPuntoCorteDetalleDTO> ListaPuntoCorteMedia { get; set; }
        public List<ProgramaGeneralPuntoCorteDetalleDTO> ListaPuntoCorteAlta { get; set; }
        public List<ProgramaGeneralPuntoCorteDetalleDTO> ListaPuntoCorteMuyAlta { get; set; }
    }
    public class ProgramaGeneralPuntoCorteMasivoDTO
    {
        public List<int> ListaIdPgeneral { get; set; }
        public bool AplicaTodos { get; set; }
        public List<ProgramaGeneralPuntoCorteDTO> ProgramaGeneralPuntoCorte { get; set; }
    }
    public class ProgramaGeneralPuntoCorteBaseDTO
    {
        public int Id { get; set; }
        public int IdProgramaGeneral { get; set; }
        public decimal? PuntoCorteMedia { get; set; }
        public decimal? PuntoCorteAlta { get; set; }
        public decimal? PuntoCorteMuyAlta { get; set; }
        public int? IdPais { get; set; }
    }
    public class ProgramaGeneralPuntoCorteAreaSubAreaDTO
    {
        public int? IdProgramaGeneralPuntoCorte { get; set; }
        public int IdProgramaGeneral { get; set; }
        public string NombreProgramaGeneral { get; set; }
        public decimal PuntoCorteMedia { get; set; }
        public decimal PuntoCorteAlta { get; set; }
        public decimal PuntoCorteMuyAlta { get; set; }
        public int IdAreaCapacitacion { get; set; }
        public int IdSubAreaCapacitacion { get; set; }
        public int? IdPais { get; set; }
    }
    public class ProgramaGeneralPuntoCorteFiltroDTO
    {
        public List<int> ListaIdAreaCapacitacion { get; set; }
        public List<int> ListaIdSubAreaCapacitacion { get; set; }
        public List<int> ListaIdProgramaGeneral { get; set; }
        public bool? ActivoProgramaGeneral { get; set; }
    }
    public class PuntoCorteDetalleFiltroDTO
    {
        public int IdProgramaGeneral { get; set; }
        public int IdPais { get; set; }
        public int IdPuntoCorte { get; set; }
    }
}
