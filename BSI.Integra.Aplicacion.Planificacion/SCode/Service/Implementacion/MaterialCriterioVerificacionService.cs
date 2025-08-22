using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion.Planificacion
{
    /// Service: MaterialCriterioVerificacionService
    /// Autor: Gretel Canasa
    /// Fecha: 15/04/2023
    /// <summary>
    /// Gestión general de T_MaterialCriterioVerificacion
    /// </summary>
    public class MaterialCriterioVerificacionService : IMaterialCriterioVerificacionService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public MaterialCriterioVerificacionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TMaterialCriterioVerificacion, MaterialCriterioVerificacion>(MemberList.None).ReverseMap();
                    cfg.CreateMap<TMaterialCriterioVerificacion, MaterialCriterioVerificacionDTO>(MemberList.None).ReverseMap();
                    cfg.CreateMap<MaterialCriterioVerificacion, MaterialCriterioVerificacionDTO>(MemberList.None).ReverseMap();
                }
            );
            _mapper = new Mapper(config);
        }
        /// Autor: Gretel Canasa
        /// Fecha: 25/04/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los materiales Criterio de Verificacion
        /// </summary>
        /// <returns> Lista MaterialCriterioVerificacionDTO </returns>
        public List<MaterialCriterioVerificacionDTO> Obtener()
        {
            try
            {
                var respuesta = _unitOfWork.MaterialCriterioVerificacionRepository.ObtenerTodo();
                return _mapper.Map<List<MaterialCriterioVerificacionDTO>>(respuesta);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Gretel Canasa
        /// Fecha: 25/04/2023
        /// Version: 1.0
        /// <summary>
        /// Obtener por el ID 
        public MaterialCriterioVerificacionDTO ObtenerPorId(int id)
        {
            try
            {
                var respuesta = _unitOfWork.MaterialCriterioVerificacionRepository.ObtenerPorId(id);
                if (respuesta != null && respuesta.Id != 0)
                {
                    return _mapper.Map<MaterialCriterioVerificacionDTO>(respuesta);
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
        /// Fecha: 25/04/2023
        /// Version: 1.0
        /// <summary>
        /// Registra un nuevo Material Criterio de Verificacion
        /// </summary>
        /// <param name="dto">Material Criterio de Verificacion</param>
        /// <param name="usuario">Usuario Registro</param>
        /// <returns>MaterialCriterioVerificacionDTO</returns>
        public MaterialCriterioVerificacionDTO Insertar(MaterialCriterioVerificacionDTO dto, string usuario)
        {
            try
            {
                if (dto != null)
                {
                    MaterialCriterioVerificacion entidad = new()
                    {
                        Nombre = dto.Nombre,
                        Descripcion = dto.Descripcion,
                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                    };
                    var respuesta = _unitOfWork.MaterialCriterioVerificacionRepository.Add(entidad);
                    _unitOfWork.Commit();
                    return _mapper.Map<MaterialCriterioVerificacionDTO>(respuesta);
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
        /// Fecha: 18/04/2023
        /// Version: 1.0
        /// <summary>
        /// Registra una lista de Criterios de Verificación
        /// </summary>
        /// <param name="dtos">Lista de Materiales Criterios de Verificación </param>
        /// <param name="usuario">Usuario de Registro</param>
        /// <returns>Lista MaterialCriterioVerificacionDTO</returns>
        public List<MaterialCriterioVerificacionDTO> InsertarLista(List<MaterialCriterioVerificacionDTO> dtos, string usuario)
        {
            try
            {
                if (dtos != null && dtos.Count() > 0)
                {
                    List<MaterialCriterioVerificacion> entidades = new();
                    foreach (var item in dtos)
                    {
                        MaterialCriterioVerificacion entidad = new()
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
                    var respuesta = _unitOfWork.MaterialCriterioVerificacionRepository.Add(entidades);
                    _unitOfWork.Commit();
                    return _mapper.Map<List<MaterialCriterioVerificacionDTO>>(respuesta);
                }
                return new List<MaterialCriterioVerificacionDTO>();
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Gretel Canasa
        /// Fecha: 18/04/2023
        /// Version: 1.0
        /// <summary>
        /// Modifica un Material Criterios de Verificacion
        /// </summary>
        /// <param name="dto">Material Criterios de Verificacion</param>
        /// <param name="usuario">Usuario Modificacion</param>
        /// <returns>MaterialCriterioVerificacionDTO</returns>
        public MaterialCriterioVerificacionDTO Actualizar(MaterialCriterioVerificacionDTO dto, string usuario)
        {
            try
            {
                MaterialCriterioVerificacion entidad = new();
                if (dto != null)
                {
                    if (dto.Id != 0)
                    {
                        entidad = _unitOfWork.MaterialCriterioVerificacionRepository.ObtenerPorId(dto.Id);
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
                var respuesta = _unitOfWork.MaterialCriterioVerificacionRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<MaterialCriterioVerificacionDTO>(respuesta);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Gretel Canasa
        /// Fecha: 18/04/2023
        /// Version: 1.0
        /// <summary>
        /// Registra una lista de Material Criterios de Verificacion
        /// </summary>
        /// <param name="dtos">Lista de Material Criterios de Verificacion</param>
        /// <param name="usuario">Usuario de Registro</param>
        /// <returns>Lista MaterialCriterioVerificacionDTO</returns>
        public List<MaterialCriterioVerificacionDTO> ActualizarLista(List<MaterialCriterioVerificacionDTO> dtos, string usuario)
        {
            try
            {
                if (dtos != null && dtos.Count() > 0)
                {
                    List<MaterialCriterioVerificacion> entidades = new();
                    var ids = dtos.Select(x => x.Id).ToList();
                    var lista = _unitOfWork.MaterialCriterioVerificacionRepository.ObtenerPorIds(ids);

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
                        var respuesta = _unitOfWork.MaterialCriterioVerificacionRepository.Update(entidades);
                        _unitOfWork.Commit();
                        return _mapper.Map<List<MaterialCriterioVerificacionDTO>>(respuesta);
                    }
                }
                return new List<MaterialCriterioVerificacionDTO>();

            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Gretel Canasa
        /// Fecha: 18/04/2023
        /// Version: 1.0
        /// <summary>
        public bool Eliminar(int id, string usuario)
        {
            try
            {
                var materialCriterioVerificacion = _unitOfWork.MaterialCriterioVerificacionRepository.ObtenerPorId(id);
                if (materialCriterioVerificacion != null && materialCriterioVerificacion.Id != 0)
                {
                    var respuesta = _unitOfWork.MaterialCriterioVerificacionRepository.Delete(id, usuario);
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
        /// Fecha: 18/04/2023
        /// Version: 1.0
        /// <summary>

        public bool EliminarLista(List<int> ids, string usuario)
        {
            try
            {
                if (ids.Count() > 0)
                {
                    var materialCriterioVerificacion = _unitOfWork.MaterialCriterioVerificacionRepository.ObtenerPorIds(ids);
                    if (materialCriterioVerificacion != null && materialCriterioVerificacion.Count() > 0)
                    {
                        var respuesta = _unitOfWork.MaterialCriterioVerificacionRepository.Delete(materialCriterioVerificacion.Select(x => x.Id).ToList(), usuario);
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
