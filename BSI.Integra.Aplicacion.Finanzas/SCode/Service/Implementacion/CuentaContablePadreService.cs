using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Implementacion
{
    /// Service: CuentaContablePadreService
    /// Autor Modificacion: Griselberto Huaman.
    /// Fecha: 24/06/2022
    /// <summary>
    /// Gestión general de T_CuentaContablePadre
    /// </summary>
    public class CuentaContablePadreService : ICuentaContablePadreService
    {

        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public CuentaContablePadreService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCuentaContablePadre, CuentaContablePadre>(MemberList.None).ReverseMap();
                cfg.CreateMap<CuentasContablePadreDTO, CuentaContablePadre>(MemberList.None).ReverseMap();
            }
            );

            
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public CuentaContablePadre Add(CuentasContablePadreDTO entidad, string Usuario)
        {
            try
            {
                CuentaContablePadre data = _mapper.Map<CuentaContablePadre>(entidad);
                data.Id = 0;
                data.UsuarioModificacion = Usuario;
                data.UsuarioCreacion = Usuario;
                data.FechaCreacion = DateTime.Now;
                data.FechaModificacion = DateTime.Now;
                data.Estado = true;
                var modelo = _unitOfWork.CuentaContablePadreRepository.Add(data);
                _unitOfWork.Commit();
                return _mapper.Map<CuentaContablePadre>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CuentaContablePadre Update(CuentasContablePadreDTO entidad,string Usuario)
        {
            try
            {
                var rep = _unitOfWork.CuentaContablePadreRepository;
                var entidadActual = _mapper.Map<CuentaContablePadre>(rep.FirstById(entidad.Id));
                entidadActual.UsuarioModificacion = Usuario;
                entidadActual.FechaModificacion = DateTime.Now;
                entidadActual.CuentaPadre = entidad.CuentaPadre;
                entidadActual.Descripcion = entidad.Descripcion;
                var modelo = _unitOfWork.CuentaContablePadreRepository.Update(entidadActual);
                _unitOfWork.Commit();
                return _mapper.Map<CuentaContablePadre>(modelo);
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
                _unitOfWork.CuentaContablePadreRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CuentaContablePadre> Add(List<CuentaContablePadre> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.CuentaContablePadreRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<CuentaContablePadre>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CuentaContablePadre> Update(List<CuentaContablePadre> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.CuentaContablePadreRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<CuentaContablePadre>>(modelo);
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
                _unitOfWork.CuentaContablePadreRepository.Delete(listadoIds, usuario);
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
        /// Obtiene todos los registros de T_CuentaContablePadre
        /// </summary>
        /// <returns> List<CuentaContablePadreDTO> </returns>
        public IEnumerable<CuentaContablePadreDTO> ObtenerCuentaContablePadre()
        {
            try
            {
                return _unitOfWork.CuentaContablePadreRepository.ObtenerCuentaContablePadre();
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
        /// Obtiene registros de T_CuentaContablePadre para mostrarse en combo.
        /// </summary>
        /// <returns> List<CuentaContablePadreComboDTO> </returns>
        public IEnumerable<CuentaContablePadreComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.CuentaContablePadreRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}
