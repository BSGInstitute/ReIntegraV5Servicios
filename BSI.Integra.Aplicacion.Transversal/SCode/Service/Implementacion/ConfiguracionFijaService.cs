using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: ConfiguracionFijaService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 02/08/2022
    /// <summary>
    /// Gestión general de T_ConfiguracionFija
    /// </summary>
    public class ConfiguracionFijaService : IConfiguracionFijaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ConfiguracionFijaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TConfiguracionFija, ConfiguracionFija>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public ConfiguracionFija Add(ConfiguracionFija entidad)
        {
            try
            {
                var modelo = _unitOfWork.ConfiguracionFijaRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ConfiguracionFija>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ConfiguracionFija Update(ConfiguracionFija entidad)
        {
            try
            {
                var modelo = _unitOfWork.ConfiguracionFijaRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ConfiguracionFija>(modelo);
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
                _unitOfWork.ConfiguracionFijaRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ConfiguracionFija> Add(List<ConfiguracionFija> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ConfiguracionFijaRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ConfiguracionFija>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ConfiguracionFija> Update(List<ConfiguracionFija> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ConfiguracionFijaRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ConfiguracionFija>>(modelo);
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
                _unitOfWork.ConfiguracionFijaRepository.Delete(listadoIds, usuario);
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
        /// Fecha: 02/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_ConfiguracionFija
        /// </summary>
        /// <returns> List<ConfiguracionFijaDTO> </returns>
        public IEnumerable<ConfiguracionFijaDTO> ObtenerConfiguracionFija()
        {
            try
            {
                return _unitOfWork.ConfiguracionFijaRepository.ObtenerConfiguracionFija();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 02/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_ConfiguracionFija para mostrarse en combo.
        /// </summary>
        /// <returns> List<ConfiguracionFijaComboDTO> </returns>
        public IEnumerable<ConfiguracionFijaComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.ConfiguracionFijaRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Rodrigo Montesinos Paredes.
        /// Fecha: 13/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de De valor estatico
        /// </summary>
        /// <returns> List<ValorEstaticoDTO> </returns>
        public List<ValorEstaticoDTO> ObtenerTodosLosRegistros()
        {
            try
            {
                return _unitOfWork.ConfiguracionFijaRepository.ObtenerTodosLosRegistros();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
