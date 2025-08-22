using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    public class RegistroRecuperacionWhatsAppRepository : GenericRepository<TRegistroRecuperacionWhatsApp>, IRegistroRecuperacionWhatsAppRepository
    {
        private Mapper _mapper;
        private object idPersonal;

        public RegistroRecuperacionWhatsAppRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TRegistroRecuperacionWhatsApp, RegistroRecuperacionWhatsAppDTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TRegistroRecuperacionWhatsApp MapeoEntidad(RegistroRecuperacionWhatsAppDTO entidad)
        {
            try
            {
                TRegistroRecuperacionWhatsApp modelo = _mapper.Map<TRegistroRecuperacionWhatsApp>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TRegistroRecuperacionWhatsApp Add(RegistroRecuperacionWhatsAppDTO entidad)
        {
            try
            {
                var RegistroRecuperacionWhatsAppDTO = MapeoEntidad(entidad);
                base.Insert(RegistroRecuperacionWhatsAppDTO);
                return RegistroRecuperacionWhatsAppDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TRegistroRecuperacionWhatsApp Update(RegistroRecuperacionWhatsAppDTO entidad)
        {
            try
            {
                var RegistroRecuperacionWhatsAppDTO = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                RegistroRecuperacionWhatsAppDTO.RowVersion = entidadExistente.RowVersion;

                base.Update(RegistroRecuperacionWhatsAppDTO);
                return RegistroRecuperacionWhatsAppDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TRegistroRecuperacionWhatsApp Update(TRegistroRecuperacionWhatsApp entidad)
        {
            try
            {
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                entidad.RowVersion = entidadExistente.RowVersion;

                base.Update(entidad);
                return entidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(int id, string usuario)
        {
            try
            {
                base.Delete(id, usuario);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public IEnumerable<TRegistroRecuperacionWhatsApp> Add(IEnumerable<RegistroRecuperacionWhatsAppDTO> listadoEntidad)
        {
            try
            {
                List<TRegistroRecuperacionWhatsApp> listado = new List<TRegistroRecuperacionWhatsApp>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }
                base.Insert(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<TRegistroRecuperacionWhatsApp> Update(IEnumerable<RegistroRecuperacionWhatsAppDTO> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TRegistroRecuperacionWhatsApp> listado = new List<TRegistroRecuperacionWhatsApp>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }

                var infoExistente = base.GetBy(w => listadoEntidad.Select(s => s.Id).Contains(w.Id), s => new { s.Id, s.RowVersion });
                foreach (var item in listado)
                {
                    var entidadExistente = infoExistente.FirstOrDefault(w => w.Id == item.Id);
                    item.RowVersion = entidadExistente.RowVersion;
                }
                base.Update(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Delete(IEnumerable<int> listadoIds, string usuario)
        {
            try
            {
                base.Delete(listadoIds, usuario);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        /// <summary>
        /// Actualiza el estado de completado de WhatsApp
        /// </summary>
        /// <param name="idCampaniaGeneral">Id de la campania general (PK de la tabla mkt.T_CampaniaGeneral)</param>
        /// <param name="idCampaniaGeneralDetalleResponsable">Id del responsable la campania general (PK de la tabla mkt.T_CampaniaGeneralDetalleResponsable)</param
        /// <returns>Booleano con true o false</returns>
        public bool ActualizarCompletadoRegistroWhatsApp(int idCampaniaGeneralDetalle, int idCampaniaGeneralDetalleResponsable)
        {
            try
            {
                var spActualizarCompletadoWhatsApp = "[mkt].[SP_ActualizarCompletadoRegistroWhatsApp]";
                var resultadoSp = _dapperRepository.QuerySPFirstOrDefault(spActualizarCompletadoWhatsApp, new { IdCampaniaGeneralDetalle = idCampaniaGeneralDetalle, IdCampaniaGeneralDetalleResponsable = idCampaniaGeneralDetalleResponsable });

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene la cantidad de peticiones rechazadas por caida del servidor (Servicios 2)
        /// </summary>
        /// <returns>Obtiene la cantidad de solicitudes erradas</returns>
        public int ObtenerCantidadCaidaRecuperacionWhatsApp()
        {
            try
            {
                int cantidadCaida = 0;
                var consultaCantidadServidorInhabilitado = "SELECT Valor FROM mkt.V_ObtenerCantidadCaidaMinuto";
                var resultadoVista = _dapperRepository.FirstOrDefault(consultaCantidadServidorInhabilitado, null);

                if (!string.IsNullOrEmpty(resultadoVista) && !resultadoVista.Contains("[]") && resultadoVista != "null")
                {
                    var cantidadResultante = JsonConvert.DeserializeObject<ValorIntDTO>(resultadoVista);

                    cantidadCaida = Convert.ToInt32(cantidadResultante.Valor);
                }

                return cantidadCaida;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene la cantidad de WhatsApp Preprocesada
        /// </summary>
        /// <param name="idCampaniaGeneralDetalle">Id de la campania general detalle (PK de la tabla mkt.T_CampaniaGeneralDetalle)</param>
        /// <param name="idPersonal">Id del personal (PK de la tabla gp.T_Personal)</param>
        /// <param name="fechaInicio">Fecha de inicio de la busqueda de envios</param>
        /// <param name="fechaFin">Fecha de finalizacion de la busqueda de envios</param>
        /// <returns>Booleano con true o false</returns>
        public int ObtenerCantidadWhatsAppPreprocesadoRealizado(int idCampaniaGeneralDetalle, int idPersonal, DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                int cantidadWhatsAppRealizada = 0;

                var spActualizarCompletadoWhatsApp = "[mkt].[SP_ObtenerCantidadWhatsAppPreprocesado]";
                var resultadoSp = _dapperRepository.QuerySPFirstOrDefault(spActualizarCompletadoWhatsApp, new { IdCampaniaGeneralDetalle = idCampaniaGeneralDetalle, IdPersonal = idPersonal, FechaInicio = fechaInicio, FechaFin = fechaFin });

                if (!string.IsNullOrEmpty(resultadoSp) && !resultadoSp.Contains("[]") && resultadoSp != "null")
                {
                    var cantidadResultante = JsonConvert.DeserializeObject<ValorIntDTO>(resultadoSp);

                    cantidadWhatsAppRealizada = Convert.ToInt32(cantidadResultante.Valor);
                }

                return cantidadWhatsAppRealizada;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Actualiza el estado de completado de WhatsApp
        /// </summary>
        /// <param name="idCampaniaGeneral">Id de la campania general (PK de la tabla mkt.T_CampaniaGeneral)</param>
        /// <param name="idCampaniaGeneralDetalleResponsable">Id del responsable la campania general (PK de la tabla mkt.T_CampaniaGeneralDetalleResponsable)</param
        /// <returns>Booleano con true o false</returns>
        public bool ActualizarFalloRegistroWhatsApp(int idCampaniaGeneralDetalle, int idCampaniaGeneralDetalleResponsable)
        {
            try
            {
                var spActualizarCompletadoWhatsApp = "[mkt].[SP_ActualizarFalloRegistroWhatsApp]";
                var resultadoSp = _dapperRepository.QuerySPFirstOrDefault(spActualizarCompletadoWhatsApp, new { IdCampaniaGeneralDetalle = idCampaniaGeneralDetalle, IdCampaniaGeneralDetalleResponsable = idCampaniaGeneralDetalleResponsable });

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Actualiza el estado de completado de WhatsApp
        /// </summary>
        /// <param name="usuario">Usuario responsable</param>
        /// <returns>Booleano con true o false</returns>
        public bool DesactivarCompletadoRegistroWhatsApp(string usuario)
        {
            try
            {
                var spDesactivarCompletadoWhatsApp = "[mkt].[SP_DesactivarHistorialCompletadoRegistroWhatsApp]";
                var resultadoSp = _dapperRepository.QuerySPFirstOrDefault(spDesactivarCompletadoWhatsApp, new { UsuarioResponsable = usuario });

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<RegistroRecuperacionWhatsAppDTO> GetBy(Expression<Func<TRegistroRecuperacionWhatsApp, bool>> filter)
        {
            IEnumerable<TRegistroRecuperacionWhatsApp> listado = base.GetBy(filter);
            List<RegistroRecuperacionWhatsAppDTO> listadoBO = new List<RegistroRecuperacionWhatsAppDTO>();
            foreach (var itemEntidad in listado)
            {
                RegistroRecuperacionWhatsAppDTO objetoBO = _mapper.Map<RegistroRecuperacionWhatsAppDTO>(listado);
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public TRegistroRecuperacionWhatsApp FirstBy(Expression<Func<TRegistroRecuperacionWhatsApp, bool>> filter)
        {
            try
            {
                return base.FirstBy(filter);
            }catch(Exception e)
            {
                throw e;
            }
        }
        public List<TRegistroRecuperacionWhatsApp> Insert(List<RegistroRecuperacionWhatsAppDTO> registroSeguimientoRecuperacion)
        {
            try
            {
                var data = _mapper.Map<List<TRegistroRecuperacionWhatsApp>>(registroSeguimientoRecuperacion);
                base.Insert(data);
                return data;
            }catch(Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Obtiene la cantidad de peticiones rechazadas por caida del servidor (Servicios 2)
        /// </summary>
        /// <returns>Obtiene la cantidad de solicitudes erradas</returns>
        public List<AsesoresMktDTO> ListaAsesoresMarketing()
        {
            try
            {
                List<AsesoresMktDTO> cantidadCaida = new List<AsesoresMktDTO>();

                var consultaCantidadServidorInhabilitado = "SELECT P.Email FROM mkt.T_AsesorMarketing AS AM INNER JOIN gp.T_Personal AS P ON P.Id = AM.IdPersonal WHERE AM.Estado = 1";
                var resultadoVista = _dapperRepository.QueryDapper(consultaCantidadServidorInhabilitado, null);

                if (!string.IsNullOrEmpty(resultadoVista) && !resultadoVista.Contains("[]") && resultadoVista != "null")
                {
                    cantidadCaida = JsonConvert.DeserializeObject<List<AsesoresMktDTO>>(resultadoVista);

                }

                return cantidadCaida;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
