using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: ArticuloTagService
    /// Autor: Max Mantilla.
    /// Fecha: 25/10/2022
    /// <summary>
    /// Gestión general de T_ArticuloTag
    /// </summary>
    public class ArticuloTagService : IArticuloTagService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ArticuloTagService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TArticuloTag, ArticuloTag>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public ArticuloTag Add(ArticuloTag entidad)
        {
            try
            {
                var modelo = _unitOfWork.ArticuloTagRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ArticuloTag>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ArticuloTag Update(ArticuloTag entidad)
        {
            try
            {
                var modelo = _unitOfWork.ArticuloTagRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ArticuloTag>(modelo);
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
                _unitOfWork.ArticuloTagRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ArticuloTag> Add(List<ArticuloTag> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ArticuloTagRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ArticuloTag>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ArticuloTag> Update(List<ArticuloTag> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ArticuloTagRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ArticuloTag>>(modelo);
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
                _unitOfWork.ArticuloTagRepository.Delete(listadoIds, usuario);
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
        /// Obtiene el listado de tags asociados a pla.T_Articulo
        /// </summary>
        /// <returns> List<int> </returns>
        public List<ArticuloTag> ObtenerArticuloTagsAsociados(int IdArticulo)
        {
            try
            {
                return _unitOfWork.ArticuloTagRepository.ObtenerArticuloTagsAsociados(IdArticulo);
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
        /// Obtiene el listado de tags asociados a pla.T_Articulo
        /// </summary>
        /// <returns> List<FiltroDTO> </returns>
        public List<FiltroDTO> ObtenerTagsAsociadosArticulo(int IdArticulo)
        {
            try
            {
                return _unitOfWork.ArticuloTagRepository.ObtenerTagsAsociadosArticulo(IdArticulo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
