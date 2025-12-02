using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: CampoContactoService
    /// Autor: Margiory Ramirez Neyra.
    /// Fecha: 13/09/2022
    /// <summary>
    /// Gestión general de T_CampoContacto
    /// </summary>
    public class CampoContactoService : ICampoContactoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public CampoContactoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCampoContacto, CampoContacto>(MemberList.None).ReverseMap();
                cfg.CreateMap<CampoContactoDTO, CampoContacto>(MemberList.None).ReverseMap();
            }
            );

            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public CampoContacto Add(CampoContactoDTO entidad, string Usuario)
        {
            try
            {

                CampoContacto data = _mapper.Map<CampoContacto>(entidad);
                data.Id = 0;
                data.UsuarioModificacion = Usuario;
                data.UsuarioCreacion = Usuario;
                data.FechaCreacion = DateTime.Now;
                data.FechaModificacion = DateTime.Now;
                data.Estado = true;
                var modelo = _unitOfWork.CampoContactoRepository.Add(data);
                _unitOfWork.Commit();
                return _mapper.Map<CampoContacto>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CampoContacto Update(CampoContactoDTO entidad, string Usuario)
        {
            try
            {
                var rep = _unitOfWork.CampoContactoRepository;
                var entidadActual = _mapper.Map<CampoContacto>(rep.FirstById(entidad.Id));
                entidadActual.UsuarioModificacion = Usuario;
                entidadActual.FechaModificacion = DateTime.Now;
                entidadActual.Nombre = entidad.Nombre;
                entidadActual.NombreLabel = entidad.NombreLabel;
                entidadActual.TipoControl = entidad.TipoControl;
                entidadActual.ValoresPreEstablecidos = entidad.ValoresPreEstablecidos;
                entidadActual.Procedimiento = entidad.Procedimiento;
                var modelo = _unitOfWork.CampoContactoRepository.Update(entidadActual);
                _unitOfWork.Commit();
                return _mapper.Map<CampoContacto>(modelo);
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
                _unitOfWork.CampoContactoRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CampoContacto> Add(List<CampoContacto> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.CampoContactoRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<CampoContacto>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CampoContacto> Update(List<CampoContacto> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.CampoContactoRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<CampoContacto>>(modelo);
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
                _unitOfWork.CampoContactoRepository.Delete(listadoIds, usuario);
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
        /// Fecha: 13/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_CampoContacto para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<DTO.ComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.CampoContactoRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 13/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_CampoContacto
        /// </summary>
        /// <returns> List<CampoContactoDTO> </returns>
        public IEnumerable<CampoContactoDTO> ObtenerCampoContacto()
        {
            try
            {
                return _unitOfWork.CampoContactoRepository.ObtenerCampoContacto();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 13/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros para filtros  de T_CampoContacto
        /// </summary>
        /// <returns> List<CampoContactoDTO> </returns>
        public IEnumerable<CampoContactoFiltroDTO> ObtenerFiltroCampoContacto()
        {
            try
            {
                return _unitOfWork.CampoContactoRepository.ObtenerFiltroCampoContacto();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 13/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los filtros de T_CampoContacto
        /// </summary>
        /// <returns> List<CampoContactoDTO> </returns>

        public IEnumerable<CampoContactoTodoDTO> ObtenerFiltroCampoContactoTodo()
        {
            try
            {
                return _unitOfWork.CampoContactoRepository.ObtenerFiltroCampoContactoTodo();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
