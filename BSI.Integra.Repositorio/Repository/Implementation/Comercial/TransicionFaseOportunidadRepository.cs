using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Comercial;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: TransicionFaseOportunidadRepository
    /// Autor: Jose Vega
    /// Fecha: 15/09/2025
    /// <summary>
    /// Gestión general de T_TransicionFaseOportunidad
    /// </summary>
    public class TransicionFaseOportunidadRepository : GenericRepository<TTransicionFaseOportunidad>, ITransicionFaseOportunidadRepository
    {
        private Mapper _mapper;
        public TransicionFaseOportunidadRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TTransicionFaseOportunidad, TransicionFaseOportunidad>(MemberList.None).ReverseMap();
                cfg.CreateMap<TCriterioCalificacionFaseOportunidad, TransicionFaseCriterioOportunidad>(MemberList.None).ReverseMap();
                cfg.CreateMap<TransicionFaseCriterioOportunidad, TTransicionFaseCriterioOportunidad>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TTransicionFaseOportunidad MapeoEntidad(TransicionFaseOportunidad entidad)
        {
            try
            {
                TTransicionFaseOportunidad transicionFase = _mapper.Map<TTransicionFaseOportunidad>(entidad);
                //mapea los hijos
                if (entidad.TransicionFaseCriterioOportunidad != null && entidad.TransicionFaseCriterioOportunidad.Count > 0)
                {
                    var listadoHijoNivel1 = _mapper.Map<List<TTransicionFaseCriterioOportunidad>>(entidad.TransicionFaseCriterioOportunidad);
                    foreach (var hijoNivel1 in listadoHijoNivel1)
                    {
                        transicionFase.TTransicionFaseCriterioOportunidads.Add(hijoNivel1);
                    }
                }

                return transicionFase;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TTransicionFaseOportunidad Add(TransicionFaseOportunidad entidad)
        {
            try
            {

                var transicionFaseOportunidad = MapeoEntidad(entidad);
                base.Insert(transicionFaseOportunidad);
                return transicionFaseOportunidad;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        
        public TTransicionFaseOportunidad Update(TransicionFaseOportunidad entidad)
        {
            try
            {
                var TransicionCalificacionFase = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id);
                base.Update(TransicionCalificacionFase);
                return TransicionCalificacionFase;
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

        public bool DeleteCriterios(int id, string usuario)
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

        #endregion

        /// Autor: Jose Vega
        /// Fecha: 15/09/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los campos de T_TransicionFaseOportunidad por el Id.
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <returns> TransicionFaseOportunidad </returns>
        public TransicionFaseOportunidadPlanoDto ObtenerPorId(int id)
        {
            try
            {
                string query = @"SELECT IdTransicionFaseOportunidad AS Id,
                                    IdTransicionFaseOportunidad AS IdTransicionFaseOportunidad,
                                    IdFaseOportunidadOrigen,
                                    CodigoFaseOrigen,
                                    NombreFaseOrigen,
                                    IdFaseOportunidadDestino,
                                    CodigoFaseDestino,
                                    NombreFaseDestino,
                                    Estado,
                                    UsuarioCreacion,
                                    UsuarioModificacion,
                                    FechaCreacion,
                                    FechaModificacion,
                                    RowVersion,
                                    MigracionId,
                                    IdTransicionFaseCriterioOportunidad,
                                    IdTransicionFaseOportunidadReferencia,
                                    IdCriterioCalificacionFaseOportunidad,
                                    NombreCriterio,
                                    DescripcionCriterio
                                 FROM com.V_TransicionFaseOportunidad_TransicionDeFase 
                                 WHERE IdTransicionFaseOportunidad = @Id
                                 ORDER BY FechaCreacion DESC";
                string resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<TransicionFaseOportunidadPlanoDto>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#TCF-OPI-001@Error en ObtenerPorId: {ex.Message}", ex);
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 15/09/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> List<TransicionFaseOportunidadDTO> </returns>
        public List<TransicionFaseOportunidadPlanoDto> Obtener()
        {
            try
            {
                List<TransicionFaseOportunidadPlanoDto> transicionesFiltro = new();
                var query = @"SELECT
                                IdTransicionFaseOportunidad AS Id,
                                IdTransicionFaseOportunidad AS IdTransicionFaseOportunidad,
                                IdFaseOportunidadOrigen,
                                CodigoFaseOrigen,
                                NombreFaseOrigen,
                                IdFaseOportunidadDestino,
                                CodigoFaseDestino,
                                NombreFaseDestino,
                                Estado,
                                UsuarioCreacion,
                                UsuarioModificacion,
                                FechaCreacion,
                                FechaModificacion,
                                RowVersion,
                                MigracionId,
                                IdTransicionFaseCriterioOportunidad,
                                IdTransicionFaseOportunidadReferencia,
                                IdCriterioCalificacionFaseOportunidad,
                                NombreCriterio,
                                DescripcionCriterio
                            FROM com.V_TransicionFaseOportunidad_TransicionDeFase
                            ORDER BY FechaCreacion DESC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    transicionesFiltro = JsonConvert.DeserializeObject<List<TransicionFaseOportunidadPlanoDto>>(resultado)!;
                }
                return transicionesFiltro;
            }
            catch (Exception E)
            {
                throw new Exception(E.Message);
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 15/09/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los campos de T_TransicionFaseOportunidad por el Id.
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <returns> TransicionFaseOportunidad </returns>
        public TransicionFaseOportunidad ObtenerPorIdUD(int id)
        {
            try
            {
                string query = @"SELECT
                                    IdTransicionFaseOportunidad AS Id,
                                    IdTransicionFaseOportunidad AS IdTransicionFaseOportunidad,
                                    IdFaseOportunidadOrigen,
                                    CodigoFaseOrigen,
                                    NombreFaseOrigen,
                                    IdFaseOportunidadDestino,
                                    CodigoFaseDestino,
                                    NombreFaseDestino,
                                    Estado,
                                    UsuarioCreacion,
                                    UsuarioModificacion,
                                    FechaCreacion,
                                    FechaModificacion,
                                    RowVersion,
                                    MigracionId,
                                    IdTransicionFaseCriterioOportunidad,
                                    IdTransicionFaseOportunidadReferencia,
                                    IdCriterioCalificacionFaseOportunidad,
                                    NombreCriterio,
                                    DescripcionCriterio
                                 FROM com.V_TransicionFaseOportunidad_TransicionDeFase 
                                 WHERE IdTransicionFaseOportunidad = @Id
                                 ORDER BY FechaCreacion DESC";
                string resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<TransicionFaseOportunidad>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#TCF-OPI-001@Error en ObtenerPorId: {ex.Message}", ex);
            }
        }
        /// Autor: Jose Vega
        /// Fecha: 15/09/2025
        /// Version: 1.0
        /// <summary>
        ///  Obtiene todos los campos de T_TransicionFaseCriterioOportunidad por el Id.
        /// </summary> 
        /// <returns> List<TransicionFaseCriterioOportunidad> </returns>
        public List<TransicionFaseCriterioOportunidad> ObtenerPorIdTransicion(int idTransicionFaseOportunidad)
        {
            try
            {
                var sql = @"
                    SELECT 
                        Id,
                        IdTransicionFaseOportunidad,
                        IdCriterioCalificacionFaseOportunidad,
                        Estado,
                        UsuarioCreacion,
                        UsuarioModificacion,
                        FechaCreacion,
                        FechaModificacion
                    FROM com.T_TransicionFaseCriterioOportunidad
                    WHERE Estado = 1 AND IdTransicionFaseOportunidad = @IdTransicionFaseOportunidad";

                var resultado = _dapperRepository.QueryDapper(sql, new { IdTransicionFaseOportunidad = idTransicionFaseOportunidad });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<List<TransicionFaseCriterioOportunidad>>(resultado)!;
                }
                return new List<TransicionFaseCriterioOportunidad>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener criterios por transición {idTransicionFaseOportunidad}: {ex.Message}", ex);
            }
        }
    }
}