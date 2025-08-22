using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: ProveedorCampaniaIntegraService
    /// Autor: Margiory Ramirez Neyra.
    /// Fecha: 22/08/2022
    /// <summary>
    /// Gestión general de T_ProveedorCampaniaIntegra
    /// </summary>
    public class ProveedorCampaniaIntegraService : IProveedorCampaniaIntegraService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ProveedorCampaniaIntegraService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProveedorCampaniaIntegra, ProveedorCampaniaIntegra>(MemberList.None).ReverseMap();
                cfg.CreateMap<ProveedorCampaniaDTO, ProveedorCampaniaIntegra>(MemberList.None).ReverseMap();


            });
           
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public ProveedorCampaniaIntegra Add(ProveedorCampaniaDTO entidad, string Usuario)
        {
            try
            {
                ProveedorCampaniaIntegra data = _mapper.Map<ProveedorCampaniaIntegra>(entidad);
                data.Id = 0;
                data.UsuarioModificacion = Usuario;
                data.UsuarioCreacion = Usuario;
                data.FechaCreacion = DateTime.Now;
                data.FechaModificacion = DateTime.Now;
                data.Estado = true;
                var modelo = _unitOfWork.ProveedorCampaniaIntegraRepository.Add(data);
                _unitOfWork.Commit();
                return _mapper.Map<ProveedorCampaniaIntegra>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ProveedorCampaniaIntegra Update(ProveedorCampaniaDTO entidad, string Usuario)
        {
            try
            {
              
                var rep = _unitOfWork.ProveedorCampaniaIntegraRepository;
                var entidadActual = _mapper.Map<ProveedorCampaniaIntegra>(rep.FirstById(entidad.Id));
                entidadActual.UsuarioModificacion = Usuario;
                entidadActual.FechaModificacion = DateTime.Now;
                entidadActual.Nombre = entidad.Nombre;
                entidadActual.PorDefecto = entidad.PorDefecto;
                var modelo = _unitOfWork.ProveedorCampaniaIntegraRepository.Update(entidadActual);
                _unitOfWork.Commit();
                return _mapper.Map<ProveedorCampaniaIntegra>(modelo);
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
                _unitOfWork.ProveedorCampaniaIntegraRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProveedorCampaniaIntegra> Add(List<ProveedorCampaniaIntegra> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ProveedorCampaniaIntegraRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ProveedorCampaniaIntegra>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProveedorCampaniaIntegra> Update(List<ProveedorCampaniaIntegra> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ProveedorCampaniaIntegraRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ProveedorCampaniaIntegra>>(modelo);
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
                _unitOfWork.ProveedorCampaniaIntegraRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 22/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_ProveedorCampaniaIntegra para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<DTO.ComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.ProveedorCampaniaIntegraRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 22/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_ProveedorCampaniaIntegra
        /// </summary>
        /// <returns> List<ProveedorCampaniaIntegraDTO> </returns>
        public IEnumerable<ProveedorCampaniaIntegraDTO> ObtenerProveedorCampaniaIntegra()
        {
            try
            {
                return _unitOfWork.ProveedorCampaniaIntegraRepository.ObtenerProveedorCampaniaIntegra();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 18/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_ProveedorCampaniaIntegra
        /// </summary>
        /// <returns> List<ProveedorCampaniaIntegraDTO> </returns>
        public IEnumerable<ProveedorCampaniaIntegraFiltroDTO> ObtenerProveedorCampaniaIntegraFiltro()
        {
            try
            {
                return _unitOfWork.ProveedorCampaniaIntegraRepository.ObtenerProveedorCampaniaIntegraFiltro();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
