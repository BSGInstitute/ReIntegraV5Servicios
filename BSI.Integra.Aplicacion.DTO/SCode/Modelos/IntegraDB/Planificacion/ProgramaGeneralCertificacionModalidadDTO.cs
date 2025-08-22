namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class ProgramaGeneralCertificacionModalidadDTO
    {

        public int Id { get; set; }
        public int IdProgramaGeneralCertificacion { get; set; }
        public int IdModalidadCurso { get; set; }
        public string Nombre { get; set; } = null!;
        public int IdPgeneral { get; set; }
    }
    public class CertificacionModalidadDTO
    {
        public int IdCertificacion { get; set; }
        public int IdPGeneral { get; set; }
        public string NombreCertificacion { get; set; }
        public int IdModalidadCurso { get; set; }
        public string NombreModalidad { get; set; }
        public int? IdArgumentoCertificacion { get; set; }
        public string NombreArgumentoCertificacion { get; set; }
        public int IdModalidadCertificacion { get; set; }
    }
    public class CompuestoCertificacionModalidadDTO
    {
        public int IdCertificacion { get; set; }
        public int IdPGeneral { get; set; }
        public string NombreCertificacion { get; set; }
        public List<ComboDTO> CertificacionesArgumentos { get; set; }
        public List<ModalidadCursoAlternoDTO> Modalidades { get; set; }
    }
}