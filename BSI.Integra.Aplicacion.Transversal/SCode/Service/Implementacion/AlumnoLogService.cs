using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: AlumnoLogService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 14/07/2022
    /// <summary>
    /// Gestión general de T_AlumnoLog
    /// </summary>
    public class AlumnoLogService : IAlumnoLogService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public AlumnoLogService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TAlumnoLog, AlumnoLog>(MemberList.None).ReverseMap();
                cfg.CreateMap<AlumnoLogDTO, AlumnoLog>(MemberList.None);
            }
            );
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public AlumnoLog Add(AlumnoLog entidad)
        {
            try
            {
                var modelo = _unitOfWork.AlumnoLogRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<AlumnoLog>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public AlumnoLog Update(AlumnoLog entidad)
        {
            try
            {
                var modelo = _unitOfWork.AlumnoLogRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<AlumnoLog>(modelo);
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
                _unitOfWork.AlumnoLogRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AlumnoLog> Add(List<AlumnoLog> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.AlumnoLogRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<AlumnoLog>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AlumnoLog> Update(List<AlumnoLog> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.AlumnoLogRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<AlumnoLog>>(modelo);
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
                _unitOfWork.AlumnoLogRepository.Delete(listadoIds, usuario);
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
        /// Fecha: 02/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene log del alumno relacionado a un Identificador de Alumno
        /// </summary>
        /// <param name="idAlumno">Id del Alumno</param>
        /// <returns> List<AlumnoLogAgendaFechaStringDTO> </returns>
        public IEnumerable<AlumnoLogAgendaFechaStringDTO> ObtenerAlumnoLogParaAgendaPorIdAlumno(int idAlumno)
        {
            try
            {
                var logRepositorio = _unitOfWork.AlumnoLogRepository.ObtenerAlumnoLogParaAgendaPorIdAlumno(idAlumno);
                var historialModificacion = logRepositorio.Select(p => new AlumnoLogAgendaFechaStringDTO
                {
                    Id = p.Id,
                    CampoActualizado = p.CampoActualizado,
                    ValorAnterior = p.ValorAnterior,
                    ValorNuevo = p.ValorNuevo,
                    UsuarioCreacion = p.UsuarioCreacion,
                    FechaCreacion = p.FechaCreacion.ToString("dd/MM/yyyy HH:mm")
                });
                return historialModificacion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 19/08/2022
        /// Version: 1.0
        /// <summary>
        /// Construye una Entidad AlumnoLog segun los argumentos enviados
        /// </summary>
        /// <param name="idAlumno">Id del Alumno</param>
        /// <param name="campoActualizado">Campo Actualizado</param>
        /// <param name="valorAnterior">Valor Anterior</param>
        /// <param name="valorNuevo">Valor Nuevo</param>
        /// <param name="usuario">Usuario que modifica el valor</param>
        /// <returns> AlumnoLog </returns>
        public AlumnoLog ConstruirEntidadAlumnoLog(int idAlumno, string campoActualizado, string valorAnterior, string valorNuevo, string usuario)
        {
            try
            {
                return new AlumnoLog()
                {
                    IdAlumno = idAlumno,
                    CampoActualizado = campoActualizado,
                    ValorAnterior = valorAnterior,
                    ValorNuevo = valorNuevo,
                    Estado = true,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
