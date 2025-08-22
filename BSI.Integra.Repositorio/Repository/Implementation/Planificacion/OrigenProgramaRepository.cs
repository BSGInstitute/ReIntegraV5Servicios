using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: OrigenProgramaRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_OrigenPrograma
    /// </summary>
    public class OrigenProgramaRepository : GenericRepository<TOrigenPrograma>, IOrigenProgramaRepository
    {
        private Mapper _mapper;
        public OrigenProgramaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TOrigenPrograma, OrigenPrograma>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TOrigenPrograma MapeoEntidad(OrigenPrograma entidad)
        {
            try
            {
                TOrigenPrograma modelo = _mapper.Map<TOrigenPrograma>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TOrigenPrograma Add(OrigenPrograma entidad)
        {
            try
            {
                var OrigenPrograma = MapeoEntidad(entidad);
                base.Insert(OrigenPrograma);
                return OrigenPrograma;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TOrigenPrograma Update(OrigenPrograma entidad)
        {
            try
            {
                var OrigenPrograma = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                OrigenPrograma.RowVersion = entidadExistente.RowVersion;

                base.Update(OrigenPrograma);
                return OrigenPrograma;
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
        public IEnumerable<TOrigenPrograma> Add(IEnumerable<OrigenPrograma> listadoEntidad)
        {
            try
            {
                List<TOrigenPrograma> listado = new List<TOrigenPrograma>();
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
        public IEnumerable<TOrigenPrograma> Update(IEnumerable<OrigenPrograma> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TOrigenPrograma> listado = new List<TOrigenPrograma>();
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
        /// Obtiene todos los registros de T_OrigenPrograma.
        /// </summary>
        /// <returns> List<OrigenProgramaDTO> </returns>
        public IEnumerable<OrigenPrograma> ObtenerTodo()
        {
            try
            {
                List<OrigenPrograma> rpta = new();
                string query = @"
                    SELECT
	                    Id,
	                    Descripcion,
	                    Estado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    RowVersion,
	                    IdMigracion
                    FROM pla.T_OrigenPrograma
                    WHERE Estado = 1";
                string resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<OrigenPrograma>>(resultado)!;
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
        /// Obtiene todos los registros de T_OrigenPrograma por id
        /// </summary>
        /// <param name="id">Id de OrigenPrograma</param>
        /// <returns> List<OrigenProgramaDTO> </returns>
        public OrigenPrograma ObtenerPorId(int id)
        {
            try
            {
                OrigenPrograma rpta = new();
                string query = @"
                    SELECT
	                    Id,
	                    Descripcion,
	                    Estado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    RowVersion,
	                    IdMigracion
                    FROM pla.T_OrigenPrograma
                    WHERE Estado = 1 AND Id=@id";
                string resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<OrigenPrograma>(resultado)!;
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
        /// Obtiene todos los registros de T_OrigenPrograma por lista de Ids.
        /// </summary>
        /// <param name="ids">Lista de idsOrigenPrograma</param>
        /// <returns> List<OrigenProgramaDTO> </returns>
        public List<OrigenPrograma> ObtenerPorIds(List<int> ids)
        {
            try
            {
                List<OrigenPrograma> rpta = new();
                string query = @"
                    SELECT
	                    Id,
	                    Descripcion,
	                    Estado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    RowVersion,
	                    IdMigracion
                    FROM pla.T_OrigenPrograma
                    WHERE Estado = 1 AND Id IN @ids";
                string resultado = _dapperRepository.QueryDapper(query, new { ids });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<OrigenPrograma>>(resultado)!;
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
        /// Obtiene los Origen Programa para filtros
        /// </summary>
        /// <param name="ids">Lista de idsOrigenPrograma</param>
        /// <returns> Lista de combos datos origen programa </returns>
        public IEnumerable<ComboDTO> ObtenerDatosOrigenPrograma()
        {
            try
            {
                string query = "SELECT Id, Nombre FROM pla.V_DatosOrigenPrograma WHERE Estado = 1";
                string resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    return JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado)!;
                return new List<ComboDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerDatosOrigenPrograma(): {ex.Message}", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 03/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los Origen Programa para filtros
        /// </summary>
        /// <param name="ids">Lista de idsOrigenPrograma</param>
        /// <returns> Lista de combos datos origen programa </returns>
        public async Task<IEnumerable<ComboDTO>> ObtenerDatosOrigenProgramaAsync()
        {
            try
            {
                string query = "SELECT Id, Nombre FROM pla.V_DatosOrigenPrograma WHERE Estado = 1";
                string resultado = await _dapperRepository.QueryDapperAsync(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    return JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado)!;
                return new List<ComboDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerDatosOrigenPrograma(): {ex.Message}", ex);
            }
        }
    }
}



