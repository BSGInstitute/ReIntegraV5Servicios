


using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Comercial.Service.Implementacion
{
    /// Service: ProgramaGeneralBeneficioRespuestaService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 12/08/2022
    /// <summary>
    /// Gestión general de T_ProgramaGeneralBeneficioRespuesta
    /// </summary>
    public class ProgramaGeneralBeneficioRespuestaService : IProgramaGeneralBeneficioRespuestaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public ProgramaGeneralBeneficioRespuestaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TProgramaGeneralBeneficioRespuestum, ProgramaGeneralBeneficioRespuesta>(MemberList.None).ReverseMap();
                    cfg.CreateMap<ProgramaGeneralBeneficioRespuestaDTO, ProgramaGeneralBeneficioRespuesta>(MemberList.None);
                }
            );
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public ProgramaGeneralBeneficioRespuesta Add(ProgramaGeneralBeneficioRespuesta entidad)
        {
            try
            {
                var modelo = _unitOfWork.ProgramaGeneralBeneficioRespuestaRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ProgramaGeneralBeneficioRespuesta>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ProgramaGeneralBeneficioRespuesta Update(ProgramaGeneralBeneficioRespuesta entidad)
        {
            try
            {
                var modelo = _unitOfWork.ProgramaGeneralBeneficioRespuestaRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ProgramaGeneralBeneficioRespuesta>(modelo);
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
                _unitOfWork.ProgramaGeneralBeneficioRespuestaRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProgramaGeneralBeneficioRespuesta> Add(List<ProgramaGeneralBeneficioRespuesta> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ProgramaGeneralBeneficioRespuestaRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ProgramaGeneralBeneficioRespuesta>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProgramaGeneralBeneficioRespuesta> Update(List<ProgramaGeneralBeneficioRespuesta> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ProgramaGeneralBeneficioRespuestaRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ProgramaGeneralBeneficioRespuesta>>(modelo);
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
                _unitOfWork.ProgramaGeneralBeneficioRespuestaRepository.Delete(listadoIds, usuario);
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
        /// Fecha: 12/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_ProgramaGeneralBeneficioRespuesta
        /// </summary>
        /// <returns> List<ProgramaGeneralBeneficioRespuestaDTO> </returns>
        public IEnumerable<ProgramaGeneralBeneficioRespuestaDTO> ObtenerProgramaGeneralBeneficioRespuesta()
        {
            try
            {
                return _unitOfWork.ProgramaGeneralBeneficioRespuestaRepository.ObtenerProgramaGeneralBeneficioRespuesta();
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
        /// Obtiene ProgramaGeneralBeneficioRespuesta asociado a una Oportunidad y Beneficio
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <param name="idBeneficio">Id del Beneficio asociado a un Programa General</param>
        /// <returns> ProgramaGeneralBeneficioRespuesta </returns>
        public ProgramaGeneralBeneficioRespuesta ObtenerPorIdOportunidadIdBeneficio(int idOportunidad, int idBeneficio)
        {
            try
            {
                var beneficioRespuesta = _unitOfWork.ProgramaGeneralBeneficioRespuestaRepository.ObtenerPorIdOportunidadIdBeneficio(idOportunidad, idBeneficio);
                var entidad = _mapper.Map<ProgramaGeneralBeneficioRespuesta>(beneficioRespuesta);
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
        public bool GuardarCambiosAgenda(ProgramaGeneralBeneficioRespuestaDTO obj, string usuario)
        {
            try
            {
                ProgramaGeneralBeneficioRespuesta beneficioRespuesta = _unitOfWork.ProgramaGeneralBeneficioRespuestaRepository.ObtenerPorIdOportunidadIdBeneficio(obj.IdOportunidad, obj.IdProgramaGeneralBeneficio);

                if (beneficioRespuesta != null && beneficioRespuesta.Id != 0)
                {
                    beneficioRespuesta.Respuesta = obj.Respuesta;
                    beneficioRespuesta.UsuarioModificacion = usuario;
                    beneficioRespuesta.FechaModificacion = DateTime.Now;
                    _unitOfWork.ProgramaGeneralBeneficioRespuestaRepository.Update(beneficioRespuesta);
                }
                else
                {
                    beneficioRespuesta = new ProgramaGeneralBeneficioRespuesta()
                    {
                        IdOportunidad = obj.IdOportunidad,
                        IdProgramaGeneralBeneficio = obj.IdProgramaGeneralBeneficio,
                        Respuesta = obj.Respuesta,
                        Estado = true,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                    _unitOfWork.ProgramaGeneralBeneficioRespuestaRepository.Add(beneficioRespuesta);
                }
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
