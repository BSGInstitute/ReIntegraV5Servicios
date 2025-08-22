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
    /// Service: FacebookAudienciaCuentaPublicitariumService
    /// Autor: Edson Daniel Mayta Escobedo
    /// Fecha: 26/08/2022
    /// <summary>
    /// Gestión general de T_OrigenSector
    /// </summary>
    public class FacebookAudienciaCuentaPublicitariumService : IFacebookAudienciaCuentaPublicitariumService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;


        public FacebookAudienciaCuentaPublicitariumService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TFacebookAudienciaCuentaPublicitarium, FacebookAudienciaCuentaPublicitarium>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public FacebookAudienciaCuentaPublicitarium Add(FacebookAudienciaCuentaPublicitarium entidad)
        {
            try
            {
                var modelo = _unitOfWork.FacebookAudienciaCuentaPublicitariumRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<FacebookAudienciaCuentaPublicitarium>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public FacebookAudienciaCuentaPublicitarium Update(FacebookAudienciaCuentaPublicitarium entidad)
        {
            try
            {
                var modelo = _unitOfWork.FacebookAudienciaCuentaPublicitariumRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<FacebookAudienciaCuentaPublicitarium>(modelo);
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
                _unitOfWork.FacebookAudienciaCuentaPublicitariumRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FacebookAudienciaCuentaPublicitarium> Add(List<FacebookAudienciaCuentaPublicitarium> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.FacebookAudienciaCuentaPublicitariumRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<FacebookAudienciaCuentaPublicitarium>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FacebookAudienciaCuentaPublicitarium> Update(List<FacebookAudienciaCuentaPublicitarium> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.FacebookAudienciaCuentaPublicitariumRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<FacebookAudienciaCuentaPublicitarium>>(modelo);
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
                _unitOfWork.FacebookAudienciaCuentaPublicitariumRepository.Delete(listadoIds, usuario);
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
