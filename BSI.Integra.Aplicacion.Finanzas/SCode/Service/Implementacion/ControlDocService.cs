using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Implementacion
{
    /// Service: ControlDocService
    /// Autor: Jonathan Caipo
    /// Fecha: 20/10/2022
    /// <summary>
    /// Gestión general de T_DocumentoLegalAreaTrabajo
    /// </summary>
    public class ControlDocService : IControlDocService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ControlDocService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TControlDoc, ControlDoc>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public ControlDoc Add(ControlDoc entidad)
        {
            try
            {
                var modelo = _unitOfWork.ControlDocRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ControlDoc>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ControlDoc Update(ControlDoc entidad)
        {
            try
            {
                var modelo = _unitOfWork.ControlDocRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ControlDoc>(modelo);
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

        public List<ControlDoc> Add(List<ControlDoc> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ControlDocRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ControlDoc>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ControlDoc> Update(List<ControlDoc> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ControlDocRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ControlDoc>>(modelo);
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

        public List<ControlDocumentoMatriculaDTO> ObtenerDocumentosPorMatriculaCabeceraControl(int idMatriculaCabecera)
        {
            try
            {
                return _unitOfWork.ControlDocRepository.ObtenerDocumentosPorMatriculaCabeceraControl(idMatriculaCabecera);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ControlDocumentoDTO ActualizarControlDocumento(ControlDocumentoDTO entidad, string usuario)
        {

            try
            {
      
                if (_unitOfWork.ControlDocRepository.Exist(entidad.IdControlDoc))
                {
                    var controlDocBO = _unitOfWork.ControlDocRepository.FirstById(entidad.IdControlDoc);
                    controlDocBO.FechaModificacion = DateTime.Now;
                    controlDocBO.UsuarioModificacion = usuario;
                    controlDocBO.EstadoDocumento = entidad.EstadoDocumento;
                    controlDocBO.Recepcionado = entidad.Recepcionado;

                    var modelo = _unitOfWork.ControlDocRepository.Update(controlDocBO);
                    _unitOfWork.Commit();
                    return entidad;
                }
                else
                {
                    return null;
                }
         
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
