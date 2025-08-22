using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Implementacion
{
    /// Service: CondicionTipoPagoService
    /// Autor Modificacion: Griselberto Huaman.
    /// Fecha: 24/06/2022
    /// <summary>
    /// Gestión general de T_CondicionTipoPago
    /// </summary>
    public class CondicionTipoPagoService : ICondicionTipoPagoService
    {

        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public CondicionTipoPagoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TCondicionTipoPago, CondicionTipoPago>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public CondicionTipoPago Add(CondicionTipoPago entidad)
        {
            try
            {
                var modelo = _unitOfWork.CondicionTipoPagoRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<CondicionTipoPago>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CondicionTipoPago Update(CondicionTipoPago entidad)
        {
            try
            {
                var modelo = _unitOfWork.CondicionTipoPagoRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<CondicionTipoPago>(modelo);
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
                _unitOfWork.CondicionTipoPagoRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CondicionTipoPago> Add(List<CondicionTipoPago> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.CondicionTipoPagoRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<CondicionTipoPago>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CondicionTipoPago> Update(List<CondicionTipoPago> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.CondicionTipoPagoRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<CondicionTipoPago>>(modelo);
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
                _unitOfWork.CondicionTipoPagoRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.CondicionTipoPagoRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}
