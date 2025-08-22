using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: DataCreditoBusquedaService
    /// Autor: Gilmer Quispe.
    /// Fecha: 09/09/2022
    /// <summary>
    /// Gestión general de DataCreditoBusqueda
    /// </summary>
    public class DataCreditoBusquedumService : IDataCreditoBusquedumService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public DataCreditoBusquedumService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TDataCreditoBusquedum, DataCreditoBusquedum>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public DataCreditoBusquedum Add(DataCreditoBusquedum entidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoBusquedumRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<DataCreditoBusquedum>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataCreditoBusquedum Update(DataCreditoBusquedum entidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoBusquedumRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<DataCreditoBusquedum>(modelo);
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
                _unitOfWork.DataCreditoBusquedumRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DataCreditoBusquedum> Add(List<DataCreditoBusquedum> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoBusquedumRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<DataCreditoBusquedum>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DataCreditoBusquedum> Update(List<DataCreditoBusquedum> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.DataCreditoBusquedumRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<DataCreditoBusquedum>>(modelo);
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
                _unitOfWork.DataCreditoBusquedumRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor: Gilmer Quispe
        /// Fecha: 09/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el IdDataCredito de un alumno por su id.
        /// </summary>
        /// <param name="idAlumno"> Numero de documento del alumno </param>
        /// <returns> ObjetoDTO: DataCreditoDTO </returns>
        public DataCreditoDataDTO ObtenerIdDataCreditoDeAlumnoPorId(int idAlumno)
        {
            try
            {
                return _unitOfWork.DataCreditoBusquedumRepository.ObtenerIdDataCreditoDeAlumnoPorId(idAlumno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 14/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene informacion de DataCredito por el Id
        /// </summary>
        /// <param name="idDataCredito"> Id de data credito en la BD </param>
        /// <returns>Lista de ObjetosDTO: List(DataCreditoInformacionDTO)</returns>
        public DataCreditoInformacionDTO ObtenerInformacionDataCreditoPorId(int idDataCredito)
        {
            try
            {
                return _unitOfWork.DataCreditoBusquedumRepository.ObtenerInformacionDataCreditoPorId(idDataCredito);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 14/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el historial de tarjetas de credito de DataCredito
        /// </summary>
        /// <param name="idDataCredito"> Id de data credito en la BD </param>
        /// <returns>Lista de ObjetosDTO: List(DataCreditoTarjetaCreditoDTO)</returns>
        public List<DataCreditoTarjetaCreditoDTO> ObtenerHistorialTarjetasDataCreditoPorId(int idDataCredito)
        {
            try
            {
                return _unitOfWork.DataCreditoBusquedumRepository.ObtenerHistorialTarjetasDataCreditoPorId(idDataCredito);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 14/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el historial de Deudas vigentes de credito de DataCredito
        /// </summary>
        /// <param name="idDataCredito"> Id de data credito en la BD </param>
        /// <returns>Lista de ObjetosDTO: List(DataCreditoCreditoVigenteDTO)</returns>
        public List<DataCreditoCreditoVigenteDTO> ObtenerHistorialDeudasDataCreditoPorId(int idDataCredito)
        {
            try
            {
                return _unitOfWork.DataCreditoBusquedumRepository.ObtenerHistorialDeudasDataCreditoPorId(idDataCredito);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
