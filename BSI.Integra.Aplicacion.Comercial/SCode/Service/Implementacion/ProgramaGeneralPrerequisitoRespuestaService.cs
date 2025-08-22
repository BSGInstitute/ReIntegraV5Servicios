using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Comercial.Service.Implementacion
{
    /// Service: ProgramaGeneralPrerequisitoRespuestaService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 13/08/2022
    /// <summary>
    /// Gestión general de T_ProgramaGeneralPrerequisitoRespuesta
    /// </summary>
    public class ProgramaGeneralPrerequisitoRespuestaService : IProgramaGeneralPrerequisitoRespuestaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public ProgramaGeneralPrerequisitoRespuestaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TProgramaGeneralPrerequisitoRespuestum, ProgramaGeneralPrerequisitoRespuesta>(MemberList.None).ReverseMap();
                    cfg.CreateMap<ProgramaGeneralPrerequisitoRespuestaDTO, TProgramaGeneralPrerequisitoRespuestum>(MemberList.None).ReverseMap();
                    cfg.CreateMap<ProgramaGeneralPrerequisitoRespuestaDTO, ProgramaGeneralPrerequisitoRespuesta>(MemberList.None);
                }
            );
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public ProgramaGeneralPrerequisitoRespuesta Add(ProgramaGeneralPrerequisitoRespuesta entidad)
        {
            try
            {
                var modelo = _unitOfWork.ProgramaGeneralPrerequisitoRespuestaRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ProgramaGeneralPrerequisitoRespuesta>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ProgramaGeneralPrerequisitoRespuesta Update(ProgramaGeneralPrerequisitoRespuesta entidad)
        {
            try
            {
                var modelo = _unitOfWork.ProgramaGeneralPrerequisitoRespuestaRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ProgramaGeneralPrerequisitoRespuesta>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(int id, string usuario)
        {
            try
            {
                _unitOfWork.ProgramaGeneralPrerequisitoRespuestaRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProgramaGeneralPrerequisitoRespuesta> Add(List<ProgramaGeneralPrerequisitoRespuesta> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ProgramaGeneralPrerequisitoRespuestaRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ProgramaGeneralPrerequisitoRespuesta>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProgramaGeneralPrerequisitoRespuesta> Update(List<ProgramaGeneralPrerequisitoRespuesta> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ProgramaGeneralPrerequisitoRespuestaRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ProgramaGeneralPrerequisitoRespuesta>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(List<int> listadoIds, string usuario)
        {
            try
            {
                _unitOfWork.ProgramaGeneralPrerequisitoRespuestaRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 13/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_ProgramaGeneralPrerequisitoRespuesta
        /// </summary>
        /// <returns> List<ProgramaGeneralPrerequisitoRespuestaDTO> </returns>
        public IEnumerable<ProgramaGeneralPrerequisitoRespuestaDTO> ObtenerProgramaGeneralPrerequisitoRespuesta()
        {
            try
            {
                return _unitOfWork.ProgramaGeneralPrerequisitoRespuestaRepository.ObtenerProgramaGeneralPrerequisitoRespuesta();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 16/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la entidad ProgramaGeneralPrerequisitoRespuesta asociada a una Oportunidad y un Prerequisito.
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <param name="idPrerequisito">Id del Prerequisito asociado a un Programa General</param>
        /// <returns> ProgramaGeneralPrerequisitoRespuesta </returns>
        public ProgramaGeneralPrerequisitoRespuesta ObtenerPrerequisitoRespuesta(int idOportunidad, int idPrerequisito)
        {
            try
            {
                var respuesta = _unitOfWork.ProgramaGeneralPrerequisitoRespuestaRepository.ObtenerPrerequisitoRespuesta(idOportunidad, idPrerequisito);
                var entidad = _mapper.Map<ProgramaGeneralPrerequisitoRespuesta>(respuesta);
                if (entidad != null) entidad.Estado = true;
                return entidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ProgramaGeneralPrerequisitoRespuestaDTO GuardarCambios(ProgramaGeneralPrerequisitoRespuestaDTO item, string usuario)
        {
            try
            {
                ProgramaGeneralPrerequisitoRespuesta prerequisitoRespuesta = _unitOfWork.ProgramaGeneralPrerequisitoRespuestaRepository.ObtenerPorIdOportunidadIdPrerequisito(item.IdOportunidad, item.IdProgramaGeneralPrerequisito);
                TProgramaGeneralPrerequisitoRespuestum respuesta;
                if (prerequisitoRespuesta != null && prerequisitoRespuesta.Id != 0)
                {
                    prerequisitoRespuesta.Respuesta = item.Respuesta;
                    prerequisitoRespuesta.UsuarioModificacion = usuario;
                    prerequisitoRespuesta.FechaModificacion = DateTime.Now;
                    respuesta = _unitOfWork.ProgramaGeneralPrerequisitoRespuestaRepository.Update(prerequisitoRespuesta);
                }
                else
                {
                    prerequisitoRespuesta = new ProgramaGeneralPrerequisitoRespuesta()
                    {
                        IdOportunidad = item.IdOportunidad,
                        IdProgramaGeneralPrerequisito = item.IdProgramaGeneralPrerequisito,
                        Respuesta = item.Respuesta,
                        Estado = true,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                    };
                    respuesta = _unitOfWork.ProgramaGeneralPrerequisitoRespuestaRepository.Add(prerequisitoRespuesta);
                }
                _unitOfWork.Commit();
                return _mapper.Map<ProgramaGeneralPrerequisitoRespuestaDTO>(respuesta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
