using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.GestionPersonas.Service.Implementacion
{
    /// Service: PersonalAreaTrabajoService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 14/07/2022
    /// <summary>
    /// Gestión general de T_PersonalAreaTrabajo
    /// </summary>
    public class PersonalAreaTrabajoService : IPersonalAreaTrabajoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public PersonalAreaTrabajoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPersonalAreaTrabajo, PersonalAreaTrabajo>(MemberList.None).ReverseMap();
                cfg.CreateMap<TPersonalAreaTrabajo, PersonalAreaTrabajoDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<PersonalAreaTrabajoDTO, PersonalAreaTrabajo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 14/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_PersonalAreaTrabajo
        /// </summary>
        /// <returns> List<PersonalAreaTrabajoDTO> </returns>
        public IEnumerable<PersonalAreaTrabajoDTO> Obtener()
        {
            return _unitOfWork.PersonalAreaTrabajoRepository.Obtener();
        }

        /// Metodo Insertar
        /// Autor: Marco Jose Villanueva Torres
        /// Fecha: 10/01/2024
        /// Version: 1.0
        /// <summary>
        /// Realiza una inserción basica a la tabla y sus detalles
        /// </summary>   
        /// <param name="PersonalAreaTrabajoDTO">  </param>
        /// <returns> CertificadoPartnerComplementoDTO </returns>
        public PersonalAreaTrabajoDTO Insertar(PersonalAreaTrabajoDTO dto, string usuario)
        {
            try
            {
                if (dto != null)
                {
                    PersonalAreaTrabajo entidad = new()
                    {
                        Nombre = dto.Nombre,
                        Codigo = dto.Codigo,
                        Descripcion = dto.Descripcion,
                        Estado = true,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                    };
                    var respuesta = _unitOfWork.PersonalAreaTrabajoRepository.Add(entidad);
                    _unitOfWork.Commit();
                    entidad.Id = respuesta.Id;
                    var resultado = _mapper.Map<PersonalAreaTrabajoDTO>(respuesta);

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
        /// Fecha: 10/01/2024
        /// Version: 1.0
        /// <summary>
        /// Realiza una inserción o actualizacion basica a la tabla y sus detalles
        /// </summary>   
        /// <param name="certificadoPartnerComplementoDTO"> parametros de la nueva Plantilla_PW y sus detalles </param>
        public PersonalAreaTrabajoDTO Actualizar(PersonalAreaTrabajoDTO dto, string usuario)
        {
            try
            {
                PersonalAreaTrabajo? entidad = new();
                if (dto != null)
                {
                    if (dto.Id != 0)
                    {
                        entidad = _unitOfWork.PersonalAreaTrabajoRepository.ObtenerPorId(dto.Id);
                        if (entidad != null && entidad.Id != 0)
                        {

                            entidad.Nombre = dto.Nombre;
                            entidad.Codigo = dto.Codigo;
                            entidad.Descripcion = dto.Descripcion;
                            entidad.UsuarioCreacion = usuario;
                            entidad.UsuarioModificacion = usuario;
                            entidad.FechaCreacion = DateTime.Now;
                            entidad.FechaModificacion = DateTime.Now;
                            var respuesta = _unitOfWork.PersonalAreaTrabajoRepository.Update(entidad);
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

        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 14/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_PersonalAreaTrabajo para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            return _unitOfWork.PersonalAreaTrabajoRepository.ObtenerCombo();
        }
        /// Metodo Eliminar.
        /// Autor: Marco Jose Villanueva Torres
        /// Fecha: 16/01/2024
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
                var entidad = _unitOfWork.PersonalAreaTrabajoRepository.ObtenerPorId(id);
                if (entidad != null && entidad.Id != 0)
                {
                    var respuesta = _unitOfWork.PersonalAreaTrabajoRepository.Delete(id, usuario);

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
