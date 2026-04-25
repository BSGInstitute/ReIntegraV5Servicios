using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.SCode.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;

namespace BSI.Integra.Aplicacion.Planificacion.SCode.Service.Implementacion
{
    /// <summary>
    /// Implementación del servicio para el manejo de fases de gestión de contactos
    /// </summary>
    public class FaseGestionContactoService : IFaseGestionContactoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public FaseGestionContactoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TFaseGestionContacto, FaseGestionContacto>(MemberList.None).ReverseMap();
                cfg.CreateMap<FaseGestionContacto, TFaseGestionContacto>(MemberList.None).ReverseMap();
                cfg.CreateMap<TFaseGestionContacto, FaseGestionContactoDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<FaseGestionContacto, FaseGestionContactoDTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        public List<FaseGestionContactoDTO> Obtener()
        {
            try
            {
                var respuesta = _unitOfWork.FaseGestionContactoRepository.ObtenerFaseGestionContacto();
                return _mapper.Map<List<FaseGestionContactoDTO>>(respuesta);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public FaseGestionContactoDTO ObtenerPorId(int id)
        {
            try
            {
                var respuesta = _unitOfWork.FaseGestionContactoRepository.ObtenerPorId(id);
                if (respuesta != null && respuesta.Id != 0)
                {
                    return _mapper.Map<FaseGestionContactoDTO>(respuesta);
                }
                else
                {
                    throw new BadRequestException($"No existe la fase de gestión de contacto con el id {id}");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public FaseGestionContactoDTO Insertar(FaseGestionContactoDTO dto, string usuario)
        {
            try
            {
                if (dto != null)
                {
                    FaseGestionContacto entidad = new()
                    {
                        Codigo = dto.Codigo,
                        Nombre = dto.Nombre,
                        Descripcion = dto.Descripcion,
                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                    };

                    var respuesta = _unitOfWork.FaseGestionContactoRepository.Add(entidad);
                    _unitOfWork.Commit();
                    return _mapper.Map<FaseGestionContactoDTO>(respuesta);
                }
                else
                    throw new BadRequestException("Entidad Nula");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public FaseGestionContactoDTO Actualizar(FaseGestionContactoDTO dto, string usuario)
        {
            try
            {
                FaseGestionContacto entidad = new();
                if (dto != null && dto.Id != 0)
                {
                    entidad = _unitOfWork.FaseGestionContactoRepository.ObtenerPorId(dto.Id);
                    if (entidad != null && entidad.Id != 0)
                    {
                        entidad.Codigo = dto.Codigo;
                        entidad.Nombre = dto.Nombre;
                        entidad.Descripcion = dto.Descripcion;
                        entidad.FechaModificacion = DateTime.Now;
                        entidad.UsuarioModificacion = usuario;
                    }
                    else
                        throw new BadRequestException("Fase de gestión de contacto no encontrada");
                }
                else
                    throw new BadRequestException("Id Entidad 0");

                var respuesta = _unitOfWork.FaseGestionContactoRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<FaseGestionContactoDTO>(respuesta);
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
                var faseGestionContacto = _unitOfWork.FaseGestionContactoRepository.ObtenerPorId(id);
                if (faseGestionContacto != null && faseGestionContacto.Id != 0)
                {
                    var respuesta = _unitOfWork.FaseGestionContactoRepository.Delete(id, usuario);
                    _unitOfWork.Commit();
                    return respuesta;
                }
                else
                {
                    throw new BadRequestException($"No se encontró la fase de gestión de contacto con el id {id}");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
