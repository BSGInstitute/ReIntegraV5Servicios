

using AutoMapper;

using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

using Nancy.Diagnostics;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    public class TagPwService : ITagPwService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public TagPwService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {
                    //cfg.CreateMap<MaterialAdicionalAulaVirtual, MaterialAdicionalAulaVirtualDTO>(MemberList.None).ReverseMap();
                }
            );
            _mapper = new Mapper(config);
        }
        public IEnumerable<TagEntidadPwDTO> Obtener()
        {
            try
            {
                var respuesta = _unitOfWork.TagPwRepository.Obtener();
                return _mapper.Map<List<TagEntidadPwDTO>>(respuesta);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IEnumerable<ParametroSeoPortalWebDTO> ObtenerParametroPorIdTag(int id)
        {
            try
            {
                var respuesta = _unitOfWork.TagPwRepository.ObtenerParametroPorIdTag(id);
                return _mapper.Map<List<ParametroSeoPortalWebDTO>>(respuesta);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IEnumerable<TagEntidadPwDTO> Insertar(TagEntidadPwDTO dto, string usuario)
        {
            try
            {
                if (dto != null)
                {
                    TagPw entidad = new()
                    {
                        Nombre = dto.Nombre,
                        Descripcion = dto.Descripcion,
                        Codigo = dto.Codigo,
                        Estado = true,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                    };
                    entidad.TagParametroSeoPws = new List<TagParametroSeoPw>();

                    foreach (var item in dto.ParametroSeoAsociados)
                    {
                        entidad.TagParametroSeoPws.Add(new TagParametroSeoPw
                        {
                            IdTagPw = entidad.Id,
                            IdParametroSeopw = item.IdParametroSeopw,
                            Descripcion = item.Descripcion,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            Estado = true
                        });
                    }
                    var respuesta = _unitOfWork.TagPwRepository.Add(entidad);
                    _unitOfWork.Commit();

                    var entidadActualizar = _unitOfWork.TagPwRepository.ObtenerPorId(respuesta.Id);
                    entidadActualizar.TagWebId = respuesta.Id;
                    _unitOfWork.DetachAll();
                    _unitOfWork.TagPwRepository.Update(entidadActualizar);
                    _unitOfWork.Commit();
                    return _unitOfWork.TagPwRepository.Obtener();
                }
                else
                    throw new BadRequestException("Entidad Nula");
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IEnumerable<TagEntidadPwDTO> Actualizar(TagEntidadPwDTO dto, string usuario)
        {
            try
            {
                var tagPw = _unitOfWork.TagPwRepository.ObtenerPorId(dto.Id);
                if(tagPw != null)
                    {
                    var parametroSeoHijos = _unitOfWork.TagPwRepository.ObtenerParametroSeoPwPorIdTag(dto.Id).ToList();

                    parametroSeoHijos.RemoveAll(x => dto.ParametroSeoAsociados.Any(y => y.Id == x.Id));
                    if(parametroSeoHijos != null && parametroSeoHijos.Count > 0)
                    {
                        _unitOfWork.TagParametroSeoPwRepository.Delete(parametroSeoHijos.Select(x => x.Id), usuario);
                        _unitOfWork.Commit();
                    };

                    tagPw.Nombre = dto.Nombre;
                    tagPw.Descripcion = dto.Descripcion;
                    tagPw.Codigo = dto.Codigo;
                    tagPw.UsuarioModificacion = usuario;
                    tagPw.FechaModificacion = DateTime.Now;
                    tagPw.TagParametroSeoPws = new List<TagParametroSeoPw>();
                    foreach (var item in dto.ParametroSeoAsociados)
                    {
                        var tagParametro = _unitOfWork.TagParametroSeoPwRepository.ObtenerPorId((int)item.Id);
                        if(tagParametro != null && item.Id != 0)
                        {
                            tagParametro.Descripcion = item.Descripcion;
                            tagParametro.UsuarioModificacion = usuario;
                            tagParametro.FechaModificacion = DateTime.Now;
                        } else
                        {
                            tagParametro = new TagParametroSeoPw();
                            tagParametro.IdTagPw = tagPw.Id;
                            tagParametro.IdParametroSeopw = item.IdParametroSeopw;
                            tagParametro.Descripcion = item.Descripcion;
                            tagParametro.UsuarioCreacion = usuario;
                            tagParametro.UsuarioModificacion = usuario;
                            tagParametro.FechaCreacion = DateTime.Now;
                            tagParametro.FechaModificacion = DateTime.Now;
                            tagParametro.Estado = true;
                        }
                        tagPw.TagParametroSeoPws.Add(tagParametro);
                    }
                    var respuesta = _unitOfWork.TagPwRepository.Update(tagPw);
                    _unitOfWork.Commit();
                    return _unitOfWork.TagPwRepository.Obtener();
                }
                else
                    throw new BadRequestException($"No se encontro la entidad con el id {dto.Id}");
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool Eliminar(int id, string usuario)
        {
            try
            {
                var tagPw = _unitOfWork.TagPwRepository.ObtenerPorId(id);
                if (tagPw != null && tagPw.Id != 0)
                {
                    var respuesta = _unitOfWork.TagPwRepository.Delete(id, usuario);
                    _unitOfWork.Commit();
                    return respuesta;
                }
                else
                    throw new BadRequestException($"No se encontro la entidad con el id {id}");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
