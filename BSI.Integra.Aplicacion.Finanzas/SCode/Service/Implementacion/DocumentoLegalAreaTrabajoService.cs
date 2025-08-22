using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Implementacion
{
    /// Service: DocumentoLegalAreaTrabajoService
    /// Autor Modificacion: Erick Marcelo Quispe.
    /// Fecha: 16/07/2022
    /// <summary>
    /// Gestión general de T_DocumentoLegalAreaTrabajo
    /// </summary>
    public class DocumentoLegalAreaTrabajoService : IDocumentoLegalAreaTrabajoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public DocumentoLegalAreaTrabajoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TDocumentoLegalAreaTrabajo, DocumentoLegalAreaTrabajo>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public DocumentoLegalAreaTrabajo Add(DocumentoLegalAreaTrabajo entidad)
        {
            try
            {
                var modelo = _unitOfWork.DocumentoLegalAreaTrabajoRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<DocumentoLegalAreaTrabajo>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DocumentoLegalAreaTrabajo Update(DocumentoLegalAreaTrabajo entidad)
        {
            try
            {
                var modelo = _unitOfWork.DocumentoLegalAreaTrabajoRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<DocumentoLegalAreaTrabajo>(modelo);
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
                _unitOfWork.DocumentoLegalAreaTrabajoRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DocumentoLegalAreaTrabajo> Add(List<DocumentoLegalAreaTrabajo> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.DocumentoLegalAreaTrabajoRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<DocumentoLegalAreaTrabajo>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DocumentoLegalAreaTrabajo> Update(List<DocumentoLegalAreaTrabajo> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.DocumentoLegalAreaTrabajoRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<DocumentoLegalAreaTrabajo>>(modelo);
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
                _unitOfWork.DocumentoLegalAreaTrabajoRepository.Delete(listadoIds, usuario);
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
        /// Obtiene todos los registros de T_DocumentoLegalAreaTrabajo
        /// </summary>
        /// <returns> List<DocumentoLegalAreaTrabajoDTO> </returns>
        public IEnumerable<DocumentoLegalAreaTrabajoDTO> ObtenerDocumentoLegalAreaTrabajo()
        {
            try
            {
                return _unitOfWork.DocumentoLegalAreaTrabajoRepository.ObtenerDocumentoLegalAreaTrabajo();
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
        /// Obtiene registros de T_DocumentoLegalAreaTrabajo para mostrarse en combo.
        /// </summary>
        /// <returns> List<DocumentoLegalAreaTrabajoComboDTO> </returns>
        public IEnumerable<DocumentoLegalAreaTrabajoComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.DocumentoLegalAreaTrabajoRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
