using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System.Net.Mail;
using Twilio.TwiML.Voice;

namespace BSI.Integra.Aplicacion.Comercial.Service.Implementacion
{
    /// Service: AsesorChatService
    /// Autor: Jonathan Caipo
    /// Fecha: 25/11/2022
    /// <summary>
    /// Gestión general de Informacion del Chat de Asesores
    /// </summary>
    public class AsesorChatService : IAsesorChatService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public AsesorChatService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TAsesorChat, AsesorChat>(MemberList.None).ReverseMap();
                    //cfg.CreateMap<ChatDetalleIntegraArchivoDTO, ComboDTO>(MemberList.None);
                }
            );
            _mapper = new Mapper(config);
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 25/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Informacion de usuario por numero de alumno
        /// </summary>
        /// <param name="idCentroCosto"></param>
        /// <param name="numero"></param>
        /// <returns> PersonalAlumnoDTO </returns>
        public PersonalAlumnoDTO ObtenerOportunidadPorNumero(int idCentroCosto, string numero)
        {
            try
            {
                return _unitOfWork.AsesorChatRepository.ObtenerOportunidadPorNumero(idCentroCosto, numero);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ChatAsignadoNoAsignadoDTO> ObtenerTodoChatAsignados()
        {
            try
            {
                return _unitOfWork.AsesorChatRepository.ObtenerTodoChatAsignados();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 25/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la configuración del personal por medio de numero, idCentroCosto, idPais
        /// </summary>
        /// <param name="numero"></param>
        /// <param name="idCentroCosto"></param>
        /// <param name="idPais"></param>
        /// <returns></returns>
        public PersonalAlumnoDTO ObtenerPersonalConfiguracion(string numero, int idCentroCosto, int idPais)
        {
            AsesorChatService servicioAsesorChat = new AsesorChatService(_unitOfWork);
            string celular = "";
            if (idPais == 51)
            {
                celular = numero.Substring(2, 9);
            }
            else if (idPais == 57)
            {
                celular = "00" + numero;
            }
            else if (idPais == 591)
            {
                celular = "00" + numero;
            }
            else
            {
                celular = "00" + numero;
            }
            var oportunidad = servicioAsesorChat.ObtenerOportunidadPorNumero(idCentroCosto, celular);
            return oportunidad;
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 25/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la configuración del personal por medio de numero, idCentroCosto, idPais
        /// </summary>
        /// <param name="numero"></param>
        /// <param name="idCentroCosto"></param>
        /// <param name="idPais"></param>
        /// <returns></returns>
        public bool NotificarError(string Objeto)
        {
            try
            {
                List<AddresseeDTO> correosPersonalizados = new List<AddresseeDTO>();
                correosPersonalizados = _unitOfWork.AsesorChatRepository.ListaPersonaNotificacion();

                EnvioCorreo("AlertaWebHookWhatsApp", "Objeto no encontrado", Objeto, correosPersonalizados);
            return true;
            }
            catch (Exception ex)
            {
            return true;
            }
          
        }

        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 27/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el alumno y asesor para buscarlo en la base de la tablla Alumno

        public PersonalAlumnoDTO BuscarAlumnoPorWebHook(string Celular)
        {
            try
            {
                PersonalAlumnoDTO PersonalAlumno = new PersonalAlumnoDTO();
                PersonalAlumno = _unitOfWork.AsesorChatRepository.BuscarAlumnoPorWebHook(Celular);

                return PersonalAlumno;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 19/09/2022
        /// Version: 1.0
        /// <summary>
        /// método que manda correos por sender
        /// </summary>
        /// <returns>bool</returns>
        public bool EnvioCorreo(string displayname, string subject, string mensaje, List<AddresseeDTO> listaReceptores)
        {
            using (SmtpClient smtp = new SmtpClient())
            {
                try
                {
                    SenderDTO Sender = new SenderDTO();
                    Sender = _unitOfWork.AsignacionRegularRepository.ObtenerSender();


                    //CONFIGURACION DEL MENSAJE
                    MailMessage mail = new MailMessage();
                    mail.To.Add("emayta@bsginstitute.com");

                    if (listaReceptores != null && listaReceptores.Count > 0)
                    {
                        foreach (var copia in listaReceptores)
                        {
                            mail.Bcc.Add(copia.Email);
                        }
                    }
                    mail.From = new MailAddress("emayta@bsginstitute.com", displayname, System.Text.Encoding.UTF8);
                    mail.Subject = subject;
                    mail.Body = mensaje;
                    mail.BodyEncoding = System.Text.Encoding.UTF8;
                    mail.IsBodyHtml = true;
                    //CONFIGURACIÓN DEL STMP

                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.UseDefaultCredentials = false;

                    smtp.Credentials = new System.Net.NetworkCredential(Sender.Email, Sender.Contrasenia);// Enter seders   User name and password
                    smtp.EnableSsl = true;
                    smtp.Send(mail)
;
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
