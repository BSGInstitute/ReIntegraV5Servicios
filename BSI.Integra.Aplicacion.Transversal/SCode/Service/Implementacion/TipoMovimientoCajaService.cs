using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System.Runtime.InteropServices;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: TipoMovimientoCajaService
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_TipoDato
    /// </summary>
    public class TipoMovimientoCajaService : ITipoMovimientoCajaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public TipoMovimientoCajaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TTipoMovimientoCaja, TipoMovimientoCaja>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public TipoMovimientoCaja Add(TipoMovimientoCaja entidad)
        {
            try
            {
                var modelo = _unitOfWork.TipoMovimientoCajaRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<TipoMovimientoCaja>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TipoMovimientoCaja Update(TipoMovimientoCaja entidad)
        {
            try
            {
                var modelo = _unitOfWork.TipoMovimientoCajaRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<TipoMovimientoCaja>(modelo);
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
                _unitOfWork.TipoMovimientoCajaRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TipoMovimientoCaja> Add(List<TipoMovimientoCaja> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.TipoMovimientoCajaRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<TipoMovimientoCaja>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TipoMovimientoCaja> Update(List<TipoMovimientoCaja> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.TipoMovimientoCajaRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<TipoMovimientoCaja>>(modelo);
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
                _unitOfWork.TipoMovimientoCajaRepository.Delete(listadoIds, usuario);
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
        /// Fecha: 12/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Lista de Tipo de Datos   de TTipoMovimientoCaja
        /// </summary>
        /// <returns></returns>
        public List<TipoMovimientoCajaDTO> ObtenerListaTipoMovimientoCaja()
        {
            try
            {
                return _unitOfWork.TipoMovimientoCajaRepository.ObtenerListaTipoMovimientoCaja();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




    }
}
