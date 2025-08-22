using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: FuentesPortalWebService
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_FuentesPortalWeb
    /// </summary>
    public class FuentesPortalWebService : IFuentesPortalWebService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public FuentesPortalWebService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TFuentesPortalWeb, FuentesPortalWeb>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        public FuentesPortalWeb Add(FuentesPortalWebEnvio data)
        {
            try
            {
                var repFuentesPortalWeb = _unitOfWork.FuentesPortalWebRepository;
                FuentesPortalWeb entidad = new FuentesPortalWeb();
                entidad.NombreArchivo = data.NombreArchivo;
                entidad.Url = data.Url;
                entidad.FechaCreacion = DateTime.Now;
                entidad.FechaModificacion = DateTime.Now;
                entidad.UsuarioCreacion = data.Usuario;
                entidad.UsuarioModificacion = data.Usuario;
                entidad.Estado = true;


                var modelo = _unitOfWork.FuentesPortalWebRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<FuentesPortalWeb>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public FuentesPortalWeb Update(FuentesPortalWebEnvio data)
        {
            try
            {
                var repFuentesPortalWeb = _unitOfWork.FuentesPortalWebRepository;
                FuentesPortalWeb entidad = new FuentesPortalWeb();
                entidad = _mapper.Map<FuentesPortalWeb>(repFuentesPortalWeb.FirstById(data.Id));
                entidad.NombreArchivo = data.NombreArchivo;
                entidad.Url = data.Url;
                entidad.FechaCreacion = DateTime.Now;
                entidad.FechaModificacion = DateTime.Now;
                entidad.UsuarioCreacion = data.Usuario;
                entidad.UsuarioModificacion = data.Usuario;
                entidad.Estado = true;

                var modelo = _unitOfWork.FuentesPortalWebRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<FuentesPortalWeb>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        #region Metodos Base

        public bool Delete(int id, string usuario)
        {
            try
            {
                _unitOfWork.FuentesPortalWebRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FuentesPortalWeb> Add(List<FuentesPortalWeb> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.FuentesPortalWebRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<FuentesPortalWeb>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FuentesPortalWeb> Update(List<FuentesPortalWeb> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.FuentesPortalWebRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<FuentesPortalWeb>>(modelo);
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
                _unitOfWork.FuentesPortalWebRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_FuentesPortalWeb para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<comboFuentes> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.FuentesPortalWebRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_FuentesPortalWeb
        /// </summary>
        /// <returns> List<FuentesPortalWebDTO> </returns>
        public IEnumerable<FuentesPortalWeb> ObtenerFuentesPortalWeb()
        {
            try
            {
                return _unitOfWork.FuentesPortalWebRepository.ObtenerFuentesPortalWeb();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
