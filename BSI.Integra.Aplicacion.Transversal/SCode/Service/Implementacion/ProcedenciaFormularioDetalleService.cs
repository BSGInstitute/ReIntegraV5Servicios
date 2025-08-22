using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: ProcedenciaFormularioDetalleService
    /// Autor: Margiory Meiss Ramirez Neyra.
    /// Fecha: 22/08/2022
    /// <summary>
    /// Gestión general de T_ProcedenciaFormularioDetalle
    /// </summary>
    public class ProcedenciaFormularioDetalleService : IProcedenciaFormularioDetalleService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ProcedenciaFormularioDetalleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProcedenciaFormularioDetalle, ProcedenciaFormularioDetalle>(MemberList.None).ReverseMap();
                cfg.CreateMap<ProcedenciaFormularioDetalleDTO, ProcedenciaFormularioDetalle>(MemberList.None).ReverseMap();
            }
          );


            
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public ProcedenciaFormularioDetalle Add(ProcedenciaFormularioDetalle entidad)
        {
            try
            {
                var modelo = _unitOfWork.ProcedenciaFormularioDetalleRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ProcedenciaFormularioDetalle>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ProcedenciaFormularioDetalle Update(ProcedenciaFormularioDetalle entidad)
        {
            try
            {
                var modelo = _unitOfWork.ProcedenciaFormularioDetalleRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ProcedenciaFormularioDetalle>(modelo);
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
                _unitOfWork.ProcedenciaFormularioDetalleRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProcedenciaFormularioDetalle> Add(List<ProcedenciaFormularioDetalleDTO> listadoEntidad,string Usuario)
        {
            try
            {
                List<ProcedenciaFormularioDetalle> data = _mapper.Map<List<ProcedenciaFormularioDetalle>>(listadoEntidad);
                foreach (var item in data)
                {
                    item.Id = 0;
                    item.UsuarioModificacion = Usuario;
                    item.UsuarioCreacion = Usuario;
                    item.FechaCreacion = DateTime.Now;
                    item.FechaModificacion = DateTime.Now;
                    item.Estado = true;
                };
                var modelo = _unitOfWork.ProcedenciaFormularioDetalleRepository.Add(data);
                _unitOfWork.Commit();
                return _mapper.Map<List<ProcedenciaFormularioDetalle>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProcedenciaFormularioDetalle> Update(List<ProcedenciaFormularioDetalle> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ProcedenciaFormularioDetalleRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ProcedenciaFormularioDetalle>>(modelo);
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
                _unitOfWork.ProcedenciaFormularioDetalleRepository.Delete(listadoIds, usuario);
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
        /// Obtiene registros de T_ProcedenciaFormularioDetalle para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<DTO.ComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.ProcedenciaFormularioDetalleRepository.ObtenerCombo();
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
        /// Obtiene todos los registros de T_ProcedenciaFormularioDetalle
        /// </summary>
        /// <returns> List<ProcedenciaFormularioDetalleDTO> </returns>
        public IEnumerable<ProcedenciaFormularioDetalleDTO> ObtenerProcedenciaFormularioDetalle()
        {
            try
            {
                return _unitOfWork.ProcedenciaFormularioDetalleRepository.ObtenerProcedenciaFormularioDetalle();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor:Margiory Meiss Ramirez Neyra.
        /// Fecha: 22/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene datos de T_ProcedenciaFormularioDetalle.
        /// </summary>
        /// <param name="IdProcedenciaFormulario">Id del Alumno</param>
        /// <returns> List<PasarelaPagoPwAgendaDTO> </returns>
        public IEnumerable<ProcedenciaFormularioDetalleInteraccionDTO> ObtenerProcedenciaFormularioDetallePorIdProcedenciaFormulario(int IdProcedenciaFormulario)
        {
            try
            {
                return _unitOfWork.ProcedenciaFormularioDetalleRepository.ObtenerProcedenciaFormularioDetallePorIdProcedenciaFormulario(IdProcedenciaFormulario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
