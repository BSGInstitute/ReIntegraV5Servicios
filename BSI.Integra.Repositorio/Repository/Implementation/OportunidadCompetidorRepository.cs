using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: OportunidadCompetidorRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 26/07/2022
    /// <summary>
    /// Gestión general de T_OportunidadCompetidor
    /// </summary>
    public class OportunidadCompetidorRepository : GenericRepository<TOportunidadCompetidor>, IOportunidadCompetidorRepository
    {
        private Mapper _mapper;

        public OportunidadCompetidorRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TOportunidadCompetidor, OportunidadCompetidor>(MemberList.None).ReverseMap();
                cfg.CreateMap<TOportunidadPrerequisitoGeneral, OportunidadPrerequisitoGeneral>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TOportunidadCompetidor MapeoEntidad(OportunidadCompetidor entidad)
        {
            try
            {
                //crea la entidad padre
                TOportunidadCompetidor modelo = _mapper.Map<TOportunidadCompetidor>(entidad);

                //mapea los hijos
                if (entidad.OportunidadPrerequisitoGenerals != null && entidad.OportunidadPrerequisitoGenerals.Count > 0)
                {
                    var listadoHijoNivel1 = _mapper.Map<List<TOportunidadPrerequisitoGeneral>>(entidad.OportunidadPrerequisitoGenerals);
                    foreach (var hijoNivel1 in listadoHijoNivel1)
                    {
                        modelo.TOportunidadPrerequisitoGenerals.Add(hijoNivel1);
                    }
                }

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TOportunidadCompetidor Add(OportunidadCompetidor entidad)
        {
            try
            {
                var OportunidadCompetidor = MapeoEntidad(entidad);
                base.Insert(OportunidadCompetidor);
                return OportunidadCompetidor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TOportunidadCompetidor Update(OportunidadCompetidor entidad)
        {
            try
            {
                var OportunidadCompetidor = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                OportunidadCompetidor.RowVersion = entidadExistente.RowVersion;

                base.Update(OportunidadCompetidor);
                return OportunidadCompetidor;
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


        public IEnumerable<TOportunidadCompetidor> Add(IEnumerable<OportunidadCompetidor> listadoEntidad)
        {
            try
            {
                List<TOportunidadCompetidor> listado = new List<TOportunidadCompetidor>();
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

        public IEnumerable<TOportunidadCompetidor> Update(IEnumerable<OportunidadCompetidor> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TOportunidadCompetidor> listado = new List<TOportunidadCompetidor>();
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
        /// Fecha: 26/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_OportunidadCompetidor.
        /// </summary>
        /// <returns> List<OportunidadCompetidorDTO> </returns>
        public IEnumerable<OportunidadCompetidorDTO> ObtenerOportunidadCompetidor()
        {
            try
            {
                List<OportunidadCompetidorDTO> rpta = new List<OportunidadCompetidorDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    IdOportunidad,
	                    OtroBeneficio,
	                    Respuesta,
	                    Completado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion
                    FROM com.T_OportunidadCompetidor
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<OportunidadCompetidorDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 26/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_OportunidadCompetidor para mostrarse en combo.
        /// </summary>
        /// <returns> List<OportunidadCompetidorComboDTO> </returns>
        public IEnumerable<OportunidadCompetidorComboDTO> ObtenerCombo()
        {
            try
            {
                List<OportunidadCompetidorComboDTO> rpta = new List<OportunidadCompetidorComboDTO>();
                var query = @"SELECT Id,IdOportunidad,OtroBeneficio FROM com.T_OportunidadCompetidor WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<OportunidadCompetidorComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 26/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_OportunidadCompetidor asociados a una Oportunidad.
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> List<OportunidadCompetidorAgendaDTO> </returns>
        public IEnumerable<OportunidadCompetidorAgendaDTO> ObtenerOportunidadCompetidorPorIdOportunidad(int idOportunidad)
        {
            try
            {
                List<OportunidadCompetidorAgendaDTO> oportunidadCompetidores = new List<OportunidadCompetidorAgendaDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    IdOportunidad,
	                    OtroBeneficio,
	                    Respuesta,
	                    Completado
                    FROM com.T_OportunidadCompetidor
                    WHERE Estado = 1 AND IdOportunidad = @idOportunidad";
                var resultado = _dapperRepository.QueryDapper(query, new { idOportunidad });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    oportunidadCompetidores = JsonConvert.DeserializeObject<List<OportunidadCompetidorAgendaDTO>>(resultado);
                }
                return oportunidadCompetidores;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 16/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro de T_OportunidadCompetidor asociado a un Id.
        /// </summary>
        /// <param name="idOportunidadCompetidor">Id de OportunidadCompetidor</param>
        /// <returns> OportunidadCompetidorDTO </returns>
        public OportunidadCompetidor ObtenerOportunidadCompetidorPorId(int idOportunidadCompetidor)
        {
            try
            {
                OportunidadCompetidor rpta = new OportunidadCompetidor();
                var query = @"
                    SELECT
	                    Id,
	                    IdOportunidad,
	                    OtroBeneficio,
	                    Respuesta,
	                    Completado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion
                    FROM com.T_OportunidadCompetidor
                    WHERE Estado = 1 AND Id = @idOportunidadCompetidor";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idOportunidadCompetidor });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<OportunidadCompetidor>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public OportunidadCompetidor ObtenerPorId(int idOportunidadCompetidor)
        {
            try
            {
                OportunidadCompetidor rpta = new OportunidadCompetidor();
                var query = @"
                            SELECT 
                                     Id,
                                        IdOportunidad,
                                        OtroBeneficio,
                                        Respuesta,
                                        Completado,
                                        Estado,
                                        UsuarioCreacion,
                                        UsuarioModificacion,
                                        FechaCreacion,
                                        FechaModificacion,
                                        RowVersion,
                                        IdMigracion
                            FROM com.T_OportunidadCompetidor
                            WHERE Estado = 1 AND Id = @Id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = idOportunidadCompetidor });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<OportunidadCompetidor>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 16/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro de T_OportunidadCompetidor asociado a un Id.
        /// </summary>
        /// <param name="idOportunidadCompetidor">Id de OportunidadCompetidor</param>
        /// <returns> OportunidadCompetidorDTO </returns>
        public async Task<OportunidadCompetidor> ObtenerPorIdAsync(int idOportunidadCompetidor)
        {
            try
            {
                OportunidadCompetidor rpta = new OportunidadCompetidor();
                var query = @"
                            SELECT 
                                     Id,
                                        IdOportunidad,
                                        OtroBeneficio,
                                        Respuesta,
                                        Completado,
                                        Estado,
                                        UsuarioCreacion,
                                        UsuarioModificacion,
                                        FechaCreacion,
                                        FechaModificacion,
                                        RowVersion,
                                        IdMigracion
                            FROM com.T_OportunidadCompetidor
                            WHERE Estado = 1 AND Id = @Id";
                var resultado = await _dapperRepository.FirstOrDefaultAsync(query, new { Id = idOportunidadCompetidor });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<OportunidadCompetidor>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        OportunidadCompetidorDTO IOportunidadCompetidorRepository.ObtenerOportunidadCompetidorPorId(int idOportunidadCompetidor)
        {
            throw new NotImplementedException();
        }
    }
}
