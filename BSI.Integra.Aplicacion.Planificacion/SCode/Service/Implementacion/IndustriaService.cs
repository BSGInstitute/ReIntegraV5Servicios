using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    /// Service: IndustriaService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 13/06/2022
    /// <summary>
    /// Gestión general de T_Industria
    /// </summary>
    public class IndustriaService : IIndustriaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public IndustriaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TIndustrium, Industria>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public Industria Add(Industria entidad)
        {
            try
            {
                var modelo = _unitOfWork.IndustriaRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<Industria>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Industria Update(Industria entidad)
        {
            try
            {
                var modelo = _unitOfWork.IndustriaRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<Industria>(modelo);
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
                _unitOfWork.IndustriaRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Industria> Add(List<Industria> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.IndustriaRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<Industria>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Industria> Update(List<Industria> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.IndustriaRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<Industria>>(modelo);
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
                _unitOfWork.IndustriaRepository.Delete(listadoIds, usuario);
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
        /// Fecha: 13/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_Industria para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.IndustriaRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 13/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_Industria para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerComboTiempoExperiencia()
        {
            try
            {
                return _unitOfWork.IndustriaRepository.ObtenerComboTiempoExperiencia();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 13/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_Industria para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerComboTamanioEmpresa()
        {
            try
            {
                return _unitOfWork.IndustriaRepository.ObtenerComboTamanioEmpresa();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
