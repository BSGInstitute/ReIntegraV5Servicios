using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: ArticuloSeoService
    /// Autor: Max Mantilla.
    /// Fecha: 25/10/2022
    /// <summary>
    /// Gestión general de T_ArticuloSeo
    /// </summary>
    public class ArticuloSeoService : IArticuloSeoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ArticuloSeoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TArticuloSeo, ArticuloSeo>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public ArticuloSeo Add(ArticuloSeo entidad)
        {
            try
            {
                var modelo = _unitOfWork.ArticuloSeoRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ArticuloSeo>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ArticuloSeo Update(ArticuloSeo entidad)
        {
            try
            {
                var modelo = _unitOfWork.ArticuloSeoRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ArticuloSeo>(modelo);
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
                _unitOfWork.ArticuloSeoRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ArticuloSeo> Add(List<ArticuloSeo> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ArticuloSeoRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ArticuloSeo>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ArticuloSeo> Update(List<ArticuloSeo> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ArticuloSeoRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ArticuloSeo>>(modelo);
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
                _unitOfWork.ArticuloSeoRepository.Delete(listadoIds, usuario);
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
        /// Obtiene todos parametroSeo los registros de T_ArticuloSeo
        /// </summary>
        /// <returns> List<ParametroSeoContenidoArticuloDTO> </returns>
        public List<ParametroSeoContenidoArticuloDTO> ObtenerArticuloSeoParametro(int IdArticulo)
        {
            try
            {
                return _unitOfWork.ArticuloSeoRepository.ObtenerArticuloSeoParametro(IdArticulo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 25/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros ParametroSeo para combo
        /// </summary>
        /// <returns> IEnumerable<ParametroSeoComboDTO> </returns>
        public IEnumerable<ParametroSeoComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.ArticuloSeoRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
