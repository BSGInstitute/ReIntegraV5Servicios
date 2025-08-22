using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.GestionPersonas.Service.Implementacion
{
    public class PerfilPuestoTrabajoEstadoSolicitudService : IPerfilPuestoTrabajoEstadoSolicitudService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public PerfilPuestoTrabajoEstadoSolicitudService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPerfilPuestoTrabajoEstadoSolicitud, PerfilPuestoTrabajoEstadoSolicitud>(MemberList.None).ReverseMap();
                cfg.CreateMap<PerfilPuestoTrabajoEstadoSolicitud, PerfilPuestoTrabajoEstadoSolicitudDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<TPerfilPuestoTrabajoEstadoSolicitud, PerfilPuestoTrabajoEstadoSolicitudDTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 30/04/2024
        /// <summary>
        /// Obtiene todos los registros de PerfilPuestoTrabajoEstadoSolicitud
        /// </summary>
        /// <returns> Lista PerfilPuestoTrabajoEstadoSolicitudDTO </returns>
        public IEnumerable<PerfilPuestoTrabajoEstadoSolicitudDTO> Obtener()
        {
            return _unitOfWork.PerfilPuestoTrabajoEstadoSolicitudRepository.Obtener();
        }

        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 30/04/2024
        /// Version: 1.0
        /// <summary>
        /// Registra un nuevo PerfilPuestoTrabajoEstadoSolicitud
        /// </summary>
        /// <param name="dto">PerfilPuestoTrabajoEstadoSolicitudDTO</param>
        /// <param name="usuario">Usuario Registro</param>
        /// <returns>PerfilPuestoTrabajoEstadoSolicitudDTO</returns>
        public PerfilPuestoTrabajoEstadoSolicitudDTO Insertar(PerfilPuestoTrabajoEstadoSolicitudDTO dto, string usuario)
        {
            try
            {
                if (dto != null)
                {
                    PerfilPuestoTrabajoEstadoSolicitud entidad = new()
                    {
                        Nombre = dto.Nombre,
                        Estado = true,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                    };
                    var respuesta = _unitOfWork.PerfilPuestoTrabajoEstadoSolicitudRepository.Add(entidad);
                    _unitOfWork.Commit();
                    entidad.Id = respuesta.Id;
                    var resultado = _mapper.Map<PerfilPuestoTrabajoEstadoSolicitudDTO>(respuesta);
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
        /// <param name="dto"> PerfilPuestoTrabajoEstadoSolicitudDTO</param>
        /// <param name="usuario">Usuario Modificacion</param>
        public PerfilPuestoTrabajoEstadoSolicitudDTO Actualizar(PerfilPuestoTrabajoEstadoSolicitudDTO dto, string usuario)
        {
            try
            {
                PerfilPuestoTrabajoEstadoSolicitud? entidad = new();
                if (dto != null)
                {
                    if (dto.Id != 0)
                    {
                        entidad = _unitOfWork.PerfilPuestoTrabajoEstadoSolicitudRepository.ObtenerPorId(dto.Id);
                        if (entidad != null && entidad.Id != 0)
                        {
                            entidad.Nombre = dto.Nombre;
                            entidad.UsuarioModificacion = usuario;
                            entidad.FechaModificacion = DateTime.Now;
                            var respuesta = _unitOfWork.PerfilPuestoTrabajoEstadoSolicitudRepository.Update(entidad);
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
                var entidad = _unitOfWork.PerfilPuestoTrabajoEstadoSolicitudRepository.ObtenerPorId(id);
                if (entidad != null && entidad.Id != 0)
                {
                    var respuesta = _unitOfWork.PerfilPuestoTrabajoEstadoSolicitudRepository.Delete(id, usuario);
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
