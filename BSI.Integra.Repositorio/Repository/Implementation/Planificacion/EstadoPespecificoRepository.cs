using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: EstadoPespecificoRepository
    /// Autor: Flavio R. Mamani Fabian
    /// Fecha: 03/05/2023
    /// <summary>
    /// Gestión general de T_EstadoPespecifico
    /// </summary>
    public class EstadoPespecificoRepository : GenericRepository<TEstadoPespecifico>, IEstadoPespecificoRepository
    {
        private Mapper _mapper;
        public EstadoPespecificoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TEstadoPespecifico, EstadoPespecifico>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TEstadoPespecifico MapeoEntidad(EstadoPespecifico entidad)
        {
            try
            {
                TEstadoPespecifico modelo = _mapper.Map<TEstadoPespecifico>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TEstadoPespecifico Add(EstadoPespecifico entidad)
        {
            try
            {
                var EstadoPespecifico = MapeoEntidad(entidad);
                base.Insert(EstadoPespecifico);
                return EstadoPespecifico;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TEstadoPespecifico Update(EstadoPespecifico entidad)
        {
            try
            {
                var EstadoPespecifico = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                EstadoPespecifico.RowVersion = entidadExistente.RowVersion;

                base.Update(EstadoPespecifico);
                return EstadoPespecifico;
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
        public IEnumerable<TEstadoPespecifico> Add(IEnumerable<EstadoPespecifico> listadoEntidad)
        {
            try
            {
                List<TEstadoPespecifico> listado = new List<TEstadoPespecifico>();
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
        public IEnumerable<TEstadoPespecifico> Update(IEnumerable<EstadoPespecifico> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TEstadoPespecifico> listado = new List<TEstadoPespecifico>();
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
        /// Obtiene todos los registros de T_EstadoPespecifico.
        /// </summary>
        /// <returns> List<EstadoPespecificoDTO> </returns>
        public IEnumerable<EstadoPespecifico> ObtenerTodo()
        {
            try
            {
                List<EstadoPespecifico> rpta = new();
                string query = @"
                    SELECT 
                        Id,
	                    Nombre,
	                    IdLocacion,
	                    IdTipoEstadoPespecifico,
	                    Capacidad,
	                    Virtual,
	                    Estado,
	                    FechaCreacion,
	                    FechaModificacion,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    RowVersion,
	                    IdMigracion
                    FROM pla.T_EstadoPespecifico
                    WHERE Estado = 1";
                string resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<EstadoPespecifico>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception("Se ha producido un error al ejecutar el método ObtenerTodo()", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 03/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_EstadoPespecifico por id
        /// </summary>
        /// <param name="id">Id de EstadoPespecifico</param>
        /// <returns> List<EstadoPespecificoDTO> </returns>
        public EstadoPespecifico ObtenerPorId(int id)
        {
            try
            {
                EstadoPespecifico rpta = new();
                string query = @"
                    SELECT
	                    Id,
	                    Nombre,
	                    Estado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    RowVersion,
	                    IdMigracion
                    FROM pla.T_EstadoPEspecifico
                    WHERE Estado = 1 AND Id=@id";
                string resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<EstadoPespecifico>(resultado)!;
                }
                return rpta;
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
        /// Obtiene todos los registros de T_EstadoPespecifico por lista de Ids.
        /// </summary>
        /// <param name="ids">Lista de idsEstadoPespecifico</param>
        /// <returns> List<EstadoPespecificoDTO> </returns>
        public List<EstadoPespecifico> ObtenerPorIds(List<int> ids)
        {
            try
            {
                List<EstadoPespecifico> rpta = new();
                string query = @"
                    SELECT 
                        Id,
	                    Nombre,
	                    IdLocacion,
	                    IdTipoEstadoPespecifico,
	                    Capacidad,
	                    Virtual,
	                    Estado,
	                    FechaCreacion,
	                    FechaModificacion,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    RowVersion,
	                    IdMigracion
                    FROM pla.T_EstadoPespecifico
                    WHERE Estado = 1 AND Id IN @ids";
                string resultado = _dapperRepository.QueryDapper(query, new { ids });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<EstadoPespecifico>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception("Se ha producido un error al ejecutar el método ObtenerPorIds()", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 03/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de TEstadoPespecifico con su Ciudad
        /// </summary>
        /// <param name="ids">Lista de idsEstadoPespecifico</param>
        /// <returns> List<EstadoPespecificoDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                string query = "SELECT Id, Nombre FROM pla.T_EstadoPEspecifico WHERE Estado = 1";
                string resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    return JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado)!;
                return new List<ComboDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerCombo(): {ex.Message}", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 03/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de TEstadoPespecifico con su Ciudad
        /// </summary>
        /// <param name="ids">Lista de idsEstadoPespecifico</param>
        /// <returns> List<EstadoPespecificoDTO> </returns>
        public async Task<IEnumerable<ComboDTO>> ObtenerComboAsync()
        {
            try
            {
                string query = "SELECT Id, Nombre FROM pla.T_EstadoPEspecifico WHERE Estado = 1";
                string resultado = await _dapperRepository.QueryDapperAsync(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    return JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado)!;
                return new List<ComboDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerComboAsync(): {ex.Message}", ex);
            }
        }
    }
}



