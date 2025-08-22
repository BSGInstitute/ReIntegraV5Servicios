using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class MatriculaCabeceraDatosCertificado : BaseIntegraEntity
    {
        public int IdMatriculaCabecera { get; set; }
        public string Duracion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }
        public string NombreCurso { get; set; }
        public bool EstadoCambioDatos { get; set; }

        public int IdCertificadoGeneradoAutomatico { get; set; }
    }
}
