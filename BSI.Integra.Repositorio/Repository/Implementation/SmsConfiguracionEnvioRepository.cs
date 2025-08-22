using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: SmsConfiguracionEnvioRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 18/08/2022
    /// <summary>
    /// Gestión general de T_SmsConfiguracionEnvio
    /// </summary>
    public class SmsConfiguracionEnvioRepository : GenericRepository<TSmsConfiguracionEnvio>, ISmsConfiguracionEnvioRepository
    {
        private Mapper _mapper;

        public SmsConfiguracionEnvioRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSmsConfiguracionEnvio, SmsConfiguracionEnvio>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TSmsConfiguracionEnvio MapeoEntidad(SmsConfiguracionEnvio entidad)
        {
            try
            {
                //crea la entidad padre
                TSmsConfiguracionEnvio modelo = _mapper.Map<TSmsConfiguracionEnvio>(entidad);

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

        public TSmsConfiguracionEnvio Add(SmsConfiguracionEnvio entidad)
        {
            try
            {
                var SmsConfiguracionEnvio = MapeoEntidad(entidad);
                base.Insert(SmsConfiguracionEnvio);
                return SmsConfiguracionEnvio;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TSmsConfiguracionEnvio Update(SmsConfiguracionEnvio entidad)
        {
            try
            {
                var SmsConfiguracionEnvio = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                SmsConfiguracionEnvio.RowVersion = entidadExistente.RowVersion;

                base.Update(SmsConfiguracionEnvio);
                return SmsConfiguracionEnvio;
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


        public IEnumerable<TSmsConfiguracionEnvio> Add(IEnumerable<SmsConfiguracionEnvio> listadoEntidad)
        {
            try
            {
                List<TSmsConfiguracionEnvio> listado = new List<TSmsConfiguracionEnvio>();
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

        public IEnumerable<TSmsConfiguracionEnvio> Update(IEnumerable<SmsConfiguracionEnvio> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TSmsConfiguracionEnvio> listado = new List<TSmsConfiguracionEnvio>();
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
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 18/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_SmsConfiguracionEnvio.
        /// </summary>
        /// <returns> List<SmsConfiguracionEnvioDTO> </returns>
        public IEnumerable<SmsConfiguracionEnvioDTO> ObtenerSmsConfiguracionEnvio()
        {
            try
            {
                List<SmsConfiguracionEnvioDTO> rpta = new List<SmsConfiguracionEnvioDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    Nombre,
	                    Descripcion,
	                    IdPersonal,
	                    IdPlantilla,
	                    IdPGeneral,
	                    IdConjuntoListaDetalle,
	                    Activo,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion
                    FROM mkt.T_SmsConfiguracionEnvio
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<SmsConfiguracionEnvioDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 18/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la configuracion de Sms basado en la oportunidad
        /// </summary>
        /// <param name="idOportunidad">Id de la oportunidad (PK de la tabla com.T_Oportunidad)</param>
        /// <returns>Objeto de clase SmsEnvioAnexoDTO</returns>
        public SmsEnvioAnexoDTO ConfiguracionSmsOportunidad(int idOportunidad)
        {
            try
            {
                SmsEnvioAnexoDTO rpta = new SmsEnvioAnexoDTO();
                var query = @"
                    SELECT IdPersonal,IdAlumno,IdOportunidad,IdCodigoPais,Celular,Servidor,Tipo,Puerto
                    FROM mkt.V_ConfiguracionSmsOportunidad
                    WHERE IdOportunidad = @idOportunidad";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idOportunidad });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<SmsEnvioAnexoDTO>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 18/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los dias sin contacto
        /// </summary>
        /// <param name="idOportunidad">Id de la oportunidad (PK de la tabla com.T_Oportunidad)</param>
        /// <returns>Objeto de clase OportunidadDiasSinContactoDTO</returns>
        public OportunidadDiasSinContactoDTO ObtenerDiasSinContacto(int idOportunidad)
        {
            try
            {
                OportunidadDiasSinContactoDTO rpta = new OportunidadDiasSinContactoDTO()
                {
                    IdOportunidad = idOportunidad,
                    DiasSinContacto = 0
                };
                var storeProcedure = "mkt.SP_CalcularDiasSinContactoPorIdOportunidad";
                var resultado = _dapperRepository.QuerySPFirstOrDefault(storeProcedure, new { idOportunidad });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]") && !(resultado == "null"))
                {
                    rpta = JsonConvert.DeserializeObject<OportunidadDiasSinContactoDTO>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                OportunidadDiasSinContactoDTO rpta = new OportunidadDiasSinContactoDTO()
                {
                    IdOportunidad = idOportunidad,
                    DiasSinContacto = 0
                };
                return rpta;
            }
        }
    }
}
