using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Comercial.Service.Implementacion
{
    /// Service: DocumentoOportunidadService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 15/07/2022
    /// <summary>
    /// Gestión general de T_DocumentoOportunidad
    /// </summary>
    public class DocumentoOportunidadService : IDocumentoOportunidadService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public DocumentoOportunidadService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TDocumentoOportunidad, DocumentoOportunidad>(MemberList.None).ReverseMap();
                    cfg.CreateMap<DocumentoOportunidadDTO, ComboDTO>(MemberList.None);
                }
            );
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public DocumentoOportunidad Add(DocumentoOportunidad entidad)
        {
            try
            {
                var modelo = _unitOfWork.DocumentoOportunidadRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<DocumentoOportunidad>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DocumentoOportunidad Update(DocumentoOportunidad entidad)
        {
            try
            {
                var modelo = _unitOfWork.DocumentoOportunidadRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<DocumentoOportunidad>(modelo);
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
                _unitOfWork.DocumentoOportunidadRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DocumentoOportunidad> Add(List<DocumentoOportunidad> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.DocumentoOportunidadRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<DocumentoOportunidad>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DocumentoOportunidad> Update(List<DocumentoOportunidad> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.DocumentoOportunidadRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<DocumentoOportunidad>>(modelo);
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
                _unitOfWork.DocumentoOportunidadRepository.Delete(listadoIds, usuario);
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
        /// Obtiene todos los registros de T_DocumentoOportunidad
        /// </summary>
        /// <returns> List<DocumentoOportunidadDTO> </returns>
        public IEnumerable<DocumentoOportunidadDTO> ObtenerDocumentoOportunidad()
        {
            try
            {
                return _unitOfWork.DocumentoOportunidadRepository.ObtenerDocumentoOportunidad();
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
        /// Obtiene registros de T_DocumentoOportunidad para mostrarse en combo.
        /// </summary>
        /// <returns> List<DocumentoOportunidadComboDTO> </returns>
        public IEnumerable<DocumentoOportunidadComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.DocumentoOportunidadRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 08/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los datos de un programa general especifico
        /// </summary>
        /// <returns>Objeto de tipo PgeneralDTO</returns>
        public List<DocumentoOportunidadInsertadoDTO> ObtenerDocumentosPorOportunidad(int idOportunidad)
        {
            try
            {
                return _unitOfWork.DocumentoOportunidadRepository.ObtenerDocumentosPorOportunidad(idOportunidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 26/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene lel id del DocumentoOportunidad por el IdOportunidad y el tipo
        /// </summary>
        /// <param name="idOportunidad">Id de la oportunidad</param>
        /// <param name="idTipo">Id del tipo documento doportunidad</param>
        /// <returns>Objeto de tipo ValorIntDTO</returns>
        public ValorIntDTO ObtenerDocOportunidadPorIdYTipo(int idOportunidad, int idTipo)
        {
            try
            {
                return _unitOfWork.DocumentoOportunidadRepository.ObtenerDocOportunidadPorIdYTipo(idOportunidad, idTipo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
