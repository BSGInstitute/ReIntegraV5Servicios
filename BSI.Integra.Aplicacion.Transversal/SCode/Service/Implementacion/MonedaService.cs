using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: MonedaService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 21/06/2022
    /// <summary>
    /// Gestión general de T_Moneda
    /// </summary>
    public class MonedaService : IMonedaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public MonedaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TMonedum, Moneda>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public Moneda Add(Moneda entidad)
        {
            try
            {
                var modelo = _unitOfWork.MonedaRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<Moneda>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Moneda Update(Moneda entidad)
        {
            try
            {
                var modelo = _unitOfWork.MonedaRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<Moneda>(modelo);
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
                _unitOfWork.MonedaRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Moneda> Add(List<Moneda> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.MonedaRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<Moneda>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Moneda> Update(List<Moneda> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.MonedaRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<Moneda>>(modelo);
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
                _unitOfWork.MonedaRepository.Delete(listadoIds, usuario);
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
        /// Fecha: 21/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_Moneda
        /// </summary>
        /// <returns> List<MonedaDTO> </returns>
        public IEnumerable<MonedaDTO> ObtenerMoneda()
        {
            try
            {
                return _unitOfWork.MonedaRepository.ObtenerMoneda();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 21/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_Moneda para mostrarse en combo.
        /// </summary>
        /// <returns> List<MonedaComboDTO> </returns>
        public IEnumerable<MonedaComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.MonedaRepository.ObtenerCombo();
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
        /// Obtiene el Codigo Moneda asociado al Id del Alumno.
        /// </summary>
        /// <param name="idAlumno">Id del Alumno</param>
        /// <returns> ValorStringDTO </returns>
        public StringDTO ObtenerCodigoMonedaPorIdAlumno(int idAlumno)
        {
            try
            {
                return _unitOfWork.MonedaRepository.ObtenerCodigoMonedaPorIdAlumno(idAlumno);
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
        /// Obtiene el Id, Nombre Plural y Simbolo de los registros de T_Moneda
        /// </summary>
        /// <returns> MonedaNombrePluralSimboloDTO </returns>
        public IEnumerable<MonedaNombrePluralSimboloDTO> ObtenerMonedaNombrePluralSimbolo()
        {
            try
            {
                return _unitOfWork.MonedaRepository.ObtenerMonedaNombrePluralSimbolo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 03/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Codigo y Cambios con Dolar de Monedas
        /// </summary>
        /// <returns> List<MonedaCodigoCambioDTO> </returns>
        public IEnumerable<MonedaCodigoCambioDTO> ObtenerMonedaCodigoCambio()
        {
            try
            {
                return _unitOfWork.MonedaRepository.ObtenerMonedaCodigoCambio();
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
        /// Obtiene informacion de Moneda asociados a un Id para DocumentoAgenda
        /// </summary>
        /// <param name="idMoneda">Id de Moneda</param>
        /// <returns> MonedaNombrePluralSimboloDTO </returns>
        public MonedaNombrePluralSimboloDTO ObtenerMonedaParaDocumento(int idMoneda)
        {
            try
            {
                return _unitOfWork.MonedaRepository.ObtenerMonedaParaDocumento(idMoneda);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 07/11/2022
        /// <summary>
        /// Obtiene Moneda por Id
        /// </summary>        
        /// <returns> MonedaCostoTotalConDescuentoDTO </returns>
        public List<MonedaNombrePluralSimboloDTO> ObtenerMonedaTodo()
        {
            try
            {
                return _unitOfWork.MonedaRepository.ObtenerMonedaTodo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 07/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Moneda por Id
        /// </summary>
        /// <param name="id">Id de moneda </param>
        /// <returns> MonedaCostoTotalConDescuentoDTO </returns>
        public MonedaNombrePluralSimboloDTO ObtenerMonedaPorId(int id)
        {
            try
            {
                return _unitOfWork.MonedaRepository.ObtenerMonedaPorId(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Margiory Ramirez.
        /// Fecha: 20/12//2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Moneda  filtro Moneda
        /// </summary
        /// <returns> </returns>
        public List<FiltroGenericoDTO> ObtenerFiltroMoneda()
    
        {
            try
            {
                return _unitOfWork.MonedaRepository.ObtenerFiltroMoneda();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
