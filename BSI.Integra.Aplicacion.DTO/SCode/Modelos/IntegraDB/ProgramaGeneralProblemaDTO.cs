namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ProgramaGeneralProblemaDTO
    {
        public int Id { get; set; }
        public int IdPgeneral { get; set; }
        public string Nombre { get; set; } = null!;
        public bool EsVisibleAgenda { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
    public class ProgramaGeneralProblemaComboDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;

    }
    public class ProgramaGeneralProblemaAgendaDTO
    {
        public int IdProblema { get; set; }
        public string NombreProblema { get; set; } = null!;
        public int Respuesta { get; set; }
        public string Completado { get; set; } = null!;
    }
    public class ProgramaGeneralProblemaDetalleAgendaDTO
    {
        public int IdProblema { get; set; }
        public string NombreProblema { get; set; } = null!;
        public int Respuesta { get; set; }
        public string Completado { get; set; } = null!;
        public List<ProgramaGeneralProblemaDetalleSolucionAgendaDTO> Argumentos { get; set; } = new List<ProgramaGeneralProblemaDetalleSolucionAgendaDTO>();
    }
    public class ProgramaGeneralProblemaDetalleAgendaNuevaAgendaDTO
    {
        public int IdProblema { get; set; }
        public string NombreProblema { get; set; } = null!;
        public int Respuesta { get; set; }
        public string Completado { get; set; } = null!;
        public List<ProgramaGeneralProblemaDetalleSolucionAgendaNuevaAgendaDTO> Argumentos { get; set; } = new List<ProgramaGeneralProblemaDetalleSolucionAgendaNuevaAgendaDTO>();
    }
    public class ProgramaGeneralProblemaArgumentoModalidadDTO
    {
        public int IdProblema { get; set; }
        public int IdPGeneral { get; set; }
        public string NombreProblema { get; set; } = null!;
        public int IdModalidadCurso { get; set; }
        public string NombreModalidad { get; set; } = null!;
        public int? IdArgumentoProblema { get; set; }
        public string? DetalleArgumentoProblema { get; set; }
        public string? SolucionArgumentoProblema { get; set; }
        public int IdModalidadProblema { get; set; }
    }
    public class ProgramaGeneralProblemaArgumentoModalidadDetalleDTO
    {
        public int IdProblema { get; set; }
        public int IdPGeneral { get; set; }
        public string NombreProblema { get; set; } = null!;
        public List<ProgramaGeneralProblemaArgumentoDetalleSolucionDTO> Argumentos { get; set; } = new List<ProgramaGeneralProblemaArgumentoDetalleSolucionDTO>();
        public List<ProblemaModalidadDTO> Modalidades { get; set; } = new List<ProblemaModalidadDTO>();
    }
    public class ProblemaVentasDTO
    {
        public CompuestoProblemaModalidadDTO? Problemas { get; set; }
        public string? Usuario { get; set; }
        public int? IdPGeneral { get; set; }
    }
    public class ProgramaGeneralProblemaVentasDTO
    {
        public int? IdPgeneral { get; set; }
        public string? Nombre { get; set; }
        public int? IdMigracion { get; set; }
        public bool EsVisibleAgenda { get; set; }
        public List<ProblemaDetalleSolucionDTO>? ProgramaGeneralProblemaModalidad { get; set; }
        public List<ProblemaModalidadVentasDTO>? programaGeneralProblemaDetalleSolucion { get; set; }
    }
}
