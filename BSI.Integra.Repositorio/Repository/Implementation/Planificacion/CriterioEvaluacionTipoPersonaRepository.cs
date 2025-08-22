using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: CriterioEvaluacionTipoPersonaRepository
    /// Autor: Gilmer Qm
    /// Fecha: 01/06/2023
    /// <summary>
    /// Gestión general de T_CriterioEvaluacionTipoPersona
    /// </summary>
    public class CriterioEvaluacionTipoPersonaRepository : GenericRepository<TCriterioEvaluacionTipoPersona>, ICriterioEvaluacionTipoPersonaRepository
    {
        private Mapper _mapper;
        public CriterioEvaluacionTipoPersonaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCriterioEvaluacionTipoPersona, CriterioEvaluacionTipoPersona>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TCriterioEvaluacionTipoPersona MapeoEntidad(CriterioEvaluacionTipoPersona entidad)
        {
            try
            {
                TCriterioEvaluacionTipoPersona criterioEvaluacion = _mapper.Map<TCriterioEvaluacionTipoPersona>(entidad);
                return criterioEvaluacion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TCriterioEvaluacionTipoPersona Add(CriterioEvaluacionTipoPersona entidad)
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

        public TCriterioEvaluacionTipoPersona Update(CriterioEvaluacionTipoPersona entidad)
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


        public IEnumerable<TCriterioEvaluacionTipoPersona> Add(IEnumerable<CriterioEvaluacionTipoPersona> listadoEntidad)
        {
            try
            {
                List<TCriterioEvaluacionTipoPersona> listado = new List<TCriterioEvaluacionTipoPersona>();
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

        public IEnumerable<TCriterioEvaluacionTipoPersona> Update(IEnumerable<CriterioEvaluacionTipoPersona> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TCriterioEvaluacionTipoPersona> listado = new List<TCriterioEvaluacionTipoPersona>();
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
        /// <returns> List<CriterioEvaluacionTipoPersonaDTO> </returns>  
        public List<CriterioEvaluacionTipoPersonaDTO> ObtenerPorIdCriterioEvaluacion(int idCriterioEvaluacion)
        {
            try
            {
                var respuesta = new List<CriterioEvaluacionTipoPersonaDTO>();
                var query = @"SELECT Id,
                                   IdCriterioEvaluacion,
                                   IdTipoPersona
                            FROM pla.T_CriterioEvaluacionTipoPersona
                            WHERE Estado = 1
                                  AND IdCriterioEvaluacion = @IdCriterioEvaluacion;";
                var resultado = _dapperRepository.QueryDapper(query, new { IdCriterioEvaluacion = idCriterioEvaluacion });
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    respuesta = JsonConvert.DeserializeObject<List<CriterioEvaluacionTipoPersonaDTO>>(resultado);
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
        /// <param name="idTipoPersona"> PK de T_TipoPersona </param> 
        /// <returns> CriterioEvaluacionTipoPersona </returns>  
        public CriterioEvaluacionTipoPersona ObtenerPorIdTipoPersonaYIdCriterioEvaluacion(int idTipoPersona, int idCriterioEvaluacion)
        {
            try
            {
                var query = @"SELECT Id,
                                   IdCriterioEvaluacion,
                                   IdTipoPersona,
                                   Estado,
                                   UsuarioCreacion,
                                   UsuarioModificacion,
                                   FechaCreacion,
                                   FechaModificacion,
                                   RowVersion,
                                   IdMigracion
                            FROM pla.T_CriterioEvaluacionTipoPersona
                            WHERE Estado = 1
                                  AND IdCriterioEvaluacion = @IdCriterioEvaluacion
                                  AND IdTipoPersona = @IdTipoPersona;";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdCriterioEvaluacion = idCriterioEvaluacion, IdTipoPersona = idTipoPersona });
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    return JsonConvert.DeserializeObject<CriterioEvaluacionTipoPersona>(resultado);
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
