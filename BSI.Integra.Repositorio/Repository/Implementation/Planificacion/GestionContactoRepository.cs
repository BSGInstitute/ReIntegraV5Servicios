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
using Microsoft.EntityFrameworkCore;
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
                {
                    var lista = JsonConvert.DeserializeObject<IEnumerable<ProveedorClasificacionDTO>>(resultado);
                    // Prioridad: tipo 4 (Proveedor) primero; si no existe, cualquier clasificación disponible.
                    return lista.FirstOrDefault(x => x.IdTipoPersona == 4) ?? lista.FirstOrDefault();
                }
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
        /// Fecha: 07/05/2026
        /// Versión: 1.0
        /// <summary>
        /// Crea una Oportunidad Docente completa en UNA SOLA transacción atómica:
        ///   1. Inserta la cabecera en pla.T_GestionContacto.
        ///   2. Inserta el vínculo con el flujo en pla.T_GestionContactoDocenteFlujo.
        ///   3. Congela el flujo invocando pla.SP_FlujoDocenteCongelar.
        /// Si cualquier paso falla, rollback total — nunca queda data huérfana.
        ///
        /// Reemplaza la orquestación previa que hacía 3 llamadas HTTP separadas desde
        /// el frontend (createOportunidad.usecase.ts), donde un fallo entre paso 1 y
        /// paso 3 dejaba GestionContactos sin flujo o sin congelar.
        ///
        /// El SP de congelamiento se invoca via ExecuteSqlRawAsync (no Dapper) para
        /// que comparta la conexión y la transacción del DbContext.
        /// </summary>
        public async Task<CrearOportunidadCompletaResponseDTO> CrearOportunidadCompletaAsync(CrearOportunidadCompletaRequestDTO dto)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                // Validación de input — al menos uno de los dos identificadores de docente.
                if (!dto.IdClasificacionPersona.HasValue && !dto.IdProveedor.HasValue)
                    throw new Exception("Se debe enviar IdClasificacionPersona (flujo General) o IdProveedor (flujo Asignado al Curso).");

                if (dto.IdGestionDocenteFlujo <= 0)
                    throw new Exception("IdGestionDocenteFlujo es obligatorio.");

                if (dto.IdPersonalAsignado <= 0)
                    throw new Exception("IdPersonalAsignado es obligatorio.");

                // 1. Resolver idClasificacionPersona (puede venir directo o via Proveedor).
                int idClasificacionPersona;
                if (dto.IdClasificacionPersona.HasValue)
                {
                    idClasificacionPersona = dto.IdClasificacionPersona.Value;
                }
                else
                {
                    var clasificacion = ObtenerClasificacionPorProveedor(dto.IdProveedor.Value);
                    if (clasificacion == null)
                        throw new Exception($"No se encontró clasificación de persona para el proveedor {dto.IdProveedor.Value}.");
                    idClasificacionPersona = clasificacion.IdClasificacionPersona;
                }

                DateTime fechaActual = DateTime.Now;
                string usuario = string.IsNullOrWhiteSpace(dto.UsuarioCreacion) ? "sgradosn" : dto.UsuarioCreacion;

                // 2. Insertar cabecera T_GestionContacto.
                var nuevaGestion = new TGestionContacto
                {
                    IdCentroCosto             = dto.IdCentroCosto,
                    IdPersonalAsignado        = dto.IdPersonalAsignado,
                    IdClasificacionPersona    = idClasificacionPersona,
                    IdFaseGestionContacto     = 2,
                    IdOrigen                  = 1124,
                    IdEstadoGestionContacto   = 1,
                    UltimoComentario          = "Creacion de Oportunidad Docente Registrada",
                    EstadoSeguimientoWhatsApp = false,
                    Estado                    = true,
                    UsuarioCreacion           = usuario,
                    UsuarioModificacion       = usuario,
                    FechaCreacion             = fechaActual,
                    FechaModificacion         = fechaActual
                };
                _dbContext.TGestionContactos.Add(nuevaGestion);
                await _dbContext.SaveChangesAsync();

                // 3. Insertar vínculo T_GestionContactoDocenteFlujo.
                var docenteFlujo = new TGestionContactoDocenteFlujo
                {
                    IdGestionContacto     = nuevaGestion.Id,
                    IdGestionDocenteFlujo = dto.IdGestionDocenteFlujo,
                    Estado                = true,
                    UsuarioCreacion       = usuario,
                    UsuarioModificacion   = usuario,
                    FechaCreacion         = fechaActual,
                    FechaModificacion     = fechaActual
                };
                _dbContext.TGestionContactoDocenteFlujos.Add(docenteFlujo);
                await _dbContext.SaveChangesAsync();

                // 4. Congelar flujo invocando SP via EF (comparte conexión y transacción).
                //    El SP detecta la categoría del flujo internamente:
                //      Cat 1 (General):     calcula disparadores TIEMPO_FIJO desde @FechaInicioFlujoCongelado.
                //      Cat 2 (Ej. Curso):   calcula disparadores CRONOGRAMA desde sesiones del PE,
                //                           ignora @FechaInicioFlujoCongelado.
                await _dbContext.Database.ExecuteSqlRawAsync(
                    "EXEC pla.SP_FlujoDocenteCongelar @IdGestionContactoDocenteFlujo, @UsuarioCreacion, @FechaInicioFlujoCongelado",
                    new Microsoft.Data.SqlClient.SqlParameter("@IdGestionContactoDocenteFlujo", docenteFlujo.Id),
                    new Microsoft.Data.SqlClient.SqlParameter("@UsuarioCreacion", usuario),
                    new Microsoft.Data.SqlClient.SqlParameter("@FechaInicioFlujoCongelado", (object)dto.FechaInicioFlujoCongelado ?? DBNull.Value));

                // 5. Commit único.
                await transaction.CommitAsync();

                return new CrearOportunidadCompletaResponseDTO
                {
                    IdGestionContacto             = nuevaGestion.Id,
                    IdGestionContactoDocenteFlujo = docenteFlujo.Id,
                    FlujoCongelado                = true,
                    Mensaje                       = "Oportunidad creada y flujo congelado correctamente."
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception($"Error en CrearOportunidadCompletaAsync(): {ex.Message}", ex);
            }
        }

        /// Autor: Joseph Llanque
        /// Fecha: 07/05/2026
        /// Versión: 1.0
        /// <summary>
        /// Soft delete de Oportunidad Docente con dos validaciones de negocio.
        /// El snapshot congelado se conserva intacto (auditoría histórica); solo se
        /// desactiva la cabecera para que la fila desaparezca de los listados (el
        /// listado ya filtra por gc.Estado = 1, ver ObtenerOportunidadesDocente).
        /// </summary>
        public async Task<EliminarOportunidadResponseDTO> EliminarOportunidadAsync(int idGestionContacto, string usuario)
        {
            try
            {
                // Validación 0: la oportunidad existe y está activa.
                var existe = await _dbContext.TGestionContactos
                    .Where(g => g.Id == idGestionContacto && g.Estado == true)
                    .Select(g => g.Id)
                    .FirstOrDefaultAsync();

                if (existe == 0)
                {
                    return new EliminarOportunidadResponseDTO
                    {
                        Exito   = false,
                        Mensaje = "La oportunidad no existe o ya fue eliminada.",
                        Motivo  = "NO_ENCONTRADA"
                    };
                }

                // Validación 1: ocurrencias marcadas.
                const string sqlCountMarcadas = @"
                    SELECT COUNT(1)
                    FROM pla.T_GestionDocenteOcurrenciaMarcada OM WITH (NOLOCK)
                    INNER JOIN pla.T_GestionContactoDocenteFlujo GCDF WITH (NOLOCK)
                        ON GCDF.Id = OM.IdGestionContactoDocenteFlujo
                    WHERE GCDF.IdGestionContacto = @IdGestionContacto
                      AND OM.Estado = 1;";

                string countMarcadasResult = await _dapperRepository.FirstOrDefaultAsync(
                    sqlCountMarcadas, new { IdGestionContacto = idGestionContacto });
                int cantidadMarcadas = int.TryParse(countMarcadasResult, out int c) ? c : 0;

                if (cantidadMarcadas > 0)
                {
                    return new EliminarOportunidadResponseDTO
                    {
                        Exito                       = false,
                        Mensaje                     = $"No se puede eliminar: la oportunidad tiene {cantidadMarcadas} ocurrencia(s) marcada(s).",
                        Motivo                      = "OCURRENCIAS_MARCADAS",
                        CantidadOcurrenciasMarcadas = cantidadMarcadas
                    };
                }

                // Validación 2: el flujo ya inició (algún disparador fijo con Fecha <= GETDATE()).
                const string sqlFechaInicio = @"
                    SELECT MIN(RTF.Fecha) AS FechaInicio
                    FROM pla.T_GestionContactoDocenteFlujo GCDF WITH (NOLOCK)
                    INNER JOIN pla.T_GestionContactoFlujoCongelado FC WITH (NOLOCK)
                        ON FC.IdGestionContactoDocenteFlujo = GCDF.Id
                    INNER JOIN pla.T_GestionDocenteActividadCabeceraCongelada AC WITH (NOLOCK)
                        ON AC.IdGestionContactoFlujoCongelado = FC.Id
                    INNER JOIN pla.T_GestionDocenteActividadDetalleCongelada AD WITH (NOLOCK)
                        ON AD.IdGestionDocenteActividadCabeceraCongelada = AC.Id
                    INNER JOIN pla.T_GestionDocenteDisparadorCongelado DC WITH (NOLOCK)
                        ON DC.IdGestionDocenteActividadDetalleCongelada = AD.Id
                    INNER JOIN pla.T_GestionDocenteDisparadorReglaTiempoFijoCongelado RTF WITH (NOLOCK)
                        ON RTF.IdGestionDocenteDisparadorCongelado = DC.Id
                    WHERE GCDF.IdGestionContacto = @IdGestionContacto
                      AND GCDF.Estado = 1
                      AND RTF.Estado = 1;";

                string fechaInicioStr = await _dapperRepository.FirstOrDefaultAsync(
                    sqlFechaInicio, new { IdGestionContacto = idGestionContacto });

                if (DateTime.TryParse(fechaInicioStr, out DateTime fechaInicio))
                {
                    if (fechaInicio <= DateTime.Now)
                    {
                        return new EliminarOportunidadResponseDTO
                        {
                            Exito                     = false,
                            Mensaje                   = $"No se puede eliminar: el flujo ya inició el {fechaInicio:dd/MM/yyyy HH:mm}.",
                            Motivo                    = "FLUJO_INICIADO",
                            FechaInicioFlujoDetectada = fechaInicio
                        };
                    }
                }

                // Validaciones OK → soft delete.
                var gestion = await _dbContext.TGestionContactos
                    .FirstOrDefaultAsync(g => g.Id == idGestionContacto);

                if (gestion == null)
                {
                    return new EliminarOportunidadResponseDTO
                    {
                        Exito   = false,
                        Mensaje = "La oportunidad no existe.",
                        Motivo  = "NO_ENCONTRADA"
                    };
                }

                gestion.Estado              = false;
                gestion.UltimoComentario    = "Oportunidad eliminada (soft delete)";
                gestion.UsuarioModificacion = string.IsNullOrWhiteSpace(usuario) ? "sgradosn" : usuario;
                gestion.FechaModificacion   = DateTime.Now;

                _dbContext.TGestionContactos.Update(gestion);
                await _dbContext.SaveChangesAsync();

                return new EliminarOportunidadResponseDTO
                {
                    Exito   = true,
                    Mensaje = "Oportunidad eliminada correctamente."
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en EliminarOportunidadAsync(): {ex.Message}", ex);
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 09/03/2026
        /// Version: 1.0
        /// <summary>
        /// Congela una cabecera de actividad especifica y la asocia unicamente a una lista de sesiones.
        /// Invoca el SP pla.SP_ActividadDocenteCongelarPorSesiones.
        /// </summary>
        public async Task<int> CongelarActividadPorSesionesAsync(int idGestionContactoDocenteFlujo, int idGestionDocenteActividadCabecera, string idPEspecificoSesion_Lista, string usuarioCreacion)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@IdGestionContactoDocenteFlujo", idGestionContactoDocenteFlujo, DbType.Int32);
                parameters.Add("@IdGestionDocenteActividadCabecera", idGestionDocenteActividadCabecera, DbType.Int32);
                parameters.Add("@IdPEspecificoSesion_Lista", idPEspecificoSesion_Lista, DbType.String);
                parameters.Add("@UsuarioCreacion", usuarioCreacion, DbType.String, size: 50);

                await _dapperRepository.QuerySPDapperAsync(
                    "pla.SP_ActividadDocenteCongelarPorSesiones",
                    parameters
                );

                return idGestionContactoDocenteFlujo;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en CongelarActividadPorSesionesAsync(): {ex.Message}", ex);
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 10/03/2026
        /// Version: 1.0
        /// <summary>
        /// Agrega una actividad extra a un flujo ya congelado.
        /// Invoca el SP pla.SP_ActividadDocenteAgregarExtraCongelada.
        /// </summary>
        public async Task<int> AgregarActividadExtraCongeladaAsync(int idGestionContactoFlujoCongelado, int idGestionDocenteActividadCabecera, string idPEspecificoSesion_Lista, string usuarioCreacion)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@IdGestionContactoFlujoCongelado", idGestionContactoFlujoCongelado, DbType.Int32);
                parameters.Add("@IdGestionDocenteActividadCabecera", idGestionDocenteActividadCabecera, DbType.Int32);
                parameters.Add("@IdPEspecificoSesion_Lista", idPEspecificoSesion_Lista, DbType.String);
                parameters.Add("@UsuarioCreacion", usuarioCreacion, DbType.String, size: 50);

                await _dapperRepository.QuerySPDapperAsync(
                    "pla.SP_ActividadDocenteAgregarExtraCongelada",
                    parameters
                );

                return idGestionContactoFlujoCongelado;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en AgregarActividadExtraCongeladaAsync(): {ex.Message}", ex);
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
                    -- Tipo 4: Proveedor
                    LEFT JOIN fin.T_Proveedor p WITH(NOLOCK)
                        ON p.Id = cp.IdTablaOriginal AND cp.IdTipoPersona = 4
                    -- Tipo 6: DocentePostulante
                    LEFT JOIN pla.T_DocentePostulante dp WITH(NOLOCK)
                        ON dp.Id = cp.IdTablaOriginal AND cp.IdTipoPersona = 6
                    LEFT JOIN pla.T_CentroCosto cc WITH(NOLOCK)
                        ON cc.Id = gc.IdCentroCosto
                    LEFT JOIN pla.T_GestionContactoDocenteFlujo gcdf WITH(NOLOCK)
                        ON gcdf.IdGestionContacto = gc.Id AND gcdf.Estado = 1
                    LEFT JOIN pla.T_GestionDocenteFlujo gdf WITH(NOLOCK)
                        ON gdf.Id = gcdf.IdGestionDocenteFlujo
                    WHERE gc.Estado = 1
                      AND (@Busqueda IS NULL OR @Busqueda = ''
                           OR p.RazonSocial     LIKE '%' + @Busqueda + '%'
                           OR p.Nombre1         LIKE '%' + @Busqueda + '%'
                           OR dp.Nombre1        LIKE '%' + @Busqueda + '%'
                           OR dp.ApellidoPaterno LIKE '%' + @Busqueda + '%'
                           OR cc.Nombre         LIKE '%' + @Busqueda + '%'
                           OR gdf.Nombre        LIKE '%' + @Busqueda + '%')";

                string queryData = @"
                    SELECT
                        gc.Id,
                        gc.IdClasificacionPersona AS DocenteId,
                        COALESCE(LTRIM(RTRIM(
                            CASE
                                WHEN cp.IdTipoPersona = 6
                                    THEN dp.Nombre1 + ' ' +
                                         COALESCE(dp.Nombre2 + ' ', '') +
                                         COALESCE(dp.ApellidoPaterno, '') + ' ' +
                                         COALESCE(dp.ApellidoMaterno, '')
                                WHEN p.Nombre1 IS NOT NULL AND p.Nombre1 <> ''
                                    THEN p.Nombre1 + ' ' + COALESCE(p.ApePaterno, '')
                                ELSE p.RazonSocial
                            END
                        )), 'Sin nombre') AS DocenteNombre,
                        GDC.Id AS IdCategoria,
                        GDC.Nombre AS NombreCategoria,
                        gc.IdCentroCosto,
	                    gc.IdPersonal_Asignado,
                        COALESCE(C.IdPais, CDP.IdPais) AS IdPais,
                        COALESCE(cc.Nombre, '')  AS Curso,
                        COALESCE(gdf.Nombre, '') AS FlujoAsignado
                    FROM pla.T_GestionContacto gc WITH(NOLOCK)
                    LEFT JOIN conf.T_ClasificacionPersona cp WITH(NOLOCK)
                        ON cp.Id = gc.IdClasificacionPersona
                    -- Tipo 4: Proveedor
                    LEFT JOIN fin.T_Proveedor p WITH(NOLOCK)
                        ON p.Id = cp.IdTablaOriginal AND cp.IdTipoPersona = 4
                    LEFT JOIN conf.T_Ciudad C WITH(NOLOCK)
                        ON C.Id = p.IdCiudad AND C.Estado = 1
                    -- Tipo 6: DocentePostulante
                    LEFT JOIN pla.T_DocentePostulante dp WITH(NOLOCK)
                        ON dp.Id = cp.IdTablaOriginal AND cp.IdTipoPersona = 6
                    LEFT JOIN conf.T_Ciudad CDP WITH(NOLOCK)
                        ON CDP.Id = dp.IdCiudad AND CDP.Estado = 1
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
                           OR p.RazonSocial      LIKE '%' + @Busqueda + '%'
                           OR p.Nombre1          LIKE '%' + @Busqueda + '%'
                           OR dp.Nombre1         LIKE '%' + @Busqueda + '%'
                           OR dp.ApellidoPaterno  LIKE '%' + @Busqueda + '%'
                           OR cc.Nombre          LIKE '%' + @Busqueda + '%'
                           OR gdf.Nombre         LIKE '%' + @Busqueda + '%')
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
        /// Version: 1.1
        /// <summary>
        /// Obtiene el listado de docentes (proveedores con clasificacion persona activa)
        /// para el combo de seleccion en oportunidades de tipo General.
        /// Retorna Id, IdTipoPersona, NombreTipoPersona y Nombre del docente.
        /// Filtra por tipos de persona: 4 (Proveedor) y 6 (otro tipo).
        /// </summary>
        public IEnumerable<DocenteComboDTO> ObtenerDocentes()
        {
            try
            {
                string query = @"
                    -- Tipo 4: Proveedor (fin.T_Proveedor)
                    -- Id = cp.Id (ClasificacionPersona) para evitar colisión con IDs de tipo 6
                    SELECT DISTINCT
                        cp.Id AS Id,
                        cp.IdTipoPersona,
                        tp.Nombre AS NombreTipoPersona,
                        COALESCE(LTRIM(RTRIM(
                            CASE WHEN p.Nombre1 IS NOT NULL AND p.Nombre1 <> ''
                                 THEN p.Nombre1 + ' ' + COALESCE(p.ApePaterno, '')
                                 ELSE p.RazonSocial
                            END
                        )), 'Sin nombre') AS Nombre,
                        p.Id AS IdTablaOriginal
                    FROM conf.T_ClasificacionPersona cp WITH(NOLOCK)
                    JOIN fin.T_Proveedor p WITH(NOLOCK)
                        ON p.Id = cp.IdTablaOriginal
                    LEFT JOIN conf.T_TipoPersona tp WITH(NOLOCK)
                        ON tp.Id = cp.IdTipoPersona
                    WHERE cp.Estado = 1
                      AND p.Estado = 1
                      AND tp.Id = 4
                      AND tp.Estado = 1

                    UNION

                    -- Tipo 6: DocentePostulante (pla.T_DocentePostulante)
                    -- Id = cp.Id (ClasificacionPersona) para evitar colisión con IDs de tipo 4
                    SELECT DISTINCT
                        cp.Id AS Id,
                        cp.IdTipoPersona,
                        tp.Nombre AS NombreTipoPersona,
                        COALESCE(LTRIM(RTRIM(
                            dp.Nombre1 + ' ' +
                            COALESCE(dp.Nombre2 + ' ', '') +
                            COALESCE(dp.ApellidoPaterno, '') + ' ' +
                            COALESCE(dp.ApellidoMaterno, '')
                        )), 'Sin nombre') AS Nombre,
                        dp.Id AS IdTablaOriginal
                    FROM conf.T_ClasificacionPersona cp WITH(NOLOCK)
                    JOIN pla.T_DocentePostulante dp WITH(NOLOCK)
                        ON dp.Id = cp.IdTablaOriginal
                    LEFT JOIN conf.T_TipoPersona tp WITH(NOLOCK)
                        ON tp.Id = cp.IdTipoPersona
                    WHERE cp.Estado = 1
                      AND dp.Estado = 1
                      AND tp.Id = 6
                      AND tp.Estado = 1

                    ORDER BY Nombre";

                string resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    return JsonConvert.DeserializeObject<IEnumerable<DocenteComboDTO>>(resultado);

                return new List<DocenteComboDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerDocentes(): {ex.Message}", ex);
            }
        }

        /// Autor: Joseph Llanque
        /// Fecha: 28/02/2026
        /// Version: 1.1
        /// <summary>
        /// Obtiene las actividades de un flujo congelado agrupadas jerarquicamente segun categoria.
        /// Categoria 1 (General): retorna Actividades con Detalles y Disparadores.
        /// Categoria 2 (Ejecucion Curso): retorna Sesiones con Actividades, Detalles y Disparadores.
        /// Incluye ocurrencias congeladas asociadas a cada detalle (segundo result set del SP).
        /// </summary>
        public async Task<ActividadesFlujoPorCategoriaResponseDTO> ObtenerActividadesFlujoPorCategoriaAsync(int idGestionContactoDocenteFlujo)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@IdGestionContactoDocenteFlujo", idGestionContactoDocenteFlujo, DbType.Int32);

                List<ActividadFlujoRawDTO> registrosPlanos;
                List<OcurrenciaCongeladaDTO> ocurrenciasPlanas;
                List<DisparadorOcurrenciaPreviaDTO> disparadorOcurrenciaPrevia = new List<DisparadorOcurrenciaPreviaDTO>();
                List<OcurrenciaMarcadaDTO> ocurrenciasMarcadas = new List<OcurrenciaMarcadaDTO>();

                using (var conn = _connectionFactory.GetConnection)
                {
                    using (var multi = await conn.QueryMultipleAsync(
                        "pla.SP_GestionDocenteActividadesFlujoPorCategoria",
                        parameters,
                        commandType: CommandType.StoredProcedure))
                    {
                        registrosPlanos = (await multi.ReadAsync<ActividadFlujoRawDTO>()).ToList();
                        ocurrenciasPlanas = (await multi.ReadAsync<OcurrenciaCongeladaDTO>()).ToList();
                        // Result-sets nuevos (v1.5 del SP) para calcular rama activa.
                        // Si el SP esta en una version anterior, IsConsumed = true tras leer
                        // los 2 result-sets esperados. En ese caso saltamos los Reads extras y
                        // los flags EstaEnRamaActiva quedan en true por default (compatibilidad).
                        if (!multi.IsConsumed)
                        {
                            disparadorOcurrenciaPrevia = (await multi.ReadAsync<DisparadorOcurrenciaPreviaDTO>()).ToList();
                        }
                        if (!multi.IsConsumed)
                        {
                            ocurrenciasMarcadas = (await multi.ReadAsync<OcurrenciaMarcadaDTO>()).ToList();
                        }
                    }
                }

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

                var ocurrenciasPorDetalle = ocurrenciasPlanas
                    .GroupBy(o => o.IdGestionDocenteActividadDetalleCongelada)
                    .ToDictionary(g => g.Key, g => g.ToList() as IEnumerable<OcurrenciaCongeladaDTO>);

                // ===== Calculo de "rama activa" por detalle congelado =====
                // Si el padre de la ocurrencia previa que dispara el detalle ya marco OTRA
                // ocurrencia hermana, este detalle pertenece a una rama descartada.
                // Si el padre aun no marco nada, queda como "rama_pendiente" (visible).
                var disparadorAOcurrenciaPrevia = disparadorOcurrenciaPrevia
                    .ToDictionary(x => x.IdDisparadorCongelado, x => x.IdOcurrenciaPreviaCongelada);
                var ocurrenciasMarcadasIds = new HashSet<int>(
                    ocurrenciasMarcadas.Select(x => x.IdGestionDocenteOcurrenciaCongelada));
                var detallesPadreConMarca = new HashSet<int>(
                    ocurrenciasMarcadas.Select(x => x.IdDetalleCongeladaPadre));
                var ocurrenciaCongeladaADetallePadre = ocurrenciasPlanas
                    .ToDictionary(
                        o => o.IdGestionDocenteOcurrenciaCongelada,
                        o => o.IdGestionDocenteActividadDetalleCongelada);

                var esRamaActivaPorDetalle = new Dictionary<int, bool>();
                var motivoExclusionPorDetalle = new Dictionary<int, string>();
                foreach (var grupoDetalle in registrosPlanos
                    .GroupBy(r => r.IdGestionDocenteActividadDetalleCongelada))
                {
                    bool esActiva = true;
                    string motivo = null;
                    foreach (var idDisp in grupoDetalle.Select(r => r.IdDisparadorCongelado).Distinct())
                    {
                        if (!disparadorAOcurrenciaPrevia.TryGetValue(idDisp, out var idOcPrevia))
                            continue;  // disparador no es de tipo "ocurrencia-anterior"
                        if (!ocurrenciaCongeladaADetallePadre.TryGetValue(idOcPrevia, out var idDetallePadre))
                            continue;
                        if (!detallesPadreConMarca.Contains(idDetallePadre))
                        {
                            // padre aun sin marcar → ambas ramas potencialmente vivas
                            motivo = "rama_pendiente";
                            continue;
                        }
                        if (ocurrenciasMarcadasIds.Contains(idOcPrevia))
                        {
                            // padre marco JUSTO esta rama → activa, sin motivo
                            motivo = null;
                            esActiva = true;
                            break;
                        }
                        // padre marco OTRA rama hermana → este disparador es descartado
                        esActiva = false;
                        motivo = "rama_descartada";
                        break;
                    }
                    esRamaActivaPorDetalle[grupoDetalle.Key] = esActiva;
                    motivoExclusionPorDetalle[grupoDetalle.Key] = motivo;
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
                                    r.IdGestionDocenteActividadCabecera,
                                    r.IdGestionDocenteActividadCabeceraCongelada,
                                    r.NombreCabecera,
                                    r.DescripcionCabecera
                                })
                                .Select(cabeceraGroup => new ActividadCabeceraDTO
                                {
                                    IdGestionDocenteActividadCabecera= cabeceraGroup.Key.IdGestionDocenteActividadCabecera,
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
                                            r.EstadoEjecucionDetalle,
                                            r.NombreOcurrenciaMarcada,
                                            r.TipoOcurrenciaMarcada,
                                            r.ComentarioOcurrenciaMarcada,
                                            r.UsuarioEjecucion,
                                            r.ClasificacionComentarioIA
                                        })
                                        .Select(detalleGroup => new ActividadDetalleDTO
                                        {
                                            IdGestionDocenteActividadCabecera = cabeceraGroup.Key.IdGestionDocenteActividadCabecera,
                                            IdGestionDocenteActividadDetalleCongelada = detalleGroup.Key.IdGestionDocenteActividadDetalleCongelada,
                                            NombreDetalle = detalleGroup.Key.NombreDetalle,
                                            NombrePlantilla = detalleGroup.Key.NombrePlantilla,
                                            MedioComunicacion = detalleGroup.Key.MedioComunicacion,
                                            EstadoEjecucionDetalle = detalleGroup.Key.EstadoEjecucionDetalle,
                                            NombreOcurrenciaMarcada = detalleGroup.Key.NombreOcurrenciaMarcada,
                                            TipoOcurrenciaMarcada = detalleGroup.Key.TipoOcurrenciaMarcada,
                                            ComentarioOcurrenciaMarcada = detalleGroup.Key.ComentarioOcurrenciaMarcada,
                                            UsuarioEjecucion = detalleGroup.Key.UsuarioEjecucion,
                                            ClasificacionComentarioIA = detalleGroup.Key.ClasificacionComentarioIA,
                                            EstaEnRamaActiva = esRamaActivaPorDetalle.TryGetValue(detalleGroup.Key.IdGestionDocenteActividadDetalleCongelada, out var raC2) ? raC2 : true,
                                            MotivoExclusion = motivoExclusionPorDetalle.TryGetValue(detalleGroup.Key.IdGestionDocenteActividadDetalleCongelada, out var meC2) ? meC2 : null,
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
                                            }).ToList(),
                                            Ocurrencias = ocurrenciasPorDetalle.TryGetValue(detalleGroup.Key.IdGestionDocenteActividadDetalleCongelada, out var ocs) ? ocs : new List<OcurrenciaCongeladaDTO>()
                                        }).ToList()
                                }).ToList()
                        }).ToList();

                    return new ActividadesFlujoPorCategoriaResponseDTO
                    {
                        IdGestionContactoFlujoCongelado = primerRegistro.IdGestionContactoFlujoCongelado,
                        IdCategoria = 2,
                        NombreCategoria = "Ejecucion Curso",
                        IdGestionDocenteActividadCabecera = registrosPlanos.First().IdGestionDocenteActividadCabecera,
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
                            r.IdGestionDocenteActividadCabecera,
                            r.IdGestionDocenteActividadCabeceraCongelada,
                            r.NombreCabecera,
                            r.DescripcionCabecera
                        })
                        .Select(cabeceraGroup => new ActividadCabeceraDTO
                        {
                            IdGestionDocenteActividadCabecera = cabeceraGroup.Key.IdGestionDocenteActividadCabecera,
                            IdGestionDocenteActividadCabeceraCongelada = cabeceraGroup.Key.IdGestionDocenteActividadCabeceraCongelada,
                            NombreCabecera = cabeceraGroup.Key.NombreCabecera,
                            DescripcionCabecera = cabeceraGroup.Key.DescripcionCabecera,
                            Detalles = cabeceraGroup
                                .GroupBy(r => new
                                {
                                    r.IdGestionDocenteActividadCabecera,
                                    r.IdGestionDocenteActividadDetalleCongelada,
                                    r.NombreDetalle,
                                    r.NombrePlantilla,
                                    r.MedioComunicacion,
                                    r.EstadoEjecucionDetalle,
                                    r.NombreOcurrenciaMarcada,
                                    r.TipoOcurrenciaMarcada,
                                    r.ComentarioOcurrenciaMarcada,
                                    r.UsuarioEjecucion,
                                    r.ClasificacionComentarioIA
                                })
                                .Select(detalleGroup => new ActividadDetalleDTO
                                {
                                    IdGestionDocenteActividadCabecera = cabeceraGroup.Key.IdGestionDocenteActividadCabecera,
                                    IdGestionDocenteActividadDetalleCongelada = detalleGroup.Key.IdGestionDocenteActividadDetalleCongelada,
                                    NombreDetalle = detalleGroup.Key.NombreDetalle,
                                    NombrePlantilla = detalleGroup.Key.NombrePlantilla,
                                    MedioComunicacion = detalleGroup.Key.MedioComunicacion,
                                    EstadoEjecucionDetalle = detalleGroup.Key.EstadoEjecucionDetalle,
                                    NombreOcurrenciaMarcada = detalleGroup.Key.NombreOcurrenciaMarcada,
                                    TipoOcurrenciaMarcada = detalleGroup.Key.TipoOcurrenciaMarcada,
                                    ComentarioOcurrenciaMarcada = detalleGroup.Key.ComentarioOcurrenciaMarcada,
                                    UsuarioEjecucion = detalleGroup.Key.UsuarioEjecucion,
                                    ClasificacionComentarioIA = detalleGroup.Key.ClasificacionComentarioIA,
                                    EstaEnRamaActiva = esRamaActivaPorDetalle.TryGetValue(detalleGroup.Key.IdGestionDocenteActividadDetalleCongelada, out var raC1) ? raC1 : true,
                                    MotivoExclusion = motivoExclusionPorDetalle.TryGetValue(detalleGroup.Key.IdGestionDocenteActividadDetalleCongelada, out var meC1) ? meC1 : null,
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
                                    }).ToList(),
                                    Ocurrencias = ocurrenciasPorDetalle.TryGetValue(detalleGroup.Key.IdGestionDocenteActividadDetalleCongelada, out var ocs) ? ocs : new List<OcurrenciaCongeladaDTO>()
                                }).ToList()
                        }).ToList();

                    return new ActividadesFlujoPorCategoriaResponseDTO
                    {
                        IdGestionContactoFlujoCongelado = primerRegistro.IdGestionContactoFlujoCongelado,
                        IdCategoria = 1,
                        NombreCategoria = "General",
                        IdGestionDocenteActividadCabecera = registrosPlanos.First().IdGestionDocenteActividadCabecera,
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
        /// Fecha: 17/03/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene los disparadores ejecutados cuyos resultados (ocurrencias) estan
        /// pendientes de clasificacion por el servicio externo de IA.
        /// Llama a pla.SP_GestionDocenteDisparadorPendienteClasificacion.
        /// </summary>
        public async Task<List<DisparadorPendienteClasificacionDTO>> ObtenerDisparadoresPendientesClasificacionAsync()
        {
            try
            {
                var parameters = new DynamicParameters();
                string resultado = await _dapperRepository.QuerySPDapperAsync(
                    "pla.SP_GestionDocenteDisparadorPendienteClasificacion",
                    parameters
                );

                if (string.IsNullOrEmpty(resultado) || resultado.Contains("[]"))
                    return new List<DisparadorPendienteClasificacionDTO>();

                return JsonConvert.DeserializeObject<List<DisparadorPendienteClasificacionDTO>>(resultado);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// Autor: Lolo Zaa
        /// Fecha: 03/03/2026
        /// Version: 2.0
        /// <summary>
        /// Llama al SP pla.SP_GestionDocenteOcurrenciaMarcar que inserta en
        /// T_GestionDocenteOcurrenciaMarcada y retorna los disparadores dependientes.
        /// La logica de conversion de disparadores vive en GestionContactoService.
        /// </summary>
        public async Task<List<DisparadorConversionDTO>> MarcarOcurrenciaAsync(MarcarOcurrenciaRequestDTO request)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@IdGestionDocenteOcurrenciaCongelada", request.IdGestionDocenteOcurrenciaCongelada, DbType.Int32);
                parameters.Add("@IdGestionContacto",                   request.IdGestionContacto,                  DbType.Int32);
                parameters.Add("@Comentario",                          request.Comentario,                         DbType.String);
                parameters.Add("@FechaHoraOcurrencia",                 request.FechaHoraOcurrencia,                DbType.DateTime);
                parameters.Add("@UsuarioCreacion",                     request.UsuarioCreacion,                    DbType.String, size: 50);

                string resultado = await _dapperRepository.QuerySPDapperAsync(
                    "pla.SP_GestionDocenteOcurrenciaMarcar",
                    parameters
                );

                if (string.IsNullOrEmpty(resultado) || resultado.Contains("[]"))
                    return new List<DisparadorConversionDTO>();

                return JsonConvert.DeserializeObject<List<DisparadorConversionDTO>>(resultado)
                    ?? new List<DisparadorConversionDTO>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// Autor: Lolo Zaa
        /// Fecha: 19/03/2026
        /// Version: 1.0
        /// <summary>
        /// Orquesta la persistencia completa del marcado de ocurrencia:
        /// 1. Llama al SP (inserta ocurrencia marcada + retorna disparadores dependientes)
        /// 2. Por cada disparador: crea regla fija congelada (solo tablas congeladas, sin tocar maestras)
        /// 3. Actualiza estados y genera logs de auditoria.
        /// Todo dentro de una transaccion.
        /// </summary>
        public async Task<ResultadoEjecucionDTO> MarcarOcurrenciaConDisparadoresAsync(MarcarOcurrenciaRequestDTO request)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                // 1. Llamar al SP — inserta ocurrencia marcada y retorna disparadores dependientes
                var disparadoresData = await MarcarOcurrenciaAsync(request);

                DateTime fechaHoraMarcado = DateTime.Now;

                // 1b. Actualizar T_GestionDocenteOcurrenciaCongelada a EJECUTADO (siempre, con o sin dependientes)
                int idEstadoEjecutado = await _dbContext.TGestionDocenteEstadoEjecucions
                    .Where(e => e.Codigo == "EJECUTADO")
                    .Select(e => e.Id)
                    .FirstAsync();

                var ocurrenciaCongelada = await _dbContext.TGestionDocenteOcurrenciaCongelada
                    .FindAsync(request.IdGestionDocenteOcurrenciaCongelada);

                if (ocurrenciaCongelada != null)
                {
                    ocurrenciaCongelada.IdGestionDocenteEstadoEjecucion = idEstadoEjecutado;
                    ocurrenciaCongelada.UsuarioModificacion             = request.UsuarioCreacion;
                    ocurrenciaCongelada.FechaModificacion               = fechaHoraMarcado;
                }

                if (!disparadoresData.Any())
                {
                    // El asesor confirmó la ocurrencia → si el GC estaba en estado 2 (Alerta)
                    // por la propuesta IA del schedule, baja a 1 (Activo). El proximo tick
                    // del schedule lo subira otra vez si vuelve a haber pendiente.
                    await BajarEstadoGCSiAlertaAsync(request.IdGestionDocenteOcurrenciaCongelada, request.UsuarioCreacion);

                    await _dbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return new ResultadoEjecucionDTO
                    {
                        Exitoso    = true,
                        IdRegistro = 0,
                        Mensaje    = "Ocurrencia marcada correctamente. Sin disparadores dependientes."
                    };
                }

                int idOcurrenciaMarcada = disparadoresData.First().IdGestionDocenteOcurrenciaMarcada;

                foreach (var disparador in disparadoresData)
                {
                    // 2a. Regla fija congelada — solo toca tablas congeladas.
                    // IdGestionDocenteDisparadorReglaTiempo viene del SP (relativo congelado existente).
                    // IdGestionDocenteDisparadorReglaTiempoFijo es null porque el disparador era RELATIVO
                    // y no existe maestro FIJO previo para este detalle.
                    if (disparador.IdGestionDocenteDisparadorReglaTiempo.HasValue)
                    {
                        var reglaFijaCongelada = new TGestionDocenteDisparadorReglaTiempoFijoCongelado
                        {
                            IdGestionDocenteDisparadorCongelado       = disparador.IdGestionDocenteDisparadorCongelado,
                            IdGestionDocenteDisparadorReglaTiempoFijo = null,
                            IdGestionDocenteDisparadorReglaTiempo     = disparador.IdGestionDocenteDisparadorReglaTiempo.Value,
                            IdGestionDocenteDisparadorDetalle         = disparador.IdGestionDocenteDisparadorDetalle,
                            Fecha                                     = disparador.FechaCalculada,
                            IdGestionDocenteEstadoEjecucion           = disparador.IdGestionDocenteEstadoPorEjecutar,
                            Estado              = true,
                            UsuarioCreacion     = request.UsuarioCreacion,
                            UsuarioModificacion = request.UsuarioCreacion,
                            FechaCreacion       = fechaHoraMarcado,
                            FechaModificacion   = fechaHoraMarcado
                        };
                        _dbContext.TGestionDocenteDisparadorReglaTiempoFijoCongelados.Add(reglaFijaCongelada);
                    }

                    // 2d. Actualizar disparador congelado
                    var disparadorCongelado = await _dbContext.TGestionDocenteDisparadorCongelados
                        .FindAsync(disparador.IdGestionDocenteDisparadorCongelado);

                    if (disparadorCongelado != null)
                    {
                        var estadoAnterior = disparadorCongelado.IdGestionDocenteEstadoEjecucion;

                        disparadorCongelado.IdGestionDocenteDisparadorFlujoTipo = 1;
                        disparadorCongelado.IdGestionDocenteEstadoEjecucion     = disparador.IdGestionDocenteEstadoPorEjecutar;
                        disparadorCongelado.UsuarioModificacion                 = request.UsuarioCreacion;
                        disparadorCongelado.FechaModificacion                   = fechaHoraMarcado;

                        _dbContext.TGestionDocenteDisparadorCongeladoLogs.Add(new TGestionDocenteDisparadorCongeladoLog
                        {
                            IdGestionDocenteDisparadorCongelado     = disparador.IdGestionDocenteDisparadorCongelado,
                            IdGestionDocenteEstadoEjecucionAnterior = estadoAnterior,
                            IdGestionDocenteEstadoEjecucionNuevo    = disparador.IdGestionDocenteEstadoPorEjecutar,
                            Estado              = true,
                            UsuarioCreacion     = request.UsuarioCreacion,
                            UsuarioModificacion = request.UsuarioCreacion,
                            FechaCreacion       = fechaHoraMarcado,
                            FechaModificacion   = fechaHoraMarcado
                        });
                    }

                    // 2e. Actualizar actividad detalle
                    var actividadDetalle = await _dbContext.TGestionDocenteActividadDetalleCongelada
                        .FindAsync(disparador.IdGestionDocenteActividadDetalleCongelada);

                    if (actividadDetalle != null &&
                        actividadDetalle.IdGestionDocenteEstadoEjecucion != disparador.IdGestionDocenteEstadoPorEjecutar)
                    {
                        var estadoAnteriorActividad = actividadDetalle.IdGestionDocenteEstadoEjecucion;

                        actividadDetalle.IdGestionDocenteEstadoEjecucion = disparador.IdGestionDocenteEstadoPorEjecutar;
                        actividadDetalle.UsuarioModificacion             = request.UsuarioCreacion;
                        actividadDetalle.FechaModificacion               = fechaHoraMarcado;

                        _dbContext.TGestionDocenteActividadDetalleCongeladaLogs.Add(new TGestionDocenteActividadDetalleCongeladaLog
                        {
                            IdGestionDocenteActividadDetalleCongelada  = disparador.IdGestionDocenteActividadDetalleCongelada,
                            IdGestionDocenteEstadoEjecucionAnterior    = estadoAnteriorActividad,
                            IdGestionDocenteEstadoEjecucionNuevo       = disparador.IdGestionDocenteEstadoPorEjecutar,
                            Estado              = true,
                            UsuarioCreacion     = request.UsuarioCreacion,
                            UsuarioModificacion = request.UsuarioCreacion,
                            FechaCreacion       = fechaHoraMarcado,
                            FechaModificacion   = fechaHoraMarcado
                        });
                    }
                }

                // Misma lógica de cierre de alerta que el branch sin dependientes.
                await BajarEstadoGCSiAlertaAsync(request.IdGestionDocenteOcurrenciaCongelada, request.UsuarioCreacion);

                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                return new ResultadoEjecucionDTO
                {
                    Exitoso    = true,
                    IdRegistro = idOcurrenciaMarcada,
                    Mensaje    = $"Ocurrencia marcada correctamente. {disparadoresData.Count} disparador(es) convertido(s) a TIEMPO_FIJO"
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return new ResultadoEjecucionDTO
                {
                    Exitoso = false,
                    Error   = $"Error al marcar ocurrencia: {ex.Message}"
                };
            }
        }

        /// Autor: Joseph Llanque
        /// Fecha: 07/05/2026
        /// Version: 1.0
        /// <summary>
        /// Resuelve el cierre de alerta del GestionContacto al confirmar una ocurrencia
        /// (típicamente la propuesta de la IA en flujo MARM).
        ///
        /// Recorre la cadena de tablas congeladas: ocurrencia → detalle → cabecera → flujo →
        /// docenteFlujo → gestionContacto. Si el GC está actualmente en estado 2 (Alerta),
        /// lo baja a 1 (Activo). El predicate `AND IdEstadoGestionContacto = 2` actúa como
        /// guarda — si el GC no está alertado (1, 3, 4) este método es no-op.
        ///
        /// El próximo tick del schedule lo volverá a subir a 2 si detecta nuevamente una
        /// ocurrencia pendiente, manteniendo la coherencia del workflow de alertas.
        ///
        /// Se ejecuta dentro de la misma transacción del marcado (DbContext.Database
        /// reusa la transacción activa automáticamente).
        /// </summary>
        private async Task BajarEstadoGCSiAlertaAsync(int idOcurrenciaCongelada, string usuario)
        {
            await _dbContext.Database.ExecuteSqlInterpolatedAsync($@"
                UPDATE GC
                SET GC.IdEstadoGestionContacto = 1,
                    GC.UsuarioModificacion     = {usuario},
                    GC.FechaModificacion       = GETDATE()
                FROM pla.T_GestionContacto GC
                INNER JOIN pla.T_GestionContactoDocenteFlujo GCDF
                    ON GCDF.IdGestionContacto = GC.Id
                INNER JOIN pla.T_GestionContactoFlujoCongelado FC
                    ON FC.IdGestionContactoDocenteFlujo = GCDF.Id
                INNER JOIN pla.T_GestionDocenteActividadCabeceraCongelada AC
                    ON AC.IdGestionContactoFlujoCongelado = FC.Id
                INNER JOIN pla.T_GestionDocenteActividadDetalleCongelada AD
                    ON AD.IdGestionDocenteActividadCabeceraCongelada = AC.Id
                INNER JOIN pla.T_GestionDocenteOcurrenciaCongelada OC
                    ON OC.IdGestionDocenteActividadDetalleCongelada = AD.Id
                WHERE OC.Id = {idOcurrenciaCongelada}
                  AND GC.Estado = 1
                  AND GC.IdEstadoGestionContacto = 2;");
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
                string query = "EXEC [pla].[SP_GestionDocenteActividadDetalleCongelada_ObtenerParaEjecucion] @IdActividadDetalleCongelada, @IdDisparadorCongelado";

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
