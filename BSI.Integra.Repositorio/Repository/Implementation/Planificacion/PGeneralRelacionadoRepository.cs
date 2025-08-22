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
    /// Repositorio: PGeneralRelacionadoRepository
    /// Autor: Gilmer Qm
    /// Fecha: 15/06/2023
    /// <summary>
    /// Gestión general de T_PGeneralRelacionado
    /// </summary>
    public class PGeneralRelacionadoRepository : GenericRepository<TPgeneralRelacionado>, IPGeneralRelacionadoRepository
    {
        private Mapper _mapper;

        public PGeneralRelacionadoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPgeneralRelacionado, PgeneralRelacionado>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TPgeneralRelacionado MapeoEntidad(PgeneralRelacionado entidad)
        {
            try
            {
                //crea la entidad padre
                TPgeneralRelacionado pGeneralRelacionado = _mapper.Map<TPgeneralRelacionado>(entidad);

                return pGeneralRelacionado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPgeneralRelacionado Add(PgeneralRelacionado entidad)
        {
            try
            {
                var pGeneralRelacionado = MapeoEntidad(entidad);
                Insert(pGeneralRelacionado);
                return pGeneralRelacionado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPgeneralRelacionado Update(PgeneralRelacionado entidad)
        {
            try
            {
                var pGeneralRelacionado = MapeoEntidad(entidad);
                var entidadExistente = FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                pGeneralRelacionado.RowVersion = entidadExistente.RowVersion;

                Update(pGeneralRelacionado);
                return pGeneralRelacionado;
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

        public IEnumerable<TPgeneralRelacionado> Add(IEnumerable<PgeneralRelacionado> listadoEntidad)
        {
            try
            {
                List<TPgeneralRelacionado> listado = new List<TPgeneralRelacionado>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }
                Insert(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<TPgeneralRelacionado> Update(IEnumerable<PgeneralRelacionado> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPgeneralRelacionado> listado = new List<TPgeneralRelacionado>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }

                var infoExistente = GetBy(w => listadoEntidad.Select(s => s.Id).Contains(w.Id), s => new { s.Id, s.RowVersion });
                foreach (var item in listado)
                {
                    var entidadExistente = infoExistente.FirstOrDefault(w => w.Id == item.Id);
                    item.RowVersion = entidadExistente.RowVersion;
                }
                Update(listado);
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

        /// Autor: Jonathan Caipo
        /// Fecha: 07/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene toda información de T_PGeneralRelacionado por medio del Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Entidad - PgeneralRelacionado </returns>
        public PgeneralRelacionado? ObtenerPorId(int id)
        {
            try
            {
                var query = $@"
                    SELECT 
                        Id,
                        IdPGeneral AS IdPgeneral,
                        IdPGeneral_Relacionado AS IdPgeneralRelacionado,
                        Estado,
                        UsuarioCreacion,
                        UsuarioModificacion,
                        FechaCreacion,
                        FechaModificacion,
                        RowVersion,
                        IdMigracion 
                    FROM 
                        pla.T_PGeneralRelacionado
                    WHERE
                        Estado = 1 AND Id = @id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<PgeneralRelacionado>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#PGRR-OPI-001@Error en ObtenerPorId() {ex.Message}", ex);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 07/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene una lista de entidad mediante el idPGeneral
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns> Lista Entidad - List<PgeneralRelacionado>() </returns>
        public IEnumerable<PgeneralRelacionado> ObtenerPorIdPGeneral(int idPGeneral)
        {
            try
            {
                var query = $@"
                    SELECT 
                        Id,
                        IdPGeneral AS IdPgeneral,
                        IdPGeneral_Relacionado AS IdPgeneralRelacionado,
                        Estado,
                        UsuarioCreacion,
                        UsuarioModificacion,
                        FechaCreacion,
                        FechaModificacion,
                        RowVersion,
                        IdMigracion 
                    FROM 
                        pla.T_PGeneralRelacionado
                    WHERE
                        Estado = 1 AND IdPGeneral = @idPGeneral";
                var resultado = _dapperRepository.QueryDapper(query, new { idPGeneral });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<PgeneralRelacionado>>(resultado)!;
                }
                return new List<PgeneralRelacionado>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#PGRR-OPIPG-002@Error en ObtenerPorIdPGeneral() {ex.Message}", ex);
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 15/06/2023
        /// <summary>
        /// Obtiene la  lista de cursos relacionados a un programa
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns> List<PGeneralProgramaRelacionadoDTO> </returns>
        public IEnumerable<PGeneralProgramaRelacionadoDTO> ObtenerCursosRelacionadosPorPrograma(int idPGeneral)
        {

            try
            {
                var _query = @"SELECT Id,
                                   IdRelacionado,
                                   Nombre
                            FROM pla.V_TPGeneralRelacionado
                            WHERE EstadoPrograma = 1
                                  AND EstadoRelacionados = 1
                                  AND IdPGeneralPadre = @IdPGeneralPadre;";
                var respuestaDapper = _dapperRepository.QueryDapper(_query, new { IdPGeneralPadre = idPGeneral });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<List<PGeneralProgramaRelacionadoDTO>>(respuestaDapper)!;
                }
                return new List<PGeneralProgramaRelacionadoDTO>();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 15/06/2023
        /// <summary>
        /// Obtiene la  lista de cursos relacionados a un programa
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns> List<PGeneralProgramaRelacionadoDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCursosNoRelacionadosPorPrograma(int idPGeneral)
        {

            try
            {
                var _query = @"SELECT Id AS Id,
                                   Nombre AS Nombre 
                            FROM pla.T_PGeneral
                            WHERE Estado = 1
                                  AND Id NOT IN
                                      (
                                          SELECT IdRelacionado
                                          FROM pla.V_TPGeneralRelacionado
                                          WHERE IdPGeneralPadre = @Id
                                                AND EstadoPrograma = 1
                                                AND EstadoRelacionados = 1
                                      )
                                  AND Id != @Id;";
                var respuestaDapper = _dapperRepository.QueryDapper(_query, new { Id = idPGeneral });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<List<ComboDTO>>(respuestaDapper);
                }
                return null;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
    }
}
