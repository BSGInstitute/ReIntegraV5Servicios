using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ProgramaGeneralProblemaModalidadDTO
    {
        public int Id { get; set; }
        public int IdProgramaGeneralProblema { get; set; }
        public int IdModalidadCurso { get; set; }
        public string Nombre { get; set; } = null!;
        public int IdPgeneral { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
    public class ProblemaModalidadDTO
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public int? IdModalidadCurso { get; set; }
    }
    public class ProblemaModalidadAlternoDTO
    {
        public int IdProblema { get; set; }
        public int IdPGeneral { get; set; }
        public string NombreProblema { get; set; }
        public int IdModalidadCurso { get; set; }
        public string NombreModalidad { get; set; }
        public int? IdArgumentoProblema { get; set; }
        public string DetalleArgumentoProblema { get; set; }
        public string SolucionArgumentoProblema { get; set; }
        public int IdModalidadProblema { get; set; }
        public bool EsVisibleAgenda { get; set; }
    }
    public class ProblemaModalidadVentasDTO
    {
        public int? IdProgramaGeneralProblema { get; set; }
        public int? IdModalidadCurso { get; set; }
        public string? Nombre { get; set; }
        public int? IdPgeneral { get; set; }
        public int? IdMigracion { get; set; }
    }
    public class CompuestoProblemaModalidadAlternoDTO
    {
        public int IdProblema { get; set; }
        public int IdPGeneral { get; set; }
        public string NombreProblema { get; set; }
        public bool EsVisibleAgenda { get; set; }
        public List<ProblemaDetalleSolucionAlternoDTO> ProblemasArgumentos { get; set; }
        public List<ModalidadCursoAlternoDTO> Modalidades { get; set; }
    }
    public class CompuestoProblemaModalidadDTO
    {
        public int IdProblema { get; set; }
        public int IdPGeneral { get; set; }
        public string? NombreProblema { get; set; }
        public bool EsVisibleAgenda { get; set; }
        public List<ProgramaGeneralProblemaArgumentoDetalleSolucionDTO>? ProblemasArgumentos { get; set; }
        public List<ModalidadCursoProblemaDTO>? Modalidades { get; set; }
    }
    public class CompuestoPresentacionArgumentoModalidadDTO
    {
        public int IdPresentacionArgumento { get; set; }
        public int IdPGeneral { get; set; }
        public string NombrePresentacionArgumento { get; set; }
        public string? DescripcionPresentacionArgumento { get; set; }
        public bool EsVisibleAgenda { get; set; }
        public List<PresentacionArgumentoDetalleSolucionAlternoDTO> PresentacionArgumento { get; set; }
        public List<ModalidadCursoPresentacionArgumentoDTO> Modalidades { get; set; }
    }
    public class CompuestoPresentacionArgumentoModalidadAlternoDTO
    {
        public int IdPresentacionArgumento { get; set; }
        public int IdPGeneral { get; set; }
        public string NombrePresentacionArgumento { get; set; }
        public string? DescripcionPresentacionArgumento { get; set; }
        public bool EsVisibleAgenda { get; set; }
        public List<PresentacionArgumentoDetalleSolucionAlternoDTO>? PresentacionArgumento { get; set; }
        public List<ModalidadCursoAlternoDTO>? Modalidades { get; set; }
    }
}
