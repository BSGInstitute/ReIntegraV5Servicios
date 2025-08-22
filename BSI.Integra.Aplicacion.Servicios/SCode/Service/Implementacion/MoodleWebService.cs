using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using System.Net;
using System.Xml;

namespace BSI.Integra.Aplicacion.Servicios.Service.Implementacion
{

    public class MoodleWebService
    {
        public string token { get; set; }
        public string url { get; set; }
        public string patron { get; set; }

        public MoodleWebService()
        {
            //this.url = "http://192.168.0.21/moodle-2/webservice/rest/server.php?";
            this.url = "https://virtual.bsginstitute.com/webservice/rest/server.php?";
            this.token = "756117b43335932a2d25204e2f95ff3a";
        }
        public MoodleWebServiceRespuestaDTO CrearUsuario(MoodleWebServiceCrearUsuarioDTO usuario)
        {
            MoodleWebServiceRespuestaDTO respuesta = new MoodleWebServiceRespuestaDTO();
            try
            {
                patron = Patron_CrearUsuario(usuario);
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                XmlDocument xml_respuesta_usuario = new XmlDocument();
                xml_respuesta_usuario.Load(this.patron);

                respuesta = VerificarRespuesta_CrearUsuario(xml_respuesta_usuario);
            }
            catch (Exception ex)
            {
                respuesta.Estado = false;
                respuesta.Mensaje = ex.Message;
                respuesta.Tipo = MoodleWebServiceTipoRespuestaDTO.Danger;
            }
            return respuesta;
        }
        public MoodleWebServiceRespuestaDTO ActualizarClaveMoodle(MoodleWebServiceActualizarClaveDTO accesos)
        {
            MoodleWebServiceRespuestaDTO respuesta = new MoodleWebServiceRespuestaDTO();
            try
            {
                patron = Patron_ActualizarClave(accesos);
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                XmlDocument xml_respuesta_matricula = new XmlDocument();
                xml_respuesta_matricula.Load(this.patron);

                respuesta = VerificarRespuesta_ActualizarClaveMoodle(xml_respuesta_matricula);
            }
            catch (Exception e)
            {
                respuesta.Estado = false;
                respuesta.Mensaje = e.Message;
                respuesta.Tipo = MoodleWebServiceTipoRespuestaDTO.Danger;
            }
            return respuesta;
        }
        public MoodleWebServiceRespuestaDTO RegistrarMatricula(MoodleWebServiceRegistrarMatriculaDTO matricula)
        {
            MoodleWebServiceRespuestaDTO respuesta = new MoodleWebServiceRespuestaDTO();

            try
            {
                patron = Patron_RegistrarMatricula(matricula);
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                XmlDocument xml_respuesta_matricula = new XmlDocument();
                xml_respuesta_matricula.Load(this.patron);

                respuesta = VerificarRespuesta_RegistrarMatricula(xml_respuesta_matricula);
            }
            catch (Exception ex)
            {
                respuesta.Estado = false;
                respuesta.Mensaje = ex.Message;
                respuesta.Tipo = MoodleWebServiceTipoRespuestaDTO.Danger;
            }
            return respuesta;
        }
        public string Patron_CrearUsuario(MoodleWebServiceCrearUsuarioDTO usuario)
        {
            string url_temporal = "";
            string token = "wstoken=" + this.token;
            string wsfunction = "&wsfunction=core_user_create_users";
            string username = "&users[0][username]=" + usuario.username;
            string password = "&users[0][password]=" + usuario.password;
            string firstname = "&users[0][firstname]=" + usuario.firstname;
            string lastname = "&users[0][lastname]=" + usuario.lastname;
            string email = "&users[0][email]=" + usuario.email;
            string auth = "&users[0][auth]=" + usuario.auth;
            string country = "&users[0][country]=" + usuario.country;
            string city = "&users[0][city]=" + usuario.city;

            url_temporal = this.url + token + wsfunction + username + password + firstname + lastname + email + auth + country + city;

            return url_temporal;
        }
        private string Patron_RegistrarMatricula(MoodleWebServiceRegistrarMatriculaDTO matricula)
        {
            string url_temporal = "";
            string token = "wstoken=" + this.token;
            string wsfunction = "&wsfunction=enrol_manual_enrol_users";
            string userid = "&enrolments[0][userid]=" + matricula.userid;
            string courseid = "&enrolments[0][courseid]=" + matricula.courseid;
            string roleid = "&enrolments[0][roleid]=" + matricula.roleid;
            string timestart = "&enrolments[0][timestart]=" + matricula.timestart;
            string timeend = "&enrolments[0][timeend]=" + matricula.timeend;

            url_temporal = this.url + token + wsfunction + userid + courseid + roleid + timestart + timeend;

            return url_temporal;
        }
        private string Patron_ActualizarClave(MoodleWebServiceActualizarClaveDTO accesos)
        {
            string url_temporal = "";
            string token = "wstoken=" + this.token;
            string wsfunction = "&wsfunction=core_user_update_users";
            string userid = "&users[0][id]=" + accesos.IdMoodle;
            string userpassword = "&users[0][password]=" + accesos.Clave;

            url_temporal = this.url + token + wsfunction + userid + userpassword;

            return url_temporal;
        }
        private MoodleWebServiceRespuestaDTO VerificarRespuesta_CrearUsuario(XmlDocument xml_respuesta)
        {
            MoodleWebServiceRespuestaDTO respuesta = new MoodleWebServiceRespuestaDTO();

            if (xml_respuesta.DocumentElement.Name == "RESPONSE")
            {
                int id_usuario = 0;
                string nombre_usuario = "";
                foreach (XmlNode nodo in xml_respuesta.DocumentElement.FirstChild.FirstChild)
                {
                    foreach (XmlAttribute atributo in nodo.Attributes)
                    {
                        if (atributo.Value == "id")
                            id_usuario = Convert.ToInt32(nodo.FirstChild.InnerText);
                        if (atributo.Value == "username")
                            nombre_usuario = nodo.FirstChild.InnerText;
                    }
                }

                //respuesta.Tipo = xml_respuesta.DocumentElement.Name;
                //respuesta.CodigoError = null;
                //respuesta.Mensaje = null;
                //respuesta.IdUsuarioMoodle = id_usuario;
                //respuesta.NombreUsuarioMoodle = nombre_usuario;

                respuesta.Estado = true;
                respuesta.Mensaje = "Usuario Creado en moodle Correctamente - IdUduario: " + id_usuario +
                                    " - Nombre Usuario: " + nombre_usuario;
                respuesta.Tipo = MoodleWebServiceTipoRespuestaDTO.Danger;
            }
            else
            {
                string codigo_error = "";
                string mensaje = "";
                foreach (XmlNode nodo in xml_respuesta.DocumentElement.ChildNodes)
                {
                    if (nodo.Name == "ERRORCODE")
                        codigo_error = nodo.InnerText;
                    if (nodo.Name == "MESSAGE")
                        mensaje = nodo.InnerText;
                }

                //respuesta.Tipo = xml_respuesta.DocumentElement.Name;
                //respuesta.CodigoError = codigo_error;
                //respuesta.Mensaje = mensaje;
                //respuesta.IdUsuarioMoodle = 0;
                //respuesta.NombreUsuarioMoodle = null;
                //respuesta.Estado = false;

                respuesta.Estado = false;
                respuesta.Mensaje = "Error al crear el usuario en - Codigo Error: " + codigo_error;
                respuesta.Tipo = MoodleWebServiceTipoRespuestaDTO.Danger;
            }

            return respuesta;
        }
        private MoodleWebServiceRespuestaDTO VerificarRespuesta_RegistrarMatricula(XmlDocument xml_respuesta)
        {
            MoodleWebServiceRespuestaDTO respuesta = new MoodleWebServiceRespuestaDTO();

            if (xml_respuesta.DocumentElement.Name == "RESPONSE")
            {
                //respuesta.Tipo = xml_respuesta.DocumentElement.Name;
                respuesta.Estado = true;
                respuesta.Mensaje = "Matrícula Registrada/Actualizada Correctamente.";
                respuesta.Tipo = MoodleWebServiceTipoRespuestaDTO.Success;
            }
            else
            {
                string codigo_error = "";
                string mensaje = "";
                foreach (XmlNode nodo in xml_respuesta.DocumentElement.ChildNodes)
                {
                    if (nodo.Name == "ERRORCODE")
                        codigo_error = nodo.InnerText;
                    if (nodo.Name == "MESSAGE")
                        mensaje = nodo.InnerText;
                }

                //respuesta.Tipo = xml_respuesta.DocumentElement.Name;
                respuesta.Estado = false;
                respuesta.Mensaje = "Error al registrar la matrícula en moodle - Code: " + codigo_error + " - Mensaje: " + mensaje;
                respuesta.Tipo = MoodleWebServiceTipoRespuestaDTO.Danger;
            }

            return respuesta;
        }
        private MoodleWebServiceRespuestaDTO VerificarRespuesta_ActualizarClaveMoodle(XmlDocument xml_respuesta)
        {
            MoodleWebServiceRespuestaDTO respuesta = new MoodleWebServiceRespuestaDTO();

            if (xml_respuesta.DocumentElement.Name == "RESPONSE")
            {
                //respuesta.Tipo = xml_respuesta.DocumentElement.Name;
                respuesta.Estado = true;
                respuesta.Mensaje = "Cambio de clave actualizada correctamente.";
                respuesta.Tipo = MoodleWebServiceTipoRespuestaDTO.Success;
            }
            else
            {
                string codigo_error = "";
                string mensaje = "";
                foreach (XmlNode nodo in xml_respuesta.DocumentElement.ChildNodes)
                {
                    if (nodo.Name == "ERRORCODE")
                        codigo_error = nodo.InnerText;
                    if (nodo.Name == "MESSAGE")
                        mensaje = nodo.InnerText;
                }

                //respuesta.Tipo = xml_respuesta.DocumentElement.Name;
                respuesta.Estado = false;
                respuesta.Mensaje = "Error al actualizar la clave en moodle - Code: " + codigo_error + " - Mensaje: " + mensaje;
                respuesta.Tipo = MoodleWebServiceTipoRespuestaDTO.Danger;
            }

            return respuesta;
        }

    }


}
