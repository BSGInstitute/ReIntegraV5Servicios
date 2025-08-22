using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    /// Service: AreaCentroCostoSerevice
    /// Autor : Klebert Layme.
    /// Fecha: 25/04/2023
    /// <summary>
    /// Gestión general de T_AreaCc
    /// </summary>
    public class AreaCcService : IAreaCcService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public AreaCcService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TAreaCc, AreaCC>(MemberList.None).ReverseMap();
                    cfg.CreateMap<TAreaCc, AreaCcDTO>(MemberList.None).ReverseMap();
                    cfg.CreateMap<AreaCC, AreaCcDTO>(MemberList.None).ReverseMap();
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
        /// <returns> Lista MaterialAccionDTO </returns>
        public List<AreaCcDTO> Obtener()
        {
            try
            {
                var respuesta = _unitOfWork.AreaCentroCostoRepository.Obtener();
                return _mapper.Map<List<AreaCcDTO>>(respuesta);
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
        /// Obtiene por id AreaCentroCostoSerevice
        /// </summary>
        /// <param name="idPGeneral">Id del area centro de costo</param>

        /// <returns> AreaTrabajo </returns>
        public AreaCcDTO ObtenerPorId(int id)
        {
            try
            {
                var respuesta = _unitOfWork.AreaCentroCostoRepository.ObtenerPorId(id);
                if (respuesta != null && respuesta.Id != 0)
                {
                    return _mapper.Map<AreaCcDTO>(respuesta);
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
        /// Registra un nuevo Area centro de costo
        /// </summary>
        /// <param name="dto">Area Centro Costo</param>
        /// <param name="usuario">Usuario Registro</param>
        /// <returns>AreaCc</returns>
        public AreaCcDTO Insertar(AreaCcDTO dto, string usuario)
        {
            try
            {
                if (dto != null)
                {
                    AreaCC entidad = new()
                    {
                        Nombre = dto.Nombre,
                        Codigo = dto.Codigo,
                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                    };
                    var respuesta = _unitOfWork.AreaCentroCostoRepository.Add(entidad);
                    _unitOfWork.Commit();
                    return _mapper.Map<AreaCcDTO>(respuesta);
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
        /// Fecha: 26/04/2023
        /// Version: 1.0
        /// <summary>
        /// Modifica un Area centro costo
        /// </summary>
        /// <param name="dto">Are de centro costo</param>
        /// <param name="usuario">Usuario Modificacion</param>
        /// <returns>AreaTrabajo</returns>
        public AreaCcDTO Actualizar(AreaCcDTO dto, string usuario)
        {
            try
            {
                AreaCC entidad = new();
                if (dto != null)
                {
                    if (dto.Id != 0)
                    {
                        entidad = _unitOfWork.AreaCentroCostoRepository.ObtenerPorId(dto.Id);
                        if (entidad != null && entidad.Id != 0)
                        {
                            entidad.Nombre = dto.Nombre;
                            entidad.Codigo = dto.Codigo;
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
                var respuesta = _unitOfWork.AreaCentroCostoRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<AreaCcDTO>(respuesta);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// Autor: Klebert Layme
        /// Fecha: 26/04/2023
        /// Version: 1.0
        /// <summary>
        /// Elimina un Area centro costo
        /// </summary>
        /// <param name="idPGeneral">Id del Area centro costo</param>
        /// <returns> AreaCentroCostoDTO </returns>
        public bool Eliminar(int id, string usuario)
        {
            try
            {
                var areaCentroCostoServicio = _unitOfWork.AreaCentroCostoRepository.ObtenerPorId(id);
                if (areaCentroCostoServicio != null && areaCentroCostoServicio.Id != 0)
                {
                    var respuesta = _unitOfWork.AreaCentroCostoRepository.Delete(id, usuario);
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

    }
}
