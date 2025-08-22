using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.GestionPersonas.Service.Implementacion
{
    /// Service: TipoFormacionService
    /// Autor: Villanueva Torres Marco Jose
    /// Fecha: 15/04/2024
    /// <summary>
    /// Tipo Formacion Service
    /// </summary>
    public class TipoFormacionService : ITipoFormacionService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public TipoFormacionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TTipoFormacion, TipoFormacion>(MemberList.None).ReverseMap();
                cfg.CreateMap<TipoFormacion, TipoFormacionDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<TipoFormacionDTO, TipoFormacion>(MemberList.None).ReverseMap();
                cfg.CreateMap<TTipoFormacion, TipoFormacionDTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        /// Autor: Villanueva Torres Marco Jose
        /// Fecha: 15/04/2024
        /// <summary>
        /// Tipo Formacion Service
        /// </summary>
        /// <returns> Lista CategoriaPreguntaDTO </returns>
        public IEnumerable<TipoFormacionDTO> Obtener()
        {
            return _unitOfWork.TipoFormacionRepository.Obtener();
        }

        /// Autor: Villanueva Torres Marco Jose
        /// Fecha: 15/04/2024
        /// Version: 1.0
        /// <summary>
        /// Registra un nuevo TipoFormacion
        /// </summary>
        /// <param name="dto">TipoFormacionDTO</param>
        /// <param name="usuario">Usuario Registro</param>
        /// <returns>TipoFormacionDTO</returns>
        public TipoFormacionDTO Insertar(TipoFormacionDTO dto, string usuario)
        {
            try
            {
                if (dto != null)
                {
                    TipoFormacion entidad = new()
                    {
                        Nombre = dto.Nombre,
                        Estado = true,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                    };
                    var respuesta = _unitOfWork.TipoFormacionRepository.Add(entidad);
                    _unitOfWork.Commit();
                    entidad.Id = respuesta.Id;
                    var resultado = _mapper.Map<TipoFormacionDTO>(respuesta);


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
        /// <param name="CategoriaPreguntaDTO"> parametros de la nueva TipoFormacionDTO y sus detalles </param>

        public TipoFormacionDTO Actualizar(TipoFormacionDTO dto, string usuario)
        {
            try
            {
                TipoFormacion? entidad = new();
                if (dto != null)
                {
                    if (dto.Id != 0)
                    {
                        entidad = _unitOfWork.TipoFormacionRepository.ObtenerPorId(dto.Id);
                        if (entidad != null && entidad.Id != 0)
                        {
                            entidad.Nombre = dto.Nombre;
                            entidad.UsuarioModificacion = usuario;                   
                            entidad.FechaModificacion = DateTime.Now;
                            var respuesta = _unitOfWork.TipoFormacionRepository.Update(entidad);
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
                var entidad = _unitOfWork.TipoFormacionRepository.ObtenerPorId(id);
                if (entidad != null && entidad.Id != 0)
                {
                    var respuesta = _unitOfWork.TipoFormacionRepository.Delete(id, usuario);

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
