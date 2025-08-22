using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using OfficeOpenXml.Drawing.Chart;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion.Planificacion
{
    /// Service: CourierService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 27/07/2022
    /// <summary>
    /// Gestión general de T_Courier
    /// </summary>
    public class CourierService : ICourierService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public CourierService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TCourier, Courier>(MemberList.None).ReverseMap();
                    cfg.CreateMap<Courier, TCourier>(MemberList.None).ReverseMap();
                    cfg.CreateMap<TCourier, CourierDTO>(MemberList.None).ReverseMap();
                    cfg.CreateMap<Courier, CourierDTO>(MemberList.None).ReverseMap();
                }
            );
            _mapper = new Mapper(config);
        }
        /// Autor: Gretel Canasa
        /// Fecha: 18/04/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los materiales de accion
        /// </summary>
        /// <returns> Lista CourierDTO </returns>
        public List<CourierDTO> Obtener()
        {
            try
            {
                var respuesta = _unitOfWork.CourierRepository.ObtenerCourier();
                return _mapper.Map<List<CourierDTO>>(respuesta);
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
        /// Obtiene Seccion Especifico asociada a un ProgramaGeneral con un Titulo especifico.
        /// </summary>
        /// <param name="idPGeneral">Id del Programa General</param>
        /// <param name="tituloSeccion">Titulo de la Seccion</param>
        /// <returns> PGeneralDocumentoSeccionDTO </returns>
        public CourierDTO ObtenerPorId(int id)
        {
            try
            {
                var respuesta = _unitOfWork.CourierRepository.ObtenerPorId(id);
                if (respuesta != null && respuesta.Id != 0)
                {
                    return _mapper.Map<CourierDTO>(respuesta);
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
        /// Fecha: 18/04/2023
        /// Version: 1.0
        /// <summary>
        /// Registra un nuevo Material de Accion
        /// </summary>
        /// <param name="dto">Material de Accion</param>
        /// <param name="usuario">Usuario Registro</param>
        /// <returns>CourierDTO</returns>
        public CourierDTO Insertar(CourierDTO dto, string usuario)
        {
            try
            {
                if (dto != null)
                {
                    Courier entidad = new()
                    {
                        Nombre = dto.Nombre,
                        IdPais = dto.IdPais,
                        IdCiudad = dto.IdCiudad,
                        Direccion = dto.Direccion,
                        Telefono = dto.Telefono,
                        Url = dto.Url,
                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                    };
                    var respuesta = _unitOfWork.CourierRepository.Add(entidad);
                    _unitOfWork.Commit();
                    return _mapper.Map<CourierDTO>(respuesta);
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
        /// Registra una lista de Materiales de Accion
        /// </summary>
        /// <param name="dtos">Lista de Materiales de Accion</param>
        /// <param name="usuario">Usuario de Registro</param>
        /// <returns>Lista CourierDTO</returns>
        public List<CourierDTO> InsertarLista(List<CourierDTO> dtos, string usuario)
        {
            try
            {
                if (dtos != null && dtos.Count() > 0)
                {
                    List<Courier> entidades = new();
                    foreach (var item in dtos)
                    {
                        Courier entidad = new()
                        {
                            Nombre = item.Nombre,
                            IdPais = item.IdPais,
                            IdCiudad = item.IdCiudad,
                            Direccion = item.Direccion,
                            Telefono = item.Telefono,
                            Url = item.Url,
                            Estado = true,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                        };
                        entidades.Add(entidad);
                    }
                    var respuesta = _unitOfWork.CourierRepository.Add(entidades);
                    _unitOfWork.Commit();
                    return _mapper.Map<List<CourierDTO>>(respuesta);
                }
                return new List<CourierDTO>();
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
        /// Modifica un Material de Accion
        /// </summary>
        /// <param name="dto">Material de Accion</param>
        /// <param name="usuario">Usuario Modificacion</param>
        /// <returns>CourierDTO</returns>
        public CourierDTO Actualizar(CourierDTO dto, string usuario)
        {
            try
            {
                Courier entidad = new();
                if (dto != null)
                {
                    if (dto.Id != 0)
                    {
                        entidad = _unitOfWork.CourierRepository.ObtenerPorId(dto.Id);
                        if (entidad != null && entidad.Id != 0)
                        {
                            entidad.Nombre = dto.Nombre;
                            entidad.IdPais = dto.IdPais;
                            entidad.IdCiudad = dto.IdCiudad;
                            entidad.Direccion = dto.Direccion;
                            entidad.Telefono = dto.Telefono;
                            entidad.Url = dto.Url;
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
                var respuesta = _unitOfWork.CourierRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<CourierDTO>(respuesta);
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
        /// Registra una lista de Materiales de Accion
        /// </summary>
        /// <param name="dtos">Lista de Materiales de Accion</param>
        /// <param name="usuario">Usuario de Registro</param>
        /// <returns>Lista CourierDTO</returns>
        public List<CourierDTO> ActualizarLista(List<CourierDTO> dtos, string usuario)
        {
            try
            {
                if (dtos != null && dtos.Count() > 0)
                {
                    List<Courier> entidades = new();
                    var ids = dtos.Select(x => x.Id).ToList();
                    var lista = _unitOfWork.CourierRepository.ObtenerPorIds(ids);

                    if (lista != null && lista.Count() > 0)
                    {
                        foreach (var item in dtos)
                        {
                            var entidad = lista.FirstOrDefault(x => x.Id == item.Id);
                            if (entidad != null && entidad.Id != 0)
                            {
                                entidad.Nombre = item.Nombre;
                                entidad.IdPais = item.IdPais;
                                entidad.IdCiudad = item.IdCiudad;
                                entidad.Direccion = item.Direccion;
                                entidad.Telefono = item.Telefono;
                                entidad.Url = item.Url;
                                entidad.FechaModificacion = DateTime.Now;
                                entidad.UsuarioModificacion = usuario;
                                entidades.Add(entidad);
                            }
                        }
                        var respuesta = _unitOfWork.CourierRepository.Update(entidades);
                        _unitOfWork.Commit();
                        return _mapper.Map<List<CourierDTO>>(respuesta);
                    }
                }
                return new List<CourierDTO>();

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
        /// Obtiene Seccion Especifico asociada a un ProgramaGeneral con un Titulo especifico.
        /// </summary>
        /// <param name="idPGeneral">Id del Programa General</param>
        /// <returns> PGeneralDocumentoSeccionDTO </returns>
        public bool Eliminar(int id, string usuario)
        {
            try
            {
                var materialAccion = _unitOfWork.CourierRepository.ObtenerPorId(id);
                if (materialAccion != null && materialAccion.Id != 0)
                {
                    var respuesta = _unitOfWork.CourierRepository.Delete(id, usuario);
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
                    var materialesAccion = _unitOfWork.CourierRepository.ObtenerPorIds(ids);
                    if (materialesAccion != null && materialesAccion.Count() > 0)
                    {
                        var respuesta = _unitOfWork.CourierRepository.Delete(materialesAccion.Select(x => x.Id).ToList(), usuario);
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
