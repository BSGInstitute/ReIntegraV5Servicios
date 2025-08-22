using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Implementacion
{
    /// Service: CondicionPagoService
    /// Autor Modificacion: Griselberto Huaman.
    /// Fecha: 24/06/2022
    /// <summary>
    /// Gestión general de T_CondicionPago
    /// </summary>
    public class CondicionPagoService : ICondicionPagoService
    {

        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public CondicionPagoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TCondicionPago, CondicionPago>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public CondicionPago Add(CondicionPago entidad)
        {
            try
            {
                var modelo = _unitOfWork.CondicionPagoRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<CondicionPago>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CondicionPago Update(CondicionPago entidad)
        {
            try
            {
                var modelo = _unitOfWork.CondicionPagoRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<CondicionPago>(modelo);
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
                _unitOfWork.CondicionPagoRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CondicionPago> Add(List<CondicionPago> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.CondicionPagoRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<CondicionPago>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CondicionPago> Update(List<CondicionPago> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.CondicionPagoRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<CondicionPago>>(modelo);
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
                _unitOfWork.CondicionPagoRepository.Delete(listadoIds, usuario);
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
                return _unitOfWork.CondicionPagoRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}
