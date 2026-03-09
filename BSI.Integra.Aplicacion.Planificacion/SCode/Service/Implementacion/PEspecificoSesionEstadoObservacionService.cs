using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
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
    public class PEspecificoSesionEstadoObservacionService : IPEspecificoSesionEstadoObservacionService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public PEspecificoSesionEstadoObservacionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPespecificoSesionEstadoObservacion, PEspecificoSesionEstadoObservacion>(MemberList.None).ReverseMap();
                cfg.CreateMap<PEspecificoSesionEstadoObservacion, PEspecificoSesionEstadoObservacionDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<PEspecificoSesionEstadoObservacionDTO, PEspecificoSesionEstadoObservacion>(MemberList.None).ReverseMap();
                cfg.CreateMap<TPespecificoSesionEstadoObservacion, PEspecificoSesionEstadoObservacionDTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        /// Autor: Villanueva Torres Marco Jose
        /// Fecha: 15/04/2024
        /// <summary>
        /// Tipo Formacion Service
        /// </summary>
        /// <returns> Lista CategoriaPreguntaDTO </returns>
        public IEnumerable<PEspecificoSesionEstadoObservacionDTO> Obtener()
        {
            return _unitOfWork.PEspecificoSesionEstadoObservacionRepository.Obtener();
        }

        /// Autor: Villanueva Torres Marco Jose
        /// Fecha: 15/04/2024
        /// Version: 1.0
        /// <summary>
        /// Registra un nuevo PEspecificoSesionEstadoObservacion
        /// </summary>
        /// <param name="dto">PEspecificoSesionEstadoObservacionDTO</param>
        /// <param name="usuario">Usuario Registro</param>
        /// <returns>PEspecificoSesionEstadoObservacionDTO</returns>
        public PEspecificoSesionEstadoObservacionDTO Insertar(PEspecificoSesionEstadoObservacionDTO dto, string usuario)
        {
            if (dto == null)
                throw new BadRequestException("Entidad nula");

            if (string.IsNullOrWhiteSpace(dto.Descripcion))
                throw new BadRequestException("La descripción es obligatoria");

            if (dto.Observaciones == null || !dto.Observaciones.Any())
                throw new BadRequestException("Debe enviar al menos una observación");

            var fechaActual = DateTime.Now;

            // 1. Insertar cabecera
            PEspecificoSesionEstadoObservacion cabecera = new()
            {
                Descripcion = dto.Descripcion,
                IdPEspecificoSesionEstado = dto.IdPEspecificoSesionEstado,
                Estado = true,
                UsuarioCreacion = usuario,
                UsuarioModificacion = usuario,
                FechaCreacion = fechaActual,
                FechaModificacion = fechaActual
            };

            var cabeceraInsertada = _unitOfWork.PEspecificoSesionEstadoObservacionRepository.Add(cabecera);
            _unitOfWork.Commit();

            // 2. Insertar detalles
            var detallesInsertados = new List<PEspecificoSesionEstadoObservacionDetalle>();

            foreach (var item in dto.Observaciones.OrderBy(x => x.Orden))
            {
                PEspecificoSesionEstadoObservacionDetalle detalle = new()
                {
                    Nombre = item.Contenido,
                    IdPEspecificoSesionEstadoObservacion = cabeceraInsertada.Id,
                    Orden = item.Orden,
                    Estado = true,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario,
                    FechaCreacion = fechaActual,
                    FechaModificacion = fechaActual
                };

                var detalleInsertado = _unitOfWork.PEspecificoSesionEstadoObservacionDetalleRepository.Add(detalle);
                detallesInsertados.Add(detalleInsertado);
            }

            _unitOfWork.Commit();

            // 3. Retornar respuesta
            var resultado = new PEspecificoSesionEstadoObservacionDTO
            {
                Id = cabeceraInsertada.Id,
                Descripcion = cabeceraInsertada.Descripcion,
                IdPEspecificoSesionEstado = cabeceraInsertada.IdPEspecificoSesionEstado,
                Observaciones = detallesInsertados
                    .OrderBy(x => x.Orden)
                    .Select(x => new PEspecificoSesionEstadoObservacionDetalleDTO
                    {
                        Id = x.Id,
                        Contenido = x.Nombre,
                        Orden = x.Orden
                    })
                    .ToList()
            };

            return resultado;
        }
        /// Metodo Actualizar
        /// Autor: Marco Jose Villanueva Torres
        /// Fecha: 15/04/2024
        /// Version: 1.0
        /// <summary>
        /// Realiza una inserción o actualizacion basica a la tabla y sus detalles
        /// </summary>   
        /// <param name="CategoriaPreguntaDTO"> parametros de la nueva PEspecificoSesionEstadoObservacionDTO y sus detalles </param>

        public PEspecificoSesionEstadoObservacionDTO Actualizar(PEspecificoSesionEstadoObservacionDTO dto, string usuario)
        {
            try
            {
                PEspecificoSesionEstadoObservacion? entidad = new();
                if (dto != null)
                {
                    if (dto.Id != 0)
                    {
                        entidad = _unitOfWork.PEspecificoSesionEstadoObservacionRepository.ObtenerPorId(dto.Id);
                        if (entidad != null && entidad.Id != 0)
                        {
                            entidad.Nombre = dto.Nombre;
                            entidad.IdPEspecificoSesionEstado = dto.IdPEspecificoSesionEstado;
                            entidad.UsuarioModificacion = usuario;
                            entidad.FechaModificacion = DateTime.Now;
                            var respuesta = _unitOfWork.PEspecificoSesionEstadoObservacionRepository.Update(entidad);
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
                var entidad = _unitOfWork.PEspecificoSesionEstadoObservacionRepository.ObtenerPorId(id);
                if (entidad != null && entidad.Id != 0)
                {
                    var respuesta = _unitOfWork.PEspecificoSesionEstadoObservacionRepository.Delete(id, usuario);

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
