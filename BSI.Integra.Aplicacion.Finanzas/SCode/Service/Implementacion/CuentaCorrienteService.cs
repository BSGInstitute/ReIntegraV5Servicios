using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Implementacion
{
    /// Service: CuentaCorrienteService
    /// Autor Modificacion: Griselberto Huaman.
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_CuentaCorriente
    /// </summary>
    public class CuentaCorrienteService : ICuentaCorrienteService
    {

        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public CuentaCorrienteService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCuentaCorriente, CuentaCorriente>(MemberList.None).ReverseMap();
                cfg.CreateMap<CuentaBancariaRecibidoDTO, CuentaCorriente>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public CuentaCorriente Add(CuentaBancariaRecibidoDTO data, string Usuario)
        {
            try
            {
                CuentaCorriente entidad = _mapper.Map<CuentaCorriente>(data);
                entidad.Id = 0;
                entidad.UsuarioCreacion = Usuario;
                entidad.UsuarioModificacion = Usuario;
                entidad.FechaModificacion = DateTime.Now;
                entidad.FechaCreacion = DateTime.Now;
                entidad.Estado = true;

                var modelo = _unitOfWork.CuentaCorrienteRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<CuentaCorriente>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CuentaCorriente Update(CuentaBancariaRecibidoDTO data, string Usuario)
        {
            try
            {
                var repositorio = _unitOfWork.CuentaCorrienteRepository;
                var antiguo = repositorio.FirstById(data.Id);
                CuentaCorriente entidadNueva = _mapper.Map<CuentaCorriente>(data);
                entidadNueva.UsuarioCreacion = antiguo.UsuarioCreacion;
                entidadNueva.UsuarioModificacion = Usuario;
                entidadNueva.FechaModificacion = DateTime.Now;
                entidadNueva.FechaCreacion = antiguo.FechaCreacion;
                entidadNueva.Estado = antiguo.Estado;

                var modelo = _unitOfWork.CuentaCorrienteRepository.Update(entidadNueva);
                _unitOfWork.Commit();
                return _mapper.Map<CuentaCorriente>(modelo);
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
                _unitOfWork.CuentaCorrienteRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CuentaCorriente> Add(List<CuentaCorriente> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.CuentaCorrienteRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<CuentaCorriente>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CuentaCorriente> Update(List<CuentaCorriente> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.CuentaCorrienteRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<CuentaCorriente>>(modelo);
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
                _unitOfWork.CuentaCorrienteRepository.Delete(listadoIds, usuario);
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
        /// Obtiene todos los registros de T_CuentaCorriente
        /// </summary>
        /// <returns> List<CuentaCorrientesDTO> </returns>
        public IEnumerable<CuentaCorrientesDTO> ObtenerCuentaCorriente()
        {
            try
            {
                return _unitOfWork.CuentaCorrienteRepository.ObtenerCuentaCorriente();
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
        /// Obtiene todos los registros de V_ObtenerCuentasBancarias.
        /// </summary>
        /// <returns> List<CuentaCorrientesDTO> </returns>
        public IEnumerable<CuentaBancariaDTO> ObtenerCuentaBancaria()
        {
            try
            {
                return _unitOfWork.CuentaCorrienteRepository.ObtenerCuentaBancaria();
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
        /// Obtiene todos los registros de V_ObtenerCuentaEntidadCiudad.
        /// </summary>
        /// <returns> List<CuentaCorrientesDTO> </returns>
        public IEnumerable<CuentaCorrienteEntidadCiudadDTO> ObtenerCuentaCorrienteConEntidad()
        {
            try
            {
                return _unitOfWork.CuentaCorrienteRepository.ObtenerCuentaCorrienteConEntidad();
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
        /// Obtiene registros de T_CuentaCorriente para mostrarse en combo.
        /// </summary>
        /// <returns> List<CuentaCorrienteComboDTO> </returns>
        public IEnumerable<CuentaCorrienteComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.CuentaCorrienteRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Retorna Solo una Cuenta Corriente Correspondiente al Id.
        /// </summary>
        /// <returns> List<CuentaCorrienteComboDTO> </returns>
        /// <param name="Id"> Corresponde al Id de busqueda apra cuenta corriente </param>
        public string ObtenerCuentaCorrienteById(int Id)
        {
            try
            {
                return _unitOfWork.CuentaCorrienteRepository.ObtenerCuentaCorrienteById(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




    }

}
