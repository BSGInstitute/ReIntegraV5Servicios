using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: AdwordsApiVolumenBusquedumService
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_AdwordsApiVolumenBusquedum
    /// </summary>
    public class AdwordsApiVolumenBusquedumService : IAdwordsApiVolumenBusquedumService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public AdwordsApiVolumenBusquedumService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TAdwordsApiVolumenBusquedum, AdwordsApiVolumenBusquedum>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public AdwordsApiVolumenBusquedum Add(AdwordsApiVolumenBusquedum entidad)
        {
            try
            {
                var modelo = _unitOfWork.AdwordsApiVolumenBusquedumRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<AdwordsApiVolumenBusquedum>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public AdwordsApiVolumenBusquedum Update(AdwordsApiVolumenBusquedum entidad)
        {
            try
            {
                var modelo = _unitOfWork.AdwordsApiVolumenBusquedumRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<AdwordsApiVolumenBusquedum>(modelo);
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
                _unitOfWork.AdwordsApiVolumenBusquedumRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AdwordsApiVolumenBusquedum> Add(List<AdwordsApiVolumenBusquedum> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.AdwordsApiVolumenBusquedumRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<AdwordsApiVolumenBusquedum>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AdwordsApiVolumenBusquedum> Update(List<AdwordsApiVolumenBusquedum> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.AdwordsApiVolumenBusquedumRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<AdwordsApiVolumenBusquedum>>(modelo);
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
                _unitOfWork.AdwordsApiVolumenBusquedumRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


    }
}
