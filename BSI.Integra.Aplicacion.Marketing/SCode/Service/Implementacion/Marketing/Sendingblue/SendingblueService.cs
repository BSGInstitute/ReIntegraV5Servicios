using BSI.Integra.Aplicacion.Marketing.Service.Interface.Sendingblue;
using BSI.Integra.Repositorio.UnitOfWork;
using Nancy.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue.SendingBlueCampaniasDTO;
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue.SendingblueCampaniasEnvioApiDTO;
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue.SendingblueContactosDTO;
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue.SendingblueSendersDTO;
using sib_api_v3_sdk.Client;
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue.SendingblueRespuestaGenericaDTO;
using MailBee.BounceMail;

using System.Collections;
using static BSI.Integra.Persistencia.Entidades.IntegraDB.Sendingblue.IntegracionConIntegraDB.T_SendinblueAtributoDTO;
using static BSI.Integra.Persistencia.Entidades.IntegraDB.Sendingblue.IntegracionConIntegraDB.T_SendinblueCarpetaDTO;
using static BBSI.Integra.Persistencia.Entidades.IntegraDB.Sendingblue.TSendingblueContactosDTO;
using static BSI.Integra.Persistencia.Entidades.IntegraDB.Sendingblue.IntegracionConIntegraDB.T_SendinblueRelacionAlmunoSBDTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue;
using static BSI.Integra.Persistencia.Entidades.IntegraDB.Sendingblue.IntegracionConIntegraDB.T_SendinblueCamapaniaDTO;
using static BSI.Integra.Persistencia.Entidades.IntegraDB.Sendingblue.IntegracionConIntegraDB.T_SendingblueListaDTO;
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue.IntegracionConIntegraDB.UpdateCampania;
using RestSharp.Portable;
using RestSharp.Portable.HttpClient;
using System.Text.Encodings.Web;
using System.Text.Json.Nodes;
using Nancy;
using iTextSharp.text;
using HttpStatusCode = System.Net.HttpStatusCode;
using Microsoft.WindowsAzure.Storage;
using MySqlX.XDevAPI.Common;

namespace BSI.Integra.Aplicacion.Marketing.Service.Implementacion.Sendingblue
{
    /// Service: SendingblueService
    /// Autor: Enamnuel Rodrigo Montesinos.
    /// Fecha: 11/22/2022
    /// <summary>
    /// Gestión general de SendinBlue
    /// </summary>
    public class SendingblueService : ISendingblueService
    {
        string apikey = "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";
        string urlSendinBlue = "https://api.brevo.com/v3/";
        private ContactsApi contactsApi = new ContactsApi();
        private EmailCampaignsApi apiInstance = new EmailCampaignsApi();
        private RespuestaGenerica respuesta = new RespuestaGenerica();
        private DetailError detalleSendinBlue = new DetailError() { Codigo="SBLC-Ex0000001x0SB",Descripcion= "Se perdio la coneccion con sendinblue intenalo nuevamente",Mensaje= "Se perdio la coneccion con sendinblue intenalo nuevamente" };

        public SendingblueService()
        {
            var dato = Configuration.Default.ApiKey.FirstOrDefault(x => x.Key == "api-key");
            if (dato.Value == null && Configuration.Default.ApiKey.Count<1)
            {
                Configuration.Default.ApiKey.Clear();
                Configuration.Default.ApiKey.Add("api-key", apikey);
            }
        }
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todas las campanias
        /// </summary>
        /// <param name="emailCampaigns">Obtiene los datos necesarios para la creacion de campanias email</param>
        /// <returns> RespuestaGenerica </returns>
        public RespuestaGenerica SendinblueCampania(GetEmailCampaignsDTO emailCampaigns)
        {
            
            try
            {
                RespuestaGenerica respuesta = null;
                if (emailCampaigns.offset == null)
                {
                    emailCampaigns.offset = 0;
                }
                if(emailCampaigns.limit == null)
                {
                    emailCampaigns.limit = 20;
                }
                string general = "limit=" + emailCampaigns.limit +"&offset=" + emailCampaigns.offset + "&sort=desc";
                string fragmento = "";
                if (emailCampaigns.type != null)
                {
                    fragmento = "type=" + emailCampaigns.type;
                }
                if(emailCampaigns.status != null)
                {
                    if (fragmento != string.Empty)
                    {
                        fragmento += "&" + "status=" + emailCampaigns.status;
                    }
                    else
                    {
                        fragmento = "status=" + emailCampaigns.status;
                    }
                }
                if(emailCampaigns.startDate!= null)
                {
                    if (fragmento != string.Empty)
                    {
                        fragmento += "&" + "startDate=" + emailCampaigns.startDate;
                    }
                    else
                    {
                        fragmento = "startDate=" + emailCampaigns.startDate;
                    }
                }
                if (emailCampaigns.endDate != null)
                {
                    if (fragmento != string.Empty)
                    {
                        fragmento += "&" + "startDate=" + emailCampaigns.endDate;
                    }
                    else
                    {
                        fragmento = "startDate=" + emailCampaigns.endDate;
                    }
                }
                if (fragmento != string.Empty)
                {
                    respuesta = ServicioCompletoDeConsultaGetURL("emailCampaigns?" + fragmento + "&" + general);
                }
                else
                {
                    respuesta = ServicioCompletoDeConsultaGetURL("emailCampaigns?" + general);
                }
                return respuesta;
            }
            catch (Exception e)
            {
                respuesta.error.Response = true;
                respuesta.error.Detalle = new DetailError { Codigo = "SB-C-Ex00001-42", Descripcion = "Este error fue generado en la funcion SendinblueCampania, linea: " + e.StackTrace.Split("line")[1] + " \n el mensaje enviado es: " + e.Message,Mensaje="No se pudo obtener las campañas de email" };
                return respuesta;
            }
        }
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Version: 1.0
        /// <summary>
        /// Envia la cmapania por un id
        /// </summary>
        /// <param name="campaignId">identificador de campania por id</param>
        /// <returns> RespuestaGenerica </returns>
        public RespuestaGenerica MandarCampaniaPorId(long campaignId)
        {
            try
            {
                RespuestaGenerica respuesta = ServicioCompletoDeConsultaGetURL("emailCampaigns/"+campaignId+"/sendNow");
                return respuesta;
            }
            catch (Exception e)
            {
                Dictionary<string, bool> res = new Dictionary<string, bool>();
                res.Add("Respuesta", false);
                respuesta.SendingblueRespuesta = JsonConvert.SerializeObject(res);
                respuesta.error.Response = true;
                respuesta.error.Detalle = new DetailError { Codigo = "SB-C-Ex00002-59", Descripcion = "Este error fue generado en la funcion MandarCampaniaPorId, linea: " + e.StackTrace.Split("line")[1] + " \n el mensaje enviado es: " + e.Message, Mensaje = "No se pudo enviar la campaña" };
                return respuesta;
            }
        }
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene contactos paginados
        /// </summary>
        /// <param name="limit">Cantidad de datos que se esperan seran retornados</param>
        /// <param name="offset">Indicador desde donde debe empezar el conteo de datos</param>
        /// <returns> RespuestaGenerica </returns>
        public RespuestaGenerica ConseguirContactos(long limit, long offset)
        {
            try
            {
                RespuestaGenerica respuesta = ServicioCompletoDeConsultaGetURL("contacts?limit=" + limit + "&offset=" + offset + "&sort=desc");
                return respuesta;
            }
            catch (Exception e)
            {
                respuesta.error.Response = true;
                respuesta.error.Detalle = new DetailError { Codigo = "SB-C-Ex00003-80", Descripcion = "Este error fue generado en la funcion ConseguirContactos, linea: " + e.StackTrace.Split("line")[1] + " \n el mensaje enviado es: " + e.Message, Mensaje = "No se pudo obtener los contactos, recuerde que solo se puede obtener de un maximo de 1000 en 1000" };
                return respuesta;
            }
        }
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Version: 1.0
        /// <summary>
        /// elimina contacto
        /// </summary>
        /// <param name="email">Correo que se usara para la eliminacion</param>
        /// <returns> RespuestaGenerica </returns>
        public async Task<RespuestaGenerica> EliminarContactos(string email)
        {
            try
            {
                RespuestaGenerica respuesta = await ServicioCompletoDeConsultaDeleteURL("contacts/" + email,null,null);
                return respuesta;
            }
            catch (Exception e)
            {
                Dictionary<string, bool> res = new Dictionary<string, bool>();
                res.Add("Respuesta", false);
                respuesta.SendingblueRespuesta = JsonConvert.SerializeObject(res);
                respuesta.error.Response = true;
                respuesta.error.Detalle = new DetailError { Codigo = "SB-C-Ex00004-96", Descripcion = "Este error fue generado en la funcion EliminarContactos, linea: " + e.StackTrace.Split("line")[1] + " \n el mensaje enviado es: " + e.Message, Mensaje = "No se pudo eliminar el contacto" };
                return respuesta;
            }
        }
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza contactos 
        /// </summary>
        /// <param name="contacto">contacto a actualziar</param>
        /// <returns> RespuestaGenerica </returns>
        public async Task<RespuestaGenerica> ActualizarContactos(SendingContactosDTO contacto)
        {
            try
            {
                SendingContactosRESTDTO contactosRESTDTO = new SendingContactosRESTDTO {
                    attributes = contacto.atributos,
                    emailBlacklisted =contacto.emailBlacklisted,
                    listIds =contacto.listIds,
                    smsBlacklisted = contacto.smsBlacklisted,
                    //unlinkListIds =contacto.unlinkListIds
                };
                RespuestaGenerica respuesta = await ServicioCompletoDeConsultaUpdateURL("contacts/" + UrlEncoder.Default.Encode(contacto.email), contactosRESTDTO, null);
                if (!respuesta.error.Response)
                {
                    JObject attributes = new JObject();
                    foreach (var dat in contacto.atributos)
                    {
                        attributes.Add(dat.Key, dat.Value);
                    }
                    List<long?> listIds = new List<long?>();
                    listIds.AddRange(contacto.listIds);
                    CrearSendingblueContactos contactSendBd = new CrearSendingblueContactos()
                    {
                        Atributo = JsonConvert.SerializeObject(attributes),
                        Email = contacto.email,
                        EstadoGuardado = true,
                        FechaModificacionSendinblue = DateTime.Now.ToString("yyyy-M-ddTHH:mm:ss"),
                        IdLista = JsonConvert.SerializeObject(listIds),
                        ListaNegraCorreo = false,
                        ListaNegroMensaje = false,
                        Respuesta = JsonConvert.SerializeObject(respuesta.SendingblueRespuesta),
                    };
                    respuesta.SendingblueRespuesta = JsonConvert.SerializeObject(contactSendBd);
                    respuesta.error.Response = false;
                }
                return respuesta;
            }
            catch (Exception e)
            {
                Dictionary<string, bool> res = new Dictionary<string, bool>();
                res.Add("Respuesta", false);
                respuesta.SendingblueRespuesta = JsonConvert.SerializeObject(res);
                respuesta.error.Response = true;
                respuesta.error.Detalle = new DetailError { Codigo = "SB-C-Ex00006-117", Descripcion = "Este error fue generado en la funcion ActualizarContactos, linea: " + e.StackTrace.Split("line")[1] + " \n el mensaje enviado es: " + e.Message, Mensaje = "No se pudo actualizar el contacto" };
                return respuesta;
            }
        }
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Version: 1.0
        /// <summary>
        /// Realiza la creacion de un contacto
        /// </summary>
        /// <param name="contacto">Crea un contacto</param>
        /// <returns> RespuestaGenerica </returns>
        public RespuestaGenerica CrearContactos(SendingContactosDTO contacto)
        {
            respuesta.error = new ErrorGenerico();
            try
            {

                int idAluno = 0;
                JObject attributes = new JObject();
                foreach (var dat in contacto.atributos)
                {
                    attributes.Add(dat.Key, dat.Value);
                    if(dat.Key.ToUpper() == "IDALUMNO")
                    {
                        idAluno = Convert.ToInt32(dat.Value);
                    }
                }
                SendingContactosCrearRESTDTO resu = new SendingContactosCrearRESTDTO
                {
                    updateEnabled = contacto.updateEnabled,
                    unlinkListIds = contacto.unlinkListIds,
                    smtpBlacklistedSender = contacto.smtpBlacklistedSender,
                    smsBlacklisted = contacto.smsBlacklisted,
                    listIds = contacto.listIds,
                    emailBlacklisted = contacto.emailBlacklisted,
                    email = contacto.email,
                    attributes=JsonConvert.DeserializeObject<Dictionary<string,string>>(JsonConvert.SerializeObject(attributes)),
                };
                var resParcial = ServicioCompletoDeConsultaPostURL("contacts", resu, null);
                if (!resParcial.error.Response)
                {
                    CreateUpdateContactModel result = JsonConvert.DeserializeObject<CreateUpdateContactModel>(resParcial.SendingblueRespuesta);
                    respuesta.SendingblueRespuesta = JsonConvert.SerializeObject(result);
                    CrearSendingblueContactos contactSendBd = new CrearSendingblueContactos()
                    {
                        Atributo = JsonConvert.SerializeObject(attributes),
                        Email = contacto.email,
                        EstadoGuardado = true,
                        FechaCreacionSendinblue = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"),
                        FechaModificacionSendinblue = "",
                        IdLista = JsonConvert.SerializeObject(resu.listIds),
                        ListaNegraCorreo = false,
                        ListaNegroMensaje = false,
                        Respuesta = JsonConvert.SerializeObject(result),
                    };
                    var EnvioDeData = new CrearSendinblueRelacionAlmunoSB { IdAlumno = idAluno, IdSendinblue = Convert.ToInt32(result.Id) };
                    PuenteControllerHelperCrearContacto regresar = new PuenteControllerHelperCrearContacto { crearSendinblueRelacionAlmunoSB = EnvioDeData, crearSendingblueContactos = contactSendBd };
                    respuesta.SendingblueRespuesta = JsonConvert.SerializeObject(regresar);
                    respuesta.error.Response = false;
                }
                return respuesta;
            }
            catch (Exception e)
            {
                Dictionary<string, bool> res = new Dictionary<string, bool>();
                res.Add("Respuesta", false);
                respuesta.SendingblueRespuesta = JsonConvert.SerializeObject(res);
                respuesta.error.Response = true;
                respuesta.error.Detalle = new DetailError { Codigo = "SB-C-Ex00007-152", Descripcion = "Este error fue generado en la funcion CrearContactos, linea: " + e.StackTrace.Split("line")[1] + " \n el mensaje enviado es: " + e.Message, Mensaje = "No se pudo crear el contacto" };
                return respuesta;
            }
        }
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene folders pro limite y offset
        /// </summary>
        /// <param name="limit">Cantidad de datos que se esperan seran retornados</param>
        /// <param name="offset">Indicador desde donde debe empezar el conteo de datos</param>
        /// <returns> RespuestaGenerica </returns>
        public RespuestaGenerica ListarFolders(long limit, long offset)
        {
            respuesta.error = new ErrorGenerico();
            try
            {
                RespuestaGenerica result = ServicioCompletoDeConsultaGetURL("contacts/folders?limit=" + limit + "&offset=" + offset + "&sort=desc");
                return result;
            }
            catch (Exception e)
            {
                respuesta.error.Response = true;
                respuesta.error.Detalle = new DetailError { Codigo = "SB-C-Ex00008-188", Descripcion = "Este error fue generado en la funcion ListarFolders, linea: " + e.StackTrace.Split("line")[1] + " \n el mensaje enviado es: " + e.Message, Mensaje = "No se pudo obtener las carpetas" };
                return respuesta;
            }
        }
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Version: 1.0
        /// <summary>
        /// Crea un nuevo folder
        /// </summary>
        /// <param name="myFolderName">Nombre del folder que sera creado</param>
        /// <returns> RespuestaGenerica </returns>
        public RespuestaGenerica CrearFolder(string myFolderName)
        {
            respuesta.error = new ErrorGenerico();
            try
            {
                CreacionmyFolderName data = new CreacionmyFolderName { name = myFolderName };
                RespuestaGenerica result = ServicioCompletoDeConsultaPostURL("contacts/folders", data, null);
                return result;
            }
            catch (Exception e)
            {
                respuesta.error.Response = true;
                respuesta.error.Detalle = new DetailError { Codigo = "SB-C-Ex00009-204", Descripcion = "Este error fue generado en la funcion CrearFolder, linea: " + e.StackTrace.Split("line")[1] + " \n el mensaje enviado es: " + e.Message, Mensaje = "No se pudo crear la carpetas" };
                return respuesta;
            }
        }
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Version: 1.0
        /// <summary>
        /// Actualzia los datos de un folder
        /// </summary>
        /// <param name="idFolder">Identificador unico del folder</param>
        /// <param name="name">Nombre para el folder</param>
        /// <returns> RespuestaGenerica </returns>
        public async Task<RespuestaGenerica> ActualizarFolder(long idFolder, string name)
        {
            respuesta.error = new ErrorGenerico();
            try
            {
                CreacionmyFolderName data = new CreacionmyFolderName { name = name };
                RespuestaGenerica result = await ServicioCompletoDeConsultaUpdateURL("contacts/folders/" + idFolder, data, null);
                return result;
            }
            catch (Exception e)
            {
                Dictionary<string, bool> res = new Dictionary<string, bool>();
                res.Add("Respuesta", false);
                respuesta.SendingblueRespuesta = JsonConvert.SerializeObject(res);
                respuesta.error.Response = true;
                respuesta.error.Detalle = new DetailError { Codigo = "SB-C-Ex00010-220", Descripcion = "Este error fue generado en la funcion ActualizarFolder, linea: " + e.StackTrace.Split("line")[1] + " \n el mensaje enviado es: " + e.Message, Mensaje = "No se pudo actualizar la carpeta" };
                return respuesta;
            }
        }
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Version: 1.0
        /// <summary>
        /// Elimina folder
        /// </summary>
        /// <param name="folderId">Identificador unico de folder</param>
        /// <returns> RespuestaGenerica </returns>
        public async Task<RespuestaGenerica> EliminarFolder(long folderId)
        {
            respuesta.error = new ErrorGenerico();
            try
            {
                RespuestaGenerica respuesta = await ServicioCompletoDeConsultaDeleteURL("contacts/folders/" + folderId, null, null);

                return respuesta;
            }
            catch (Exception e)
            {
                Dictionary<string, bool> res = new Dictionary<string, bool>();
                res.Add("Respuesta", false);
                respuesta.SendingblueRespuesta = JsonConvert.SerializeObject(res);
                respuesta.error.Response = true;
                respuesta.error.Detalle = new DetailError { Codigo = "SB-C-Ex00011-242", Descripcion = "Este error fue generado en la funcion EliminarFolder, linea: " + e.StackTrace.Split("line")[1] + " \n el mensaje enviado es: " + e.Message, Mensaje = "No se pudo eliminar la carpeta" };
                return respuesta;
            }
        }
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene las listas
        /// </summary>
        /// <param name="limit">Cantidad de datos que se esperan seran retornados</param>
        /// <param name="offset">Indicador desde donde debe empezar el conteo de datos</param>
        /// <returns> RespuestaGenerica </returns>
        public RespuestaGenerica Listas(long limit, long offset)
        {
            respuesta.error = new ErrorGenerico();
            try
            {
                if (limit > 50){limit = 50;}
                RespuestaGenerica respuesta = ServicioCompletoDeConsultaGetURL("contacts/lists?limit="+limit+"&offset="+offset+"&sort=desc");
                return respuesta;
            }
            catch (Exception e)
            {
                respuesta.error.Response = true;
                respuesta.error.Detalle = new DetailError { Codigo = "SB-C-Ex00012-263", Descripcion = "Este error fue generado en la funcion Listas, linea: " + e.StackTrace.Split("line")[1] + " \n el mensaje enviado es: " + e.Message, Mensaje = "No se pudo obtener las listas" };
                return respuesta;
            }
        }
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Version: 1.0
        /// <summary>
        /// Crear lista en sendinblue
        /// </summary>
        /// <param name="id">identificador de lista</param>
        /// <param name="myListName">Nombre de lista</param>
        /// <returns> RespuestaGenerica </returns>
        public RespuestaGenerica CrearLista(int id, string myListName)
        {
            respuesta.error = new ErrorGenerico();
            try
            {
                var data = new CreateList(myListName, id);
                RespuestaGenerica respuesta = ServicioCompletoDeConsultaPostURL("contacts/lists",data, null);
                if (!respuesta.error.Response)
                {
                   respuesta= UpdateCantidadDeContactosLista(JsonConvert.DeserializeObject<CrearSendingblueListaDTO>(respuesta.SendingblueRespuesta).Id);

                }
                return respuesta;
            }
            catch (Exception e)
            {
                respuesta.error.Response = true;
                respuesta.error.Detalle = new DetailError { Codigo = "SB-C-Ex00013-283", Descripcion = "Este error fue generado en la funcion CrearLista, linea: " + e.StackTrace.Split("line")[1] + " \n el mensaje enviado es: " + e.Message, Mensaje = "No se pudo crear la listas" };
                return respuesta;
            }
        }
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza la cantidad de contactos en lista
        /// </summary>
        /// <param name="idLista">identificador de lista</param>
        /// <returns> RespuestaGenerica </returns>
        public RespuestaGenerica UpdateCantidadDeContactosLista(int idLista)
        {
            try
            {
                var listasndb = DetalleDeLista(idLista);
                var listaDeserealizada = JsonConvert.DeserializeObject<GetExtendedList>(listasndb.SendingblueRespuesta);
                CrearSendingblueListaDTO lista = new CrearSendingblueListaDTO()
                {
                    Id = 0,
                    IdSendinblueLista = Convert.ToInt32(listaDeserealizada.Id),
                    Estado = true,
                    EstadoGuardado = true,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    IdSendinblueCarpeta = Convert.ToInt32(listaDeserealizada.FolderId),
                    Nombre = listaDeserealizada.Name,
                    Respuesta = listasndb.SendingblueRespuesta,
                    TotalExcluido = 0,
                    TotalSuscrito = Convert.ToInt32(listaDeserealizada.TotalSubscribers),
                    UnicoSuscrito = Convert.ToInt32(listaDeserealizada.UniqueSubscribers),
                };
                respuesta.SendingblueRespuesta = JsonConvert.SerializeObject(lista);
                return respuesta;
            }catch(Exception e)
            {
                throw e;
            }
        }

        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Version: 1.0
        /// <summary>
        /// Realzia la actualziacion de una lista
        /// </summary>
        /// <param name="list">Datos apra actualizar</param>
        /// <returns> RespuestaGenerica </returns>
        public async Task<RespuestaGenerica> UpdateLista(ListUpdate list)
        {
            respuesta.error = new ErrorGenerico();
            try
            {
                var updateList = new UpdateList(list.name, list.folderId);
                RespuestaGenerica respuesta = await ServicioCompletoDeConsultaUpdateURL("contacts/lists/" + list.listId, updateList, null);
                return respuesta;
            }
            catch (Exception e)
            {
                Dictionary<string, bool> res = new Dictionary<string, bool>();
                res.Add("Respuesta", false);
                respuesta.SendingblueRespuesta = JsonConvert.SerializeObject(res);
                respuesta.error.Response = true;
                respuesta.error.Detalle = new DetailError { Codigo = "SB-C-Ex00014-299", Descripcion = "Este error fue generado en la funcion UpdateLista, linea: " + e.StackTrace.Split("line")[1] + " \n el mensaje enviado es: " + e.Message, Mensaje = "No se pudo actualizar la lista" };
                return respuesta;
            }
        }
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene detalle de lista
        /// </summary>
        /// <param name="idList">Identificador de lista</param>
        /// <returns> RespuestaGenerica </returns>
        public async Task<RespuestaGenerica> DeleteLista(int idList)
        {
            respuesta.error = new ErrorGenerico();
            try
            {
                RespuestaGenerica respuesta = await ServicioCompletoDeConsultaDeleteURL("contacts/lists/" + idList,null,null);
                return respuesta;
            }
            catch (Exception e)
            {
                Dictionary<string, bool> res = new Dictionary<string, bool>();
                res.Add("Respuesta", false);
                respuesta.SendingblueRespuesta = JsonConvert.SerializeObject(res);
                respuesta.error.Response = true;
                respuesta.error.Detalle = new DetailError { Codigo = "SB-C-Ex00015-321", Descripcion = "Este error fue generado en la funcion DeleteLista, linea: " + e.StackTrace.Split("line")[1] + " \n el mensaje enviado es: " + e.Message, Mensaje = "No se pudo eliminar la lista" };
                return respuesta;
            }
        }
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Version: 1.0
        /// <summary>
        /// Lista contactos paginados
        /// </summary>
        /// <param name="limit">Cantidad de datos que se esperan seran retornados</param>
        /// <param name="offset">Indicador desde donde debe empezar el conteo de datos</param>
        /// <returns> RespuestaGenerica </returns>
        public RespuestaGenerica ListarContactos(long limit, long offset)
        {
            respuesta.error = new ErrorGenerico();
            try
            {
                RespuestaGenerica respuesta = ServicioCompletoDeConsultaGetURL("contacts?limit=" + limit + "&offset=" + offset + "&sort=desc");
                return respuesta;
            }
            catch (Exception e)
            {
                respuesta.error.Response = true;
                respuesta.error.Detalle = new DetailError { Codigo = "SB-C-Ex00016-342", Descripcion = "Este error fue generado en la funcion ListarContactos, linea: " + e.StackTrace.Split("line")[1] + " \n el mensaje enviado es: " + e.Message, Mensaje = "No se pudo obtener los contactos" };
                return respuesta;
            }
        }
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene detalle de lista
        /// </summary>
        /// <param name="idlist">identificador unico de lista</param>
        /// <returns> RespuestaGenerica </returns>
        public RespuestaGenerica DetalleDeLista(long idlist)
        {
            respuesta.error = new ErrorGenerico();
            try
            {
                RespuestaGenerica respuesta = ServicioCompletoDeConsultaGetURL("contacts/lists/"+idlist);
                return respuesta;
            }
            catch (Exception e)
            {
                respuesta.error.Response = true;
                respuesta.error.Detalle = new DetailError { Codigo = "SB-C-Ex00017-358", Descripcion = "Este error fue generado en la funcion ListarContactos, linea: " + e.StackTrace.Split("line")[1] + " \n el mensaje enviado es: " + e.Message, Mensaje = "No se pudo obtener el detalle de lista" };
                return respuesta;
            }
        }
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Version: 1.0
        /// <summary>
        /// Aniade un conatcto a lista
        /// </summary>
        /// <param name="nuevoCorreo">Datos para al adcin de un contacto a una lista</param>
        /// <returns> RespuestaGenerica </returns>
        public RespuestaGenerica AgregarContactosALista(CrearContactosListaDto nuevoCorreo)
        {
            respuesta.error = new ErrorGenerico();
            try
            {
                var contactEmails = new AddContactToList(nuevoCorreo.email);
                RespuestaGenerica respuesta = ServicioCompletoDeConsultaPostURL("contacts/lists/"+nuevoCorreo.idList+"/contacts/add",contactEmails,null);
                return respuesta;
            }
            catch (Exception e)
            {
                respuesta.error.Response = true;
                respuesta.error.Detalle = new DetailError { Codigo = "SB-C-Ex00018-374", Descripcion = "Este error fue generado en la funcion AgregarContactosALista, linea: " + e.StackTrace.Split("line")[1] + " \n el mensaje enviado es: " + e.Message, Mensaje = "No se pudo agregar los contactos a lista" };
                return respuesta;
            }
        }
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Version: 1.0
        /// <summary>
        /// retira un cotnacto de la lista
        /// </summary>
        /// <param name="nuevoCorreo">Datos para retirar un correo de lista</param>
        /// <returns> RespuestaGenerica </returns>
        public RespuestaGenerica EliminarContactosDeLista(CrearContactosListaDto nuevoCorreo)
        {
            respuesta.error = new ErrorGenerico();
            try
            {
                var contactEmails = new AddContactToList(nuevoCorreo.email);
                RespuestaGenerica respuesta = ServicioCompletoDeConsultaPostURL("contacts/lists/" + nuevoCorreo.idList + "/contacts/add", contactEmails, null);
                return respuesta;
            }
            catch (Exception e)
            {
                respuesta.error.Response = true;
                respuesta.error.Detalle = new DetailError { Codigo = "SB-C-Ex00019-391", Descripcion = "Este error fue generado en la funcion EliminarContactosDeLista, linea: " + e.StackTrace.Split("line")[1] + " \n el mensaje enviado es: " + e.Message, Mensaje = "No se pudo eliminar los contactos de la lista" };
                return respuesta;
            }
        }
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene contactos por lista
        /// </summary>
        /// <param name="idDelist">Identificador de lista</param>
        /// <param name="limit">Cantidad de datos que se esperan seran retornados</param>
        /// <param name="offset">Indicador desde donde debe empezar el conteo de datos</param>
        /// <returns> RespuestaGenerica </returns>
        public RespuestaGenerica ObtenerContactosPorLista(long idDelist, long limit, long offset)
        {
            respuesta.error = new ErrorGenerico();
            try
            {
                RespuestaGenerica respuesta = ServicioCompletoDeConsultaGetURL("contacts/lists/"+idDelist+"/contacts?limit="+limit+"&offset="+offset+"&sort=desc");
                return respuesta;
            }
            catch (Exception e)
            {
                respuesta.error.Response = true;
                respuesta.error.Detalle = new DetailError { Codigo = "SB-C-Ex00020-408", Descripcion = "Este error fue generado en la funcion ObtenerContactosPorLista, linea: " + e.StackTrace.Split("line")[1] + " \n el mensaje enviado es: " + e.Message, Mensaje = "No se pudo obtener los contactos de la lista" };
                return respuesta;
            }
        }
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los atributos
        /// </summary>
        /// <returns> RespuestaGenerica </returns>
        public RespuestaGenerica ObtenerTodosLosatributos()
        {
            respuesta.error = new ErrorGenerico();
            try
            {
                RespuestaGenerica respuesta = ServicioCompletoDeConsultaGetURL("contacts/attributes");
                return respuesta;
            }
            catch (Exception e)
            {
                respuesta.error.Response = true;
                respuesta.error.Detalle = new DetailError { Codigo = "SB-C-Ex00021-425", Descripcion = "Este error fue generado en la funcion ObtenerTodosLosatributos, linea: " + e.StackTrace.Split("line")[1] + " \n el mensaje enviado es: " + e.Message, Mensaje = "No se pudo obtener los atributos" };
                return respuesta;
            }
        }
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Version: 1.0
        /// <summary>
        /// Agrega atributos
        /// </summary>
        /// <param name="categoria">Cantidad de datos que se esperan seran retornados</param>
        /// <param name="nombre">Indicador desde donde debe empezar el conteo de datos</param>
        /// <param name="enumerations">Lsita de atributos enumerados</param>
        /// <param name="tipo">Tipo de atributo</param>
        /// <returns> RespuestaGenerica </returns>
        public RespuestaGenerica AgregarAtributos(string categoria,string nombre, List<CreateAttributeEnumeration> enumerations,string tipo)
        {
            respuesta.error = new ErrorGenerico();
            try
            {
                CreateAttribute.TypeEnum? type;
                string tipoCURL = "text";
                switch (tipo.ToLower())
                {
                    case "string":
                        type = CreateAttribute.TypeEnum.Text;
                        tipoCURL = "text";
                        break;
                    case "bool":
                        type = CreateAttribute.TypeEnum.Boolean;
                        tipoCURL = "boolean";
                        break;
                    case "date":
                        type = CreateAttribute.TypeEnum.Date;
                        tipoCURL = "date";
                        break;
                    case "float":
                        type = CreateAttribute.TypeEnum.Float;
                        tipoCURL = "float";
                        break;
                    case "category":
                        type = CreateAttribute.TypeEnum.Category;
                        tipoCURL = "category";
                        break;
                    case "id":
                        type = CreateAttribute.TypeEnum.Id;
                        tipoCURL = "id";
                        break;
                    default:
                        type = CreateAttribute.TypeEnum.Text;
                        tipoCURL = "text";
                        break;
                }
                var createAttribute = new CreateAttribute(null, enumerations, type);
                RespuestaGenerica respuesta = ServicioCompletoDeConsultaPostURL("contacts/attributes/"+categoria+"/"+nombre, createAttribute, null);
                if (!respuesta.error.Response)
                {
                    //contactsApi.CreateAttribute(categoria, nombre, createAttribute);
                    Dictionary<string, string> enviosb = new Dictionary<string, string>();
                    enviosb.Add("name", nombre);
                    enviosb.Add("category", categoria);
                    enviosb.Add("type", tipo);
                    CrearSendinblueAtributo attr = new CrearSendinblueAtributo() { Categoria = categoria, EstadoGuardado = true, Nombre = nombre, Tipo = tipo, Valor = null, Respuesta = JsonConvert.SerializeObject(enviosb) };
                    respuesta.SendingblueRespuesta = JsonConvert.SerializeObject(attr);
                }
                return respuesta;
            }
            catch(Exception e)
            {
                Dictionary<string, bool> res = new Dictionary<string, bool>();
                res.Add("Respuesta", false);
                respuesta.SendingblueRespuesta = JsonConvert.SerializeObject(res);
                respuesta.error.Response = true;
                respuesta.error.Detalle = new DetailError { Codigo = "SB-C-Ex00022-445", Descripcion = "Este error fue generado en la funcion AgregarAtributos, linea: " + e.StackTrace.Split("line")[1] + " \n el mensaje enviado es: " + e.Message, Mensaje = "No se pudo agregar el atributo" };
                return respuesta;
            }
        }
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Version: 1.0
        /// <summary>
        /// Crea campanias de email
        /// </summary>
        /// <param name="campania">Campania de sendinblue</param>
        /// <returns> RespuestaGenerica </returns>
        public RespuestaGenerica CrearCampaignEmail(CrearCampaniaSendinblue campania)
        {
            respuesta.error = new ErrorGenerico();
            try
            {
                var ListasIds = new RecipientsCambiosSendinBlue();
                ListasIds.lists = new List<int>();
                ListasIds.lists.AddRange(campania.recipients.listIds);
                RespuestaGenerica respuesta = ServicioCompletoDeConsultaPostURL("emailCampaigns", campania, null);
                if (!respuesta.error.Response) {
                    var data = JsonConvert.DeserializeObject<Dictionary<string, Int64>>(respuesta.SendingblueRespuesta);
                    CrearSendinblueCamapaniaDTO crearSendinblue = new CrearSendinblueCamapaniaDTO()
                    {
                        Asunto = campania.subject,
                        ContenidoHtml = "",
                        Estado = true,
                        IdPlantilla = campania.templateId,
                        Nombre = campania.name,
                        Receptor = JsonConvert.SerializeObject(ListasIds.lists),
                        Respuesta = respuesta.SendingblueRespuesta,
                        HoraProgramada=campania.scheduledAt,
                        IdSendinblueRemitente = Convert.ToInt32(data["id"]),
                        EstadoGuardado =true,
                        Campo=campania.toField,
                        PruebaAb=false,
                        AsuntoA="",
                        AsuntoB="",
                        GanadorCriterio="",
                        ReglaDivision=0,
                        GanadorTiempoAtraso=0,
                    };
                    respuesta.SendingblueRespuesta = JsonConvert.SerializeObject(crearSendinblue);
                    respuesta.error.Response = false;
                    return respuesta;
                }
                else {
                    respuesta.error.Response = true;
                    respuesta.error.Detalle = new DetailError { Codigo = "SB-C-Ex00023-495", Descripcion = "Este error fue generado en la funcion CrearCampaignEmail," , Mensaje = "No se pudo insertar la campania en sendingblue" };
                    return respuesta;
                }
            }
            catch (Exception e)
            {
                respuesta.error.Response = true;
                respuesta.error.Detalle = new DetailError { Codigo = "SB-C-Ex00023-495", Descripcion = "Este error fue generado en la funcion CrearCampaignEmail, linea: " + e.StackTrace.Split("line")[1] + " \n el mensaje enviado es: " + e.Message, Mensaje = "No se pudo crear las campañas" };
                return respuesta;
            }
        }
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene senders de sendinblue
        /// </summary>
        /// <returns> RespuestaGenerica </returns>
        public SendingblueObtenerSenders ObtenerSenders()
        {
            respuesta.error = new ErrorGenerico();
            try
            {
                RespuestaGenerica resultado = ServicioCompletoDeConsultaGetURL("senders");
                if (!respuesta.error.Response)
                {
                    SendingblueObtenerSenders obtenerSenders = new SendingblueObtenerSenders();
                    obtenerSenders = JsonConvert.DeserializeObject<SendingblueObtenerSenders>(respuesta.SendingblueRespuesta);
                    obtenerSenders.error = new ErrorGenerico();
                    obtenerSenders.error.Response = false;
                    return obtenerSenders;
                }
                else
                {
                    SendingblueObtenerSenders obtenerSenders = new SendingblueObtenerSenders();
                    obtenerSenders.error = new ErrorGenerico();
                    obtenerSenders.error.Response = true;
                    obtenerSenders.error.Detalle = new DetailError { Codigo = "SB-C-Ex00024-517", Descripcion = "Este error fue generado en la funcion ObtenerSenders", Mensaje = "No se pudo obtener los senders" };
                    return obtenerSenders;
                }
            }
            catch (Exception e)
            {
                SendingblueObtenerSenders obtenerSenders = new SendingblueObtenerSenders();
                obtenerSenders.error = new ErrorGenerico();
                obtenerSenders.error.Response = true;
                obtenerSenders.error.Detalle = new DetailError { Codigo = "SB-C-Ex00024-517", Descripcion = "Este error fue generado en la funcion ObtenerSenders, linea: " + e.StackTrace.Split("line")[1] + " \n el mensaje enviado es: " + e.Message, Mensaje = "No se pudo obtener los senders" };
                return obtenerSenders;
            }
        }
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Version: 1.0
        /// <summary>
        /// agrega senders
        /// </summary>
        /// <param name="sendersDTO">Objeto de sender</param>
        /// <returns> RespuestaGenerica </returns>
        public SendingblueSendersRespuesta AgregarSender(SengindblueSenders sendersDTO)
        {
            respuesta.error = new ErrorGenerico();
            try
            {
                SendingblueSendersRespuesta sendersRespuesta = new SendingblueSendersRespuesta();
                sendersRespuesta.error = new ErrorGenerico();
                var apiInstance = new SendersApi();
                List<CreateSenderIps> ips = new List<CreateSenderIps>();
                if (sendersDTO.ips!=null)
                {
                    foreach (var ipsfe in sendersDTO.ips)
                    {
                        var createSenderIps = new CreateSenderIps(ipsfe.ip, ipsfe.domain, ipsfe.weight);
                        ips.Add(createSenderIps);
                    }
                }
                var sender = new CreateSender(sendersDTO.name, sendersDTO.email, ips);
                RespuestaGenerica respuesta = ServicioCompletoDeConsultaPostURL("senders", sender, null);
                var result = JsonConvert.DeserializeObject<CreateSenderModel>(respuesta.SendingblueRespuesta);
                sendersRespuesta.spfError = result.SpfError;
                sendersRespuesta.id = result.Id;
                sendersRespuesta.dkimError = result.DkimError;
                sendersRespuesta.error.Response = false;
                return sendersRespuesta;
            }
            catch (Exception e)
            {
                SendingblueSendersRespuesta sendersRespuesta = new SendingblueSendersRespuesta();
                sendersRespuesta.error = new ErrorGenerico();
                sendersRespuesta.error.Response = true;
                sendersRespuesta.error.Detalle = new DetailError { Codigo = "SB-C-Ex00025-540", Descripcion = "Este error fue generado en la funcion AgregarSender, linea: " + e.StackTrace.Split("line")[1] + " \n el mensaje enviado es: " + e.Message, Mensaje = "No se pudo agregar el sender" };
                return sendersRespuesta;
            }
        }
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza datos de un sender
        /// </summary>
        /// <param name="idSender">identificador de sender</param>
        /// <param name="sendersDTO">Datos de semder para actualziar</param>
        /// <returns> RespuestaGenerica </returns>
        public async Task<RespuestaGenerica> ActualizarSender(SengindblueSenders sendersDTO, int idSender)
        {
            respuesta.error = new ErrorGenerico();
            try
            {
                var apiInstance = new SendersApi();
                List<CreateSenderIps> ips = new List<CreateSenderIps>();
                foreach (var ipsfe in sendersDTO.ips)
                {
                    var createSenderIps = new CreateSenderIps(ipsfe.ip, ipsfe.domain, ipsfe.weight);
                    ips.Add(createSenderIps);
                }
                var sender = new UpdateSender(sendersDTO.name, sendersDTO.email, ips);
                RespuestaGenerica respuesta = await ServicioCompletoDeConsultaUpdateURL("senders/" + idSender, sendersDTO,null);
                return respuesta;
            }
            catch (Exception e)
            {
                Dictionary<string, bool> res = new Dictionary<string, bool>();
                res.Add("Respuesta", false);
                respuesta.SendingblueRespuesta = JsonConvert.SerializeObject(res);
                respuesta.error.Response = true;
                respuesta.error.Detalle = new DetailError { Codigo = "SB-C-Ex00026-571", Descripcion = "Este error fue generado en la funcion ActualizarSender, linea: " + e.StackTrace.Split("line")[1] + " \n el mensaje enviado es: " + e.Message, Mensaje = "No se pudo actualizar el sender" };
                return respuesta;
            }
        }
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Version: 1.0
        /// <summary>
        /// Elimina un sender
        /// </summary>
        /// <param name="idSender">indentificador de sender</param>
        /// <returns> RespuestaGenerica </returns>
        public async Task<RespuestaGenerica> EliminarSender(int idSender)
        {
            respuesta.error = new ErrorGenerico();
            try
            {
                RespuestaGenerica respuesta = await ServicioCompletoDeConsultaDeleteURL("senders/" + idSender,null,null);
                return respuesta;
            }
            catch (Exception e)
            {
                Dictionary<string, bool> res = new Dictionary<string, bool>();
                res.Add("Respuesta", false);
                respuesta.SendingblueRespuesta = JsonConvert.SerializeObject(res);
                respuesta.error.Response = true;
                respuesta.error.Detalle = new DetailError { Codigo = "SB-C-Ex00027-601", Descripcion = "Este error fue generado en la funcion EliminarSender, linea: " + e.StackTrace.Split("line")[1] + " \n el mensaje enviado es: " + e.Message, Mensaje = "No se pudo eliminar el sender" };
                return respuesta;
            }
        }
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene tamplates de sendinblue
        /// </summary>
        /// <param name="limit">Cantidad de datos que se esperan seran retornados</param>
        /// <param name="offset">Indicador desde donde debe empezar el conteo de datos</param>
        /// <param name="estadoDeTemplate">Estado del template bsucado</param>
        /// <param name="sort">Tipo de ordenameinto</param>
        /// <returns> RespuestaGenerica </returns>
        public RespuestaGenerica ObtenerTemplate(int limit,int offset,string sort, bool estadoDeTemplate)
        {
            respuesta.error = new ErrorGenerico();
            try
            {
                RespuestaGenerica respuesta = ServicioCompletoDeConsultaGetURL("smtp/templates?templateStatus=true&limit=" + limit+"&offset="+ offset + "&sort=desc");
                return respuesta;
            }
            catch(Exception e)
            {
                respuesta.error.Response = true;
                respuesta.error.Detalle = new DetailError { Codigo = "SB-C-Ex00028-623", Descripcion = "Este error fue generado en la funcion ObtenerTemplate, linea: " + e.StackTrace.Split("line")[1] + " \n el mensaje enviado es: " + e.Message, Mensaje = "No se pudo obtener los templates" };
                return respuesta;
            }
        }
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza la campania
        /// </summary>
        /// <param name="idCampania">identificador de campania</param>
        /// <param name="tipo">estado de campania</param>
        /// <returns> RespuestaGenerica </returns>
        public async Task<RespuestaGenerica> ActualziarCampania(int idCampania,string tipo)
        {
            respuesta.error = new ErrorGenerico();
            try {
                String tipoSend = "Sent";
                UpdateCampaignStatus.StatusEnum type = UpdateCampaignStatus.StatusEnum.Sent;
                switch (tipo.ToLower())
                {
                    case "suspended":
                        type = UpdateCampaignStatus.StatusEnum.Suspended;
                        tipoSend = "suspended";
                        break;
                    case "archive":
                        type = UpdateCampaignStatus.StatusEnum.Archive;
                        tipoSend = "archive";
                        break;
                    case "darchive":
                        type = UpdateCampaignStatus.StatusEnum.Darchive;
                        tipoSend = "darchive";
                        break;
                    case "sent":
                        type = UpdateCampaignStatus.StatusEnum.Sent;
                        tipoSend = "sent";
                        break;
                    case "queued":
                        type = UpdateCampaignStatus.StatusEnum.Queued;
                        tipoSend = "queued";
                        break;
                    case "replicate":
                        type = UpdateCampaignStatus.StatusEnum.Replicate;
                        tipoSend = "replicate";
                        break;
                    case "replicateTemplate":
                        type = UpdateCampaignStatus.StatusEnum.ReplicateTemplate;
                        tipoSend = "replicateTemplate";
                        break;
                    case "draft":
                        type = UpdateCampaignStatus.StatusEnum.Draft;
                        tipoSend = "draft";
                        break;
                    default:
                        type = UpdateCampaignStatus.StatusEnum.Sent;
                        tipoSend = "Sent";
                        break;
                }
                var status = new UpdateCampaignStatus(type);
                RespuestaGenerica respuesta = await ServicioCompletoDeConsultaUpdateURL("emailCampaigns/" + idCampania + "/status", status,null);
                return respuesta;
            }
            catch(Exception e)
            {
                respuesta.error.Response = true;
                respuesta.error.Detalle = new DetailError { Codigo = "SB-C-Ex00029-683", Descripcion = "Este error fue generado en la funcion ActualziarCampania, linea: " + e.StackTrace.Split("line")[1] + " \n el mensaje enviado es: " + e.Message, Mensaje = "No se pudo actualizar la campania" };
                return respuesta;
            }
        }
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene campania por id 
        /// </summary>
        /// <param name="id">Identificador de campania</param>
        /// <returns> RespuestaGenerica </returns>
        public RespuestaGenerica ObtenerCampaniaPorId(int id)
        {
            respuesta.error = new ErrorGenerico();
            try
            {
                RespuestaGenerica respuestaGenerica = ServicioCompletoDeConsultaGetURL("emailCampaigns/" + id);
                return respuesta;
            }
            catch(Exception e)
            {
                respuesta.error.Response = true;
                respuesta.error.Detalle = new DetailError { Codigo = "SB-C-Ex00030-779", Descripcion = "Este error fue generado en la funcion ObtenerCampaniaPorId, linea: " + e.StackTrace.Split("line")[1] + " \n el mensaje enviado es: " + e.Message, Mensaje = "Obtener la campania por id" };
                return respuesta;
            }
        }
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza la campania
        /// </summary>
        /// <param name="aBTest">actualziacion de campanias</param>
        /// <param name="idcampania">Indicador unico campania</param>
        /// <returns> RespuestaGenerica </returns>
        public RespuestaGenerica ActualizarCampania(UpdateCampaniaDTO aBTest,int idcampania)
        {
            respuesta.error = new ErrorGenerico();
            try
            {
                var ListasIds = new RecipientsCambiosSendinBlue();
                ListasIds.lists = new List<int>();
                ListasIds.lists.AddRange(aBTest.recipients.listIds);
                var serializer = new JavaScriptSerializer();
                var p = new CrearCampaniaSendinblue();
                WebClient comunicacion = new WebClient();
                comunicacion.Headers[HttpRequestHeader.ContentType] = "application/json";
                comunicacion.Headers["api-key"] = apikey;
                var result = comunicacion.UploadString("https://api.sendinblue.com/v3/emailCampaigns/" + idcampania, WebRequestMethods.Http.Put, JsonConvert.SerializeObject(aBTest)); //post
                if (result.Contains("id"))
                {
                    Dictionary<string, object> datos = new Dictionary<string, object>();
                    var data = JsonConvert.DeserializeObject<Dictionary<string, Int64>>(result);
                    CrearSendinblueCamapaniaDTO crearSendinblue = new CrearSendinblueCamapaniaDTO()
                    {
                        ContenidoHtml = "",
                        Estado = true,
                        Nombre = aBTest.name,
                        Receptor = JsonConvert.SerializeObject(ListasIds.lists),
                        Respuesta = result,
                        AsuntoA = aBTest.subjectA,
                        AsuntoB = aBTest.subjectB,
                        EstadoGuardado = true,
                        PruebaAb = true,
                        IdSendinblueRemitente = Convert.ToInt32(data["id"])

                    };
                    respuesta.SendingblueRespuesta = JsonConvert.SerializeObject(crearSendinblue);
                    respuesta.error.Response = false;
                    return respuesta;
                }
                else
                {
                    respuesta.error.Response = true;
                    respuesta.error.Detalle = new DetailError { Codigo = "SB-C-Ex00031-797", Descripcion = "Este error fue generado en la funcion CrearCampaignEmail,", Mensaje = "No se pudo insertar la campania en sendingblue" };
                    return respuesta;
                }
            }
            catch (Exception e)
            {
                respuesta.error.Response = true;
                respuesta.error.Detalle = new DetailError { Codigo = "SB-C-Ex00031-797", Descripcion = "Este error fue generado en la funcion CrearCampaignEmail, linea: " + e.StackTrace.Split("line")[1] + " \n el mensaje enviado es: " + e.Message, Mensaje = "No se pudo crear las campañas" };
                return respuesta;
            }
        }
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Version: 1.0
        /// <summary>
        /// Agrega una campania abtest
        /// </summary>
        /// <param name="aBTest">datos necesarios para la creacion de una campania abtest</param>
        /// <returns> RespuestaGenerica </returns>
        public RespuestaGenerica AgregarCampaniaABTest(CrearCampaniaSendinblueABTest aBTest)
        {
            respuesta.error = new ErrorGenerico();
            try
            {
                var ListasIds = new RecipientsCambiosSendinBlue();
                ListasIds.lists = new List<int>();
                ListasIds.lists.AddRange(aBTest.recipients.listIds);
                var serializer = new JavaScriptSerializer();
                var p = new CrearCampaniaSendinblue();
                WebClient comunicacion = new WebClient();
                comunicacion.Headers[HttpRequestHeader.ContentType] = "application/json";
                comunicacion.Headers["api-key"] = apikey;
                var result = comunicacion.UploadString("https://api.sendinblue.com/v3/emailCampaigns", JsonConvert.SerializeObject(aBTest)); //post
                if (result.Contains("id"))
                {
                    Dictionary<string, object> datos = new Dictionary<string, object>();
                    var data = JsonConvert.DeserializeObject<Dictionary<string, Int64>>(result);
                    CrearSendinblueCamapaniaDTO crearSendinblue = new CrearSendinblueCamapaniaDTO()
                    {
                        Asunto = aBTest.subject,
                        ContenidoHtml = "",
                        Estado = true,
                        IdPlantilla = aBTest.templateId,
                        Nombre = aBTest.name,
                        Receptor = JsonConvert.SerializeObject(ListasIds.lists),
                        Respuesta = result,
                        HoraProgramada = aBTest.scheduledAt,
                        AsuntoA = aBTest.subjectA,
                        AsuntoB = aBTest.subjectB,
                        EstadoGuardado = true,
                        PruebaAb = true,
                        GanadorCriterio=aBTest.winnerCriteria,
                        GanadorTiempoAtraso=aBTest.winnerDelay,
                        ReglaDivision=aBTest.splitRule,
                        Campo=aBTest.toField,
                        IdSendinblueRemitente = Convert.ToInt32(data["id"])
                    };
                    respuesta.SendingblueRespuesta = JsonConvert.SerializeObject(crearSendinblue);
                    respuesta.error.Response = false;
                    return respuesta;
                }
                else
                {
                    respuesta.error.Response = true;
                    respuesta.error.Detalle = new DetailError { Codigo = "SB-C-Ex00023-495", Descripcion = "Este error fue generado en la funcion CrearCampaignEmail,", Mensaje = "No se pudo insertar la campania en sendingblue" };
                    return respuesta;
                }
            }
            catch (Exception e)
            {
                respuesta.error.Response = true;
                respuesta.error.Detalle = new DetailError { Codigo = "SB-C-Ex00023-495", Descripcion = "Este error fue generado en la funcion CrearCampaignEmail, linea: " + e.StackTrace.Split("line")[1] + " \n el mensaje enviado es: " + e.Message, Mensaje = "No se pudo crear las campañas" };
                return respuesta;
            }
        }
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Version: 1.0
        /// <summary>
        /// Metodo apra la isnercion de datos (POST)
        /// </summary>
        /// <param name="url">Url de consulta</param>
        /// <param name="objeto">objeto</param>
        /// <param name="listaObjeto">Lista de objetos</param>
        /// <returns> RespuestaGenerica </returns>
        public RespuestaGenerica ServicioCompletoDeConsultaPostURL(string url, Object? objeto,List<Object>? listaObjeto)
        {
            respuesta.error = new ErrorGenerico();
            try
            {
                WebClient comunicacion = new WebClient();
                comunicacion.Headers[HttpRequestHeader.ContentType] = "application/json";
                comunicacion.Headers["api-key"] = apikey;
                String result = "";
                if (objeto != null)
                {
                    result = comunicacion.UploadString(urlSendinBlue + url, JsonConvert.SerializeObject(objeto));
                }
                else if (listaObjeto != null)
                {
                    result = comunicacion.UploadString(urlSendinBlue + url, JsonConvert.SerializeObject(listaObjeto));
                }
                respuesta.SendingblueRespuesta = result;
                respuesta.error.Response = false;
                return respuesta;
            }catch(Exception ex)
            {
                respuesta.error.Response = true;
                respuesta.error.Detalle = new DetailError { Codigo = "SB-SCURL-Ex00001-N0", Descripcion = "Este error fue generado en la consulta por url,"+ex.Message, Mensaje = "Error en consulta http" };
                return respuesta;
            }
        }
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Version: 1.0
        /// <summary>
        /// Solicita datos de un endpoint (GET)
        /// </summary>
        /// <param name="url">url de consulta</param>
        /// <returns> RespuestaGenerica </returns>
        public RespuestaGenerica ServicioCompletoDeConsultaGetURL(string url)
        {
            respuesta.error = new ErrorGenerico();
            try
            {
                WebClient comunicacion = new WebClient();
                comunicacion.Headers[HttpRequestHeader.ContentType] = "application/json";
                comunicacion.Headers["api-key"] = apikey;
                String result = "";
                if (url != string.Empty)
                {
                    result = comunicacion.DownloadString(urlSendinBlue + url);
                }
                respuesta.SendingblueRespuesta = result;
                respuesta.error.Response = false;
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.error.Response = true;
                respuesta.error.Detalle = new DetailError { Codigo = "SB-SCURL-Ex00001-N1", Descripcion = "Este error fue generado en la consulta por url," + ex.Message, Mensaje = "Error en consulta http" };
                return respuesta;
            }
        }
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Version: 1.0
        /// <summary>
        /// Realiza la eliminacion de datos de a un endpoint (DELETE)
        /// </summary>
        /// <param name="url">Url de consulta</param>
        /// <param name="objeto">Objeto</param>
        /// <param name="listaObjeto">Lista de objetos</param>
        /// <returns> RespuestaGenerica </returns>
        public async Task<RespuestaGenerica> ServicioCompletoDeConsultaDeleteURL(string url, Object? objeto, List<Object>? listaObjeto)
        {
            respuesta.error = new ErrorGenerico();
            try
            {
                var client = new RestClient(urlSendinBlue);
                var solicitud = new RestRequest(url, Method.DELETE);
                solicitud.AddHeader("accept", "application/json");
                solicitud.AddHeader("api-key", apikey);
                var response = await client.Execute(solicitud);
                if (response.StatusCode == HttpStatusCode.OK || response.Content == "")
                {
                    Dictionary<string, bool> res = new Dictionary<string, bool>();
                    res.Add("Respuesta", true);
                    respuesta.SendingblueRespuesta = JsonConvert.SerializeObject(res);
                    respuesta.error.Response = false;
                    return respuesta;
                }
                else
                {
                    Dictionary<string, bool> res = new Dictionary<string, bool>();
                    res.Add("Respuesta", false);
                    respuesta.SendingblueRespuesta = JsonConvert.SerializeObject(res);
                    respuesta.error.Response = false;
                    return respuesta;
                }
            }
            catch (Exception ex)
            {
                respuesta.error.Response = true;
                respuesta.error.Detalle = new DetailError { Codigo = "SB-SCURL-Ex00001-N0", Descripcion = "Este error fue generado en la consulta por url," + ex.Message, Mensaje = "Error en consulta http" };
                return respuesta;
            }
        }
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Version: 1.0
        /// <summary>
        /// Realiza la actualizacion de datos a un endpoint (PUT)
        /// </summary>
        /// <param name="url">Url de consulta</param>
        /// <param name="objeto">Objeto</param>
        /// <param name="listaObjeto">Lista de objetos</param>
        /// <returns> RespuestaGenerica </returns>
        public async Task<RespuestaGenerica> ServicioCompletoDeConsultaUpdateURL(string url, Object? objeto, List<Object>? listaObjeto)
        {
            respuesta.error = new ErrorGenerico();
            try
            {
                WebClient comunicacion = new WebClient();
                comunicacion.Headers[HttpRequestHeader.ContentType] = "application/json";
                comunicacion.Headers["api-key"] = apikey;
                var result = comunicacion.UploadString(urlSendinBlue + url, WebRequestMethods.Http.Put, JsonConvert.SerializeObject(objeto));
                
            Dictionary<string, bool> res = new Dictionary<string, bool>();
            res.Add("Respuesta", true);
            respuesta.SendingblueRespuesta = JsonConvert.SerializeObject(res);
            respuesta.error.Response = false;
            return respuesta;
        }
            catch (Exception ex)
            {
                Dictionary<string, bool> res = new Dictionary<string, bool>();
                res.Add("Respuesta", false);
                respuesta.SendingblueRespuesta = JsonConvert.SerializeObject(res);
                respuesta.error.Response = true;
                respuesta.error.Detalle = new DetailError { Codigo = "SB-SCURL-Ex00001-N0", Descripcion = "Este error fue generado en la consulta por url," + ex.Message, Mensaje = "Error en consulta http" };
                return respuesta;
            }
        }

        public RespuestaGenerica CrearCampaignEmailHtmlContent(CrearCampaignEmailHtmlContentDTO campania)
        {
            respuesta.error = new ErrorGenerico();
            try
            {
                var ListasIds = new RecipientsCambiosSendinBlue();
                ListasIds.lists = new List<int>();
                ListasIds.lists.AddRange(campania.recipients.listIds);
                RespuestaGenerica respuesta = ServicioCompletoDeConsultaPostURL("emailCampaigns", campania, null);
                if (!respuesta.error.Response)
                {
                    var data = JsonConvert.DeserializeObject<Dictionary<string, Int64>>(respuesta.SendingblueRespuesta);
                    CrearSendinblueCamapaniaDTO crearSendinblue = new CrearSendinblueCamapaniaDTO()
                    {
                        Asunto = campania.subject,
                        ContenidoHtml = campania.htmlContent,
                        Estado = true,
                        Nombre = campania.name,
                        Receptor = JsonConvert.SerializeObject(ListasIds.lists),
                        Respuesta = respuesta.SendingblueRespuesta,
                        HoraProgramada = campania.scheduledAt,
                        IdSendinblueRemitente = Convert.ToInt32(data["id"]),
                        EstadoGuardado = true,
                        Campo = campania.toField,
                        PruebaAb = false,
                        AsuntoA = "",
                        AsuntoB = "",
                        GanadorCriterio = "",
                        ReglaDivision = 0,
                        GanadorTiempoAtraso = 0,
                    };
                    respuesta.SendingblueRespuesta = JsonConvert.SerializeObject(crearSendinblue);
                    respuesta.error.Response = false;
                    return respuesta;
                }
                else
                {
                    respuesta.error.Response = true;
                    respuesta.error.Detalle = new DetailError { Codigo = "SB-C-Ex00023-495", Descripcion = "Este error fue generado en la funcion CrearCampaignEmailPruebita,", Mensaje = "No se pudo insertar la campania en sendingblue" };
                    return respuesta;
                }
            }
            catch (Exception e)
            {
                respuesta.error.Response = true;
                respuesta.error.Detalle = new DetailError { Codigo = "SB-C-Ex00023-495", Descripcion = "Este error fue generado en la funcion CrearCampaignEmailPruebita, linea: " + e.StackTrace.Split("line")[1] + " \n el mensaje enviado es: " + e.Message, Mensaje = "No se pudo crear las campañas" };
                return respuesta;
            }
        }


        public RespuestaGenerica AgregarCampaniaABTestHtmlContent(CrearCampaniaSendinblueABTestHtmlContent aBTest)
        {
            respuesta.error = new ErrorGenerico();
            try
            {
                var ListasIds = new RecipientsCambiosSendinBlue();
                ListasIds.lists = new List<int>();
                ListasIds.lists.AddRange(aBTest.recipients.listIds);
                var serializer = new JavaScriptSerializer();
                var p = new CrearCampaniaSendinblue();
                WebClient comunicacion = new WebClient();
                comunicacion.Headers[HttpRequestHeader.ContentType] = "application/json";
                comunicacion.Headers["api-key"] = apikey;
                var result = comunicacion.UploadString("https://api.brevo.com/v3/emailCampaigns", JsonConvert.SerializeObject(aBTest)); //post
                if (result.Contains("id"))
                {
                    Dictionary<string, object> datos = new Dictionary<string, object>();
                    var data = JsonConvert.DeserializeObject<Dictionary<string, Int64>>(result);
                    CrearSendinblueCamapaniaDTO crearSendinblue = new CrearSendinblueCamapaniaDTO()
                    {
                        Asunto = aBTest.subject,
                        ContenidoHtml = aBTest.htmlContent,
                        Estado = true,
                        //IdPlantilla = aBTest.templateId,
                        Nombre = aBTest.name,
                        Receptor = JsonConvert.SerializeObject(ListasIds.lists),
                        Respuesta = result,
                        HoraProgramada = aBTest.scheduledAt,
                        AsuntoA = aBTest.subjectA,
                        AsuntoB = aBTest.subjectB,
                        EstadoGuardado = true,
                        PruebaAb = true,
                        GanadorCriterio = aBTest.winnerCriteria,
                        GanadorTiempoAtraso = aBTest.winnerDelay,
                        ReglaDivision = aBTest.splitRule,
                        Campo = aBTest.toField,
                        IdSendinblueRemitente = Convert.ToInt32(data["id"])
                    };
                    respuesta.SendingblueRespuesta = JsonConvert.SerializeObject(crearSendinblue);
                    respuesta.error.Response = false;
                    return respuesta;
                }
                else
                {
                    respuesta.error.Response = true;
                    respuesta.error.Detalle = new DetailError { Codigo = "SB-C-Ex00023-495", Descripcion = "Este error fue generado en la funcion CrearCampaignEmail,", Mensaje = "No se pudo insertar la campania en sendingblue" };
                    return respuesta;
                }
            }
            catch (Exception e)
            {
                respuesta.error.Response = true;
                respuesta.error.Detalle = new DetailError { Codigo = "SB-C-Ex00023-495", Descripcion = "Este error fue generado en la funcion CrearCampaignEmail, linea: " + e.StackTrace.Split("line")[1] + " \n el mensaje enviado es: " + e.Message, Mensaje = "No se pudo crear las campañas" };
                return respuesta;
            }
        }
    }
}
