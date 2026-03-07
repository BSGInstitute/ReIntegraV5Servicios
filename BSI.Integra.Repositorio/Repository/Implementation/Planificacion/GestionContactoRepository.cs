using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Dapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    public class GestionContactoRepository : GenericRepository<TGestionContacto>, IGestionContactoRepository
    {
        private Mapper _mapper;
        private readonly IntegraDBContext _dbContext;

        public GestionContactoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository)
            : base(context, connectionFactory, dapperRepository)
        {
            _dbContext = context;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<GestionContacto, TGestionContacto>().ReverseMap();
                cfg.CreateMap<GestionContactoLog, TGestionContactoLog>().ReverseMap();
                cfg.CreateMap<ActividadDetalleGestionContacto, TActividadDetalleGestionContacto>().ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        public TGestionContacto AddAsync(GestionContacto entidad)
        {
            try
            {
                var tGestionContacto = MapeoEntidad(entidad);
                base.Insert(tGestionContacto);
                return tGestionContacto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private TGestionContacto MapeoEntidad(GestionContacto entidad)
        {
            try
            {
                TGestionContacto modelo = _mapper.Map<TGestionContacto>(entidad);
                if (entidad.ListaGestionContactoLog != null && entidad.ListaGestionContactoLog.Count > 0)
                {
                    foreach (var logBO in entidad.ListaGestionContactoLog)
                    {
                        var logDB = _mapper.Map<TGestionContactoLog>(logBO);
                        modelo.TGestionContactoLogs.Add(logDB);
                    }
                }
                if (entidad.ListaActividadDetalle != null && entidad.ListaActividadDetalle.Count > 0)
                {
                    foreach (var actividadBO in entidad.ListaActividadDetalle)
                    {
                        var actividadDB = _mapper.Map<TActividadDetalleGestionContacto>(actividadBO);
                        modelo.TActividadDetalleGestionContactos.Add(actividadDB);
                    }
                }
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> ExisteGestionActivaAsync(int idDocente, int idCentroCosto)
        {
            try
            {
                string query = "SELECT COUNT(1) FROM pla.T_GestionContacto WHERE IdClasificacionPersona = @IdDocente AND IdCentroCosto = @IdCentroCosto AND Estado = 1";

                var cantidad = await _dapperRepository.FirstOrDefaultAsync(query, new { IdDocente = idDocente, IdCentroCosto = idCentroCosto });
                if (!string.IsNullOrEmpty(cantidad) && int.TryParse(cantidad, out int count))
                {
                    return count > 0;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al validar duplicidad de gestión", ex);
            }
        }
        public async Task<bool> ExisteCentroCostoAsync(int id)
        {
            try
            {
                string query = "SELECT 1 FROM pla.T_CentroCosto WHERE Id = @Id AND Estado = 1";

                var resultado = await _dapperRepository.FirstOrDefaultAsync(query, new { Id = id });

                return !string.IsNullOrEmpty(resultado);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al validar existencia de Centro de Costo {id}", ex);
            }
        }

        // 2. Validar Personal (Asesor)
        public async Task<bool> ExistePersonalAsync(int id)
        {
            try
            {
                string query = "SELECT 1 FROM gp.T_Personal WHERE Id = @Id AND Estado = 1 AND Activo = 1";

                var resultado = await _dapperRepository.FirstOrDefaultAsync(query, new { Id = id });

                return !string.IsNullOrEmpty(resultado);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al validar existencia de Personal {id}", ex);
            }
        }

        // 3. Validar Clasificación Persona (Docente)
        public async Task<bool> ExisteClasificacionPersonaAsync(int id)
        {
            try
            {
                string query = "SELECT 1 FROM conf.T_ClasificacionPersona WHERE Id = @Id AND Estado = 1";

                var resultado = await _dapperRepository.FirstOrDefaultAsync(query, new { Id = id });

                return !string.IsNullOrEmpty(resultado);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al validar existencia de Clasificación Persona {id}", ex);
            }
        }

        // 4. Validar Fase Gestión Contacto
        public async Task<bool> ExisteFaseGestionAsync(int id)
        {
            try
            {
                string query = "SELECT 1 FROM pla.T_FaseGestionContacto WHERE Id = @Id AND Estado = 1";

                var resultado = await _dapperRepository.FirstOrDefaultAsync(query, new { Id = id });

                return !string.IsNullOrEmpty(resultado);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al validar existencia de Fase Gestión {id}", ex);
            }
        }

        // 5. Validar Origen
        public async Task<bool> ExisteOrigenAsync(int id)
        {
            try
            {
                string query = "SELECT 1 FROM mkt.T_Origen WHERE Id = @Id AND Estado = 1";

                var resultado = await _dapperRepository.FirstOrDefaultAsync(query, new { Id = id });

                return !string.IsNullOrEmpty(resultado);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al validar existencia de Origen {id}", ex);
            }
        }

        /// Autor: JoseVega
        /// Fecha: 27/12/2025
        /// Version: 1.0
        /// <summary>
        /// Actualiza la entidad GestionContacto con control de concurrencia.
        /// </summary>
        /// <param name="entidad">Objeto con la informacion a actualizar</param>
        /// <returns>Entidad actualizada de tipo TGestionContacto</returns>
        public TGestionContacto Update(GestionContacto entidad)
        {
            try
            {
                var gestionContactoEntidad = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });

                if (entidadExistente != null)
                {
                    gestionContactoEntidad.RowVersion = entidadExistente.RowVersion;
                }

                base.Update(gestionContactoEntidad);
                return gestionContactoEntidad;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar GestionContacto", ex);
            }
        }

        /// Autor: JoseVega
        /// Fecha: 27/12/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene un registro de GestionContacto por su Id
        /// </summary>
        /// <param name="id">Identificador de la gestión</param>
        /// <returns>Objeto GestionContacto</returns>
        public async Task<GestionContactoSimpleDTO> ObtenerPorIdAsync(int id)
        {
            try
            {
                string query = @"
                SELECT 
                      Id,
                      IdClasificacionPersona, 
                      IdPersonal_Asignado,
                      IdCentroCosto,
                      IdFaseGestionContacto,
                      IdEstadoGestionContacto,
                      IdClasificacionPersona,
                      IdOrigen,
                      UltimoComentario,
                      UsuarioCreacion,
                      UsuarioModificacion,
                      FechaCreacion,
                      FechaModificacion
                FROM pla.T_GestionContacto WITH(NOLOCK)
                WHERE Id = @Id AND Estado = 1";

                var resultadoDinamico = await _dapperRepository.FirstOrDefaultAsync(query, new { Id = id });

                if (string.IsNullOrEmpty(resultadoDinamico)) return null;

                return JsonConvert.DeserializeObject<GestionContactoSimpleDTO>(resultadoDinamico);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener GestionContacto por ID", ex);
            }
        }
        /// Autor: Lolo Zaa
        /// Fecha: 12/02/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene id y nombre de centros de costo basado en un Nombre Parcial.
        /// </summary>
        /// <param name="valor">Nombre parcial de centro de costo</param>
        /// <returns> lista de combo de centro de costo </returns>
        public IEnumerable<ComboDTO> ObtenerFiltroAutocomplete(string valor)
        {
          try
          {
            string query = @"SELECT Id, Nombre FROM pla.V_TCentroCosto_ParaFiltro WHERE Estado = 1 AND Nombre LIKE @Valor Order BY Nombre ASC";
            string resultado = _dapperRepository.QueryDapper(query, new {Valor = $"%{valor}%"});
            if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
              return JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado)!;
            return new List<ComboDTO>();
          }
          catch (Exception ex)
          {
            throw new Exception($"Error en ObtenerFiltroAutocomplete(): {ex.Message}", ex);
          }
        }

        /// Autor: Lolo Zaa
        /// Fecha: 13/02/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene Id y Nombre de T_PEspecifico filtrado por IdCentroCosto.
        /// </summary>
        /// <param name="idCentroCosto">Identificador del centro de costo</param>
        /// <returns>Lista de Id y Nombre de los registros encontrados</returns>
        public IEnumerable<ComboDTO> ObtenerPEspecificoPorCentroCosto(int idCentroCosto)
        {
            try
            {
                string query = "EXEC pla.SP_TPEspecifico_ObtenerIdCentroCosto @IdCentroCosto";
                string resultado = _dapperRepository.QueryDapper(query, new { IdCentroCosto = idCentroCosto });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    return JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado);
                return new List<ComboDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerPEspecificoPorCentroCosto(): {ex.Message}", ex);
            }
        }

        /// Autor: Lolo Zaa
        /// Fecha: 13/02/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene las sesiones con datos del proveedor asociado a un PE especifico.
        /// </summary>
        /// <param name="idPEspecifico">Identificador del presupuesto especifico</param>
        /// <returns>Lista de IdPEspecificoSesion, IdProveedor y NombreProveedor</returns>
        public IEnumerable<PEspecificoSesionProveedorDTO> ObtenerSesionesProveedorPorPEspecifico(int idPEspecifico)
        {
            try
            {
                string query = "EXEC pla.SP_PEspecificoSesionProveedorPorIdPEspecifico @IdPEspecifico";
                string resultado = _dapperRepository.QueryDapper(query, new { IdPEspecifico = idPEspecifico });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    return JsonConvert.DeserializeObject<IEnumerable<PEspecificoSesionProveedorDTO>>(resultado);
                return new List<PEspecificoSesionProveedorDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerSesionesProveedorPorPEspecifico(): {ex.Message}", ex);
            }
        }

        /// Autor: Lolo Zaa
        /// Fecha: 13/02/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene los flujos de gestion docente activos.
        /// </summary>
        /// <returns>Lista de Id y Nombre de T_GestionDocenteFlujo</returns>
        public IEnumerable<ComboDTO> ObtenerGestionDocenteFlujos()
        {
            try
            {
                string query = "EXEC pla.SP_TGestionDocenteFlujo_Obtener";
                string resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    return JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado);
                return new List<ComboDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerGestionDocenteFlujos(): {ex.Message}", ex);
            }
        }

        /// Autor: Lolo Zaa
        /// Fecha: 13/02/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene la clasificacion de persona asociada a un proveedor.
        /// </summary>
        /// <param name="idProveedor">Id del proveedor a consultar</param>
        /// <returns>IdClasificacionPersona, IdProveedor y RazonSocial</returns>
        public ProveedorClasificacionDTO ObtenerClasificacionPorProveedor(int idProveedor)
        {
            try
            {
                string query = "EXEC fin.SP_ProveedorClasificacionPorId @IdProveedor";
                string resultado = _dapperRepository.QueryDapper(query, new { IdProveedor = idProveedor });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    return JsonConvert.DeserializeObject<IEnumerable<ProveedorClasificacionDTO>>(resultado).FirstOrDefault();
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerClasificacionPorProveedor(): {ex.Message}", ex);
            }
        }

        /// Autor: Lolo Zaa
        /// Fecha: 13/02/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene los estados de gestion de contacto activos.
        /// </summary>
        /// <returns>Lista de Id, Nombre y Descripcion de T_EstadoGestionContacto</returns>
        public IEnumerable<EstadoGestionContactoDTO> ObtenerEstadosGestionContacto()
        {
            try
            {
                string query = "EXEC pla.SP_TEstadoGestionContacto_Obtener";
                string resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    return JsonConvert.DeserializeObject<IEnumerable<EstadoGestionContactoDTO>>(resultado);
                return new List<EstadoGestionContactoDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerEstadosGestionContacto(): {ex.Message}", ex);
            }
        }

        /// Autor: Lolo Zaa
        /// Fecha: 13/02/2026
        /// Version: 1.0
        /// <summary>
        /// Inserta un registro en T_GestionContactoDocenteFlujo.
        /// </summary>
        /// <param name="dto">Datos necesarios para crear la relacion</param>
        /// <returns>Entidad insertada con el Id generado</returns>
        public TGestionContactoDocenteFlujo InsertarGestionContactoDocenteFlujo(InsertarGestionContactoDocenteFlujoDTO dto)
        {
            try
            {
                DateTime fechaActual = DateTime.Now;

                var entidad = new TGestionContactoDocenteFlujo
                {
                    IdGestionContacto    = dto.IdGestionContacto,
                    IdGestionDocenteFlujo = dto.IdGestionDocenteFlujo,
                    Estado               = true,
                    UsuarioCreacion      = dto.UsuarioCreacion,
                    UsuarioModificacion  = dto.UsuarioCreacion,
                    FechaCreacion        = fechaActual,
                    FechaModificacion    = fechaActual
                };

                _dbContext.TGestionContactoDocenteFlujos.Add(entidad);
                return entidad;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en InsertarGestionContactoDocenteFlujo(): {ex.Message}", ex);
            }
        }

        /// Autor: Lolo Zaa
        /// Fecha: 21/02/2026
        /// Version: 2.1
        /// <summary>
        /// Congela un flujo de gestión docente invocando el SP pla.SP_FlujoDocenteCongelar.
        /// El usuario de creación está hardcodeado como 'sgradosn'.
        /// Para flujos de categoría General (IdGestionDocenteCategoria = 1), se puede especificar
        /// la fecha de inicio del flujo congelado. Si no se proporciona, el SP usará NULL.
        /// </summary>
        /// <param name="idGestionContactoDocenteFlujo">ID del vínculo entre gestión contacto y flujo docente</param>
        /// <param name="fechaInicioFlujoCongelado">Fecha de inicio opcional (solo aplica para flujos categoría General)</param>
        public async Task<int> CongelarFlujoDocenteAsync(int idGestionContactoDocenteFlujo, DateTime? fechaInicioFlujoCongelado = null)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@IdGestionContactoDocenteFlujo", idGestionContactoDocenteFlujo, DbType.Int32);
                parameters.Add("@UsuarioCreacion", "sgradosn", DbType.String, size: 50);
                parameters.Add("@FechaInicioFlujoCongelado", fechaInicioFlujoCongelado, DbType.DateTime);

                await _dapperRepository.QuerySPDapperAsync(
                    "pla.SP_FlujoDocenteCongelar",
                    parameters
                );

                // El SP no retorna nada, podríamos retornar el ID del vínculo como confirmación
                return idGestionContactoDocenteFlujo;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en CongelarFlujoDocenteAsync(): {ex.Message}", ex);
            }
        }

        /// Autor: Joseph Llanque
        /// Fecha: 23/02/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene el listado paginado de oportunidades docentes con nombre de docente,
        /// curso (centro de costo) y flujo asignado.
        /// </summary>
        public OportunidadDocenteListResponseDTO ObtenerOportunidadesDocente(
            string busqueda, int pagina, int porPagina)
        {
            try
            {
                int offset = (pagina - 1) * porPagina;

                string queryCount = @"
                    SELECT COUNT(*)
                    FROM pla.T_GestionContacto gc WITH(NOLOCK)
                    LEFT JOIN conf.T_ClasificacionPersona cp WITH(NOLOCK)
                        ON cp.Id = gc.IdClasificacionPersona
                    LEFT JOIN fin.T_Proveedor p WITH(NOLOCK)
                        ON p.Id = cp.IdTablaOriginal
                    LEFT JOIN pla.T_CentroCosto cc WITH(NOLOCK)
                        ON cc.Id = gc.IdCentroCosto
                    LEFT JOIN pla.T_GestionContactoDocenteFlujo gcdf WITH(NOLOCK)
                        ON gcdf.IdGestionContacto = gc.Id AND gcdf.Estado = 1
                    LEFT JOIN pla.T_GestionDocenteFlujo gdf WITH(NOLOCK)
                        ON gdf.Id = gcdf.IdGestionDocenteFlujo
                    WHERE gc.Estado = 1
                      AND (@Busqueda IS NULL OR @Busqueda = ''
                           OR p.RazonSocial LIKE '%' + @Busqueda + '%'
                           OR p.Nombre1    LIKE '%' + @Busqueda + '%'
                           OR cc.Nombre    LIKE '%' + @Busqueda + '%'
                           OR gdf.Nombre   LIKE '%' + @Busqueda + '%')";

                string queryData = @"
                    SELECT
                        gc.Id,
                        gc.IdClasificacionPersona AS DocenteId,
                        COALESCE(LTRIM(RTRIM(
                            CASE WHEN p.Nombre1 IS NOT NULL AND p.Nombre1 <> ''
                                 THEN p.Nombre1 + ' ' + COALESCE(p.ApePaterno, '')
                                 ELSE p.RazonSocial
                            END
                        )), 'Sin nombre') AS DocenteNombre,
	                    GDC.Id,
	                    GDC.Nombre,
   	                    C.IdPais,
                        COALESCE(cc.Nombre, '')  AS Curso,
                        COALESCE(gdf.Nombre, '') AS FlujoAsignado
                    FROM pla.T_GestionContacto gc WITH(NOLOCK)
                    LEFT JOIN conf.T_ClasificacionPersona cp WITH(NOLOCK)
                        ON cp.Id = gc.IdClasificacionPersona
                    LEFT JOIN fin.T_Proveedor p WITH(NOLOCK)
                        ON p.Id = cp.IdTablaOriginal
                    LEFT JOIN conf.T_Ciudad C
   	                    ON p.IdCiudad=c.Id AND c.Estado=1
                    LEFT JOIN pla.T_CentroCosto cc WITH(NOLOCK)
                        ON cc.Id = gc.IdCentroCosto
                    LEFT JOIN pla.T_GestionContactoDocenteFlujo gcdf WITH(NOLOCK)
                        ON gcdf.IdGestionContacto = gc.Id AND gcdf.Estado = 1
                    LEFT JOIN pla.T_GestionDocenteFlujo gdf WITH(NOLOCK)
                        ON gdf.Id = gcdf.IdGestionDocenteFlujo
                    LEFT JOIN pla.T_GestionDocenteCategoria GDC WITH(NOLOCK)
	                    ON GDC.Id = gdf.IdGestionDocenteCategoria
                    WHERE gc.Estado = 1
                      AND (@Busqueda IS NULL OR @Busqueda = ''
                           OR p.RazonSocial LIKE '%' + @Busqueda + '%'
                           OR p.Nombre1    LIKE '%' + @Busqueda + '%'
                           OR cc.Nombre    LIKE '%' + @Busqueda + '%'
                           OR gdf.Nombre   LIKE '%' + @Busqueda + '%')
                    ORDER BY gc.FechaCreacion DESC
                    OFFSET @Offset ROWS FETCH NEXT @PorPagina ROWS ONLY";

                var param = new { Busqueda = busqueda, Offset = offset, PorPagina = porPagina };

                string countStr = _dapperRepository
                    .FirstOrDefaultAsync(queryCount, param).GetAwaiter().GetResult();
                int total = int.TryParse(countStr, out int t) ? t : 0;

                string resultado = _dapperRepository.QueryDapper(queryData, param);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    return new OportunidadDocenteListResponseDTO
                    {
                        Oportunidades = JsonConvert.DeserializeObject<
                            IEnumerable<OportunidadDocenteListItemDTO>>(resultado),
                        Total     = total,
                        Pagina    = pagina,
                        PorPagina = porPagina,
                    };

                return new OportunidadDocenteListResponseDTO
                {
                    Oportunidades = new List<OportunidadDocenteListItemDTO>(),
                    Total     = 0,
                    Pagina    = pagina,
                    PorPagina = porPagina,
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerOportunidadesDocente(): {ex.Message}", ex);
            }
        }

        /// Autor: Joseph Llanque
        /// Fecha: 23/02/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene el listado de docentes (proveedores con clasificacion persona activa)
        /// para el combo de seleccion en oportunidades de tipo General.
        /// </summary>
        public IEnumerable<ComboDTO> ObtenerDocentes()
        {
            try
            {
                string query = @"
                    SELECT DISTINCT
                        p.Id AS Id,
	                    cp.IdTipoPersona,
	                    tp.Nombre AS NombreTipoPersona,
                        COALESCE(LTRIM(RTRIM(
                            CASE WHEN p.Nombre1 IS NOT NULL AND p.Nombre1 <> ''
                                 THEN p.Nombre1 + ' ' + COALESCE(p.ApePaterno, '')
                                 ELSE p.RazonSocial
                            END
                        )), 'Sin nombre') AS Nombre
                    FROM conf.T_ClasificacionPersona cp WITH(NOLOCK)
                    JOIN fin.T_Proveedor p WITH(NOLOCK)
                        ON p.Id = cp.IdTablaOriginal
                    LEFT JOIN conf.T_TipoPersona tp WITH(NOLOCK)
	                    ON tp.Id=cp.IdTipoPersona
                    WHERE cp.Estado = 1
                      AND p.Estado = 1
                      AND tp.Id IN (4,6)
                      AND tp.Estado =1
                    ORDER BY Nombre";

                string resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    return JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado);

                return new List<ComboDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerDocentes(): {ex.Message}", ex);
            }
        }

        /// Autor: Joseph Llanque
        /// Fecha: 28/02/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene las actividades de un flujo congelado agrupadas jerarquicamente segun categoria.
        /// Categoria 1 (General): retorna Actividades con Detalles y Disparadores.
        /// Categoria 2 (Ejecucion Curso): retorna Sesiones con Actividades, Detalles y Disparadores.
        /// </summary>
        public async Task<ActividadesFlujoPorCategoriaResponseDTO> ObtenerActividadesFlujoPorCategoriaAsync(int idGestionContactoDocenteFlujo)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@IdGestionContactoDocenteFlujo", idGestionContactoDocenteFlujo, DbType.Int32);

                string resultado = await _dapperRepository.QuerySPDapperAsync(
                    "pla.SP_GestionDocenteActividadesFlujoPorCategoria",
                    parameters
                );

                if (string.IsNullOrEmpty(resultado) || resultado.Contains("[]"))
                {
                    return new ActividadesFlujoPorCategoriaResponseDTO
                    {
                        IdCategoria = 0,
                        NombreCategoria = "Sin categoria",
                        Sesiones = new List<SesionConActividadesDTO>(),
                        Actividades = new List<ActividadCabeceraDTO>()
                    };
                }

                var registrosPlanos = JsonConvert.DeserializeObject<List<ActividadFlujoRawDTO>>(resultado);
                if (registrosPlanos == null || !registrosPlanos.Any())
                {
                    return new ActividadesFlujoPorCategoriaResponseDTO
                    {
                        IdCategoria = 0,
                        NombreCategoria = "Sin categoria",
                        Sesiones = new List<SesionConActividadesDTO>(),
                        Actividades = new List<ActividadCabeceraDTO>()
                    };
                }

                var primerRegistro = registrosPlanos.First();
                bool esCategoria2 = primerRegistro.IdSesion.HasValue;

                if (esCategoria2)
                {
                    // Categoria 2: Agrupado por Sesion > ActividadCabecera > ActividadDetalle > Disparador
                    var sesiones = registrosPlanos
                        .GroupBy(r => new
                        {
                            r.IdSesion,
                            r.NumeroSesion,
                            r.FechaInicioSesion,
                            r.IdPEspecifico,
                            r.NombrePEspecifico,
                            r.IdProveedor,
                            r.RazonSocialDocente
                        })
                        .Select(sesionGroup => new SesionConActividadesDTO
                        {
                            IdSesion = sesionGroup.Key.IdSesion.Value,
                            NumeroSesion = sesionGroup.Key.NumeroSesion.Value,
                            FechaInicioSesion = sesionGroup.Key.FechaInicioSesion,
                            IdPEspecifico = sesionGroup.Key.IdPEspecifico,
                            NombrePEspecifico = sesionGroup.Key.NombrePEspecifico,
                            IdProveedor = sesionGroup.Key.IdProveedor,
                            RazonSocialDocente = sesionGroup.Key.RazonSocialDocente,
                            Actividades = sesionGroup
                                .GroupBy(r => new
                                {
                                    r.IdGestionDocenteActividadCabeceraCongelada,
                                    r.NombreCabecera,
                                    r.DescripcionCabecera
                                })
                                .Select(cabeceraGroup => new ActividadCabeceraDTO
                                {
                                    IdGestionDocenteActividadCabeceraCongelada = cabeceraGroup.Key.IdGestionDocenteActividadCabeceraCongelada,
                                    NombreCabecera = cabeceraGroup.Key.NombreCabecera,
                                    DescripcionCabecera = cabeceraGroup.Key.DescripcionCabecera,
                                    Detalles = cabeceraGroup
                                        .GroupBy(r => new
                                        {
                                            r.IdGestionDocenteActividadDetalleCongelada,
                                            r.NombreDetalle,
                                            r.NombrePlantilla,
                                            r.MedioComunicacion,
                                            r.EstadoEjecucionDetalle
                                        })
                                        .Select(detalleGroup => new ActividadDetalleDTO
                                        {
                                            IdGestionDocenteActividadDetalleCongelada = detalleGroup.Key.IdGestionDocenteActividadDetalleCongelada,
                                            NombreDetalle = detalleGroup.Key.NombreDetalle,
                                            NombrePlantilla = detalleGroup.Key.NombrePlantilla,
                                            MedioComunicacion = detalleGroup.Key.MedioComunicacion,
                                            EstadoEjecucionDetalle = detalleGroup.Key.EstadoEjecucionDetalle,
                                            Disparadores = detalleGroup.Select(r => new DisparadorDTO
                                            {
                                                IdDisparadorCongelado = r.IdDisparadorCongelado,
                                                TipoDisparador = r.TipoDisparador,
                                                FechaProgramada = r.FechaProgramada,
                                                FechaFija = r.FechaFija,
                                                CantidadTiempoRelativo = r.CantidadTiempoRelativo,
                                                UnidadTiempo = r.UnidadTiempo,
                                                CodigoReferenciaTiempo = r.CodigoReferenciaTiempo,
                                                NombreReferenciaTiempo = r.NombreReferenciaTiempo,
                                                NombreEvento = r.NombreEvento,
                                                OcurrenciaPrevia = r.OcurrenciaPrevia,
                                                EstadoEjecucionDisparador = r.EstadoEjecucionDisparador,
                                                TieneFechaFija = r.TieneFechaFija,
                                                TieneTiempoRelativo = r.TieneTiempoRelativo,
                                                TieneEvento = r.TieneEvento,
                                                TieneOcurrenciaPrevia = r.TieneOcurrenciaPrevia
                                            }).ToList()
                                        }).ToList()
                                }).ToList()
                        }).ToList();

                    return new ActividadesFlujoPorCategoriaResponseDTO
                    {
                        IdCategoria = 2,
                        NombreCategoria = "Ejecucion Curso",
                        Sesiones = sesiones,
                        Actividades = new List<ActividadCabeceraDTO>()
                    };
                }
                else
                {
                    // Categoria 1: Agrupado por ActividadCabecera > ActividadDetalle > Disparador
                    var actividades = registrosPlanos
                        .GroupBy(r => new
                        {
                            r.IdGestionDocenteActividadCabeceraCongelada,
                            r.NombreCabecera,
                            r.DescripcionCabecera
                        })
                        .Select(cabeceraGroup => new ActividadCabeceraDTO
                        {
                            IdGestionDocenteActividadCabeceraCongelada = cabeceraGroup.Key.IdGestionDocenteActividadCabeceraCongelada,
                            NombreCabecera = cabeceraGroup.Key.NombreCabecera,
                            DescripcionCabecera = cabeceraGroup.Key.DescripcionCabecera,
                            Detalles = cabeceraGroup
                                .GroupBy(r => new
                                {
                                    r.IdGestionDocenteActividadDetalleCongelada,
                                    r.NombreDetalle,
                                    r.NombrePlantilla,
                                    r.MedioComunicacion,
                                    r.EstadoEjecucionDetalle
                                })
                                .Select(detalleGroup => new ActividadDetalleDTO
                                {
                                    IdGestionDocenteActividadDetalleCongelada = detalleGroup.Key.IdGestionDocenteActividadDetalleCongelada,
                                    NombreDetalle = detalleGroup.Key.NombreDetalle,
                                    NombrePlantilla = detalleGroup.Key.NombrePlantilla,
                                    MedioComunicacion = detalleGroup.Key.MedioComunicacion,
                                    EstadoEjecucionDetalle = detalleGroup.Key.EstadoEjecucionDetalle,
                                    Disparadores = detalleGroup.Select(r => new DisparadorDTO
                                    {
                                        IdDisparadorCongelado = r.IdDisparadorCongelado,
                                        TipoDisparador = r.TipoDisparador,
                                        FechaProgramada = r.FechaProgramada,
                                        FechaFija = r.FechaFija,
                                        CantidadTiempoRelativo = r.CantidadTiempoRelativo,
                                        UnidadTiempo = r.UnidadTiempo,
                                        CodigoReferenciaTiempo = r.CodigoReferenciaTiempo,
                                        NombreReferenciaTiempo = r.NombreReferenciaTiempo,
                                        NombreEvento = r.NombreEvento,
                                        OcurrenciaPrevia = r.OcurrenciaPrevia,
                                        EstadoEjecucionDisparador = r.EstadoEjecucionDisparador,
                                        TieneFechaFija = r.TieneFechaFija,
                                        TieneTiempoRelativo = r.TieneTiempoRelativo,
                                        TieneEvento = r.TieneEvento,
                                        TieneOcurrenciaPrevia = r.TieneOcurrenciaPrevia
                                    }).ToList()
                                }).ToList()
                        }).ToList();

                    return new ActividadesFlujoPorCategoriaResponseDTO
                    {
                        IdCategoria = 1,
                        NombreCategoria = "General",
                        Sesiones = new List<SesionConActividadesDTO>(),
                        Actividades = actividades
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerActividadesFlujoPorCategoriaAsync(): {ex.Message}", ex);
            }
        }

        /// Autor: Lolo Zaa
        /// Fecha: 03/03/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene las actividades pendientes listas para ejecutar por Hangfire.
        /// </summary>
        public async Task<List<ActividadPendienteDTO>> ObtenerActividadesPendientesAsync()
        {
            try
            {
                var parameters = new DynamicParameters();
                string resultado = await _dapperRepository.QuerySPDapperAsync(
                    "pla.SP_GestionDocenteActividadesPendientesEjecucion",
                    parameters
                );

                if (string.IsNullOrEmpty(resultado) || resultado.Contains("[]"))
                    return new List<ActividadPendienteDTO>();

                return JsonConvert.DeserializeObject<List<ActividadPendienteDTO>>(resultado);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// Autor: Lolo Zaa
        /// Fecha: 03/03/2026
        /// Version: 1.0
        /// <summary>
        /// Actualiza el estado de una actividad despues de su ejecucion por Hangfire.
        /// </summary>
        public async Task<ResultadoEjecucionDTO> ActualizarEstadoActividadAsync(ActualizarEstadoRequestDTO request)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@IdGestionDocenteActividadDetalleCongelada", request.IdActividadDetalleCongelada, DbType.Int32);
                parameters.Add("@IdGestionDocenteDisparadorCongelado", request.IdDisparadorCongelado, DbType.Int32);
                parameters.Add("@CodigoNuevoEstado", request.CodigoNuevoEstado, DbType.String, size: 50);
                parameters.Add("@MensajeResultado", request.MensajeResultado, DbType.String);
                parameters.Add("@MensajeError", request.MensajeError, DbType.String);
                parameters.Add("@UsuarioModificacion", request.UsuarioModificacion, DbType.String, size: 50);

                string resultado = await _dapperRepository.QuerySPDapperAsync(
                    "pla.SP_GestionDocenteActividadActualizarEstado",
                    parameters
                );

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    var resultadoObj = JsonConvert.DeserializeObject<dynamic>(resultado);
                    var primerElemento = ((Newtonsoft.Json.Linq.JArray)resultadoObj)[0];

                    return new ResultadoEjecucionDTO
                    {
                        Exitoso = true,
                        IdRegistro = (int)(primerElemento["IdEjecucion"] ?? 0),
                        Mensaje = "Estado actualizado correctamente"
                    };
                }

                return new ResultadoEjecucionDTO
                {
                    Exitoso = true,
                    IdRegistro = 0,
                    Mensaje = "Estado actualizado correctamente"
                };
            }
            catch (Exception ex)
            {
                return new ResultadoEjecucionDTO
                {
                    Exitoso = false,
                    Error = $"Error al actualizar estado: {ex.Message}"
                };
            }
        }

        /// Autor: Lolo Zaa
        /// Fecha: 03/03/2026
        /// Version: 1.0
        /// <summary>
        /// Marca una ocurrencia y activa disparadores dependientes.
        /// </summary>
        public async Task<ResultadoEjecucionDTO> MarcarOcurrenciaAsync(MarcarOcurrenciaRequestDTO request)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@IdGestionDocenteOcurrenciaCongelada", request.IdGestionDocenteOcurrenciaCongelada, DbType.Int32);
                parameters.Add("@IdGestionContacto", request.IdGestionContacto, DbType.Int32);
                parameters.Add("@Comentario", request.Comentario, DbType.String);
                parameters.Add("@FechaHoraOcurrencia", request.FechaHoraOcurrencia, DbType.DateTime);
                parameters.Add("@UsuarioCreacion", request.UsuarioCreacion, DbType.String, size: 50);

                string resultado = await _dapperRepository.QuerySPDapperAsync(
                    "pla.SP_GestionDocenteOcurrenciaMarcar",
                    parameters
                );

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    var resultadoObj = JsonConvert.DeserializeObject<dynamic>(resultado);
                    var primerElemento = ((Newtonsoft.Json.Linq.JArray)resultadoObj)[0];

                    return new ResultadoEjecucionDTO
                    {
                        Exitoso = true,
                        IdRegistro = (int)(primerElemento["IdOcurrenciaMarcada"] ?? 0),
                        Mensaje = "Ocurrencia marcada correctamente"
                    };
                }

                return new ResultadoEjecucionDTO
                {
                    Exitoso = true,
                    IdRegistro = 0,
                    Mensaje = "Ocurrencia marcada correctamente"
                };
            }
            catch (Exception ex)
            {
                return new ResultadoEjecucionDTO
                {
                    Exitoso = false,
                    Error = $"Error al marcar ocurrencia: {ex.Message}"
                };
            }
        }

        // DTO interno para deserializar resultado plano del SP
        private class ActividadFlujoRawDTO
        {
            public int IdGestionDocenteActividadCabeceraCongelada { get; set; }
            public string NombreCabecera { get; set; }
            public string DescripcionCabecera { get; set; }
            public int IdGestionDocenteActividadDetalleCongelada { get; set; }
            public string NombreDetalle { get; set; }
            public int IdDisparadorCongelado { get; set; }
            public string TipoDisparador { get; set; }
            public DateTime? FechaProgramada { get; set; }
            public DateTime? FechaFija { get; set; }
            public int? CantidadTiempoRelativo { get; set; }
            public string UnidadTiempo { get; set; }
            public string CodigoReferenciaTiempo { get; set; }
            public string NombreReferenciaTiempo { get; set; }
            public string NombreEvento { get; set; }
            public string OcurrenciaPrevia { get; set; }
            public string NombrePlantilla { get; set; }
            public string MedioComunicacion { get; set; }
            public string EstadoEjecucionDetalle { get; set; }
            public string EstadoEjecucionDisparador { get; set; }
            public bool TieneFechaFija { get; set; }
            public bool TieneTiempoRelativo { get; set; }
            public bool TieneEvento { get; set; }
            public bool TieneOcurrenciaPrevia { get; set; }
            public int? IdSesion { get; set; }
            public int? NumeroSesion { get; set; }
            public DateTime? FechaInicioSesion { get; set; }
            public int? IdPEspecifico { get; set; }
            public string NombrePEspecifico { get; set; }
            public int? IdProveedor { get; set; }
            public string RazonSocialDocente { get; set; }
        }

        /// Autor: Lolo Zaa
        /// Fecha: 05/03/2026
        /// Version: 2.0
        /// <summary>
        /// Obtiene una actividad especifica para ejecucion manual sin filtros de estado o fecha.
        /// </summary>
        public async Task<ActividadPendienteDTO> ObtenerActividadParaEjecucionManualAsync(int idActividadDetalleCongelada, int idDisparadorCongelado)
        {
            try
            {
                string query = @"
                    SELECT
                        adc.Id AS IdActividadDetalleCongelada,
                        ad.Nombre AS NombreActividad,
                        ad.IdGestionDocenteActividadDetalleTipo AS IdTipoActividad,
                        ad.IdPlantillaMedioComunicacion,
                        fdc.Id AS IdGestionContactoFlujoCongelado,
                        fdc.IdGestionContactoDocenteFlujo AS IdGestionContactoDocenteFlujo,
                        gc.Id AS IdGestionContacto,
                        dc.Id AS IdDisparadorCongelado,
                        dd.IdGestionDocenteDisparadorFlujoTipo AS TipoDisparador,
                        ISNULL(rtfc.Fecha, GETDATE()) AS FechaEjecucion,
                        NULL AS IdPEspecificoSesion,
                        p.IdPlantillaBase,
                        pmc.IdPlantilla
                    FROM pla.T_GestionDocenteActividadDetalleCongelada adc
                    INNER JOIN pla.T_GestionDocenteActividadDetalle ad ON adc.IdGestionDocenteActividadDetalle = ad.Id
                    INNER JOIN mkt.T_PlantillaMedioComunicacion pmc ON ad.IdPlantillaMedioComunicacion = pmc.Id
                    INNER JOIN mkt.T_Plantilla p ON pmc.IdPlantilla = p.Id
                    INNER JOIN pla.T_GestionDocenteActividadCabeceraCongelada acc ON adc.IdGestionDocenteActividadCabeceraCongelada = acc.Id
                    INNER JOIN pla.T_GestionContactoFlujoCongelado fdc ON acc.IdGestionContactoFlujoCongelado = fdc.Id
                    INNER JOIN pla.T_GestionContactoDocenteFlujo gcdf ON fdc.IdGestionContactoDocenteFlujo = gcdf.Id
                    INNER JOIN pla.T_GestionContacto gc ON gcdf.IdGestionContacto = gc.Id
                    INNER JOIN pla.T_GestionDocenteDisparadorCongelado dc ON adc.Id = dc.IdGestionDocenteActividadDetalleCongelada
                    INNER JOIN pla.T_GestionDocenteDisparadorDetalle dd ON dc.IdGestionDocenteDisparadorDetalle = dd.Id
                    LEFT JOIN pla.T_GestionDocenteDisparadorReglaTiempoFijoCongelado rtfc ON dc.Id = rtfc.IdGestionDocenteDisparadorCongelado AND rtfc.Estado = 1
                    WHERE adc.Id = @IdActividadDetalleCongelada
                      AND dc.Id = @IdDisparadorCongelado
                      AND adc.Estado = 1
                      AND dc.Estado = 1";

                var result = await _dapperRepository.FirstOrDefaultAsync(query, new
                {
                    IdActividadDetalleCongelada = idActividadDetalleCongelada,
                    IdDisparadorCongelado = idDisparadorCongelado
                });

                if (string.IsNullOrEmpty(result))
                    return null;

                // Deserializar manualmente porque FirstOrDefaultAsync retorna string
                var actividad = JsonConvert.DeserializeObject<ActividadPendienteDTO>(result);
                return actividad;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener actividad para ejecucion manual: {ex.Message}", ex);
            }
        }

        /// Autor: Lolo Zaa
        /// Fecha: 05/03/2026
        /// Version: 1.0
        /// <summary>
        /// Ejecuta una actividad inmediatamente sin esperar a Hangfire.
        /// Si MarcarOcurrenciaAsociada es true, tambien marca la ocurrencia y activa dependientes.
        /// NOTA: Solo marca como ejecutado. El envio real de correos se hace en el Service.
        /// </summary>
        public async Task<ResultadoEjecucionDTO> EjecutarActividadManualmenteAsync(EjecutarActividadManualDTO request)
        {
            try
            {
                // 1. Obtener datos de la actividad sin filtros de estado (para ejecucion manual)
                var actividad = await ObtenerActividadParaEjecucionManualAsync(
                    request.IdActividadDetalleCongelada,
                    request.IdDisparadorCongelado
                );

                if (actividad == null)
                {
                    return new ResultadoEjecucionDTO
                    {
                        Exitoso = false,
                        Error = "Actividad no encontrada"
                    };
                }

                // 2. Retornar datos de la actividad para que el controller haga el envio
                // El controller actualizara el estado despues del envio (exitoso o fallido)
                return new ResultadoEjecucionDTO
                {
                    Exitoso = true,
                    IdRegistro = actividad.IdActividadDetalleCongelada,
                    Mensaje = "Actividad lista para ejecutar",
                    DatosActividad = actividad
                };
            }
            catch (Exception ex)
            {
                return new ResultadoEjecucionDTO
                {
                    Exitoso = false,
                    Error = $"Error al ejecutar actividad manualmente: {ex.Message}"
                };
            }
        }

        /// Autor: Lolo Zaa
        /// Fecha: 05/03/2026
        /// Version: 1.0
        /// <summary>
        /// Adelanta la fecha de ejecucion de una actividad para que Hangfire la procese pronto.
        /// </summary>
        public async Task<ResultadoEjecucionDTO> AdelantarFechaActividadAsync(AdelantarActividadDTO request)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@IdDisparadorCongelado", request.IdDisparadorCongelado, DbType.Int32);
                parameters.Add("@UsuarioModificacion", request.UsuarioModificacion, DbType.String, size: 50);

                DateTime nuevaFecha = DateTime.Now;

                string query = $@"
                    UPDATE pla.T_GestionDocenteDisparadorCongelado
                    SET FechaProgramada = '{nuevaFecha:yyyy-MM-dd HH:mm:ss}',
                        UsuarioModificacion = @UsuarioModificacion,
                        FechaModificacion = GETDATE()
                    WHERE Id = @IdDisparadorCongelado
                      AND Estado = 1;

                    SELECT 1 AS Actualizado;
                ";

                string resultado = await _dapperRepository.FirstOrDefaultAsync(query, new {
                    IdDisparadorCongelado = request.IdDisparadorCongelado,
                    UsuarioModificacion = request.UsuarioModificacion
                });

                if (!string.IsNullOrEmpty(resultado))
                {
                    return new ResultadoEjecucionDTO
                    {
                        Exitoso = true,
                        Mensaje = "Fecha adelantada correctamente",
                        NuevaFecha = nuevaFecha
                    };
                }

                return new ResultadoEjecucionDTO
                {
                    Exitoso = false,
                    Error = "No se pudo adelantar la fecha"
                };
            }
            catch (Exception ex)
            {
                return new ResultadoEjecucionDTO
                {
                    Exitoso = false,
                    Error = $"Error al adelantar fecha: {ex.Message}"
                };
            }
        }

        /// Autor: Lolo Zaa
        /// Fecha: 05/03/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene las actividades que dependen de una actividad especifica via ocurrencias.
        /// Busca actividades que tienen disparador OCURRENCIA_PREVIA vinculado a la misma ocurrencia.
        /// </summary>
        public async Task<List<ActividadDependienteDTO>> ObtenerActividadesDependientesAsync(int idActividadDetalleCongelada)
        {
            try
            {
                string query = @"
                    -- Buscar actividades dependientes via ocurrencias
                    SELECT DISTINCT
                        ADC_Dependiente.Id AS IdActividadDetalleCongelada,
                        ADC_Dependiente.Nombre AS NombreActividad,
                        DFT.Nombre AS TipoDisparador,
                        OC.Nombre AS NombreOcurrencia
                    FROM pla.T_GestionDocenteActividadDetalleCongelada ADC
                    INNER JOIN pla.T_GestionDocenteActividadCabeceraCongelada ACC
                        ON ACC.Id = ADC.IdGestionDocenteActividadCabeceraCongelada
                    INNER JOIN pla.T_GestionDocenteFlujoCongelado FC
                        ON FC.Id = ACC.IdGestionDocenteFlujoCongelado

                    -- Buscar ocurrencias asociadas a la actividad
                    INNER JOIN pla.T_GestionDocenteOcurrenciaCongelada OC
                        ON OC.IdGestionDocenteFlujoCongelado = FC.Id

                    -- Buscar actividades que dependen de esa ocurrencia
                    INNER JOIN pla.T_GestionDocenteDisparadorReglaOcurrenciaCongelado DROC
                        ON DROC.IdGestionDocenteOcurrenciaCongelada = OC.Id
                        AND DROC.Estado = 1
                    INNER JOIN pla.T_GestionDocenteDisparadorCongelado DC_Dependiente
                        ON DC_Dependiente.Id = DROC.IdGestionDocenteDisparadorCongelado
                        AND DC_Dependiente.Estado = 1
                    INNER JOIN pla.T_GestionDocenteActividadDetalleCongelada ADC_Dependiente
                        ON ADC_Dependiente.Id = DC_Dependiente.IdGestionDocenteActividadDetalleCongelada
                        AND ADC_Dependiente.Estado = 1
                    INNER JOIN pla.T_GestionDocenteDisparadorFlujoTipo DFT
                        ON DFT.Id = DC_Dependiente.IdGestionDocenteDisparadorFlujoTipo

                    WHERE ADC.Id = @IdActividadDetalleCongelada
                      AND ADC.Estado = 1
                      AND DC_Dependiente.IdGestionDocenteDisparadorFlujoTipo = 2 -- OCURRENCIA_PREVIA
                    ORDER BY ADC_Dependiente.Nombre;
                ";

                string resultado = await _dapperRepository.FirstOrDefaultAsync(query, new {
                    IdActividadDetalleCongelada = idActividadDetalleCongelada
                });

                if (string.IsNullOrEmpty(resultado) || resultado.Contains("[]"))
                    return new List<ActividadDependienteDTO>();

                return JsonConvert.DeserializeObject<List<ActividadDependienteDTO>>(resultado);
            }
            catch (Exception)
            {
                return new List<ActividadDependienteDTO>();
            }
        }

    }
}
