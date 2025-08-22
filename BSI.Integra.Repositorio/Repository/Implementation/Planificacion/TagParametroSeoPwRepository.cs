
using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;

using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    public class TagParametroSeoPwRepository : GenericRepository<TTagParametroSeoPw>, ITagParametroSeoPwRepository
    {
        private Mapper _mapper;

        public TagParametroSeoPwRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TTagParametroSeoPw, TagParametroSeoPw>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TTagParametroSeoPw MapeoEntidad(TagParametroSeoPw entidad)
        {
            try
            {
                TTagParametroSeoPw modelo = _mapper.Map<TTagParametroSeoPw>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TTagParametroSeoPw Add(TagParametroSeoPw entidad)
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
        public TTagParametroSeoPw Update(TagParametroSeoPw entidad)
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
        public IEnumerable<TTagParametroSeoPw> Add(IEnumerable<TagParametroSeoPw> listadoEntidad)
        {
            try
            {
                List<TTagParametroSeoPw> listado = new List<TTagParametroSeoPw>();
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
        public IEnumerable<TTagParametroSeoPw> Update(IEnumerable<TagParametroSeoPw> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TTagParametroSeoPw> listado = new List<TTagParametroSeoPw>();
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
        public TagParametroSeoPw? ObtenerPorId(int id)
        {
            try
            {
                string query = @"SELECT Id,
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
                              WHERE Id = @id AND Estado = 1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<TagParametroSeoPw>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#TPSR-OPI-001@Error en ObtenerPorId() {ex.Message}", ex);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 23/08/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene registros por el IdTag
        /// </summary>
        /// <param name="idTag"></param>
        /// <returns> Lista DTO - List<ParametroContenidoDTO> </returns>
        public IEnumerable<ParametroContenidoDTO> ObtenerTodoParametroPorIdTag(int idTag)
        {
            try
            {
                var query = @"SELECT Id, IdTag, IdParametroSeo, NombreParametroSeo, Descripcion AS Contenido
                              FROM pla.V_TagParametroSeo
                              WHERE IdTag = @idTag";
                var resultado = _dapperRepository.QueryDapper(query, new { idTag });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<ParametroContenidoDTO>>(resultado)!;
                }
                return new List<ParametroContenidoDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#TPSR-OTPIT-002@Error en ObtenerPorId() {ex.Message}", ex);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 23/08/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la entidad por medio del idParametroSeopw y idTagPw
        /// </summary>
        /// <param name="idParametroSeopw"></param>
        /// <param name="idTagPw"></param>
        /// <returns> Entidad - TagParametroSeoPw </returns>
        public TagParametroSeoPw? ObtenerPorIdParametroSEOyIdTag(int idParametroSeopw, int idTagPw)
        {
            try
            {
                TagParametroSeoPw rpta = new TagParametroSeoPw();
                string query = @"SELECT Id,
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
                              WHERE IdParametroSeopw = @idParametroSeopw AND IdTagPw = @idTagPw AND Estado = 1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idParametroSeopw, idTagPw });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<TagParametroSeoPw>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#TPSR-OTPIT-004@Error en ObtenerPorId() {ex.Message}", ex);

            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 23/08/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene registros por la Tabla TagParametroSeoPw
        /// </summary>
        /// <param name="idTag"></param>
        /// <returns> Lista Entidad - List<ParametroSeoPw>() </returns>
        public IEnumerable<TagParametroSeoPw> ObtenerPorIdTag(int idTag)
        {
            try
            {
                var query = @"SELECT Id,
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
                            WHERE Estado = 1 AND IdTagPW = @idTag";
                var resultado = _dapperRepository.QueryDapper(query, new { idTag });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<TagParametroSeoPw>>(resultado)!;
                }
                return new List<TagParametroSeoPw>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#TPSR-OPIT-005@Error en ObtenerPorIdTag() {ex.Message}", ex);
            }
        }
    }
}
