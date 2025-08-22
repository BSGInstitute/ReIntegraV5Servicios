using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.GestionPersonas.Service.Implementacion
{
    public class PersonalRelacionExternaService : IPersonalRelacionExternaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public PersonalRelacionExternaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPersonalRelacionExterna, PersonalRelacionExterna>(MemberList.None).ReverseMap();
                cfg.CreateMap<PersonalRelacionExterna, PersonalRelacionExternaDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<TPersonalRelacionExterna, PersonalRelacionExternaDTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Autor: Marco Jose Villanueva Torres
        /// Fecha: 30/04/2024
        /// <summary>
        /// Obtiene todos los registros de PersonalRelacionExterna
        /// </summary>
        /// <returns> Lista PersonalRelacionExternaDTO </returns>
        public IEnumerable<PersonalRelacionExternaDTO> Obtener()
        {
            return _unitOfWork.PersonalRelacionExternaRepository.Obtener();
        }
        /// Autor: Marco Jose Villanueva Torres
        /// Fecha: 30/04/2024
        /// <summary>
        /// Obtiene todos los registros de Area Trabajo
        /// </summary>
        /// <returns> Lista NivelEstudioDTO </returns>
        public IEnumerable<ComboDTO> ObtenerAreaTrabajo()
        {
            return _unitOfWork.PersonalAreaTrabajoRepository.ObtenerTodoFiltroAreaTrabajo();
        }

        /// Autor: Marco Jose Villanueva Torres
        /// Fecha: 30/04/2024
        /// Version: 1.0
        /// <summary>
        /// Registra un nuevo PersonalRelacionExterna
        /// </summary>
        /// <param name="dto">PersonalRelacionExternaDTO</param>
        /// <param name="usuario">Usuario Registro</param>
        /// <returns>PersonalRelacionExternaDTO</returns>
        public PersonalRelacionExternaDTO Insertar(PersonalRelacionExternaDTO dto, string usuario)
        {
            try
            {
                if (dto != null)
                {
                    PersonalRelacionExterna entidad = new()
                    {
                        Nombre = dto.Nombre,
                        IdPersonalAreaTrabajo=dto.IdPersonalAreaTrabajo,
                        Estado = true,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                    };
                    var respuesta = _unitOfWork.PersonalRelacionExternaRepository.Add(entidad);
                    _unitOfWork.Commit();
                    entidad.Id = respuesta.Id;
                    var resultado = _mapper.Map<PersonalRelacionExternaDTO>(respuesta);
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
        /// Autor: Marco Jose Villanueva Torres
        /// Fecha: 30/04/2024
        /// Version: 1.0
        /// <summary>
        /// Realiza una inserción o actualizacion basica a la tabla y sus detalles
        /// </summary>   
        /// <param name="dto"> PersonalRelacionExternaDTO</param>
        /// <param name="usuario">Usuario Modificacion</param>
        public PersonalRelacionExternaDTO Actualizar(PersonalRelacionExternaDTO dto, string usuario)
        {
            try
            {
                PersonalRelacionExterna? entidad = new();
                if (dto != null)
                {
                    if (dto.Id != 0)
                    {
                        entidad = _unitOfWork.PersonalRelacionExternaRepository.ObtenerPorId(dto.Id);
                        if (entidad != null && entidad.Id != 0)
                        {
                            entidad.Nombre = dto.Nombre;
                            entidad.IdPersonalAreaTrabajo = dto.IdPersonalAreaTrabajo;
                            entidad.UsuarioModificacion = usuario;
                            entidad.FechaModificacion = DateTime.Now;
                            var respuesta = _unitOfWork.PersonalRelacionExternaRepository.Update(entidad);
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
        /// Autor: Marco Jose Villanueva Torres
        /// Fecha: 30/04/2024
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
                var entidad = _unitOfWork.PersonalRelacionExternaRepository.ObtenerPorId(id);
                if (entidad != null && entidad.Id != 0)
                {
                    var respuesta = _unitOfWork.PersonalRelacionExternaRepository.Delete(id, usuario);
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
