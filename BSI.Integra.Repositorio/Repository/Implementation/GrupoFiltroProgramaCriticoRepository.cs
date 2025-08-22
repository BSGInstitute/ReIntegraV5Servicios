using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: GrupoFiltroProgramaCriticoRepository
    /// Autor: Margiory Ramirez Neyra.
    /// Fecha: 07/10/2022
    /// <summary>
    /// Gestión general de T_GrupoFiltroProgramaCritico
    /// </summary>
    public class GrupoFiltroProgramaCriticoRepository : GenericRepository<TGrupoFiltroProgramaCritico>, IGrupoFiltroProgramaCriticoRepository
    {
        private Mapper _mapper;

        public GrupoFiltroProgramaCriticoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TGrupoFiltroProgramaCritico, GrupoFiltroProgramaCritico>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TGrupoFiltroProgramaCritico MapeoEntidad(GrupoFiltroProgramaCritico entidad)
        {
            try
            {
                //crea la entidad padre
                TGrupoFiltroProgramaCritico modelo = _mapper.Map<TGrupoFiltroProgramaCritico>(entidad);

                //mapea los hijos
                //if (entidad.ListadoHijoNivel1 != null && entidad.ListadoHijoNivel1.Count > 0)
                //{
                //    var listadoHijoNivel1 = _mapper.Map<List<THijo>>(entidad.ListadoHijoNivel1);
                //    foreach (var hijoNivel1 in listadoHijoNivel1)
                //    {
                //        modelo.THijo.Add(hijoNivel1);
                //    }
                //}

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TGrupoFiltroProgramaCritico Add(GrupoFiltroProgramaCritico entidad)
        {
            try
            {
                var GrupoFiltroProgramaCritico = MapeoEntidad(entidad);
                base.Insert(GrupoFiltroProgramaCritico);
                return GrupoFiltroProgramaCritico;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TGrupoFiltroProgramaCritico Update(GrupoFiltroProgramaCritico entidad)
        {
            try
            {
                var GrupoFiltroProgramaCritico = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                GrupoFiltroProgramaCritico.RowVersion = entidadExistente.RowVersion;

                base.Update(GrupoFiltroProgramaCritico);
                return GrupoFiltroProgramaCritico;
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


        public IEnumerable<TGrupoFiltroProgramaCritico> Add(IEnumerable<GrupoFiltroProgramaCritico> listadoEntidad)
        {
            try
            {
                List<TGrupoFiltroProgramaCritico> listado = new List<TGrupoFiltroProgramaCritico>();
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

        public IEnumerable<TGrupoFiltroProgramaCritico> Update(IEnumerable<GrupoFiltroProgramaCritico> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TGrupoFiltroProgramaCritico> listado = new List<TGrupoFiltroProgramaCritico>();
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
        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 07/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_GrupoFiltroProgramaCritico para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                List<ComboDTO> rpta = new List<ComboDTO>();

                var query = "SELECT Id, Nombre FROM mkt.T_GrupoFiltroProgramaCritico WHERE Estado=1";
                var resultado = _dapperRepository.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 07/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Id,Nombre,Apellidos,NombreCompleto,IdAsesor de los todo el personal que son asesores
        /// </summary>
        /// <returns>Lista de objetos de clase DatosPersonalAsesorDTO</returns>
        public List<DatosPersonalAsesorDTO> ObtenerTodoPersonalAsesoresFiltro()
        {
            try
            {
                List<DatosPersonalAsesorDTO> personalAsesores = new List<DatosPersonalAsesorDTO>();
                var query = string.Empty;
                query = "SELECT Id,Nombres,Apellidos,Email,NombreCompleto,asignado,IdAsesor FROM gp.V_TPersonal_ObtenerAsesores WHERE Rol = 'VENTAS' and (TipoPersonal = 'Coordinador' or TipoPersonal = 'Asesor') and Estado = 1 order by Id";
                var personalAsesor = _dapperRepository.QueryDapper(query, null);
                personalAsesores = JsonConvert.DeserializeObject<List<DatosPersonalAsesorDTO>>(personalAsesor);

                return personalAsesores;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 07/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros sin los campos de auditoría
        /// </summary>
        /// <returns>Lista de objetos de clase GrupoFiltroProgramaCriticoDTO</returns>
        public List<GrupoFiltroProgramaCriticoDTO> ObtenerTodoGrid()
        {
            try
            {
                var lista = GetBy(x => true, y => new GrupoFiltroProgramaCriticoDTO
                {
                    Id = y.Id,
                    Nombre = y.Nombre,
                    Descripcion = y.Descripcion,

                }).OrderByDescending(x => x.Id).ToList();

                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 09/10/2022
        /// Version: 1.0
        /// Se obtienen los programas generales por GrupoFiltroProgramaCritico
        /// </summary>
        /// <param name="idGrupo">Id del grupo de programa critico (PK de la tabla pla.T_GrupoFiltroProgramaCritico)</param>
        /// <returns>Lista de objetos de clase PGeneralSubAreaDTO</returns>
        public List<PGeneralSubAreaDTO> ObtenerPorIdGrupo(int idGrupo)
        {
            try
            {
                List<PGeneralSubAreaDTO> lista = new List<PGeneralSubAreaDTO>();
                var query = "SELECT Id, Nombre, NombreAreaCapacitacion, NombreSubAreaCapacitacion,AsignaVenta FROM mkt.V_ObtenerPGeneralPorGrupoFiltro WHERE IdGrupoFiltro = @idGrupo";
                var respuestaDapper = _dapperRepository.QueryDapper(query, new { idGrupo = idGrupo });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<PGeneralSubAreaDTO>>(respuestaDapper);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool gru(int id, string usuario)
        {
            throw new NotImplementedException();
        }
        /// Autor: Margiory Ramirez.
        /// Fecha: 08/01/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene el reporte de programas criticos
        /// </summary>
        /// <param name="filtros">Objeto de clase ReporteProgramasCriticosFiltroDTO pasando los parametros necesarios para generar el reporte</param>
        /// <returns>Lista de objetos de clase ReporteEstructuradoAsignacionProgramasCriticosDTO</returns>

        public List<ReporteProgramasCriticosAsignacionDiariaSimplificadoDTO> ObtenerReporteProgramasCriticosAsignacion(ReporteProgramasCriticosFiltroDTO filtros)
        {
            try
            {
                string grupos = filtros.Grupos.Any() ? string.Join(",", filtros.Grupos) : null;
                string periodos = filtros.Periodo.Any() ? string.Join(",", filtros.Periodo) : null;

                List<ReporteProgramasCriticosAsignacionDiariaSimplificadoDTO> resultadoCrudo = new List<ReporteProgramasCriticosAsignacionDiariaSimplificadoDTO>();
                var query = _dapperRepository.QuerySPDapper("com.SP_ReporteAsignacionDiaria_TodosNuevoModelo", new
                {
                    IdPeriodo = periodos,
                    IdGrupo = grupos
                });



                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    resultadoCrudo = JsonConvert.DeserializeObject<List<ReporteProgramasCriticosAsignacionDiariaSimplificadoDTO>>(query);
                }

                return resultadoCrudo;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: Margiory Ramirez.
        /// Fecha: 08/01/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Id y Nombre de todos los registros.
        /// </summary>
        /// <returns>Lista de objetos de clase FiltroIdNombreDTO</returns>


        public List<FiltroIdNombreDTO> ObtenerFiltro()
        {
            try
            {
                List<FiltroIdNombreDTO> lista = new List<FiltroIdNombreDTO>();

                string query = "SELECT Id, Nombre FROM pla.T_GrupoFiltroProgramaCritico WHERE Nombre NOT IN ('W. coordinadores', 'V. CESADOS') ORDER BY Nombre";
                var resultado = _dapperRepository.QueryDapper(query, null);

                lista = JsonConvert.DeserializeObject<List<FiltroIdNombreDTO>>(resultado);
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// Autor: Margiory Ramirez.
        /// Fecha: 08/01/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene el reporte de programas criticos (indicadores de ventas)
        /// </summary>
        /// <param name="filtros">Objeto de clase ReporteProgramasCriticosFiltroDTO pasando los parametros necesarios para generar el reporte</param>
        /// <returns>Lista de objetos de clase ReporteProgramasCriticosDTO</returns>
        public List<ReporteProgramasCriticosDTO> ObtenerReporteProgramasCriticos(ReporteProgramasCriticosFiltroDTO filtros)
        {
            try
            {
                string grupos = null, paises = null, estadoPrograma = null, areas = null, subareas = null;
                if (filtros.Grupos.Count() > 0) grupos = String.Join(",", filtros.Grupos);
                if (filtros.Pais.Count() > 0) paises = String.Join(",", filtros.Pais);
                if (filtros.EstadoPrograma.Count() > 0) estadoPrograma = String.Join(",", filtros.EstadoPrograma);
                if (filtros.Areas.Count() > 0) areas = String.Join(",", filtros.Areas);
                if (filtros.Subareas.Count() > 0) subareas = String.Join(",", filtros.Subareas);

                List<ReporteProgramasCriticosDTO> items = new List<ReporteProgramasCriticosDTO>();
                var query = _dapperRepository.QuerySPDapper("com.SP_ReporteProgramasCriticosNuevoModelo", new { grupos, paises, estadoPrograma, areas, subareas });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteProgramasCriticosDTO>>(query);
                }

                foreach (var item in items)
                {
                    if (item.IngresoPromedioIS == 0 && item.PrecioPromedio10Descuento != 0)
                    {
                        item.PuntoEquilibrio = Convert.ToInt32(item.CostoPrograma / item.PrecioPromedio10Descuento);
                    }
                    else if (item.IngresoPromedioIS > 0)
                    {
                        item.PuntoEquilibrio = Convert.ToInt32(item.CostoPrograma / item.IngresoPromedioIS);
                    }
                    else item.PuntoEquilibrio = 0;
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
    }
}

      
        
