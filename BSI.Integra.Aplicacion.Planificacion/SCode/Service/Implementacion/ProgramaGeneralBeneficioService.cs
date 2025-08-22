using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    /// Service: ProgramaGeneralBeneficioService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 26/07/2022
    /// <summary>
    /// Gestión general de T_ProgramaGeneralBeneficio
    /// </summary>
    public class ProgramaGeneralBeneficioService : IProgramaGeneralBeneficioService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ProgramaGeneralBeneficioService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TProgramaGeneralBeneficio, ProgramaGeneralBeneficio>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public ProgramaGeneralBeneficio Add(ProgramaGeneralBeneficio entidad)
        {
            try
            {
                var modelo = _unitOfWork.ProgramaGeneralBeneficioRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ProgramaGeneralBeneficio>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ProgramaGeneralBeneficio Update(ProgramaGeneralBeneficio entidad)
        {
            try
            {
                var modelo = _unitOfWork.ProgramaGeneralBeneficioRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ProgramaGeneralBeneficio>(modelo);
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
                _unitOfWork.ProgramaGeneralBeneficioRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProgramaGeneralBeneficio> Add(List<ProgramaGeneralBeneficio> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ProgramaGeneralBeneficioRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ProgramaGeneralBeneficio>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProgramaGeneralBeneficio> Update(List<ProgramaGeneralBeneficio> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ProgramaGeneralBeneficioRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ProgramaGeneralBeneficio>>(modelo);
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
                _unitOfWork.ProgramaGeneralBeneficioRepository.Delete(listadoIds, usuario);
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
        /// Fecha: 26/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_ProgramaGeneralBeneficio
        /// </summary>
        /// <returns> List<ProgramaGeneralBeneficioDTO> </returns>
        public IEnumerable<ProgramaGeneralBeneficioDTO> ObtenerProgramaGeneralBeneficio()
        {
            try
            {
                return _unitOfWork.ProgramaGeneralBeneficioRepository.ObtenerProgramaGeneralBeneficio();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 26/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_ProgramaGeneralBeneficio para mostrarse en combo.
        /// </summary>
        /// <returns> List<ProgramaGeneralBeneficioComboDTO> </returns>
        public IEnumerable<ProgramaGeneralBeneficioComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.ProgramaGeneralBeneficioRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 26/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Beneficios asociados a una Oportunidad.
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> List<ProgramaGeneralBeneficioOportunidadDTO> </returns>
        public IEnumerable<ProgramaGeneralBeneficioOportunidadDTO> ObtenerProgramaGeneralBeneficioPorIdOportunidad(int idOportunidad)
        {
            try
            {
                return _unitOfWork.ProgramaGeneralBeneficioRepository.ObtenerProgramaGeneralBeneficioPorIdOportunidad(idOportunidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
