namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ProgramaGeneralProblemaDetalleSolucionDTO
    {
        public int Id { get; set; }
        public int IdProgramaGeneralProblema { get; set; }
        public string Detalle { get; set; } = null!;
        public string Solucion { get; set; } = null!;
        public int IdPgeneral { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
    public class ProgramaGeneralProblemaDetalleSolucionComboDTO
    {
        public int Id { get; set; }
        public int IdProgramaGeneralProblema { get; set; }
        public string Detalle { get; set; } = null!;
        public string Solucion { get; set; } = null!;
    }
    public class ProgramaGeneralProblemaDetalleSolucionAgendaDTO
    {
        public int Id { get; set; }
        public int IdProgramaGeneralProblema { get; set; }
        public string Detalle { get; set; } = null!;
        public string Solucion { get; set; } = null!;
        public bool Solucionado { get; set; }
        public bool Seleccionado { get; set; }
    }
    public class ProgramaGeneralProblemaDetalleSolucionAgendaNuevaAgendaDTO
    {
        public int Id { get; set; }
        public int IdProgramaGeneralProblema { get; set; }
        public bool CheckValor { get; set; }
        public string Cabecera { get; set; } = null!;
        public string Caso { get; set; } = null!;
    }
    public class ProgramaGeneralProblemaArgumentoDetalleSolucionDTO
    {
        public int? Id { get; set; }
        public string? Detalle { get; set; }
        public string? Solucion { get; set; }
    }
    public class ProblemaDetalleSolucionDTO
    {
        public int? IdProgramaGeneralProblema { get; set; }
        public string? Detalle { get; set; }
        public string? Solucion { get; set; }
        public int? IdPgeneral { get; set; }
        public int? IdMigracion { get; set; }
    }
    public class ProblemaDetalleSolucionAlternoDTO
    {
        public int? Id { get; set; }
        public string Detalle { get; set; }
        public string Solucion { get; set; }
    }

}
