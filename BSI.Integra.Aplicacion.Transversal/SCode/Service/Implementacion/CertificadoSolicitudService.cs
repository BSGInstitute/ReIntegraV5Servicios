using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: CertificadoSolicitudService
    /// Autor: Gilmer Quispe.
    /// Fecha: 06/01/2023
    /// <summary>
    /// Gestión general de T_CertificadoSolicitud
    /// </summary>
    public class CertificadoSolicitudService : ICertificadoSolicitudService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public CertificadoSolicitudService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCertificadoSolicitud, CertificadoSolicitud>(MemberList.None).ReverseMap(); 
            }
            );
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public CertificadoSolicitud Add(CertificadoSolicitud entidad)
        {
            try
            {
                var modelo = _unitOfWork.CertificadoSolicitudRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<CertificadoSolicitud>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        } 
        public CertificadoSolicitud Update(CertificadoSolicitud entidad)
        {
            try
            {
                var modelo = _unitOfWork.CertificadoSolicitudRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<CertificadoSolicitud>(modelo);
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
                _unitOfWork.CertificadoSolicitudRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        } 
        public List<CertificadoSolicitud> Add(List<CertificadoSolicitud> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.CertificadoSolicitudRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<CertificadoSolicitud>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        } 
        public List<CertificadoSolicitud> Update(List<CertificadoSolicitud> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.CertificadoSolicitudRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<CertificadoSolicitud>>(modelo);
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
                _unitOfWork.CertificadoSolicitudRepository.Delete(listadoIds, usuario);
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
