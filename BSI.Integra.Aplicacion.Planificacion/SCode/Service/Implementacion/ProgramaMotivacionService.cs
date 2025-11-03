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
    public class ProgramaMotivacionService : IProgramaMotivacionService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public ProgramaMotivacionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProgramaMotivacion, ProgramaMotivacion>(MemberList.None).ReverseMap();
                cfg.CreateMap<ProgramaMotivacion, ProgramaMotivacionDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<ProgramaMotivacionDTO, ProgramaMotivacion>(MemberList.None).ReverseMap();
                cfg.CreateMap<TProgramaMotivacion, ProgramaMotivacionDTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        /// Autor: Villanueva Torres Marco Jose
        /// Fecha: 15/04/2024
        /// Version: 1.0
        /// <summary>
        /// Registra un nuevo ProgramaMotivacion
        /// </summary>
        /// <param name="dto">ProgramaMotivacionDTO</param>
        /// <param name="usuario">Usuario Registro</param>
        /// <returns>ProgramaMotivacionDTO</returns>
        public ProgramaMotivacionDTO Insertar(ProgramaMotivacionDTO dto, string usuario)
        {
            try
            {
                if (dto != null)
                {
                    ProgramaMotivacion entidad = new()
                    {
                        Descripcion = dto.Descripcion,
                        Estado = true,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                    };
                    var respuesta = _unitOfWork.ProgramaMotivacionRepository.Add(entidad);
                    _unitOfWork.Commit();
                    entidad.Id = respuesta.Id;
                    var resultado = _mapper.Map<ProgramaMotivacionDTO>(respuesta);


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
        /// <param name="CategoriaPreguntaDTO"> parametros de la nueva ProgramaMotivacionDTO y sus detalles </param>

        public ProgramaMotivacionDTO Actualizar(ProgramaMotivacionDTO dto, string usuario)
        {
            try
            {
                ProgramaMotivacion? entidad = new();
                if (dto != null)
                {
                    if (dto.Id != 0)
                    {
                        entidad = _unitOfWork.ProgramaMotivacionRepository.ObtenerPorId(dto.Id);
                        if (entidad != null && entidad.Id != 0)
                        {
                            entidad.Descripcion = dto.Descripcion;
                            entidad.UsuarioModificacion = usuario;
                            entidad.FechaModificacion = DateTime.Now;
                            var respuesta = _unitOfWork.ProgramaMotivacionRepository.Update(entidad);
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
                var entidad = _unitOfWork.ProgramaMotivacionRepository.ObtenerPorId(id);
                if (entidad != null && entidad.Id != 0)
                {
                    var respuesta = _unitOfWork.ProgramaMotivacionRepository.Delete(id, usuario);

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


        public IEnumerable<ProgramaMotivacionDTO> Obtener()
        {
            return _unitOfWork.ProgramaMotivacionRepository.Obtener();
        }
    }
}
