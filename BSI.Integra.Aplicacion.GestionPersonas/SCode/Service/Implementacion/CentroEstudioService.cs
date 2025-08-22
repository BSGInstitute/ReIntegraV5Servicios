using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.GestionPersonas.SCode.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.GestionPersonas.SCode.Service.Implementacion
{
    /// Service: CentroEstudioService
    /// Autor: Juan D. Huanaco Quispe
    /// Fecha: 08/04/2024
    /// <summary>
    /// Gestión general de T_CentroEstudio
    /// </summary>
    public class CentroEstudioService : ICentroEstudioService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public CentroEstudioService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCentroEstudio, CentroEstudio>(MemberList.None).ReverseMap();
                cfg.CreateMap<TCentroEstudio, CentroEstudioDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<CentroEstudio, CentroEstudioDTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base 
        CentroEstudioDTO ICentroEstudioService.Insertar(CentroEstudioDTO dto, string usuario)
        {
            try
            {
                if (dto == null)
                    throw new BadRequestException("Entidad Nula");

                ValidarIntegridadIdsExternas(dto);

                CentroEstudio entidad = new()
                {
                    Nombre = dto.Nombre,
                    IdPais = dto.IdPais,
                    IdCiudad = dto.IdCiudad,
                    IdTipoCentroEstudio = dto.IdTipoCentroEstudio,
                    Estado = true,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                };

                var respuesta = _unitOfWork.CentroEstudioRepository.Add(entidad);
                _unitOfWork.Commit();
                var resultado = _mapper.Map<CentroEstudioDTO>(respuesta);
                return resultado;

            }
            catch (Exception)
            {
                throw;
            }
        }

        CentroEstudioDTO ICentroEstudioService.Actualizar(CentroEstudioDTO dto, string usuario)
        {
            try
            {
                CentroEstudio? entidad = new();
                if (dto == null)
                    throw new BadRequestException("Entidad Nula");

                if (dto.Id <= 0)
                    throw new BadRequestException("Id Entidad Invalida");

                entidad = _unitOfWork.CentroEstudioRepository.ObtenerPorId(dto.Id);

                if (entidad == null || entidad.Id < 0)
                    throw new BadRequestException("Entidad no encontrada");

                ValidarIntegridadIdsExternas(dto);

                entidad.Nombre = dto.Nombre;
                entidad.IdPais = dto.IdPais;
                entidad.IdCiudad = dto.IdCiudad;
                entidad.IdTipoCentroEstudio = dto.IdTipoCentroEstudio;
                entidad.UsuarioModificacion = usuario;
                entidad.FechaModificacion = DateTime.Now;
                var respuesta = _unitOfWork.CentroEstudioRepository.Update(entidad);
                _unitOfWork.Commit();
                return dto;

            }
            catch (Exception)
            {
                throw;
            }
        }

        bool ICentroEstudioService.Eliminar(int id, string usuario)
        {
            try
            {
                if (id <= 0)
                    throw new BadRequestException($"Id no valido");

                var entidad = _unitOfWork.CentroEstudioRepository.ObtenerPorId(id);
                if (entidad == null || entidad.Id < 0)
                    throw new BadRequestException($"No se encontro la entidad con el id {id}");

                var respuesta = _unitOfWork.CentroEstudioRepository.Delete(id, usuario);
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
        /// Valida la integridad/existencia de Ids externos
        /// </summary>
        /// <exception cref="BadRequestException">Si algun Id (Pais, Ciudad, TipoCentroEstudio) no es valido o no existe</exception>
        private void ValidarIntegridadIdsExternas(CentroEstudioDTO dto)
        {
            if (_unitOfWork.PaisRepository.ObtenerPorId(dto.IdPais) == null)
                throw new BadRequestException("El Id del Pais no existe");

            var ciudad = _unitOfWork.CiudadRepository.ObtenerPorId(dto.IdCiudad);

            if (ciudad == null)
                throw new BadRequestException("El Id de Ciudad no existe");

            if (ciudad.IdPais != dto.IdPais)
                throw new BadRequestException("El Id de Ciudad no pertenece al Id del Pais");

            if (_unitOfWork.TipoCentroEstudioRepository.ObtenerPorId(dto.IdTipoCentroEstudio) == null)
                throw new BadRequestException("El Id del Tipo Centro Estudio no existe");
        }

        /// Autor: Juan D. Huanaco Quispe
        /// Fecha: 08/04/2024
        /// <summary>
        /// Obtiene los registros
        /// </summary>
        /// <returns> IEnumerable CentroEstudioDTO</returns>
        IEnumerable<CentroEstudioDTO> ICentroEstudioService.Obtener()
        {
            return _unitOfWork.CentroEstudioRepository.Obtener();
        }


        public IEnumerable<CentroEstudioComboDTO> ObtenerComboCentroEstudio()
        {
            try
            {
                return _unitOfWork.CentroEstudioRepository.ObtenerComboCentroEstudio();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<CentroEstudioComboDTO> ObtenerListaEstadoEstudioCombo()
        {
            try
            {
                return _unitOfWork.CentroEstudioRepository.ObtenerListaEstadoEstudioCombo();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
