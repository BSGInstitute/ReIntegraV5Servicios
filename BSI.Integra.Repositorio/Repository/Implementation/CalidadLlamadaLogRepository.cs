using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: CalidadLlamadaLogRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 13/08/2022
    /// <summary>
    /// Gestión general de T_CalidadLlamadaLog
    /// </summary>
    public class CalidadLlamadaLogRepository : GenericRepository<TCalidadLlamadaLog>, ICalidadLlamadaLogRepository
    {
        private Mapper _mapper;

        public CalidadLlamadaLogRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCalidadLlamadaLog, CalidadLlamadaLog>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TCalidadLlamadaLog MapeoEntidad(CalidadLlamadaLog entidad)
        {
            try
            {
                //crea la entidad padre
                TCalidadLlamadaLog modelo = _mapper.Map<TCalidadLlamadaLog>(entidad);

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

        public TCalidadLlamadaLog Add(CalidadLlamadaLog entidad)
        {
            try
            {
                var CalidadLlamadaLog = MapeoEntidad(entidad);
                base.Insert(CalidadLlamadaLog);
                return CalidadLlamadaLog;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TCalidadLlamadaLog AddAsync(CalidadLlamadaLog entidad)
        {
            try
            {
                var CalidadLlamadaLog = MapeoEntidad(entidad);
                base.InsertAsync(CalidadLlamadaLog);
                return CalidadLlamadaLog;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TCalidadLlamadaLog Update(CalidadLlamadaLog entidad)
        {
            try
            {
                var CalidadLlamadaLog = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                CalidadLlamadaLog.RowVersion = entidadExistente.RowVersion;

                base.Update(CalidadLlamadaLog);
                return CalidadLlamadaLog;
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


        public IEnumerable<TCalidadLlamadaLog> Add(IEnumerable<CalidadLlamadaLog> listadoEntidad)
        {
            try
            {
                List<TCalidadLlamadaLog> listado = new List<TCalidadLlamadaLog>();
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

        public IEnumerable<TCalidadLlamadaLog> Update(IEnumerable<CalidadLlamadaLog> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TCalidadLlamadaLog> listado = new List<TCalidadLlamadaLog>();
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
        /// Fecha: 13/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_CalidadLlamadaLog.
        /// </summary>
        /// <returns> List<CalidadLlamadaLogDTO> </returns>
        public IEnumerable<CalidadLlamadaLogDTO> ObtenerCalidadLlamadaLog()
        {
            try
            {
                List<CalidadLlamadaLogDTO> rpta = new List<CalidadLlamadaLogDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    IdProblemaLlamada,
	                    IdCalidadLlamada,
	                    IdActividadDetalle,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion
                    FROM com.T_CalidadLLamadaLog
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<CalidadLlamadaLogDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
