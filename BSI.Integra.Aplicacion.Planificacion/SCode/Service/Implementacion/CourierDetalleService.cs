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
    /// Service: CourierDetalleService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 27/07/2022
    /// <summary>
    /// Gestión general de T_CourierDetalle
    /// </summary>
    public class CourierDetalleService : ICourierDetalleService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public CourierDetalleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TCourierDetalle, CourierDetalle>(MemberList.None).ReverseMap();
                    cfg.CreateMap<CourierDetalle, TCourierDetalle>(MemberList.None).ReverseMap();
                    cfg.CreateMap<TCourierDetalle, CourierDetalleDTO>(MemberList.None).ReverseMap();
                    cfg.CreateMap<CourierDetalle, CourierDetalleDTO>(MemberList.None).ReverseMap();
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
        /// <returns> Lista CourierDetalleDTO </returns>
        public List<CourierDetalleDTO> ObtenerPorIdCourier(int idCourier)
        {
            try
            {
                var respuesta = _unitOfWork.CourierDetalleRepository.ObtenerPorIdCourier(idCourier);
                return _mapper.Map<List<CourierDetalleDTO>>(respuesta);
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
        public CourierDetalleDTO ObtenerPorId(int id)
        {
            try
            {
                var respuesta = _unitOfWork.CourierDetalleRepository.ObtenerPorId(id);
                if (respuesta != null && respuesta.Id != 0)
                {
                    return _mapper.Map<CourierDetalleDTO>(respuesta);
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
        /// <returns>CourierDetalleDTO</returns>
        public CourierDetalleDTO Insertar(CourierDetalleDTO dto, string usuario)
        {
            try
            {
                if (dto != null)
                {
                    CourierDetalle entidad = new()
                    {
                        IdCourier = dto.IdCourier,
                        IdPais = dto.IdPais,
                        IdCiudad = dto.IdCiudad,
                        Direccion = dto.Direccion,
                        Telefono = dto.Telefono,
                        TiempoEnvio = dto.TiempoEnvio,
                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                    };
                    var respuesta = _unitOfWork.CourierDetalleRepository.Add(entidad);
                    _unitOfWork.Commit();
                    return _mapper.Map<CourierDetalleDTO>(respuesta);
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
        /// Modifica un Material de Accion
        /// </summary>
        /// <param name="dto">Material de Accion</param>
        /// <param name="usuario">Usuario Modificacion</param>
        /// <returns>CourierDetalleDTO</returns>
        public CourierDetalleDTO Actualizar(CourierDetalleDTO dto, string usuario)
        {
            try
            {
                CourierDetalle entidad = new();
                if (dto != null)
                {
                    if (dto.Id != 0)
                    {
                        entidad = _unitOfWork.CourierDetalleRepository.ObtenerPorId(dto.Id);
                        if (entidad != null && entidad.Id != 0)
                        {
                            entidad.IdCourier = dto.IdCourier;
                            entidad.IdPais = dto.IdPais;
                            entidad.IdCiudad = dto.IdCiudad;
                            entidad.Direccion = dto.Direccion;
                            entidad.Telefono = dto.Telefono;
                            entidad.TiempoEnvio = dto.TiempoEnvio;
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
                var respuesta = _unitOfWork.CourierDetalleRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<CourierDetalleDTO>(respuesta);
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
                var materialAccion = _unitOfWork.CourierDetalleRepository.ObtenerPorId(id);
                if (materialAccion != null && materialAccion.Id != 0)
                {
                    var respuesta = _unitOfWork.CourierDetalleRepository.Delete(id, usuario);
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
                    var materialesAccion = _unitOfWork.CourierDetalleRepository.ObtenerPorIds(ids);
                    if (materialesAccion != null && materialesAccion.Count() > 0)
                    {
                        var respuesta = _unitOfWork.CourierDetalleRepository.Delete(materialesAccion.Select(x => x.Id).ToList(), usuario);
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

        public CourierDetalleDTO ObtenerCourierDetallePorIdCourierYIdCiudad(int idCourier, int idCiudad)
        {
            try
            {
                var t = _unitOfWork.CourierDetalleRepository.FirstBy(x => x.IdCourier == idCourier && x.IdCiudad == idCiudad && x.Estado == true);
                return _mapper.Map<CourierDetalleDTO>(t);
                
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
