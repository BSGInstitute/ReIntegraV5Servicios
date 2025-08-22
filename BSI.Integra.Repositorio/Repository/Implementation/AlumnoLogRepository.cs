using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: AlumnoLogRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 14/07/2022
    /// <summary>
    /// Gestión general de T_AlumnoLog
    /// </summary>
    public class AlumnoLogRepository : GenericRepository<TAlumnoLog>, IAlumnoLogRepository
    {
        private Mapper _mapper;

        public AlumnoLogRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TAlumnoLog, AlumnoLog>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TAlumnoLog MapeoEntidad(AlumnoLog entidad)
        {
            try
            {
                //crea la entidad padre
                TAlumnoLog modelo = _mapper.Map<TAlumnoLog>(entidad);

                //mapea los hijos
                //if (entidad.ListadoHijoNivel1 != null && entidad.ListadoHijoNivel1.Count > 0)
                //{
                //    var listadoHijoNivel1 = _mapper.Map<List<THijo>>(entidad.ListadoHijoNivel1);
                //    foreach (var hijoNivel1 in listadoHijoNivel1)
                //    {
                //        modelo.THijo.Add(hijoNivel1);
                //    }
                //}

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TAlumnoLog Add(AlumnoLog entidad)
        {
            try
            {
                var AlumnoLog = MapeoEntidad(entidad);
                base.Insert(AlumnoLog);
                return AlumnoLog;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TAlumnoLog Update(AlumnoLog entidad)
        {
            try
            {
                var AlumnoLog = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                AlumnoLog.RowVersion = entidadExistente.RowVersion;

                base.Update(AlumnoLog);
                return AlumnoLog;
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
                base.Delete(id, usuario);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public IEnumerable<TAlumnoLog> Add(IEnumerable<AlumnoLog> listadoEntidad)
        {
            try
            {
                List<TAlumnoLog> listado = new List<TAlumnoLog>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }
                base.Insert(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<TAlumnoLog> Update(IEnumerable<AlumnoLog> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TAlumnoLog> listado = new List<TAlumnoLog>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }

                var infoExistente = base.GetBy(w => listadoEntidad.Select(s => s.Id).Contains(w.Id), s => new { s.Id, s.RowVersion });
                foreach (var item in listado)
                {
                    var entidadExistente = infoExistente.FirstOrDefault(w => w.Id == item.Id);
                    item.RowVersion = entidadExistente.RowVersion;
                }
                base.Update(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Delete(IEnumerable<int> listadoIds, string usuario)
        {
            try
            {
                base.Delete(listadoIds, usuario);
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
        /// <returns> List<AlumnoLogComboDTO> </returns>
        public IEnumerable<AlumnoLogAgendaDTO> ObtenerAlumnoLogParaAgendaPorIdAlumno(int idAlumno)
        {
            try
            {
                List<AlumnoLogAgendaDTO> alumnoLog = new List<AlumnoLogAgendaDTO>();
                var query = @"
                    SELECT Id,CampoActualizado,ValorAnterior,ValorNuevo,UsuarioCreacion,FechaCreacion
                    FROM mkt.T_AlumnoLog
                    WHERE Estado = 1 AND IdAlumno = @idAlumno
                    ORDER BY FechaCreacion DESC";
                var resultadoQuery = _dapperRepository.QueryDapper(query, new { idAlumno });
                if (!string.IsNullOrEmpty(resultadoQuery) && !resultadoQuery.Contains("[]"))
                {
                    alumnoLog = JsonConvert.DeserializeObject<List<AlumnoLogAgendaDTO>>(resultadoQuery);
                }
                return alumnoLog;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
