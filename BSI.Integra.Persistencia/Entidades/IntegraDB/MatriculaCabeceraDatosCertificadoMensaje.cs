using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class MatriculaCabeceraDatosCertificadoMensaje : BaseIntegraEntity
    {
        public int IdMatriculaCabecera { get; set; }
        public int IdPersonalRemitente { get; set; }
        public int IdPersonalReceptor { get; set; }
        public string Mensaje { get; set; } = null!;
        public string ValorAntiguo { get; set; } = null!;
        public string ValorNuevo { get; set; } = null!;
        public bool EstadoMensaje { get; set; }
    }
}
