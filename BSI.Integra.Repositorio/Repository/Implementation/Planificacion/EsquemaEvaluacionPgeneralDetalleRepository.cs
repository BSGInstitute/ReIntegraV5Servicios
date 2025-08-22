using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;
namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: EsquemaEvaluacionPgeneralDetalleRepository
    /// Autor: Flavio R. Mamani Fabian
    /// Fecha: 31/07/2023
    /// Version: 1.0
    /// <summary>
    /// Gestión general de T_EsquemaEvaluacionPgeneralDetalle
    /// </summary>
    public class EsquemaEvaluacionPgeneralDetalleRepository : GenericRepository<TEsquemaEvaluacionPgeneralDetalle>, IEsquemaEvaluacionPgeneralDetalleRepository
    {
        private Mapper _mapper;

        public EsquemaEvaluacionPgeneralDetalleRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TEsquemaEvaluacionPgeneralDetalle, EsquemaEvaluacionPgeneralDetalle>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TEsquemaEvaluacionPgeneralDetalle MapeoEntidad(EsquemaEvaluacionPgeneralDetalle entidad)
        {
            try
            {
                TEsquemaEvaluacionPgeneralDetalle modelo = _mapper.Map<TEsquemaEvaluacionPgeneralDetalle>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TEsquemaEvaluacionPgeneralDetalle Add(EsquemaEvaluacionPgeneralDetalle entidad)
        {
            try
            {
                var perfilAtrabajoCoeficiente = MapeoEntidad(entidad);
                base.Insert(perfilAtrabajoCoeficiente);
                return perfilAtrabajoCoeficiente;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TEsquemaEvaluacionPgeneralDetalle Update(EsquemaEvaluacionPgeneralDetalle entidad)
        {
            try
            {
                var perfilAtrabajoCoeficiente = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                perfilAtrabajoCoeficiente.RowVersion = entidadExistente.RowVersion;

                base.Update(perfilAtrabajoCoeficiente);
                return perfilAtrabajoCoeficiente;
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
        public IEnumerable<TEsquemaEvaluacionPgeneralDetalle> Add(IEnumerable<EsquemaEvaluacionPgeneralDetalle> listadoEntidad)
        {
            try
            {
                List<TEsquemaEvaluacionPgeneralDetalle> listado = new List<TEsquemaEvaluacionPgeneralDetalle>();
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
        public IEnumerable<TEsquemaEvaluacionPgeneralDetalle> Update(IEnumerable<EsquemaEvaluacionPgeneralDetalle> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TEsquemaEvaluacionPgeneralDetalle> listado = new List<TEsquemaEvaluacionPgeneralDetalle>();
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

        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 31/07/2023
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns> EsquemaEvaluacionPgeneralDetalle </returns>
        public EsquemaEvaluacionPgeneralDetalle? ObtenerPorId(int id)
        {
            try
            {
                var query = $@"
                    SELECT 
                        Id,
		                IdEsquemaEvaluacionPGeneral AS IdEsquemaEvaluacionPgeneral,
		                IdCriterioEvaluacion,
		                Nombre,
		                UrlArchivoInstrucciones,
		                Estado,
		                UsuarioCreacion,
		                UsuarioModificacion,
		                FechaCreacion,
		                FechaModificacion,
		                RowVersion,
		                IdMigracion,
		                IdProveedor
                    FROM 
                        pla.T_EsquemaEvaluacionPgeneralDetalle
                    WHERE
                        Estado = 1 AND Id = @id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<EsquemaEvaluacionPgeneralDetalle>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#EEPGDR-OPI-001@Error en ObtenerPorId() {ex.Message}", ex);
            }
        }
    }
}
