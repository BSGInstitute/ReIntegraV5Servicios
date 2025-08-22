using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: CertificadoGeneradoAutomaticoContenidoService
    /// Autor: Jonathan Caipo
    /// Fecha: 28/11/2022
    /// <summary>
    /// Gestión general de T_CertificadoGeneradoAutomaticoContenido
    /// </summary>
    public class CertificadoGeneradoAutomaticoContenidoService : ICertificadoGeneradoAutomaticoContenidoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public CertificadoGeneradoAutomaticoContenidoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TCertificadoGeneradoAutomaticoContenido, CertificadoGeneradoAutomaticoContenido>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public CertificadoGeneradoAutomaticoContenido Add(CertificadoGeneradoAutomaticoContenido entidad)
        {
            try
            {
                var modelo = _unitOfWork.CertificadoGeneradoAutomaticoContenidoRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<CertificadoGeneradoAutomaticoContenido>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CertificadoGeneradoAutomaticoContenido Update(CertificadoGeneradoAutomaticoContenido entidad)
        {
            try
            {
                var modelo = _unitOfWork.CertificadoGeneradoAutomaticoContenidoRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<CertificadoGeneradoAutomaticoContenido>(modelo);
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
                _unitOfWork.CertificadoGeneradoAutomaticoContenidoRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CertificadoGeneradoAutomaticoContenido> Add(List<CertificadoGeneradoAutomaticoContenido> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.CertificadoGeneradoAutomaticoContenidoRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<CertificadoGeneradoAutomaticoContenido>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CertificadoGeneradoAutomaticoContenido> Update(List<CertificadoGeneradoAutomaticoContenido> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.CertificadoGeneradoAutomaticoContenidoRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<CertificadoGeneradoAutomaticoContenido>>(modelo);
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
                _unitOfWork.CertificadoGeneradoAutomaticoContenidoRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion



        public List<ContenidoCertificadoSinFondoDTO> ObtenerDatosParaCertificadoFisico(int IdCertificadoGeneradoAutomatico)
        {
            try
            {
                return _unitOfWork.CertificadoGeneradoAutomaticoContenidoRepository.ObtenerDatosParaCertificadoFisico(IdCertificadoGeneradoAutomatico);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
