using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ConfiguracionEnvioMailingRepository
    /// Autor: Gilmer Quispe.
    /// Fecha: 11/10/2022
    /// <summary>
    /// Gestión general de T_ConfiguracionEnvioMailing
    /// </summary>
    public class ConfiguracionEnvioMailingRepository : GenericRepository<TConfiguracionEnvioMailing>, IConfiguracionEnvioMailingRepository
    {
        private Mapper _mapper;

        public ConfiguracionEnvioMailingRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TConfiguracionEnvioMailing, ConfiguracionEnvioMailing>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TConfiguracionEnvioMailing MapeoEntidad(ConfiguracionEnvioMailing entidad)
        {
            try
            {
                //crea la entidad padre
                TConfiguracionEnvioMailing modelo = _mapper.Map<TConfiguracionEnvioMailing>(entidad);

                //mapea los hijos
                //if (entidad.ListadoHijoNivel1 != null && entidad.ListadoHijoNivel1.Count > 0)
                //{
                //    var listadoHijoNivel1 = _mapper.Map<List<THijo>>(entidad.ListadoHijoNivel1);
                //    foreach (var hijoNivel1 in listadoHijoNivel1)
                //    {
                //        modelo.THijo.Add(hijoNivel1);
                //    }
                //}

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TConfiguracionEnvioMailing Add(ConfiguracionEnvioMailing entidad)
        {
            try
            {
                var ConfiguracionEnvioMailing = MapeoEntidad(entidad);
                base.Insert(ConfiguracionEnvioMailing);
                return ConfiguracionEnvioMailing;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TConfiguracionEnvioMailing Update(ConfiguracionEnvioMailing entidad)
        {
            try
            {
                var ConfiguracionEnvioMailing = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ConfiguracionEnvioMailing.RowVersion = entidadExistente.RowVersion;

                base.Update(ConfiguracionEnvioMailing);
                return ConfiguracionEnvioMailing;
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


        public IEnumerable<TConfiguracionEnvioMailing> Add(IEnumerable<ConfiguracionEnvioMailing> listadoEntidad)
        {
            try
            {
                List<TConfiguracionEnvioMailing> listado = new List<TConfiguracionEnvioMailing>();
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

        public IEnumerable<TConfiguracionEnvioMailing> Update(IEnumerable<ConfiguracionEnvioMailing> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TConfiguracionEnvioMailing> listado = new List<TConfiguracionEnvioMailing>();
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

        /// Autor: Gilmer Quispe.
        /// Fecha: 11/10/2022
        /// <summary>
        /// Obtiene los envios masivos
        /// </summary>
        /// <param name="email"> Correo. </param>
        /// <returns> Lista de objetos (CorreoDTO) </returns>
        public List<CorreoDTO> ObtenerEnviosMasivos(string email)
        {
            try
            {
                var Configuracion = new List<CorreoDTO>();
                string _queryConfiguraciones = $@" SELECT Id,Asunto,EmailBody,Fecha,Remitente,Destinatarios,Seen, 
                                                           TotalCorreos,IdPersonal,IdAlumno,ConCopia,EnvioMasivoMandrill
                                                    FROM mkt.V_ObtenerEnviosMasivos
                                                    WHERE Destinatarios = @email;";
                var queryConfiguraciones = _dapperRepository.QueryDapper(_queryConfiguraciones, new { email });
                if (queryConfiguraciones != "[]" && queryConfiguraciones != "null")
                {
                    Configuracion = JsonConvert.DeserializeObject<List<CorreoDTO>>(queryConfiguraciones);
                    return Configuracion;
                }
                return Configuracion;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 01/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los envios masivos
        /// </summary>
        /// <param name="id"> Id de correo</param>
        /// <returns>ObjetoDTO: CorreoDTO</returns>
        public CorreoDTO ObtenerEnvioMasivo(int id)
        {
            try
            {
                CorreoDTO configuracion = new CorreoDTO();
                string query = $@"SELECT 
                                    Id, Asunto, EmailBody, Fecha, Remitente, Destinatarios, Seen,
                                    TotalCorreos, IdPersonal,IdAlumno, ConCopia, EnvioMasivoMandrill
                                FROM 
                                    mkt.V_ObtenerEnviosMasivos
                                WHERE 
                                    Id = @Id;";
                var queryConfiguraciones = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(queryConfiguraciones) && queryConfiguraciones != "null")
                {
                    configuracion = JsonConvert.DeserializeObject<CorreoDTO>(queryConfiguraciones)!;
                    return configuracion;
                }
                return configuracion;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
