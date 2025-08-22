using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion.Planificacion
{
    /// Service: MaterialEstadoService
    /// Autor: Gretel Canasa
    /// Fecha: 11/05/2023
    /// <summary>
    /// Gestión general de T_MaterialEstado
    /// </summary>
    public class MaterialEstadoService : IMaterialEstadoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public MaterialEstadoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TMaterialEstado, MaterialEstado>(MemberList.None).ReverseMap();
                    cfg.CreateMap<TMaterialEstado, MaterialEstadoDTO>(MemberList.None).ReverseMap();
                    cfg.CreateMap<MaterialEstado, MaterialEstadoDTO>(MemberList.None).ReverseMap();
                }
            );
            _mapper = new Mapper(config);
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 18/04/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los materiales de accion
        /// </summary>
        /// <returns> Lista MaterialEstadoDTO </returns>
        public List<MaterialEstadoDTO> Obtener()
        {
            try
            {
                var respuesta = _unitOfWork.MaterialEstadoRepository.ObtenerTodo();
                return _mapper.Map<List<MaterialEstadoDTO>>(respuesta);
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
        public MaterialEstadoDTO ObtenerPorId(int id)
        {
            try
            {
                var respuesta = _unitOfWork.MaterialEstadoRepository.ObtenerPorId(id);
                if (respuesta != null && respuesta.Id != 0)
                {
                    return _mapper.Map<MaterialEstadoDTO>(respuesta);
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
        /// <returns>MaterialEstadoDTO</returns>
        public MaterialEstadoDTO Insertar(MaterialEstadoDTO dto, string usuario)
        {
            try
            {
                if (dto != null)
                {
                    MaterialEstado entidad = new()
                    {
                        Nombre = dto.Nombre,
                        Descripcion = dto.Descripcion,
                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                    };
                    var respuesta = _unitOfWork.MaterialEstadoRepository.Add(entidad);
                    _unitOfWork.Commit();
                    return _mapper.Map<MaterialEstadoDTO>(respuesta);
                }
                else
                    throw new BadRequestException("Entidad Nula");
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
        /// Registra una lista de Materiales de Accion
        /// </summary>
        /// <param name="dtos">Lista de Materiales de Accion</param>
        /// <param name="usuario">Usuario de Registro</param>
        /// <returns>Lista MaterialEstadoDTO</returns>
        public List<MaterialEstadoDTO> InsertarLista(List<MaterialEstadoDTO> dtos, string usuario)
        {
            try
            {
                if (dtos != null && dtos.Count() > 0)
                {
                    List<MaterialEstado> entidades = new();
                    foreach (var item in dtos)
                    {
                        MaterialEstado entidad = new()
                        {
                            Nombre = item.Nombre,
                            Descripcion = item.Descripcion,
                            Estado = true,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                        };
                        entidades.Add(entidad);
                    }
                    var respuesta = _unitOfWork.MaterialEstadoRepository.Add(entidades);
                    _unitOfWork.Commit();
                    return _mapper.Map<List<MaterialEstadoDTO>>(respuesta);
                }
                return new List<MaterialEstadoDTO>();
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
        /// <returns>MaterialEstadoDTO</returns>
        public MaterialEstadoDTO Actualizar(MaterialEstadoDTO dto, string usuario)
        {
            try
            {
                MaterialEstado entidad = new();
                if (dto != null)
                {
                    if (dto.Id != 0)
                    {
                        entidad = _unitOfWork.MaterialEstadoRepository.ObtenerPorId(dto.Id);
                        if (entidad != null && entidad.Id != 0)
                        {
                            entidad.Nombre = dto.Nombre;
                            entidad.Descripcion = dto.Descripcion;
                            entidad.FechaModificacion = DateTime.Now;
                            entidad.UsuarioModificacion = usuario;
                        }
                        else
                            throw new BadRequestException("Entidad no encontrada");
                    }
                    else
                        throw new BadRequestException("Id Entidad 0");
                }
                else
                    throw new BadRequestException("Entidad Nula");
                var respuesta = _unitOfWork.MaterialEstadoRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<MaterialEstadoDTO>(respuesta);
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
        /// Registra una lista de Materiales de Accion
        /// </summary>
        /// <param name="dtos">Lista de Materiales de Accion</param>
        /// <param name="usuario">Usuario de Registro</param>
        /// <returns>Lista MaterialEstadoDTO</returns>
        public List<MaterialEstadoDTO> ActualizarLista(List<MaterialEstadoDTO> dtos, string usuario)
        {
            try
            {
                if (dtos != null && dtos.Count() > 0)
                {
                    List<MaterialEstado> entidades = new();
                    var ids = dtos.Select(x => x.Id).ToList();
                    var lista = _unitOfWork.MaterialEstadoRepository.ObtenerPorIds(ids);

                    if (lista != null && lista.Count() > 0)
                    {
                        foreach (var item in dtos)
                        {
                            var entidad = lista.FirstOrDefault(x => x.Id == item.Id);
                            if (entidad != null && entidad.Id != 0)
                            {
                                entidad.Nombre = item.Nombre;
                                entidad.Descripcion = item.Descripcion;
                                entidad.FechaModificacion = DateTime.Now;
                                entidad.UsuarioModificacion = usuario;
                                entidades.Add(entidad);
                            }
                        }
                        var respuesta = _unitOfWork.MaterialEstadoRepository.Update(entidades);
                        _unitOfWork.Commit();
                        return _mapper.Map<List<MaterialEstadoDTO>>(respuesta);
                    }
                }
                return new List<MaterialEstadoDTO>();

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
        public bool Eliminar(int id, string usuario)
        {
            try
            {
                var materialEstado = _unitOfWork.MaterialEstadoRepository.ObtenerPorId(id);
                if (materialEstado != null && materialEstado.Id != 0)
                {
                    var respuesta = _unitOfWork.MaterialEstadoRepository.Delete(id, usuario);
                    _unitOfWork.Commit();
                    return respuesta;
                }
                else
                {
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
                    var materialEstado = _unitOfWork.MaterialEstadoRepository.ObtenerPorIds(ids);
                    if (materialEstado != null && materialEstado.Count() > 0)
                    {
                        var respuesta = _unitOfWork.MaterialEstadoRepository.Delete(materialEstado.Select(x => x.Id).ToList(), usuario);
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
