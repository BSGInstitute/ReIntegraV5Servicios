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
    /// Service: FacebookAudienciaAlumnoService
    /// Autor: Edson Daniel Mayta Escobedo
    /// Fecha: 26/08/2022
    /// <summary>
    /// Gestión general de T_OrigenSector
    /// </summary>
    public class FacebookAudienciaAlumnoService : IFacebookAudienciaAlumnoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;


        public FacebookAudienciaAlumnoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TFacebookCuentaPublicitarium, FacebookAudienciaAlumno>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public FacebookAudienciaAlumno Add(FacebookAudienciaAlumno entidad)
        {
            try
            {
                var modelo = _unitOfWork.FacebookAudienciaAlumnoRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<FacebookAudienciaAlumno>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public FacebookAudienciaAlumno Update(FacebookAudienciaAlumno entidad)
        {
            try
            {
                var modelo = _unitOfWork.FacebookAudienciaAlumnoRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<FacebookAudienciaAlumno>(modelo);
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
                _unitOfWork.FacebookAudienciaAlumnoRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FacebookAudienciaAlumno> Add(List<FacebookAudienciaAlumno> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.FacebookAudienciaAlumnoRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<FacebookAudienciaAlumno>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FacebookAudienciaAlumno> Update(List<FacebookAudienciaAlumno> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.FacebookAudienciaAlumnoRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<FacebookAudienciaAlumno>>(modelo);
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
                _unitOfWork.FacebookAudienciaAlumnoRepository.Delete(listadoIds, usuario);
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
