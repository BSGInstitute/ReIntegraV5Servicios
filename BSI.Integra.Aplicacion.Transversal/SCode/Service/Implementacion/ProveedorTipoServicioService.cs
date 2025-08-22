using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: ProveedorTipoServicioService
    /// Autor: Griselberto Huaman.
    /// Fecha: 07/07/2022
    /// <summary>
    /// Gestión general de T_ProveedorTipoServicio
    /// </summary>
    public class ProveedorTipoServicioService : IProveedorTipoServicioService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ProveedorTipoServicioService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TProveedorTipoServicio, ProveedorTipoServicio>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public ProveedorTipoServicio Add(ProveedorTipoServicio entidad)
        {
            try
            {
                var modelo = _unitOfWork.ProveedorTipoServicioRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ProveedorTipoServicio>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ProveedorTipoServicio Update(ProveedorTipoServicio entidad)
        {
            try
            {
                var modelo = _unitOfWork.ProveedorTipoServicioRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ProveedorTipoServicio>(modelo);
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
                _unitOfWork.ProveedorTipoServicioRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProveedorTipoServicio> Add(List<ProveedorTipoServicio> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ProveedorTipoServicioRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ProveedorTipoServicio>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProveedorTipoServicio> Update(List<ProveedorTipoServicio> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ProveedorTipoServicioRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ProveedorTipoServicio>>(modelo);
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
                _unitOfWork.ProveedorTipoServicioRepository.Delete(listadoIds, usuario);
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
        /// Obtiene todos los registros de T_ProveedorTipoServicio.
        /// </summary>
        /// /// <param name="listaIdProveedor"></param>
        /// <returns> List<ProveedorTipoServicioDTO> </returns>
        public IEnumerable<ProveedorTipoServicioDTO> ObtenerProveedorTipoServicio(List<int> listaIdProveedor)
        {
            try
            {
                return _unitOfWork.ProveedorTipoServicioRepository.ObtenerProveedorTipoServicio(listaIdProveedor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Griselberto Huaman.
        /// Fecha: 07/07/2022
        /// Version: 1.0
        /// <summary>
        /// Elimina de forma Logica los registros asociados al proveedor
        /// </summary>
        /// <param name="idProveedor">Identificador del Proveedor</param>
        /// <param name="usuario">Usuario que llamo al End Point</param>
        /// <param name="nuevos"></param>
        public void EliminacionLogicoPorPlantilla(int idProveedor, string usuario, List<int> nuevos)
        {
            try
            {
                _unitOfWork.ProveedorTipoServicioRepository.EliminacionLogicoPorPlantilla(idProveedor, usuario, nuevos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
