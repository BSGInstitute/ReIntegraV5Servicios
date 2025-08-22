using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: DocumentoEnviadoWebPwService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 13/08/2022
    /// <summary>
    /// Gestión general de T_DocumentoEnviadoWebPw
    /// </summary>
    public class DocumentoEnviadoWebPwService : IDocumentoEnviadoWebPwService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public DocumentoEnviadoWebPwService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TDocumentoEnviadoWebPw, DocumentoEnviadoWebPw>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public DocumentoEnviadoWebPw Add(DocumentoEnviadoWebPw entidad)
        {
            try
            {
                var modelo = _unitOfWork.DocumentoEnviadoWebPwRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<DocumentoEnviadoWebPw>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DocumentoEnviadoWebPw Update(DocumentoEnviadoWebPw entidad)
        {
            try
            {
                var modelo = _unitOfWork.DocumentoEnviadoWebPwRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<DocumentoEnviadoWebPw>(modelo);
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
                _unitOfWork.DocumentoEnviadoWebPwRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DocumentoEnviadoWebPw> Add(List<DocumentoEnviadoWebPw> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.DocumentoEnviadoWebPwRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<DocumentoEnviadoWebPw>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DocumentoEnviadoWebPw> Update(List<DocumentoEnviadoWebPw> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.DocumentoEnviadoWebPwRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<DocumentoEnviadoWebPw>>(modelo);
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
                _unitOfWork.DocumentoEnviadoWebPwRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 13/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_DocumentoEnviadoWebPw
        /// </summary>
        /// <returns> List<DocumentoEnviadoWebPwDTO> </returns>
        public IEnumerable<DocumentoEnviadoWebPwDTO> ObtenerDocumentoEnviadoWebPw()
        {
            try
            {
                return _unitOfWork.DocumentoEnviadoWebPwRepository.ObtenerDocumentoEnviadoWebPw();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
