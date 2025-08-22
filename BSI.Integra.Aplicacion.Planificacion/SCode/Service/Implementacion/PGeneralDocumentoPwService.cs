using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    /// Service: PGeneralDocumentoPwService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 09/08/2022
    /// <summary>
    /// Gestión general de T_PGeneralDocumentoPw
    /// </summary>
    public class PGeneralDocumentoPwService : IPGeneralDocumentoPwService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public PGeneralDocumentoPwService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TPgeneralDocumentoPw, PGeneralDocumentoPw>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public PGeneralDocumentoPw Add(PGeneralDocumentoPw entidad)
        {
            try
            {
                var modelo = _unitOfWork.PGeneralDocumentoPwRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<PGeneralDocumentoPw>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PGeneralDocumentoPw Update(PGeneralDocumentoPw entidad)
        {
            try
            {
                var modelo = _unitOfWork.PGeneralDocumentoPwRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<PGeneralDocumentoPw>(modelo);
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
                _unitOfWork.PGeneralDocumentoPwRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PGeneralDocumentoPw> Add(List<PGeneralDocumentoPw> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.PGeneralDocumentoPwRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<PGeneralDocumentoPw>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PGeneralDocumentoPw> Update(List<PGeneralDocumentoPw> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.PGeneralDocumentoPwRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<PGeneralDocumentoPw>>(modelo);
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
                _unitOfWork.PGeneralDocumentoPwRepository.Delete(listadoIds, usuario);
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
        /// Fecha: 09/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_PGeneralDocumentoPw
        /// </summary>
        /// <returns> List<PGeneralDocumentoPwDTO> </returns>
        public IEnumerable<PGeneralDocumentoPwDTO> ObtenerPGeneralDocumentoPw()
        {
            try
            {
                return _unitOfWork.PGeneralDocumentoPwRepository.ObtenerPGeneralDocumentoPw();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Seccion Especifico asociada a un ProgramaGeneral con un Titulo especifico.
        /// </summary>
        /// <param name="idPGeneral">Id del Programa General</param>
        /// <param name="tituloSeccion">Titulo de la Seccion</param>
        /// <returns> PGeneralDocumentoSeccionDTO </returns>
        public PGeneralDocumentoSeccionDTO ObtenerSeccionDocumentoPGeneral(int idPGeneral, string tituloSeccion)
        {
            try
            {
                return _unitOfWork.PGeneralDocumentoPwRepository.ObtenerSeccionDocumentoPGeneral(idPGeneral, tituloSeccion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
