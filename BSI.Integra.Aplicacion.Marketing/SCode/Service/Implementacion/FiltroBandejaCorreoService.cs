using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Marketing.Service.Interface;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Http;
using System.Net.Mail;

namespace BSI.Integra.Aplicacion.Marketing.Service.Implementacion
{
    /// Service: FiltroBandejaCorreoService
    /// Autor: Gilmer Quispe.
    /// Fecha: 04/11/2022
    /// <summary>
    /// Gestión general de FiltroBandejaCorreo
    /// </summary>
    public class FiltroBandejaCorreoService : IFiltroBandejaCorreoService
    {
        private IUnitOfWork _unitOfWork;

        public FiltroBandejaCorreoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 04/11/2022
        /// <summary>
        /// Enviar correo con archivo adjunto.
        /// </summary>
        /// <param name="Correo"> Correo. </param>
        /// <param name="Clave"> Contraseña del Correo. </param>
        /// <param name="mailData"> Datos del correo. </param>
        /// <param name="Files"> Archivos adjuntos. </param>
        /// <returns>Booleano</returns>
        public bool envioEmailAdjunto(string Correo, string Clave, TMKMailDataDTO mailData, IList<IFormFile> Files)
        {
            string host = "smtp.gmail.com";
            int port = 587;

            using (var smtp = new SmtpClient())
            {
                try
                {
                    //CONFIGURACION DEL MENSAJE
                    var mail = new MailMessage();
                    mail.To.Add(mailData.Recipient);

                    if (!string.IsNullOrEmpty(mailData.Cc))
                    {
                        var copiasCC = mailData.Cc.Split(',');

                        foreach (var copiaCC in copiasCC)
                        {
                            mail.CC.Add(copiaCC);
                        }
                    }
                    if (!string.IsNullOrEmpty(mailData.Bcc))
                    {
                        var copiasBcC = mailData.Bcc.Split(',');

                        foreach (var copiaBcc in copiasBcC)
                        {
                            mail.CC.Add(copiaBcc);
                        }
                    }
                    mail.From = new MailAddress(Correo, mailData.RemitenteC, System.Text.Encoding.UTF8);
                    mail.Subject = mailData.Subject;
                    mail.Body = mailData.Message;
                    mail.BodyEncoding = System.Text.Encoding.UTF8;
                    mail.IsBodyHtml = true;
                    //CONFIGURACIÓN DEL STMP
                    smtp.Host = host;
                    smtp.Port = port;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new System.Net.NetworkCredential(Correo, Clave);// Enter seders User name and password
                    smtp.EnableSsl = true;
                   
                    foreach (var file in Files)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            var fileBytes = ms.ToArray();
                            mail.Attachments.Add(new Attachment(new MemoryStream(fileBytes), file.FileName));
                        }
                    }
                    smtp.Send(mail);
                    mail.Dispose();
                    smtp.Dispose();
                    return true;
                }
                catch (Exception ex)
                {
                    smtp.Dispose();
                    return false;
                }
            }
        }

        public EstadoCorreoSmtpDTO envioEmailAdjuntoOperaciones(string Correo, string Clave, TMKMailDataDTO mailData, IList<IFormFile> Files)
        {
            string host = "smtp.gmail.com";
            int port = 587;
            EstadoCorreoSmtpDTO respuestaEnvio = new ();
            using (var smtp = new SmtpClient())
            {
                try
                {
                    //CONFIGURACION DEL MENSAJE
                    var mail = new MailMessage();
                    mail.To.Add(mailData.Recipient);

                    if (!string.IsNullOrEmpty(mailData.Cc))
                    {
                        var copiasCC = mailData.Cc.Split(',');

                        foreach (var copiaCC in copiasCC)
                        {
                            mail.CC.Add(copiaCC);
                        }
                    }
                    if (!string.IsNullOrEmpty(mailData.Bcc))
                    {
                        var copiasBcC = mailData.Bcc.Split(',');

                        foreach (var copiaBcc in copiasBcC)
                        {
                            mail.CC.Add(copiaBcc);
                        }
                    }
                    mail.From = new MailAddress(Correo, mailData.RemitenteC, System.Text.Encoding.UTF8);
                    mail.Subject = mailData.Subject;
                    mail.Body = mailData.Message;
                    mail.BodyEncoding = System.Text.Encoding.UTF8;
                    mail.IsBodyHtml = true;
                    //CONFIGURACIÓN DEL STMP
                    smtp.Host = host;
                    smtp.Port = port;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new System.Net.NetworkCredential(Correo, Clave);// Enter seders User name and password
                    smtp.EnableSsl = true;

                    foreach (var file in Files)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            var fileBytes = ms.ToArray();
                            mail.Attachments.Add(new Attachment(new MemoryStream(fileBytes), file.FileName));
                        }
                    }
                    smtp.Send(mail);
                    mail.Dispose();
                    smtp.Dispose();
                    respuestaEnvio.codigo = "200";
                    respuestaEnvio.respuesta = "Ok";
                    return respuestaEnvio;
                }
                catch (SmtpException smtpEx)
                {
                    smtp.Dispose();
                    respuestaEnvio.codigo = smtpEx.StatusCode.ToString();
                    respuestaEnvio.respuesta = smtpEx.Message;
                    return respuestaEnvio;
                }
            }
        }

        /// Autor: Gilmer Quispe.
        /// Fecha: 10/11/2022
        /// <summary>
        /// Obtener bandejar de entrada de correo en speech de oportunidad.
        /// </summary>
        /// <returns>BandejaCorreoDTO</returns>
        public BandejaCorreoDTO ObtenerBandejaEntradaMailInboxSpeech(FiltroBandejaCorreoDTO filtroBandejaCorreoDTO)
        {
            string rango_datos = string.Empty;
            int tipoFolder = 0;
            string _queryFiltro = string.Empty;
            try
            {
                var bandejaCorreo = new BandejaCorreoDTO();
                var objGmail = new CorreoGmailService(_unitOfWork);
                bandejaCorreo.ListaCorreos = new List<CorreoDTO>();

                if (filtroBandejaCorreoDTO.Folder == "inbox")
                {
                    tipoFolder = 1;
                }
                else if (filtroBandejaCorreoDTO.Folder == "[Gmail]/Enviados")
                {
                    tipoFolder = 3;
                }
                if (filtroBandejaCorreoDTO.FiltroKendo == null || filtroBandejaCorreoDTO.FiltroKendo.Filters.Count == 0)
                {
                    _queryFiltro = "";
                    bandejaCorreo.ListaCorreos = objGmail.FiltroCorreosPorPersona(tipoFolder, _queryFiltro);
                }
                else if (filtroBandejaCorreoDTO.FiltroKendo.Filters.Count == 1)
                {
                    switch (filtroBandejaCorreoDTO.FiltroKendo.Filters[0].Field)
                    {
                        case "Asunto":
                            _queryFiltro = " AND Asunto like '%" + filtroBandejaCorreoDTO.FiltroKendo.Filters[0].Value + "%'";
                            bandejaCorreo.ListaCorreos = objGmail.FiltroCorreosPorPersona(tipoFolder, _queryFiltro);
                            break;
                        case "Remitente":
                        case "Destinatario":
                            if (tipoFolder == 1)
                            {
                                if (filtroBandejaCorreoDTO.TipoCorreos == "Normal")
                                {
                                    _queryFiltro = " AND EmailRemitente like '%" + filtroBandejaCorreoDTO.FiltroKendo.Filters[0].Value + "%' and (EmailConCopiaOculta<>'modpru@bsginstitute.com' and EmailConCopiaOculta<>'modpru@bsgrupo.com')";
                                }
                                else
                                {
                                    _queryFiltro = " AND EmailRemitente like '%" + filtroBandejaCorreoDTO.FiltroKendo.Filters[0].Value + "%' and (EmailConCopiaOculta='modpru@bsginstitute.com' or EmailConCopiaOculta='modpru@bsgrupo.com')";
                                }
                            }
                            else if (tipoFolder == 3)
                            {
                                if (filtroBandejaCorreoDTO.TipoCorreos == "Normal")
                                {
                                    _queryFiltro = " AND Destinatarios like '%" + filtroBandejaCorreoDTO.FiltroKendo.Filters[0].Value + "%' and (EmailConCopiaOculta<>'modpru@bsginstitute.com' and EmailConCopiaOculta<>'modpru@bsgrupo.com')";
                                }
                                else
                                {
                                    _queryFiltro = " AND Destinatarios like '%" + filtroBandejaCorreoDTO.FiltroKendo.Filters[0].Value + "%' and (EmailConCopiaOculta='modpru@bsginstitute.com' or EmailConCopiaOculta='modpru@bsgrupo.com')";
                                }
                            }
                            bandejaCorreo.ListaCorreos = objGmail.FiltroCorreosPorPersona(tipoFolder, _queryFiltro);
                            break;
                    }
                }
                else if (filtroBandejaCorreoDTO.FiltroKendo.Filters.Count == 2)
                {
                    foreach (var item in filtroBandejaCorreoDTO.FiltroKendo.Filters)
                    {
                        switch (item.Field)
                        {
                            case "Asunto":
                                _queryFiltro += " AND Asunto like '%" + filtroBandejaCorreoDTO.FiltroKendo.Filters[0].Value + "%' ";
                                break;
                            case "Remitente":
                            case "Destinatario":
                                if (tipoFolder == 1)
                                {
                                    _queryFiltro = " AND EmailRemitente like '%" + filtroBandejaCorreoDTO.FiltroKendo.Filters[0].Value + "%'";
                                }
                                else if (tipoFolder == 3)
                                {
                                    _queryFiltro = " AND Destinatarios like '%" + filtroBandejaCorreoDTO.FiltroKendo.Filters[0].Value + "%'";
                                }
                                break;
                        }
                    }
                    bandejaCorreo.ListaCorreos = objGmail.FiltroCorreosPorPersona(tipoFolder, _queryFiltro);
                }
                if (bandejaCorreo.ListaCorreos != null)
                {
                    bandejaCorreo.ListaCorreos = bandejaCorreo.ListaCorreos.OrderByDescending(x => x.Fecha).ToList();
                    bandejaCorreo.TotalEnviados = bandejaCorreo.ListaCorreos.Count;
                }
                return bandejaCorreo;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 14/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtener correos por grupos
        /// </summary>
        /// <param name="IdCentroCosto"> Id del centro de costo</param>
        /// <param name="IdPaquete"> Id de paquete </param>
        /// <param name="Estados"> Estados </param>
        /// <param name="SubEstados"> Sub estados </param>
        /// <returns> Booleano </returns>
        public ListaCorreosGrupoDTO ObtenerCorreosGrupos(int idCentroCosto, int idPaquete, List<int> estado, List<int> subEstado)
        {
            try
            {
                CorreoGmailService correoGmailService = new CorreoGmailService(_unitOfWork);

                ListaCorreosGrupoDTO respuesta = new ListaCorreosGrupoDTO();
                if (idPaquete == 0)
                {
                    respuesta = correoGmailService.ObtenerCorreosGruposSinVersion(idCentroCosto, estado, subEstado);
                }
                else
                {
                    respuesta = correoGmailService.ObtenerCorreosGruposConVersion(idCentroCosto, idPaquete, estado, subEstado);
                }


                if (respuesta != null)
                {
                    return respuesta;

                }
                else
                {
                    ListaCorreosGrupoDTO respuestaSinDatos = new ListaCorreosGrupoDTO();
                    respuestaSinDatos.ListaCorreos = "";
                    respuestaSinDatos.TotalCorreos = 0;
                    respuestaSinDatos.Errores = true;

                    return respuestaSinDatos;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
