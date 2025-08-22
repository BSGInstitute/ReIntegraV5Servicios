using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: CategoriaCiudadRepository
    /// Autor: Flavio R. Mamani Fabian
    /// Fecha: 30/05/2023
    /// <summary>
    /// Gestión general de T_CategoriaCiudad
    /// </summary>
    public class CategoriaCiudadRepository : GenericRepository<TCategoriaCiudad>, ICategoriaCiudadRepository
    {
        private Mapper _mapper;
        public CategoriaCiudadRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCategoriaCiudad, CategoriaCiudad>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TCategoriaCiudad MapeoEntidad(CategoriaCiudad entidad)
        {
            try
            {
                TCategoriaCiudad modelo = _mapper.Map<TCategoriaCiudad>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TCategoriaCiudad Add(CategoriaCiudad entidad)
        {
            try
            {
                var CategoriaCiudad = MapeoEntidad(entidad);
                base.Insert(CategoriaCiudad);
                return CategoriaCiudad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TCategoriaCiudad Update(CategoriaCiudad entidad)
        {
            try
            {
                var CategoriaCiudad = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                CategoriaCiudad.RowVersion = entidadExistente.RowVersion;

                base.Update(CategoriaCiudad);
                return CategoriaCiudad;
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
        public IEnumerable<TCategoriaCiudad> Add(IEnumerable<CategoriaCiudad> listadoEntidad)
        {
            try
            {
                List<TCategoriaCiudad> listado = new List<TCategoriaCiudad>();
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
        public IEnumerable<TCategoriaCiudad> Update(IEnumerable<CategoriaCiudad> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TCategoriaCiudad> listado = new List<TCategoriaCiudad>();
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
        /// Fecha: 30/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene registro de T_CategoriaCiudad por id
        /// </summary>
        /// <returns> List<CategoriaCiudadDTO> </returns>
        public CategoriaCiudad ObtenerPorId(int id)
        {
            try
            {
                CategoriaCiudad rpta = new();
                var query = @"
                    SELECT
	                    Id,
	                    IdCategoriaPrograma,
	                    IdCiudad,
	                    TroncalCompleto,
	                    IdRegionCiudad,
	                    Estado,
	                    FechaCreacion,
	                    FechaModificacion,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    RowVersion,
	                    IdMigracion
                    FROM pla.T_CategoriaCiudad
                        WHERE Estado = 1 AND Id=@id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<CategoriaCiudad>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerPorId: {ex.Message}", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 30/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los registros de T_CategoriaCiudad por ids
        /// </summary>
        /// <returns> List<CategoriaCiudadDTO> </returns>
        public IEnumerable<CategoriaCiudad> ObtenerPorIds(List<int> id)
        {
            try
            {
                var query = @"
                     SELECT
	                    Id,
	                    IdCategoriaPrograma,
	                    IdCiudad,
	                    TroncalCompleto,
	                    IdRegionCiudad,
	                    Estado,
	                    FechaCreacion,
	                    FechaModificacion,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    RowVersion,
	                    IdMigracion
                    FROM pla.T_CategoriaCiudad
                    WHERE Estado = 1 AND Id IN @id";
                var resultado = _dapperRepository.QueryDapper(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<CategoriaCiudad>>(resultado)!;
                }
                return new List<CategoriaCiudad>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerPorIds: {ex.Message}", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 30/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los registros de T_CategoriaCiudad por ids
        /// </summary>
        /// <returns> List<CategoriaCiudadDTO> </returns>
        public string? ObtenerTroncalPorIdCiudadIdCategoria(int idCiudad, int idCategoriaPrograma)
        {
            try
            {
                var query = @"
                     SELECT TOP 1
	                    TroncalCompleto as Valor
                    FROM pla.T_CategoriaCiudad
                    WHERE Estado = 1 AND IdCiudad=@idCiudad AND IdCategoriaPrograma=@idCategoriaPrograma";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idCiudad, idCategoriaPrograma });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    var rpta = JsonConvert.DeserializeObject<StringDTO>(resultado)!;
                    return rpta.Valor;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerTroncalPorIdCiudadIdCategoria: {ex.Message}", ex);
            }
        }
        /// Autor: Christian A. Quispe Mamani
        /// Fecha: 01/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los registros de V_Troncal
        /// </summary>
        /// <returns> List<CategoriaCiudadDTO> </returns>
        public IEnumerable<TroncalDTO> ObtenerTroncales()
        {
            try
            {
                IEnumerable<TroncalDTO> rpta = new List<TroncalDTO>();
                string query = "SELECT Id, IdCategoriaPrograma, IdRegionCiudad, TroncalCompleto, NombreRegionCiudad, NombreCategoriaPrograma FROM pla.V_Troncal where Estado=1 ORDER BY Id DESC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    rpta = JsonConvert.DeserializeObject<List<TroncalDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Christian A. Quispe Mamani
        /// Fecha: 01/06/2023
        /// Version: 1.0
        /// <summary>
        /// Inserta una nueva troncal
        /// </summary>
        /// <returns> TroncalEntidadDTO </returns>
        public TroncalEntidadDTO ValidarPorCiudadCategoria(int idCategoriaPrograma, int idRegionCiudad)
        {
            TroncalEntidadDTO rpta = new TroncalEntidadDTO();
            var query = "SELECT Id,IdRegionCiudad,IdCategoriaPrograma FROM pla.V_Troncal WHERE IdCategoriaPrograma = @idCategoriaPrograma AND IdRegionCiudad = @idRegionCiudad";
            var resultado = _dapperRepository.FirstOrDefault(query, new { idCategoriaPrograma, idRegionCiudad });
            if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
            {
                rpta = JsonConvert.DeserializeObject<TroncalEntidadDTO>(resultado)!;
            }
            return rpta;
        }
        /// Autor: Christian A. Quispe Mamani
        /// Fecha: 01/06/2023
        /// Version: 1.0
        /// <summary>
        /// Inserta una nueva troncal
        /// </summary>
        /// <returns> TroncalEntidadDTO </returns>
        public TroncalEntidadDTO ValidarTroncal(string troncalCompleto)
        {
            TroncalEntidadDTO rpta = new TroncalEntidadDTO();
            var query = "SELECT Id,TroncalCompleto FROM pla.V_Troncal WHERE  TroncalCompleto = @troncalCompleto";
            var resultado = _dapperRepository.FirstOrDefault(query, new { troncalCompleto });
            if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
            {
                rpta = JsonConvert.DeserializeObject<TroncalEntidadDTO>(resultado)!;
            }
            return rpta;
        }
    }
}



