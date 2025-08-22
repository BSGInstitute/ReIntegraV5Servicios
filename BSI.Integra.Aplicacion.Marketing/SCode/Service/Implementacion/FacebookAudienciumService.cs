using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Marketing.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Nancy.Json;
using Newtonsoft.Json;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.Service.Implementacion
{
    /// Service: FacebookAudienciumService
    /// Autor: Edson Daniel Mayta Escobedo
    /// Fecha: 26/08/2022
    /// <summary>
    /// Gestión general de T_OrigenSector
    /// </summary>
    public class FacebookAudienciumService : IFacebookAudienciumService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;


        public FacebookAudienciumService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TFacebookAudiencium, FacebookAudiencium>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public FacebookAudiencium Add(FacebookAudiencium entidad)
        {
            try
            {
                var modelo = _unitOfWork.FacebookAudienciumRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<FacebookAudiencium>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public FacebookAudiencium Update(FacebookAudiencium entidad)
        {
            try
            {
                var modelo = _unitOfWork.FacebookAudienciumRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<FacebookAudiencium>(modelo);
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
                _unitOfWork.FacebookAudienciumRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FacebookAudiencium> Add(List<FacebookAudiencium> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.FacebookAudienciumRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<FacebookAudiencium>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FacebookAudiencium> Update(List<FacebookAudiencium> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.FacebookAudienciumRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<FacebookAudiencium>>(modelo);
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
                _unitOfWork.FacebookAudienciumRepository.Delete(listadoIds, usuario);
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
