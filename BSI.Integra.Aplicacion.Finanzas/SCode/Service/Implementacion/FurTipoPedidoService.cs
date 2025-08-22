using AutoMapper;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Implementacion
{
    /// Service: FurTipoPedidoService
    /// Autor Modificacion: Griselberto Huaman.
    /// Fecha: 24/06/2022
    /// <summary>
    /// Gestión general de T_FurTipoPedido
    /// </summary>
    public class FurTipoPedidoService : IFurTipoPedidoService
    {

        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public FurTipoPedidoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TFurTipoPedido, FurTipoPedido>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public FurTipoPedido Add(FurTipoPedido entidad)
        {
            try
            {
                var modelo = _unitOfWork.FurTipoPedidoRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<FurTipoPedido>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public FurTipoPedido Update(FurTipoPedido entidad)
        {
            try
            {
                var modelo = _unitOfWork.FurTipoPedidoRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<FurTipoPedido>(modelo);
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
                _unitOfWork.FurTipoPedidoRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FurTipoPedido> Add(List<FurTipoPedido> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.FurTipoPedidoRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<FurTipoPedido>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FurTipoPedido> Update(List<FurTipoPedido> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.FurTipoPedidoRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<FurTipoPedido>>(modelo);
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
                _unitOfWork.FurTipoPedidoRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public IEnumerable<object> ObtenerTipoPedidoFur()
        {
            try
            {
                return _unitOfWork.FurTipoPedidoRepository.ObtenerTipoPedidoFur();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}
