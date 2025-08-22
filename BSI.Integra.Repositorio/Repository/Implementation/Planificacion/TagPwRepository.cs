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
    /// Repositorio: TagPwRepository
    /// Autor: Giancarlo Romero Monroy
    /// Fecha: 29/05/2023
    /// <summary>
    /// Gestión general de TPgeneralTagsPw
    /// </summary>
    public class TagPwRepository : GenericRepository<TTagPw>, ITagPwRepository
    {
        private Mapper _mapper;

        public TagPwRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TTagPw, TagPw>(MemberList.None).ReverseMap();
                cfg.CreateMap<TTagParametroSeoPw, TagParametroSeoPw>(MemberList.None).ReverseMap();
                cfg.CreateMap<TPgeneralTagsPw, PgeneralTagsPw>(MemberList.None).ReverseMap();

                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TTagPw MapeoEntidad(TagPw entidad)
        {
            try
            {
                TTagPw modelo = _mapper.Map<TTagPw>(entidad);
                if (entidad.PgeneralTagsPws != null && entidad.PgeneralTagsPws.Count > 0)
                {
                    modelo.TPgeneralTagsPws = _mapper.Map<List<TPgeneralTagsPw>>(entidad.PgeneralTagsPws);
                }
                if (entidad.TagParametroSeoPws != null && entidad.TagParametroSeoPws.Count > 0)
                {
                    modelo.TTagParametroSeoPws = _mapper.Map<List<TTagParametroSeoPw>>(entidad.TagParametroSeoPws);
                }
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TTagPw Add(TagPw entidad)
        {
            try
            {
                var tagPw = MapeoEntidad(entidad);
                base.Insert(tagPw);
                return tagPw;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TTagPw Update(TagPw entidad)
        {
            try
            {
                var tagPw = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                tagPw.RowVersion = entidadExistente.RowVersion;

                base.Update(tagPw);
                return tagPw;
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
        #endregion
        public TagPw? ObtenerPorId(int id)
        {
            try
            {
                string query = @"SELECT
                                    Id,
                                    Nombre,
                                    Descripcion,
                                    TagWebId,
                                    Estado,
                                    UsuarioCreacion,
                                    UsuarioModificacion,
                                    FechaCreacion,
                                    FechaModificacion,
                                    RowVersion,
                                    IdMigracion,
                                    Codigo
                                  FROM pla.T_Tag_PW
                                  WHERE Id = @id AND Estado = 1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<TagPw>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#TPwR-OPI-001@Error en ObtenerPorId() {ex.Message}", ex);
            }
        }
        public IEnumerable<TagEntidadPwDTO> Obtener()
        {
            try
            {
                IEnumerable<TagEntidadPwDTO> rpta = new List<TagEntidadPwDTO>();
                string _query = @"SELECT Id, Nombre, Descripcion, TagWebId, Codigo
                                  FROM pla.V_TagPw ORDER BY Id DESC";
                var result = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(result) && result != "[]")
                {
                    rpta = JsonConvert.DeserializeObject<List<TagEntidadPwDTO>>(result)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<ParametroSeoPortalWebDTO> ObtenerParametroPorIdTag(int idTag)
        {
            try
            {
                IEnumerable<ParametroSeoPortalWebDTO> rpta = new List<ParametroSeoPortalWebDTO>();
                string _query = @"SELECT Id, IdParametroSeo, NombreParametroSeo, Descripcion
                                  FROM [pla].[V_TagParametroSeo]
                                  WHERE IdTag = @IdTag";
                var result = _dapperRepository.QueryDapper(_query, new { idTag });
                if (!string.IsNullOrEmpty(result) && result != "[]")
                {
                    rpta = JsonConvert.DeserializeObject<List<ParametroSeoPortalWebDTO>>(result)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<ParametroSeoPw> ObtenerParametroSeoPwPorIdTag(int idTag)
        {
            IEnumerable<ParametroSeoPw> rpta = new List<ParametroSeoPw>();
            string _query = @"SELECT Id,
                                    Descripcion,
                                    IdTagPW,
                                    IdParametroSEOPW,
                                    Estado,
                                    UsuarioCreacion,
                                    UsuarioModificacion,
                                    FechaCreacion,
                                    FechaModificacion,
                                    RowVersion,
                                    IdMigracion
                              FROM pla.T_TagParametroSEO_PW
                              WHERE IdTagPw = @idTag AND Estado = 1";
            var result = _dapperRepository.QueryDapper(_query, new { idTag });
            if (!string.IsNullOrEmpty(result) && result != "[]")
            {
                rpta = JsonConvert.DeserializeObject<List<ParametroSeoPw>>(result)!;
            }
            return rpta;
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 25/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene la lista de de Ids y nombres de Tags Sin Asociar por medio de la lista de IdTags
        /// </summary>
        /// <param name="ids"></param>
        /// <returns> Lista DTO - List<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerTagSinAsociar(List<int> ids)
        {
            try
            {
                string query = @"SELECT Id, Nombre 
                                 FROM pla.T_Tag_PW
                                 WHERE Estado = 1 AND Id NOT IN @ids";
                var resultado = _dapperRepository.QueryDapper(query, new { ids });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado)!;
                }
                return new List<ComboDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#TPwR-OTSAPw-001@Error en ObtenerTagSinAsociar() {ex.Message}", ex);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 25/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene la lista de de Ids y nombres de Tags Sin Asociar por medio de la lista de IdTags
        /// </summary>
        /// <param name="ids"></param>
        /// <returns> Lista DTO - List<ComboDTO> </returns>
        public IEnumerable<DatosTagPwDTO> ObtenerTagAsociados(List<int> ids)
        {
            try
            {
                string query = "";
                if (ids.Count() > 0)
                {
                    query = @"SELECT Id, Nombre, Descripcion 
                              FROM pla.T_Tag_PW
                              WHERE Estado = 1 AND Id IN (" + string.Join(",", ids) + ")";
                }
                else
                {
                    query = @"SELECT Id, Nombre, Descripcion 
                              FROM pla.T_Tag_PW
                              WHERE Estado = 1 AND Id = @ids";
                }
                var resultado = _dapperRepository.QueryDapper(query, new { ids });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<DatosTagPwDTO>>(resultado)!;
                }
                return new List<DatosTagPwDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#TPwR-OTSAPw-002@Error en ObtenerTagAsociados() {ex.Message}", ex);
            }
        }
    }
}
