using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Comercial.Service.Implementacion
{
    /// Service: ProgramaGeneralProblemaDetalleSolucionRespuestaService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 13/08/2022
    /// <summary>
    /// Gestión general de T_ProgramaGeneralProblemaDetalleSolucionRespuesta
    /// </summary>
    public class ProgramaGeneralProblemaDetalleSolucionRespuestaService : IProgramaGeneralProblemaDetalleSolucionRespuestaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public ProgramaGeneralProblemaDetalleSolucionRespuestaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TProgramaGeneralProblemaDetalleSolucionRespuestum, ProgramaGeneralProblemaDetalleSolucionRespuesta>(MemberList.None).ReverseMap();
                    cfg.CreateMap<TProgramaGeneralProblemaDetalleSolucionRespuestum, ProgramaGeneralProblemaDetalleSolucionRespuestaDTO>(MemberList.None).ReverseMap();
                    cfg.CreateMap<ProgramaGeneralProblemaDetalleSolucionRespuestaDTO, ProgramaGeneralProblemaDetalleSolucionRespuesta>(MemberList.None);
                }
            );
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public ProgramaGeneralProblemaDetalleSolucionRespuesta Add(ProgramaGeneralProblemaDetalleSolucionRespuesta entidad)
        {
            try
            {
                var modelo = _unitOfWork.ProgramaGeneralProblemaDetalleSolucionRespuestaRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ProgramaGeneralProblemaDetalleSolucionRespuesta>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ProgramaGeneralProblemaDetalleSolucionRespuesta Update(ProgramaGeneralProblemaDetalleSolucionRespuesta entidad)
        {
            try
            {
                var modelo = _unitOfWork.ProgramaGeneralProblemaDetalleSolucionRespuestaRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ProgramaGeneralProblemaDetalleSolucionRespuesta>(modelo);
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
                _unitOfWork.ProgramaGeneralProblemaDetalleSolucionRespuestaRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProgramaGeneralProblemaDetalleSolucionRespuesta> Add(List<ProgramaGeneralProblemaDetalleSolucionRespuesta> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ProgramaGeneralProblemaDetalleSolucionRespuestaRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ProgramaGeneralProblemaDetalleSolucionRespuesta>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProgramaGeneralProblemaDetalleSolucionRespuesta> Update(List<ProgramaGeneralProblemaDetalleSolucionRespuesta> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ProgramaGeneralProblemaDetalleSolucionRespuestaRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ProgramaGeneralProblemaDetalleSolucionRespuesta>>(modelo);
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
                _unitOfWork.ProgramaGeneralProblemaDetalleSolucionRespuestaRepository.Delete(listadoIds, usuario);
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
        /// Obtiene todos los registros de T_ProgramaGeneralProblemaDetalleSolucionRespuesta
        /// </summary>
        /// <returns> List<ProgramaGeneralProblemaDetalleSolucionRespuestaDTO> </returns>
        public IEnumerable<ProgramaGeneralProblemaDetalleSolucionRespuesta> ObtenerTodo()
        {
            try
            {
                return _unitOfWork.ProgramaGeneralProblemaDetalleSolucionRespuestaRepository.ObtenerTodo();
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
        public bool GuardarCambiosAgenda(ProgramaGeneralProblemaDetalleSolucionRespuestaDTO obj, string usuario)
        {
            try
            {
                ProgramaGeneralProblemaDetalleSolucionRespuesta problemaRespuesta = _unitOfWork.ProgramaGeneralProblemaDetalleSolucionRespuestaRepository.ObtenerPorIdOportunidadIdProblemaSolucion(obj.IdOportunidad, obj.IdProgramaGeneralProblemaDetalleSolucion);

                if (problemaRespuesta != null && problemaRespuesta.Id != 0)
                {
                    problemaRespuesta.EsSeleccionado = obj.EsSeleccionado;
                    problemaRespuesta.EsSolucionado = obj.EsSolucionado;
                    problemaRespuesta.UsuarioModificacion = usuario;
                    problemaRespuesta.FechaModificacion = DateTime.Now;
                    _unitOfWork.ProgramaGeneralProblemaDetalleSolucionRespuestaRepository.Update(problemaRespuesta);
                }
                else
                {
                    problemaRespuesta = new ProgramaGeneralProblemaDetalleSolucionRespuesta()
                    {
                        IdOportunidad = obj.IdOportunidad,
                        IdProgramaGeneralProblemaDetalleSolucion = obj.IdProgramaGeneralProblemaDetalleSolucion,
                        EsSeleccionado = obj.EsSeleccionado,
                        EsSolucionado = obj.EsSolucionado,
                        Estado = true,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = DateTime.Now,  
                        FechaModificacion = DateTime.Now,
                    };
                    _unitOfWork.ProgramaGeneralProblemaDetalleSolucionRespuestaRepository.Add(problemaRespuesta);
                }
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
