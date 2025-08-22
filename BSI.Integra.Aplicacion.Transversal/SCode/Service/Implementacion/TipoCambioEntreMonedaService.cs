using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: TipoCambioEntreMonedaService
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_TipoCambioEntreMoneda
    /// </summary>
    public class TipoCambioEntreMonedaService : ITipoCambioEntreMonedaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public TipoCambioEntreMonedaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TTipoCambioEntreMonedum, TipoCambioEntreMoneda>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public TipoCambioEntreMoneda Add(TipoCambioEntreMoneda entidad)
        {
            try
            {
                var modelo = _unitOfWork.TipoCambioEntreMonedaRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<TipoCambioEntreMoneda>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TipoCambioEntreMoneda Update(TipoCambioEntreMoneda entidad)
        {
            try
            {
                var modelo = _unitOfWork.TipoCambioEntreMonedaRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<TipoCambioEntreMoneda>(modelo);
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
                _unitOfWork.TipoCambioEntreMonedaRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TipoCambioEntreMoneda> Add(List<TipoCambioEntreMoneda> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.TipoCambioEntreMonedaRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<TipoCambioEntreMoneda>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TipoCambioEntreMoneda> Update(List<TipoCambioEntreMoneda> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.TipoCambioEntreMonedaRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<TipoCambioEntreMoneda>>(modelo);
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
                _unitOfWork.TipoCambioEntreMonedaRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 30/01/2023
        /// Version: 1.0
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public object ObtenerParaFiltro()
        {
            try
            {
                return _unitOfWork.TipoCambioEntreMonedaRepository.ObtenerParaFiltro();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
