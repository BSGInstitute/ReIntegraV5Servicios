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
    /// Repositorio: CriterioEvaluacionModalidadCursoRepository
    /// Autor: Gilmer Qm
    /// Fecha: 01/06/2023
    /// <summary>
    /// Gestión general de T_CriterioEvaluacionModalidadCurso
    /// </summary>
    public class CriterioEvaluacionModalidadCursoRepository : GenericRepository<TCriterioEvaluacionModalidadCurso>, ICriterioEvaluacionModalidadCursoRepository
    {
        private Mapper _mapper;
        public CriterioEvaluacionModalidadCursoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCriterioEvaluacionModalidadCurso, CriterioEvaluacionModalidadCurso>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TCriterioEvaluacionModalidadCurso MapeoEntidad(CriterioEvaluacionModalidadCurso entidad)
        {
            try
            {
                TCriterioEvaluacionModalidadCurso criterioEvaluacion = _mapper.Map<TCriterioEvaluacionModalidadCurso>(entidad);
                return criterioEvaluacion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TCriterioEvaluacionModalidadCurso Add(CriterioEvaluacionModalidadCurso entidad)
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

        public TCriterioEvaluacionModalidadCurso Update(CriterioEvaluacionModalidadCurso entidad)
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


        public IEnumerable<TCriterioEvaluacionModalidadCurso> Add(IEnumerable<CriterioEvaluacionModalidadCurso> listadoEntidad)
        {
            try
            {
                List<TCriterioEvaluacionModalidadCurso> listado = new List<TCriterioEvaluacionModalidadCurso>();
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

        public IEnumerable<TCriterioEvaluacionModalidadCurso> Update(IEnumerable<CriterioEvaluacionModalidadCurso> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TCriterioEvaluacionModalidadCurso> listado = new List<TCriterioEvaluacionModalidadCurso>();
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
        public List<CriterioEvaluacionModalidadCursoDTO> ObtenerPorIdCriterioEvaluacion(int idCriterioEvaluacion)
        {
            try
            {
                var respuesta = new List<CriterioEvaluacionModalidadCursoDTO>();
                var query = @"SELECT Id,
                                   IdCriterioEvaluacion,
                                   IdModalidadCurso
                            FROM pla.T_CriterioEvaluacionModalidadCurso
                            WHERE Estado = 1
                                  AND IdCriterioEvaluacion = @IdCriterioEvaluacion;";
                var resultado = _dapperRepository.QueryDapper(query, new { IdCriterioEvaluacion = idCriterioEvaluacion });
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    respuesta = JsonConvert.DeserializeObject<List<CriterioEvaluacionModalidadCursoDTO>>(resultado);
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
        /// <param name="idModalidadCurso"> PK de T_ModalidadCurso </param> 
        /// <returns> CriterioEvaluacionModalidadCurso </returns>  
        public CriterioEvaluacionModalidadCurso ObtenerPorIdModalidadCursoYIdCriterioEvaluacion(int idCriterioEvaluacion, int idModalidadCurso)
        {
            try
            {
                var query = @"SELECT Id,
                                   IdCriterioEvaluacion,
                                   IdModalidadCurso,
                                   Estado,
                                   UsuarioCreacion,
                                   UsuarioModificacion,
                                   FechaCreacion,
                                   FechaModificacion,
                                   RowVersion,
                                   IdMigracion
                            FROM pla.T_CriterioEvaluacionModalidadCurso
                            WHERE Estado = 1
                                  AND IdCriterioEvaluacion = @IdCriterioEvaluacion
                                  AND IdModalidadCurso = @IdModalidadCurso;";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdCriterioEvaluacion = idCriterioEvaluacion, IdModalidadCurso = idModalidadCurso });
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    return JsonConvert.DeserializeObject<CriterioEvaluacionModalidadCurso>(resultado);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<CriterioEvaluacionModalidadDTO> ListarCriteriosEvaluacionModalidad()
        {
            try
            {
                List<CriterioEvaluacionModalidadDTO> criteriosFiltro = new List<CriterioEvaluacionModalidadDTO>();
               
                var _queryfiltrocriterio = "Select Id,Nombre,IdModalidadCurso FROM pla.V_ObtenerCriteriosEvaluacionPorModalidad";
                var SubfiltroCriterio = _dapperRepository.QueryDapper(_queryfiltrocriterio, null);
                if (!string.IsNullOrEmpty(SubfiltroCriterio) && !SubfiltroCriterio.Contains("[]"))
                {
                    criteriosFiltro = JsonConvert.DeserializeObject<List<CriterioEvaluacionModalidadDTO>>(SubfiltroCriterio);
                }
                return criteriosFiltro;
            }
            catch (Exception E)
            {
                throw new Exception(E.Message);
            }
        }


    }
}
