using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Implementacion
{
    /// Service: TipoServicioService
    /// Autor Modificacion: Griselberto Huaman.
    /// Fecha: 24/06/2022
    /// <summary>
    /// Gestión general de T_TipoServicio
    /// </summary>
    public class TipoServicioService : ITipoServicioService
    {

        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public TipoServicioService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TTipoServicio, TipoServicio>(MemberList.None).ReverseMap();
                cfg.CreateMap<TipoServiciosDTO, TipoServicio>(MemberList.None).ReverseMap();
            }
           );

         
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public TipoServicio Add(TipoServiciosDTO entidad, string Usuario)
        {
            try
            {

                TipoServicio data = _mapper.Map<TipoServicio>(entidad);
                data.Id = 0;
                data.UsuarioModificacion = Usuario;
                data.UsuarioCreacion = Usuario;
                data.FechaCreacion = DateTime.Now;
                data.FechaModificacion = DateTime.Now;
                data.Estado = true;
                var modelo = _unitOfWork.TipoServicioRepository.Add(data);
                _unitOfWork.Commit();
                return _mapper.Map<TipoServicio>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TipoServicio Update(TipoServiciosDTO entidad,string Usuario)
        {
            try
            {
                var rep = _unitOfWork.TipoServicioRepository;
                var entidadActual = _mapper.Map<TipoServicio>(rep.FirstById(entidad.Id));
                entidadActual.UsuarioModificacion = Usuario;
                entidadActual.FechaModificacion = DateTime.Now;
                entidadActual.Nombre = entidad.Nombre;
                entidadActual.Descripcion = entidad.Descripcion;
                var modelo = _unitOfWork.TipoServicioRepository.Update(entidadActual);
                _unitOfWork.Commit();
                return _mapper.Map<TipoServicio>(modelo);
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
                _unitOfWork.TipoServicioRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TipoServicio> Add(List<TipoServicio> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.TipoServicioRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<TipoServicio>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TipoServicio> Update(List<TipoServicio> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.TipoServicioRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<TipoServicio>>(modelo);
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
                _unitOfWork.TipoServicioRepository.Delete(listadoIds, usuario);
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
        /// Obtiene todos los registros de T_TipoServicio
        /// </summary>
        /// <returns> List<TipoServicioDTO> </returns>
        public IEnumerable<TipoServicioDTO> ObtenerTipoServicio()
        {
            try
            {
                return _unitOfWork.TipoServicioRepository.ObtenerTipoServicio();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor Modificacion: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_TipoServicio para mostrarse en combo.
        /// </summary>
        /// <returns> List<TipoServicioComboDTO> </returns>
        public IEnumerable<TipoServicioComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.TipoServicioRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}
