using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaGeneralDetalleSubAreaWhatsapp;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaMailingWhatsapp;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue;
using BSI.Integra.Aplicacion.Marketing.Service.Interface.Marketing.CampaniasMailingWhatsapp;
using BSI.Integra.Aplicacion.Servicios.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaMailingWhatsapp.CampaniaMailingWhatsAppFiltradoDTO;
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue.SendingblueRespuestaGenericaDTO;

namespace BSI.Integra.Aplicacion.Marketing.Service.Implementacion.Marketing.CampaniasMailingWhatsapp
{
    public class CampaniaMailingFiltradoService : ICampaniaMailingFiltradoService
    {
        private readonly IUnitOfWork unitOfWork;
        private SendingblueRespuestaGenericaDTO.ErrorGenerico error;
        private RespuestaGenerica respuesta = new RespuestaGenerica();

        public CampaniaMailingFiltradoService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            error = new SendingblueRespuestaGenericaDTO.ErrorGenerico();

        }
        /// Autor: Rodrigo Montesinos
        /// Fecha: 05/12/2021
        /// Versión: 1.0
        /// <summary>
        /// Ejecuta el filtrado de datos para mailing
        /// </summary>
        /// <returns>Respusta generica</returns>
        public RespuestaGenerica FiltradoDeDatosParaMailing(CampaniaMailingFiltrado datosFiltro)
        {
            try
            {
                var dat = unitOfWork.campaniaMailingFiltradoRepositorioa.FiltradoDeDatosParaMailing(datosFiltro);
                Dictionary<string, bool> res = new Dictionary<string, bool>();
                List<string> correos = new List<string>{"emontesinos@bsginstitute.com"};
                if (dat)
                {
                    TMK_MailService Mailservice = new TMK_MailService();
                    TMKMailDataDTO mailData = new TMKMailDataDTO
                    {
                        Sender = "gmiranda@bsginstitute.com",
                        Recipient = string.Join(",", correos),
                        Subject = string.Concat("Procesar oportunidades - identificador de campania general. ", datosFiltro.IdcampaniaGeneral),
                        Message = string.Concat("Message: ", JsonConvert.SerializeObject(respuesta)),
                        Cc = string.Empty,
                        Bcc = string.Empty,
                        AttachedFiles = null
                    };
                    Mailservice.SetData(mailData);
                    Mailservice.SendMessageTask();
                    res.Add("Respuesta", true);
                    respuesta.SendingblueRespuesta = JsonConvert.SerializeObject(res);
                    respuesta.error = new SendingblueRespuestaGenericaDTO.ErrorGenerico()
                    {
                        Response = false
                    };
                    return respuesta;
                }
                res.Add("Respuesta", false);
                respuesta.SendingblueRespuesta = "";
                respuesta.error = new SendingblueRespuestaGenericaDTO.ErrorGenerico()
                {
                    Response = true,
                    Detalle = new DetailError { Codigo = "SP-FM-Ex00001", Descripcion = "El error se produjo en la funcion: FiltradoDeDatosParaMailing, en CampaniaMailingFiltradoService linea:32", Mensaje = "No se pudo generar el filtrado de datos" }

                };
                return respuesta;
            }
            catch (Exception ex)
            {
                List<string> correos = new List<string>
                    {
                        "emontesinos@bsginstitute.com"
                    };
                TMK_MailService Mailservice = new TMK_MailService();
                TMKMailDataDTO mailData = new TMKMailDataDTO
                {
                    Sender = "gmiranda@bsginstitute.com",
                    Recipient = string.Join(",", correos),
                    Subject = string.Concat("Procesar oportunidades - identificador de campania general. ", datosFiltro.IdcampaniaGeneral),
                    Message = string.Concat("Message: ocurrio un error en generar la campania de mailing --  se adjuta respuesta: <br> ", JsonConvert.SerializeObject(respuesta)),
                    Cc = string.Empty,
                    Bcc = string.Empty,
                    AttachedFiles = null
                };
                Mailservice.SetData(mailData);
                Mailservice.SendMessageTask();

                throw ex;
            }
        }
        /// Autor: Rodrigo Montesinos
        /// Fecha: 05/12/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los datos filtrados para mailing por prioridad y id de campania
        /// </summary>
        /// <param name="IdcampaniaGeneral">Identificador unico de la campania general</param>
        /// <param name="Prioridad">Prioridad del filtro</param>
        /// <returns>Una respuesta generica que contiene una lista de los datos filtrados</returns>
        public RespuestaGenerica FiltradoDeDatosParaMailingObtenerData(int IdcampaniaGeneral, int Prioridad)
        {
           respuesta.SendingblueRespuesta=JsonConvert.SerializeObject(unitOfWork.campaniaMailingFiltradoRepositorioa.FiltradoDeDatosParaMailingObtenerData(IdcampaniaGeneral,Prioridad));
            return respuesta;
        }
        /// Autor: Rodrigo Montesinos
        /// Fecha: 05/12/2021
        /// Versión: 1.0
        /// <summary>
        /// Realiza una eliminacion logica del filtrado de mailing 
        /// </summary>
        /// <param name="IdcampaniaGeneral">Identificador unico de la campania general</param>
        /// <param name="usuario">usuario que realizo la eliminacion</param>
        /// <returns>una respuesta generica que tendra en respuesta un valor boleano</returns>
        public RespuestaGenerica EliminacionLogicaDeFiltroMialing(int IdcampaniaGeneral,string usuario)
        {
            try
            {
                var dat = unitOfWork.campaniaMailingFiltradoRepositorioa.EliminarFiltradoPasado(IdcampaniaGeneral, usuario);
                Dictionary<string, bool> res = new Dictionary<string, bool>();
                if (dat)
                {
                    res.Add("Respuesta", true);
                    respuesta.SendingblueRespuesta = JsonConvert.SerializeObject(res);
                    respuesta.error = new SendingblueRespuestaGenericaDTO.ErrorGenerico()
                    {
                        Response = false
                    };

                    return respuesta;
                }
                res.Add("Respuesta", false);
                respuesta.SendingblueRespuesta = "";
                respuesta.error = new SendingblueRespuestaGenericaDTO.ErrorGenerico()
                {
                    Response = true,
                    Detalle = new DetailError { Codigo = "SP-FM-Ex00001", Descripcion = "El error se produjo en la funcion: EliminacionLogicaDeFiltroMialing, en CampaniaMailingFiltradoService linea:65", Mensaje = "No se pudo eliminar el filtrado de datos" }

                };
                return respuesta;
            }catch(Exception ex)
            {
                throw ex; 
            }
        }
        /// Autor: Rodrigo Montesinos
        /// Fecha: 05/12/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los detalle de una campaña detalle responsable
        /// </summary>
        /// <param name="IdcampaniaGeneral">identificaor unico de campania general</param>
        /// <param name="Prioridad">prioridad</param>
        /// <returns>respuesta generica que contiene una lista del filtrado de mailing</returns>
        public RespuestaGenerica FiltradoDeDatosParaMailingObtenerDataMailing(int IdcampaniaGeneral, int Prioridad)
        {
            respuesta.SendingblueRespuesta = JsonConvert.SerializeObject(unitOfWork.campaniaMailingFiltradoRepositorioa.FiltradoDeDatosParaMailingObtenerDataMailing(IdcampaniaGeneral, Prioridad));
            return respuesta;
        }
        /// Autor: Rodrigo Montesinos
        /// Fecha: 05/12/2021
        /// Versión: 1.0
        /// <summary>
        /// Realiza el envio de correos y actualizacion de datos en campania general detalle
        /// </summary>
        /// <param name="IdcampaniaGeneral">identificador unico de campania general</param>
        /// <param name="usuario">Usuarioq ue realizo el envio de correo</param>
        /// <returns>vacio</returns>
        public bool SendinMail(string usuario, int IdcampaniaGeneral)
        {
            try
            {
                var listaCampaniaGeneralDetalle = unitOfWork.CampaniaGeneralDetalleRepository.ObtenerCampaniaGeneralDetallePorIdDeCampaniaGenerla(IdcampaniaGeneral);
                foreach (var campaniaGeneralDetalle in listaCampaniaGeneralDetalle)
                {
                    var prioridadActualAEjecutar = new PrioridadMailingEjecucionDTO()
                    {
                        IdCampaniaGeneralDetalle = Convert.ToInt32(campaniaGeneralDetalle.Id),
                        Usuario = usuario
                    };
                    unitOfWork.CampaniaGeneralDetalleRepository.ActualizarEstadoEjecucionCampaniaGeneralDetalle(prioridadActualAEjecutar.IdCampaniaGeneralDetalle, true, prioridadActualAEjecutar.Usuario);
                    // Flag de ejecucion correcta
                    bool filtroEjecutadoCorrectamente = false;
                    var CampaniaGeneral = unitOfWork.campaniaGeneralRepositorio.Obtener(IdcampaniaGeneral);
                    // Creacion de Url Formulario
                    var resultadoCreacionFormulario = unitOfWork.campaniaGeneralRepositorio.CrearUrlFormularioPrioridad(campaniaGeneralDetalle.Id, usuario);
                    // Obtencion Url Formulario
                    var urlFormularioCreado = unitOfWork.campaniaGeneralRepositorio.ObtenerUrlFormularioPrioridad(campaniaGeneralDetalle.Id);

                    DateTime horaFin = DateTime.Now;

                    var usuarioResponsable = unitOfWork.IntegraAspNetUserRepository.ObtenerPorUsuario(usuario);

                    // Enviar mensaje sistemas
                    List<string> correos = new List<string>
                    {
                        "emontesinos@bsginstitute.com"
                    };
                    var datos = unitOfWork.campaniaMailingFiltradoRepositorioa.FiltradoDeDatosParaMailingObtenerData(IdcampaniaGeneral,campaniaGeneralDetalle.Prioridad);
                    TMK_MailService Mailservice = new TMK_MailService();
                    TMKMailDataDTO mailData = new TMKMailDataDTO
                    {
                        Sender = "gmiranda@bsginstitute.com",
                        Recipient = string.Join(",", correos),
                        Subject = string.Concat("Procesar oportunidades - Correcto ", CampaniaGeneral.Nombre),
                        Message = string.Concat("Message: ", JsonConvert.SerializeObject(campaniaGeneralDetalle)),
                        Cc = string.Empty,
                        Bcc = string.Empty,
                        AttachedFiles = null
                    };
                    Mailservice.SetData(mailData);
                    Mailservice.SendMessageTask();

                    // Mensaje a usuario final
                    var mensajeFinal = new List<MensajeProcesarDTO>();
                    mensajeFinal.Add(new MensajeProcesarDTO
                    {
                        Nombre = "CORRECTO",
                        ListaDetalle = datos.Select(X => new MensajeProcesarDetalleDTO
                        {
                            NombreCampania = CampaniaGeneral.Nombre,
                            NombreLista = campaniaGeneralDetalle.Nombre,
                            NroIntentos = datos.Count()
                        }).ToList()
                    });

                    List<string> copiaCorreos = new List<string>
                    {
                        "emontesinos@bsginstitute.com"
                    };
                    TMK_MailService MailservicePersonalizado = new TMK_MailService();
                    unitOfWork.CampaniaGeneralDetalleRepository.AgregarUrl(urlFormularioCreado, campaniaGeneralDetalle.Id, datos.Count());
                    var datapruabmail = usuarioResponsable.FirstOrDefault().Email;
                    TMKMailDataDTO mailDataPersonalizado = new TMKMailDataDTO
                    {
                        Sender = "gmiranda@bsginstitute.com",
                        Recipient = string.Join(",", datapruabmail),
                        Subject = string.Concat("Procesar prioridades Mailing - Correcto ", CampaniaGeneral.Nombre),
                        Message = GenerarNuevaPlantillaNotificacionProcesamientoMailingGeneral(mensajeFinal, datos.Count(), 0, urlFormularioCreado, campaniaGeneralDetalle.FechaCreacion, horaFin),
                        Cc = string.Empty,
                        Bcc = string.Join(",", copiaCorreos),
                        AttachedFiles = null
                    };
                    MailservicePersonalizado.SetData(mailDataPersonalizado);
                    MailservicePersonalizado.SendMessageTask();
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        /// Autor: Rodrigo Montesinos
        /// Fecha: 05/12/2021
        /// Versión: 1.0
        /// <summary>
        /// Genera nueva plantilla de notificacion de procesamiento de mailing general
        /// </summary>
        /// <param name="lista">Lista de objetos de clase MensajeProcesarDTO</param>
        /// <param name="cantidadMailing">Cantidad de contactos Mailing calculados</param>
        /// <param name="cantidadWhatsApp">Cantidad de contactos WhatsApp calculados</param>
        /// <param name="horaInicio">Hora inicio del procesamiento</param>
        /// <param name="horaFin">Hora fin del procesamiento</param>
        /// <returns>Objeto de clase CampaniaGeneralDetalleProgramaPlantillaDTO</returns>
        public string GenerarNuevaPlantillaNotificacionProcesamientoMailingGeneral(List<MensajeProcesarDTO> lista, int cantidadMailing, int cantidadWhatsApp, string urlFormulario, DateTime horaInicio, DateTime horaFin)
        {
            try
            {
                string texto = "";
                var valor = lista.First(x => x.Nombre == "CORRECTO").ListaDetalle.FirstOrDefault();
                if (valor != null) {
                    texto = $@"<table style='font-family:'Helvetica Neue',Helvetica,Arial,'Lucida Grande',sans-serif; border-collapse: collapse;'>
                <tr style='padding: 5px;'>
                    <td>
                        <div style = 'background-color: #224060; padding: 25px 100px;'>
                            <img src='https://integrav4.bsginstitute.com/Images/Sistema/bsginstitute.png' alt='' />
                        </div>
                    </td>
                </tr>
                <tr><td><br /></td></tr>
                <tr style='text-align: center;'>
                       <td>Ha finalizado correctamente el procesamiento de la prioridad:</td>
                </tr>
                <tr style='text-align: center;'>
                    <td>
                        <h3 style='color: #4A92DB;'>CAMPAÑA:</h3>
                        <h3>{lista.First(x => x.Nombre == "CORRECTO").ListaDetalle.FirstOrDefault().NombreCampania}</h3>
                    </td>
                </tr>
                <tr style='text-align: center;'>
                    <td>
                        <h3 style='color: #4A92DB;'>PRIORIDAD:</h3>
                        <h3>{lista.First(x => x.Nombre == "CORRECTO").ListaDetalle.FirstOrDefault().NombreLista}</h3>
                    </td>
                </tr>
                <tr style='text-align: center;'>
                    <td>
                        <h3 style='color: #CA341D;'>HORA DE INICIO:</h3>
                        <h3>{horaInicio}</h3>
                    </td>
                </tr>
                <tr style='text-align: center;'>
                    <td>
                        <h3 style='color: #CA341D;'>HORA DE FIN:</h3>
                        <h3>{horaFin}</h3>
                    </td>
                </tr>
                <tr style='text-align: center;'>
                    <td style='padding-bottom:25px;'>
                        <h3><span style='color: #4A92DB;'>INTENTOS:</span> {lista.FirstOrDefault().ListaDetalle.Count + lista.FirstOrDefault().ListaDetalle.Count}</h3>
                        <h3>Se ha calculado <span style='color: #4A92DB;'>{cantidadMailing}</span> contactos Mailing</h3>
                        <h3>Se ha calculado <span style='color: #4A92DB;'>{cantidadWhatsApp}</span> contactos WhatsApp</h3>
                    </td>
                </tr>
                <tr style='text-align: center;'>
                    <td style='padding-bottom:25px;'>
                        <h3><span style='color: #4A92DB;'>Direccion Formulario</span></h3>
                        <h3>{urlFormulario}</h3>
                    </td>
                </tr>
            </table>";
                }
                else
                {
                    texto = $@"<table style='font-family:'Helvetica Neue',Helvetica,Arial,'Lucida Grande',sans-serif; border-collapse: collapse;'>
                <tr style='padding: 5px;'>
                    <td>
                        <div style = 'background-color: #224060; padding: 25px 100px;'>
                            <img src='https://integrav4.bsginstitute.com/Images/Sistema/bsginstitute.png' alt='' />
                        </div>
                    </td>
                </tr>
                <tr><td><br /></td></tr>
                <tr style='text-align: center;'>
                       <td>Ha finalizado correctamente el procesamiento de la prioridad:</td>
                </tr>
                <tr style='text-align: center;'>
                    <td>
                        <h3 style='color: #4A92DB;'>CAMPAÑA:</h3>
                        <h3>no hay datos</h3>
                    </td>
                </tr>
                <tr style='text-align: center;'>
                    <td>
                        <h3 style='color: #4A92DB;'>PRIORIDAD:</h3>
                        <h3>no hay datos</h3>
                    </td>
                </tr>
                <tr style='text-align: center;'>
                    <td>
                        <h3 style='color: #CA341D;'>HORA DE INICIO:</h3>
                        <h3>{horaInicio}</h3>
                    </td>
                </tr>
                <tr style='text-align: center;'>
                    <td>
                        <h3 style='color: #CA341D;'>HORA DE FIN:</h3>
                        <h3>{horaFin}</h3>
                    </td>
                </tr>
                <tr style='text-align: center;'>
                    <td style='padding-bottom:25px;'>
                        <h3><span style='color: #4A92DB;'>INTENTOS:</span> 0</h3>
                        <h3>Se ha calculado <span style='color: #4A92DB;'>{cantidadMailing}</span> contactos Mailing</h3>
                        <h3>Se ha calculado <span style='color: #4A92DB;'>{cantidadWhatsApp}</span> contactos WhatsApp</h3>
                    </td>
                </tr>
                <tr style='text-align: center;'>
                    <td style='padding-bottom:25px;'>
                        <h3><span style='color: #4A92DB;'>Direccion Formulario</span></h3>
                        <h3>{urlFormulario}</h3>
                    </td>
                </tr>
            </table>";
                }

                return texto;
            }catch(Exception e)
            {
                string texto = $@"<table style='font-family:'Helvetica Neue',Helvetica,Arial,'Lucida Grande',sans-serif; border-collapse: collapse;'>
                <tr style='padding: 5px;'>
                    <td>
                        <div style = 'background-color: #224060; padding: 25px 100px;'>
                            <img src='https://integrav4.bsginstitute.com/Images/Sistema/bsginstitute.png' alt='' />
                        </div>
                    </td>
                </tr>
                <tr><td><br /></td></tr>
                <tr style='text-align: center;'>
                       <td>Ha finalizado correctamente el procesamiento de la prioridad:</td>
                </tr>
                <tr style='text-align: center;'>
                    <td>
                        <h3 style='color: #4A92DB;'>CAMPAÑA:</h3>
                        <h3>no hay datos</h3>
                    </td>
                </tr>
                <tr style='text-align: center;'>
                    <td>
                        <h3 style='color: #4A92DB;'>PRIORIDAD:</h3>
                        <h3>no hay datos</h3>
                    </td>
                </tr>
                <tr style='text-align: center;'>
                    <td>
                        <h3 style='color: #CA341D;'>HORA DE INICIO:</h3>
                        <h3>{horaInicio}</h3>
                    </td>
                </tr>
                <tr style='text-align: center;'>
                    <td>
                        <h3 style='color: #CA341D;'>HORA DE FIN:</h3>
                        <h3>{horaFin}</h3>
                    </td>
                </tr>
                <tr style='text-align: center;'>
                    <td style='padding-bottom:25px;'>
                        <h3><span style='color: #4A92DB;'>INTENTOS:</span> 0</h3>
                        <h3>Se ha calculado <span style='color: #4A92DB;'>{cantidadMailing}</span> contactos Mailing</h3>
                        <h3>Se ha calculado <span style='color: #4A92DB;'>{cantidadWhatsApp}</span> contactos WhatsApp</h3>
                    </td>
                </tr>
                <tr style='text-align: center;'>
                    <td style='padding-bottom:25px;'>
                        <h3><span style='color: #4A92DB;'>Direccion Formulario</span></h3>
                        <h3>{urlFormulario}</h3>
                    </td>
                </tr>
            </table>";

                return texto;
            }
        }
        /// Autor: Rodrigo Montesinos
        /// Fecha: 05/12/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los detalle de una campaña detalle responsable
        /// </summary>
        /// <param name="IdCampaniaGeneral"></param>
        /// <returns>Lista de campania general detalle</returns>
        public List<TCampaniaGeneralDetalle> ObtenerDetalleCampaniaGeneralPorIdDeCampaniaGeneral(int IdCampaniaGeneral)
        {
            var listaCampaniaGeneralDetalle = unitOfWork.CampaniaGeneralDetalleRepository.ObtenerCampaniaGeneralDetallePorIdDeCampaniaGenerla(IdCampaniaGeneral);
            return listaCampaniaGeneralDetalle;
        }
        /// Autor: Rodrigo Montesinos
        /// Fecha: 05/12/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los detalle de una campaña detalle responsable
        /// </summary>
        /// <param name="idCamapniaGeneral"></param>
        /// <param name="idCampaniaGeneralDetalle"></param>
        /// <returns>Una lista de Campania geneneral detale</returns>
        public List<CampaniaGeneralDetalleAreaSubAreaProgramaReturn> ObtenerCampaniaGeneralMasAreaSubAreaYprogramaById(int idCampaniaGeneralDetalle, int idCamapniaGeneral)
        {
            return unitOfWork.CampaniaGeneralDetalleRepository.ObtenerCampaniaGeneralMasAreaSubAreaYprogramaById(idCampaniaGeneralDetalle, idCamapniaGeneral);
        }
        /// Autor: Rodrigo Montesinos
        /// Fecha: 05/12/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los detalle de una campaña detalle responsable
        /// </summary>
        /// <param name="IdCampaniaGeneral"></param>
        /// <returns>Lista de campania general detalle</returns>
        public CampaniaGeneralDTO ObtenerCampaniaGeneralDetallePorIdDeCampaniaGenerlaCompleta(int IdCampaniaGeneral)
        {
            var listaCampaniaGeneralDetalle = unitOfWork.CampaniaGeneralDetalleRepository.ObtenerCampaniaGeneralDetallePorIdDeCampaniaGenerlaCompleta(IdCampaniaGeneral);
            return listaCampaniaGeneralDetalle;
        }
    }
}