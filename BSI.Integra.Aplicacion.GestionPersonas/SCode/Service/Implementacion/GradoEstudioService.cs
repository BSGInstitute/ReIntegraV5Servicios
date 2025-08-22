using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.GestionPersonas.SCode.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.GestionPersonas.SCode.Service.Implementacion
{
    /// Service: GradoEstudioService
    /// Autor: Juan D. Huanaco Quispe
    /// Fecha: 04/04/2024
    /// <summary>
    /// Gestión general de T_GradoEstudio
    /// </summary>
    public class GradoEstudioService : IGradoEstudioService
    {
        private Mapper _mapper;
        private IUnitOfWork _unitOfWork;

        public GradoEstudioService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TGradoEstudio, GradoEstudio>(MemberList.None).ReverseMap();
                cfg.CreateMap<TGradoEstudio, GradoEstudioDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<GradoEstudio, GradoEstudioDTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base 
        GradoEstudioDTO IGradoEstudioService.Insertar(GradoEstudioDTO dto, string usuario)
        {
            try
            {
                if (dto == null)
                    throw new BadRequestException("Entidad Nula");

                GradoEstudio entidad = new()
                {
                    Nombre = dto.Nombre,
                    Estado = true,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                };

                var respuesta = _unitOfWork.GradoEstudioRepository.Add(entidad);
                _unitOfWork.Commit();
                var resultado = _mapper.Map<GradoEstudioDTO>(respuesta);
                return resultado;

            }
            catch (Exception)
            {
                throw;
            }
        }

        GradoEstudioDTO IGradoEstudioService.Actualizar(GradoEstudioDTO dto, string usuario)
        {
            try
            {
                GradoEstudio? entidad = new();
                if (dto == null)
                    throw new BadRequestException("Entidad Nula");
                
                if (dto.Id <= 0)
                    throw new BadRequestException("Id Entidad Invalida");
                
                entidad = _unitOfWork.GradoEstudioRepository.ObtenerPorId(dto.Id);

                if (entidad == null || entidad.Id < 0)
                    throw new BadRequestException($"Entidad no encontrada con id {dto.Id}");
                
                entidad.Nombre = dto.Nombre;
                entidad.UsuarioModificacion = usuario;
                entidad.FechaModificacion = DateTime.Now;
                var respuesta = _unitOfWork.GradoEstudioRepository.Update(entidad);
                _unitOfWork.Commit();
                return dto;
                
            }
            catch (Exception)
            {
                throw;
            }
        }

        bool IGradoEstudioService.Eliminar(int id, string usuario)
        {
            try
            {
                if (id <= 0)
                    throw new BadRequestException($"Id no valido");
                
                var entidad = _unitOfWork.GradoEstudioRepository.ObtenerPorId(id);
                if (entidad == null || entidad.Id <= 0)
                    throw new BadRequestException($"No se encontro la entidad con el id {id}");

                var respuesta = _unitOfWork.GradoEstudioRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return respuesta;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        /// Autor: Juan D. Huanaco Quispe
        /// Fecha: 04/04/2024
        /// <summary>
        /// Obtiene los registros
        /// </summary>
        /// <returns> IEnumerable GradoEstudioDTO </returns>
        IEnumerable<GradoEstudioDTO> IGradoEstudioService.Obtener()
        {
            return _unitOfWork.GradoEstudioRepository.Obtener();
        }
    }
}
