using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Implementacion
{
    /// Service: DocumentoLegalPaisService
    /// Autor Modificacion: Erick Marcelo Quispe.
    /// Fecha: 16/07/2022
    /// <summary>
    /// Gestión general de T_DocumentoLegalPais
    /// </summary>
    public class DocumentoLegalPaisService : IDocumentoLegalPaisService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public DocumentoLegalPaisService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TDocumentoLegalPai, DocumentoLegalPais>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public DocumentoLegalPais Add(DocumentoLegalPais entidad)
        {
            try
            {
                var modelo = _unitOfWork.DocumentoLegalPaisRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<DocumentoLegalPais>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DocumentoLegalPais Update(DocumentoLegalPais entidad)
        {
            try
            {
                var modelo = _unitOfWork.DocumentoLegalPaisRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<DocumentoLegalPais>(modelo);
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
                _unitOfWork.DocumentoLegalPaisRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DocumentoLegalPais> Add(List<DocumentoLegalPais> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.DocumentoLegalPaisRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<DocumentoLegalPais>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DocumentoLegalPais> Update(List<DocumentoLegalPais> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.DocumentoLegalPaisRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<DocumentoLegalPais>>(modelo);
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
                _unitOfWork.DocumentoLegalPaisRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor Modificacion: Erick Marcelo Quispe.
        /// Fecha: 16/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_DocumentoLegalPais
        /// </summary>
        /// <returns> List<DocumentoLegalPaisDTO> </returns>
        public IEnumerable<DocumentoLegalPaisDTO> ObtenerDocumentoLegalPais()
        {
            try
            {
                return _unitOfWork.DocumentoLegalPaisRepository.ObtenerDocumentoLegalPais();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor Modificacion: Erick Marcelo Quispe.
        /// Fecha: 16/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_DocumentoLegalPais para mostrarse en combo.
        /// </summary>
        /// <returns> List<DocumentoLegalPaisComboDTO> </returns>
        public IEnumerable<DocumentoLegalPaisComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.DocumentoLegalPaisRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
