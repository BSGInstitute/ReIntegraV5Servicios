namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ContenidoCertificadoIrcaDTO
    {
        public int Id { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public string CodigoMatricula { get; set; }
        public int CursoIrcaId { get; set; }
        public string NombreCurso { get; set; } = null!;
        public string CodigoCurso { get; set; } = null!;
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int DuracionCurso { get; set; }
        public string ResultadoCurso { get; set; } = null!;
        public int IdCentroCostoIrca { get; set; }
        public string CentroCostoIrca { get; set; }
        public bool Procesado { get; set; }
        public string Usuario { get; set; }
    }
    public class VistaPreviaCertificadoIrcaDTO
    {
        public int Id { get; set; }
        public int IdPespecifico { get; set; }
    }
}
