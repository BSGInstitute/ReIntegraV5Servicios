using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    public class PEspecificoSesionEstadoService : IPEspecificoSesionEstadoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public PEspecificoSesionEstadoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPespecificoSesionEstado, PEspecificoSesionEstado>(MemberList.None).ReverseMap();
                cfg.CreateMap<PEspecificoSesionEstado, PEspecificoSesionEstadoDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<PEspecificoSesionEstadoDTO, PEspecificoSesionEstado>(MemberList.None).ReverseMap();
                cfg.CreateMap<TPespecificoSesionEstado, PEspecificoSesionEstadoDTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        /// Autor: Villanueva Torres Marco Jose
        /// Fecha: 15/04/2024
        /// <summary>
        /// Tipo Formacion Service
        /// </summary>
        /// <returns> Lista CategoriaPreguntaDTO </returns>
        public IEnumerable<PEspecificoSesionEstadoDTO> Obtener()
        {
            return _unitOfWork.PEspecificoSesionEstadoRepository.Obtener();
        }

        /// Autor: Villanueva Torres Marco Jose
        /// Fecha: 15/04/2024
        /// Version: 1.0
        /// <summary>
        /// Registra un nuevo PEspecificoSesionEstado
        /// </summary>
        /// <param name="dto">PEspecificoSesionEstadoDTO</param>
        /// <param name="usuario">Usuario Registro</param>
        /// <returns>PEspecificoSesionEstadoDTO</returns>
        public PEspecificoSesionEstadoDTO Insertar(PEspecificoSesionEstadoDTO dto, string usuario)
        {
            try
            {
                if (dto != null)
                {
                    PEspecificoSesionEstado entidad = new()
                    {
                        Nombre = dto.Nombre,
                        Estado = true,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                    };
                    var respuesta = _unitOfWork.PEspecificoSesionEstadoRepository.Add(entidad);
                    _unitOfWork.Commit();
                    entidad.Id = respuesta.Id;
                    var resultado = _mapper.Map<PEspecificoSesionEstadoDTO>(respuesta);


                    return resultado;
                }
                else
                    throw new BadRequestException("Entidad Nula");
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Metodo Actualizar
        /// Autor: Marco Jose Villanueva Torres
        /// Fecha: 15/04/2024
        /// Version: 1.0
        /// <summary>
        /// Realiza una inserción o actualizacion basica a la tabla y sus detalles
        /// </summary>   
        /// <param name="CategoriaPreguntaDTO"> parametros de la nueva PEspecificoSesionEstadoDTO y sus detalles </param>

        public PEspecificoSesionEstadoDTO Actualizar(PEspecificoSesionEstadoDTO dto, string usuario)
        {
            try
            {
                PEspecificoSesionEstado? entidad = new();
                if (dto != null)
                {
                    if (dto.Id != 0)
                    {
                        entidad = _unitOfWork.PEspecificoSesionEstadoRepository.ObtenerPorId(dto.Id);
                        if (entidad != null && entidad.Id != 0)
                        {
                            entidad.Nombre = dto.Nombre;
                            entidad.UsuarioModificacion = usuario;
                            entidad.FechaModificacion = DateTime.Now;
                            var respuesta = _unitOfWork.PEspecificoSesionEstadoRepository.Update(entidad);
                            _unitOfWork.Commit();


                            return dto;
                        }
                        else
                            throw new BadRequestException("Entidad no encontrada");
                    }
                    else
                        throw new BadRequestException("Id Entidad 0");
                }
                else
                    throw new BadRequestException("Entidad Nula");
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// Metodo Eliminar.
        /// Autor: Marco Jose Villanueva Torres
        /// Fecha: 15/04/2024
        /// Version: 1.0
        /// <summary>
        /// Realiza una eliminacion logica por el Primary Key
        /// </summary>   
        /// <param name="id"> (PK) </param>
        public bool Eliminar(int id, string usuario)
        {
            try
            {
                if (id == 0)
                {
                    throw new BadRequestException($"Id 0 no valido");
                }
                var entidad = _unitOfWork.PEspecificoSesionEstadoRepository.ObtenerPorId(id);
                if (entidad != null && entidad.Id != 0)
                {
                    var respuesta = _unitOfWork.PEspecificoSesionEstadoRepository.Delete(id, usuario);

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

        public bool ActualizarEstadoCurso(EstadoCursoDTO dto , string usuario)
        {
            _unitOfWork.PEspecificoSesionEstadoRepository.ActualizarEstadoCurso(dto,usuario);
            return true;
        }
        public bool ActualizarEstadoObservacion(EstadoCursoObservacionDTO dto, string usuario)
        {
            _unitOfWork.PEspecificoSesionEstadoRepository.ActualizarEstadoObservacion(dto, usuario);
            return true;
        }

    }
}
