using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: TipoArticuloService
    /// Autor: Max Mantilla Rodríguez.
    /// Fecha: 25/10/2022
    /// <summary>
    /// Gestión general de T_TipoArticulo
    /// </summary>
    public class TipoArticuloService : ITipoArticuloService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public TipoArticuloService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TTipoArticulo, TipoArticulo>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public TipoArticulo Add(TipoArticulo entidad)
        {
            try
            {
                var modelo = _unitOfWork.TipoArticuloRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<TipoArticulo>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TipoArticulo Update(TipoArticulo entidad)
        {
            try
            {
                var modelo = _unitOfWork.TipoArticuloRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<TipoArticulo>(modelo);
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
                _unitOfWork.TipoArticuloRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TipoArticulo> Add(List<TipoArticulo> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.TipoArticuloRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<TipoArticulo>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TipoArticulo> Update(List<TipoArticulo> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.TipoArticuloRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<TipoArticulo>>(modelo);
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
                _unitOfWork.TipoArticuloRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 25/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_TipoArticulo
        /// </summary>
        /// <returns> IEnumerable<TipoArticuloDTO> </returns>
        public IEnumerable<TipoArticuloDTO> ObtenerFiltroTipoArticulo()
        {
            try
            {
                return _unitOfWork.TipoArticuloRepository.ObtenerFiltroTipoArticulo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
