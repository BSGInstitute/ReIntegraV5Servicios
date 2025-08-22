using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: ArticuloPGeneralService
    /// Autor: Max Mantilla.
    /// Fecha: 25/10/2022
    /// <summary>
    /// Gestión general de T_ArticuloTag
    /// </summary>
    public class ArticuloPGeneralService : IArticuloPGeneralService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ArticuloPGeneralService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TArticuloPgeneral, ArticuloPGeneral>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public ArticuloPGeneral Add(ArticuloPGeneral entidad)
        {
            try
            {
                var modelo = _unitOfWork.ArticuloPGeneralRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ArticuloPGeneral>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ArticuloPGeneral Update(ArticuloPGeneral entidad)
        {
            try
            {
                var modelo = _unitOfWork.ArticuloPGeneralRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ArticuloPGeneral>(modelo);
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
                _unitOfWork.ArticuloPGeneralRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ArticuloPGeneral> Add(List<ArticuloPGeneral> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ArticuloPGeneralRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ArticuloPGeneral>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ArticuloPGeneral> Update(List<ArticuloPGeneral> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ArticuloPGeneralRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ArticuloPGeneral>>(modelo);
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
                _unitOfWork.ArticuloPGeneralRepository.Delete(listadoIds, usuario);
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
        /// Obtiene programas asociados para pla.T_Articulo
        /// </summary>
        /// <returns> List<int> </returns>
        public List<ArticuloPGeneral> ObtenerArticuloPGeneralAsociados(int IdArticulo)
        {
            try
            {
                return _unitOfWork.ArticuloPGeneralRepository.ObtenerArticuloPGeneralAsociados(IdArticulo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
