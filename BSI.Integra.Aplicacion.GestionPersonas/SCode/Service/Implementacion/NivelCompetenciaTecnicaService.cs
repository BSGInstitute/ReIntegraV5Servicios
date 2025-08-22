using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.GestionPersonas.SCode.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
namespace BSI.Integra.Aplicacion.GestionPersonas.SCode.Service.Implementacion
{
    /// Service: NivelCompetenciaTecnicaService
    /// Autor: Juan D. Huanaco Quispe
    /// Fecha: 06/04/2024
    /// <summary>
    /// Gestión general de T_NivelCompetenciaTecnica
    /// </summary>
    public class NivelCompetenciaTecnicaService : INivelCompetenciaTecnicaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public NivelCompetenciaTecnicaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TNivelCompetenciaTecnica, NivelCompetenciaTecnica>(MemberList.None).ReverseMap();
                cfg.CreateMap<TNivelCompetenciaTecnica, NivelCompetenciaTecnicaDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<NivelCompetenciaTecnica, NivelCompetenciaTecnicaDTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base 
        NivelCompetenciaTecnicaDTO INivelCompetenciaTecnicaService.Insertar(NivelCompetenciaTecnicaDTO dto, string usuario)
        {
            try
            {
                if (dto == null)
                    throw new BadRequestException("Entidad Nula");

                NivelCompetenciaTecnica entidad = new()
                {
                    Nombre = dto.Nombre,
                    Estado = true,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                };

                var respuesta = _unitOfWork.NivelCompetenciaTecnicaRepository.Add(entidad);
                _unitOfWork.Commit();
                var resultado = _mapper.Map<NivelCompetenciaTecnicaDTO>(respuesta);
                return resultado;

            }
            catch (Exception)
            {
                throw;
            }
        }

        NivelCompetenciaTecnicaDTO INivelCompetenciaTecnicaService.Actualizar(NivelCompetenciaTecnicaDTO dto, string usuario)
        {
            try
            {
                NivelCompetenciaTecnica? entidad = new();
                if (dto == null)
                    throw new BadRequestException("Entidad Nula");

                if (dto.Id <= 0)
                    throw new BadRequestException("Id Entidad Invalida");

                entidad = _unitOfWork.NivelCompetenciaTecnicaRepository.ObtenerPorId(dto.Id);

                if (entidad == null || entidad.Id < 0)
                    throw new BadRequestException("Entidad no encontrada");

                entidad.Nombre = dto.Nombre;
                entidad.UsuarioModificacion = usuario;
                entidad.FechaModificacion = DateTime.Now;
                var respuesta = _unitOfWork.NivelCompetenciaTecnicaRepository.Update(entidad);
                _unitOfWork.Commit();
                return dto;

            }
            catch (Exception)
            {
                throw;
            }
        }

        bool INivelCompetenciaTecnicaService.Eliminar(int id, string usuario)
        {
            try
            {
                if (id <= 0)
                    throw new BadRequestException($"Id no valido");

                var entidad = _unitOfWork.NivelCompetenciaTecnicaRepository.ObtenerPorId(id);
                if (entidad == null || entidad.Id <= 0)
                    throw new BadRequestException($"No se encontro la entidad con el id {id}");

                var respuesta = _unitOfWork.NivelCompetenciaTecnicaRepository.Delete(id, usuario);
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
        /// Fecha: 06/04/2024
        /// <summary>
        /// Obtiene los registros
        /// </summary>
        /// <returns> IEnumerable NivelCompetenciaTecnicaDTO </returns>
        IEnumerable<NivelCompetenciaTecnicaDTO> INivelCompetenciaTecnicaService.Obtener()
        {
            return _unitOfWork.NivelCompetenciaTecnicaRepository.Obtener();
        }

    }
}
