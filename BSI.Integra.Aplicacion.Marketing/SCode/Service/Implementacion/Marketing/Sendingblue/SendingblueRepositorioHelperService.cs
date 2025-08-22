using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue;
using BSI.Integra.Aplicacion.Marketing.Service.Interface.Sendingblue;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio.TwiML.Voice;
using static BBSI.Integra.Persistencia.Entidades.IntegraDB.Sendingblue.TSendingblueContactosDTO;
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue.SendingBlueCampaniasDTO;
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue.SendingblueRespuestaGenericaDTO;
using static BSI.Integra.Persistencia.Entidades.IntegraDB.Sendingblue.IntegracionConIntegraDB.T_SendinblueAtributoDTO;
using static BSI.Integra.Persistencia.Entidades.IntegraDB.Sendingblue.IntegracionConIntegraDB.T_SendinblueCamapaniaDTO;
using static BSI.Integra.Persistencia.Entidades.IntegraDB.Sendingblue.IntegracionConIntegraDB.T_SendinblueCarpetaDTO;
using static BSI.Integra.Persistencia.Entidades.IntegraDB.Sendingblue.IntegracionConIntegraDB.T_SendinblueRelacionAlmunoSBDTO;
using static BSI.Integra.Persistencia.Entidades.IntegraDB.Sendingblue.IntegracionConIntegraDB.T_SendingblueListaDTO;

namespace BSI.Integra.Aplicacion.Marketing.Service.Implementacion.Sendingblue
{
    public class SendingblueRepositorioHelperService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ErrorGenerico error;

        public SendingblueRepositorioHelperService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            error = new ErrorGenerico();

        }
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una peticion para obtener los detalles de campania general detalle por el id de campania general
        /// </summary>
        /// <param name="RelacionDeAlumnosConSendingblue">Relacion de alumnos con sendingblue</param>
        /// <param name="usuaario">usuario que realiza la accion</param>
        /// <returns>Retorna Un objeto de tipo error que indica si existe error o no</returns>
        public ErrorGenerico AgregarRelacionAlumnoSendingBlue(List<CrearSendinblueRelacionAlmunoSB> RelacionDeAlumnosConSendingblue,string usuaario)
        {
            try
            {
                var DataAEnviar = RelacionDeAlumnosConSendingblue.Select(x => new CrearSendinblueRelacionAlmunoSB {
                Estado = true,
                FechaCreacion = DateTime.Now,
                FechaModificacion = DateTime.Now,
                IdAlumno = x.IdAlumno,
                IdSendinblue = x.IdSendinblue,
                UsuarioCreacion=usuaario,
                UsuarioModificacion=usuaario
                }).ToList();
                var resp = unitOfWork.sendingblueRelacionAlumnoSbRepository.Add(JsonConvert.SerializeObject(DataAEnviar));
                error.Response = false;
                return error;
            }
            catch (Exception e)
            {
                error.Response = true;
                error.Detalle = new DetailError { Codigo = "SB-CBD-Ex00001-27", Descripcion = "Este error fue generado en la funcion AgregarRelacionAlumnoSendingBlue, linea: " + e.StackTrace.Split("line")[1] + " \n el mensaje enviado es: " + e.Message, Mensaje = "No se pudo agregar las reaciones entre alumno y sendinblue" };
                return error;
            }
        }
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una peticion para agregar una relacion enre alumnos de sending blue y alumnos de integra
        /// </summary>
        /// <param name="RelacionDeAlumnosConSendingblue">Relacion de alumnos con sendingblue</param>
        /// <param name=usuario">usuario que realiza la accion</param>
        /// <returns>Retorna Un objeto de tipo error que indica si existe error o no</returns>
        public ErrorGenerico AgregarUnaRelacionAlumnoSendingBlue(CrearSendinblueRelacionAlmunoSB RelacionDeAlumnosConSendingblue,string usuario)
        {
            try
            {
                RelacionDeAlumnosConSendingblue.UsuarioModificacion = usuario;
                RelacionDeAlumnosConSendingblue.UsuarioCreacion = usuario;
                RelacionDeAlumnosConSendingblue.FechaModificacion = DateTime.Now;
                RelacionDeAlumnosConSendingblue.FechaCreacion = DateTime.Now;
                RelacionDeAlumnosConSendingblue.Estado = true;
                var resp = unitOfWork.sendingblueRelacionAlumnoSbRepository.Add(JsonConvert.SerializeObject(RelacionDeAlumnosConSendingblue));
                error.Response = false;
                return error;
            }
            catch (Exception e)
            {
                error.Response = true;
                error.Detalle = new DetailError { Codigo = "SB-CBD-Ex00002-51", Descripcion = "Este error fue generado en la funcion AgregarRelacionAlumnoSendingBlue, linea: " + e.StackTrace.Split("line")[1] + " \n el mensaje enviado es: " + e.Message, Mensaje = "No se pudo agregar las reaciones entre alumno y sendinblue" };
                return error;
            }
        }
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Agrega una relacion enre alumno de sending blue y de integra por json (usado para array de datos)
        /// </summary>
        /// <param name="RelacionDeAlumnosConSendingblue">Relacion de alumnos con sendingblue</param>
        /// <returns>Retorna Un objeto de tipo error que indica si existe error o no</returns>
        public ErrorGenerico AgregarUnaRelacionAlumnoSendingBlueArrayDeDatoss(string RelacionDeAlumnosConSendingblue)
        {
            try
            {
                var resp = unitOfWork.sendingblueRelacionAlumnoSbRepository.Add(RelacionDeAlumnosConSendingblue);
                error.Response = false;
                return error;
            }
            catch (Exception e)
            {
                error.Response = true;
                error.Detalle = new DetailError { Codigo = "SB-CBD-Ex00002-51", Descripcion = "Este error fue generado en la funcion AgregarRelacionAlumnoSendingBlue, linea: " + e.StackTrace.Split("line")[1] + " \n el mensaje enviado es: " + e.Message, Mensaje = "No se pudo agregar las reaciones entre alumno y sendinblue" };
                return error;
            }
        }
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Agrega atributos de sendinblue
        /// </summary>
        /// <param name="attr">Atributo de sendinblue</param>
        /// <param name="usuario">usuario que realiza la accion</param>
        /// <returns>Retorna Un objeto de tipo error que indica si existe error o no</returns>
        public ErrorGenerico AgregarAtributosDeSensingBlue(CrearSendinblueAtributo attr,string usuario)
        {
            try
            {
                attr.UsuarioCreacion = usuario;
                attr.UsuarioModificacion = usuario;
                attr.FechaCreacion = DateTime.Now;
                attr.FechaModificacion = DateTime.Now;
                attr.Estado= true;
                var resp = unitOfWork.sendinblueAtributoRepository.Add(attr);
                unitOfWork.Commit();
                error.Response = false;
                return error;
            }
            catch (Exception e)
            {
                error.Response = true;
                error.Detalle = new DetailError { Codigo = "SB-CBD-Ex00003-73", Descripcion = "Este error fue generado en la funcion AgregarAtributosDeSensingBlue, linea: " + e.StackTrace.Split("line")[1] + " \n el mensaje enviado es: " + e.Message, Mensaje = "No se pudo agregar los atributos" };
                return error;
            }
        }
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una peticion para agregar un nuevo contacto de sendinblue
        /// </summary>
        /// <param name="contacto">Contacto de sendinblue</param>
        /// <param name="usuario">usuario que realiza la accion</param>
        /// <returns>Retorna Un objeto de tipo error que indica si existe error o no</returns>
        public ErrorGenerico AgregarContactosDeSendingblue(CrearSendingblueContactos contacto,string usuario)
        {
            try
            {
                contacto.FechaCreacion = DateTime.Now;
                contacto.FechaModificacion=DateTime.Now;
                contacto.UsuarioCreacion = usuario;
                contacto.UsuarioModificacion = usuario;
                contacto.Estado = true;
                var cotncto = unitOfWork.sendinblueContactoRepository.ObtenerCampaniaPorEmail(contacto.Email);
                if (cotncto == null)
                {
                    var resp = unitOfWork.sendinblueContactoRepository.Add(JsonConvert.SerializeObject(contacto));
                    error.Response = false;
                }
                else
                {
                    error.Detalle= new DetailError{ Codigo="SB-DB-RC-000001",Descripcion="No es un error de procedimiento, es una validacion de duplicidad",Mensaje= "El contacto ya existe en la base de datos, valida los datos del usuario e intentelo de nuevo" };
                    error.Response = true;
                }
                return error;
            }
            catch (Exception e)
            {
                error.Response = true;
                error.Detalle = new DetailError { Codigo = "SB-CBD-Ex00003-94", Descripcion = "Este error fue generado en la funcion AgregarAtributosDeSensingBlue, linea: " + e.StackTrace.Split("line")[1] + " \n el mensaje enviado es: " + e.Message, Mensaje = "No se pudo agregar los atributos" };
                return error;
            }
        }
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una inseccion de adtos de contacto por emdio de json
        /// </summary>
        /// <param name="contacto">Contacto de sendinblue</param>
        /// <returns>Retorna Un objeto de tipo error que indica si existe error o no</returns>
        public ErrorGenerico AgregarContactosDeSendingbluejsonConvertido(string contacto)
        {
            try
            {

                    var resp = unitOfWork.sendinblueContactoRepository.Add(contacto);
                    error.Response = false;
               
                return error;
            }
            catch (Exception e)
            {
                error.Response = true;
                error.Detalle = new DetailError { Codigo = "SB-CBD-Ex00003-94", Descripcion = "Este error fue generado en la funcion AgregarAtributosDeSensingBlue, linea: " + e.StackTrace.Split("line")[1] + " \n el mensaje enviado es: " + e.Message, Mensaje = "No se pudo agregar los atributos" };
                return error;
            }
        }
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una inseccion de la entidad carpeta de sendinblue
        /// </summary>
        /// <param name="carpeta">Entidad a insertar</param>
        /// <param name="usuario">usuario que realiza la accion</param>
        /// <returns>Retorna Un objeto de tipo error que indica si existe error o no</returns>
        public ErrorGenerico AgregarCarpeta(CrearSendinblueCarpeta carpeta ,string nombre, string usuario)
        {
            try
            {
                CrearSendinblueCarpeta carp = new CrearSendinblueCarpeta
                {
                    Id = 0,
                    Estado = true,
                    EstadoGuardado = true,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    IdListaSendinblue = carpeta.Id,
                    ListaNegra = carpeta.ListaNegra != null ? carpeta.ListaNegra : 0,
                    Nombre = carpeta.Nombre!=null ? carpeta.Nombre : nombre,
                    Respuesta = JsonConvert.SerializeObject(carpeta),
                    SuscritoUnico = carpeta.SuscritoUnico != null ? carpeta.SuscritoUnico : 0,
                    TotalSuscrito = carpeta.TotalSuscrito != null ? carpeta.TotalSuscrito : 0,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario
                };
                var resp = unitOfWork.sendinblueCarpetaRepository.Add(carp);
                unitOfWork.Commit();
                error.Response = false;
                return error;
            }
            catch (Exception e)
            {
                error.Response = true;
                error.Detalle = new DetailError { Codigo = "SB-CBD-Ex00004-114", Descripcion = "Este error fue generado en la funcion AgregarCarpeta, linea: " + e.StackTrace.Split("line")[1] + " \n el mensaje enviado es: " + e.Message, Mensaje = "No se pudo agregar los atributos" };
                return error;
            }
        }
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una peticion para obtener los folders mas las listas
        /// </summary>
        /// <param name="idFolder">Identificador unico de folder</param>
        /// <returns>Retorna Un objeto de tipo respuestaGenerica que contiene la el folder ams la lista de listas de sendinblue</returns>
        public RespuestaGenerica ObtenerFolderMasListas(int idFolder)
        {
            try
            {
                RespuestaGenerica resultado = new RespuestaGenerica();
                var resp = unitOfWork.sendinblueCarpetaRepository.ObtenerFolderMasListas(idFolder);
                resultado.SendingblueRespuesta = JsonConvert.SerializeObject(resp);
                error.Response = false;
                error.Detalle = null;
                resultado.error = error;
                return resultado;
            }
            catch(Exception e)
            {
                RespuestaGenerica resultado = new RespuestaGenerica();
                error.Response = true;
                error.Detalle = new DetailError { Codigo = "SB-CBD-Ex00005-135", Descripcion = "Este error fue generado en la funcion ObtenerFolderMasLista, linea: " + e.StackTrace.Split("line")[1] + " \n el mensaje enviado es: " + e.Message, Mensaje = "No se pudo obtener las listas del folder" };
                resultado.error = error;
                return resultado;
            }
        }
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una peticion para actualziar contactos
        /// </summary>
        /// <param name="RelacionDeAlumnosConSendingblue">Relacion de alumnos con sendingblue</param>
        /// <param name="usuario">usuario que realiza la accion</param>
        /// <returns>Retorna Un objeto de tipo error que indica si existe error o no</returns>
        public ErrorGenerico ActualizarContactos(CrearSendingblueContactos RelacionDeAlumnosConSendingblue, string usuario)
        {
            try
            {
                RelacionDeAlumnosConSendingblue.UsuarioModificacion = usuario;
                RelacionDeAlumnosConSendingblue.FechaModificacion = DateTime.Now;
                RelacionDeAlumnosConSendingblue.Estado = true;
                var contacto = unitOfWork.sendinblueContactoRepository.ObtenerCampaniaPorEmail(RelacionDeAlumnosConSendingblue.Email);
                RelacionDeAlumnosConSendingblue.FechaCreacionSendinblue = contacto.FechaCreacionSendinblue;
                RelacionDeAlumnosConSendingblue.FechaCreacion = contacto.FechaCreacion;
                RelacionDeAlumnosConSendingblue.FechaModificacionSendinblue = contacto.FechaModificacionSendinblue;
                RelacionDeAlumnosConSendingblue.UsuarioModificacion= usuario;
                RelacionDeAlumnosConSendingblue.UsuarioCreacion = contacto.UsuarioCreacion;
                RelacionDeAlumnosConSendingblue.Id = contacto.Id;
                var resp = unitOfWork.sendinblueContactoRepository.Update(RelacionDeAlumnosConSendingblue);
                unitOfWork.Commit();
                error.Response = false;
                return error;
            }
            catch (Exception e)
            {
                error.Response = true;
                error.Detalle = new DetailError { Codigo = "SB-CBD-Ex00006-156", Descripcion = "Este error fue generado en la funcion ActualizarContactos, linea: " + e.StackTrace.Split("line")[1] + " \n el mensaje enviado es: " + e.Message, Mensaje = "No se pudo Actualziar el contacto" };
                return error;
            }
        }
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una eliminacion de contacto
        /// </summary>
        /// <param name="email">correo del contacto a eliminar</param>
        /// <param name="usuario">usuario que realiza la accion</param>
        /// <returns>Retorna Un objeto de tipo error que indica si existe error o no</returns>
        public ErrorGenerico EliminarContacto(string email, string usuario)
        {
            try
            {
                var contacto = unitOfWork.sendinblueContactoRepository.ObtenerCampaniaPorEmail(email);
                CrearSendingblueContactos contacEnviar = new CrearSendingblueContactos()
                {
                    Email = contacto.Email,
                    Atributo = contacto.Atributo,
                    Estado = false,
                    FechaCreacion = contacto.FechaCreacion,
                    UsuarioModificacion = usuario,
                    FechaModificacion = DateTime.Now,
                    EstadoGuardado = true,
                    FechaCreacionSendinblue = contacto.FechaCreacionSendinblue,
                    FechaModificacionSendinblue = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"),
                    Id = contacto.Id,
                    IdLista = contacto.IdLista,
                    ListaNegraCorreo = contacto.ListaNegraCorreo,
                    ListaNegroMensaje = contacto.ListaNegroMensaje,
                    Respuesta = contacto.Respuesta,
                    UsuarioCreacion = contacto.UsuarioCreacion
                };
                
                var resp = unitOfWork.sendinblueContactoRepository.Update(contacEnviar);
                unitOfWork.Commit();
                error.Response = false;
                return error;
            }
            catch (Exception e)
            {
                error.Response = true;
                error.Detalle = new DetailError { Codigo = "SB-CBD-Ex00007-177", Descripcion = "Este error fue generado en la funcion EliminarContacto, linea: " + e.StackTrace.Split("line")[1] + " \n el mensaje enviado es: " + e.Message, Mensaje = "No se pudo Eliminar el contacto" };
                return error;
            }
        }
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una peticion para isnertar campanias 
        /// </summary>
        /// <param name="datos">entidad a agregar</param>
        /// <param name="usuario">usuario que realiza la accion</param>
        /// <returns>Retorna Un objeto de tipo error que indica si existe error o no</returns>
        public ErrorGenerico AgregarCampanias(CrearSendinblueCamapaniaDTO datos,string usuario)
        {
            try
            {
                datos.UsuarioCreacion = usuario;
                datos.UsuarioModificacion = usuario;
                datos.FechaCreacion = DateTime.Now;
                datos.FechaModificacion = DateTime.Now;
                datos.Estado = true;
                var resp = unitOfWork.sendinblueCampaniaRepository.Add(datos);
                unitOfWork.Commit();
                error.Response = false;
                return error;
            }
            catch(Exception e)
            {
                error.Response = true;
                error.Detalle = new DetailError { Codigo = "SB-CBD-Ex00008-214", Descripcion = "Este error fue generado en la funcion AgregarPruebasAbTest, linea: " + e.StackTrace.Split("line")[1] + " \n el mensaje enviado es: " + e.Message, Mensaje = "No se pudo agregar la campania" };
                return error;
            }
        }
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una peticion para Agregar lsitas
        /// </summary>
        /// <param name="listaDTO">entidad de lista</param>
        /// <param name="usuario">usuario que realiza la accion</param>
        /// <returns>Retorna Un objeto de tipo error que indica si existe error o no</returns>
        public ErrorGenerico AgregarLista(CrearSendingblueListaDTO listaDTO,string usuario)
        {
            try
            {
                listaDTO.UsuarioCreacion = usuario;
                listaDTO.UsuarioModificacion = usuario;
                listaDTO.FechaModificacion = DateTime.Now;
                listaDTO.FechaCreacion = DateTime.Now;
                listaDTO.Estado = true;
                var resp = unitOfWork.sendinblueListaRepository.Add(listaDTO);
                unitOfWork.Commit();
                error.Response = false;
                return error;
            }
            catch (Exception e)
            {
                error.Response = true;
                error.Detalle = new DetailError { Codigo = "SB-CBD-Ex00009-311", Descripcion = "Este error fue generado en la funcion AgregarLista, linea: " + e.StackTrace.Split("line")[1] + " \n el mensaje enviado es: " + e.Message, Mensaje = "No se pudo agregar la lista" };
                return error;
            }
        }
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una peticion para obtener los detalles de campania general detalle por el id de campania general
        /// </summary>
        /// <param name="id">identificador unico de la lista</param>
        /// <param name="nombre">nombre de la lista</param>
        /// <param name="listacarptea">identificador unico de la carpeta</param>
        /// <param name="usuario">usuario que realiza la accion</param>
        /// <returns>Retorna Un objeto de tipo error que indica si existe error o no</returns>
        public ErrorGenerico ActualizaDataDeLista(CrearSendingblueListaDTO lista, string usuario)
        {
            try
            {
                var listaDTO = unitOfWork.sendinblueListaRepository.ObtenerListaPorIdSendinBlue(lista.IdSendinblueLista);
                if (listaDTO != null)
                {
                    listaDTO.TotalSuscrito = lista.TotalSuscrito;
                    listaDTO.TotalExcluido = lista.TotalExcluido;
                    listaDTO.UnicoSuscrito = lista.UnicoSuscrito;
                    listaDTO.UsuarioModificacion = usuario;
                    listaDTO.FechaModificacion = DateTime.Now;
                    var resp = unitOfWork.sendinblueListaRepository.UpdateObjetoTabla(listaDTO);
                    unitOfWork.Commit();
                    error.Response = false;
                    return error;
                }
                error.Response = true;
                return error;
            }
            catch (Exception e)
            {
                error.Response = true;
                error.Detalle = new DetailError { Codigo = "SB-CBD-Ex00010-332", Descripcion = "Este error fue generado en la funcion ActualizarLista, linea: " + e.StackTrace.Split("line")[1] + " \n el mensaje enviado es: " + e.Message, Mensaje = "No se pudo actualizar la lista" };
                return error;
            }
        }
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una peticion para obtener los detalles de campania general detalle por el id de campania general
        /// </summary>
        /// <param name="id">identificador unico de la lista</param>
        /// <param name="nombre">nombre de la lista</param>
        /// <param name="listacarptea">identificador unico de la carpeta</param>
        /// <param name="usuario">usuario que realiza la accion</param>
        /// <returns>Retorna Un objeto de tipo error que indica si existe error o no</returns>
        public ErrorGenerico ActualizarLista(int id,string nombre,int listacarptea,string usuario)
        {
            try
            {
                var listaDTO = unitOfWork.sendinblueListaRepository.ObtenerListaPorIdSendinBlue(id);
                if (listaDTO != null)
                {
                    listaDTO.UsuarioModificacion = usuario;
                    listaDTO.Nombre= nombre;
                    listaDTO.IdSendinblueCarpeta = listacarptea;
                    listaDTO.FechaModificacion = DateTime.Now;
                    var resp = unitOfWork.sendinblueListaRepository.UpdateObjetoTabla(listaDTO);
                    unitOfWork.Commit();
                    error.Response = false;
                    return error;
                }
                error.Response = true;
                return error;
            }
            catch (Exception e)
            {
                error.Response = true;
                error.Detalle = new DetailError { Codigo = "SB-CBD-Ex00010-332", Descripcion = "Este error fue generado en la funcion ActualizarLista, linea: " + e.StackTrace.Split("line")[1] + " \n el mensaje enviado es: " + e.Message, Mensaje = "No se pudo actualizar la lista" };
                return error;
            }
        }
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una peticion para eliminado de la lista
        /// </summary>
        /// <param name="id">identificador unico de la lista</param>
        /// <param name="usuario">usuario que realiza la accion</param>
        /// <returns>Retorna Un objeto de tipo error que indica si existe error o no</returns>
        public ErrorGenerico EliminarLista(int id, string usuario)
        {
            try
            {
                var listaDTO = unitOfWork.sendinblueListaRepository.ObtenerListaPorId(id);
                if (listaDTO != null)
                {
                    listaDTO.UsuarioModificacion = usuario;
                    listaDTO.FechaModificacion = DateTime.Now;
                    listaDTO.Estado = false;
                    var resp = unitOfWork.sendinblueListaRepository.UpdateObjetoTabla(listaDTO);
                    unitOfWork.Commit();
                    error.Response = false;
                    return error;
                }
                error.Response = true;
                return error;
            }
            catch (Exception e)
            {
                error.Response = true;
                error.Detalle = new DetailError { Codigo = "SB-CBD-Ex00011-357", Descripcion = "Este error fue generado en la funcion ActualizarLista, linea: " + e.StackTrace.Split("line")[1] + " \n el mensaje enviado es: " + e.Message, Mensaje = "No se pudo actualizar la lista" };
                return error;
            }
        }
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Versión: 1.0
        /// <summary>
        /// metodo para agregar contactos de sendingblue por medio de un array
        /// </summary>
        /// <param name="contacto">Lista de contacto</param>
        /// <param name="usuaario">usuario que realiza la accion</param>
        /// <returns>Retorna Un objeto de tipo error que indica si existe error o no</returns>
        public ErrorGenerico AgregarContactosDeSendingblueArray(List<CrearSendingblueContactos> contacto, string usuaario)
        {
            try
            {
                var DataAEnviar = contacto.Select(x => new CrearSendingblueContactos
                {
                    Estado = true,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    Atributo = x.Atributo,
                    Email = x.Email,
                    EstadoGuardado = x.EstadoGuardado,
                    FechaCreacionSendinblue = x.FechaCreacionSendinblue,
                    FechaModificacionSendinblue = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.zzz.k"),
                    IdLista= x.IdLista,
                    ListaNegraCorreo=x.ListaNegraCorreo,
                    ListaNegroMensaje =x.ListaNegroMensaje,
                    Respuesta = x.Respuesta,
                    Id = x.Id,
                    UsuarioCreacion = usuaario,
                    UsuarioModificacion = usuaario
                }).ToList();

                var resp = unitOfWork.sendinblueContactoRepository.Add(JsonConvert.SerializeObject(contacto));
                    error.Response = false;

                return error;
            }
            catch (Exception e)
            {
                error.Response = true;
                error.Detalle = new DetailError { Codigo = "SB-CBD-Ex00010-94", Descripcion = "Este error fue generado en la funcion AgregarAtributosDeSensingBlue, linea: " + e.StackTrace.Split("line")[1] + " \n el mensaje enviado es: " + e.Message, Mensaje = "No se pudo agregar los atributos" };
                return error;
            }
        }
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 30/03/2023
        /// Versión: 1.0
        /// <summary>
        /// Este metodo es usado apra agregar la arelacione ntre las nuevas listas y lso contactos
        /// </summary>
        /// <param name="datos">Lista de correos mas el id de la lista de sendiblue</param>
        /// <param name="usuario">usuario que realiza la accion</param>
        /// <returns>No retorna valores</returns>
        public void AgregarRelacionContactosLista(CrearContactosListaDto datos , string usuario)
        {
            try
            {
                List<SendinblueRelacionListaContactoEmailDTO> datosinsert = new List<SendinblueRelacionListaContactoEmailDTO>();
                foreach(var i in datos.email)
                {
                    SendinblueRelacionListaContactoEmailDTO creado = new()
                    {
                        Email = i,
                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        IdSendinblueLista = Convert.ToInt32(datos.idList),
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario
                    };
                    datosinsert.Add(creado);
                }
                unitOfWork.SendinblueRelacionListaContactoEmailRepository.Add(datosinsert);
                unitOfWork.Commit();    
            }catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
