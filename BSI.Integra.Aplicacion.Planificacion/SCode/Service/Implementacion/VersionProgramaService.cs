using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    /// Service: VersionProgramaService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 09/08/2022
    /// <summary>
    /// Gestión general de T_VersionPrograma
    /// </summary>
    public class VersionProgramaService : IVersionProgramaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public VersionProgramaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TVersionPrograma, VersionPrograma>(MemberList.None).ReverseMap();
                    cfg.CreateMap<TVersionPrograma, VersionProgramaDTO>(MemberList.None).ReverseMap();
                    cfg.CreateMap<VersionProgramaDTO, VersionPrograma>(MemberList.None).ReverseMap();
                    cfg.CreateMap<VersionPrograma, VersionProgramaDTO>(MemberList.None).ReverseMap();

                });
            _mapper = new Mapper(config);
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_VersionPrograma
        /// </summary>
        /// <returns> List<VersionProgramaDTO> </returns>
        public IEnumerable<VersionProgramaDTO> ObtenerVersionPrograma()
        {
            return _unitOfWork.VersionProgramaRepository.ObtenerVersionPrograma();
        }
        /// Autor: Gretel Canasa
        /// Fecha: 11/05/2023
        /// Version: 1.0
        /// <summary>
        /// Registra un nuevo Material de Accion
        /// </summary>
        /// <param name="dto">Material de Accion</param>
        /// <param name="usuario">Usuario Registro</param>
        /// <returns>VersionProgramaDTO</returns>
        public VersionProgramaDTO Insertar(VersionProgramaDTO dto, string usuario)
        {
            try
            {
                if (dto != null)
                {
                    VersionPrograma entidad = new()
                    {
                        Nombre = dto.Nombre,
                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                    };
                    var respuesta = _unitOfWork.VersionProgramaRepository.Add(entidad);
                    _unitOfWork.Commit();
                    return _mapper.Map<VersionProgramaDTO>(respuesta);
                }
                else
                    throw new BadRequestException("Entidad Nula");
            }
            catch (Exception)
            {
                throw;
            }
        }


        public VersionProgramaDTO Actualizar(VersionProgramaDTO dto, string usuario)
        {
            try
            {
                VersionPrograma entidad = new();
                if (dto != null)
                {
                    if (dto.Id != 0)
                    {
                        entidad = _unitOfWork.VersionProgramaRepository.ObtenerPorId(dto.Id);
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
                var respuesta = _unitOfWork.VersionProgramaRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<VersionProgramaDTO>(respuesta);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Gretel Canasa
        /// Fecha: 25/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene Seccion Especifico asociada a un ProgramaGeneral con un Titulo especifico.
        /// </summary>
        /// <param name="idPGeneral">Id del Programa General</param>
        /// <param name="tituloSeccion">Titulo de la Seccion</param>
        /// <returns> PGeneralDocumentoSeccionDTO </returns>
        public VersionProgramaDTO ObtenerPorId(int id)
        {
            try
            {
                var respuesta = _unitOfWork.VersionProgramaRepository.ObtenerPorId(id);
                if (respuesta != null && respuesta.Id != 0)
                {
                    return _mapper.Map<VersionProgramaDTO>(respuesta);
                }
                else
                {
                    throw new BadRequestException($"No existe la entidad con el id {id}");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// Autor: Gretel Canasa
        /// Fecha: 18/04/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene Seccion Especifico asociada a un ProgramaGeneral con un Titulo especifico.
        /// </summary>
        /// <param name="idPGeneral">Id del Programa General</param>
        /// <returns> PGeneralDocumentoSeccionDTO </returns>
        public bool Eliminar(int id, string usuario)
        {
            try
            {
                var versionPrograma = _unitOfWork.VersionProgramaRepository.ObtenerPorId(id);
                if (versionPrograma != null && versionPrograma.Id != 0)
                {
                    var respuesta = _unitOfWork.VersionProgramaRepository.Delete(id, usuario);
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
