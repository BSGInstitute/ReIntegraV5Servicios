using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    /// Service: ProgramaGeneralBeneficioArgumentoService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: Fecha: 22/06/2022
    /// <summary>
    /// Gestión general de T_ProgramaGeneralBeneficioArgumento
    /// </summary>
    public class ProgramaGeneralBeneficioArgumentoService : IProgramaGeneralBeneficioArgumentoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ProgramaGeneralBeneficioArgumentoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TProgramaGeneralBeneficioArgumento, ProgramaGeneralBeneficioArgumento>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public ProgramaGeneralBeneficioArgumento Add(ProgramaGeneralBeneficioArgumento entidad)
        {
            try
            {
                var modelo = _unitOfWork.ProgramaGeneralBeneficioArgumentoRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ProgramaGeneralBeneficioArgumento>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ProgramaGeneralBeneficioArgumento Update(ProgramaGeneralBeneficioArgumento entidad)
        {
            try
            {
                var modelo = _unitOfWork.ProgramaGeneralBeneficioArgumentoRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ProgramaGeneralBeneficioArgumento>(modelo);
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
                _unitOfWork.ProgramaGeneralBeneficioArgumentoRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProgramaGeneralBeneficioArgumento> Add(List<ProgramaGeneralBeneficioArgumento> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ProgramaGeneralBeneficioArgumentoRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ProgramaGeneralBeneficioArgumento>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProgramaGeneralBeneficioArgumento> Update(List<ProgramaGeneralBeneficioArgumento> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ProgramaGeneralBeneficioArgumentoRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ProgramaGeneralBeneficioArgumento>>(modelo);
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
                _unitOfWork.ProgramaGeneralBeneficioArgumentoRepository.Delete(listadoIds, usuario);
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
        /// Fecha: 22/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_ProgramaGeneralBeneficioArgumento
        /// </summary>
        /// <returns> List<ProgramaGeneralBeneficioArgumentoDTO> </returns>
        public IEnumerable<ProgramaGeneralBeneficioArgumentoDTO> ObtenerProgramaGeneralBeneficioArgumento()
        {
            try
            {
                return _unitOfWork.ProgramaGeneralBeneficioArgumentoRepository.ObtenerProgramaGeneralBeneficioArgumento();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 22/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_ProgramaGeneralBeneficioArgumento para mostrarse en combo.
        /// </summary>
        /// <returns> List<ProgramaGeneralBeneficioArgumentoComboDTO> </returns>
        public IEnumerable<ProgramaGeneralBeneficioArgumentoComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.ProgramaGeneralBeneficioArgumentoRepository.ObtenerCombo();
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
        /// Obtiene registros de T_ProgramaGeneralBeneficioArgumento asociados a un ProgramaGeneralBeneficio.
        /// </summary>
        /// <param name="idBeneficio">Id de ProgramaGeneralBeneficio</param>

        /// <returns> List<ProgramaGeneralBeneficioArgumentoComboDTO> </returns>
        public IEnumerable<ProgramaGeneralBeneficioArgumentoAgendaDTO> ObtenerProgramaGeneralBeneficioArgumentoPorIdBeneficio(int idBeneficio)
        {
            try
            {
                return _unitOfWork.ProgramaGeneralBeneficioArgumentoRepository.ObtenerProgramaGeneralBeneficioArgumentoPorIdBeneficio(idBeneficio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
