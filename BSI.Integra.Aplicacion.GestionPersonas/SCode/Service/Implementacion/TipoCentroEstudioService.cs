using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.GestionPersonas.SCode.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.GestionPersonas.SCode.Service.Implementacion
{
    /// Service: TipoCentroEstudioService
    /// Autor: Juan D. Huanaco Quispe
    /// Fecha: 08/04/2024
    /// <summary>
    /// Gestión general de T_TipoCentroEstudio
    /// </summary>
    public class TipoCentroEstudioService : ITipoCentroEstudioService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public TipoCentroEstudioService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TTipoCentroEstudio, TipoCentroEstudio>(MemberList.None).ReverseMap();
                cfg.CreateMap<TTipoCentroEstudio, TipoCentroEstudioDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<TipoCentroEstudio, TipoCentroEstudioDTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        TipoCentroEstudioDTO ITipoCentroEstudioService.Insertar(TipoCentroEstudioDTO dto, string usuario)
        {
            try
            {
                if (dto == null)
                    throw new BadRequestException("Entidad Nula");

                TipoCentroEstudio entidad = new()
                {
                    Nombre = dto.Nombre,
                    Estado = true,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                };

                var respuesta = _unitOfWork.TipoCentroEstudioRepository.Add(entidad);
                _unitOfWork.Commit();
                var resultado = _mapper.Map<TipoCentroEstudioDTO>(respuesta);
                return resultado;

            }
            catch (Exception)
            {
                throw;
            }
        }

        TipoCentroEstudioDTO ITipoCentroEstudioService.Actualizar(TipoCentroEstudioDTO dto, string usuario)
        {
            try
            {
                TipoCentroEstudio? entidad = new();
                if (dto == null)
                    throw new BadRequestException("Entidad Nula");

                if (dto.Id <= 0)
                    throw new BadRequestException("Id Entidad Invalida");

                entidad = _unitOfWork.TipoCentroEstudioRepository.ObtenerPorId(dto.Id);

                if (entidad == null || entidad.Id <= 0)
                    throw new BadRequestException("Entidad no encontrada");

                entidad.Nombre = dto.Nombre;
                entidad.UsuarioModificacion = usuario;
                entidad.FechaModificacion = DateTime.Now;
                var respuesta = _unitOfWork.TipoCentroEstudioRepository.Update(entidad);
                _unitOfWork.Commit();
                return dto;

            }
            catch (Exception)
            {
                throw;
            }
        }

        bool ITipoCentroEstudioService.Eliminar(int id, string usuario)
        {
            try
            {
                if (id <= 0)
                    throw new BadRequestException($"Id no valido");

                var entidad = _unitOfWork.TipoCentroEstudioRepository.ObtenerPorId(id);
                if (entidad == null || entidad.Id <= 0)
                    throw new BadRequestException($"No se encontro la entidad con el id {id}");

                var respuesta = _unitOfWork.TipoCentroEstudioRepository.Delete(id, usuario);
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
        /// Fecha: 08/04/2024
        /// <summary>
        /// Obtiene los registros
        /// </summary>
        /// <returns> IEnumerable TipoCentroEstudioDTO</returns>
        IEnumerable<TipoCentroEstudioDTO> ITipoCentroEstudioService.Obtener()
        {
            return _unitOfWork.TipoCentroEstudioRepository.Obtener();
        }
    }
}
