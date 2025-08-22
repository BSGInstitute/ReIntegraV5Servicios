using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;


namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    /// Service: ProgramaGeneralProblemaModalidadService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 04/08/2022
    /// <summary>
    /// Gestión general de T_ProgramaGeneralProblemaModalidad
    /// </summary>
    public class ProgramaGeneralProblemaModalidadService : IProgramaGeneralProblemaModalidadService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public ProgramaGeneralProblemaModalidadService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TProgramaGeneralProblemaModalidad, ProgramaGeneralProblemaModalidad>(MemberList.None).ReverseMap();
                    cfg.CreateMap<ProgramaGeneralProblemaModalidadDTO, ComboDTO>(MemberList.None);
                }
            );
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public ProgramaGeneralProblemaModalidad Add(ProgramaGeneralProblemaModalidad entidad)
        {
            try
            {
                var modelo = _unitOfWork.ProgramaGeneralProblemaModalidadRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ProgramaGeneralProblemaModalidad>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ProgramaGeneralProblemaModalidad Update(ProgramaGeneralProblemaModalidad entidad)
        {
            try
            {
                var modelo = _unitOfWork.ProgramaGeneralProblemaModalidadRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ProgramaGeneralProblemaModalidad>(modelo);
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
                _unitOfWork.ProgramaGeneralProblemaModalidadRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProgramaGeneralProblemaModalidad> Add(List<ProgramaGeneralProblemaModalidad> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ProgramaGeneralProblemaModalidadRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ProgramaGeneralProblemaModalidad>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProgramaGeneralProblemaModalidad> Update(List<ProgramaGeneralProblemaModalidad> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ProgramaGeneralProblemaModalidadRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ProgramaGeneralProblemaModalidad>>(modelo);
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
                _unitOfWork.ProgramaGeneralProblemaModalidadRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 04/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_ProgramaGeneralProblemaModalidad
        /// </summary>
        /// <returns> List<ProgramaGeneralProblemaModalidadDTO> </returns>
        public IEnumerable<ProgramaGeneralProblemaModalidadDTO> ObtenerProgramaGeneralProblemaModalidad()
        {
            try
            {
                return _unitOfWork.ProgramaGeneralProblemaModalidadRepository.ObtenerProgramaGeneralProblemaModalidad();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 04/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los registros de T_ProgramaGeneralProblemaModalidad asociados a un Problema.
        /// </summary>
        /// <param name="idProblema">Id del ProgramaGeneralProblema</param>
        /// <returns> List<ProgramaGeneralProblemaModalidadDTO> </returns>
        public IEnumerable<ProgramaGeneralProblemaModalidadDTO> ObtenerModalidadPorIdProblema(int idProblema)
        {
            try
            {
                return _unitOfWork.ProgramaGeneralProblemaModalidadRepository.ObtenerModalidadPorIdProblema(idProblema);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 05/08/2022
        /// Version: 1.0
        /// <summary>
        /// Elimina los Registros de ProgramaGeneralProblemaModalidad relacionados a un Problema.
        /// </summary>
        /// <param name="idProblema">Id del ProgramaGeneralProblema</param>
        /// <param name="usuario">Usuario asociado a la Eliminacion</param>
        /// <param name="nuevos">Datos a Insertar que no deberian eliminarse</param>
        /// <returns> List<ProgramaGeneralProblemaModalidadDTO> </returns>
        public void EliminacionLogicaPorProblema(int idProblema, string usuario, List<ModalidadCursoProblemaDTO> nuevos)
        {
            try
            {
                var paraBorrar = ObtenerModalidadPorIdProblema(idProblema).ToList();
                paraBorrar.RemoveAll(m => nuevos.Any(n => n.Id == m.Id));
                paraBorrar.ForEach(m => Delete(m.Id, usuario));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 05/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los atributos de ProgramaGeneralProblemaModalidad asociados a un Id
        /// </summary>
        /// <param name="idProgramaGeneralProblemaModalidad">Id de ProgramaGeneralProblemaModalidad</param>
        /// <returns> bool </returns>
        public ProgramaGeneralProblemaModalidad ObtenerEntidadPorId(int idProgramaGeneralProblemaModalidad)
        {
            try
            {
                return _mapper.Map<ProgramaGeneralProblemaModalidad>(_unitOfWork.ProgramaGeneralProblemaRepository.FirstById(idProgramaGeneralProblemaModalidad));
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
