using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Implementacion
{
    /// Service: DocumentoIdentidadService
    /// Autor Modificacion: Griselberto Huaman.
    /// Fecha: 24/06/2022
    /// <summary>
    /// Gestión general de T_DocumentoIdentidad
    /// </summary>
    public class DocumentoIdentidadService : IDocumentoIdentidadService
    {

        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public DocumentoIdentidadService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TDocumentoIdentidad, DocumentoIdentidad>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public DocumentoIdentidad Add(DocumentoIdentidad entidad)
        {
            try
            {
                var modelo = _unitOfWork.DocumentoIdentidadRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<DocumentoIdentidad>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DocumentoIdentidad Update(DocumentoIdentidad entidad)
        {
            try
            {
                var modelo = _unitOfWork.DocumentoIdentidadRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<DocumentoIdentidad>(modelo);
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
                _unitOfWork.DocumentoIdentidadRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DocumentoIdentidad> Add(List<DocumentoIdentidad> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.DocumentoIdentidadRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<DocumentoIdentidad>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DocumentoIdentidad> Update(List<DocumentoIdentidad> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.DocumentoIdentidadRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<DocumentoIdentidad>>(modelo);
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
                _unitOfWork.DocumentoIdentidadRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        /// Autor Modificacion: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_DocumentoIdentidad para mostrarse en combo.
        /// </summary>
        /// <returns> List<DocumentoIdentidadComboDTO> </returns>
        public IEnumerable<DocumentoIdentidadComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.DocumentoIdentidadRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}
