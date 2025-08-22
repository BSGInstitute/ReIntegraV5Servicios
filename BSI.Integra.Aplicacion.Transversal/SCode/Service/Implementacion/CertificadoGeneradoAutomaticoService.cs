using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: CertificadoGeneradoAutomaticoService
    /// Autor: Jonathan Caipo
    /// Fecha: 28/11/2022
    /// <summary>
    /// Gestión general de T_CertificadoGeneradoAutomatico
    /// </summary>
    public class CertificadoGeneradoAutomaticoService : ICertificadoGeneradoAutomaticoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public CertificadoGeneradoAutomaticoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCertificadoGeneradoAutomatico, CertificadoGeneradoAutomatico>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public CertificadoGeneradoAutomatico Add(CertificadoGeneradoAutomatico entidad)
        {
            try
            {
                var modelo = _unitOfWork.CertificadoGeneradoAutomaticoRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<CertificadoGeneradoAutomatico>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public CertificadoGeneradoAutomatico Update(CertificadoGeneradoAutomatico entidad)
        {
            try
            {
                var modelo = _unitOfWork.CertificadoGeneradoAutomaticoRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<CertificadoGeneradoAutomatico>(modelo);
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
                _unitOfWork.CertificadoGeneradoAutomaticoRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CertificadoGeneradoAutomatico> Add(List<CertificadoGeneradoAutomatico> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.CertificadoGeneradoAutomaticoRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<CertificadoGeneradoAutomatico>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CertificadoGeneradoAutomatico> Update(List<CertificadoGeneradoAutomatico> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.CertificadoGeneradoAutomaticoRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<CertificadoGeneradoAutomatico>>(modelo);
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
                _unitOfWork.CertificadoGeneradoAutomaticoRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        public int ObtenerCorrelativoCertificado()
        {
            try
            {
                return _unitOfWork.CertificadoGeneradoAutomaticoRepository.ObtenerCorrelativoCertificado();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
