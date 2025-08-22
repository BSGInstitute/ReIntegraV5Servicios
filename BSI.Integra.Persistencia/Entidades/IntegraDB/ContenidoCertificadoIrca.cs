using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class ContenidoCertificadoIrca : BaseIntegraEntity
    {
        public int IdMatriculaCabecera { get; set; }
        public int CursoIrcaId { get; set; }
        public string NombreCurso { get; set; } = null!;
        public string CodigoCurso { get; set; } = null!;
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int DuracionCurso { get; set; }
        public string ResultadoCurso { get; set; } = null!;
        public int IdCentroCostoIrca { get; set; }
        public bool Procesado { get; set; }
        public int? IdMigracion { get; set; }
    }
}
