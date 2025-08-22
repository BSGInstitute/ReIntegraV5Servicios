using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    /// Service: ProgramaGeneralCertificacionArgumentoService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 22/07/2022
    /// <summary>
    /// Gestión general de T_ProgramaGeneralCertificacionArgumento
    /// </summary>
    public class ProgramaGeneralCertificacionArgumentoService : IProgramaGeneralCertificacionArgumentoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ProgramaGeneralCertificacionArgumentoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TProgramaGeneralCertificacionArgumento, ProgramaGeneralCertificacionArgumento>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public ProgramaGeneralCertificacionArgumento Add(ProgramaGeneralCertificacionArgumento entidad)
        {
            try
            {
                var modelo = _unitOfWork.ProgramaGeneralCertificacionArgumentoRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ProgramaGeneralCertificacionArgumento>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ProgramaGeneralCertificacionArgumento Update(ProgramaGeneralCertificacionArgumento entidad)
        {
            try
            {
                var modelo = _unitOfWork.ProgramaGeneralCertificacionArgumentoRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ProgramaGeneralCertificacionArgumento>(modelo);
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
                _unitOfWork.ProgramaGeneralCertificacionArgumentoRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProgramaGeneralCertificacionArgumento> Add(List<ProgramaGeneralCertificacionArgumento> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ProgramaGeneralCertificacionArgumentoRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ProgramaGeneralCertificacionArgumento>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProgramaGeneralCertificacionArgumento> Update(List<ProgramaGeneralCertificacionArgumento> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ProgramaGeneralCertificacionArgumentoRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ProgramaGeneralCertificacionArgumento>>(modelo);
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
                _unitOfWork.ProgramaGeneralCertificacionArgumentoRepository.Delete(listadoIds, usuario);
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
        /// Obtiene todos los registros de T_ProgramaGeneralCertificacionArgumento
        /// </summary>
        /// <returns> List<ProgramaGeneralCertificacionArgumentoDTO> </returns>
        public IEnumerable<ProgramaGeneralCertificacionArgumentoDTO> ObtenerProgramaGeneralCertificacionArgumento()
        {
            try
            {
                return _unitOfWork.ProgramaGeneralCertificacionArgumentoRepository.ObtenerProgramaGeneralCertificacionArgumento();
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
        /// Obtiene registros de T_ProgramaGeneralCertificacionArgumento para mostrarse en combo.
        /// </summary>
        /// <returns> List<ProgramaGeneralCertificacionArgumentoComboDTO> </returns>
        public IEnumerable<ProgramaGeneralCertificacionArgumentoComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.ProgramaGeneralCertificacionArgumentoRepository.ObtenerCombo();
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
        /// Obtiene registros de T_ProgramaGeneralCertificacionArgumento asociados a un Id Certificacion.
        /// </summary>
        /// <param name="idCertificacion">Id de Certificacion</param>
        /// <returns> List<ProgramaGeneralCertificacionArgumentoComboDTO> </returns>
        public IEnumerable<ProgramaGeneralCertificacionArgumentoComboDTO> ObtenerProgramaGeneralCertificacionArgumentoAgendaPorIdCertificacion(int idCertificacion)
        {
            try
            {
                return _unitOfWork.ProgramaGeneralCertificacionArgumentoRepository.
                    ObtenerProgramaGeneralCertificacionArgumentoAgendaPorIdCertificacion(idCertificacion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
