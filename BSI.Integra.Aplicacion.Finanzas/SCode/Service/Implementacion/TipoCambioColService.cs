using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Google.Api.Ads.AdWords.v201809;
using System.Transactions;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Implementacion
{
    /// Service: TipoCambioColService
    /// Autor Modificacion: Griselberto Huaman.
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_TipoCambioCol
    /// </summary>
    public class TipoCambioColService : ITipoCambioColService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public Mapper mapperTipoCambioCol;

        public TipoCambioColService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TTipoCambioCol, TipoCambioCol>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
            mapperTipoCambioCol = new Mapper(config);
        }

        #region Metodos Base
        public TipoCambioCol Add(TipoCambioCol entidad)
        {
            try
            {
                var modelo = _unitOfWork.TipoCambioColRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<TipoCambioCol>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TipoCambioCol Update(TipoCambioCol entidad)
        {
            try
            {
                var modelo = _unitOfWork.TipoCambioColRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<TipoCambioCol>(modelo);
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
                _unitOfWork.TipoCambioColRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TipoCambioCol> Add(List<TipoCambioCol> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.TipoCambioColRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<TipoCambioCol>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TipoCambioCol> Update(List<TipoCambioCol> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.TipoCambioColRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<TipoCambioCol>>(modelo);
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
                _unitOfWork.TipoCambioColRepository.Delete(listadoIds, usuario);
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
        /// Fecha: 01/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el ultimo tipo cambio colombiano
        /// </summary>
        /// <returns>Valor double Pesos Dolares</returns>
        public double ObtenerPesosDolaresUltimoTipoCambioColombia()
        {
            try
            {
                return _unitOfWork.TipoCambioColRepository.ObtenerPesosDolaresUltimoTipoCambioColombia();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 01/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos  Tipos de Cambios Col
        /// </summary>
        /// <returns>Nuevo objeto : Id, PesosDolares, DolaresPesos, Fecha, IdMoneda</returns>
        public object Obtener()
        {
            try
            {
                return _unitOfWork.TipoCambioColRepository.Obtener();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jorge Gamero.
        /// Fecha: 21/09/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene el tipo cambio colombiano por la fecha dada
        /// </summary>
        /// <returns>Valor double Pesos Dolares</returns>
        public TipoCambioColombiaDTO ObtenerPesosDolaresTipoCambioColombia(string fecha)
        {
            try
            {
                return _unitOfWork.TipoCambioColRepository.ObtenerPesosDolaresTipoCambioColombia(fecha);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }

}


