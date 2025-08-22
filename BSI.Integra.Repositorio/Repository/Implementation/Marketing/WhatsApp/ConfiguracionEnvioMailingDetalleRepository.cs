using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ConfiguracionEnvioMailingDetalleRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 12/08/2022
    /// <summary>
    /// Gestión general de T_ConfiguracionEnvioMailingDetalle
    /// </summary>
    public class ConfiguracionEnvioMailingDetalleRepository : GenericRepository<TConfiguracionEnvioMailingDetalle>, IConfiguracionEnvioMailingDetalleRepository
    {
        private Mapper _mapper;

        public ConfiguracionEnvioMailingDetalleRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TConfiguracionEnvioMailingDetalle, ConfiguracionEnvioMailingDetalle>(MemberList.None).ReverseMap();
                cfg.CreateMap<TAgendaTab, AgendaTab>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        private TAgendaTab MapeoEntidad(AgendaTab entidad)
        {
            try
            {
                TAgendaTab modelo = _mapper.Map<TAgendaTab>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private AgendaTab MapeoEntidadReverse(TAgendaTab entidad)
        {
            try
            {
                AgendaTab modelo = _mapper.Map<AgendaTab>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Metodos Base

        public IEnumerable<ConfiguracionEnvioMailingDetalle> GetBy(Expression<Func<TConfiguracionEnvioMailingDetalle, bool>> filter)
        {
            IEnumerable<TConfiguracionEnvioMailingDetalle> listado = base.GetBy(filter);
            List<ConfiguracionEnvioMailingDetalle> listadoBO = new List<ConfiguracionEnvioMailingDetalle>();
            foreach (var itemEntidad in listado)
            {
                ConfiguracionEnvioMailingDetalle objetoBO = _mapper.Map<ConfiguracionEnvioMailingDetalle>(listado);
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ConfiguracionEnvioMailingDetalle FirstById(int id)
        {
            try
            {
                TConfiguracionEnvioMailingDetalle entidad = base.FirstById(id);
                ConfiguracionEnvioMailingDetalle objetoBO = new ConfiguracionEnvioMailingDetalle();
                _mapper.Map<ConfiguracionEnvioMailingDetalle>(entidad);

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ConfiguracionEnvioMailingDetalle FirstBy(Expression<Func<TConfiguracionEnvioMailingDetalle, bool>> filter)
        {
            try
            {
                TConfiguracionEnvioMailingDetalle entidad = base.FirstBy(filter);
                ConfiguracionEnvioMailingDetalle objetoBO = _mapper.Map<ConfiguracionEnvioMailingDetalle>(entidad); ;

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ConfiguracionEnvioMailingDetalle objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TConfiguracionEnvioMailingDetalle entidad = MapeoEntidad(objetoBO);

                bool resultado = base.Insert(entidad);
                if (resultado)
                    AsignacionId(entidad, objetoBO);

                return resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(IEnumerable<ConfiguracionEnvioMailingDetalle> listadoBO)
        {
            try
            {
                foreach (var objetoBO in listadoBO)
                {
                    bool resultado = Insert(objetoBO);
                    if (resultado == false)
                        return false;
                }

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Update(ConfiguracionEnvioMailingDetalle objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TConfiguracionEnvioMailingDetalle entidad = MapeoEntidad(objetoBO);

                bool resultado = base.Update(entidad);
                if (resultado)
                    AsignacionId(entidad, objetoBO);

                return resultado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Update(IEnumerable<ConfiguracionEnvioMailingDetalle> listadoBO)
        {
            try
            {
                foreach (var objetoBO in listadoBO)
                {
                    bool resultado = Update(objetoBO);
                    if (resultado == false)
                        return false;
                }

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void AsignacionId(TConfiguracionEnvioMailingDetalle entidad, ConfiguracionEnvioMailingDetalle objetoBO)
        {
            try
            {
                if (entidad != null && objetoBO != null)
                {
                    objetoBO.Id = entidad.Id;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public TConfiguracionEnvioMailingDetalle MapeoEntidad(ConfiguracionEnvioMailingDetalle objetoBO)
        {
            try
            {
                //crea la entidad padre
                TConfiguracionEnvioMailingDetalle entidad = new TConfiguracionEnvioMailingDetalle();
                entidad = _mapper.Map<TConfiguracionEnvioMailingDetalle>(objetoBO);

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<ConfiguracionEnvioMailingDetalle> GetFiltered<KProperty>(IEnumerable<Expression<Func<TConfiguracionEnvioMailingDetalle, bool>>> filters, Expression<Func<TConfiguracionEnvioMailingDetalle, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TConfiguracionEnvioMailingDetalle> listado = base.GetFiltered(filters, orderBy, ascending);
            List<ConfiguracionEnvioMailingDetalle> listadoBO = new List<ConfiguracionEnvioMailingDetalle>();

            foreach (var itemEntidad in listado)
            {
                ConfiguracionEnvioMailingDetalle objetoBO = _mapper.Map<ConfiguracionEnvioMailingDetalle>(itemEntidad);
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion

        public List<ListaAlumnoMailingDTO> ObtenerRegistrosParaEnvioPersonalizado(int IdMatriculaCabecera)
        {
            try
            {
                List<ListaAlumnoMailingDTO> listaAlumnos = new List<ListaAlumnoMailingDTO>();
                string _query = "SP_ObtenerAlumnosParaEnvioPersonalizado";
                string query = _dapperRepository.QuerySPDapper(_query, new { IdMatriculaCabecera });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    listaAlumnos = JsonConvert.DeserializeObject<List<ListaAlumnoMailingDTO>>(query);
                }
                return listaAlumnos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public string ObtenerContenidoPlantilla(int IdPlantilla)
        {
            try
            {
                StringDTO plantilla = new StringDTO();
                string _query = "Select contenido as Valor From mkt.V_ObtenerContenidoPlantilla where IdPlantilla = @IdPlantilla";
                string query = _dapperRepository.QuerySPDapper(_query, new { IdPlantilla });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    plantilla = JsonConvert.DeserializeObject<StringDTO>(query);
                    return plantilla.Valor;
                }
                return "";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// Inserta un nuevo registro en la tabla mkt.T_ConfiguracionEnvioMailingDetalle
        /// </summary>
        /// <param name="listaConfiguracionEnvioMailingDetalle">Lista de objeto de tipo ConfiguracionEnvioMailingDetalle</param>
        /// <returns>Retorna booleano dependiendo del resultado final de la insercion</returns>
        public bool InsertarConfiguracionEnvioMailingDetalle(List<ConfiguracionEnvioMailingDetalle> listaConfiguracionEnvioMailingDetalle)
        {
            try
            {
                string spQuery = "[mkt].[SP_InsertarConfiguracionEnvioMailingDetalle]";

                foreach (var filtro in listaConfiguracionEnvioMailingDetalle)
                {
                    try
                    {
                        var query = _dapperRepository.QuerySPFirstOrDefault(spQuery, new
                        {
                            filtro.Asunto,
                            filtro.CuerpoHtml,
                            filtro.EnviadoCorrectamente,
                            filtro.IdConfiguracionEnvioMailing,
                            filtro.IdConjuntoListaResultado,
                            filtro.UsuarioCreacion,
                            filtro.UsuarioModificacion,
                            filtro.MensajeError,
                            filtro.IdMandrilEnvioCorreo
                        });
                    }
                    catch (Exception e)
                    {
                        continue;
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Verifica existencia de mkt.T_ConfiguracionEnvioMailingDetalle
        /// </summary>
        /// <param name="idConfiguracionEnvioMailingDetalle">Id de ConfiguracionEnvioMailingDetalle (PK de la tabla mkt.T_ConfiguracionEnvioMailingDetalle)</param>
        /// <returns>Booleano para determinar si existe o no una configuracion detalle para envio de mailing</returns>
        public bool ExisteConfiguracionEnvioMailingDetalle(int idConfiguracionEnvioMailingDetalle)
        {
            try
            {
                string spQuery = "[mkt].[SP_ExisteConfiguracionEnvioMailingDetalle]";

                var query = _dapperRepository.QuerySPFirstOrDefault(spQuery, new
                {
                    Id = idConfiguracionEnvioMailingDetalle
                });

                return !string.IsNullOrEmpty(query) && !query.Contains("[]");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Busca un registro por el Id en mkt.T_ConfiguracionEnvioMailingDetalle
        /// </summary>
        /// <param name="idConfiguracionEnvioMailingDetalle">Id de ConfiguracionEnvioMailingDetalle (PK de la tabla mkt.T_ConfiguracionEnvioMailingDetalle)</param>
        /// <returns>Registro con todos los campos en la tabla mkt.T_ConfiguracionEnvioMailingDetalle</returns>
        public ConfiguracionEnvioMailingDetalle BuscaConfiguracionEnvioMailingDetallePorId(int idConfiguracionEnvioMailingDetalle)
        {
            try
            {
                var configuracionEnvioMailingDetalle = new ConfiguracionEnvioMailingDetalle();

                string spQuery = "[mkt].[SP_BuscaConfiguracionEnvioMailingDetallePorId]";

                var query = _dapperRepository.QuerySPFirstOrDefault(spQuery, new
                {
                    Id = idConfiguracionEnvioMailingDetalle
                });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    configuracionEnvioMailingDetalle = JsonConvert.DeserializeObject<ConfiguracionEnvioMailingDetalle>(query);
                }

                return configuracionEnvioMailingDetalle;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Busca un registro por el IdConfiguracionEnvioMailing y el flag de EnviadoCorrectamente en mkt.T_ConfiguracionEnvioMailingDetalle
        /// </summary>
        /// <param name="idConfiguracionEnvioMailing">Id de la configuracion de envio mailing</param>
        /// <param name="enviadoCorrectamente">Flag para evaluar si se envio correctamente el mailing</param>
        /// <returns>Registro con todos los campos en la tabla mkt.T_ConfiguracionEnvioMailingDetalle</returns>
        public List<ConfiguracionEnvioMailingDetalle> BuscaConfiguracionEnvioMailingDetallePorIdConfiguracionEnvioMailing(int idConfiguracionEnvioMailing, bool enviadoCorrectamente)
        {
            try
            {
                var configuracionEnvioMailingDetalle = new List<ConfiguracionEnvioMailingDetalle>();

                string spQuery = "[mkt].[SP_BuscaConfiguracionEnvioMailingDetallePorIdConfiguracionEnvioMailing]";

                var query = _dapperRepository.QuerySPDapper(spQuery, new
                {
                    IdConfiguracionEnvioMailing = idConfiguracionEnvioMailing,
                    EnviadoCorrectamente = enviadoCorrectamente
                });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    configuracionEnvioMailingDetalle = JsonConvert.DeserializeObject<List<ConfiguracionEnvioMailingDetalle>>(query);
                }

                return configuracionEnvioMailingDetalle;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Actualiza un registro en la tabla mkt.T_ConfiguracionEnvioMailingDetalle
        /// </summary>
        /// <param name="filtro">Objeto de tipo ConfiguracionEnvioMailingDetalle</param>
        /// <returns>Retorna booleano dependiendo del resultado final de la actualizacion</returns>
        public bool ActualizarConfiguracionEnvioMailingDetalle(ConfiguracionEnvioMailingDetalle filtro)
        {
            try
            {
                string spQuery = "[mkt].[SP_ActualizarConfiguracionEnvioMailingDetalle]";

                var query = _dapperRepository.QuerySPFirstOrDefault(spQuery, new
                {
                    filtro.EnviadoCorrectamente,
                    filtro.MensajeError,
                    filtro.IdMandrilEnvioCorreo,
                    filtro.UsuarioModificacion,
                    filtro.FechaModificacion,
                    filtro.Id
                });

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


    }
}
