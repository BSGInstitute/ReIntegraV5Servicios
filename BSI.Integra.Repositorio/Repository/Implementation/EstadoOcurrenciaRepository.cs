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
    /// Repositorio: EstadoOcurrenciaRepository
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 09/06/2022
    /// <summary>
    /// Gestión general de T_EstadoOcurrencia
    /// </summary>
    public class EstadoOcurrenciaRepository : GenericRepository<TEstadoOcurrencium>, IEstadoOcurrenciaRepository
    {
        private Mapper _mapper;

        public EstadoOcurrenciaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TEstadoOcurrencium, EstadoOcurrencia>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TEstadoOcurrencium MapeoEntidad(EstadoOcurrencia entidad)
        {
            try
            {
                TEstadoOcurrencium modelo = _mapper.Map<TEstadoOcurrencium>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TEstadoOcurrencium Add(EstadoOcurrencia entidad)
        {
            try
            {
                var EstadoOcurrencia = MapeoEntidad(entidad);
                base.Insert(EstadoOcurrencia);
                return EstadoOcurrencia;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TEstadoOcurrencium Update(EstadoOcurrencia entidad)
        {
            try
            {
                var EstadoOcurrencia = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                EstadoOcurrencia.RowVersion = entidadExistente.RowVersion;

                base.Update(EstadoOcurrencia);
                return EstadoOcurrencia;
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


        public IEnumerable<TEstadoOcurrencium> Add(IEnumerable<EstadoOcurrencia> listadoEntidad)
        {
            try
            {
                List<TEstadoOcurrencium> listado = new List<TEstadoOcurrencium>();
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

        public IEnumerable<TEstadoOcurrencium> Update(IEnumerable<EstadoOcurrencia> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TEstadoOcurrencium> listado = new List<TEstadoOcurrencium>();
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
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 09/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_EstadoOcurrencia para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                List<ComboDTO> rpta = new List<ComboDTO>();

                var query = "SELECT Id, Nombre FROM com.T_EstadoOcurrencia WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 06/10/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_EstadoOcurrencia para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public async Task<IEnumerable<ComboDTO>> ObtenerComboAsync()
        {
            try
            {
                List<ComboDTO> rpta = new List<ComboDTO>();
                var query = "SELECT Id, Nombre FROM com.T_EstadoOcurrencia WHERE Estado = 1";
                var resultado = await _dapperRepository.QueryDapperAsync(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ComboDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 09/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_EstadoOcurrencia
        /// </summary>
        /// <returns> List<EstadoOcurrenciaDTO> </returns>
        public IEnumerable<EstadoOcurrenciaDTO> ObtenerEstadoOcurrencia()
        {
            try
            {
                List<EstadoOcurrenciaDTO> rpta = new List<EstadoOcurrenciaDTO>();
                var query = @"SELECT Id, Nombre, Descripcion, Estado, UsuarioCreacion, UsuarioModificacion, FechaCreacion, FechaModificacion 
                            FROM com.T_EstadoOcurrencia 
                            WHERE Estado=1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<EstadoOcurrenciaDTO>>(resultado);
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
