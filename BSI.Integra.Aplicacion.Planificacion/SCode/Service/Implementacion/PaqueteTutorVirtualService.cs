using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    public class PaqueteTutorVirtualService : IPaqueteTutorVirtualService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public PaqueteTutorVirtualService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPaqueteTutorVirtual, PaqueteTutorVirtual>(MemberList.None).ReverseMap();
                cfg.CreateMap<PaqueteTutorVirtual, PaqueteTutorVirtualDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<PaqueteTutorVirtualDTO, PaqueteTutorVirtual>(MemberList.None).ReverseMap();
                cfg.CreateMap<TPaqueteTutorVirtual, PaqueteTutorVirtualDTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        /// Autor: Villanueva Torres Marco Jose
        /// Fecha:  27/11/2025
        /// <summary>
        /// Tipo Formacion Service
        /// </summary>
        /// <returns> Lista CategoriaPreguntaDTO </returns>
        public IEnumerable<PaqueteTutorVirtualDTO> Obtener()
        {
            return _unitOfWork.PaqueteTutorVirtualRepository.Obtener();
        }

        /// Autor: Villanueva Torres Marco Jose
        /// Fecha:  27/11/2025
        /// Version: 1.0
        /// <summary>
        /// Registra un nuevo PaqueteTutorVirtual
        /// </summary>
        /// <param name="dto">PaqueteTutorVirtualDTO</param>
        /// <param name="usuario">Usuario Registro</param>
        /// <returns>PaqueteTutorVirtualDTO</returns>
        public PaqueteTutorVirtualDTO Insertar(PaqueteTutorVirtualDTO dto, string usuario)
        {
            try
            {
                if (dto != null)
                {
                    PaqueteTutorVirtual entidad = new()
                    {
                        Nombre = dto.Nombre,
                        Estado = true,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                    };
                    var respuesta = _unitOfWork.PaqueteTutorVirtualRepository.Add(entidad);
                    _unitOfWork.Commit();
                    entidad.Id = respuesta.Id;
                    var resultado = _mapper.Map<PaqueteTutorVirtualDTO>(respuesta);


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
        /// Fecha:  27/11/2025
        /// Version: 1.0
        /// <summary>
        /// Realiza una inserción o actualizacion basica a la tabla y sus detalles
        /// </summary>   
        /// <param name="CategoriaPreguntaDTO"> parametros de la nueva PaqueteTutorVirtualDTO y sus detalles </param>

        public PaqueteTutorVirtualDTO Actualizar(PaqueteTutorVirtualDTO dto, string usuario)
        {
            try
            {
                PaqueteTutorVirtual? entidad = new();
                if (dto != null)
                {
                    if (dto.Id != 0)
                    {
                        entidad = _unitOfWork.PaqueteTutorVirtualRepository.ObtenerPorId(dto.Id);
                        if (entidad != null && entidad.Id != 0)
                        {
                            entidad.Nombre = dto.Nombre;
                            entidad.UsuarioModificacion = usuario;
                            entidad.FechaModificacion = DateTime.Now;
                            var respuesta = _unitOfWork.PaqueteTutorVirtualRepository.Update(entidad);
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
        /// Fecha:  27/11/2025
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
                var entidad = _unitOfWork.PaqueteTutorVirtualRepository.ObtenerPorId(id);
                if (entidad != null && entidad.Id != 0)
                {
                    var respuesta = _unitOfWork.PaqueteTutorVirtualRepository.Delete(id, usuario);

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
