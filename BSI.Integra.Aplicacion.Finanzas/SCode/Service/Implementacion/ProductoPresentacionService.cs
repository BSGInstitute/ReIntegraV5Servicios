using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Implementacion
{
    /// Service: ProductoPresentacionService
    /// Autor Modificacion: Griselberto Huaman.
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_ProductoPresentacion
    /// </summary>
    public class ProductoPresentacionService : IProductoPresentacionService
    {

        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ProductoPresentacionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TProductoPresentacion, ProductoPresentacion>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public ProductoPresentacion Add(ProductoPresentacion entidad)
        {
            try
            {
                var modelo = _unitOfWork.ProductoPresentacionRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ProductoPresentacion>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ProductoPresentacion Update(ProductoPresentacion entidad)
        {
            try
            {
                var modelo = _unitOfWork.ProductoPresentacionRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ProductoPresentacion>(modelo);
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
                _unitOfWork.ProductoPresentacionRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProductoPresentacion> Add(List<ProductoPresentacion> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ProductoPresentacionRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ProductoPresentacion>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProductoPresentacion> Update(List<ProductoPresentacion> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ProductoPresentacionRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ProductoPresentacion>>(modelo);
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
                _unitOfWork.ProductoPresentacionRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        /// Autor Modificacion: Griselberto Huaman.
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_ProductoPresentacion para mostrarse en combo.
        /// </summary>
        /// <returns> List<ProductoPresentacionComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.ProductoPresentacionRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}
