using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Operaciones.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Operaciones.Service.Implementacion
{
    /// Service: IvrEjecucionService
    /// Autor Modificacion: Griselberto Huaman.
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_IvrEjecucion
    /// </summary>
    public class IvrEjecucionService : IIvrEjecucionService
    {

        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public IvrEjecucionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TIvrEjecucion, IvrEjecucion>(MemberList.None).ReverseMap();
                cfg.CreateMap<IvrEjecucionDTO, IvrEjecucion>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        

        #region Metodos Base
        public IvrEjecucion Add(IvrEjecucionDTO data,string Usuario)
        {
            try
            {
                IvrEjecucion entidad = _mapper.Map<IvrEjecucion>(data);
                entidad.Id = 0;
                entidad.UsuarioCreacion = Usuario;
                entidad.UsuarioModificacion = Usuario;
                entidad.FechaModificacion = DateTime.Now;
                entidad.FechaCreacion = DateTime.Now;
                entidad.Estado = true;

                var modelo = _unitOfWork.IvrEjecucionRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<IvrEjecucion>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IvrEjecucion Update(IvrEjecucionDTO data, string Usuario)
        {
            try
            {
                var repositorioIvrEjecucion = _unitOfWork.IvrEjecucionRepository;
                var entidad = _mapper.Map<IvrEjecucion>(repositorioIvrEjecucion.FirstById(data.Id));
                entidad.Nombre = data.Nombre;
                entidad.Descripcion = data.Descripcion;
                entidad.UsuarioModificacion = Usuario;
                entidad.FechaModificacion = DateTime.Now;

                var modelo = _unitOfWork.IvrEjecucionRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<IvrEjecucion>(modelo);
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
                _unitOfWork.IvrEjecucionRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<IvrEjecucion> Add(List<IvrEjecucion> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.IvrEjecucionRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<IvrEjecucion>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<IvrEjecucion> Update(List<IvrEjecucion> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.IvrEjecucionRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<IvrEjecucion>>(modelo);
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
                _unitOfWork.IvrEjecucionRepository.Delete(listadoIds, usuario);
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
        /// Obtiene todos los registros de T_IvrEjecucion
        /// </summary>
        /// <returns> List<IvrEjecucionDTO> </returns>
        public IEnumerable<IvrEjecucionDTO> ObtenerIvrEjecucion()
        {
            try
            {
                return _unitOfWork.IvrEjecucionRepository.ObtenerIvrEjecucion();
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
        /// Obtiene registros de T_IvrEjecucion para mostrarse en combo.
        /// </summary>
        /// <returns> List<IvrEjecucionComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.IvrEjecucionRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}
