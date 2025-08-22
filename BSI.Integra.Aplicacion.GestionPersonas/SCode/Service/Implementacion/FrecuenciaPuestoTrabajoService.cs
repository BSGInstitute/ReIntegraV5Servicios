using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.GestionPersonas.Service.Implementacion
{
    public class FrecuenciaPuestoTrabajoService : IFrecuenciaPuestoTrabajoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public FrecuenciaPuestoTrabajoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TFrecuenciaPuestoTrabajo, FrecuenciaPuestoTrabajo>(MemberList.None).ReverseMap();
                cfg.CreateMap<FrecuenciaPuestoTrabajo, FrecuenciaPuestoTrabajoDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<TFrecuenciaPuestoTrabajo, FrecuenciaPuestoTrabajoDTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 30/04/2024
        /// <summary>
        /// Obtiene todos los registros de FrecuenciaPuestoTrabajo
        /// </summary>
        /// <returns> Lista FrecuenciaPuestoTrabajoDTO </returns>
        public IEnumerable<FrecuenciaPuestoTrabajoDTO> Obtener()
        {
            return _unitOfWork.FrecuenciaPuestoTrabajoRepository.Obtener();
        }

        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 30/04/2024
        /// Version: 1.0
        /// <summary>
        /// Registra un nuevo FrecuenciaPuestoTrabajo
        /// </summary>
        /// <param name="dto">FrecuenciaPuestoTrabajoDTO</param>
        /// <param name="usuario">Usuario Registro</param>
        /// <returns>FrecuenciaPuestoTrabajoDTO</returns>
        public FrecuenciaPuestoTrabajoDTO Insertar(FrecuenciaPuestoTrabajoDTO dto, string usuario)
        {
            try
            {
                if (dto != null)
                {
                    FrecuenciaPuestoTrabajo entidad = new()
                    {
                        Nombre = dto.Nombre,
                        Estado = true,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                    };
                    var respuesta = _unitOfWork.FrecuenciaPuestoTrabajoRepository.Add(entidad);
                    _unitOfWork.Commit();
                    entidad.Id = respuesta.Id;
                    var resultado = _mapper.Map<FrecuenciaPuestoTrabajoDTO>(respuesta);
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
        /// <param name="dto"> FrecuenciaPuestoTrabajoDTO</param>
        /// <param name="usuario">Usuario Modificacion</param>
        public FrecuenciaPuestoTrabajoDTO Actualizar(FrecuenciaPuestoTrabajoDTO dto, string usuario)
        {
            try
            {
                FrecuenciaPuestoTrabajo? entidad = new();
                if (dto != null)
                {
                    if (dto.Id != 0)
                    {
                        entidad = _unitOfWork.FrecuenciaPuestoTrabajoRepository.ObtenerPorId(dto.Id);
                        if (entidad != null && entidad.Id != 0)
                        {
                            entidad.Nombre = dto.Nombre;
                            entidad.UsuarioModificacion = usuario;
                            entidad.FechaModificacion = DateTime.Now;
                            var respuesta = _unitOfWork.FrecuenciaPuestoTrabajoRepository.Update(entidad);
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
                var entidad = _unitOfWork.FrecuenciaPuestoTrabajoRepository.ObtenerPorId(id);
                if (entidad != null && entidad.Id != 0)
                {
                    var respuesta = _unitOfWork.FrecuenciaPuestoTrabajoRepository.Delete(id, usuario);
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
