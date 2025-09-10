using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: ConfiguracionBeneficioProgramaGeneralService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 11/07/2022
    /// <summary>
    /// Gestión general de T_ConfiguracionBeneficioProgramaGeneral
    /// </summary>
    public class ConfiguracionBeneficioProgramaGeneralService : IConfiguracionBeneficioProgramaGeneralService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ConfiguracionBeneficioProgramaGeneralService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TConfiguracionBeneficioProgramaGeneral, ConfiguracionBeneficioProgramaGeneral>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public ConfiguracionBeneficioProgramaGeneral Add(ConfiguracionBeneficioProgramaGeneral entidad)
        {
            try
            {
                var modelo = _unitOfWork.ConfiguracionBeneficioProgramaGeneralRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ConfiguracionBeneficioProgramaGeneral>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ConfiguracionBeneficioProgramaGeneral Update(ConfiguracionBeneficioProgramaGeneral entidad)
        {
            try
            {
                var modelo = _unitOfWork.ConfiguracionBeneficioProgramaGeneralRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ConfiguracionBeneficioProgramaGeneral>(modelo);
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
                _unitOfWork.ConfiguracionBeneficioProgramaGeneralRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ConfiguracionBeneficioProgramaGeneral> Add(List<ConfiguracionBeneficioProgramaGeneral> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ConfiguracionBeneficioProgramaGeneralRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ConfiguracionBeneficioProgramaGeneral>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ConfiguracionBeneficioProgramaGeneral> Update(List<ConfiguracionBeneficioProgramaGeneral> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ConfiguracionBeneficioProgramaGeneralRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ConfiguracionBeneficioProgramaGeneral>>(modelo);
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
                _unitOfWork.ConfiguracionBeneficioProgramaGeneralRepository.Delete(listadoIds, usuario);
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
        /// Fecha: 11/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_ConfiguracionBeneficioProgramaGeneral para mostrarse en combo.
        /// </summary>
        /// <returns> List<ConfiguracionBeneficioProgramaGeneralComboDTO> </returns>
        public IEnumerable<ConfiguracionBeneficioProgramaGeneralComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.ConfiguracionBeneficioProgramaGeneralRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtener Programa General Configuracion Beneficios segun ciertos filtros
        /// </summary>
        /// <param name="idPgeneral">Id del Programa General</param>
        /// <param name="idPais">Id del Pais</param>
        /// <param name="idPaquete">Id del Paquete</param>
        /// <returns> List<string> </returns> 
        public List<string> ObtenerDescripcionPGeneralConfiguracionBeneficios(int idPgeneral, int? idPais, int? idPaquete)
        {
            try
            {
                var descripcionBeneficios = new List<string>();
                var beneficios = _unitOfWork.ConfiguracionBeneficioProgramaGeneralRepository.ObtenerPGeneralConfiguracionBeneficios(idPgeneral);
                if (idPais != null)
                {
                    descripcionBeneficios = beneficios.Where(b => b.Versiones.Any(p => p == idPaquete) && b.Paises.Any(p => p == idPais)).Select(b => b.Descripcion).ToList();
                }
                return descripcionBeneficios;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtener beneficios del programa general Tipo 1 version 2 Internacional.
        /// </summary>
        /// <param name="idPGeneral">Id de Programa General</param>
        /// <returns> List<BeneficioDTO> </returns>
        public List<BeneficioDTO> ObtenerBeneficiosPGeneralTipo1V2Internacional(int idPGeneral)
        {
            try
            {
                return _unitOfWork.ConfiguracionBeneficioProgramaGeneralRepository.ObtenerBeneficiosPGeneralTipo1V2Internacional(idPGeneral);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BeneficioDTOjson> ObtenerBeneficiosPGeneralTipo1V2Internacionaljson(int idPGeneral)
        {
            try
            {
                return _unitOfWork.ConfiguracionBeneficioProgramaGeneralRepository.ObtenerBeneficiosPGeneralTipo1V2Internacionaljson(idPGeneral);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtener beneficios del programa general Tipo 1 version 2
        /// </summary>
        /// <param name="idPGeneral">Id Programa General </param>
        /// <param name="codigoPais">Codigo pais </param>
        /// <returns> List<BeneficioDTO> </returns>
        public List<BeneficioDTO> ObtenerBeneficiosPGeneralTipo1V2(int idPGeneral, int codigoPais = 0)
        {
            try
            {
                return _unitOfWork.ConfiguracionBeneficioProgramaGeneralRepository.ObtenerBeneficiosPGeneralTipo1V2(idPGeneral, codigoPais);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BeneficioDTOjson> ObtenerBeneficiosPGeneralTipo1V2json(int idPGeneral, int codigoPais = 0)
        {
            try
            {
                return _unitOfWork.ConfiguracionBeneficioProgramaGeneralRepository.ObtenerBeneficiosPGeneralTipo1V2json(idPGeneral, codigoPais);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los beneficios por programa general y pais tipo 1
        /// </summary>
        /// <param name="idPGeneral">Id Programa General </param>
        /// <param name="codigoPais">Codigo pais </param>
        /// <returns> List<BeneficioDTO> </returns>
        public List<BeneficioDTO> ObtenerBeneficiosPGeneralTipo1(int idPGeneral, int codigoPais = 0)
        {
            try
            {
                return _unitOfWork.ConfiguracionBeneficioProgramaGeneralRepository.ObtenerBeneficiosPGeneralTipo1(idPGeneral, codigoPais);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BeneficioDTOjson> ObtenerBeneficiosPGeneralTipo1json(int idPGeneral, int codigoPais = 0)
        {
            try
            {
                return _unitOfWork.ConfiguracionBeneficioProgramaGeneralRepository.ObtenerBeneficiosPGeneralTipo1json(idPGeneral, codigoPais);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el titulo de un beneficio por programa general tipo 2
        /// </summary>
        /// <param name="idPGeneral">Id Programa General </param>
        /// <returns> BeneficioDTO </returns>
        public BeneficioDTO ObtenerBeneficiosPGeneralTipo2(int idPGeneral)
        {
            try
            {
                return _unitOfWork.ConfiguracionBeneficioProgramaGeneralRepository.ObtenerBeneficiosPGeneralTipo2(idPGeneral);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BeneficioDTOjson ObtenerBeneficiosPGeneralTipo2json(int idPGeneral)
        {
            try
            {
                return _unitOfWork.ConfiguracionBeneficioProgramaGeneralRepository.ObtenerBeneficiosPGeneralTipo2json(idPGeneral);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Gilmer Quispe.
        /// Fecha: 26/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los detalles de Configuracion Beneficio PGeneral 
        /// </summary>
        /// <param name="idBeneficio">Id Beneficio</param>
        /// <param name="idPGeneral">Id Programa General </param>
        /// <returns> BeneficioDetalleRequisitoDTO </returns>
        public BeneficioDetalleRequisitoDTO BeneficioDetalleRequisitoPorPGeneralYBeneficio(int idBeneficio, int idPGeneral)
        {
            try
            {
                return _unitOfWork.ConfiguracionBeneficioProgramaGeneralRepository.ObtenerBeneficioDetalleRequisitoPorPGeneralYBeneficio(idBeneficio, idPGeneral);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
