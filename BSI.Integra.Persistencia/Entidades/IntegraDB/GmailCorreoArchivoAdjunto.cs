using BSI.Integra.Aplicacion.Base;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class GmailCorreoArchivoAdjunto : BaseIntegraEntity
    {

        public int IdGmailCorreo { get; set; }
        public string Nombre { get; set; } = null!;
        public string UrlArchivoRepositorio { get; set; }

        public virtual TGmailCorreo IdGmailCorreoNavigation { get; set; } = null!;
    }
}
