using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Servicios.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System.Net.Mail;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: OportunidadInformacionService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 20/08/2022
    /// <summary>
    /// Gestión general de Informacion de Oportunidades
    /// </summary>
    public class CorreoService : ICorreoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public CorreoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            //var config = new MapperConfiguration(cfg => cfg.CreateMap<TEntity, Entity>(MemberList.None).ReverseMap());
            //_mapper = new Mapper(config);
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 20/08/2022
        /// Version: 1.0
        /// <summary>
        /// Envia correo sin Copia
        /// </summary>
        /// <param name="email">Correo Destinatario</param>
        /// <param name="displayname">DisplayName</param>
        /// <param name="subject">Asunto de Correo</param>
        /// <param name="mensaje">Mensaje de Correo</param>
        /// <returns> bool </returns>
        public bool EnvioEmailSinCopia(string email, string displayname, string subject, string mensaje)
        {
            using (SmtpClient smtp = new SmtpClient())
            {
                try
                {
                    // CONFIGURACION DEL MENSAJE
                    MailMessage mail = new MailMessage();
                    mail.To.Add(email);

                    string correo = "matriculas@bsginstitute.com";
                    string clave = "kjhhxbosejaqszbi";
                    string host = "smtp.gmail.com";
                    int port = 587;

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
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 20/08/2022
        /// Version: 1.0
        /// <summary>
        /// Envia Correo
        /// </summary>
        /// <param name="email">Correo Destinatario</param>
        /// <param name="displayname">DisplayName</param>
        /// <param name="subject">Asunto de Correo</param>
        /// <param name="mensaje">Mensaje de Correo</param>
        /// <param name="correos">Lista de Copia</param>
        /// <returns> bool </returns>
        public bool EnvioEmail(string email, string displayname, string subject, string mensaje, List<string> correos)
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

                    string correo = "matriculas@bsginstitute.com";
                    string clave = "kjhhxbosejaqszbi";
                    string alias = "matriculas@bsginstitute.com";
                    string host = "smtp.gmail.com";
                    int port = 587;

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
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 20/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el mensaje asociado al Proceso de Pago de Peru
        /// </summary>
        /// <param name="email">Correo del Alumno</param>
        /// <param name="password">Contraseña del Alumno</param>
        /// <param name="contacto">Datos del Contacto</param>
        /// <param name="listaCronogama">Cronograma de Pagos</param>
        /// <param name="pgeneral">Nombre de Programa General</param>
        /// <param name="codigoMatricula">Codigo de Matricula del Alumno</param>
        /// <param name="pais">Nombre del Pais</param>
        /// <param name="simboloMoneda">Simbolo de la moneda asociada</param>
        /// <returns> string </returns>
        public string ObtenerMensajeCorreoProcesoPagoPeru(string email, string password, string url, AlumnoDTO contacto, List<MontoPagoCronogramaDetalleDTO> listaCronogama, string pgeneral, string codigoMatricula, string pais, string simboloMoneda)
        {
            string mensaje = string.Empty, montoCuota = string.Empty;
            string[] datosAdicionales = codigoMatricula.Split('-');
            int contador = 0;

            mensaje += "Estimado(a) " + contacto.Nombre1 + "<br><br>";
            mensaje += "<p style='font-size:10pt;'>Según lo conversado le envió el código de alumno correspondiente a su inscripción en el <b>" + pgeneral + "</b><p>";
            mensaje += "<p style='font-size:10pt;'><b>NOMBRE DEL PARTICIPANTE:</b> " + contacto.Nombre1 + " " + contacto.Nombre2 + " " + contacto.ApellidoPaterno + " " + contacto.ApellidoMaterno + "</p>";

            if (pais == "CO")
            {
                mensaje += "<p style='font-size:10pt;'><b>Nro Convenio Bancolombia:</b> 56470<p>";
                mensaje += "<p style='font-size:10pt;'><b>Nro de Referencia:</b> " + codigoMatricula + "<p>";
            }
            else
            {
                mensaje += "<p style='font-size:10pt;'><b>CODIGO DE ALUMNO:</b> " + codigoMatricula + "</p>";
            }

            mensaje += "<p style='font-size:10pt;'><b>CRONOGRAMA DE PAGOS:</b>" + "</p>";

            mensaje += "<table style='border: 1px solid #e6e6e6;border-collapse:collapse' border='' cellspacing='0' cellpadding='2'>";
            mensaje += "<tbody>";
            mensaje += "<tr>";
            mensaje += "<th style='border: 1px solid #e6e6e6' bgcolor='#FAFAFA'>Descripcion</th>";
            mensaje += "<th style='border: 1px solid #e6e6e6' bgcolor='#FAFAFA'>Fecha pago</th>";
            mensaje += "<th style='border: 1px solid #e6e6e6' bgcolor='#FAFAFA'>Monto cuota con descuento</th>";
            mensaje += "</tr>";

            foreach (var item in listaCronogama)
            {
                contador = contador + 1;
                if (contador == 1)
                {
                    montoCuota = item.MontoCuotaDescuento.ToString("0.00");
                }
                mensaje += "<tr>";
                mensaje += "<th style='border: 1px solid #e6e6e6; font-weight: 100;'>" + item.CuotaDescripcion.ToString() + "</th>";
                mensaje += "<th style='border: 1px solid #e6e6e6; font-weight: 100;'>" + item.FechaPago.ToString("dd/MM/yyyy") + "</th>";
                mensaje += "<th style='border: 1px solid #e6e6e6; font-weight: 100;' align='right'>" + simboloMoneda + " " + item.MontoCuotaDescuento.ToString("0.00") + "</th>";
                mensaje += "</tr>";
            }
            mensaje += "</tbody>";
            mensaje += "</table>";

            if (pais == "PE")
            {
                mensaje += "<p style='font-size:10pt;'><b><u>SOBRE EL PAGO:</u></b></p>";
                mensaje += "<p>Usted puede realizar el pago con su código de alumno mediante: </p>";
                mensaje += "<ul>";
                mensaje += "<li><span style='font -size:10pt'>Sitio Web de BSG Institute</span></li>";
                mensaje += "<li><span style='font -size:10pt'>Sitio Web VIABCP (si cuenta con clave de internet y clave token) </span></li>";
                mensaje += "<li><span style='font -size:10pt'>Oficinas BSG Institute</span></li>";
                mensaje += "<li><span style='font -size:10pt'>En los locales del BCP (Ventanilla)</span></li>";
                mensaje += "<li><span style='font -size:10pt'>Agente BCP</span></li>";
                mensaje += "</ul>";

                mensaje += "<p style='font-size:10pt;'><b><u>Por el Sitio Web de BSG Institute</u></b></p>";
                mensaje += "<br>";

                mensaje += "<p>Ingrese al siguiente enlace:</p>";
                mensaje += "<p><a href='" + url + "'>" + url + "</a></p><br>";
                mensaje += "<p style='font-size:10pt;'><b>Sus datos de acceso son</b></p>";
                mensaje += "<ul>";
                mensaje += "<li><b>Usuario:</b><span> " + email + "</span></li>";
                mensaje += "<li><b>Clave:</b><span> " + password + "</span></li>";
                mensaje += "</ul>";

                mensaje += "<p style='font-size:10pt;'><u><b>Por el Sito Web VIABCP – Banca por Internet</b> (Solo si dispone de clave token)</u></p>";
                mensaje += "<ul>";
                mensaje += "<li><span style='font-size:10pt'>Ingrese a la página del BCP <a href='https://www.viabcp.com/wps/portal/' target='_blank'>viabcp.com.pe</a></span></li>";
                mensaje += "<li><span style='font-size:10pt'>Dar click en “Banca por Internet” e ingrese a sus cuentas.</span></li>";
                mensaje += "<li><span style='font-size:10pt'>Dar en click en Pagos y Transferencias y luego en “Pago de Servicios”</span></li>";
                mensaje += "<li><span style='font-size:10pt'>En directorio de pagos escriba en el recuadro blanco <strong>BSG INSTITUTE </strong>y da click en buscar</span></li>";
                mensaje += "<li><span style='font-size:10pt'>Aparecerá <strong>BSG Institute </strong>y luego da click en “" + datosAdicionales[1].ToString() + "”</span></li>";
                mensaje += "<li><span style='font-size:10pt'>Luego ingrese su código de alumno “" + datosAdicionales[0].ToString() + "”</span></li>";
                mensaje += "<li><span style='font-size:10pt'>Selecciona la cuota a pagar y da click en continuar</span></li>";
                mensaje += "<li><span style='font-size:10pt'>Confirme el abono mediante el reenvió del correo de pago recibido del BCP a su asesor(a) comercial.</span></li>";
                mensaje += "</ul>";

                mensaje += "<p style='font-size:10pt;'><u><b>En las oficinas de BSG Institute:</b></u></p>";
                mensaje += "<ul>";
                mensaje += "<li><span style='font-size:10pt'><strong>Lima:</strong> Av. Alfredo Benavides 768 - Oficina 401, Centro Empresarial Reducto – Miraflores</span></li>";
                mensaje += "<li><span style='font-size:10pt'><strong>Arequipa:</strong> Urb. León XIII Calle 2 N° 107. - Cayma</span></li>";
                mensaje += "</ul>";

                mensaje += "<p style='font-size:10pt;'><b>En los locales del BCP</b></p>"; ;
                mensaje += "<ul>";
                mensaje += "<li><span style='font-size:10pt'>Solicite en Ventanilla un&nbsp;<strong>CREDIPAGO</strong>o&nbsp;<strong>PAGO DE SERVICIO</strong>&nbsp;a la empresa&nbsp;<strong>BSG Institute</strong>&nbsp;</span></li>";
                mensaje += "<li><span style='font-size:10pt'>Indicar que la cuenta a abonar es&nbsp;“" + datosAdicionales[1].ToString() + "”</span></li>";
                mensaje += "<li><span style='font-size:10pt'>Indicará su Código de Alumno el cual es “" + datosAdicionales[0].ToString() + "”</span></li>";
                mensaje += "<li><span style='font-size:10pt'>Confirma el abono mediante el envío del voucher de pago a su asesor(a) comercial.</span></li>";
                mensaje += "</ul>";

                mensaje += "<p style='font-size:10pt;'><u><b>Por Agente BCP</b> (Solo para importes menores a S/. 500)</u></p>";
                mensaje += "<ul>";
                //mensaje += "<li><span style='font -size:10pt'>Solicite realizar un pago a la empresa <strong>BSG Institute</strong> con código <strong>00323&nbsp;</strong></span></li>";
                mensaje += "<li><span style='font -size:10pt'>Solicite realizar un pago a la empresa <strong>BSG Institute</strong> con código <strong>18185&nbsp;</strong></span></li>";
                mensaje += "<li><span style='font -size:10pt'>Indicar que la cuenta a abonar es&nbsp;“" + datosAdicionales[1].ToString() + "”</span></li>";
                mensaje += "<li><span style='font -size:10pt'>Indicará su Código de Alumno el cual es “" + datosAdicionales[0].ToString() + "”</span></li>";
                mensaje += "<li><span style='font -size:10pt'>Confirma el abono mediante el envío del voucher de pago a su asesor(a) comercial.</span></li>";
                mensaje += "</ul>";

            }
            else if (pais == "CO")
            {
                mensaje += "<p style='font-size:10pt;'><b><u>SOBRE EL PAGO:</u></b></p>";
                mensaje += "<p>Usted puede realizar el pago con su código de alumno mediante: </p>";
                mensaje += "<ul>";
                mensaje += "<li><span style='font -size:10pt'>Sitio Web de BSG Institute</span></li>";
                mensaje += "<li><span style='font -size:10pt'>Oficinas de Bancolombia (Ventanilla)</span></li>";
                mensaje += "<li><span style='font -size:10pt'>Por el Sitio Web de Bancolombia (Si tiene una cuenta en este banco)</span></li>";
                mensaje += "<li><span style='font -size:10pt'>Oficina BSG Institute</span></li>";
                mensaje += "</ul>";

                mensaje += "<p style='font-size:10pt;'><b><u>Por el Sitio Web de BSG Institute</u></b></p>";
                mensaje += "<br>";

                mensaje += "<p>Ingrese al siguiente enlace:</p>";
                mensaje += "<p><a href='" + url + "'>" + url + "</a></p><br>";
                mensaje += "<p style='font-size:10pt;'><b>Sus datos de acceso son</b></p>";
                mensaje += "<ul>";
                mensaje += "<li><b>Usuario:</b><span> " + email + "</span></li>";
                mensaje += "<li><b>Clave:</b><span> " + password + "</span></li>";
                mensaje += "</ul>";

                mensaje += "<p style='font-size:10pt;'><u><b>En las Oficinas de Bancolombia</b> (Ventanilla)</u></p>";
                mensaje += "<ul>";
                mensaje += "<li><span style='font-size:10pt'>Solicite en Ventanilla un deposito a la empresa con número de convenio 56470</span></li>";
                mensaje += "<li><span style='font-size:10pt'>Indicar el monto a depositar “" + montoCuota + "”</span></li>";
                mensaje += "<li><span style='font-size:10pt'>Indicar como referencia a consignar su código de matrícula “Número de referencia”</span></li>";
                mensaje += "<li><span style='font-size:10pt'>Confirmar el abono mediante el comprobante de consignación a su asesor(a) comercial.</span></li>";
                mensaje += "</ul>";

                mensaje += "<p style='font-size:10pt;'><u><b>Por el Sitio Web de Bancolombia</b> (Si tiene una cuenta en este banco)</u></p>";
                mensaje += "<ul>";
                mensaje += "<li><span style='font-size:10pt'>Registrar a BSG Institute como proveedor con los siguientes datos:</span></li>";
                mensaje += "<ul>";
                mensaje += "<li><span style='font-size:10pt'><b>Empresa:</b> BS GRUPO COLOMBIA SAS</span></li>";
                mensaje += "<li><span style='font-size:10pt'><b>NIT:</b> 900776296</span></li>";
                mensaje += "<li><span style='font-size:10pt'><b>Número de Cuenta:</b> 65231918412</span></li>";
                mensaje += "</ul>";
                mensaje += "<li><span style='font-size:10pt'>Realizar la transferencia del importe acordado “" + montoCuota + "”</span></li>";
                mensaje += "<li><span style='font-size:10pt'>Confirmar el abono reenviando el correo recibido por la plataforma de Bancolombia a su asesor(a) comercial.</span></li>";
                mensaje += "</ul>";

                mensaje += "<p style='font-size:10pt;'><u><b>En las oficinas de BSG Institute:</b></u></p>";
                mensaje += "<ul>";
                mensaje += "<li><span style='font-size:10pt'><b>Bogotá:</b> Av. Carrera 45 N° 108-27 Torre 1 Oficina 1008 – Bogotá (Centro Empresarial Paralelo)</span></li>";
                mensaje += "</ul>";
            }
            else
            {
                mensaje += "<p style='font-size:10pt;'><b><u>SOBRE EL PAGO:</u></b></p>";
                mensaje += "<p>Usted puede realizar el pago con su código de alumno mediante: </p>";
                mensaje += "<ul>";
                mensaje += "<li><span style='font -size:10pt'>Sitio Web de BSG Institute</span></li>";
                mensaje += "</ul>";

                mensaje += "<p style='font-size:10pt;'><b><u>Por el Sitio Web de BSG Institute</u></b></p>";
                mensaje += "<br>";

                mensaje += "<p>Ingrese al siguiente enlace:</p>";
                mensaje += "<p><a href='" + url + "'>" + url + "</a></p><br>";
                mensaje += "<p style='font-size:10pt;'><b>Sus datos de acceso son</b></p>";
                mensaje += "<ul>";
                mensaje += "<li><b>Usuario:</b><span> " + email + "</span></li>";
                mensaje += "<li><b>Clave:</b><span> " + password + "</span></li>";
                mensaje += "</ul>";
            }



            mensaje += "<p>Sin otro particular quedo a la espera de su confirmación.</p>";
            mensaje += "<br/>";
            mensaje += "<p>Saludos,</p>";

            return mensaje;
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 20/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Mensaje para enviar en Correo
        /// </summary>
        /// <param name="contacto">Datos del Contacto</param>
        /// <param name="listaCronogama">Cronograma de Pagos</param>
        /// <param name="codigoMatricula">Codigo de Matricula del Alumno</param>
        /// <param name="simboloMoneda">Simbolo de la moneda asociada</param>
        /// <param name="centroCosto">Datos del Centro de Costo</param>
        /// <returns> string </returns>
        public string ObtenerMensajeCorreoFinanzas(AlumnoDTO contacto, List<MontoPagoCronogramaDetalleDTO> listaCronogama, string codigoMatricula, string simboloMoneda, CentroCosto centroCosto)
        {
            string mensaje = string.Empty;

            if (simboloMoneda == "COL $")
            {
                mensaje += "<p style='font-size:10pt;'><b>Nro Convenio Bancolombia:</b> 56470<p>";
                mensaje += "<p style='font-size:10pt;'><b>Nro de Referencia:</b> " + codigoMatricula.Replace("A", "") + "<p>";
                mensaje += "<p style='font-size:10pt;'><b>Codigo de Alumno:</b> " + codigoMatricula + "</p>";
            }
            else
            {
                mensaje += "<p style='font-size:10pt;'><b>CODIGO DE ALUMNO:</b> " + codigoMatricula + "</p>";
            }

            mensaje += "<p><b>Datos del Alumno</b></p>";
            mensaje += "<ul>";
            mensaje += "<li><b>Nombres:</b> " + contacto.Nombre1 + " " + contacto.Nombre2 + "</li>";
            mensaje += "<li><b>Apellidos:</b> " + contacto.ApellidoPaterno + " " + contacto.ApellidoMaterno + "</li>";
            mensaje += "<li><b>Correo:</b> " + contacto.Email1 + "</li>";
            mensaje += "<li><b>Direccion:</b> " + contacto.Direccion + "</li>";
            mensaje += "<li><b>Documento:</b> " + contacto.Dni + "</li>";
            mensaje += "<li><b>Centro de Costo:</b> " + centroCosto.Nombre + "</li>";
            mensaje += "</ul>";

            mensaje += "<p style='font-size:10pt;'><b>CRONOGRAMA DE PAGOS:</b>" + "</p>";

            mensaje += "<table style='border: 1px solid #e6e6e6;border-collapse:collapse' border='' cellspacing='0' cellpadding='2'>";
            mensaje += "<tbody>";
            mensaje += "<tr>";
            mensaje += "<th style='border: 1px solid #e6e6e6' bgcolor='#FAFAFA'>Descripcion</th>";
            mensaje += "<th style='border: 1px solid #e6e6e6' bgcolor='#FAFAFA'>Fecha pago</th>";
            mensaje += "<th style='border: 1px solid #e6e6e6' bgcolor='#FAFAFA'>Monto cuota con descuento</th>";
            mensaje += "</tr>";

            foreach (var item in listaCronogama)
            {
                mensaje += "<tr>";
                mensaje += "<th style='border: 1px solid #e6e6e6; font-weight: 100;'>" + item.CuotaDescripcion.ToString() + "</th>";
                mensaje += "<th style='border: 1px solid #e6e6e6; font-weight: 100;'>" + item.FechaPago.ToString("dd/MM/yyyy") + "</th>";
                mensaje += "<th style='border: 1px solid #e6e6e6; font-weight: 100;' align='right'>" + simboloMoneda + " " + item.MontoCuotaDescuento.ToString("0.00") + "</th>";
                mensaje += "</tr>";
            }
            mensaje += "</tbody>";
            mensaje += "</table>";

            mensaje += "<p>Sin otro particular quedo a la espera de su confirmación.</p>";
            mensaje += "<br/>";
            mensaje += "<p>Saludos,</p>";

            return mensaje;
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 20/08/2022
        /// Version: 1.0
        /// <summary>
        /// Envia correo sin Copia
        /// </summary>
        /// <param name="email">Correo Destinatario</param>
        /// <param name="displayname">DisplayName</param>
        /// <param name="subject">Asunto de Correo</param>
        /// <param name="mensaje">Mensaje de Correo</param>
        /// <param name="nombreDocumentos">Nombre de los Documentos</param>
        /// <param name="archivoBytes">Archivo en Bytes</param>
        /// <param name="correos">Correos con Copia</param>
        /// <returns> bool </returns>
        public bool EnvioEmailBlobAdjunto(string email, string displayname, string subject, string mensaje, string nombreDocumentos, byte[] archivoBytes, List<string> correos)
        {
            using (SmtpClient smtp = new SmtpClient())
            {
                try
                {
                    string correo = "matriculas@bsginstitute.com";
                    string clave = "kjhhxbosejaqszbi";
                    string alias = "matriculas@bsginstitute.com";
                    string host = "smtp.gmail.com";
                    int port = 587;

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
        /// Autor: Gilmer Quispe.
        /// Fecha: 14/10/2022
        /// Version: 1.0
        /// <summary>
        /// Valida si un correo es valido
        /// </summary>
        /// <param name="email">Direccion email</param>        
        /// <returns> Bool </returns>
        public bool EsCorreoValido(string email)
        {
            try
            {
                var mailAddress = new MailAddress(email);
                return mailAddress.Address == email;
            }
            catch
            {
                return false;
            }
        }
        public CorreoBodyDTO ObtenerInformacionEnvioMasivo(int idCorreo, int idAsesor, string? folder, string destinatario)
        {
            try
            {
                var servicioConfiguracionEnvioMailing = new ConfiguracionEnvioMailingService(_unitOfWork);
                var correoEnvioMandril = servicioConfiguracionEnvioMailing.ObtenerEnvioMasivo(idCorreo);

                CorreoDTO correo = new CorreoDTO();
                CorreoGmailService servicioCorreoGmail = new CorreoGmailService(_unitOfWork);

                var queryFiltroGmailCorreo = " AND Id = " + idCorreo;
                var correoEnvioGmail = servicioCorreoGmail.FiltroCorreosPorPersonaGmailCorreo(queryFiltroGmailCorreo).FirstOrDefault();

                if (correoEnvioMandril != null && correoEnvioMandril.Id > 0)
                {
                    if (correoEnvioGmail != null && correoEnvioGmail.Id > 0)
                    {
                        if (correoEnvioMandril.Destinatarios.Contains(destinatario) && correoEnvioMandril.IdPersonal == idAsesor)
                        {
                            correo = correoEnvioMandril;
                        }
                        else if (correoEnvioGmail.Destinatarios.Contains(destinatario) && correoEnvioGmail.IdPersonal == idAsesor)
                        {
                            correo = correoEnvioGmail;
                        }
                    }
                    else
                    {
                        correo = correoEnvioMandril;
                    }
                }
                else if (correoEnvioGmail != null)
                {
                    correo = correoEnvioGmail;
                }
                else
                {
                    throw new Exception("El mensaje no ha sido encontrado");
                }
                CorreoBodyDTO correoBodyDTO = new CorreoBodyDTO()
                {
                    EmailBody = correo.EmailBody
                };
                return (correoBodyDTO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public byte[] Descargar(int idCorreo, string nombreArchivo, int idAsesor, string folder)
        {
            try
            {
                var correoClienteCredencialDTO = _unitOfWork.GmailClienteRepository.ObtenerClienteCredencial(idAsesor);
                var imapServiceImpl = new TMK_ImapService();
                if (correoClienteCredencialDTO != null)
                {
                    byte[] fileEmail = imapServiceImpl.DownloadFileEmailInbox(idCorreo, correoClienteCredencialDTO.EmailAsesor, correoClienteCredencialDTO.PasswordCorreo, nombreArchivo, folder);
                    return fileEmail;
                }
                else
                {
                    throw new BadRequestException("#CS-D-001@El asesor no tiene credenciales");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
