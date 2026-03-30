using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Servicios.Service.Interface;
using MailBee.ImapMail;
using MailBee.Mime;

namespace BSI.Integra.Aplicacion.Servicios.Service.Implementacion
{
    public class TMK_ImapService : ITMK_ImapService
    {
        private static string _licenseMailbee = "MN110-41C9EF25D7AB64A6D1A21D2E5135-4113";
        private Imap _imap = new Imap(_licenseMailbee);

        public TMK_ImapService()
        {
        }

        public TMK_ImapService(string folder, string email, string passwordCorreo)
        {
            try
            {
                _imap.Connect("imap.gmail.com", 993);
                _imap.Login(email, passwordCorreo);
                folder = NormalizarCarpetaGmail(folder);
                _imap.SelectFolder(@folder);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al acceder a la carpeta '{folder}': {ex.Message}");
            }
        }
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 19/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obitene el archivo adjunto de un correo electronico
        /// </summary>
        /// <param name="id">Id del correo electronico</param>
        /// <param name="correo">Cuenta del usuario</param>
        /// <param name="pass">Contraseña del correo electronico</param>
        /// <param name="nombreArchivo">Nombre del archivo a descargar</param>
        /// <returns>Retorna el archivo adjunto en un arreglode bytes</returns>
        public byte[] DownloadFileEmailInbox(int id, string correo, string pass, string nombreArchivo, string folder)
        {
            byte[] archivo = null;
            try
            {
                folder = NormalizarCarpetaGmail(folder);
                MailMessage msg = Imap.QuickDownloadMessage("imap.gmail.com", correo, pass, folder, id);
                if (msg.Attachments.Count > 0)
                {
                    foreach (Attachment attach in msg.Attachments)
                    {
                        if (attach.Filename.Contains("%2c"))
                        {
                            nombreArchivo = nombreArchivo.Replace(",", "%2c");
                        }
                        if (attach.Filename == nombreArchivo)
                        {
                            archivo = attach.GetData();
                            break;
                        }
                    }
                }
                if (archivo != null)
                    return archivo;
                else
                    throw new Exception("No se encontro el Archivo Indicado");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 19/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obitene el archivo adjunto de un correo electronico por Uid
        /// </summary>
        /// <param name="messageNumber">Numero de mensaje del correo electronico</param>
        /// <param name="correo">Cuenta del usuario</param>
        /// <param name="pass">Contraseña del correo electronico</param>
        /// <param name="nombreArchivo">Nombre del archivo a descargar</param>
        /// <returns>Retorna el archivo adjunto en un arreglode bytes</returns>
        public byte[] DownloadFileEmailInbox_ByUid(int messageNumber, string correo, string pass, string nombreArchivo, string folder)
        {
            byte[] archivo = null;
            try
            {
                folder = NormalizarCarpetaGmail(folder);
                var msg = Imap.QuickDownloadMessage("imap.gmail.com", correo, pass, folder, messageNumber);
                if (msg.Attachments.Count > 0)
                {
                    foreach (Attachment attach in msg.Attachments)
                    {
                        if (attach.Filename.Contains("%2c"))
                        {
                            nombreArchivo = nombreArchivo.Replace(",", "%2c");
                        }
                        if (attach.Filename == nombreArchivo)
                        {
                            archivo = attach.GetData();
                            break;
                        }
                    }
                }
                if (archivo != null)
                    return archivo;
                else
                    throw new Exception("No se encontro el Archivo Indicado");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 19/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obitene el cuerpo de un correo electronico
        /// </summary>
        /// <param name="id">Id del correo electronico</param>
        /// <param name="correo">Cuenta del usuario</param>
        /// <param name="pass">Contraseña del correo electronico</param>
        /// <param name="folder">nombre del folder del correo</param>
        /// <returns>MailMessage</returns>
        public MailMessage ObtenerBodyCorreo(int id, string correo, string pass, string folder)
        {
            try
            {
                folder = NormalizarCarpetaGmail(folder);
                MailMessage mensaje = Imap.QuickDownloadMessage("imap.gmail.com", correo, pass, folder, id);
                return mensaje;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Carlos Crispin.
        /// Fecha: 27/03/2026
        /// Version: 1.0
        /// <summary>
        /// Obitene el cuerpo de un correo electronico
        /// </summary>
        /// <param name="Listid">Id del correo electronico</param>
        /// <param name="correo">Cuenta del usuario</param>
        /// <param name="pass">Contraseña del correo electronico</param>
        /// <param name="folder">nombre del folder del correo</param>
        /// <returns>MailMessage</returns>
        public async Task<bool> MarcarComoNoLeidoGmail(int id, string correo, string pass, string folder)
        {
            try
            {
                folder = NormalizarCarpetaGmail(folder);

                //foreach (var idunico in id)
                //{
                    await _imap.SetMessageFlagsAsync(id.ToString(), true, @"\Seen", MessageFlagAction.Remove, true);
                //}
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 19/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obitene el cuerpo de un correo electronico por Uid
        /// </summary>
        /// <param name="messageNumber">Numero de mensaje del correo electronico</param>
        /// <param name="correo">Cuenta del usuario</param>
        /// <param name="pass">Contraseña del correo electronico</param>
        /// <param name="folder">nombre del folder del correo</param>
        /// <returns>Returna el archivo adjunto en un arreglode bytes</returns>
        public MailMessage ObtenerBodyCorreo_byUid(int messageNumber, string correo, string pass, string folder)
        {
            try
            {
                folder = NormalizarCarpetaGmail(folder);
                MailMessage mensaje = Imap.QuickDownloadMessage("imap.gmail.com", correo, pass, folder, messageNumber);
                return mensaje;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 19/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obitene la cantidad de correos sin filtro
        /// </summary>
        /// <returns>int</returns>
        public int CantidadCorreosSinFiltro()
        {
            try
            {
                return _imap.MessageCount;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 19/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obitene cantidad de corres con filtro por asunto
        /// </summary>
        /// <param name="valor">Asunto del correo electronico</param>
        /// <returns>MessageNumberCollection</returns>
        public MessageNumberCollection CantidadCorreosConFiltroAsunto(string valor)
        {
            try
            {
                MessageNumberCollection NumeroMensajes = (MessageNumberCollection)_imap.Search(false, "SUBJECT " + ImapUtils.ToQuotedString(valor), null);
                return NumeroMensajes;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 19/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obitene cantidad de corres con filtro por fecha
        /// </summary>
        /// <param name="date">Fehca del correo electronico</param>
        /// <returns>MessageNumberCollection</returns>
        public MessageNumberCollection CantidadCorreosConFiltroFecha(DateTime date)
        {
            try
            {
                //MessageNumberCollection NumeroMensajes = (MessageNumberCollection)_imap.Search(false, "ON  " + ImapUtils.ToQuotedString(valor), null);
                MessageNumberCollection NumeroMensajes = (MessageNumberCollection)_imap.Search(false, "SINCE \"" + ImapUtils.GetImapDateString(date) + "\"", null);

                return NumeroMensajes;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 19/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obitene cantidad de corres con filtro por fecha por Uid
        /// </summary>
        /// <param name="date">Fehca del correo electronico</param>
        /// <returns>UidCollection</returns>
        public UidCollection CantidadCorreosConFiltroFecha_ByUid(DateTime date)
        {
            try
            {
                var stringDate = ImapUtils.GetImapDateString(date);
                UidCollection NumeroMensajes = (UidCollection)_imap.Search(true, "SINCE \"" + ImapUtils.GetImapDateString(date) + "\"", null);
                return NumeroMensajes;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 19/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obitene cantidad de corres con filtro por remitente
        /// </summary>
        /// <param name="valor">Remitente del correo electronico</param>
        /// <returns>MessageNumberCollection</returns>
        public MessageNumberCollection CantidadCorreosConFiltroRemitente(string valor)
        {
            try
            {
                MessageNumberCollection NumeroMensajes = (MessageNumberCollection)_imap.Search(false, "FROM " + ImapUtils.ToQuotedString(valor), System.Text.Encoding.UTF8.WebName);
                return NumeroMensajes;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 19/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obitene cantidad de corres con filtro por destinatario
        /// </summary>
        /// <param name="valor">Destinatario del correo electronico</param>
        /// <returns>MessageNumberCollection</returns>
        public MessageNumberCollection CantidadCorreosConFiltroDestinatario(string valor)
        {
            try
            {
                MessageNumberCollection NumeroMensajes = (MessageNumberCollection)_imap.Search(false, "TO " + ImapUtils.ToQuotedString(valor), System.Text.Encoding.UTF8.WebName);
                return NumeroMensajes;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 19/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obitene cantidad de corres con filtro por asunto y remitente
        /// </summary>
        /// <param name="valorAsunto">Asunto del correo electronico</param>
        /// <param name="valorRemitente">Remitente del correo electronico</param>
        /// <returns>MessageNumberCollection</returns>
        public MessageNumberCollection CantidadCorreosConFiltroAsuntoRemitente(string valorAsunto, string valorRemitente)
        {
            try
            {
                MessageNumberCollection NumeroMensajes = (MessageNumberCollection)_imap.Search(false, "SUBJECT " + ImapUtils.ToQuotedString(valorAsunto) + " FROM " + ImapUtils.ToQuotedString(valorRemitente), System.Text.Encoding.UTF8.WebName);
                return NumeroMensajes;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 19/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obitene los correos por indice
        /// </summary>
        /// <param name="indices">Indice del correo electronico</param>
        /// <returns>EnvelopeCollection</returns>
        public EnvelopeCollection ObtenerCorreos(string indices)
        {
            try
            {
                EnvelopeCollection listaCorreos = _imap.DownloadEnvelopes(indices, false, EnvelopeParts.All, 0);
                return listaCorreos;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 19/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obitene los correos por indice por Uid
        /// </summary>
        /// <param name="indices">Indice del correo electronico</param>
        /// <returns>EnvelopeCollection</returns>
        public EnvelopeCollection ObtenerCorreos_ByUid(string indices)
        {
            try
            {
                EnvelopeCollection listaCorreos = _imap.DownloadEnvelopes(indices, true, EnvelopeParts.All, 0);
                return listaCorreos;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 12/03/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene correos nuevos basados en el último UID procesado.
        /// </summary>
        /// <param name="lastUid">Último UID guardado en BD. Si es 0, sincroniza los últimos 30 días.</param>
        /// <returns>Colección de sobres (Envelopes)</returns>
        public EnvelopeCollection ObtenerCorreosNuevosDesdeUid(long lastUid)
        {
            UidCollection nuevosUids;
            if (lastUid == 0)
            {
                DateTime sinceDate = DateTime.Now.AddDays(-30);
                nuevosUids = (UidCollection)_imap.Search(true, "SINCE \"" + ImapUtils.GetImapDateString(sinceDate) + "\"", null);
            }
            else
            {
                nuevosUids = (UidCollection)_imap.Search(true, $"UID {lastUid + 1}:*", null);

                // IMAP puede devolver el último UID existente si lastUid+1 no existe.
                // Filtramos UIDs que ya fueron procesados.
                if (nuevosUids != null && nuevosUids.Count > 0)
                {
                    var filtrados = new UidCollection();
                    foreach (long uid in nuevosUids)
                    {
                        if (uid > lastUid)
                        {
                            filtrados.Add(uid);
                        }
                    }
                    nuevosUids = filtrados;
                }
            }

            if (nuevosUids != null && nuevosUids.Count > 0)
            {
                return _imap.DownloadEnvelopes(nuevosUids.ToString(), true, EnvelopeParts.All, 0);
            }

            return new EnvelopeCollection();
        }

        /// Autor: Jashin Salazar Taco.
        /// Fecha: 19/08/2022
        /// Version: 1.0
        /// <summary>
        /// Desconectar
        /// </summary>
        /// <returns>void</returns>
        public void Desconectar()
        {
            try
            {
                _imap.Disconnect();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 19/08/2022
        /// Version: 1.0
        /// <summary>
        /// Folders
        /// </summary>
        /// <returns>void</returns>
        public void Folders()
        {
            try
            {
                var datos = _imap.DownloadFolders(false);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        private static string NormalizarCarpetaGmail(string folder)
        {
            switch (folder.ToLower())
            {
                case "spam": return "[Gmail]/Spam";
                case "trash": return "[Gmail]/Papelera";
                case "drafts": return "[Gmail]/Borradores";
                case "starred": return "[Gmail]/Destacados";
                case "snoozed": return "[Gmail]/Pospuestos";
                case "[gmail]/spam": return "[Gmail]/Spam";
                case "[gmail]/trash": return "[Gmail]/Papelera";
                case "[gmail]/drafts": return "[Gmail]/Borradores";
                case "[gmail]/starred": return "[Gmail]/Destacados";
                case "[gmail]/snoozed": return "[Gmail]/Pospuestos";
                default: return folder;
            }
        }
    }
}
