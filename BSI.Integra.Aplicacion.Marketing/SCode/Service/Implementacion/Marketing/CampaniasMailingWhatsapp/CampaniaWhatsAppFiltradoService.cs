using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaGeneralDetalleSubAreaWhatsapp;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaMailingWhatsapp;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue;
using BSI.Integra.Aplicacion.Marketing.Service.Interface.Marketing.CampaniasMailingWhatsapp;
using BSI.Integra.Aplicacion.Servicios.Service.Implementacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue.SendingblueRespuestaGenericaDTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using Google.Api.Ads.AdWords.v201809;

namespace BSI.Integra.Aplicacion.Marketing.Service.Implementacion.Marketing.CampaniasMailingWhatsapp
{
    public class CampaniaWhatsAppFiltradoService : ICampaniaWhatsAppFiltradoService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly SendingblueRespuestaGenericaDTO.ErrorGenerico error;
        private RespuestaGenerica respuesta = new RespuestaGenerica();

        public CampaniaWhatsAppFiltradoService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            error = new SendingblueRespuestaGenericaDTO.ErrorGenerico();

        }
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza un apeticion para obtener los detalles de campnia general detalle por el id de campania general
        /// </summary>
        /// <param name="datosFiltro">Datos requeridos para la ejecucion del sp para el filtrado</param>
        /// <returns>Retorna respuesta generica que contien un valor boleano</returns>
        public async Task<RespuestaGenerica> FiltradoDeDatosParaWhatsapp(CampaniaMailingWhatsAppFiltradoDTO.CampaniaWhatsAppFiltrado datosFiltro)
        {
            try
            {
                List<string> correos = new List<string> { "emontesinos@bsginstitute.com" };
                var dat = await unitOfWork.campaniaWhatsappFiltradoRepository.FiltradoDeDatosParaWhatsapp(datosFiltro);
                Dictionary<string, bool> res = new Dictionary<string, bool>();
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
                    Detalle = new DetailError { Codigo = "SP-FM-Ex00001", Descripcion = "El error se produjo en la funcion: FiltradoDeDatosParaMailing, en CampaniaWhatsAppFiltradoService linea:42", Mensaje = "No se pudo generar el filtrado de datos" }

                };
                return respuesta;
            }
            catch (Exception e)
            {
                List<string> correos = new List<string> { "emontesinos@bsginstitute.com" };
                TMK_MailService Mailservice = new TMK_MailService();
                TMKMailDataDTO mailData = new TMKMailDataDTO
                {
                    Sender = "gmiranda@bsginstitute.com",
                    Recipient = string.Join(",", correos),
                    Subject = string.Concat("Procesar oportunidades - identificador de campania general. ", datosFiltro.IdcampaniaGeneral),
                    Message = string.Concat("Message: ocurrio un error en generar la campania de whatsApp Vinculada a el id campania: ", datosFiltro.IdcampaniaGeneral, " --  se adjuta respuesta: <br> ", JsonConvert.SerializeObject(respuesta)),
                    Cc = string.Empty,
                    Bcc = string.Empty,
                    AttachedFiles = null
                };
                Mailservice.SetData(mailData);
                Mailservice.SendMessageTask();
                throw e;
            }
        }
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza un apeticion para obtener los detalles de campania general detalle por el id de campania general
        /// </summary>
        /// <param name="IdcampaniaGeneral">Identificador unico de campania general</param>
        /// <param name="Prioridad">Prioridad</param>
        /// <returns>Retorna respuesta generica que contien una lista de datos filtrados para whatsapp</returns>
        public RespuestaGenerica FiltradoDeDatosParaWhatsappObtenerData(int IdcampaniaGeneral, int Prioridad)
        {
            respuesta.SendingblueRespuesta = JsonConvert.SerializeObject(unitOfWork.campaniaWhatsappFiltradoRepository.FiltradoDeDatosParaWhatsappObtenerData(IdcampaniaGeneral, Prioridad));
            return respuesta;
        }
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 12/21/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza un apeticion para obtener los detalles de campnia general detalle por el id de campania general
        /// </summary>
        /// <param name="IdcampaniaGeneralDetalle">Identificador unico de campania general detalle</param>
        /// <param name="prioridad">Prioridad</param>
        /// <returns>Retorna respuesta generica que contien una lista de datos filtrados para whatsapp</returns>
        public int ObtenerCantidadDeDatosPorPiroridad(int prioridad, int IdcampaniaGeneralDetalle)
        {
            try
            {
                var respuesta = unitOfWork.campaniaWhatsappFiltradoRepository.ObtenerCantidadDeDataPorPioridadYcampania(IdcampaniaGeneralDetalle, prioridad);
                return respuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
        public RespuestaGenerica EliminacionLogicaDeFiltroWhatsApp(int IdcampaniaGeneral, string usuario)
        {
            var dat = unitOfWork.campaniaWhatsappFiltradoRepository.EliminarFiltradoPasadoWhatsApp(IdcampaniaGeneral, usuario);
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
                Detalle = new DetailError { Codigo = "SP-FM-Ex00001", Descripcion = "El error se produjo en la funcion: EliminacionLogicaDeFiltroWhatsApp, en CampaniaMailingFiltradoService linea:65", Mensaje = "No se pudo eliminar el filtrado de datos" }

            };
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
        public bool SendMail(string usuario, int IdcampaniaGeneral)
        {
            try
            {
                var listaCampaniaGeneralDetalle = unitOfWork.CampaniaGeneralDetalleRepository.ObtenerCampaniaGeneralDetallePorIdDeCampaniaGenerla(IdcampaniaGeneral);
                var CampaniaGeneral = unitOfWork.campaniaGeneralRepositorio.Obtener(IdcampaniaGeneral);
                var usuarioResponsable = unitOfWork.IntegraAspNetUserRepository.ObtenerPorUsuario(usuario);
                // Enviar mensaje sistemas
                List<string> correos = new List<string>
                    {
                        "emontesinos@bsginstitute.com"
                    };
                //var datos = unitOfWork.campaniaMailingFiltradoRepositorioa.FiltradoDeDatosParaMailingObtenerData(IdcampaniaGeneral, campaniaGeneralDetalle.Prioridad);
                TMK_MailService Mailservice = new TMK_MailService();
                TMKMailDataDTO mailData = new TMKMailDataDTO
                {
                    Sender = "gmiranda@bsginstitute.com",
                    Recipient = string.Join(",", correos),
                    Subject = string.Concat("Procesar oportunidades - Correcto ", CampaniaGeneral.Nombre),
                    Message = string.Concat("Message: ", JsonConvert.SerializeObject(listaCampaniaGeneralDetalle)),
                    Cc = string.Empty,
                    Bcc = string.Empty,
                    AttachedFiles = null
                };
                Mailservice.SetData(mailData);
                Mailservice.SendMessageTask();

                List<string> datosAEnviar = new List<string>();

                // Mensaje a usuario final
                var mensajeFinal = new List<MensajeProcesarDTO>();

                List<string> copiaCorreos = new List<string>
                    {
                        "emontesinos@bsginstitute.com"
                    };
                TMK_MailService MailservicePersonalizado = new TMK_MailService();
                var datapruabmail = usuarioResponsable.FirstOrDefault().Email;
                int cantidadDeContactosEncontrados = 0;
                foreach (var e in listaCampaniaGeneralDetalle)
                {
                    var datos = unitOfWork.campaniaWhatsappFiltradoRepository.FiltradoDeDatosParaWhatsappObtenerData(IdcampaniaGeneral, e.Prioridad);
                    unitOfWork.CampaniaGeneralDetalleRepository.AgregarCantidad(e.Id, datos.Count);
                    datosAEnviar.Add("Prioridad "+e.Prioridad + " contactos: " + datos.Count());
                    cantidadDeContactosEncontrados += datos.Count();
                }
                TMKMailDataDTO mailDataPersonalizado = new TMKMailDataDTO
                {
                    Sender = "gmiranda@bsginstitute.com",
                    Recipient = string.Join(",", datapruabmail),
                    Subject = string.Concat("Procesar prioridades WhatsApp - Correcto ", CampaniaGeneral.Nombre),
                    Message = GenerarNuevaPlantillaNotificacionProcesamientoMailingGeneral(datosAEnviar, cantidadDeContactosEncontrados, CampaniaGeneral.Nombre),
                    Cc = string.Empty,
                    Bcc = string.Join(",", copiaCorreos),
                    AttachedFiles = null
                };
                MailservicePersonalizado.SetData(mailDataPersonalizado);
                MailservicePersonalizado.SendMessageTask();
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
        /// <param name="nombre">CampanaiGeneral nombre</param>
        /// <param name="cantidadWhatsApp">Cantidad de contactos WhatsApp calculados</param>
        /// <returns>Objeto de clase CampaniaGeneralDetalleProgramaPlantillaDTO</returns>
        public string GenerarNuevaPlantillaNotificacionProcesamientoMailingGeneral(List<string> lista,int cantidadWhatsApp,string nombre)
        {
            try
            {
                string texto = "";
                string listaeplicita = "";
                foreach(var i in lista)
                {
                    listaeplicita += i + "<br>";
                }

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
                       <td>Ha finalizado correctamente el procesamiento del filtro para WhatsApp:</td>
                </tr>
                <tr style='text-align: center;'>
                    <td>
                        <h3 style='color: #4A92DB;'>CAMPAÑA:</h3>
                        <h3>{nombre}</h3>
                    </td>
                </tr>
                <tr style='text-align: center;'>
                    <td>
                        <h3 style='color: #4A92DB;'>PRIORIDADES Y CANTIDAD DE CONTACTOS:</h3>
                        <h3>{listaeplicita}</h3>
                    </td>
                </tr>
                <tr style='text-align: center;'>
                    <td style='padding-bottom:25px;'>
                        <h3>Se ha calculado <span style='color: #4A92DB;'>{cantidadWhatsApp}</span> contactos para WhatsApp</h3>
                    </td>
                </tr>
            </table>";
             

                return texto;
            }
            catch (Exception e)
            {
                throw e;
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
    }

}
