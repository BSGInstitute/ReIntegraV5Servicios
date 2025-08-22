using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Operaciones.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Operaciones.Service.Implementacion
{
    /// Service: IvrTipoConfiguracionService
    /// Autor Modificacion: Griselberto Huaman.
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_IvrTipoConfiguracion
    /// </summary>
    public class IvrTipoConfiguracionService : IIvrTipoConfiguracionService
    {

        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public IvrTipoConfiguracionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TIvrTipoConfiguracion, IvrTipoConfiguracion>(MemberList.None).ReverseMap();
                cfg.CreateMap<IvrTipoConfiguracionDTO, IvrTipoConfiguracion>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        

        #region Metodos Base
        public IvrTipoConfiguracion Add(IvrTipoConfiguracionDTO data,string Usuario)
        {
            try
            {
                IvrTipoConfiguracion entidad = _mapper.Map<IvrTipoConfiguracion>(data);
                entidad.Id = 0;
                entidad.UsuarioCreacion = Usuario;
                entidad.UsuarioModificacion = Usuario;
                entidad.FechaModificacion = DateTime.Now;
                entidad.FechaCreacion = DateTime.Now;
                entidad.Estado = true;

                var modelo = _unitOfWork.IvrTipoConfiguracionRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<IvrTipoConfiguracion>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IvrTipoConfiguracion Update(IvrTipoConfiguracionDTO data, string Usuario)
        {
            try
            {
                var repositorioIvrTipoConfiguracion = _unitOfWork.IvrTipoConfiguracionRepository;
                var entidad = _mapper.Map<IvrTipoConfiguracion>(repositorioIvrTipoConfiguracion.FirstById(data.Id));
                entidad.Nombre = data.Nombre;
                entidad.Descripcion = data.Descripcion;
                entidad.UsuarioModificacion = Usuario;
                entidad.FechaModificacion = DateTime.Now;

                var modelo = _unitOfWork.IvrTipoConfiguracionRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<IvrTipoConfiguracion>(modelo);
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
                _unitOfWork.IvrTipoConfiguracionRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<IvrTipoConfiguracion> Add(List<IvrTipoConfiguracion> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.IvrTipoConfiguracionRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<IvrTipoConfiguracion>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<IvrTipoConfiguracion> Update(List<IvrTipoConfiguracion> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.IvrTipoConfiguracionRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<IvrTipoConfiguracion>>(modelo);
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
                _unitOfWork.IvrTipoConfiguracionRepository.Delete(listadoIds, usuario);
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
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_IvrTipoConfiguracion
        /// </summary>
        /// <returns> List<IvrTipoConfiguracionDTO> </returns>
        public IEnumerable<IvrTipoConfiguracionDTO> ObtenerIvrTipoConfiguracion()
        {
            try
            {
                return _unitOfWork.IvrTipoConfiguracionRepository.ObtenerIvrTipoConfiguracion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor Modificacion: Griselberto Huaman.
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_IvrTipoConfiguracion para mostrarse en combo.
        /// </summary>
        /// <returns> List<IvrTipoConfiguracionComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.IvrTipoConfiguracionRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}
