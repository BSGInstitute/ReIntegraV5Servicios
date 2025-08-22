namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class CursoMoodleDTO
    {
        public string CodigoMatricula { get; set; }
        public int IdUsuario { get; set; }
        public int IdCurso { get; set; }
        public string NombreCurso { get; set; }
        public int IdMatriculaMoodle { get; set; }
    }
    public class CostosAdministrativosDTO
    {
        public int Id { get; set; }
        public string Concepto { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public string Moneda { get; set; }
        public bool Gestionado { get; set; }
        public double Monto { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UrlDocumento { get; set; }
        public string FechaEntregaEstimada { get; set; }
        public string FechaEntregaReal { get; set; }
        public bool? SolicitudCF { get; set; }
        public int? IdEstadoCertificadoFisico { get; set; }
        public int? IdCertificadoGeneradoAutomatico { get; set; }
    }
    public class EstructuraCursoDTO
    {
        public string Contenido { get; set; }
    }
}
