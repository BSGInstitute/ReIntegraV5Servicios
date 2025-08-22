using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
//using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.FiltroTipoUrlBlockStorageDTO;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: UrlBlockStorageService
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_UrlBlockStorage
    /// </summary>
    public class UrlBlockStorageService : IUrlBlockStorageService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public UrlBlockStorageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TUrlBlockStorage, UrlBlockStorage>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public UrlBlockStorage Add(UrlBlockStorage entidad)
        {
            try
            {
                var modelo = _unitOfWork.UrlBlockStorageRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<UrlBlockStorage>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public UrlBlockStorage Update(UrlBlockStorage entidad)
        {
            try
            {
                var modelo = _unitOfWork.UrlBlockStorageRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<UrlBlockStorage>(modelo);
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
                _unitOfWork.UrlBlockStorageRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<UrlBlockStorage> Add(List<UrlBlockStorage> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.UrlBlockStorageRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<UrlBlockStorage>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<UrlBlockStorage> Update(List<UrlBlockStorage> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.UrlBlockStorageRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<UrlBlockStorage>>(modelo);
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
                _unitOfWork.UrlBlockStorageRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_UrlBlockStorage para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>

        public IEnumerable<ContenedorArchivoCompletoDTO> ObtenerInformacionPorIdUrlSubcontenedor(int IdUrlSubContenedor)
        {
            try
            {
                return _unitOfWork.UrlBlockStorageRepository.ObtenerInformacionPorIdUrlSubcontenedor(IdUrlSubContenedor);
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

    }
}

