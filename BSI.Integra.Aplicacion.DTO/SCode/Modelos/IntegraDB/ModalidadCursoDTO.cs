
namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ModalidadCursoDTO
    {
        public int? Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Codigo { get; set; } = null!;
    }
    public class ModalidadCursoProblemaDTO
    {
        public int? Id { get; set; }
        public string? Nombre { get; set; }
        public int IdModalidad { get; set; }
    }

    public class ModalidadCursoPresentacionArgumentoDTO
    {
        public int? Id { get; set; }
        public string? Nombre { get; set; }
        public int IdModalidad { get; set; }
    }
    public class ModalidadCursoAlternoDTO
    {
        public int? Id { get; set; }
        public string? Nombre { get; set; }
        public int IdModalidadCurso { get; set; }
    }
    public class CompuestoProblemaModeloCertificadoDTO
    {
        public int IdModeloCertificado { get; set; }
        public int IdPGeneral { get; set; }
        public string NombreModeloCertificado { get; set; }
        public string UrlModeloCertificado { get; set; }
        public List<ModalidadCursoAlternoDTO> Modalidades { get; set; }
    }
    public class ModalidadProgramaDTO
    {
        public string Tipo { get; set; }
        public string Ciudad { get; set; }
        public string TipoCiudad { get; set; }
        public string FechaHoraInicio { get; set; }
        public string NombrePG { get; set; }
        public int IdPEspecifico { get; set; }
        public string NombreESP { get; set; }
        public string NombreCentroCosto { get; set; }
        public string Duracion { get; set; }
        public string Pw_duracion { get; set; }
        public DateTime? FechaReal { get; set; }
    }
    public class ModalidadProgramaSincronicoDTO
    {
        public int IdPEspecifico { get; set; }
        public string ProgramaEspecifico { get; set; }
        public string Modalidad { get; set; }
        public string EstadoPEspecifico { get; set; }
        public string Pais { get; set; }
        public string FechaInicioSesion { get; set; }
    }
}
