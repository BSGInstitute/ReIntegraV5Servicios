using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
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
    /// Repositorio: TransicionCalificacionFaseRepository
    /// Autor: [Su Nombre]
    /// Fecha: [Fecha Actual]
    /// <summary>
    /// Gestión general de T_TransicionCalificacionFase
    /// </summary>
    public class TransicionCalificacionFaseRepository : GenericRepository<TTransicionFase>, ITransicionCalificacionFaseRepository
    {
        private Mapper _mapper;
        public TransicionCalificacionFaseRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TTransicionFase, TransicionCalificacionFase>(MemberList.None).ReverseMap();
                cfg.CreateMap<TCriterioCalificacionFaseOportunidad, CriterioCalificacionFaseOportunidad>(MemberList.None).ReverseMap();
                cfg.CreateMap<TLineamientoCalificacionFase, LineamientoCalificacionFase>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TTransicionFase MapeoEntidad(TransicionCalificacionFase entidad)
        {
            try
            {
                TTransicionFase transicionFase = _mapper.Map<TTransicionFase>(entidad);
                
                // Aquí puedes agregar el mapeo de colecciones relacionadas si es necesario
                // Por ejemplo, si tienes colecciones como en el ejemplo del CriterioEvaluacion

                return transicionFase;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TTransicionFase Add(TransicionCalificacionFase entidad)
        {
            try
            {
                var TransicionCalificacionFase = MapeoEntidad(entidad);
                base.Insert(TransicionCalificacionFase);
                return TransicionCalificacionFase;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /*        public TSolicitudTipoReporte Add(SolicitudTipoReporte entidad)
        {
            try
            {
                var SolicitudTipoReporte = MapeoEntidad(entidad);
                base.Insert(SolicitudTipoReporte);
                return SolicitudTipoReporte;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }*/
        public IEnumerable<TTransicionFase> Add(IEnumerable<TransicionCalificacionFase> listadoEntidad)
        {
            try
            {
                List<TTransicionFase> listado = new List<TTransicionFase>();
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

        public TTransicionFase Update(TransicionCalificacionFase entidad)
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
        /*         public TSolicitudTipoReporte Update(SolicitudTipoReporte entidad)
        {
            try
            {
                var SolicitudTipoReporte = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                SolicitudTipoReporte.RowVersion = entidadExistente.RowVersion;

                base.Update(SolicitudTipoReporte);
                return SolicitudTipoReporte;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }*/

        public IEnumerable<TTransicionFase> Update(IEnumerable<TransicionCalificacionFase> listadoEntidad)
        {
            try
            {
                List<TTransicionFase> listado = new List<TTransicionFase>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }
                base.Update(listado);
                return listado;
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

        public List<TransicionCalificacionFaseDTO> ObtenerTransicionesCalificacionFase()
        {
            try
            {
                List<TransicionCalificacionFaseDTO> transicionesFiltro = new();
                var query = @"SELECT Id,
                        IdFaseOportunidad_Origen,
                        IdFaseOportunidad_Destino
                    FROM com.T_TransicionCalificacionFase
                    WHERE Estado = 1 order by Id desc";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    transicionesFiltro = JsonConvert.DeserializeObject<List<TransicionCalificacionFaseDTO>>(resultado)!;
                }
                return transicionesFiltro;
            }
            catch (Exception E)
            {
                throw new Exception(E.Message);
            }
        }

        public TransicionCalificacionFase ObtenerTransicionCalificacionFasePorId(int idTransicionCalificacionFase)
        {
            try
            {
                var rpta = new TransicionCalificacionFase();

                var query = @"SELECT Id,
                        IdFaseOportunidadOrigen,
                        IdFaseOportunidadDestino,
                                       Estado,
                                       UsuarioCreacion,
                                       UsuarioModificacion,
                                       FechaCreacion,
                                       FechaModificacion,
                                       RowVersion,
                                       IdMigracion 
                    FROM com.T_TransicionFaseOportunidad
                    WHERE Estado = 1 AND Id = @Id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = idTransicionCalificacionFase });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<TransicionCalificacionFase>(resultado)!;
                }
                return rpta;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public TransicionCalificacionFase ObtenerPorId(int id)
        {
            try
            {
                string query = @"SELECT Id,
                                   IdFaseOportunidadOrigen,
                                   IdFaseOportunidadDestino,
                                   Estado,
                                   UsuarioCreacion,
                                   UsuarioModificacion,
                                   FechaCreacion,
                                   FechaModificacion,
                                   RowVersion,
                                   IdMigracion
                            FROM com.T_TransicionCalificacionFase
                            WHERE Estado = 1
                                  AND Id = @Id;";
                string resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<TransicionCalificacionFase>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#TCF-OPI-001@Error en ObtenerPorId: {ex.Message}", ex);
            }
        }

        public IEnumerable<TransicionCalificacionFaseDTO> ObtenerCombo()
        {
            try
            {
                var query = @"
                SELECT 
                    tcf.Id, 
                    fo.Nombre,
                    tcf.IdFaseOportunidad_Origen AS IdFaseOportunidadOrigen,
                    tcf.IdFaseOportunidad_Destino AS IdFaseOportunidadDestino,
                    CONCAT(
                        fo.Nombre, ' (', tcf.IdFaseOportunidad_Origen, ') -> ', 
                        fd.Nombre, ' (', tcf.IdFaseOportunidad_Destino, ')'
                    ) AS Nombre
                FROM 
                    com.T_TransicionCalificacionFase tcf
                    INNER JOIN pla.T_FaseOportunidad fo ON tcf.IdFaseOportunidad_Origen = fo.Id
                    INNER JOIN pla.T_FaseOportunidad fd ON tcf.IdFaseOportunidad_Destino = fd.Id
                WHERE 
                    tcf.Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<TransicionCalificacionFaseDTO>>(resultado)!;
                }
                return new List<TransicionCalificacionFaseDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#TCF-OC-001@Error en ObtenerCombo: {ex.Message}", ex);
            }
        }
    }
}