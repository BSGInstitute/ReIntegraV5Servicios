using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: AmbienteRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_Ambiente
    /// </summary>
    public class AmbienteRepository : GenericRepository<TAmbiente>, IAmbienteRepository
    {
        private Mapper _mapper;
        public AmbienteRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TAmbiente, Ambiente>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TAmbiente MapeoEntidad(Ambiente entidad)
        {
            try
            {
                TAmbiente modelo = _mapper.Map<TAmbiente>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TAmbiente Add(Ambiente entidad)
        {
            try
            {
                var Ambiente = MapeoEntidad(entidad);
                base.Insert(Ambiente);
                return Ambiente;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TAmbiente Update(Ambiente entidad)
        {
            try
            {
                var Ambiente = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                Ambiente.RowVersion = entidadExistente.RowVersion;

                base.Update(Ambiente);
                return Ambiente;
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
        public IEnumerable<TAmbiente> Add(IEnumerable<Ambiente> listadoEntidad)
        {
            try
            {
                List<TAmbiente> listado = new List<TAmbiente>();
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
        public IEnumerable<TAmbiente> Update(IEnumerable<Ambiente> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TAmbiente> listado = new List<TAmbiente>();
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
        /// Obtiene todos los registros de T_Ambiente.
        /// </summary>
        /// <returns> List<AmbienteDTO> </returns>
        public IEnumerable<Ambiente> ObtenerTodo()
        {
            try
            {
                List<Ambiente> rpta = new();
                string query = @"
                    SELECT 
                        Id,
	                    Nombre,
	                    IdLocacion,
	                    IdTipoAmbiente,
	                    Capacidad,
	                    Virtual,
	                    Estado,
	                    FechaCreacion,
	                    FechaModificacion,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    RowVersion,
	                    IdMigracion
                    FROM pla.T_Ambiente
                    WHERE Estado = 1";
                string resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<Ambiente>>(resultado)!;
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
        /// Obtiene todos los registros de T_Ambiente por id
        /// </summary>
        /// <param name="id">Id de Ambiente</param>
        /// <returns> List<AmbienteDTO> </returns>
        public Ambiente? ObtenerPorId(int id)
        {
            try
            {
                Ambiente rpta = new();
                string query = @"
                    SELECT 
                        Id,
	                    Nombre,
	                    IdLocacion,
	                    IdTipoAmbiente,
	                    Capacidad,
	                    Virtual,
	                    Estado,
	                    FechaCreacion,
	                    FechaModificacion,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    RowVersion,
	                    IdMigracion
                    FROM pla.T_Ambiente
                    WHERE Estado = 1 AND Id=@id";
                string resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<Ambiente>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerPorId(): {ex.Message}", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 03/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_Ambiente por lista de Ids.
        /// </summary>
        /// <param name="ids">Lista de idsAmbiente</param>
        /// <returns> List<AmbienteDTO> </returns>
        public List<Ambiente> ObtenerPorIds(List<int> ids)
        {
            try
            {
                List<Ambiente> rpta = new();
                string query = @"
                    SELECT 
                        Id,
	                    Nombre,
	                    IdLocacion,
	                    IdTipoAmbiente,
	                    Capacidad,
	                    Virtual,
	                    Estado,
	                    FechaCreacion,
	                    FechaModificacion,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    RowVersion,
	                    IdMigracion
                    FROM pla.T_Ambiente
                    WHERE Estado = 1 AND Id IN @ids";
                string resultado = _dapperRepository.QueryDapper(query, new { ids });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<Ambiente>>(resultado)!;
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
        /// Obtiene de Ambiente Ciudad para filtros
        /// </summary>
        /// <returns> Lista de combo ambiente ciudad </returns>
        public IEnumerable<AmbienteCiudadFiltroDTO> ObtenerAmbienteCiudadFiltro()
        {
            try
            {
                string query = "SELECT Id, Nombre, IdLocacion, IdCiudad FROM pla.V_AmbienteCiudad_ParaFiltro WHERE Estado = 1";
                string resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    return JsonConvert.DeserializeObject<IEnumerable<AmbienteCiudadFiltroDTO>>(resultado)!;
                return new List<AmbienteCiudadFiltroDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerAmbienteCiudadFiltro(): {ex.Message}", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 03/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene de Ambiente Ciudad para filtros
        /// </summary>
        /// <returns> Lista de combo ambiente ciudad </returns>
        public async Task<IEnumerable<AmbienteCiudadFiltroDTO>> ObtenerAmbienteCiudadFiltroAsync()
        {
            try
            {
                string query = "SELECT Id, Nombre, IdLocacion, IdCiudad FROM pla.V_AmbienteCiudad_ParaFiltro WHERE Estado = 1";
                string resultado = await _dapperRepository.QueryDapperAsync(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    return JsonConvert.DeserializeObject<IEnumerable<AmbienteCiudadFiltroDTO>>(resultado)!;
                return new List<AmbienteCiudadFiltroDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerAmbienteCiudadFiltroAsync(): {ex.Message}", ex);
            }
        }

    }
}



