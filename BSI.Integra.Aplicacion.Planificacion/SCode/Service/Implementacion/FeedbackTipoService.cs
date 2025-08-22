using AutoMapper;

using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;

using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    /// Service: FeedbackTipoService
    /// Autor: Christian Quispe Mamani.
    /// Fecha: 12/05/2023
    /// <summary>
    /// Gestión general de V_TFeedbackTipo_Filtro
    /// </summary>
    public class FeedbackTipoService: IFeedbackTipoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public FeedbackTipoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TFeedbackTipo, FeedbackTipoDTO>(MemberList.None).ReverseMap();
                    cfg.CreateMap<FeedbackTipo, FeedbackTipoDTO>(MemberList.None).ReverseMap();
                }
            );
            _mapper = new Mapper(config);
        }
        /// Autor: Christian A. Quispe Mamani
        /// Fecha: 12/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los FeedbackTipos
        /// </summary>
        /// <returns> Lista MaterialAccionDTO </returns>
        public List<FeedbackTipoDTO> Obtener()
        {
            try
            {
                var respuesta = _unitOfWork.FeedbackTipoRepository.Obtener();
                return _mapper.Map<List<FeedbackTipoDTO>>(respuesta);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Christian A. Quispe Mamani
        /// Fecha: 13/05/2023
        /// Version: 1.0
        /// <summary>
        /// Registra un nuevo FeedbackTipo
        /// </summary>
        /// <param name="dto">FeedbackTipo</param>
        /// <param name="usuario">Usuario Registro</param>
        /// <returns>FeedbackTipoDTO</returns>
        public FeedbackTipoDTO Insertar(FeedbackTipoDTO dto, string usuario)
        {
            try
            {
                if (dto != null)
                {
                    FeedbackTipo entidad = new()
                    {
                        Nombre = dto.Nombre,
                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                    };
                    var respuesta = _unitOfWork.FeedbackTipoRepository.Add(entidad);
                    _unitOfWork.Commit();
                    return _mapper.Map<FeedbackTipoDTO>(respuesta);
                }
                else
                    throw new BadRequestException("Entidad Nula");
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Christian A. Quispe Mamani
        /// Fecha: 13/05/2023
        /// Version: 1.0
        /// <summary>
        /// Actualiza un FeedbackTipo
        /// </summary>
        /// <param name="dto">FeedbackTipo</param>
        /// <param name="usuario">Usuario Registro</param>
        /// <returns>FeedbackTipoDTO</returns>
        public FeedbackTipoDTO Actualizar(FeedbackTipoDTO dto, string usuario)
        {
            FeedbackTipo entidad = new();
            if (dto != null)
            {
                if (dto.Id != 0)
                {
                    entidad = _unitOfWork.FeedbackTipoRepository.ObtenerPorId(dto.Id);
                    if (entidad != null && entidad.Id != 0)
                    {
                        entidad.Nombre = dto.Nombre;
                        entidad.FechaModificacion = DateTime.Now;
                        entidad.UsuarioModificacion = usuario;
                    }
                    else
                        throw new BadRequestException("Entidad no encontrada");
                }
                else
                    throw new BadRequestException("Id Entidad 0");
            }
            else
                throw new BadRequestException("Entidad Nula");
            var respuesta = _unitOfWork.FeedbackTipoRepository.Update(entidad);
            _unitOfWork.Commit();
            return _mapper.Map<FeedbackTipoDTO>(respuesta);
        }
        /// Autor: Christian A. Quispe Mamani
        /// Fecha: 13/05/2023
        /// Version: 1.0
        /// <summary>
        /// Elimina un registro de manera logica.
        /// </summary>
        /// <param name="id">Id del registro</param>
        /// <param name="usuario">usuario modificador</param>
        /// <returns>true</returns>
        public bool Eliminar(int id, string usuario)
        {
            try
            {
                var feedbackTipo = _unitOfWork.FeedbackTipoRepository.ObtenerPorId(id);
                if (feedbackTipo != null && feedbackTipo.Id != 0)
                {
                    var respuesta = _unitOfWork.FeedbackTipoRepository.Delete(id, usuario);
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
