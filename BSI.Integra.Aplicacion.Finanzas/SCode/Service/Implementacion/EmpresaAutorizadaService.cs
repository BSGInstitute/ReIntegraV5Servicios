using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Implementacion
{
    /// Service: EmpresaAutorizadaService
    /// Autor Modificacion: Griselberto Huaman.
    /// Fecha: 24/06/2022
    /// <summary>
    /// Gestión general de T_EmpresaAutorizada
    /// </summary>
    public class EmpresaAutorizadaService : IEmpresaAutorizadaService
    {

        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public EmpresaAutorizadaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TEmpresaAutorizadum, EmpresaAutorizada>(MemberList.None).ReverseMap();
                cfg.CreateMap<EmpresaAutorizadaDTO, EmpresaAutorizada>(MemberList.None).ReverseMap();
            }
            );
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public EmpresaAutorizada Add(EmpresaAutorizadaDTO data, string Usuario)
        {
            try
            {
                var entidad = _mapper.Map<EmpresaAutorizada>(data);
                entidad.Id = 0;
                entidad.UsuarioCreacion = Usuario;
                entidad.UsuarioModificacion = Usuario;
                entidad.FechaModificacion = DateTime.Now;
                entidad.FechaCreacion = DateTime.Now;
                entidad.Estado = true;
                var modelo = _unitOfWork.EmpresaAutorizadaRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<EmpresaAutorizada>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EmpresaAutorizada Update(EmpresaAutorizadaDTO data, string Usuario)
        {
            try
            {
                var repositorio = _unitOfWork.EmpresaAutorizadaRepository;
                var antiguo = repositorio.FirstById(data.Id);
                EmpresaAutorizada entidadNueva = _mapper.Map<EmpresaAutorizada>(data);
                entidadNueva.UsuarioCreacion = antiguo.UsuarioCreacion;
                entidadNueva.UsuarioModificacion = Usuario;
                entidadNueva.FechaModificacion = DateTime.Now;
                entidadNueva.FechaCreacion = antiguo.FechaCreacion;
                entidadNueva.Estado = antiguo.Estado;

                var modelo = repositorio.Update(entidadNueva);
                _unitOfWork.Commit();
                return _mapper.Map<EmpresaAutorizada>(modelo);
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
                _unitOfWork.EmpresaAutorizadaRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EmpresaAutorizada> Add(List<EmpresaAutorizada> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.EmpresaAutorizadaRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<EmpresaAutorizada>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EmpresaAutorizada> Update(List<EmpresaAutorizada> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.EmpresaAutorizadaRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<EmpresaAutorizada>>(modelo);
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
                _unitOfWork.EmpresaAutorizadaRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        /// Autor: Griselberto Huaman.
        /// Fecha: 02/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_EmpresaAutorizadum para mostrarse en combo.
        /// </summary>
        /// <returns> Lista EmpresaAutorizadaComboDTO </returns>
        public IEnumerable<EmpresaAutorizadaComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.EmpresaAutorizadaRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}
