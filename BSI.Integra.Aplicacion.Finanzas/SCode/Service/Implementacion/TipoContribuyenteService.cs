using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Implementacion
{
    /// Service: TipoContribuyenteService
    /// Autor Modificacion: Griselberto Huaman.
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_TipoContribuyente
    /// </summary>
    public class TipoContribuyenteService : ITipoContribuyenteService
    {

        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public TipoContribuyenteService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TTipoContribuyente, TipoContribuyente>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public TipoContribuyente Add(TipoContribuyente entidad)
        {
            try
            {
                var modelo = _unitOfWork.TipoContribuyenteRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<TipoContribuyente>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TipoContribuyente Update(TipoContribuyente entidad)
        {
            try
            {
                var modelo = _unitOfWork.TipoContribuyenteRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<TipoContribuyente>(modelo);
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
                _unitOfWork.TipoContribuyenteRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TipoContribuyente> Add(List<TipoContribuyente> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.TipoContribuyenteRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<TipoContribuyente>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TipoContribuyente> Update(List<TipoContribuyente> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.TipoContribuyenteRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<TipoContribuyente>>(modelo);
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
                _unitOfWork.TipoContribuyenteRepository.Delete(listadoIds, usuario);
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
        /// Obtiene registros de T_TipoContribuyente para mostrarse en combo.
        /// </summary>
        /// <returns> List<TipoContribuyenteComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.TipoContribuyenteRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<ComboDTO> ObtenerTipoContribuyente()
        {
            try
            {
                return _unitOfWork.TipoContribuyenteRepository.ObtenerTipoContribuyente();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }

}
