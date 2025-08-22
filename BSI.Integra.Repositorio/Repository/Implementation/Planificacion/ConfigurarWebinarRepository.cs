using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: ConfigurarWebinarRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_ConfigurarWebinar
    /// </summary>
    public class ConfigurarWebinarRepository : GenericRepository<TConfigurarWebinar>, IConfigurarWebinarRepository
    {
        private Mapper _mapper;
        public ConfigurarWebinarRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TConfigurarWebinar, ConfigurarWebinar>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TConfigurarWebinar MapeoEntidad(ConfigurarWebinar entidad)
        {
            try
            {
                TConfigurarWebinar modelo = _mapper.Map<TConfigurarWebinar>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TConfigurarWebinar Add(ConfigurarWebinar entidad)
        {
            try
            {
                var ConfigurarWebinar = MapeoEntidad(entidad);
                base.Insert(ConfigurarWebinar);
                return ConfigurarWebinar;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TConfigurarWebinar Update(ConfigurarWebinar entidad)
        {
            try
            {
                var ConfigurarWebinar = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ConfigurarWebinar.RowVersion = entidadExistente.RowVersion;

                base.Update(ConfigurarWebinar);
                return ConfigurarWebinar;
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
        public IEnumerable<TConfigurarWebinar> Add(IEnumerable<ConfigurarWebinar> listadoEntidad)
        {
            try
            {
                List<TConfigurarWebinar> listado = new List<TConfigurarWebinar>();
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
        public IEnumerable<TConfigurarWebinar> Update(IEnumerable<ConfigurarWebinar> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TConfigurarWebinar> listado = new List<TConfigurarWebinar>();
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

        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 03/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene solo un registro de T_ConfigurarWebinar
        /// </summary>
        /// <param name="id">Id de ConfigurarWebinar</param>
        /// <returns> registro de T_ConfigurarWebinar </returns>
        public ConfigurarWebinar? ObtenerPorId(int id)
        {
            try
            {
                string query = @"
                    SELECT 
                        Id,
                        IdPEspecifico AS IdPespecifico,
                        Modalidad,
                        Codigo,
                        IdOperadorComparacion_Avance AS IdOperadorComparacionAvance,
                        ValorAvance,
                        ValorAVanceOpc,
                        IdOperadorComparacion_Promedio AS IdOperadorComparacionPromedio,
                        ValorPromedio,
                        ValorPromedioOpc,
                        Estado,
                        UsuarioCreacion,
                        UsuarioModificacion,
                        FechaCreacion,
                        FechaModificacion,
                        RowVersion,
                        IdMigracion,
                        IdPEspecifico_Padre AS IdPespecificoPadre
                    FROM pla.T_ConfigurarWebinar
                    WHERE Estado = 1 AND Id = @Id";
                string resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<ConfigurarWebinar>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Se ha producido un error al ejecutar el método ObtenerPorId()", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 03/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene solo un registro de T_ConfigurarWebinar
        /// </summary>
        /// <param name="id">Id de ConfigurarWebinar</param>
        /// <returns> registro de T_ConfigurarWebinar </returns>
        public IEnumerable<ConfigurarWebinar> ObtenerPorIds(IEnumerable<int> ids)
        {
            try
            {
                string query = @"
                    SELECT 
                        Id,
                        IdPEspecifico AS IdPespecifico,
                        Modalidad,
                        Codigo,
                        IdOperadorComparacion_Avance AS IdOperadorComparacionAvance,
                        ValorAvance,
                        ValorAVanceOpc,
                        IdOperadorComparacion_Promedio AS IdOperadorComparacionPromedio,
                        ValorPromedio,
                        ValorPromedioOpc,
                        Estado,
                        UsuarioCreacion,
                        UsuarioModificacion,
                        FechaCreacion,
                        FechaModificacion,
                        RowVersion,
                        IdMigracion,
                        IdPEspecifico_Padre AS IdPespecificoPadre 
                    FROM pla.T_ConfigurarWebinar
                    WHERE Estado = 1 AND Id IN @ids";
                string resultado = _dapperRepository.QueryDapper(query, new { ids });
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    return JsonConvert.DeserializeObject<IEnumerable<ConfigurarWebinar>>(resultado)!;
                }
                return new List<ConfigurarWebinar>();
            }
            catch (Exception ex)
            {
                throw new Exception("Se ha producido un error al ejecutar el método ObtenerPorIds()", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 31/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene la configuracion webinar por id pespecifico padre
        /// </summary>
        /// <param name="idPEspecificoPadre">Id Pespecifico padre</param>
        /// <returns> Lista de ConfigurarWebinarDTO </returns>
        public IEnumerable<ConfigurarWebinarDTO> ObtenerPorIdPespecificoPadre(int idPEspecificoPadre)
        {
            try
            {
                var query = @"SELECT
	                    Id,
	                    IdPEspecifico AS IdPespecifico,
	                    Modalidad,
	                    Codigo,
	                    IdOperadorComparacionAvance,
	                    ValorAvance,
	                    ValorAVanceOpc,
	                    IdOperadorComparacionPromedio,
	                    ValorPromedio,
	                    ValorPromedioOpc,
	                    UsuarioModificacion,
	                    IdPEspecificoPadre AS IdPespecificoPadre
                    FROM pla.V_TObtenerWebinar
                    WHERE
	                    Estado = 1
	                    AND IdPEspecificoPadre = @idPEspecificoPadre ORDER BY Id;";
                var resultado = _dapperRepository.QueryDapper(query, new { idPEspecificoPadre });
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    return JsonConvert.DeserializeObject<IEnumerable<ConfigurarWebinarDTO>>(resultado)!;
                }
                return new List<ConfigurarWebinarDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerPorIdPespecificoPadre(): {ex.Message}", ex);
            }
        }
    }
}



