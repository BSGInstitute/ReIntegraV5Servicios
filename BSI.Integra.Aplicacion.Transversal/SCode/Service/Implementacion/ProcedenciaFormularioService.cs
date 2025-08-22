using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: ProcedenciaFormularioService
    /// Autor: Margiory Meiss Ramirez Neyra.
    /// Fecha: 22/08/2022
    /// <summary>
    /// Gestión general de T_ProcedenciaFormulario
    /// </summary>
    public class ProcedenciaFormularioService : IProcedenciaFormularioService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ProcedenciaFormularioService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;


            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProcedenciaFormulario, ProcedenciaFormulario>(MemberList.None).ReverseMap();
                cfg.CreateMap<ProcedenciaFormularioDTO, ProcedenciaFormulario>(MemberList.None).ReverseMap();
            }
           );
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public ProcedenciaFormulario Add(ProcedenciaFormularioDTO entidad, string Usuario)
        {
            try
            {
                ProcedenciaFormulario data = _mapper.Map<ProcedenciaFormulario>(entidad);
                data.Id = 0;
                data.UsuarioModificacion = Usuario;
                data.UsuarioCreacion = Usuario;
                data.FechaCreacion = DateTime.Now;
                data.FechaModificacion = DateTime.Now;
                data.Estado = true;
                var modelo = _unitOfWork.ProcedenciaFormularioRepository.Add(data);
                _unitOfWork.Commit();
                return _mapper.Map<ProcedenciaFormulario>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ProcedenciaFormulario Update(ProcedenciaFormularioDTO entidad, string Usuario)
        {
            try
            {
                var rep = _unitOfWork.ProcedenciaFormularioRepository;
                var entidadActual = _mapper.Map<ProcedenciaFormulario>(rep.FirstById(entidad.Id));
                entidadActual.UsuarioModificacion = Usuario;
                entidadActual.FechaModificacion = DateTime.Now;
                entidadActual.Nombre = entidad.Nombre;
                entidadActual.Descripcion = entidad.Descripcion;
                var modelo = _unitOfWork.ProcedenciaFormularioRepository.Update(entidadActual);
                _unitOfWork.Commit();
                return _mapper.Map<ProcedenciaFormulario>(modelo);
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
                _unitOfWork.ProcedenciaFormularioRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProcedenciaFormulario> Add(List<ProcedenciaFormulario> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ProcedenciaFormularioRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ProcedenciaFormulario>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProcedenciaFormulario> Update(List<ProcedenciaFormulario> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ProcedenciaFormularioRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ProcedenciaFormulario>>(modelo);
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
                _unitOfWork.ProcedenciaFormularioRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        /// Autor: Margiory Meiss Ramirez Neyra.
        /// Fecha: 22/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_ProcedenciaFormulario para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<DTO.ComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.ProcedenciaFormularioRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Margiory Meiss Ramirez Neyra.
        /// Fecha: 22/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_ProcedenciaFormulario
        /// </summary>
        /// <returns> List<ProcedenciaFormularioDTO> </returns>
        public IEnumerable<ProcedenciaFormularioDTO> ObtenerProcedenciaFormulario()
        {
            try
            {
                return _unitOfWork.ProcedenciaFormularioRepository.ObtenerProcedenciaFormulario();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Margiory Meiss Ramirez Neyra.
        /// Fecha: 22/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_ProcedenciaFormulario
        /// </summary>
        /// <returns> List<ProcedenciaFormularioDTO> </returns>
        public IEnumerable<ProcedenciaFormularioFiltroDTO> ObtenerProcedenciaFormularioFiltro()
        {
            try
            {
                return _unitOfWork.ProcedenciaFormularioRepository.ObtenerProcedenciaFormularioFiltro();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Margiory Meiss Ramirez Neyra.
        /// Fecha: 22/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los filtros  de T_ProcedenciaFormulario
        /// </summary>
        /// <returns> List<ProcedenciaFormularioDTO> </returns>


        public IEnumerable<ProcedenciaFormularioFiltroDTO> ObtenerProcedenciaFormularioTodo()
        {
            try
            {
                return _unitOfWork.ProcedenciaFormularioRepository.ObtenerProcedenciaFormularioTodo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
