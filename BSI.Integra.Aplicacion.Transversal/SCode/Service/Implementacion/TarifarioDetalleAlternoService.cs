using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: TarifarioDetalleAlternoService
    /// Autor: Jonathan Caipo
    /// Fecha: 08/11/2022
    /// <summary>
    /// Gestión general de T_AsignacionOportunidadLog
    /// </summary>
    public class TarifarioDetalleAlternoService : ITarifarioDetalleAlternoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public TarifarioDetalleAlternoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TTarifarioDetalleAlterno, TarifarioDetalleAlterno>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public TarifarioDetalleAlterno Add(TarifarioDetalleAlterno entidad)
        {
            try
            {
                var modelo = _unitOfWork.TarifarioDetalleAlternoRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<TarifarioDetalleAlterno>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TarifarioDetalleAlterno Update(TarifarioDetalleAlterno entidad)
        {
            try
            {
                var modelo = _unitOfWork.TarifarioDetalleAlternoRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<TarifarioDetalleAlterno>(modelo);
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
                _unitOfWork.ChatDetalleIntegraRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TarifarioDetalleAlterno> Add(List<TarifarioDetalleAlterno> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.TarifarioDetalleAlternoRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<TarifarioDetalleAlterno>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TarifarioDetalleAlterno> Update(List<TarifarioDetalleAlterno> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.TarifarioDetalleAlternoRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<TarifarioDetalleAlterno>>(modelo);
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
                _unitOfWork.ChatDetalleIntegraRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor: Jonathan Caipo
        /// Fecha: 09/11/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene una tupla de la tabla mkt.T_TarifarioDetalleAlterno por Id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TarifarioDetalleAlterno ObtenerPorId(int id)
        {
            try
            {
                return _unitOfWork.TarifarioDetalleAlternoRepository.ObtenerPorId(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
