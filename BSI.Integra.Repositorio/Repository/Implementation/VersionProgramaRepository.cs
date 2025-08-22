using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: VersionProgramaRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 09/08/2022
    /// <summary>
    /// Gestión general de T_VersionPrograma
    /// </summary>
    public class VersionProgramaRepository : GenericRepository<TVersionPrograma>, IVersionProgramaRepository
    {
        private Mapper _mapper;

        public VersionProgramaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TVersionPrograma, VersionPrograma>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TVersionPrograma MapeoEntidad(VersionPrograma entidad)
        {
            try
            {
                //crea la entidad padre
                TVersionPrograma modelo = _mapper.Map<TVersionPrograma>(entidad);

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

        public TVersionPrograma Add(VersionPrograma entidad)
        {
            try
            {
                var VersionPrograma = MapeoEntidad(entidad);
                base.Insert(VersionPrograma);
                return VersionPrograma;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TVersionPrograma Update(VersionPrograma entidad)
        {
            try
            {
                var VersionPrograma = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                VersionPrograma.RowVersion = entidadExistente.RowVersion;

                base.Update(VersionPrograma);
                return VersionPrograma;
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


        public IEnumerable<TVersionPrograma> Add(IEnumerable<VersionPrograma> listadoEntidad)
        {
            try
            {
                List<TVersionPrograma> listado = new List<TVersionPrograma>();
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

        public IEnumerable<TVersionPrograma> Update(IEnumerable<VersionPrograma> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TVersionPrograma> listado = new List<TVersionPrograma>();
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
        /// Fecha: 09/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_VersionPrograma.
        /// </summary>
        /// <returns> List<VersionProgramaDTO> </returns>
        public IEnumerable<VersionProgramaDTO> ObtenerVersionPrograma()
        {
            try
            {
                var query = @"
                    SELECT
	                    Id,
	                    Nombre
                    FROM pla.T_VersionPrograma
                    WHERE Estado = 1 ORDER BY FechaCreacion DESC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<VersionProgramaDTO>>(resultado)!;
                }
                return new List<VersionProgramaDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#VPR-OVP-001@Error en ObtenerVersionPrograma() {ex.Message}", ex);
            }
        }

        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 09/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_VersionPrograma.
        /// </summary>
        /// <returns> List<VersionProgramaDTO> </returns>
        public async Task<IEnumerable<VersionProgramaDTO>> ObtenerVersionProgramaAsync()
        {
            try
            {
                var query = @"
                    SELECT
	                    Id,
	                    Nombre
                    FROM pla.T_VersionPrograma
                    WHERE Estado = 1 ORDER BY FechaCreacion DESC";
                var resultado = await _dapperRepository.QueryDapperAsync(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<VersionProgramaDTO>>(resultado)!;
                }
                return new List<VersionProgramaDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#VPR-OVPA-001@Error en ObtenerVersionProgramaAsync() {ex.Message}", ex);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 07/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obitene Combo id, Nombre y UsuarioModificacion total de T_VersionPrograma 
        /// </summary>
        /// <returns> List<VersionProgramaNombreUsuarioDTO> </returns>
        public List<VersionProgramaNombreUsuarioDTO> ObtenerTodo()
        {
            try
            {
                List<VersionProgramaNombreUsuarioDTO> respuesta = new List<VersionProgramaNombreUsuarioDTO>();
                var query = @"SELECT Id,Nombre, UsuarioModificacion FROM pla.T_VersionPrograma WHERE Estado = 1 ORDER BY FechaCreacion DESC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    respuesta = JsonConvert.DeserializeObject<List<VersionProgramaNombreUsuarioDTO>>(resultado)!;
                }
                return respuesta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_MaterialAccion.
        /// </summary>
        /// <returns> List<MaterialAccionDTO> </returns>
        public VersionPrograma ObtenerPorId(int id)
        {
            try
            {
                VersionPrograma rpta = new();
                var query = @"
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
                        FROM pla.T_VersionPrograma
                        WHERE Estado = 1 AND Id=@id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<VersionPrograma>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception($"#VPR-OPI-001@Error en ObtenerPorId() {ex.Message}", ex);
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_VersionPrograma.
        /// </summary>
        /// <returns> List<VersionProgramaDTO> </returns>
        public IEnumerable<VersionProgramaDTO> ObtenerVersionProgramaByBeneficios()
        {
            try
            {
                var query = @"
                    SELECT
	                    Id,
	                    Nombre
                    FROM pla.T_VersionPrograma
                    WHERE Estado = 1 and Id not in (4,6) ORDER BY FechaCreacion DESC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<VersionProgramaDTO>>(resultado)!;
                }
                return new List<VersionProgramaDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#VPR-OVP-001@Error en ObtenerVersionPrograma() {ex.Message}", ex);
            }
        }
    }
}
