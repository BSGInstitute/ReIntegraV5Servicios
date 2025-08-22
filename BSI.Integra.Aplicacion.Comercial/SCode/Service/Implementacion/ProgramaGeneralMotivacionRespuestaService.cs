using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Comercial.Service.Implementacion
{
    /// Service: ProgramaGeneralMotivacionRespuestaService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 13/08/2022
    /// <summary>
    /// Gestión general de T_ProgramaGeneralMotivacionRespuesta
    /// </summary>
    public class ProgramaGeneralMotivacionRespuestaService : IProgramaGeneralMotivacionRespuestaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public ProgramaGeneralMotivacionRespuestaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TProgramaGeneralMotivacionRespuestum, ProgramaGeneralMotivacionRespuesta>(MemberList.None).ReverseMap();
                    cfg.CreateMap<ProgramaGeneralMotivacionRespuestaDTO, ProgramaGeneralMotivacionRespuesta>(MemberList.None).ReverseMap();
                    cfg.CreateMap<ProgramaGeneralMotivacionRespuestaDTO, TProgramaGeneralMotivacionRespuestum>(MemberList.None).ReverseMap();
                }
            );
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public ProgramaGeneralMotivacionRespuesta Add(ProgramaGeneralMotivacionRespuesta entidad)
        {
            try
            {
                var modelo = _unitOfWork.ProgramaGeneralMotivacionRespuestaRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ProgramaGeneralMotivacionRespuesta>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ProgramaGeneralMotivacionRespuesta Update(ProgramaGeneralMotivacionRespuesta entidad)
        {
            try
            {
                var modelo = _unitOfWork.ProgramaGeneralMotivacionRespuestaRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ProgramaGeneralMotivacionRespuesta>(modelo);
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
                _unitOfWork.ProgramaGeneralMotivacionRespuestaRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProgramaGeneralMotivacionRespuesta> Add(List<ProgramaGeneralMotivacionRespuesta> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ProgramaGeneralMotivacionRespuestaRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ProgramaGeneralMotivacionRespuesta>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProgramaGeneralMotivacionRespuesta> Update(List<ProgramaGeneralMotivacionRespuesta> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ProgramaGeneralMotivacionRespuestaRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ProgramaGeneralMotivacionRespuesta>>(modelo);
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
                _unitOfWork.ProgramaGeneralMotivacionRespuestaRepository.Delete(listadoIds, usuario);
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
        /// Obtiene todos los registros de T_ProgramaGeneralMotivacionRespuesta
        /// </summary>
        /// <returns> List<ProgramaGeneralMotivacionRespuestaDTO> </returns>
        public IEnumerable<ProgramaGeneralMotivacionRespuestaDTO> ObtenerProgramaGeneralMotivacionRespuesta()
        {
            try
            {
                return _unitOfWork.ProgramaGeneralMotivacionRespuestaRepository.ObtenerProgramaGeneralMotivacionRespuesta();
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
        /// Obtiene una entidad ProgramaGeneralMotivacionRespuesta asociado a una Oportunidad y una Motivacion.
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <param name="idMotivacion">Id de la Motivacion asociada a un Programa General</param>
        /// <returns> ProgramaGeneralMotivacionRespuesta </returns>
        public ProgramaGeneralMotivacionRespuesta ObtenerPorIdOportunidadIdMotivacion(int idOportunidad, int idMotivacion)
        {
            try
            {
                var motivacionRespuesta = _unitOfWork.ProgramaGeneralMotivacionRespuestaRepository.ObtenerPorIdOportunidadIdMotivacion(idOportunidad, idMotivacion);
                var entidad = _mapper.Map<ProgramaGeneralMotivacionRespuesta>(motivacionRespuesta);
                if (entidad != null) entidad.Estado = true;
                return entidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 06/03/2023
        /// Version: 1.0
        /// <summary>
        /// Registra el Programa General Motivacion Respuesta
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <param name="idMotivacion">Id de la Motivacion asociada a un Programa General</param>
        /// <returns> ProgramaGeneralMotivacionRespuesta </returns>
        public ProgramaGeneralMotivacionRespuestaDTO GuardarCambios(ProgramaGeneralMotivacionRespuestaDTO item, string userName)
        {
            try
            {
                ProgramaGeneralMotivacionRespuesta motivacionRespuesta = _unitOfWork.ProgramaGeneralMotivacionRespuestaRepository.ObtenerPorIdOportunidadIdMotivacion(item.IdOportunidad, item.IdProgramaGeneralMotivacion);
                var respuesta = new ProgramaGeneralMotivacionRespuestaDTO();
                if (motivacionRespuesta != null && motivacionRespuesta.Id != 0)
                {
                    motivacionRespuesta.Respuesta = item.Respuesta;
                    motivacionRespuesta.UsuarioModificacion = userName;
                    motivacionRespuesta.FechaModificacion = DateTime.Now;
                    var resultado = _unitOfWork.ProgramaGeneralMotivacionRespuestaRepository.Update(motivacionRespuesta);
                    _unitOfWork.Commit();
                    respuesta = _mapper.Map<ProgramaGeneralMotivacionRespuestaDTO>(resultado);
                }
                else
                {
                    motivacionRespuesta = new ProgramaGeneralMotivacionRespuesta()
                    {
                        IdOportunidad = item.IdOportunidad,
                        IdProgramaGeneralMotivacion = item.IdProgramaGeneralMotivacion,
                        Respuesta = item.Respuesta,
                        Estado = true,
                        UsuarioCreacion = userName,
                        UsuarioModificacion = userName,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                    };
                    var resultado = _unitOfWork.ProgramaGeneralMotivacionRespuestaRepository.Add(motivacionRespuesta);
                    _unitOfWork.Commit();
                    respuesta = _mapper.Map<ProgramaGeneralMotivacionRespuestaDTO>(resultado);
                }
                return respuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
