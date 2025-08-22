using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    /// Service: MaterialAccionService
    /// Autor: Giancarlo Romero Monroy
    /// Fecha: 25/05/2022
    /// <summary>
    /// Gestión general de T_MaterialAccion
    /// </summary>
    public class AsociarTagProgramaService : IAsociarTagProgramaService
    {
        private IUnitOfWork _unitOfWork;

        public AsociarTagProgramaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 25/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los materiales de accion
        /// </summary>
        /// <returns> Lista MaterialAccionDTO </returns>
        public async Task<AsociarTagProgramaComboDTO> ObtenerCombosModulo()
        {
            try
            {
                var taskArea = _unitOfWork.AreaCapacitacionRepository.ObtenerComboAsync();
                var taskSubArea = _unitOfWork.SubAreaCapacitacionRepository.ObtenerFiltroAsync();
                var taskCategoriaPrograma = _unitOfWork.CategoriaProgramaRepository.ObtenerComboAsync();
                var taskParametroSeoPw = _unitOfWork.ParametroSeoPwRepository.ObtenerComboAsync();

                var combos = new AsociarTagProgramaComboDTO();
                combos.Area = await taskArea;
                combos.SubArea = await taskSubArea;
                combos.CategoriaPrograma = await taskCategoriaPrograma;
                combos.ParametroSeo = await taskParametroSeoPw;

                return combos;
            }
            catch
            {
                throw;
            }
        }
        /// Autor: Giancarlo Romero Monroy
        /// Fecha: 25/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los materiales de accion
        /// </summary>
        /// <returns> Lista MaterialAccionDTO </returns>
        public List<PGeneralDTO> ObtenerProgramas()
        {
            try
            {
                return _unitOfWork.PGeneralRepository.ObtenerTodo();
            }
            catch (Exception)
            {
                throw;
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
                return _unitOfWork.TagParametroSeoPwRepository.ObtenerTodoParametroPorIdTag(idTag);
            }
            catch
            {
                throw;
            }
        }
        /// Autor: Giancarlo Romero Monroy
        /// Fecha: 25/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los materiales de accion
        /// </summary>
        /// <returns> Lista MaterialAccionDTO </returns>
        public IEnumerable<ComboDTO> ObtenerTagSinAsociarPw(int idPgeneral)
        {
            try
            {
                var idsTags = _unitOfWork.PgeneralTagsPwRepository.ObtenerIdsTagPorPGeneral(idPgeneral);
                var objetoTag = _unitOfWork.TagPwRepository.ObtenerTagSinAsociar(idsTags).ToList();
                return objetoTag;
            }
            catch
            {
                throw;
            }
        }
        /// Autor: Klebert Layme
        /// Fecha: 07/06/2023
        /// Version: 1.0
        /// <summary>
        /// Asociar Programas Tags
        /// </summary>
        /// <returns> true </returns>
        public bool AsociarTag(List<int> idsTags, int idPgeneral, string usuario)
        {
            try
            {
                if (idPgeneral != 0)
                {
                    var pGeneralTagsPw = idsTags.Select(item => new PgeneralTagsPw
                    {
                        IdPgeneral = idPgeneral,
                        IdTagPW = item,
                        Estado = true,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    }).ToList();
                    var resut = _unitOfWork.PgeneralTagsPwRepository.Add(pGeneralTagsPw);
                    _unitOfWork.Commit();

                    return true;
                }
                else
                    throw new BadRequestException("Id PGeneral no válido.");
            }
            catch
            {
                throw;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 14/08/2023
        /// Version: 1.0
        /// <summary>
        /// Desasocia Tag según idPGeneral
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <param name="idTag"></param>
        /// <returns> bool </returns>
        public bool DesasociarTag(int idPGeneral, int idTag, string usuario)
        {
            try
            {
                var objetoPGeneralTag = _unitOfWork.PgeneralTagsPwRepository.ObtenerPorIdPGeneralyIdTagPw(idPGeneral, idTag);
                if (objetoPGeneralTag != null && objetoPGeneralTag.Id != 0)
                {
                    _unitOfWork.PgeneralTagsPwRepository.Delete(objetoPGeneralTag.Id, usuario);
                    _unitOfWork.Commit();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                throw;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 10/08/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los Tags Asociados por PGeneral
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns> Lista DTO - List<ComboDTO> </returns>
        public IEnumerable<DatosTagPwDTO> ObtenerTodoTagPorPrograma(int idPGeneral)
        {
            try
            {
                var idsTags = _unitOfWork.PgeneralTagsPwRepository.ObtenerIdsTagPorPGeneral(idPGeneral);
                var objetoTag = _unitOfWork.TagPwRepository.ObtenerTagAsociados(idsTags).ToList();

                return objetoTag;
            }
            catch
            {
                throw;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 10/08/2023
        /// Version: 1.0
        /// <summary>
        /// Inserta Tag
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="usuario"></param>
        /// <returns> Entidad - TagPw </returns>
        public TagPw InsertarTagAsociar(CompuestoTagDTO dto, string usuario)
        {
            try
            {
                TagPw tag = new TagPw();

                tag.Nombre = dto.ObjetoTag.Nombre;
                tag.Descripcion = dto.ObjetoTag.Descripcion;
                tag.TagWebId = dto.ObjetoTag.TagWebId;
                tag.Estado = true;
                tag.UsuarioCreacion = usuario;
                tag.UsuarioModificacion = usuario;
                tag.FechaCreacion = DateTime.Now;
                tag.FechaModificacion = DateTime.Now;

                tag.PgeneralTagsPws = new List<PgeneralTagsPw>();
                tag.TagParametroSeoPws = new List<TagParametroSeoPw>();

                PgeneralTagsPw pGeneralTags = new PgeneralTagsPw();
                pGeneralTags.IdPgeneral = dto.IdPGeneral;
                pGeneralTags.Estado = true;
                pGeneralTags.UsuarioCreacion = usuario;
                pGeneralTags.UsuarioModificacion = usuario;
                pGeneralTags.FechaCreacion = DateTime.Now;
                pGeneralTags.FechaModificacion = DateTime.Now;

                tag.PgeneralTagsPws.Add(pGeneralTags);

                foreach (var item in dto.ListaParametro)
                {
                    TagParametroSeoPw tagParametroSeo = new TagParametroSeoPw();
                    if (String.IsNullOrEmpty(item.Contenido))
                        tagParametroSeo.Descripcion = "<!--vacio-->";
                    else
                        tagParametroSeo.Descripcion = item.Contenido;
                    tagParametroSeo.IdParametroSeopw = item.Id;
                    tagParametroSeo.Estado = true;
                    tagParametroSeo.UsuarioCreacion = usuario;
                    tagParametroSeo.UsuarioModificacion = usuario;
                    tagParametroSeo.FechaCreacion = DateTime.Now;
                    tagParametroSeo.FechaModificacion = DateTime.Now;

                    tag.TagParametroSeoPws.Add(tagParametroSeo);
                }
                _unitOfWork.TagPwRepository.Add(tag);
                _unitOfWork.Commit();

                return tag;
            }
            catch
            {
                throw;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 10/08/2023
        /// Version: 1.0
        /// <summary>
        /// Actualiza Tag
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="usuario"></param>
        /// <returns> Entidad - TagPw </returns>
        public TagPw ActualizarTag(CompuestoTagDTO dto, string usuario)
        {
            try
            {
                TagPw? tag = _unitOfWork.TagPwRepository.ObtenerPorId(dto.ObjetoTag.Id);

                if (tag != null)
                {
                    EliminacionLogicoPorTagPw(dto.ObjetoTag.Id, usuario, dto.ListaParametro);

                    tag.Nombre = dto.ObjetoTag.Nombre;
                    tag.Descripcion = dto.ObjetoTag.Descripcion;
                    tag.TagWebId = dto.ObjetoTag.TagWebId;
                    tag.UsuarioModificacion = usuario;
                    tag.FechaModificacion = DateTime.Now;

                    tag.TagParametroSeoPws = new List<TagParametroSeoPw>();

                    foreach (var item in dto.ListaParametro)
                    {
                        TagParametroSeoPw? tagParametroSeo = _unitOfWork.TagParametroSeoPwRepository.ObtenerPorIdParametroSEOyIdTag(item.Id, dto.ObjetoTag.Id);
                        if (tagParametroSeo != null)
                        {
                            tagParametroSeo.IdParametroSeopw = item.Id;
                            if (String.IsNullOrEmpty(item.Contenido))
                                tagParametroSeo.Descripcion = "<!--vacio-->";
                            else
                                tagParametroSeo.Descripcion = item.Contenido;
                            tagParametroSeo.UsuarioModificacion = usuario;
                            tagParametroSeo.FechaModificacion = DateTime.Now;
                        }
                        else
                        {
                            tagParametroSeo = new TagParametroSeoPw();
                            tagParametroSeo.IdParametroSeopw = item.Id;
                            if (String.IsNullOrEmpty(item.Contenido))
                                tagParametroSeo.Descripcion = "<!--vacio-->";
                            else
                                tagParametroSeo.Descripcion = item.Contenido;
                            tagParametroSeo.UsuarioCreacion = usuario;
                            tagParametroSeo.UsuarioModificacion = usuario;
                            tagParametroSeo.FechaCreacion = DateTime.Now;
                            tagParametroSeo.FechaModificacion = DateTime.Now;
                            tagParametroSeo.Estado = true;
                        }
                        tag.TagParametroSeoPws.Add(tagParametroSeo);
                    }
                    _unitOfWork.TagPwRepository.Update(tag);
                    _unitOfWork.Commit();

                    return tag;
                }
                else
                {
                    throw new BadRequestException("El IdTag no existe.");
                }
            }
            catch
            {
                throw;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 23/08/2023
        /// Versión: 1.0
        /// <summary>
        /// Elimina (Actualiza estado a false ) todos las registros de ParametroSeo asociados a TagPw
        /// </summary>
        /// <param name="idTag"></param>
        /// <param name="usuario"></param>
        /// <param name="nuevos"></param>
        public void EliminacionLogicoPorTagPw(int idTag, string usuario, List<ParametroSeoAsociadosDTO> nuevos)
        {
            try
            {
                var listaBorrar = _unitOfWork.TagParametroSeoPwRepository.ObtenerPorIdTag(idTag).ToList();
                //listaBorrar.RemoveAll(x => nuevos.Any(y => y.Id.Equals(x.IdParametroSeopw)));
                if (listaBorrar != null && listaBorrar.Count() > 0)
                {
                    _unitOfWork.TagParametroSeoPwRepository.Delete(listaBorrar.Select(a => a.Id), usuario);
                    _unitOfWork.Commit();
                }
            }
            catch
            {
                throw;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 23/08/2023
        /// Versión: 1.0
        /// <summary>
        /// Elimina Tag
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="usuario"></param>
        /// <returns> Bool </returns>
        public bool EliminarTag(int idTag, string usuario)
        { 
            try
            {
                var tag = _unitOfWork.TagPwRepository.ObtenerPorId(idTag);

                if (tag != null)
                {
                    _unitOfWork.TagPwRepository.Delete(idTag, usuario);

                    var listaHijosTagParametroSeo = _unitOfWork.TagParametroSeoPwRepository.ObtenerPorIdTag(idTag).Select(x => x.Id).ToList();
                    _unitOfWork.TagParametroSeoPwRepository.Delete(listaHijosTagParametroSeo, usuario);

                    var listaHijosPGeneralTag = _unitOfWork.PgeneralTagsPwRepository.ObtenerPorIdTag(idTag).Select(y => y.Id).ToList();
                    _unitOfWork.PgeneralTagsPwRepository.Delete(listaHijosPGeneralTag, usuario);

                    _unitOfWork.Commit();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
