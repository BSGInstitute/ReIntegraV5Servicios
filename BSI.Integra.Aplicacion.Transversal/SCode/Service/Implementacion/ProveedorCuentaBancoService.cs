using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: ProveedorCuentaBancoService
    /// Autor: Griselberto Huaman.
    /// Fecha: 07/07/2022
    /// <summary>
    /// Gestión general de T_ProveedorCuentaBanco
    /// </summary>
    public class ProveedorCuentaBancoService : IProveedorCuentaBancoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ProveedorCuentaBancoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TProveedorCuentaBanco, ProveedorCuentaBanco>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public ProveedorCuentaBanco Add(ProveedorCuentaBanco entidad)
        {
            try
            {
                var modelo = _unitOfWork.ProveedorCuentaBancoRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ProveedorCuentaBanco>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ProveedorCuentaBanco Update(ProveedorCuentaBanco entidad)
        {
            try
            {
                var modelo = _unitOfWork.ProveedorCuentaBancoRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ProveedorCuentaBanco>(modelo);
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
                _unitOfWork.ProveedorCuentaBancoRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProveedorCuentaBanco> Add(List<ProveedorCuentaBanco> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ProveedorCuentaBancoRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ProveedorCuentaBanco>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProveedorCuentaBanco> Update(List<ProveedorCuentaBanco> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ProveedorCuentaBancoRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ProveedorCuentaBanco>>(modelo);
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
                _unitOfWork.ProveedorCuentaBancoRepository.Delete(listadoIds, usuario);
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
        /// Fecha: 07/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene datos para llenar grilla
        /// </summary>
        /// /// <param name="IdProveedor">Identificador del Proveedor</param>
        /// <returns> List<ProveedorCuentaBancoDTO> </returns>
        public IEnumerable<ProveedorCuentaBancoDTO> ObtenerCuentasProveedorById(int IdProveedor)
        {
            try
            {
                return _unitOfWork.ProveedorCuentaBancoRepository.ObtenerCuentasProveedorById(IdProveedor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
