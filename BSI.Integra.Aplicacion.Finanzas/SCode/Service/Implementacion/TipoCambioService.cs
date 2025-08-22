using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System.Transactions;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Implementacion
{
    /// Service: TipoCambioService
    /// Autor Modificacion: Griselberto Huaman.
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_TipoCambio
    /// </summary>
    public class TipoCambioService : ITipoCambioService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public Mapper mapperTipoCambio;
        private readonly int _idMonedaSoles = 20;
        private readonly int _idMonedaCol = 10;

        public TipoCambioService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TTipoCambio, TipoCambio>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
            mapperTipoCambio = new Mapper(config);
        }

        #region Metodos Base
        public TipoCambio Add(TipoCambio entidad)
        {
            try
            {
                var modelo = _unitOfWork.TipoCambioRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<TipoCambio>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TipoCambio Update(TipoCambio entidad)
        {
            try
            {
                var modelo = _unitOfWork.TipoCambioRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<TipoCambio>(modelo);
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
                _unitOfWork.TipoCambioRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TipoCambio> Add(List<TipoCambio> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.TipoCambioRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<TipoCambio>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TipoCambio> Update(List<TipoCambio> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.TipoCambioRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<TipoCambio>>(modelo);
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
                _unitOfWork.TipoCambioRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        /// Autor: Griselberto Huaman.
        /// Fecha: 16/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_TipoContribuyente para mostrarse en combo.
        /// </summary>
        /// <returns> List<TipoContribuyenteComboDTO> </returns>
        /// <paramref name="tipoCambio"/> Tipo de cambio
        public IEnumerable<TipoCambioObtenerDTO> Obtener()
        {
            try
            {
                return _unitOfWork.TipoCambioRepository.Obtener();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Griselberto Huaman.
        /// Fecha: 16/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_TipoContribuyente para mostrarse en combo.
        /// </summary>
        /// <returns> List<TipoContribuyenteComboDTO> </returns>
        /// <paramref name="tipoCambio"/> Tipo de cambio 1 aoles, 2 Dolares
        public TipoCambioFechaDTO ObtenerTipoCambio(int tipoCambio)
        {
            try
            {
                return _unitOfWork.TipoCambioRepository.ObtenerTipoCambio(tipoCambio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 16/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_TipoContribuyente para mostrarse en combo.
        /// </summary>
        /// <returns> List<TipoContribuyenteComboDTO> </returns>
        /// <paramref name="tipoCambio"/> Tipo de cambio
        /// <paramref name="fecha"/> Fecha
        public IEnumerable<TipoCambioReporteDTO> ObtenerTipoCambioFiltro(TipoCambioFiltroDTO filtro)
        {
            try
            {
                return _unitOfWork.TipoCambioRepository.ObtenerTipoCambioFiltro(filtro);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}


