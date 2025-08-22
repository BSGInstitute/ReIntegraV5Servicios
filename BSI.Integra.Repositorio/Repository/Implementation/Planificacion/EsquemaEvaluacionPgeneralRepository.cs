using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: EsquemaEvaluacionPgeneralRepository
    /// Autor: Jonathan Caipo
    /// Fecha: 08/05/2023
    /// Version: 1.0
    /// <summary>
    /// Gestión general de T_EsquemaEvaluacionPGeneral
    /// </summary>
    public class EsquemaEvaluacionPgeneralRepository : GenericRepository<TEsquemaEvaluacionPgeneral>, IEsquemaEvaluacionPgeneralRepository
    {
        private Mapper _mapper;

        public EsquemaEvaluacionPgeneralRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TEsquemaEvaluacionPgeneral, EsquemaEvaluacionPgeneral>(MemberList.None).ReverseMap();
                cfg.CreateMap<TEsquemaEvaluacionPgeneralDetalle, EsquemaEvaluacionPgeneralDetalle>(MemberList.None).ReverseMap();
                cfg.CreateMap<TEsquemaEvaluacionPgeneralModalidad, EsquemaEvaluacionPgeneralModalidad>(MemberList.None).ReverseMap();
                cfg.CreateMap<TEsquemaEvaluacionPgeneralProveedor, EsquemaEvaluacionPgeneralProveedor>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TEsquemaEvaluacionPgeneral MapeoEntidad(EsquemaEvaluacionPgeneral entidad)
        {
            try
            {
                TEsquemaEvaluacionPgeneral modelo = _mapper.Map<TEsquemaEvaluacionPgeneral>(entidad);
                if (entidad.EsquemaEvaluacionPgeneralDetalles != null && entidad.EsquemaEvaluacionPgeneralDetalles.Count() > 0)
                {
                    modelo.TEsquemaEvaluacionPgeneralDetalles = _mapper.Map<ICollection<TEsquemaEvaluacionPgeneralDetalle>>(entidad.EsquemaEvaluacionPgeneralDetalles);
                }
                if (entidad.EsquemaEvaluacionPgeneralModalidads != null && entidad.EsquemaEvaluacionPgeneralModalidads.Count() > 0)
                {
                    modelo.TEsquemaEvaluacionPgeneralModalidads = _mapper.Map<ICollection<TEsquemaEvaluacionPgeneralModalidad>>(entidad.EsquemaEvaluacionPgeneralModalidads);
                }
                if (entidad.EsquemaEvaluacionPgeneralProveedors != null && entidad.EsquemaEvaluacionPgeneralProveedors.Count() > 0)
                {
                    modelo.TEsquemaEvaluacionPgeneralProveedors = _mapper.Map<ICollection<TEsquemaEvaluacionPgeneralProveedor>>(entidad.EsquemaEvaluacionPgeneralProveedors);
                }
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TEsquemaEvaluacionPgeneral Add(EsquemaEvaluacionPgeneral entidad)
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
        public TEsquemaEvaluacionPgeneral Update(EsquemaEvaluacionPgeneral entidad)
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
        public IEnumerable<TEsquemaEvaluacionPgeneral> Add(IEnumerable<EsquemaEvaluacionPgeneral> listadoEntidad)
        {
            try
            {
                List<TEsquemaEvaluacionPgeneral> listado = new List<TEsquemaEvaluacionPgeneral>();
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
        public IEnumerable<TEsquemaEvaluacionPgeneral> Update(IEnumerable<EsquemaEvaluacionPgeneral> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TEsquemaEvaluacionPgeneral> listado = new List<TEsquemaEvaluacionPgeneral>();
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

        /// Autor: Jonathan Caipo
        /// Fecha: 07/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene una lista de entidad mediante el idPGeneral
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns> Lista Entidad - List<EsquemaEvaluacionPGeneral>() </returns>
        public IEnumerable<EsquemaEvaluacionPgeneral> ObtenerPorIdPGeneral(int idPGeneral)
        {
            try
            {
                var query = $@"
                    SELECT 
                        Id,
                        IdEsquemaEvaluacion,
                        IdPGeneral AS IdPgeneral,
                        Estado,
                        UsuarioCreacion,
                        UsuarioModificacion,
                        FechaCreacion,
                        FechaModificacion,
                        RowVersion,
                        IdMigracion,
                        FechaInicio,
                        FechaFin,
                        Esquema_Predeterminado AS EsquemaPredeterminado
                    FROM 
                        pla.T_EsquemaEvaluacionPGeneral
                    WHERE
                        Estado = 1 AND IdPGeneral = @idPGeneral";
                var resultado = _dapperRepository.QueryDapper(query, new { idPGeneral });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<EsquemaEvaluacionPgeneral>>(resultado);
                }
                return new List<EsquemaEvaluacionPgeneral>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#EEPGR-OPIPG-002@Error en ObtenerPorIdPGeneral() {ex.Message}", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 01/08/2023
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public EsquemaEvaluacionPgeneral? ObtenerPorId(int id)
        {
            try
            {
                var query = $@"
                    SELECT 
                        Id,
                        IdEsquemaEvaluacion,
                        IdPGeneral AS IdPgeneral,
                        Estado,
                        UsuarioCreacion,
                        UsuarioModificacion,
                        FechaCreacion,
                        FechaModificacion,
                        RowVersion,
                        IdMigracion,
                        FechaInicio,
                        FechaFin,
                        Esquema_Predeterminado AS EsquemaPredeterminado
                    FROM 
                        pla.T_EsquemaEvaluacionPGeneral
                    WHERE
                        Estado = 1 AND Id = @id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<EsquemaEvaluacionPgeneral>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#EEPGR-OPI-001@Error en ObtenerPorId() {ex.Message}", ex);
            }
        }
    }
}
