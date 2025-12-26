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
    /// Autor: Lolo Zaa
    /// Fecha: 26/12/2025
    /// <summary>
    /// Implementación del servicio para el manejo de docentes postulantes
    /// </summary>
    public class DocentePostulanteService : IDocentePostulanteService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public DocentePostulanteService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TDocentePostulante, DocentePostulante>(MemberList.None).ReverseMap();
                cfg.CreateMap<DocentePostulante, TDocentePostulante>(MemberList.None).ReverseMap();
                cfg.CreateMap<TDocentePostulante, DocentePostulanteDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<DocentePostulante, DocentePostulanteDTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        public List<DocentePostulanteDTO> Obtener()
        {
            try
            {
                var respuesta = _unitOfWork.DocentePostulanteRepository.ObtenerDocentePostulante();
                return _mapper.Map<List<DocentePostulanteDTO>>(respuesta);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DocentePostulanteDTO ObtenerPorId(int id)
        {
            try
            {
                var respuesta = _unitOfWork.DocentePostulanteRepository.ObtenerPorId(id);
                if (respuesta != null && respuesta.Id != 0)
                {
                    return _mapper.Map<DocentePostulanteDTO>(respuesta);
                }
                else
                {
                    throw new BadRequestException($"No existe el docente postulante con el id {id}");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DocentePostulanteDTO Insertar(DocentePostulanteDTO dto, string usuario)
        {
            try
            {
                if (dto != null)
                {
                    DocentePostulante entidad = new()
                    {
                        Nombre1 = dto.Nombre1,
                        Nombre2 = dto.Nombre2,
                        ApellidoPaterno = dto.ApellidoPaterno,
                        ApellidoMaterno = dto.ApellidoMaterno,
                        NumeroDocumento = dto.NumeroDocumento,
                        FechaNacimiento = dto.FechaNacimiento,
                        Telefono = dto.Telefono,
                        Celular = dto.Celular,
                        Correo = dto.Correo,
                        IdCiudad = dto.IdCiudad,
                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                    };

                    var respuesta = _unitOfWork.DocentePostulanteRepository.Add(entidad);
                    _unitOfWork.Commit();
                    return _mapper.Map<DocentePostulanteDTO>(respuesta);
                }
                else
                    throw new BadRequestException("Entidad Nula");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DocentePostulanteDTO Actualizar(DocentePostulanteDTO dto, string usuario)
        {
            try
            {
                DocentePostulante entidad = new();
                if (dto != null && dto.Id != 0)
                {
                    entidad = _unitOfWork.DocentePostulanteRepository.ObtenerPorId(dto.Id);
                    if (entidad != null && entidad.Id != 0)
                    {
                        entidad.Nombre1 = dto.Nombre1;
                        entidad.Nombre2 = dto.Nombre2;
                        entidad.ApellidoPaterno = dto.ApellidoPaterno;
                        entidad.ApellidoMaterno = dto.ApellidoMaterno;
                        entidad.NumeroDocumento = dto.NumeroDocumento;
                        entidad.FechaNacimiento = dto.FechaNacimiento;
                        entidad.Telefono = dto.Telefono;
                        entidad.Celular = dto.Celular;
                        entidad.Correo = dto.Correo;
                        entidad.IdCiudad = dto.IdCiudad;
                        entidad.FechaModificacion = DateTime.Now;
                        entidad.UsuarioModificacion = usuario;
                    }
                    else
                        throw new BadRequestException("Docente postulante no encontrado");
                }
                else
                    throw new BadRequestException("Id Entidad 0");

                var respuesta = _unitOfWork.DocentePostulanteRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<DocentePostulanteDTO>(respuesta);
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
                var docentePostulante = _unitOfWork.DocentePostulanteRepository.ObtenerPorId(id);
                if (docentePostulante != null && docentePostulante.Id != 0)
                {
                    var respuesta = _unitOfWork.DocentePostulanteRepository.Delete(id, usuario);
                    _unitOfWork.Commit();
                    return respuesta;
                }
                else
                {
                    throw new BadRequestException($"No se encontró el docente postulante con el id {id}");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
