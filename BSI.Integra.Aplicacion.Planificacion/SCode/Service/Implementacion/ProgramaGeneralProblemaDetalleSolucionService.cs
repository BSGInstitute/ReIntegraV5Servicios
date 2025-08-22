using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    /// Service: ProgramaGeneralProblemaDetalleSolucionService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 25/07/2022
    /// <summary>
    /// Gestión general de T_ProgramaGeneralProblemaDetalleSolucion
    /// </summary>
    public class ProgramaGeneralProblemaDetalleSolucionService : IProgramaGeneralProblemaDetalleSolucionService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public ProgramaGeneralProblemaDetalleSolucionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TProgramaGeneralProblemaDetalleSolucion, ProgramaGeneralProblemaDetalleSolucion>(MemberList.None).ReverseMap();
                    cfg.CreateMap<ProgramaGeneralProblemaDetalleSolucionDTO, ComboDTO>(MemberList.None);
                }
            );
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public ProgramaGeneralProblemaDetalleSolucion Add(ProgramaGeneralProblemaDetalleSolucion entidad)
        {
            try
            {
                var modelo = _unitOfWork.ProgramaGeneralProblemaDetalleSolucionRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ProgramaGeneralProblemaDetalleSolucion>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ProgramaGeneralProblemaDetalleSolucion Update(ProgramaGeneralProblemaDetalleSolucion entidad)
        {
            try
            {
                var modelo = _unitOfWork.ProgramaGeneralProblemaDetalleSolucionRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ProgramaGeneralProblemaDetalleSolucion>(modelo);
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
                _unitOfWork.ProgramaGeneralProblemaDetalleSolucionRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProgramaGeneralProblemaDetalleSolucion> Add(List<ProgramaGeneralProblemaDetalleSolucion> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ProgramaGeneralProblemaDetalleSolucionRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ProgramaGeneralProblemaDetalleSolucion>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProgramaGeneralProblemaDetalleSolucion> Update(List<ProgramaGeneralProblemaDetalleSolucion> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ProgramaGeneralProblemaDetalleSolucionRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ProgramaGeneralProblemaDetalleSolucion>>(modelo);
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
                _unitOfWork.ProgramaGeneralProblemaDetalleSolucionRepository.Delete(listadoIds, usuario);
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
        /// Fecha: 25/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_ProgramaGeneralProblemaDetalleSolucion
        /// </summary>
        /// <returns> List<ProgramaGeneralProblemaDetalleSolucionDTO> </returns>
        public IEnumerable<ProgramaGeneralProblemaDetalleSolucionDTO> ObtenerProgramaGeneralProblemaDetalleSolucion()
        {
            try
            {
                return _unitOfWork.ProgramaGeneralProblemaDetalleSolucionRepository.ObtenerProgramaGeneralProblemaDetalleSolucion();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 25/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_ProgramaGeneralProblemaDetalleSolucion para mostrarse en combo.
        /// </summary>
        /// <returns> List<ProgramaGeneralProblemaDetalleSolucionComboDTO> </returns>
        public IEnumerable<ProgramaGeneralProblemaDetalleSolucionComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.ProgramaGeneralProblemaDetalleSolucionRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 25/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene DetalleSolucion de Problemas basado en Id Problema y Id Oportunidad.
        /// </summary>
        /// <param name="idProblema">Id del Problema</param>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> List<ProgramaGeneralProblemaDetalleSolucionAgendaDTO> </returns>
        public IEnumerable<ProgramaGeneralProblemaDetalleSolucionAgendaDTO> ObtenerProgramaGeneralProblemaDetalleSolucionParaAgenda(int idProblema, int idOportunidad)
        {
            try
            {
                return _unitOfWork.ProgramaGeneralProblemaDetalleSolucionRepository.ObtenerProgramaGeneralProblemaDetalleSolucionParaAgenda(idProblema, idOportunidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 25/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene DetalleSolucion de Problemas basado en Id Problema y Id Oportunidad.
        /// </summary>
        /// <param name="idProblema">Id del Problema</param>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> List<ProgramaGeneralProblemaDetalleSolucionAgendaDTO> </returns>
        public IEnumerable<ProgramaGeneralProblemaDetalleSolucionAgendaNuevaAgendaDTO> ObtenerProgramaGeneralProblemaDetalleSolucionParaAgendaNuevaAgenda(int idProblema, int idOportunidad)
        {
            try
            {
                return _unitOfWork.ProgramaGeneralProblemaDetalleSolucionRepository.ObtenerProgramaGeneralProblemaDetalleSolucionParaAgendaNuevaAgenda(idProblema, idOportunidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 05/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_ProgramaGeneralProblemaDetalleSolucion asociados a un ProgramaGeneralProblema
        /// </summary>
        /// <param name="idProblema">Id del Problema</param>
        /// <returns> List<ProgramaGeneralProblemaDetalleSolucionDTO> </returns>
        public IEnumerable<ProgramaGeneralProblemaDetalleSolucionDTO> ObtenerProblemaDetalleSolucionPorIdProblema(int idProblema)
        {
            try
            {
                return _unitOfWork.ProgramaGeneralProblemaDetalleSolucionRepository.ObtenerProblemaDetalleSolucionPorIdProblema(idProblema);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 05/08/2022
        /// Version: 1.0
        /// <summary>
        /// Existe problema asociado al Id.
        /// </summary>
        /// <param name="idDetalleSolucion">Id de ProgramaGeneralProblemaDetalleSolucion</param>
        /// <returns> bool </returns>
        public bool ExistePoblemaPorId(int idDetalleSolucion)
        {
            try
            {
                return _unitOfWork.ProgramaGeneralProblemaRepository.Exist(idDetalleSolucion);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
