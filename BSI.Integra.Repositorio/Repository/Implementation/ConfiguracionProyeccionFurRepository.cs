using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ConfiguracionProyeccionFurRepository
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_ConfiguracionProyeccionFur
    /// </summary>
    public class ConfiguracionProyeccionFurRepository : GenericRepository<TConfiguracionProyeccionFur>, IConfiguracionProyeccionFurRepository
    {
        private Mapper _mapper;

        public ConfiguracionProyeccionFurRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TConfiguracionProyeccionFur, ConfiguracionProyeccionFur>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TConfiguracionProyeccionFur MapeoEntidad(ConfiguracionProyeccionFur entidad)
        {
            try
            {
                //crea la entidad padre
                TConfiguracionProyeccionFur modelo = _mapper.Map<TConfiguracionProyeccionFur>(entidad);

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

        public TConfiguracionProyeccionFur Add(ConfiguracionProyeccionFur entidad)
        {
            try
            {
                var ConfiguracionProyeccionFur = MapeoEntidad(entidad);
                base.Insert(ConfiguracionProyeccionFur);
                return ConfiguracionProyeccionFur;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TConfiguracionProyeccionFur Update(ConfiguracionProyeccionFur entidad)
        {
            try
            {
                var ConfiguracionProyeccionFur = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ConfiguracionProyeccionFur.RowVersion = entidadExistente.RowVersion;

                base.Update(ConfiguracionProyeccionFur);
                return ConfiguracionProyeccionFur;
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


        public IEnumerable<TConfiguracionProyeccionFur> Add(IEnumerable<ConfiguracionProyeccionFur> listadoEntidad)
        {
            try
            {
                List<TConfiguracionProyeccionFur> listado = new List<TConfiguracionProyeccionFur>();
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

        public IEnumerable<TConfiguracionProyeccionFur> Update(IEnumerable<ConfiguracionProyeccionFur> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TConfiguracionProyeccionFur> listado = new List<TConfiguracionProyeccionFur>();
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


        /// Autor:Griselberto huamanc
        /// Fecha:08/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro por id
        /// </summary>
        /// <returns> ConfiguracionProyeccionFur </returns>
        public ConfiguracionProyeccionFur ObtenerConfiguracionProyeccionFurById(int Id)
        {
            try
            {
                ConfiguracionProyeccionFur rpta = new ConfiguracionProyeccionFur();
                var query = @"SELECT * FROM fin.T_ConfiguracionProyeccionFur
                            WHERE Estado=1 AND Id=@Id";
                var resultado = _dapperRepository.FirstOrDefault(query, new {Id= Id });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<ConfiguracionProyeccionFur>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Margiory Ramirez
        /// Fecha:08/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_ConfiguracionProyeccionFur
        /// </summary>
        /// <returns> List<TipoDatoDTO> </returns>
        public List<ConfiguracionProyeccionFur> ObtenerConfiguracionProyeccionFurActivos()
        {
            try
            {
                List<ConfiguracionProyeccionFur> rpta = new List<ConfiguracionProyeccionFur>();
                var query = @"SELECT Id, IdPeriodoProyeccion,FechaSemilla, Activo, Estado, UsuarioCreacion, UsuarioModificacion, FechaCreacion, FechaModificacion,fechaLimiteEnvio
                            FROM fin.T_ConfiguracionProyeccionFur 
                            WHERE Estado=1 AND ACtivo=1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ConfiguracionProyeccionFur>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Margiory Ramirez
        /// Fecha:08/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_ConfiguracionProyeccionFur
        /// </summary>
        /// <returns> List<TipoDatoDTO> </returns>
        public IEnumerable<ConfiguracionProyeccionFurDTO> ObtenerConfiguracionProyeccionFur()
        {
            try
            {
                List<ConfiguracionProyeccionFurDTO> rpta = new List<ConfiguracionProyeccionFurDTO>();
                var query = @"SELECT Id, IdPeriodoProyeccion,FechaSemilla, Activo,FechaLimiteEnvio
                            FROM fin.T_ConfiguracionProyeccionFur
                            WHERE Estado=1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ConfiguracionProyeccionFurDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
    }
}
