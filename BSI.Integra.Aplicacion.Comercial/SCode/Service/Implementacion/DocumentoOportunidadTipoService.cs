using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Comercial.Service.Implementacion
{
    /// Service: DocumentoOportunidadTipoService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 15/07/2022
    /// <summary>
    /// Gestión general de T_DocumentoOportunidadTipo
    /// </summary>
    public class DocumentoOportunidadTipoService : IDocumentoOportunidadTipoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public DocumentoOportunidadTipoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TDocumentoOportunidadTipo, DocumentoOportunidadTipo>(MemberList.None).ReverseMap();
                    cfg.CreateMap<DocumentoOportunidadTipoDTO, ComboDTO>(MemberList.None);
                }
            );
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public DocumentoOportunidadTipo Add(DocumentoOportunidadTipo entidad)
        {
            try
            {
                var modelo = _unitOfWork.DocumentoOportunidadTipoRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<DocumentoOportunidadTipo>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DocumentoOportunidadTipo Update(DocumentoOportunidadTipo entidad)
        {
            try
            {
                var modelo = _unitOfWork.DocumentoOportunidadTipoRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<DocumentoOportunidadTipo>(modelo);
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
                _unitOfWork.DocumentoOportunidadTipoRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DocumentoOportunidadTipo> Add(List<DocumentoOportunidadTipo> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.DocumentoOportunidadTipoRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<DocumentoOportunidadTipo>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DocumentoOportunidadTipo> Update(List<DocumentoOportunidadTipo> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.DocumentoOportunidadTipoRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<DocumentoOportunidadTipo>>(modelo);
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
                _unitOfWork.DocumentoOportunidadTipoRepository.Delete(listadoIds, usuario);
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
        /// Fecha: 15/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_DocumentoOportunidadTipo
        /// </summary>
        /// <returns> List<DocumentoOportunidadTipoDTO> </returns>
        public IEnumerable<DocumentoOportunidadTipoDTO> ObtenerDocumentoOportunidadTipo()
        {
            try
            {
                return _unitOfWork.DocumentoOportunidadTipoRepository.ObtenerDocumentoOportunidadTipo();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 15/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_DocumentoOportunidadTipo para mostrarse en combo.
        /// </summary>
        /// <returns> List<DocumentoOportunidadTipoComboDTO> </returns>
        public IEnumerable<DocumentoOportunidadTipoComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.DocumentoOportunidadTipoRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
