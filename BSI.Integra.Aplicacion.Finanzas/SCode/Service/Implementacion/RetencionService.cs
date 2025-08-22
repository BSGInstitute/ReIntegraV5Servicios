using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Implementacion
{
    /// Service: RetencionService
    /// Autor Modificacion: Griselberto Huaman.
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_Retencion
    /// </summary>
    public class RetencionService : IRetencionService
    {

        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public RetencionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TRetencion, Retencion>(MemberList.None).ReverseMap();
                cfg.CreateMap<RetencionRecibidoDTO, Retencion>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        

        #region Metodos Base
        public Retencion Add(RetencionRecibidoDTO data,string Usuario)
        {
            try
            {
                Retencion entidad = _mapper.Map<Retencion>(data);
                entidad.Id = 0;
                entidad.UsuarioCreacion = Usuario;
                entidad.UsuarioModificacion = Usuario;
                entidad.FechaModificacion = DateTime.Now;
                entidad.FechaCreacion = DateTime.Now;
                entidad.Estado = true;

                var modelo = _unitOfWork.RetencionRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<Retencion>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Retencion Update(RetencionRecibidoDTO data, string Usuario)
        {
            try
            {
                var repositorioRetencion = _unitOfWork.RetencionRepository;
                var entidad = _mapper.Map<Retencion>(repositorioRetencion.FirstById(data.Id));
                entidad.Valor = data.Valor;
                entidad.Descripcion = data.Descripcion;
                entidad.Nombre = data.Nombre;
                entidad.IdPais = data.IdPais;
                entidad.UsuarioModificacion = Usuario;
                entidad.FechaModificacion = DateTime.Now;

                var modelo = _unitOfWork.RetencionRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<Retencion>(modelo);
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
                _unitOfWork.RetencionRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Retencion> Add(List<Retencion> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.RetencionRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<Retencion>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Retencion> Update(List<Retencion> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.RetencionRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<Retencion>>(modelo);
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
                _unitOfWork.RetencionRepository.Delete(listadoIds, usuario);
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
        /// Obtiene todos los registros de T_Retencion
        /// </summary>
        /// <returns> List<RetencionDTO> </returns>
        public IEnumerable<RetencionDTO> ObtenerRetencion()
        {
            try
            {
                return _unitOfWork.RetencionRepository.ObtenerRetencion();
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
        /// Obtiene registros de T_Retencion para mostrarse en combo.
        /// </summary>
        /// <returns> List<RetencionComboDTO> </returns>
        public IEnumerable<RetencionComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.RetencionRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}
