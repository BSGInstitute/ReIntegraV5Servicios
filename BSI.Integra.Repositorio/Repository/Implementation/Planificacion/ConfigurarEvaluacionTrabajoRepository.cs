using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using BSI.Integra.Repositorio.UnitOfWork;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: ConfigurarEvaluacionTrabajoRepository
    /// Autor: Gilmer Quispe.
    /// Fecha: 13/07/2023
    /// <summary>
    /// Gestión general de T_ConfigurarEvaluacionTrabajo
    /// </summary>0
    public class ConfigurarEvaluacionTrabajoRepository : GenericRepository<TConfigurarEvaluacionTrabajo>, IConfigurarEvaluacionTrabajoRepository
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ConfigurarEvaluacionTrabajoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TConfigurarEvaluacionTrabajo, ConfigurarEvaluacionTrabajo>(MemberList.None).ReverseMap();
                cfg.CreateMap<TPreguntaEvaluacionTrabajo, PreguntaEvaluacionTrabajo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TConfigurarEvaluacionTrabajo MapeoEntidad(ConfigurarEvaluacionTrabajo entidad)
        {
            try
            {
                //crea la entidad padre
                TConfigurarEvaluacionTrabajo modelo = _mapper.Map<TConfigurarEvaluacionTrabajo>(entidad);

                //mapea los hijos
                if (entidad.PreguntaEvaluacionTrabajos != null && entidad.PreguntaEvaluacionTrabajos.Count > 0)
                    modelo.TPreguntaEvaluacionTrabajos = _mapper.Map<List<TPreguntaEvaluacionTrabajo>>(entidad.PreguntaEvaluacionTrabajos);

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TConfigurarEvaluacionTrabajo Add(ConfigurarEvaluacionTrabajo entidad)
        {
            try
            {
                var ConfigurarEvaluacionTrabajo = MapeoEntidad(entidad);
                base.Insert(ConfigurarEvaluacionTrabajo);
                return ConfigurarEvaluacionTrabajo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TConfigurarEvaluacionTrabajo Update(ConfigurarEvaluacionTrabajo entidad)
        {
            try
            {
                var ConfigurarEvaluacionTrabajo = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ConfigurarEvaluacionTrabajo.RowVersion = entidadExistente.RowVersion;

                base.Update(ConfigurarEvaluacionTrabajo);
                return ConfigurarEvaluacionTrabajo;
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


        public IEnumerable<TConfigurarEvaluacionTrabajo> Add(IEnumerable<ConfigurarEvaluacionTrabajo> listadoEntidad)
        {
            try
            {
                List<TConfigurarEvaluacionTrabajo> listado = new List<TConfigurarEvaluacionTrabajo>();
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

        public IEnumerable<TConfigurarEvaluacionTrabajo> Update(IEnumerable<ConfigurarEvaluacionTrabajo> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TConfigurarEvaluacionTrabajo> listado = new List<TConfigurarEvaluacionTrabajo>();
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
        /// Fecha: 13/07/2023
        /// <param name="idConfigurarEvaluacionTrabajo"> PK de T_ConfigurarEvaluacionTrabajo </param>
        /// <summary>
        /// Obtiene el el registro con detalles por el IdConfigurarEvaluacionTrabajo
        /// </summary>
        public IEnumerable<ConfigurarEvaluacionTrabajoDetalleDTO> ObtenerConDetallePorIdConfigurarEvaluacionTrabajo(int idConfigurarEvaluacionTrabajo)
        {
            try
            {
                var _queryfiltrocapitulo = @"Select Id,IdTipoEvaluacionTrabajo,Nombre,Descripcion,IdDocumentoPw,ArchivoNombre,ArchivoCarpeta,IdConfigurarEvaluacionTrabajo,IdSeccion,Fila,FechaCreacion,UsuarioCreacion,NombreTipoEvaluacion,DescripcionPregunta,HabilitarInstrucciones,HabilitarArchivo,HabilitarPreguntas,OrdenCapitulo FROM pla.V_RegistroConfigurarEvaluacionTrabajo Where IdConfigurarEvaluacionTrabajo=@IdConfigurarEvaluacionTrabajo AND IdTipoEvaluacionTrabajo=2";
                var SubfiltroCapitulo = _dapperRepository.QueryDapper(_queryfiltrocapitulo, new { IdConfigurarEvaluacionTrabajo = idConfigurarEvaluacionTrabajo });
                if (!string.IsNullOrEmpty(SubfiltroCapitulo) && !SubfiltroCapitulo.Equals("[]"))
                    return JsonConvert.DeserializeObject<IEnumerable<ConfigurarEvaluacionTrabajoDetalleDTO>>(SubfiltroCapitulo);
                return new List<ConfigurarEvaluacionTrabajoDetalleDTO>();
            }
            catch (Exception E)
            {
                throw new Exception(E.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 13/07/2023
        /// <param name="idConfigurarEvaluacionTrabajo"> PK de T_ConfigurarEvaluacionTrabajo </param>
        /// <param name="idSeccion"> (PK) T_Seccion_PW </param>
        /// <param name="fila"> numero de fila </param>
        /// <summary>
        /// Obtiene el el registro con detalles por el IdConfigurarEvaluacionTrabajo
        /// </summary>
        public IEnumerable<ConfigurarEvaluacionTrabajoDetalleDTO> ObtenerConDetallePorIdConfigurarEvaluacionTrabajoIdSeccionFila(int idConfigurarEvaluacionTrabajo, int idSeccion, int fila)
        {
            try
            {
                var _queryfiltrocapitulo = "Select Id,IdTipoEvaluacionTrabajo,Nombre,Descripcion,IdDocumentoPw,ArchivoNombre,ArchivoCarpeta,IdConfigurarEvaluacionTrabajo,IdSeccion,Fila,FechaCreacion,UsuarioCreacion,NombreTipoEvaluacion,DescripcionPregunta,HabilitarInstrucciones,HabilitarArchivo,HabilitarPreguntas,OrdenCapitulo FROM pla.V_RegistroConfigurarEvaluacionTrabajo Where IdConfigurarEvaluacionTrabajo=@IdConfigurarEvaluacionTrabajo AND IdSeccion=@IdSeccion AND Fila=@Fila";
                var SubfiltroCapitulo = _dapperRepository.QueryDapper(_queryfiltrocapitulo, new { IdConfigurarEvaluacionTrabajo = idConfigurarEvaluacionTrabajo, IdSeccion = idSeccion, Fila = fila });
                if (!string.IsNullOrEmpty(SubfiltroCapitulo) && !SubfiltroCapitulo.Equals("[]"))
                    return JsonConvert.DeserializeObject<IEnumerable<ConfigurarEvaluacionTrabajoDetalleDTO>>(SubfiltroCapitulo);
                return new List<ConfigurarEvaluacionTrabajoDetalleDTO>();
            }
            catch (Exception E)
            {
                throw new Exception(E.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 13/07/2023
        /// <param name="idPGeneral"> PK de T_PGeneral </param>
        /// <param name="idSeccion"> (PK) T_Seccion_PW </param>
        /// <param name="fila"> numero de fila </param>
        /// <summary>
        /// Obtiene el el registro con detalles por filtros
        /// </summary>
        public IEnumerable<ConfigurarEvaluacionTrabajoDetalleDTO> ObtenerPorIdPGeneralIdSeccionFila(int idPGeneral, int idSeccion, int fila)
        {
            try
            {
                var _queryfiltrocapitulo = "Select Id,IdTipoEvaluacionTrabajo,Nombre,Descripcion,IdDocumentoPw,ArchivoNombre,ArchivoCarpeta,IdPgeneral,IdSeccion,Fila,FechaCreacion,UsuarioCreacion,NombreTipoEvaluacion,DescripcionPregunta,HabilitarInstrucciones,HabilitarArchivo,HabilitarPreguntas,OrdenCapitulo,IdTareaCriterio,NombreCriterio FROM pla.V_RegistroConfigurarEvaluacionTrabajo Where IdPgeneral=@IdPGeneral AND IdSeccion=@IdSeccion AND Fila=@Fila";
                var SubfiltroCapitulo = _dapperRepository.QueryDapper(_queryfiltrocapitulo, new { IdPgeneral = idPGeneral, IdSeccion = idSeccion, Fila = fila });
                if (string.IsNullOrEmpty(SubfiltroCapitulo) || SubfiltroCapitulo.Equals("[]"))
                    return new List<ConfigurarEvaluacionTrabajoDetalleDTO>();

                var filas = JsonConvert.DeserializeObject<List<ConfigurarEvaluacionTrabajoDetalleDTO>>(SubfiltroCapitulo);

                return filas
                    .GroupBy(x => x.Id)
                    .Select(g =>
                    {
                        var item = g.First();
                        item.criterioTareas = g
                            .Where(x => x.IdTareaCriterio.HasValue)
                            .Select(x => new CriterioTarea
                            {
                                idCriterioTarea = x.IdTareaCriterio.Value,
                                nombre          = x.NombreCriterio
                            })
                            .ToList();
                        return item;
                    })
                    .ToList();
            }
            catch (Exception E)
            {
                throw new Exception(E.Message);
            }
        } 
        /// Autor: Gilmer Quispe.
        /// Fecha: 13/07/2023
        /// <param name="idPGeneral"> PK de T_PGeneral </param> 
        /// <summary>
        /// Obtiene el el registro con detalles por el IdPGeneral
        /// </summary>
        public IEnumerable<ConfigurarEvaluacionTrabajoDetalleDTO> ObtenerConDetallePorIdPGeneral(int idPGeneral)
        {
            try
            {
                var _queryfiltrocapitulo = "Select Id,IdTipoEvaluacionTrabajo,Nombre,Descripcion,IdDocumentoPw,ArchivoNombre,ArchivoCarpeta,IdPgeneral,IdSeccion,Fila,FechaCreacion,UsuarioCreacion,NombreTipoEvaluacion,DescripcionPregunta,HabilitarInstrucciones,HabilitarArchivo,HabilitarPreguntas,OrdenCapitulo FROM pla.V_RegistroConfigurarEvaluacionTrabajo Where IdPgeneral=@IdPGeneral AND IdTipoEvaluacionTrabajo=2";
                var SubfiltroCapitulo = _dapperRepository.QueryDapper(_queryfiltrocapitulo, new { IdPgeneral = idPGeneral });
                if (!string.IsNullOrEmpty(SubfiltroCapitulo) && !SubfiltroCapitulo.Equals("[]"))
                    return JsonConvert.DeserializeObject<IEnumerable<ConfigurarEvaluacionTrabajoDetalleDTO>>(SubfiltroCapitulo);
                return new List<ConfigurarEvaluacionTrabajoDetalleDTO>();
            }
            catch (Exception E)
            {
                throw new Exception(E.Message);
            } 
        } 
        /// Autor: Gilmer Quispe.
        /// Fecha: 17/07/2023
        /// <param name="id"> (PK) </param> 
        /// <summary>
        /// Obtiene el registro por el Primary Key
        /// </summary>
        public void InsertarCriterioConfiguracion(int idConfigurarEvaluacionTrabajo, int idTareaCriterio, string usuario)
        {
            try
            {
                var sp = "pla.[SP_TareaCriterioConfiguracion_Insertar]";
                _dapperRepository.QuerySPDapper(sp, new
                {
                    IdConfigurarEvaluacionTrabajo = idConfigurarEvaluacionTrabajo,
                    IdTareaCriterio               = idTareaCriterio,
                    UsuarioCreacion               = usuario,
                });
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ConfigurarEvaluacionTrabajoRepository.InsertarCriterioConfiguracion: {ex.Message}", ex);
            }
        }

        public void EliminarCriteriosPorConfiguracion(int idConfigurarEvaluacionTrabajo, string usuario)
        {
            try
            {
                var sp = "pla.[SP_TareaCriterioConfiguracion_Eliminar]";
                _dapperRepository.QuerySPDapper(sp, new
                {
                    IdConfigurarEvaluacionTrabajo = idConfigurarEvaluacionTrabajo,
                    UsuarioModificacion           = usuario
                });
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ConfigurarEvaluacionTrabajoRepository.EliminarCriteriosPorConfiguracion: {ex.Message}", ex);
            }
        }

        public ConfigurarEvaluacionTrabajo ObtenerPorId(int id)
        {
            try
            {
                var query = @"SELECT Id,
                                   IdTipoEvaluacionTrabajo,
                                   Nombre,
                                   Descripcion,
                                   IdDocumentoPw,
                                   ArchivoNombre,
                                   ArchivoCarpeta,
                                   Estado,
                                   FechaCreacion,
                                   FechaModificacion,
                                   UsuarioCreacion,
                                   UsuarioModificacion,
                                   RowVersion,
                                   IdMigracion,
                                   IdPGeneral,
                                   IdSeccion,
                                   Fila,
                                   DescripcionPregunta,
                                   OrdenCapitulo,
                                   HabilitarInstrucciones,
                                   HabilitarArchivo,
                                   HabilitarPreguntas,
                                   OrdenEvaluacion
                            FROM pla.T_ConfigurarEvaluacionTrabajo
                            WHERE Estado = 1
                                  AND Id = @Id;";
                var SubfiltroCapitulo = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(SubfiltroCapitulo) && !SubfiltroCapitulo.Equals("null"))
                    return JsonConvert.DeserializeObject<ConfigurarEvaluacionTrabajo>(SubfiltroCapitulo);
                return null;
            }
            catch (Exception E)
            {
                throw new Exception(E.Message);
            }

        }
    }
}
