using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Text;

namespace BSI.Integra.Aplicacion.Base.Classes
{
    public class HelperCorreo
    {
        private static string correo = "matriculas@bsginstitute.com";
        private static string clave = "xcgvgxdccmgkujqp";//"lgosgssloxfdnlyz";
        private static string alias = "matriculas@bsginstitute.com";

        public static string correonotificacionesfacturas = "notificacionesfacturas@bsginstitute.com";
        public static string clavenotificacionesfacturas = "xuktephsjvqcyaky";
        private static string aliasfacturas = "matriculas@bsginstitute.com";

        private static string host = "smtp.gmail.com";
        private static int port = 587;

        public bool envio_emailSinCopia(string email, string displayname, string subject, string mensaje)
        {
            using (SmtpClient smtp = new SmtpClient())
            {
                try
                {
                    // CONFIGURACION DEL MENSAJE
                    MailMessage mail = new MailMessage();
                    mail.To.Add(email);

                    mail.From = new MailAddress(correo, displayname, System.Text.Encoding.UTF8);
                    mail.Subject = subject;
                    mail.Body = mensaje;
                    mail.BodyEncoding = System.Text.Encoding.UTF8;
                    mail.IsBodyHtml = true;
                    //CONFIGURACIÓN DEL STMP

                    smtp.Host = host;
                    smtp.Port = port;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new System.Net.NetworkCredential
                    (correo, clave);// Enter seders User name and password
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
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

        public bool envio_email(string email, string displayname, string subject, string mensaje, string documentos)
        {
            using (SmtpClient smtp = new SmtpClient())
            {
                try
                {
                    // CONFIGURACION DEL MENSAJE
                    MailMessage mail = new MailMessage();
                    mail.To.Add(email);
                    mail.From = new MailAddress(alias, displayname, System.Text.Encoding.UTF8);
                    mail.Subject = subject;
                    mail.Body = mensaje;
                    mail.BodyEncoding = System.Text.Encoding.UTF8;
                    mail.IsBodyHtml = true;
                    //CONFIGURACIÓN DEL STMP

                    smtp.Host = host;
                    smtp.Port = port;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new System.Net.NetworkCredential
                    (correo, clave);// Enter seders User name and password
                    smtp.EnableSsl = true;

                    var palabras = documentos.Split(',');

                    //Attachment adjDCT;

                    foreach (string word in palabras)
                    {
                        var adjDCT = new System.Net.Mail.Attachment(word);
                        mail.Attachments.Add(adjDCT);
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

        public bool envio_email(string email, string displayname, string subject, string mensaje, List<string> correos)
        {

            using (SmtpClient smtp = new SmtpClient())
            {
                try
                {
                    // CONFIGURACION DEL MENSAJE
                    MailMessage mail = new MailMessage();
                    mail.To.Add(email);

                    foreach (var item in correos)
                    {
                        mail.Bcc.Add(item);
                    }

                    mail.From = new MailAddress(alias, displayname, System.Text.Encoding.UTF8);
                    mail.Subject = subject;
                    mail.Body = mensaje;
                    mail.BodyEncoding = System.Text.Encoding.UTF8;
                    mail.IsBodyHtml = true;
                    //CONFIGURACIÓN DEL STMP

                    smtp.Host = host;
                    smtp.Port = port;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new System.Net.NetworkCredential
                    (correo, clave);// Enter seders User name and password
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
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

        public bool envioEmailAdjuntoBlob(string email, string displayname, string subject, string mensaje, string nombreDocumentos, byte[] archivoBytes, List<string> correos)
        {
            using (SmtpClient smtp = new SmtpClient())
            {
                try
                {
                    //CONFIGURACION DEL MENSAJE
                    MailMessage mail = new MailMessage();
                    mail.To.Add(email);

                    foreach (var item in correos)
                    {
                        mail.Bcc.Add(item);
                    }

                    mail.From = new MailAddress(correo, displayname, System.Text.Encoding.UTF8);
                    mail.Subject = subject;
                    mail.Body = mensaje;
                    mail.BodyEncoding = System.Text.Encoding.UTF8;
                    mail.IsBodyHtml = true;
                    //CONFIGURACIÓN DEL STMP

                    smtp.Host = host;
                    smtp.Port = port;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new System.Net.NetworkCredential(correo, clave);// Enter seders User name and password
                    smtp.EnableSsl = true;

                    mail.Attachments.Add(new Attachment(new MemoryStream(archivoBytes), nombreDocumentos));

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

        public string mensaje_email_simple(string email, string password, string url)
        {
            string mensaje = string.Empty;

            mensaje += "<div style='padding: 0px 0px 0px 0px; background-color:#dddddd;'><table cellpadding='0' cellspacing='0' width='660' height='63' align='center' style='background-color:#094D82'><tr><td align='center'><a href='https://bsginstitute.com'><picture><img src='https://bsginstitute.com/a/repositorioweb/img/logo-mail.png' width='170' style='margin-top:5px; margin-bottom:5px;' /></picture></a></td></tr></table><table cellpadding='0' cellspacing='0' width='660' align='center'><tr><td><table style='background-color:#FFFFFF;'><tr><td><p style='vertical-align:top;text-align:left;font-family:Arial,Helvetica,sans-serif;line-height:18px;font-size:14px;font-weight:bold;color:#353535;text-decoration:none; margin-left: 10px;'>Estimado Sr(a). </p><p style='font-size:12px; font-family:Arial,Helvetica,sans-serif; text-align:justify;margin-left: 10px;margin-right: 10px;'>";
            mensaje += "Continuando con su proceso de matrícula, se le envia sus datos para poder identificarse, los cuales debe de utilizar, por favor haga clic en el boton de <b>Completar Matricula</b>, el cual lo redirigirá a nuestro portal y poder concluir con su proceso de matrícula.";
            mensaje += "</p></td></tr></table></td></tr><tr><td style='font-family:Arial,Helvetica,sans-serif;'><div style='background:#EEEEEE; padding-left:30px; padding-right:30px;'><p style='font-size:14px; font-family:Arial,Helvetica,sans-serif; color:#F5A623;font-weight:bold; padding-left:5px; padding-top:15px; padding-bottom:10px; margin-top:0px; margin-bottom:0px;'>Sus datos de acceso son:</p></div><div style='background:#F6F6F6; padding-left:30px; padding-right:30px;'>";
            mensaje += "<table cellspacing='10' style='width:100%; font-size:12px;'><tr>";
            mensaje += "<td style='border-left:solid; width:50%; border-left-color:#7CAEE9; padding:5px;'><b>Usuario:</b><span> " + email + "</span></td><td></td>";
            mensaje += "<td style='border-left:solid; width:50%; border-left-color:#7CAEE9; padding:5px;'><b>Clave:</b><span> " + password + "</span></td>";
            mensaje += "</tr></table><table style='width:100%;'><tr><td align='center' style='text-align:center;'><br><a href='" + url + "' style='padding: 10px 16px;font-size: 16px;background-color: #F5A623;border-color: #eea236;color: #fff;border-radius: 6px; text-decoration:none; display:block;'>Completar Matricula</a><br />";
            mensaje += "<p> Ó copie el siguiente enlace:</p><div style='padding: 5px;width: 100%;background: #fff;border: 1px solid transparent;border-color: #ddd;border-radius: 5px;color: #000;'>" + url + "</div><br>";
            mensaje += "</td></tr></table></div></td></tr></table><table cellpadding='0' cellspacing='0' width='660' height='25' align='center' style='background-color:#094D82; font-family:Arial; font-size:10px;'><tr><td align='center' style='color:#FFFFFF;'>© " + DateTime.Now.Year.ToString() + " BSG Institute, todos los derechos reservados.</td></tr></table></div>";

            return mensaje;
        }

        public bool envio_email_resumen_grabaciones(string email, string displayname, string subject, string mensaje, List<string> correos)
        {
            string correoResumen = "resumen@bsginstitute.com";
            string claveResumen = "lpkk uqsy gkgk weom";
            string aliasResumen = "resumen@bsginstitute.com";
            using (SmtpClient smtp = new SmtpClient())
            {
                try
                {
                    // CONFIGURACION DEL MENSAJE
                    MailMessage mail = new MailMessage();
                    mail.To.Add(email);

                    foreach (var item in correos)
                    {
                        mail.Bcc.Add(item);
                    }

                    mail.From = new MailAddress(aliasResumen, displayname, System.Text.Encoding.UTF8);
                    mail.Subject = subject;
                    mail.Body = mensaje;
                    mail.BodyEncoding = System.Text.Encoding.UTF8;
                    mail.IsBodyHtml = true;
                    //CONFIGURACIÓN DEL STMP

                    smtp.Host = host;
                    smtp.Port = port;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new System.Net.NetworkCredential
                    (correoResumen, claveResumen);// Enter seders User name and password
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
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

        public bool envioEmailResumenGrabacion(string email, string displayname, string subject, string mensaje, List<string> correos, List<string> archivo)
        {
            string correoResumen = "resumen@bsginstitute.com";
            string claveResumen = "lpkk uqsy gkgk weom";
            string aliasResumen = "resumen@bsginstitute.com";
            using (SmtpClient smtp = new SmtpClient())
            {
                try
                {
                    // CONFIGURACION DEL MENSAJE
                    MailMessage mail = new MailMessage();
                    mail.To.Add(email);

                    foreach (var item in correos)
                    {
                        mail.Bcc.Add(item);
                    }

                    mail.From = new MailAddress(aliasResumen, displayname, System.Text.Encoding.UTF8);
                    mail.Subject = subject;
                    mail.Body = mensaje;
                    mail.BodyEncoding = System.Text.Encoding.UTF8;
                    mail.IsBodyHtml = true;
                    //CONFIGURACIÓN DEL STMP
                    foreach (var itemFile in archivo)
                    {
                        // Adjuntar el archivo descargado
                        Attachment attachment = new Attachment(itemFile);
                        mail.Attachments.Add(attachment);
                    }
                    

                    smtp.Host = host;
                    smtp.Port = port;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new System.Net.NetworkCredential
                    (correoResumen, claveResumen);// Enter seders User name and password
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
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
        public static bool EnvioCorreoNotificacionesFacturas(List<string> CorreosDestinatarios, string displayname, string subject, string mensaje)
        {
            using (SmtpClient smtp = new SmtpClient())
            {
                try
                {
                    //CONFIGURACION DEL MENSAJE
                    MailMessage mail = new MailMessage();
                    if (CorreosDestinatarios != null)
                    {
                        foreach (var correo in CorreosDestinatarios)
                        {
                            mail.To.Add(correo);
                        }
                    }
                    mail.From = new MailAddress(aliasfacturas, displayname, System.Text.Encoding.UTF8);
                    mail.Subject = subject;
                    mail.Body = mensaje;
                    mail.BodyEncoding = System.Text.Encoding.UTF8;
                    mail.IsBodyHtml = true;
                    //CONFIGURACIÓN DEL STMP

                    smtp.Host = host;
                    smtp.Port = port;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new System.Net.NetworkCredential(correonotificacionesfacturas, clavenotificacionesfacturas);// Enter seders User name and password
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
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
    }
}
