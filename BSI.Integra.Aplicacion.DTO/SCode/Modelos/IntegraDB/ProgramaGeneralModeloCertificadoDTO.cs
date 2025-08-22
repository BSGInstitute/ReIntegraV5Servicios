namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ProgramaGeneralModeloCertificadoDTO
    {
        public int IdModelo { get; set; }
        public string NombreModelo { get; set; }
        public string UrlModelo { get; set; }
    }
    public class ModeloModalidadDTO
    {
        public int IdModeloCertificado { get; set; }
        public int IdPGeneral { get; set; }
        public string NombreModeloCertificado { get; set; }
        public int IdModalidadCurso { get; set; }
        public string NombreModalidad { get; set; }
        public int IdModalidadModelo { get; set; }
        public string UrlModeloCertificado { get; set; }
    }
}
