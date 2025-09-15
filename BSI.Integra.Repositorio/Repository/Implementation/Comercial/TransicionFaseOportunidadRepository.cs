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
                cfg.CreateMap<TCriterioCalificacionFaseOportunidad, CriterioCalificacionFaseOportunidad>(MemberList.None).ReverseMap();
                cfg.CreateMap<TLineamientoCalificacionFase, LineamientoCalificacionFase>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TTransicionFaseOportunidad MapeoEntidad(TransicionFaseOportunidad entidad)
        {
            try
            {
                TTransicionFaseOportunidad transicionFase = _mapper.Map<TTransicionFaseOportunidad>(entidad);
                
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
                var TransicionCalificacionFase = MapeoEntidad(entidad);
                base.Insert(TransicionCalificacionFase);
                return TransicionCalificacionFase;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<TTransicionFaseOportunidad> Add(IEnumerable<TransicionFaseOportunidad> listadoEntidad)
        {
            try
            {
                List<TTransicionFaseOportunidad> listado = new List<TTransicionFaseOportunidad>();
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

        public IEnumerable<TTransicionFaseOportunidad> Update(IEnumerable<TransicionFaseOportunidad> listadoEntidad)
        {
            try
            {
                List<TTransicionFaseOportunidad> listado = new List<TTransicionFaseOportunidad>();
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
                var query = @"SELECT 
                    tcf.Id, 
                    fo.Nombre,
                    tcf.IdFaseOportunidadOrigen AS IdFaseOportunidadOrigen,
                    tcf.IdFaseOportunidadDestino AS IdFaseOportunidadDestino,
                    CONCAT(
                        fo.Nombre, ' (', tcf.IdFaseOportunidadOrigen, ') -> ', 
                        fd.Nombre, ' (', tcf.IdFaseOportunidadDestino, ')'
                    ) AS Nombre
                FROM 
                    com.T_TransicionFaseOportunidad tcf
                    INNER JOIN pla.T_FaseOportunidad fo ON tcf.IdFaseOportunidadOrigen = fo.Id
                    INNER JOIN pla.T_FaseOportunidad fd ON tcf.IdFaseOportunidadDestino = fd.Id
                WHERE 
                    tcf.Estado = 1";
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

        public TransicionFaseOportunidad ObtenerPorId(int id)
        {
            try
            {
                string query = @"SELECT 
                    tcf.Id, 
                    fo.Nombre,
                    tcf.IdFaseOportunidadOrigen AS IdFaseOportunidadOrigen,
                    tcf.IdFaseOportunidadDestino AS IdFaseOportunidadDestino,
                    CONCAT(
                        fo.Nombre, ' (', tcf.IdFaseOportunidadOrigen, ') -> ', 
                        fd.Nombre, ' (', tcf.IdFaseOportunidadDestino, ')'
                    ) AS Nombre
                FROM 
                    com.T_TransicionFaseOportunidad tcf
                    INNER JOIN pla.T_FaseOportunidad fo ON tcf.IdFaseOportunidadOrigen = fo.Id
                    INNER JOIN pla.T_FaseOportunidad fd ON tcf.IdFaseOportunidadDestino = fd.Id
                WHERE 
                    tcf.Estado = 1 AND Id = @Id;";
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
    }
}