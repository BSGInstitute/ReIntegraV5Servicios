using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    /// Service: ProgramaGeneralMotivacionArgumentoService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 22/07/2022
    /// <summary>
    /// Gestión general de T_ProgramaGeneralMotivacionArgumento
    /// </summary>
    public class ProgramaGeneralMotivacionArgumentoService : IProgramaGeneralMotivacionArgumentoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ProgramaGeneralMotivacionArgumentoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TProgramaGeneralMotivacionArgumento, ProgramaGeneralMotivacionArgumento>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public ProgramaGeneralMotivacionArgumento Add(ProgramaGeneralMotivacionArgumento entidad)
        {
            try
            {
                var modelo = _unitOfWork.ProgramaGeneralMotivacionArgumentoRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ProgramaGeneralMotivacionArgumento>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ProgramaGeneralMotivacionArgumento Update(ProgramaGeneralMotivacionArgumento entidad)
        {
            try
            {
                var modelo = _unitOfWork.ProgramaGeneralMotivacionArgumentoRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ProgramaGeneralMotivacionArgumento>(modelo);
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
                _unitOfWork.ProgramaGeneralMotivacionArgumentoRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProgramaGeneralMotivacionArgumento> Add(List<ProgramaGeneralMotivacionArgumento> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ProgramaGeneralMotivacionArgumentoRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ProgramaGeneralMotivacionArgumento>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProgramaGeneralMotivacionArgumento> Update(List<ProgramaGeneralMotivacionArgumento> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ProgramaGeneralMotivacionArgumentoRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ProgramaGeneralMotivacionArgumento>>(modelo);
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
                _unitOfWork.ProgramaGeneralMotivacionArgumentoRepository.Delete(listadoIds, usuario);
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
        /// Fecha: 22/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_ProgramaGeneralMotivacionArgumento
        /// </summary>
        /// <returns> List<ProgramaGeneralMotivacionArgumentoDTO> </returns>
        public IEnumerable<ProgramaGeneralMotivacionArgumentoDTO> ObtenerProgramaGeneralMotivacionArgumento()
        {
            try
            {
                return _unitOfWork.ProgramaGeneralMotivacionArgumentoRepository.ObtenerProgramaGeneralMotivacionArgumento();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 22/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_ProgramaGeneralMotivacionArgumento para mostrarse en combo.
        /// </summary>
        /// <returns> List<ProgramaGeneralMotivacionArgumentoComboDTO> </returns>
        public IEnumerable<ProgramaGeneralMotivacionArgumentoComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.ProgramaGeneralMotivacionArgumentoRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 22/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene MotivacionArgumentos asociados a un Id Motivacion.
        /// </summary>
        /// <param name="idMotivacion">Id de Motivacion</param>
        /// <returns> List<ProgramaGeneralMotivacionArgumentoComboDTO> </returns>
        public IEnumerable<ProgramaGeneralMotivacionArgumentoComboDTO> ObtenerProgramaGeneralMotivacionArgumentoAgendaPorIdMotivacion(int idMotivacion)
        {
            try
            {
                return _unitOfWork.ProgramaGeneralMotivacionArgumentoRepository
                    .ObtenerProgramaGeneralMotivacionArgumentoAgendaPorIdMotivacion(idMotivacion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
