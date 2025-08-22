using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Comercial.Service.Implementacion
{
    /// Service: ProgramaGeneralCertificacionRespuestaService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 13/08/2022
    /// <summary>
    /// Gestión general de T_ProgramaGeneralCertificacionRespuesta
    /// </summary>
    public class ProgramaGeneralCertificacionRespuestaService : IProgramaGeneralCertificacionRespuestaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public ProgramaGeneralCertificacionRespuestaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TProgramaGeneralCertificacionRespuestum, ProgramaGeneralCertificacionRespuesta>(MemberList.None).ReverseMap();
                    cfg.CreateMap<ProgramaGeneralCertificacionRespuestaDTO, TProgramaGeneralCertificacionRespuestum>(MemberList.None).ReverseMap();
                    cfg.CreateMap<ProgramaGeneralCertificacionRespuestaDTO, ProgramaGeneralCertificacionRespuesta>(MemberList.None).ReverseMap();
                }
            );
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public ProgramaGeneralCertificacionRespuesta Add(ProgramaGeneralCertificacionRespuesta entidad)
        {
            try
            {
                var modelo = _unitOfWork.ProgramaGeneralCertificacionRespuestaRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ProgramaGeneralCertificacionRespuesta>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ProgramaGeneralCertificacionRespuesta Update(ProgramaGeneralCertificacionRespuesta entidad)
        {
            try
            {
                var modelo = _unitOfWork.ProgramaGeneralCertificacionRespuestaRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ProgramaGeneralCertificacionRespuesta>(modelo);
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
                _unitOfWork.ProgramaGeneralCertificacionRespuestaRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProgramaGeneralCertificacionRespuesta> Add(List<ProgramaGeneralCertificacionRespuesta> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ProgramaGeneralCertificacionRespuestaRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ProgramaGeneralCertificacionRespuesta>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProgramaGeneralCertificacionRespuesta> Update(List<ProgramaGeneralCertificacionRespuesta> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ProgramaGeneralCertificacionRespuestaRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ProgramaGeneralCertificacionRespuesta>>(modelo);
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
                _unitOfWork.ProgramaGeneralCertificacionRespuestaRepository.Delete(listadoIds, usuario);
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
        /// Obtiene todos los registros de T_ProgramaGeneralCertificacionRespuesta
        /// </summary>
        /// <returns> List<ProgramaGeneralCertificacionRespuestaDTO> </returns>
        public IEnumerable<ProgramaGeneralCertificacionRespuestaDTO> ObtenerProgramaGeneralCertificacionRespuesta()
        {
            try
            {
                return _unitOfWork.ProgramaGeneralCertificacionRespuestaRepository.ObtenerProgramaGeneralCertificacionRespuesta();
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
        /// Obtiene la entidad ProgramaGeneralCertificacionRespuesta asociada a una Oportunidad y una Certificacion.
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <param name="idCertificacion">Id de la Certificacion</param>
        /// <returns> ProgramaGeneralCertificacionRespuesta </returns>
        public ProgramaGeneralCertificacionRespuesta ObtenerCertificacionRespuesta(int idOportunidad, int idCertificacion)
        {
            try
            {
                var respuesta = _unitOfWork.ProgramaGeneralCertificacionRespuestaRepository.ObtenerCertificacionRespuesta(idOportunidad, idCertificacion);
                var entidad = _mapper.Map<ProgramaGeneralCertificacionRespuesta>(respuesta);
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
        public ProgramaGeneralCertificacionRespuestaDTO GuardarCambios(ProgramaGeneralCertificacionRespuestaDTO item, string usuario)
        {
            try
            {
                ProgramaGeneralCertificacionRespuesta certificacionRespuesta = _unitOfWork.
                        ProgramaGeneralCertificacionRespuestaRepository.ObtenerPorIdOportunidadIdCertificacion(item.IdOportunidad, item.IdProgramaGeneralCertificacion);
                TProgramaGeneralCertificacionRespuestum respuesta;
                if (certificacionRespuesta != null && certificacionRespuesta.Id != 0)
                {
                    certificacionRespuesta.Respuesta = item.Respuesta;
                    certificacionRespuesta.UsuarioModificacion = usuario;
                    certificacionRespuesta.FechaModificacion = DateTime.Now;
                    respuesta = _unitOfWork.ProgramaGeneralCertificacionRespuestaRepository.Update(certificacionRespuesta);
                    _unitOfWork.Commit();
                }
                else
                {
                    certificacionRespuesta = new ProgramaGeneralCertificacionRespuesta()
                    {
                        IdOportunidad = item.IdOportunidad,
                        IdProgramaGeneralCertificacion = item.IdProgramaGeneralCertificacion,
                        Respuesta = item.Respuesta,
                        Estado = true,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                    };
                    respuesta = _unitOfWork.ProgramaGeneralCertificacionRespuestaRepository.Add(certificacionRespuesta);
                    _unitOfWork.Commit();
                }
                return _mapper.Map<ProgramaGeneralCertificacionRespuestaDTO>(respuesta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
