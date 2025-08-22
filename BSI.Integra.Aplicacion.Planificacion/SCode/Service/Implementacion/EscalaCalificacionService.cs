using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion.Planificacion
{
    /// Service: EscalaCalificacionService
    /// Autor: Gretel Canasa
    /// Fecha: 11/05/2023
    /// <summary>
    /// Gestión general de T_EscalaCalificacion
    /// </summary>
    public class EscalaCalificacionService : IEscalaCalificacionService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public EscalaCalificacionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TEscalaCalificacion, EscalaCalificacion>(MemberList.None).ReverseMap();
                    cfg.CreateMap<TEscalaCalificacion, EscalaCalificacionDTO>(MemberList.None).ReverseMap();
                    cfg.CreateMap<EscalaCalificacion, EscalaCalificacionDTO>(MemberList.None).ReverseMap();
                    cfg.CreateMap<EscalaCalificacionDetalle, EscalaCalificacionDetalleDTO>(MemberList.None).ReverseMap();


                }
            );
            _mapper = new Mapper(config);
        }
        /// Autor: Gretel Canasa
        /// Fecha: 18/04/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los materiales de Escala Calificacion
        /// </summary>
        /// <returns> Lista EscalaCalificacionDTO </returns>
        public IEnumerable<EscalaCalificacionDTO> Obtener()
        {
            try
            {
                var respuesta = _unitOfWork.EscalaCalificacionRepository.ObtenerTodo();
                foreach (var item in respuesta)
                {
                    item.EscalaCalificacionDetalles = _mapper.Map<IEnumerable<EscalaCalificacionDetalleDTO>>(_unitOfWork.EscalaCalificacionDetalleRepository.ObtenerPorIdEscalaCalificacion(item.Id)); ;
                }
                return respuesta;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Gretel Canasa
        /// Fecha: 11/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene Seccion Especifico asociada a un ProgramaGeneral con un Titulo especifico.
        /// </summary>
        /// <param name="idPGeneral">Id del Programa General</param>
        /// <param name="tituloSeccion">Titulo de la Seccion</param>
        /// <returns> PGeneralDocumentoSeccionDTO </returns>
        public EscalaCalificacionDTO ObtenerPorId(int id)
        {
            try
            {
                var respuesta = _mapper.Map<EscalaCalificacionDTO>(_unitOfWork.EscalaCalificacionRepository.ObtenerPorId(id));

                if (respuesta != null && respuesta.Id != 0)
                {
                    respuesta.EscalaCalificacionDetalles = _mapper.Map<IEnumerable<EscalaCalificacionDetalleDTO>>(_unitOfWork.EscalaCalificacionDetalleRepository.ObtenerPorIdEscalaCalificacion(id));
                    return respuesta;
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
        /// Autor: Gretel Canasa
        /// Fecha: 11/05/2023
        /// Version: 1.0
        /// <summary>
        /// Registra un nuevo Material de Accion
        /// </summary>
        /// <param name="dto">Material de Accion</param>
        /// <param name="usuario">Usuario Registro</param>
        /// <returns>EscalaCalificacionDTO</returns>
        public EscalaCalificacionDTO Insertar(EscalaCalificacionDTO dto, string usuario)
        {
            try
            {
                if (dto != null)
                {
                    EscalaCalificacion entidad = new()
                    {
                        Nombre = dto.Nombre,
                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario
                    };
                    if (dto.EscalaCalificacionDetalles != null && dto.EscalaCalificacionDetalles.Count() > 0)
                    {
                        entidad.EscalaCalificacionDetalles = dto.EscalaCalificacionDetalles.Select(x => new EscalaCalificacionDetalle
                        {
                            Nombre = x.Nombre,
                            Valor = x.Valor,
                            Estado = true,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario
                        }).ToList();
                    }
                    var respuesta = _unitOfWork.EscalaCalificacionRepository.Add(entidad);
                    _unitOfWork.Commit();
                    dto = _mapper.Map<EscalaCalificacionDTO>(respuesta);
                    dto.EscalaCalificacionDetalles = _mapper.Map<IEnumerable<EscalaCalificacionDetalleDTO>>(_unitOfWork.EscalaCalificacionDetalleRepository.ObtenerPorIdEscalaCalificacion(dto.Id));
                    return dto;
                }
                else
                    throw new BadRequestException("Entidad Nula");
            }
            catch (Exception)
            {
                _unitOfWork.Dispose();
                throw;
            }
        }
        /// Autor: Gretel Canasa
        /// Fecha: 11/05/2023
        /// Version: 1.0
        /// <summary>
        /// Registra una lista de Materiales de Accion
        /// </summary>
        /// <param name="dtos">Lista de Materiales de Accion</param>
        /// <param name="usuario">Usuario de Registro</param>
        /// <returns>Lista EscalaCalificacionDTO</returns>
        public List<EscalaCalificacionDTO> InsertarLista(List<EscalaCalificacionDTO> dtos, string usuario)
        {
            try
            {
                if (dtos != null && dtos.Count() > 0)
                {
                    List<EscalaCalificacion> entidades = new();
                    foreach (var item in dtos)
                    {
                        EscalaCalificacion entidad = new()
                        {
                            Nombre = item.Nombre,
                            Estado = true,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                        };
                        entidades.Add(entidad);
                    }
                    var respuesta = _unitOfWork.EscalaCalificacionRepository.Add(entidades);
                    _unitOfWork.Commit();
                    return _mapper.Map<List<EscalaCalificacionDTO>>(respuesta);
                }
                return new List<EscalaCalificacionDTO>();
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Gretel Canasa
        /// Fecha: 11/05/2023
        /// Version: 1.0
        /// <summary>
        /// Modifica un Material de Accion
        /// </summary>
        /// <param name="dto">Material de Accion</param>
        /// <param name="usuario">Usuario Modificacion</param>
        /// <returns>EscalaCalificacionDTO</returns>
        public EscalaCalificacionDTO Actualizar(EscalaCalificacionDTO dto, string usuario)
        {
            try
            {
                EscalaCalificacion escalaExistente = new();
                if (dto != null && dto.Id != 0)
                {
                    escalaExistente = _unitOfWork.EscalaCalificacionRepository.ObtenerPorId(dto.Id);
                    if (escalaExistente != null && escalaExistente.Id != 0)
                    {
                        escalaExistente.Nombre = dto.Nombre;
                        escalaExistente.FechaModificacion = DateTime.Now;
                        escalaExistente.UsuarioModificacion = usuario;
                    }
                    else
                    {
                        throw new BadRequestException("Entidad no existente");
                    }
                    //DETALLES
                    var listadoDetalleExistente = _unitOfWork.EscalaCalificacionDetalleRepository.ObtenerPorIdEscalaCalificacion(dto.Id);

                    //añade la lista de detalles
                    if (dto.EscalaCalificacionDetalles != null && dto.EscalaCalificacionDetalles.Count() > 0)
                    {
                        if (listadoDetalleExistente.Count() > 0)
                        {
                            var listadoEliminar = listadoDetalleExistente.Where(x => !dto.EscalaCalificacionDetalles.Any(z => z.Id == x.Id));
                            _unitOfWork.EscalaCalificacionDetalleRepository.Delete(listadoEliminar.Select(x => x.Id), usuario);
                            _unitOfWork.Commit();
                            listadoDetalleExistente.ToList().RemoveAll(x => listadoEliminar.Any(z => z.Id == x.Id));
                            listadoDetalleExistente = _unitOfWork.EscalaCalificacionDetalleRepository.ObtenerPorIdEscalaCalificacion(dto.Id);
                        }
                        escalaExistente.EscalaCalificacionDetalles = new List<EscalaCalificacionDetalle>();
                        foreach (var item in dto.EscalaCalificacionDetalles)
                        {
                            var detalleExistente = listadoDetalleExistente.FirstOrDefault(f => f.Id == item.Id);
                            if (item != null && item.Id != 0)
                            {
                                detalleExistente.Nombre = item.Nombre;
                                detalleExistente.Valor = item.Valor;
                                detalleExistente.UsuarioModificacion = usuario;
                                detalleExistente.FechaModificacion = DateTime.Now;
                                escalaExistente.EscalaCalificacionDetalles.Add(detalleExistente);
                            }
                            else
                            {
                                var nuevoDetalle = new EscalaCalificacionDetalle()
                                {
                                    Nombre = item.Nombre,
                                    Valor = item.Valor,
                                    Estado = true,
                                    UsuarioCreacion = usuario,
                                    UsuarioModificacion = usuario,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now
                                };
                                escalaExistente.EscalaCalificacionDetalles.Add(nuevoDetalle);
                            }
                        }
                    }
                    else
                    {
                        if (listadoDetalleExistente.Count() > 0)
                        {
                            _unitOfWork.EscalaCalificacionDetalleRepository.Delete(listadoDetalleExistente.Select(x => x.Id), usuario);
                            _unitOfWork.Commit();
                        }
                    }
                    var resultado = _unitOfWork.EscalaCalificacionRepository.Update(escalaExistente);
                    _unitOfWork.Commit();
                    dto.Nombre = resultado.Nombre;
                    dto.EscalaCalificacionDetalles = _mapper.Map<IEnumerable<EscalaCalificacionDetalleDTO>>(_unitOfWork.EscalaCalificacionDetalleRepository.ObtenerPorIdEscalaCalificacion(dto.Id));
                    return dto;
                }
                else
                    throw new BadRequestException("Entidad Nula o Id Entidad 0");
            }
            catch (Exception)
            {
                _unitOfWork.Dispose();
                throw;
            }
        }

        /// Autor: Gretel Canasa
        /// Fecha: 11/05/2023
        /// Version: 1.0
        /// <summary>
        /// Registra una lista de Materiales de Accion
        /// </summary>
        /// <param name="dtos">Lista de Materiales de Accion</param>
        /// <param name="usuario">Usuario de Registro</param>
        /// <returns>Lista EscalaCalificacionDTO</returns>
        public List<EscalaCalificacionDTO> ActualizarLista(List<EscalaCalificacionDTO> dtos, string usuario)
        {
            try
            {
                if (dtos != null && dtos.Count() > 0)
                {
                    List<EscalaCalificacion> entidades = new();
                    var ids = dtos.Select(x => x.Id).ToList();
                    var lista = _unitOfWork.EscalaCalificacionRepository.ObtenerPorIds(ids);

                    if (lista != null && lista.Count() > 0)
                    {
                        foreach (var item in dtos)
                        {
                            var entidad = lista.FirstOrDefault(x => x.Id == item.Id);
                            if (entidad != null && entidad.Id != 0)
                            {
                                entidad.Nombre = item.Nombre;
                                entidad.FechaModificacion = DateTime.Now;
                                entidad.UsuarioModificacion = usuario;
                                entidades.Add(entidad);
                            }
                        }
                        var respuesta = _unitOfWork.EscalaCalificacionRepository.Update(entidades);
                        _unitOfWork.Commit();
                        return _mapper.Map<List<EscalaCalificacionDTO>>(respuesta);
                    }
                }
                return new List<EscalaCalificacionDTO>();

            }
            catch (Exception)
            {
                _unitOfWork.Dispose();
                throw;
            }
        }
        /// Autor: Gretel Canasa
        /// Fecha: 11/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene Seccion Especifico asociada a un ProgramaGeneral con un Titulo especifico.
        /// </summary>
        /// <param name="idPGeneral">Id del Programa General</param>
        /// <returns> PGeneralDocumentoSeccionDTO </returns>
        public bool Eliminar(int id, string usuario)
        {
            try
            {
                var escala = _unitOfWork.EscalaCalificacionRepository.ObtenerPorId(id);
                if (escala != null && escala.Id != 0)
                {
                    var respuesta = _unitOfWork.EscalaCalificacionRepository.Delete(id, usuario);
                    var listadoDetalleExistente = _unitOfWork.EscalaCalificacionDetalleRepository.ObtenerPorIdEscalaCalificacion(id);
                    var repuestaDetalle = _unitOfWork.EscalaCalificacionDetalleRepository.Delete(listadoDetalleExistente.Select(s => s.Id), usuario);
                    _unitOfWork.Commit();
                    return respuesta;
                }
                else
                {
                    _unitOfWork.Dispose();
                    throw new BadRequestException($"No se encontro la entidad con el id {id}");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Gretel Canasa
        /// Fecha: 11/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene Seccion Especifico asociada a un ProgramaGeneral con un Titulo especifico.
        /// </summary>
        /// <param name="idPGeneral">Id del Programa General</param>
        /// <returns> PGeneralDocumentoSeccionDTO </returns>
        public bool EliminarLista(List<int> ids, string usuario)
        {
            try
            {
                if (ids.Count() > 0)
                {
                    var materialEstado = _unitOfWork.EscalaCalificacionRepository.ObtenerPorIds(ids);
                    if (materialEstado != null && materialEstado.Count() > 0)
                    {
                        var respuesta = _unitOfWork.EscalaCalificacionRepository.Delete(materialEstado.Select(x => x.Id).ToList(), usuario);
                        _unitOfWork.Commit();
                        return respuesta;
                    }
                    else
                    {
                        throw new BadRequestException("No se encontro las entidades");
                    }
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
