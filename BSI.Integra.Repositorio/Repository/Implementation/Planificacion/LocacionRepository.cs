using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: LocacionRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_Locacion
    /// </summary>
    public class LocacionRepository : GenericRepository<TLocacion>, ILocacionRepository
    {
        private Mapper _mapper;
        public LocacionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TLocacion, Locacion>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TLocacion MapeoEntidad(Locacion entidad)
        {
            try
            {
                TLocacion modelo = _mapper.Map<TLocacion>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TLocacion Add(Locacion entidad)
        {
            try
            {
                var Locacion = MapeoEntidad(entidad);
                base.Insert(Locacion);
                return Locacion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TLocacion Update(Locacion entidad)
        {
            try
            {
                var Locacion = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                Locacion.RowVersion = entidadExistente.RowVersion;

                base.Update(Locacion);
                return Locacion;
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
        public IEnumerable<TLocacion> Add(IEnumerable<Locacion> listadoEntidad)
        {
            try
            {
                List<TLocacion> listado = new List<TLocacion>();
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
        public IEnumerable<TLocacion> Update(IEnumerable<Locacion> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TLocacion> listado = new List<TLocacion>();
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
        /// Obtiene todos los registros de T_Locacion.
        /// </summary>
        /// <returns> lista de registros T_Locacion </returns>
        public IEnumerable<Locacion> ObtenerTodo()
        {
            try
            {
                List<Locacion> rpta = new();
                string query = @"
                    SELECT
	                    Id,
	                    Nombre,
	                    IdPais,
	                    IdRegion,
	                    IdCiudad,
	                    Direccion,
	                    Estado,
	                    FechaCreacion,
	                    FechaModificacion,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    RowVersion,
	                    IdMigracion
                    FROM pla.T_Locacion
                    WHERE Estado = 1";
                string resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<Locacion>>(resultado)!;
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
        /// Obtiene solo un registro de T_Locacion
        /// </summary>
        /// <param name="id">Id de Locacion</param>
        /// <returns> registro de T_Locacion </returns>
        public Locacion ObtenerPorId(int id)
        {
            try
            {
                Locacion rpta = new();
                string query = @"
                    SELECT
	                    Id,
	                    Nombre,
	                    IdPais,
	                    IdRegion,
	                    IdCiudad,
	                    Direccion,
	                    Estado,
	                    FechaCreacion,
	                    FechaModificacion,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    RowVersion,
	                    IdMigracion
                    FROM pla.T_Locacion
                    WHERE Estado = 1 AND Id=@id";
                string resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<Locacion>(resultado)!;
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
        /// Obtiene todos los registros de T_Locacion por lista de Ids.
        /// </summary>
        /// <param name="ids">Lista de idsLocacion</param>
        /// <returns> Lista de registros de T_Locacion </returns>
        public List<Locacion> ObtenerPorIds(List<int> ids)
        {
            try
            {
                List<Locacion> rpta = new();
                string query = @"
                    SELECT
	                    Id,
	                    Nombre,
	                    IdPais,
	                    IdRegion,
	                    IdCiudad,
	                    Direccion,
	                    Estado,
	                    FechaCreacion,
	                    FechaModificacion,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    RowVersion,
	                    IdMigracion
                    FROM pla.T_Locacion
                    WHERE Estado = 1 AND Id IN @ids";
                string resultado = _dapperRepository.QueryDapper(query, new { ids });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<Locacion>>(resultado)!;
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
        /// Obtiene todos los registros de locaion para filtro
        /// </summary>
        /// <returns> Lista de LocacionComboDTO para filtros </returns>
        public IEnumerable<LocacionComboDTO> ObtenerLocacionParaFiltro()
        {
            try
            {
                string query = "SELECT Id, Nombre, IdCiudad FROM pla.V_TLocacion_ParaFiltro WHERE Estado = 1";
                string resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    return JsonConvert.DeserializeObject<IEnumerable<LocacionComboDTO>>(resultado)!;
                return new List<LocacionComboDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerLocacionParaFiltro(): {ex.Message}", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 03/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de locaion para filtro
        /// </summary>
        /// <returns> Lista de LocacionComboDTO para filtros </returns>
        public async Task<IEnumerable<LocacionComboDTO>> ObtenerLocacionParaFiltroAsync()
        {
            try
            {
                string query = "SELECT Id, Nombre, IdCiudad FROM pla.V_TLocacion_ParaFiltro WHERE Estado = 1";
                string resultado = await _dapperRepository.QueryDapperAsync(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    return JsonConvert.DeserializeObject<IEnumerable<LocacionComboDTO>>(resultado)!;
                return new List<LocacionComboDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerLocacionParaFiltroAsync(): {ex.Message}", ex);
            }
        }
    }
}



