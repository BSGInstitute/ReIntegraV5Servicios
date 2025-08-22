using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.GestionPersonas.Service.Implementacion
{
    public class MensajeTiempoInactivoService : IMensajeTiempoInactivoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public MensajeTiempoInactivoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TMensajeTiempoInactivo, MensajeTiempoInactivo>(MemberList.None).ReverseMap();
                cfg.CreateMap<MensajeTiempoInactivo, MensajeTiempoInactivoDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<TMensajeTiempoInactivo, MensajeTiempoInactivoDTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 30/04/2024
        /// <summary>
        /// Obtiene todos los registros de MensajeTiempoInactivo
        /// </summary>
        /// <returns> Lista MensajeTiempoInactivoDTO </returns>
        public IEnumerable<MensajeTiempoInactivoDTO> Obtener()
        {
            return _unitOfWork.MensajeTiempoInactivoRepository.Obtener();
        }

        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 30/04/2024
        /// Version: 1.0
        /// <summary>
        /// Registra un nuevo MensajeTiempoInactivo
        /// </summary>
        /// <param name="dto">MensajeTiempoInactivoDTO</param>
        /// <param name="usuario">Usuario Registro</param>
        /// <returns>MensajeTiempoInactivoDTO</returns>
        public MensajeTiempoInactivoDTO Insertar(MensajeTiempoInactivoDTO dto, string usuario)
        {
            try
            {
                if (dto != null)
                {
                    MensajeTiempoInactivo entidad = new()
                    {
                        Mensaje = dto.Mensaje,
                        MinutoInactivo = dto.MinutoInactivo,
                        Estado = true,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                    };
                    var respuesta = _unitOfWork.MensajeTiempoInactivoRepository.Add(entidad);
                    _unitOfWork.Commit();
                    entidad.Id = respuesta.Id;
                    var resultado = _mapper.Map<MensajeTiempoInactivoDTO>(respuesta);
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
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 30/04/2024
        /// Version: 1.0
        /// <summary>
        /// Realiza una inserción o actualizacion basica a la tabla y sus detalles
        /// </summary>   
        /// <param name="dto"> MensajeTiempoInactivoDTO</param>
        /// <param name="usuario">Usuario Modificacion</param>
        public MensajeTiempoInactivoDTO Actualizar(MensajeTiempoInactivoDTO dto, string usuario)
        {
            try
            {
                MensajeTiempoInactivo? entidad = new();
                if (dto != null)
                {
                    if (dto.Id != 0)
                    {
                        entidad = _unitOfWork.MensajeTiempoInactivoRepository.ObtenerPorId(dto.Id);
                        if (entidad != null && entidad.Id != 0)
                        {
                            entidad.Mensaje = dto.Mensaje;
                            entidad.MinutoInactivo = dto.MinutoInactivo;
                            entidad.UsuarioModificacion = usuario;
                            entidad.FechaModificacion = DateTime.Now;
                            var respuesta = _unitOfWork.MensajeTiempoInactivoRepository.Update(entidad);
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
        /// Autor: Flavio R. Mamani Fabian
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
                var entidad = _unitOfWork.MensajeTiempoInactivoRepository.ObtenerPorId(id);
                if (entidad != null && entidad.Id != 0)
                {
                    var respuesta = _unitOfWork.MensajeTiempoInactivoRepository.Delete(id, usuario);
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
