using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ContadorBicLogRepository
    /// Autor: Flavio R. Mamani Fabian
    /// Fecha: 29/08/2023
    /// <summary>
    /// Gestión general de T_ContadorBicLog
    /// </summary>
    public class ContadorBicLogRepository : GenericRepository<TContadorBicLog>, IContadorBicLogRepository
    {
        private Mapper _mapper;

        public ContadorBicLogRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TContadorBicLog, ContadorBicLog>(MemberList.None).ReverseMap();
                cfg.CreateMap<TContadorBicLogDetalle, ContadorBicLogDetalle>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TContadorBicLog MapeoEntidad(ContadorBicLog entidad)
        {
            try
            {
                TContadorBicLog modelo = _mapper.Map<TContadorBicLog>(entidad);
                if (entidad.ContadorBicLogDetalles != null && entidad.ContadorBicLogDetalles.Count() > 0)
                {
                    modelo.TContadorBicLogDetalles = _mapper.Map<ICollection<TContadorBicLogDetalle>>(entidad.ContadorBicLogDetalles);
                }
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private IEnumerable<TContadorBicLog> MapeoEntidad(IEnumerable<ContadorBicLog> entidad)
        {
            try
            {
                List<TContadorBicLog> modelo = new();
                foreach (var item in entidad)
                {
                    TContadorBicLog obj = _mapper.Map<TContadorBicLog>(item);
                    if (item.ContadorBicLogDetalles != null && item.ContadorBicLogDetalles.Count() > 0)
                    {
                        obj.TContadorBicLogDetalles = _mapper.Map<ICollection<TContadorBicLogDetalle>>(item.ContadorBicLogDetalles);
                    }
                    modelo.Add(obj);
                };
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TContadorBicLog Add(ContadorBicLog entidad)
        {
            try
            {
                var agregarEntidad = MapeoEntidad(entidad);
                Insert(agregarEntidad);
                return agregarEntidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TContadorBicLog Update(ContadorBicLog entidad)
        {
            try
            {
                var actualizarEntidad = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                actualizarEntidad.RowVersion = entidadExistente.RowVersion;
                Update(actualizarEntidad);
                return actualizarEntidad;
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
        public IEnumerable<TContadorBicLog> Add(IEnumerable<ContadorBicLog> listadoEntidad)
        {
            try
            {
                IEnumerable<TContadorBicLog> listado = MapeoEntidad(listadoEntidad);
                base.Insert(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<TContadorBicLog> Update(IEnumerable<ContadorBicLog> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                IEnumerable<TContadorBicLog> listado = MapeoEntidad(listadoEntidad);
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
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 29/08/2023
        /// Version: 1.0        
        /// <summary> 
        /// Obtiene las Fechas de las Actividades que no hubo contacto con el cliente
        /// </summary>
        /// <returns></returns>
        public ContadorBicLog? ObtenerUltimoRegistroPorIdOportunidad(int idOportunidad)
        {
            try
            {
                var fechaActual = DateTime.Now;
                var fechaInicio = new DateTime(fechaActual.Year, fechaActual.Month, fechaActual.Day, 0, 0, 0);
                var fechaFin = new DateTime(fechaActual.Year, fechaActual.Month, fechaActual.Day, 23, 59, 59);

                var query = @"
                    SELECT Id,
		                IdOportunidad,
		                SinContactoManhana,
		                SinContactoTarde,
		                IdFaseOportunidad,
		                FechaCalculo,
		                Estado,
		                UsuarioCreacion,
		                UsuarioModificacion,
		                FechaCreacion,
		                FechaModificacion,
		                RowVersion,
		                IdMigracion
	                FROM com.T_ContadorBicLog 
                    WHERE IdOportunidad=@idOportunidad
                        AND Estado=1 
                        AND FechaCalculo BETWEEN @fechaInicio AND @fechaFin
                    ORDER BY FechaCalculo DESC
                    ";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idOportunidad, fechaInicio, fechaFin });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<ContadorBicLog>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#CBLR-OPIO-001@Error en ObtenerUltimoRegistroPorIdOportunidad(), {ex.Message}");
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 31/08/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el reporte de contador bic
        /// </summary>
        /// <param name="filtros"> Filtros de búsqueda </param>
        /// <returns> Lista ContadorBicLogReporteDTO </returns>
        public List<ContadorBicLogReporteDTO> ObtenerReporteContadorBicLog(ReporteCambioFaseFiltrosDTO filtros)
        {
            try
            {
                string? asesores = null;
                string? centroCostos = null;
                DateTime fechaInicio = new DateTime(filtros.FechaInicio.Year, filtros.FechaInicio.Month, filtros.FechaInicio.Day, 0, 0, 0);
                DateTime fechaFin = new DateTime(filtros.FechaFin.Year, filtros.FechaFin.Month, filtros.FechaFin.Day, 23, 59, 59);
                if (filtros.Asesores.Count() > 0)
                {
                    asesores = string.Join(",", filtros.Asesores);
                }
                if (filtros.CentroCostos.Count() > 0)
                {
                    centroCostos = string.Join(",", filtros.CentroCostos);
                }
                var resultado = _dapperRepository.QuerySPDapper("com.SP_ReporteContadorBicLog", new
                {
                    Asesores = asesores,
                    CentroCostos = centroCostos,
                    FechaInicio = fechaInicio,
                    FechaFin = fechaFin
                });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<List<ContadorBicLogReporteDTO>>(resultado)!;
                }
                return new List<ContadorBicLogReporteDTO>();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
