using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: AdwordsApiPalabraClaveService
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_AdwordsApiPalabraClave
    /// </summary>
    public class AdwordsApiPalabraClaveService : IAdwordsApiPalabraClaveService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public AdwordsApiPalabraClaveService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TAdwordsApiPalabraClave, AdwordsApiPalabraClave>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public AdwordsApiPalabraClave Add(AdwordsApiPalabraClave entidad)
        {
            try
            {
                var modelo = _unitOfWork.AdwordsApiPalabraClaveRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<AdwordsApiPalabraClave>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public AdwordsApiPalabraClave Update(AdwordsApiPalabraClave entidad)
        {
            try
            {
                var modelo = _unitOfWork.AdwordsApiPalabraClaveRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<AdwordsApiPalabraClave>(modelo);
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
                _unitOfWork.AdwordsApiPalabraClaveRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AdwordsApiPalabraClave> Add(List<AdwordsApiPalabraClave> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.AdwordsApiPalabraClaveRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<AdwordsApiPalabraClave>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AdwordsApiPalabraClave> Update(List<AdwordsApiPalabraClave> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.AdwordsApiPalabraClaveRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<AdwordsApiPalabraClave>>(modelo);
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
                _unitOfWork.AdwordsApiPalabraClaveRepository.Delete(listadoIds, usuario);
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
