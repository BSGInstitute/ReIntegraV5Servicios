using MailBee.ImapMail;
using MailBee.Mime;

namespace BSI.Integra.Aplicacion.Servicios.Service.Interface
{
    public interface ITMK_ImapService
    {
        public byte[] DownloadFileEmailInbox(int id, string correo, string pass, string nombreArchivo, string folder);
        public byte[] DownloadFileEmailInbox_ByUid(int messageNumber, string correo, string pass, string nombreArchivo, string folder);
        public MailMessage ObtenerBodyCorreo(int id, string correo, string pass, string folder);
        public MailMessage ObtenerBodyCorreo_byUid(int messageNumber, string correo, string pass, string folder);
        public int CantidadCorreosSinFiltro();
        public MessageNumberCollection CantidadCorreosConFiltroAsunto(string valor);
        public MessageNumberCollection CantidadCorreosConFiltroFecha(DateTime date);
        public UidCollection CantidadCorreosConFiltroFecha_ByUid(DateTime date);
        public MessageNumberCollection CantidadCorreosConFiltroRemitente(string valor);
        public MessageNumberCollection CantidadCorreosConFiltroDestinatario(string valor);
        public MessageNumberCollection CantidadCorreosConFiltroAsuntoRemitente(string valorAsunto, string valorRemitente);
        public EnvelopeCollection ObtenerCorreos(string indices);
        public EnvelopeCollection ObtenerCorreos_ByUid(string indices);
        public void Desconectar();
        public void Folders();


    }
}
