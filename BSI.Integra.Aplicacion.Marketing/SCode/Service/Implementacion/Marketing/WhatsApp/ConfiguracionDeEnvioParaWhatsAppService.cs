using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp;
using BSI.Integra.Aplicacion.Marketing.Service.Interface.Marketing.WhatsApp;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Nancy.Json;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue.SendingblueRespuestaGenericaDTO;

namespace BSI.Integra.Aplicacion.Marketing.Service.Implementacion.Marketing.WhatsApp
{
    public class ConfiguracionDeEnvioParaWhatsAppService : IConfiguracionDeEnvioParaWhatsAppService
    {

        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        private RespuestaGenerica respuesta;
        private ErrorGenerico error;

        public ConfiguracionDeEnvioParaWhatsAppService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TConfiguracionDeEnvioParaWhatsApp, ConfiguracionDeEnvioParaWhatsApp> (MemberList.None).ReverseMap();
                cfg.CreateMap<ConfiguracionDeEnvioParaWhatsAppDTO, ConfiguracionDeEnvioParaWhatsAppCreate>(MemberList.None).ReverseMap();
                cfg.CreateMap<ConfiguracionDeEnvioParaWhatsApp, ConfiguracionDeEnvioParaWhatsAppCreate>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 2022/12/22
        /// Versión: 1.0
        /// <summary>
        /// Realiza una peticion para agregar una configuracion de envio para whatsapp
        /// </summary>
        /// <param name="entidad">Objeto el cual es necesario para la consulta</param>
        /// <param name="usuario">Usuario que realiza la peticion</param>
        /// <returns>Retorna respuesta generica</returns>
        public RespuestaGenerica Add(ConfiguracionDeEnvioParaWhatsAppCreate entidad1, string usuario)
        {
            try
            {


                ConfiguracionDeEnvioParaWhatsApp entidad = new ConfiguracionDeEnvioParaWhatsApp()
                {
                    FechaDeEnvio = Convert.ToDateTime(entidad1.FechaDeEnvio),
                    FechaFinDeEnvio = Convert.ToDateTime(entidad1.FechaFinDeEnvio),
                    HoraDeEnvio = entidad1.HoraDeEnvio,
                    IdCampaniaGeneralDetalle = entidad1.IdCampaniaGeneralDetalle,
                    Nombre=entidad1.Nombre,
                    IdPlantilla = entidad1.IdPlantilla,
                    TiempoEntreEnvios = entidad1.TiempoEntreEnvios,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario,
                    FechaModificacion = DateTime.Now,
                    FechaCreacion = DateTime.Now,
                    Estado = true,
                };
                
                var modelo = _unitOfWork.ConfiguracionDeEnvioParaWhatsAppRepository.Add(entidad);
                _unitOfWork.Commit();
                respuesta = new RespuestaGenerica();
                respuesta.SendingblueRespuesta= JsonConvert.SerializeObject(_mapper.Map<TConfiguracionDeEnvioParaWhatsApp>(modelo));
                respuesta.error = new ErrorGenerico()
                {
                    Response = false,
                };

                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta = new RespuestaGenerica();
                respuesta.error = new ErrorGenerico()
                {
                    Response = true,
                    Detalle = new DetailError
                    {
                        Codigo = "CDEPWS-00001",
                        Descripcion = "El error se produjo en la funcion: Add, en ConfiguracionDeEnvioParaWhatsAppService linea: " + ex.StackTrace.Split("line")[1] + " \n el mensaje enviado es: " + ex.Message,
                        Mensaje = "No se pudo agregar la configuracion de envio"
                    }
                };
                return respuesta;
            }
        }
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 2022/12/22
        /// Versión: 1.0
        /// <summary>
        /// Realiza una peticion para actualizar una configuracion de envio para whatsapp
        /// </summary>
        /// <param name="entidad">Objeto el cual es necesario para la consulta</param>
        /// <param name="usuario">Usuario que realiza la peticion</param>
        /// <returns>Retorna respuesta generica</returns>
        public RespuestaGenerica Update(ConfiguracionDeEnvioParaWhatsAppCreate entidad1, string usuario)
        {
            try
            {
                var entidad = _mapper.Map<ConfiguracionDeEnvioParaWhatsApp>(entidad1);
                var datosAnteriores = _unitOfWork.ConfiguracionDeEnvioParaWhatsAppRepository.FirstById(entidad1.Id);
                entidad.FechaFinDeEnvio = Convert.ToDateTime(entidad1.FechaFinDeEnvio).AddHours(1);
                entidad.FechaDeEnvio = Convert.ToDateTime(entidad1.FechaDeEnvio).AddHours(1);
                entidad.UsuarioModificacion = usuario;
                entidad.UsuarioCreacion = datosAnteriores.UsuarioCreacion;
                entidad.FechaCreacion = datosAnteriores.FechaCreacion;
                entidad.FechaModificacion = DateTime.Now;
                entidad.Estado = true;
                var modelo = _unitOfWork.ConfiguracionDeEnvioParaWhatsAppRepository.Update(entidad);
                _unitOfWork.Commit();
                respuesta = new RespuestaGenerica();
                respuesta.SendingblueRespuesta = JsonConvert.SerializeObject(_mapper.Map<TConfiguracionDeEnvioParaWhatsApp>(modelo));
                respuesta.error = new ErrorGenerico()
                {
                    Response = false,
                };

                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta = new RespuestaGenerica();
                respuesta.error = new ErrorGenerico()
                {
                    Response = true,
                    Detalle = new DetailError
                    {
                        Codigo = "CDEPWS-00002",
                        Descripcion = "El error se produjo en la funcion: Update, en ConfiguracionDeEnvioParaWhatsAppService linea: " + ex.StackTrace.Split("line")[1] + " \n el mensaje enviado es: " + ex.Message,
                        Mensaje = "No se pudo actualizar la configuracion de envio"
                    }
                };
                return respuesta;
            }
        }
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 2022/12/22
        /// Versión: 1.0
        /// <summary>
        /// Realiza una peticion para eliminar una configuracion de envio para whatsapp
        /// </summary>
        /// <param name="id">identificador unico de configuracion de envio para whatsapp</param>
        /// <param name="usuario">Usuario que realiza la peticion</param>
        /// <returns>Retorna respuesta generica</returns>
        public RespuestaGenerica Delete(int id, string usuario)
        {
            try
            {
                _unitOfWork.ConfiguracionDeEnvioParaWhatsAppRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                respuesta = new RespuestaGenerica();
                Dictionary<string, bool> res = new Dictionary<string, bool>();
                res.Add("Respuesta", false);
                respuesta.SendingblueRespuesta = JsonConvert.SerializeObject(res);
                respuesta.error = new ErrorGenerico()
                {
                    Response = false,
                };

                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta = new RespuestaGenerica();
                respuesta.error = new ErrorGenerico()
                {
                    Response = true,
                    Detalle = new DetailError
                    {
                        Codigo = "CDEPWS-00003",
                        Descripcion = "El error se produjo en la funcion: Delete, en ConfiguracionDeEnvioParaWhatsAppService linea: " + ex.StackTrace.Split("line")[1] + " \n el mensaje enviado es: " + ex.Message,
                        Mensaje = "No se pudo eliminar la configuracion de envio"
                    }
                };
                return respuesta;
            }
        }
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 2022/12/22
        /// Versión: 1.0
        /// <summary>
        /// Realiza una peticion para agregar una lista de configuraciones de envio para whatsapp
        /// </summary>
        /// <param name="listadoEntidad">Objeto el cual es necesario para la consulta</param>
        /// <param name="usuario">Usuario que realiza la peticion</param>
        /// <returns>Retorna respuesta generica</returns>
        public RespuestaGenerica Add(List<ConfiguracionDeEnvioParaWhatsAppDTO> listadoEntidad, string usuario)
        {
            try
            {
                var listadoEntidad1 = listadoEntidad.Select(x => new ConfiguracionDeEnvioParaWhatsApp
                {
                    FechaDeEnvio = x.FechaDeEnvio,
                    FechaFinDeEnvio = x.FechaFinDeEnvio,
                    HoraDeEnvio = x.HoraDeEnvio,
                    Id = x.Id,
                    IdCampaniaGeneralDetalle = x.IdCampaniaGeneralDetalle,
                    IdPlantilla = x.IdPlantilla,
                    TiempoEntreEnvios = x.TiempoEntreEnvios,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario,
                    FechaModificacion = DateTime.Now,
                    FechaCreacion = DateTime.Now,
                    Estado = true,
                }).ToList();
                var modelo = _unitOfWork.ConfiguracionDeEnvioParaWhatsAppRepository.Add(listadoEntidad1);
                _unitOfWork.Commit();
                respuesta = new RespuestaGenerica();
                respuesta.SendingblueRespuesta = JsonConvert.SerializeObject(_mapper.Map<List<TConfiguracionDeEnvioParaWhatsApp>>(modelo));
                respuesta.error = new ErrorGenerico()
                {
                    Response = false,
                };

                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta = new RespuestaGenerica();
                respuesta.error = new ErrorGenerico()
                {
                    Response = true,
                    Detalle = new DetailError
                    {
                        Codigo = "CDEPWS-00004",
                        Descripcion = "El error se produjo en la funcion: Add, en ConfiguracionDeEnvioParaWhatsAppService linea: " + ex.StackTrace.Split("line")[1] + " \n el mensaje enviado es: " + ex.Message,
                        Mensaje = "No se pudo agregar las configuraciones de envio"
                    }
                };
                return respuesta;
            }
        }
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 2022/12/22
        /// Versión: 1.0
        /// <summary>
        /// Realiza una peticion para actualizar una lista de configuraciones de envio para whatsapp
        /// </summary>
        /// <param name="listadoEntidad">Objeto el cual es necesario para la consulta</param>
        /// <param name="usuario">Usuario que realiza la peticion</param>
        /// <returns>Retorna respuesta generica</returns>
        public RespuestaGenerica Update(List<ConfiguracionDeEnvioParaWhatsAppDTO> listadoEntidad, string usuario)
        {
            try
            {
                var listadoEntidad1 = listadoEntidad.Select(x => new ConfiguracionDeEnvioParaWhatsApp
                {
                    FechaDeEnvio = x.FechaDeEnvio,
                    FechaFinDeEnvio = x.FechaFinDeEnvio,
                    HoraDeEnvio = x.HoraDeEnvio,
                    Id = x.Id,
                    IdCampaniaGeneralDetalle = x.IdCampaniaGeneralDetalle,
                    IdPlantilla = x.IdPlantilla,
                    TiempoEntreEnvios = x.TiempoEntreEnvios,
                    UsuarioCreacion = x.UsuarioCreacion,
                    UsuarioModificacion = usuario,
                    FechaModificacion = x.FechaModificacion,
                    FechaCreacion = DateTime.Now,
                    Estado = true,
                }).ToList();
                var modelo = _unitOfWork.ConfiguracionDeEnvioParaWhatsAppRepository.Update(listadoEntidad1);
                _unitOfWork.Commit();
                respuesta = new RespuestaGenerica();
                respuesta.SendingblueRespuesta = JsonConvert.SerializeObject(_mapper.Map<List<TConfiguracionDeEnvioParaWhatsApp>>(modelo));
                respuesta.error = new ErrorGenerico()
                {
                    Response = false,
                };
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta = new RespuestaGenerica();
                respuesta.error = new ErrorGenerico()
                {
                    Response = true,
                    Detalle = new DetailError
                    {
                        Codigo = "CDEPWS-00005",
                        Descripcion = "El error se produjo en la funcion: Update, en ConfiguracionDeEnvioParaWhatsAppService linea: " + ex.StackTrace.Split("line")[1] + " \n el mensaje enviado es: " + ex.Message,
                        Mensaje = "No se pudo actualizar las configuraciones de envio"
                    }
                };
                return respuesta;
            }
        }
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 2022/12/22
        /// Versión: 1.0
        /// <summary>
        /// Realiza una peticion para eliminar una lista de configuracion de envio para whatsapp
        /// </summary>
        /// <param name="listadoIds">lista de id's necesario para la consulta</param>
        /// <param name="usuario">Usuario que realiza la peticion</param>
        /// <returns>Retorna respuesta generica</returns>
        public RespuestaGenerica Delete(List<int> listadoIds, string usuario)
        {
            try
            {
                _unitOfWork.ConfiguracionDeEnvioParaWhatsAppRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                Dictionary<string, bool> res = new Dictionary<string, bool>();
                res.Add("Respuesta", false);
                respuesta.SendingblueRespuesta = JsonConvert.SerializeObject(res);
                respuesta.error = new ErrorGenerico()
                {
                    Response = false,
                };
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta = new RespuestaGenerica();
                respuesta.error = new ErrorGenerico()
                {
                    Response = true,
                    Detalle = new DetailError
                    {
                        Codigo = "CDEPWS-00006",
                        Descripcion = "El error se produjo en la funcion: Delete, en ConfiguracionDeEnvioParaWhatsAppService linea: " + ex.StackTrace.Split("line")[1] + " \n el mensaje enviado es: " + ex.Message,
                        Mensaje = "No se pudo elimminar las configuraciones de envio"
                    }
                };
                return respuesta;
            }
        }
        #endregion
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 2022/12/22
        /// Versión: 1.0
        /// <summary>
        /// Realiza una peticion para obtener una configuracion de envio para whatsapp
        /// </summary>
        /// <param name="id">identificador unico de configuracion de envio para whatsApp</param>
        /// <returns>Retorna respuesta generica</returns>
        public RespuestaGenerica GetById(int id)
        {
            try
            {
                respuesta = new RespuestaGenerica();
                respuesta.SendingblueRespuesta = JsonConvert.SerializeObject(_unitOfWork.ConfiguracionDeEnvioParaWhatsAppRepository.ObtenerPrioridadPAraEnvioDeWpp(id));
                respuesta.error = new ErrorGenerico()
                {
                    Response = false,
                };
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta = new RespuestaGenerica();
                respuesta.error = new ErrorGenerico()
                {
                    Response = true,
                    Detalle = new DetailError
                    {
                        Codigo = "CDEPWS-00007",
                        Descripcion = "El error se produjo en la funcion: GetById, en ConfiguracionDeEnvioParaWhatsAppService linea: " + ex.StackTrace.Split("line")[1] + " \n el mensaje enviado es: " + ex.Message,
                        Mensaje = "No se pudo encontrar la configuraciones de envio solicitada"
                    }
                };
                return respuesta;
            }
        }
    }
}
