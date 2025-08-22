using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: CriterioEvaluacionTipoProgramaRepository
    /// Autor: Gilmer Qm
    /// Fecha: 01/06/2023
    /// <summary>
    /// Gestión general de T_CriterioEvaluacionTipoPrograma
    /// </summary>
    public class CriterioEvaluacionTipoProgramaRepository : GenericRepository<TCriterioEvaluacionTipoPrograma>, ICriterioEvaluacionTipoProgramaRepository
    {
        private Mapper _mapper;
        public CriterioEvaluacionTipoProgramaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCriterioEvaluacionTipoPrograma, CriterioEvaluacionTipoPrograma>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TCriterioEvaluacionTipoPrograma MapeoEntidad(CriterioEvaluacionTipoPrograma entidad)
        {
            try
            {
                TCriterioEvaluacionTipoPrograma criterioEvaluacion = _mapper.Map<TCriterioEvaluacionTipoPrograma>(entidad);
                return criterioEvaluacion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TCriterioEvaluacionTipoPrograma Add(CriterioEvaluacionTipoPrograma entidad)
        {
            try
            {
                var MaterialAccion = MapeoEntidad(entidad);
                base.Insert(MaterialAccion);
                return MaterialAccion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TCriterioEvaluacionTipoPrograma Update(CriterioEvaluacionTipoPrograma entidad)
        {
            try
            {
                var MaterialAccion = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                MaterialAccion.RowVersion = entidadExistente.RowVersion;

                base.Update(MaterialAccion);
                return MaterialAccion;
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


        public IEnumerable<TCriterioEvaluacionTipoPrograma> Add(IEnumerable<CriterioEvaluacionTipoPrograma> listadoEntidad)
        {
            try
            {
                List<TCriterioEvaluacionTipoPrograma> listado = new List<TCriterioEvaluacionTipoPrograma>();
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

        public IEnumerable<TCriterioEvaluacionTipoPrograma> Update(IEnumerable<CriterioEvaluacionTipoPrograma> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TCriterioEvaluacionTipoPrograma> listado = new List<TCriterioEvaluacionTipoPrograma>();
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
        /// Autor: Gilmer Quispe.
        /// Fecha: 01/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros por el (FK) IdCriterioEvaluacion
        /// </summary>
        /// <param name="idCriterioEvaluacion"> PK de T_CriterioEvaluacion </param> 
        /// <returns> CriterioEvaluacionDTO </returns>  
        public List<CriterioEvaluacionTipoProgramaDTO> ObtenerPorIdCriterioEvaluacion(int idCriterioEvaluacion)
        {
            try
            {
                var respuesta = new List<CriterioEvaluacionTipoProgramaDTO>();
                var query = @"SELECT Id,
                                   IdCriterioEvaluacion,
                                   IdTipoPrograma
                            FROM pla.T_CriterioEvaluacionTipoPrograma
                            WHERE Estado = 1
                                  AND IdCriterioEvaluacion = @IdCriterioEvaluacion;";
                var resultado = _dapperRepository.QueryDapper(query, new { IdCriterioEvaluacion = idCriterioEvaluacion });
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    respuesta = JsonConvert.DeserializeObject<List<CriterioEvaluacionTipoProgramaDTO>>(resultado);
                }
                return respuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 01/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros por el (FK) IdCriterioEvaluacion y (FK) IdTipoPrograma
        /// </summary>
        /// <param name="idCriterioEvaluacion"> PK de T_CriterioEvaluacion </param> 
        /// <param name="idTipoPrograma"> PK de T_TipoPrograma </param> 
        /// <returns> CriterioEvaluacionTipoPrograma </returns>  
        public CriterioEvaluacionTipoPrograma ObtenerPorIdTipoProgramaYIdCriterioEvaluacion(int idTipoPrograma, int idCriterioEvaluacion)
        {
            try
            {
                var query = @"SELECT Id,
                                   IdCriterioEvaluacion,
                                   IdTipoPrograma,
                                   Estado,
                                   UsuarioCreacion,
                                   UsuarioModificacion,
                                   FechaCreacion,
                                   FechaModificacion,
                                   RowVersion,
                                   IdMigracion
                            FROM pla.T_CriterioEvaluacionTipoPrograma
                            WHERE Estado = 1
                                  AND IdCriterioEvaluacion = @IdCriterioEvaluacion
                                  AND IdTipoPrograma = @IdTipoPrograma;";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdCriterioEvaluacion = idCriterioEvaluacion, IdTipoPrograma = idTipoPrograma });
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    return JsonConvert.DeserializeObject<CriterioEvaluacionTipoPrograma>(resultado);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
