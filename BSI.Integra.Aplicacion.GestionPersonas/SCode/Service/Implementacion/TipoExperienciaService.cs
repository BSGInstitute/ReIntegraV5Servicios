using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.GestionPersonas.SCode.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Aplicacion.GestionPersonas.SCode.Service.Implementacion
{
    /// Service: TipoExperienciaService
    /// Autor: Juan D. Huanaco Quispe
    /// Fecha: 09/04/2024
    /// <summary>
    /// Gestión general de T_TipoExperiencia
    /// </summary>
    public class TipoExperienciaService : ITipoExperienciaService
    {
        private Mapper _mapper;
        private IUnitOfWork _unitOfWork;

        public TipoExperienciaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TTipoExperiencium, TipoExperiencia>(MemberList.None).ReverseMap();
                cfg.CreateMap<TTipoExperiencium, TipoExperienciaDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<TipoExperiencia, TipoExperienciaDTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);


        }
        #region Metodos Base
        TipoExperienciaDTO ITipoExperienciaService.Insertar(TipoExperienciaDTO dto, string usuario)
        {
            try
            {
                if (dto == null)
                    throw new BadRequestException("Entidad Nula");

                TipoExperiencia entidad = new()
                {
                    Nombre = dto.Nombre,
                    Estado = true,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                };

                var respuesta = _unitOfWork.TipoExperienciaRepository.Add(entidad);
                _unitOfWork.Commit();
                var resultado = _mapper.Map<TipoExperienciaDTO>(respuesta);
                return resultado;

            }
            catch (Exception)
            {
                throw;
            }
        }

        TipoExperienciaDTO ITipoExperienciaService.Actualizar(TipoExperienciaDTO dto, string usuario)
        {
            try
            {
                TipoExperiencia? entidad = new();
                if (dto == null)
                    throw new BadRequestException("Entidad Nula");

                if (dto.Id <= 0)
                    throw new BadRequestException("Id Entidad Invalida");

                entidad = _unitOfWork.TipoExperienciaRepository.ObtenerPorId(dto.Id);

                if (entidad == null || entidad.Id < 0)
                    throw new BadRequestException($"Entidad no encontrada con id {dto.Id}");

                entidad.Nombre = dto.Nombre;
                entidad.UsuarioModificacion = usuario;
                entidad.FechaModificacion = DateTime.Now;
                var respuesta = _unitOfWork.TipoExperienciaRepository.Update(entidad);
                _unitOfWork.Commit();
                return dto;

            }
            catch (Exception)
            {
                throw;
            }
        }

        bool ITipoExperienciaService.Eliminar(int id, string usuario)
        {
            try
            {
                if (id <= 0)
                    throw new BadRequestException($"Id no valido");

                var entidad = _unitOfWork.TipoExperienciaRepository.ObtenerPorId(id);
                if (entidad == null || entidad.Id <= 0)
                    throw new BadRequestException($"No se encontro la entidad con el id {id}");

                var respuesta = _unitOfWork.TipoExperienciaRepository.Delete(id, usuario);
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
        /// Fecha: 09/04/2024
        /// <summary>
        /// Obtiene los registros
        /// </summary>
        /// <returns> IEnumerable TipoExperienciaDTO </returns>
        IEnumerable<TipoExperienciaDTO> ITipoExperienciaService.Obtener()
        {
            return _unitOfWork.TipoExperienciaRepository.Obtener();
        }
    }
}
