using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    /// Service: AreaCapacitacionService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 15/07/2022
    /// <summary>
    /// Gestión general de T_AreaCapacitacion
    /// </summary>
    public class AreaCapacitacionService : IAreaCapacitacionService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public AreaCapacitacionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TAreaCapacitacion, AreaCapacitacion>(MemberList.None).ReverseMap();
                    cfg.CreateMap<TAreaCapacitacion, AreaCapacitacionDTO>(MemberList.None).ReverseMap();
                    cfg.CreateMap<AreaCapacitacion, AreaCapacitacionDTO>(MemberList.None).ReverseMap();
                }
            );
            _mapper = new Mapper(config);
        }

        //#region Metodos Base
        //public AreaCapacitacion Add(AreaCapacitacion entidad)
        //{
        //    try
        //    {
        //        var modelo = _unitOfWork.AreaCapacitacionRepository.Add(entidad);
        //        _unitOfWork.Commit();
        //        return _mapper.Map<AreaCapacitacion>(modelo);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public AreaCapacitacion Update(AreaCapacitacion entidad)
        //{
        //    try
        //    {
        //        var modelo = _unitOfWork.AreaCapacitacionRepository.Update(entidad);
        //        _unitOfWork.Commit();
        //        return _mapper.Map<AreaCapacitacion>(modelo);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public bool Delete(int id, string usuario)
        //{
        //    try
        //    {
        //        _unitOfWork.AreaCapacitacionRepository.Delete(id, usuario);
        //        _unitOfWork.Commit();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public List<AreaCapacitacion> Add(List<AreaCapacitacion> listadoEntidad)
        //{
        //    try
        //    {
        //        var modelo = _unitOfWork.AreaCapacitacionRepository.Add(listadoEntidad);
        //        _unitOfWork.Commit();
        //        return _mapper.Map<List<AreaCapacitacion>>(modelo);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public List<AreaCapacitacion> Update(List<AreaCapacitacion> listadoEntidad)
        //{
        //    try
        //    {
        //        var modelo = _unitOfWork.AreaCapacitacionRepository.Update(listadoEntidad);
        //        _unitOfWork.Commit();
        //        return _mapper.Map<List<AreaCapacitacion>>(modelo);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public bool Delete(List<int> listadoIds, string usuario)
        //{
        //    try
        //    {
        //        _unitOfWork.AreaCapacitacionRepository.Delete(listadoIds, usuario);
        //        _unitOfWork.Commit();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //#endregion

        /// Autor: Klebert Layme
        /// Fecha: 24/04/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene por id Area de Capacitacion
        /// </summary>
        /// <param name="idPGeneral">Id del area de Capacitacion</param>

        /// <returns> AreaCapacitacion </returns>
        public AreaCapacitacionDTO ObtenerPorId(int id)
        {
            try
            {
                var respuesta = _unitOfWork.AreaCapacitacionRepository.ObtenerPorId(id);
                if (respuesta != null && respuesta.Id != 0)
                {
                    return _mapper.Map<AreaCapacitacionDTO>(respuesta);
                }
                else
                {
                    throw new BadRequestException($"No existe la entidad con el id {id}");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// Autor: Klebert Layme
        /// Fecha: 24/04/2023
        /// Version: 1.0
        /// <summary>
        /// Registra un nuevo Area de capacitacion
        /// </summary>
        /// <param name="dto">Area de capacitacion</param>
        /// <param name="usuario">Usuario Registro</param>
        /// <returns>AreaCapacitacion</returns>
        public AreaCapacitacionDTO Insertar(CompuestoAreaDTO dto, string usuario)
        {
            try
            {
                if (dto != null)
                {
                    AreaCapacitacion
                        entidad = new()
                        {
                            Nombre = dto.AreaCapacitacion.Nombre,
                            Descripcion = dto.AreaCapacitacion.Descripcion,
                            ImgPortada = dto.AreaCapacitacion.ImgPortada,
                            ImgSecundaria = dto.AreaCapacitacion.ImgSecundaria,
                            ImgPortadaAlt = dto.AreaCapacitacion.ImgPortadaAlt,
                            ImgSecundariaAlt = dto.AreaCapacitacion.ImgSecundariaAlt,
                            IdArea = dto.AreaCapacitacion.IdArea,
                            EsWeb = dto.AreaCapacitacion.EsWeb,
                            EsVisibleWeb = dto.AreaCapacitacion.EsVisibleWeb,
                            DescripcionHtml = dto.AreaCapacitacion.DescripcionHtml,
                            IdAreaCapacitacionFacebook = dto.AreaCapacitacion.IdAreaCapacitacionFacebook,
                            Estado = true,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                        };

                    entidad.AreaParametroSeoPw = new List<AreaParametroSeoPw>();
                    foreach (var item in dto.ListaParametro)
                    {
                        AreaParametroSeoPw areaPar0ametroSeo = new AreaParametroSeoPw();

                        if (String.IsNullOrEmpty(item.Contenido))
                            areaPar0ametroSeo.Descripcion = "<!--vacio-->";
                        else
                            areaPar0ametroSeo.Descripcion = item.Contenido;
                        areaPar0ametroSeo.IdAreaCapacitacion = dto.AreaCapacitacion.Id;
                        areaPar0ametroSeo.IdParametroSeopw = item.Id;
                        areaPar0ametroSeo.Estado = true;
                        areaPar0ametroSeo.UsuarioCreacion = usuario;
                        areaPar0ametroSeo.UsuarioModificacion = usuario;
                        areaPar0ametroSeo.FechaCreacion = DateTime.Now;
                        areaPar0ametroSeo.FechaModificacion = DateTime.Now;

                        entidad.AreaParametroSeoPw.Add(areaPar0ametroSeo);
                    }
                    var respuesta = _unitOfWork.AreaCapacitacionRepository.Add(entidad);
                    _unitOfWork.Commit();
                    return _mapper.Map<AreaCapacitacionDTO>(respuesta);
                }
                else
                    throw new BadRequestException("Entidad Nula");
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Klebert Layme
        /// Fecha: 24/04/2023
        /// Version: 1.0
        /// <summary>
        /// Registra una lista de Area de capacitacion
        /// </summary>
        /// <param name="dtos">Lista de Area de capacitacion</param>
        /// <param name="usuario">Usuario de Registro</param>
        /// <returns>Lista AreaCapacitacionDTO</returns>
        public List<AreaCapacitacionDTO> InsertarLista(List<AreaCapacitacionDTO> dtos, string usuario)
        {
            try
            {
                if (dtos != null && dtos.Count() > 0)
                {
                    List<AreaCapacitacion> entidades = new();
                    foreach (var item in dtos)
                    {
                        AreaCapacitacion entidad = new()
                        {
                            Nombre = item.Nombre,
                            Descripcion = item.Descripcion,
                            ImgPortada = item.ImgPortada,
                            ImgSecundaria = item.ImgSecundaria,
                            ImgPortadaAlt = item.ImgPortadaAlt,
                            ImgSecundariaAlt = item.ImgSecundariaAlt,
                            IdArea = item.IdArea,
                            EsWeb = item.EsWeb,
                            DescripcionHtml = item.DescripcionHtml,
                            IdAreaCapacitacionFacebook = item.IdAreaCapacitacionFacebook,
                            Estado = true,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                        };
                        entidades.Add(entidad);
                    }
                    var respuesta = _unitOfWork.AreaCapacitacionRepository.Add(entidades);
                    _unitOfWork.Commit();
                    return _mapper.Map<List<AreaCapacitacionDTO>>(respuesta);
                }
                return new List<AreaCapacitacionDTO>();
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Klebert Layme
        /// Fecha: 24/04/2023
        /// Version: 1.0
        /// <summary>
        /// Modifica un Area de capacitacion
        /// </summary>
        /// <param name="dto">Are de capacitacion</param>
        /// <param name="usuario">Usuario Modificacion</param>
        /// <returns>AreaCapacitacion</returns>
        public AreaCapacitacionDTO Actualizar(CompuestoAreaDTO dto, string usuario)
        {
            try
            {
                AreaCapacitacion entidad = new();
                if (dto != null)
                {
                    if (dto.AreaCapacitacion.Id != 0)
                    {
                        entidad = _unitOfWork.AreaCapacitacionRepository.ObtenerPorId(dto.AreaCapacitacion.Id);
                        if (entidad != null && entidad.Id != 0)
                        {
                            entidad.Nombre = dto.AreaCapacitacion.Nombre;
                            entidad.Descripcion = dto.AreaCapacitacion.Descripcion;
                            entidad.ImgPortada = dto.AreaCapacitacion.ImgPortada;
                            entidad.ImgSecundaria = dto.AreaCapacitacion.ImgSecundaria;
                            entidad.ImgPortadaAlt = dto.AreaCapacitacion.ImgPortadaAlt;
                            entidad.ImgSecundariaAlt = dto.AreaCapacitacion.ImgSecundariaAlt;
                            entidad.IdArea = dto.AreaCapacitacion.IdArea;
                            entidad.EsVisibleWeb = dto.AreaCapacitacion.EsVisibleWeb;
                            entidad.EsWeb = dto.AreaCapacitacion.EsWeb;
                            entidad.DescripcionHtml = dto.AreaCapacitacion.DescripcionHtml;
                            entidad.IdAreaCapacitacionFacebook = dto.AreaCapacitacion.IdAreaCapacitacionFacebook;
                            entidad.Estado = true;
                            entidad.FechaModificacion = DateTime.Now;
                            entidad.UsuarioModificacion = usuario;
                        }
                        else
                            throw new BadRequestException("Entidad no encontrada");

                        entidad.AreaParametroSeoPw = new List<AreaParametroSeoPw>();
                        IAreaParametroSeoPwService areaParametroSeoPwService = new AreaParametroSeoPwService(_unitOfWork);
                        areaParametroSeoPwService.EliminarPorIdAreaCapacitacion(usuario, dto.AreaCapacitacion.Id, dto.ListaParametro);

                        var resultado = _unitOfWork.AreaParametroSeoPwRepository.ObtenerPorIdAreaCapacitacion(dto.AreaCapacitacion.Id);
                        foreach (var item in dto.ListaParametro)
                        {
                            AreaParametroSeoPw areaParametroSeo = resultado.FirstOrDefault(x => x.IdParametroSeopw == item.Id);
                            if (areaParametroSeo != null && areaParametroSeo.Id > 0)
                            {
                                areaParametroSeo.Descripcion = string.IsNullOrEmpty(item.Contenido) ? "<!--vacio-->" : item.Contenido;
                                areaParametroSeo.IdAreaCapacitacion = dto.AreaCapacitacion.Id;
                                areaParametroSeo.Estado = true;
                                areaParametroSeo.UsuarioModificacion = usuario;
                                areaParametroSeo.FechaModificacion = DateTime.Now;
                            }
                            else
                            {
                                areaParametroSeo = new();
                                areaParametroSeo.Descripcion = string.IsNullOrEmpty(item.Contenido) ? "<!--vacio-->" : item.Contenido;
                                areaParametroSeo.IdAreaCapacitacion = dto.AreaCapacitacion.Id;
                                areaParametroSeo.IdParametroSeopw = item.Id;
                                areaParametroSeo.Estado = true;
                                areaParametroSeo.UsuarioCreacion = usuario;
                                areaParametroSeo.UsuarioModificacion = usuario;
                                areaParametroSeo.FechaCreacion = DateTime.Now;
                                areaParametroSeo.FechaModificacion = DateTime.Now;
                            }
                            entidad.AreaParametroSeoPw.Add(areaParametroSeo);
                        }
                    }
                    else
                        throw new BadRequestException("Id Entidad 0");
                }
                else
                    throw new BadRequestException("Entidad Nula");

                var respuesta = _unitOfWork.AreaCapacitacionRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<AreaCapacitacionDTO>(respuesta);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// Autor: Klebert Layme
        /// Fecha: 24/04/2023
        /// Version: 1.0
        /// <summary>
        /// elimina Un area de capacitacion
        /// </summary>
        /// <param name="id">Id del area de capacitacion</param>
        /// <returns> Area Capacitacion </returns>
        public bool Eliminar(int Id, string usuario)
        {
            try
            {
                var areaCapacitacion = _unitOfWork.AreaCapacitacionRepository.ObtenerPorId(Id);
                if (areaCapacitacion != null && areaCapacitacion.Id > 0)
                {
                    var respuesta = _unitOfWork.AreaCapacitacionRepository.Delete(Id, usuario);
                    var hijos = _unitOfWork.AreaParametroSeoPwRepository.ObtenerPorId(areaCapacitacion.Id);
                    _unitOfWork.AreaParametroSeoPwRepository.Delete(hijos.Select(x => x.Id), usuario);
                    _unitOfWork.Commit();
                    return respuesta;
                }
                else
                {
                    throw new BadRequestException($"No se encontro la entidad con el id {Id}");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 15/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_AreaCapacitacion
        /// </summary>
        /// <returns> List<AreaCapacitacionDTO> </returns>
        public IEnumerable<AreaCapacitacionDTO> Obtener()
        {
            try
            {
                return _unitOfWork.AreaCapacitacionRepository.Obtener();
            }
            catch
            {
                throw;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 15/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_AreaCapacitacion para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.AreaCapacitacionRepository.ObtenerCombo();
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 15/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_AreaCapacitacion para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<AreaCapacitacionFiltroDTO> ObtenerFiltro()
        {
            try
            {
                return _unitOfWork.AreaCapacitacionRepository.ObtenerFiltro();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
