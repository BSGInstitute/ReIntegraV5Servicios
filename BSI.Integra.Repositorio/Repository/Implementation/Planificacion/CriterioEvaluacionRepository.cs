using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
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
    /// Repositorio: CriterioEvaluacionRepository
    /// Autor: Gilmer Qm
    /// Fecha: 31/05/2023
    /// <summary>
    /// Gestión general de T_CriterioEvaluacion
    /// </summary>
    public class CriterioEvaluacionRepository : GenericRepository<TCriterioEvaluacion>, ICriterioEvaluacionRepository
    {
        private Mapper _mapper;
        public CriterioEvaluacionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCriterioEvaluacion, CriterioEvaluacion>(MemberList.None).ReverseMap();
                cfg.CreateMap<TCriterioEvaluacionTipoPersona, CriterioEvaluacionTipoPersona>(MemberList.None).ReverseMap();
                cfg.CreateMap<TCriterioEvaluacionTipoPrograma, CriterioEvaluacionTipoPrograma>(MemberList.None).ReverseMap();
                cfg.CreateMap<TCriterioEvaluacionModalidadCurso, CriterioEvaluacionModalidadCurso>(MemberList.None).ReverseMap();
                cfg.CreateMap<TParametroEvaluacion, ParametroEvaluacion>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TCriterioEvaluacion MapeoEntidad(CriterioEvaluacion entidad)
        {
            try
            {
                TCriterioEvaluacion criterioEvaluacion = _mapper.Map<TCriterioEvaluacion>(entidad);
                if (entidad.ParametroEvaluacion != null && entidad.ParametroEvaluacion.Count >= 1)
                {
                    criterioEvaluacion.TParametroEvaluacions = _mapper.Map<List<TParametroEvaluacion>>(entidad.ParametroEvaluacion);
                }
                if (entidad.CriterioEvaluacionTipoPersona != null && entidad.CriterioEvaluacionTipoPersona.Count >= 1)
                {
                    criterioEvaluacion.TCriterioEvaluacionTipoPersonas = _mapper.Map<List<TCriterioEvaluacionTipoPersona>>(entidad.CriterioEvaluacionTipoPersona);
                }
                if (entidad.CriterioEvaluacionTipoPrograma != null && entidad.CriterioEvaluacionTipoPrograma.Count >= 1)
                {
                    criterioEvaluacion.TCriterioEvaluacionTipoProgramas = _mapper.Map<List<TCriterioEvaluacionTipoPrograma>>(entidad.CriterioEvaluacionTipoPrograma);
                }
                if (entidad.CriterioEvaluacionModalidadCurso != null && entidad.CriterioEvaluacionModalidadCurso.Count >= 1)
                {
                    criterioEvaluacion.TCriterioEvaluacionModalidadCursos = _mapper.Map<List<TCriterioEvaluacionModalidadCurso>>(entidad.CriterioEvaluacionModalidadCurso);
                }
                return criterioEvaluacion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TCriterioEvaluacion Add(CriterioEvaluacion entidad)
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

        public TCriterioEvaluacion Update(CriterioEvaluacion entidad)
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


        public IEnumerable<TCriterioEvaluacion> Add(IEnumerable<CriterioEvaluacion> listadoEntidad)
        {
            try
            {
                List<TCriterioEvaluacion> listado = new List<TCriterioEvaluacion>();
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

        public IEnumerable<TCriterioEvaluacion> Update(IEnumerable<CriterioEvaluacion> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TCriterioEvaluacion> listado = new List<TCriterioEvaluacion>();
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

        /// Autor: Gilmer Qm
        /// Fecha: 01/06/2023
        /// Version: 1.0
        /// <summary>
        /// Elimina de manera logica los registros de tablas detalle de T_CriterioEvaluacion
        /// </summary>
        /// <param name="id"> PK de T_CriterioEvaluacion </param> 
        /// <returns> List<CriterioEvaluacionDTO> </returns>
        public bool EliminarDetalles(int id)
        {
            try
            {
                string spReporte = "[pla].[SP_EliminarCriterioEvaluacionDetalles]";
                string resultadoReporte = _dapperRepository.QuerySPDapper(spReporte, new { IdCriterioEvaluacion = id });
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 31/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros con los detalles de T_CriterioEvaluacion
        /// </summary>
        /// <returns> List<CriterioEvaluacionDTO> </returns>
        public List<CriterioEvaluacionDTO> ObtenerCriteriosEvaluacion()
        {
            try
            {
                List<CriterioEvaluacionDTO> criteriosFiltro = new();
                var query = @"SELECT Id,
                        Nombre,
                        IdCriterioEvaluacionCategoria,
                        IdFormaCalificacionEvaluacion,
                        IdFormaCalculoEvaluacion,
                        IdFormaCalculoEvaluacion_Parametro as idFormaCalculoEvaluacionParametro
                    FROM pla.T_CriterioEvaluacion
                    WHERE Estado = 1 order by Id desc";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    criteriosFiltro = JsonConvert.DeserializeObject<List<CriterioEvaluacionDTO>>(resultado)!;
                }
                return criteriosFiltro;
            }
            catch (Exception E)
            {
                throw new Exception(E.Message);
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 31/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros con los detalles de T_CriterioEvaluacion
        /// </summary>
        /// <returns> List<CriterioEvaluacionDTO> </returns>
        public CriterioEvaluacionDTO? ObtenerCriterioEvaluacionPorId(int idCriterioEvaluacion)
        {
            try
            {
                CriterioEvaluacionDTO criteriosFiltro = new();
                var query = @"SELECT Id,
                        Nombre,
                        IdCriterioEvaluacionCategoria,
                        IdFormaCalificacionEvaluacion,
                        IdFormaCalculoEvaluacion,
                        IdFormaCalculoEvaluacion_Parametro as idFormaCalculoEvaluacionParametro
                    FROM pla.T_CriterioEvaluacion
                    WHERE Estado = 1 AND Id = @idCriterioEvaluacion";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idCriterioEvaluacion });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<CriterioEvaluacionDTO>(resultado)!;
                }
                return null;
            }
            catch (Exception E)
            {
                throw new Exception(E.Message);
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 31/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los campos de un registro por el PK
        /// </summary>
        /// <param name="id"> PK de T_CriterioEvaluacion </param> 
        /// <returns> CriterioEvaluacion </returns>
        public CriterioEvaluacion ObtenerPorId(int id)
        {
            try
            {
                string query = @"SELECT Id,
                                       Nombre,
                                       IdCriterioEvaluacionCategoria,
                                       Estado,
                                       UsuarioCreacion,
                                       UsuarioModificacion,
                                       FechaCreacion,
                                       FechaModificacion,
                                       RowVersion,
                                       IdMigracion,
                                       IdFormaCalificacionEvaluacion,
                                       IdFormaCalculoEvaluacion,
                                       IdFormaCalculoEvaluacion_Parametro as idFormaCalculoEvaluacionParametro
                                FROM pla.T_CriterioEvaluacion
                                WHERE Estado = 1
                                      AND Id =  @Id;";
                string resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<CriterioEvaluacion>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#PEPHR-PDPEH-001@Error en ObtenerPorId: {ex.Message}", ex);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 06/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todo el combo de la tabla T_CriterioEvaluacion
        /// </summary>
        /// <returns> Lista DTO - List<ComboDTO>() </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                var query = @"
                    SELECT 
                        Id, Nombre
                    FROM 
                        pla.T_CriterioEvaluacion
                    WHERE 
                        Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado)!;
                }
                return new List<ComboDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#CER-OTC-001@Error en ObtenerCombo: {ex.Message}", ex);
            }
        }
        public List<ComboDTO> ObtenerCriterio(int tipoprograma, int modalidadprograma)
        {
            try
            {
                List<ComboDTO> criteriosFiltro = new List<ComboDTO>();
                var _queryfiltrocriterio = "Select Id,Nombre FROM [pla].[V_CriterioEvaluacion] where Estado = 1 and IdTipoPrograma =@tipoprograma and IdModalidadCurso=@modalidadprograma group by Id,Nombre";
                var SubfiltroCriterio = _dapperRepository.QueryDapper(_queryfiltrocriterio, new { tipoprograma, modalidadprograma });
                if (!string.IsNullOrEmpty(SubfiltroCriterio) && !SubfiltroCriterio.Contains("[]"))
                {
                    criteriosFiltro = JsonConvert.DeserializeObject<List<ComboDTO>>(SubfiltroCriterio);
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
