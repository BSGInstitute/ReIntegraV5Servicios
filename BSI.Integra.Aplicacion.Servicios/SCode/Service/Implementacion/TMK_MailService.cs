using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using Mandrill;
using Mandrill.Models;
using Mandrill.Requests.Messages;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace BSI.Integra.Aplicacion.Servicios.Service.Implementacion
{
    public class TMK_MailService
    {
        //CUENTA OFICIAL         //CUENTA BAKCUP
        string apiKey = "CHRrBNYkBMXLaEr3MFEw0A";//"y75iN2N_tKNZcCm6bFYwrw";//"iQrOwKtuJAVJwIselVGiyA";
        string toEmail = "";
        string fromEmail = "";
        string EmailResult = "";
        private TMKMailDataDTO _mailData;
        private string remitentecarlos = "";
        private bool _haveFiles;
        MandrillApi _api;

        public TMK_MailService()
        {
            //_api = new MandrillApi();

        }
        /// Autor: _ _ _ _ _ _ _ .
        /// Fecha: 22/06/2021
        /// Version: 1.0
        /// <summary>
        /// Modifica información de Correo para envío
        /// </summary>
        /// <param name="data">Información de Correo</param>
        /// <returns></returns>
        public void SetData(TMKMailDataDTO data)
        {
            _mailData = data;
        }
        /// Autor: _ _ _ _ _ _ _ .
        /// Fecha: 22/06/2021
        /// Version: 1.0
        /// <summary>
        /// Envía mensaje
        /// </summary>
        /// <returns>List<TMKMensajeIdDTO></returns>
        public List<TMKMensajeIdDTO> SendMessageTask()
        {
            List<TMKMensajeIdDTO> mensaje_id = new List<TMKMensajeIdDTO>();
            //var rest = Task.Run(async () => { await SendMessageWithFiles(); }).Wait();
            Task.Run(async () => { mensaje_id = await SendMessageWithFiles(); }).Wait();
            return mensaje_id;
        }
        private async Task<List<TMKMensajeIdDTO>> SendMessageWithFiles()
        {
            MandrillApi _api = new MandrillApi(apiKey);
            char[] delimitor = new char[] { ',' };
            List<EmailAddress> Recipients = new List<EmailAddress>();
            if (!String.IsNullOrEmpty(_mailData.Recipient))
            {
                string[] RecipientList = _mailData.Recipient.Split(delimitor, StringSplitOptions.RemoveEmptyEntries);
                foreach (string EmailAddress in RecipientList)
                {
                    EmailAddress Recipient = new EmailAddress();
                    Recipient.Email = EmailAddress.Trim();
                    Recipient.Type = "to";
                    Recipients.Add(Recipient);
                }
            }
            if (!string.IsNullOrEmpty(_mailData.Cc))
            {
                string[] CopiedList = _mailData.Cc.Split(delimitor, StringSplitOptions.RemoveEmptyEntries);
                foreach (string EmailAddress in CopiedList)
                {
                    EmailAddress Copied = new EmailAddress();
                    Copied.Email = EmailAddress.Trim();
                    Copied.Type = "cc";
                    Recipients.Add(Copied);
                }
            }
            if (!string.IsNullOrEmpty(_mailData.Bcc))
            {
                string[] CopiedList = _mailData.Bcc.Split(delimitor, StringSplitOptions.RemoveEmptyEntries);
                foreach (string EmailAddress in CopiedList)
                {
                    EmailAddress Copied = new EmailAddress
                    {
                        Email = EmailAddress.Trim(),
                        Type = "bcc"
                    };
                    Recipients.Add(Copied);
                }
            }

            List<EmailResult> Results = await _api.SendMessage(new SendMessageRequest(new EmailMessage
            {
                FromEmail = _mailData.Sender,
                FromName = _mailData.RemitenteC,//"BS Grupo - Cursos y Diplomas",
                Subject = _mailData.Subject,
                Html = string.Format("<body><p>{0}</p></body>", _mailData.Message),
                Attachments = _mailData.AttachedFiles,
                PreserveRecipients = true,
                To = Recipients,
                TrackClicks = true,
                TrackOpens = true,
                Images = _mailData.EmbeddedFiles
            })).ConfigureAwait(false);
            List<TMKMensajeIdDTO> mensaje_id = new List<TMKMensajeIdDTO>();
            TMKMensajeIdDTO mensaje;
            foreach (var rpta in Results)
            {
                mensaje = new TMKMensajeIdDTO
                {
                    MensajeId = rpta.Id,
                    Email = rpta.Email,
                    Estado = rpta.Status.ToString()
                };
                mensaje_id.Add(mensaje);
            }
            return mensaje_id;
            //var rpta = Results[0].Id.ToString();
            //return Results[0].Status.ToString() == "sent";
        }
        public List<EmailAttachment> GetAttachmentList(string urlBrochure)
        {
            List<EmailAttachment> attachment_TMP = new List<EmailAttachment>();
            if (!string.IsNullOrEmpty(urlBrochure))
            {
                //Here we check if exist the file
                WebRequest webRequest = WebRequest.Create(urlBrochure);
                webRequest.Timeout = 1200;
                webRequest.Method = "HEAD";
                HttpWebResponse response = null;
                try
                {
                    response = (HttpWebResponse)webRequest.GetResponse();
                    //So far, we know that if the file exists
                    string fileName = Path.GetFileName(urlBrochure);
                    byte[] buffer = null;
                    using (var client = new WebClient())
                    {
                        var tmp_buffer = client.DownloadData(urlBrochure);
                        buffer = tmp_buffer;
                    }
                    var FileBytes = Convert.ToBase64String(buffer);
                    EmailAttachment objAttach = new EmailAttachment
                    {
                        Content = FileBytes,
                        Name = fileName,
                        Type = GetMimeType(urlBrochure)
                    };
                    attachment_TMP.Add(objAttach);
                }
                catch (WebException webException)
                {

                }
                finally
                {
                    if (response != null)
                    {
                        response.Close();
                    }
                }
            }
            return attachment_TMP;
        }
        private string GetMimeType(string fileName)
        {
            string mimeType = "application/unknown";
            string ext = Path.GetExtension(fileName).ToLower();
            Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
            if (regKey != null && regKey.GetValue("Content Type") != null)
                mimeType = regKey.GetValue("Content Type").ToString();
            return mimeType;
        }
        List<EmailAttachment> Attached = new List<EmailAttachment>();
        public bool SetFiles(IFormFile files)
        {
            //foreach (var file in files)
            //{

            //if (hpf.Length == 0)
            //    continue;
            EmailAttachment objAdjunto = new Mandrill.Models.EmailAttachment();
            IFormFile filex;
            filex = files;
            byte[] hola = ConvertToByte(filex);
            var BytesArchivo = Convert.ToBase64String(hola);

            objAdjunto.Content = BytesArchivo;
            objAdjunto.Name = filex.FileName;
            objAdjunto.Type = filex.ContentType;
            Attached.Add(objAdjunto);
            //}
            if (Attached.Count != 0)
            {
                this._mailData.AttachedFiles = Attached;
                return true;
            }
            else
            {
                return false;
            }
        }
        public byte[] ConvertToByte(IFormFile file)
        {
            BinaryReader rdr = new BinaryReader(file.OpenReadStream());
            byte[] imageByte = rdr.ReadBytes((int)file.Length);
            return imageByte;
        }
        public async Task<UserInfo> Info()
        {
            MandrillApi api = new MandrillApi(apiKey);
            UserInfo info = await api.UserInfo();
            Console.WriteLine(info.Reputation);
            return info;
        }
        public TMKMailDataDTO VerifyData()
        {
            return _mailData;
        }
    }
}
