using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    /// Service: DocumentacionComercialPwService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 11/07/2022
    /// <summary>
    /// Gestión general de T_DocumentacionComercialPw
    /// </summary>
    public class DocumentacionComercialPwService : IDocumentacionComercialPwService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public DocumentacionComercialPwService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TDocumentacionComercialPw, DocumentacionComercialPw>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public DocumentacionComercialPw Add(DocumentacionComercialPw entidad)
        {
            try
            {
                var modelo = _unitOfWork.DocumentacionComercialPwRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<DocumentacionComercialPw>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DocumentacionComercialPw Update(DocumentacionComercialPw entidad)
        {
            try
            {
                var modelo = _unitOfWork.DocumentacionComercialPwRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<DocumentacionComercialPw>(modelo);
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
                _unitOfWork.DocumentacionComercialPwRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DocumentacionComercialPw> Add(List<DocumentacionComercialPw> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.DocumentacionComercialPwRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<DocumentacionComercialPw>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DocumentacionComercialPw> Update(List<DocumentacionComercialPw> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.DocumentacionComercialPwRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<DocumentacionComercialPw>>(modelo);
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
                _unitOfWork.DocumentacionComercialPwRepository.Delete(listadoIds, usuario);
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
        /// Fecha: 11/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_DocumentacionComercialPw
        /// </summary>
        /// <returns> List<DocumentacionComercialPwDTO> </returns>
        public IEnumerable<DocumentacionComercialPwDTO> ObtenerDocumentacionComercialPw()
        {
            try
            {
                return _unitOfWork.DocumentacionComercialPwRepository.ObtenerDocumentacionComercialPw();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_DocumentacionComercialPw para mostrarse en combo.
        /// </summary>
        /// <returns> List<DocumentacionComercialPwComboDTO> </returns>
        public IEnumerable<DocumentacionComercialPwComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.DocumentacionComercialPwRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 05/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Contenido de DocumentacionComercialPw segun el Tipo de Documento, la modalidad del Programa y el Pais
        /// </summary>
        /// <param name="tipoDocumento">Tipo de Documento</param>
        /// <param name="modalidad">Modalidad del Programa</param>
        /// <param name="idPais">Id del Pais</param>
        /// <returns> ValorStringDTO </returns>
        public StringDTO ObtenerContenidoDocumentoComercial(string tipoDocumento, string modalidad, int idPais)
        {
            try
            {
                return _unitOfWork.DocumentacionComercialPwRepository.ObtenerContenidoDocumentoComercial(tipoDocumento, modalidad, idPais);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 12/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la el contenido de la Documentación Comercial
        /// </summary>
        /// <param name="tipoDocumento"></param>
        /// <param name="modalidad"></param>
        /// <param name="idPais"></param>
        /// <returns> DocumentoComercialContenidoDTO </returns>
        public DocumentoComercialContenidoDTO DocumentoComercialContenido(string tipoDocumento, string modalidad, int idPais)
        {
            try
            {
                return _unitOfWork.DocumentacionComercialPwRepository.DocumentoComercialContenido(tipoDocumento, modalidad, idPais);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
