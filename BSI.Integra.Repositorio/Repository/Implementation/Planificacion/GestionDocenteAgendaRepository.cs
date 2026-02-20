using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Autor: Joseph Llanque
    /// Fecha: 20/02/2026
    /// Versión: 1.0
    /// <summary>
    /// Implementación Dapper del repositorio de agenda de docentes.
    /// Ejecuta consultas de solo lectura sobre múltiples tablas del esquema pla, fin, gp y conf.
    /// NOTA: Verificar nombres de columna en T_Proveedor, T_Personal, T_PEspecifico
    /// contra el esquema real de la base de datos antes de ejecutar en producción.
    /// </summary>
    public class GestionDocenteAgendaRepository : IGestionDocenteAgendaRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly IDapperRepository _dapperRepository;

        public GestionDocenteAgendaRepository(
            IntegraDBContext context,
            IConnectionFactory connectionFactory,
            IDapperRepository dapperRepository)
        {
            _connectionFactory = connectionFactory;
            _dapperRepository = dapperRepository;
        }

        /// <summary>
        /// Obtiene la lista plana de docentes con sus cursos y el flujo asignado.
        /// Solo incluye docentes que tengan al menos un flujo activo en T_GestionContactoDocenteFlujo.
        /// </summary>
        public List<DocenteConCursoDTO> ObtenerDocentesConCursos()
        {
            try
            {
                var resultado = new List<DocenteConCursoDTO>();
                string query = @"
                    SELECT DISTINCT
                        prov.Id                                                         AS IdProveedor,
                        prov.NombreCompleto                                             AS NombreDocente,
                        pe.Id                                                           AS IdPEspecifico,
                        pe.Nombre                                                       AS NombreCurso,
                        ISNULL(pe.Codigo, '')                                           AS CodigoCurso,
                        ISNULL(pers.Id, 0)                                              AS IdPersonalAsignado,
                        ISNULL(
                            CONCAT(
                                pers.ApellidoPaterno, ' ',
                                ISNULL(pers.ApellidoMaterno, ''), ', ',
                                pers.Nombre
                            ), ''
                        )                                                               AS PersonalAsignado,
                        gc.Id                                                           AS IdGestionContacto,
                        gdf.Id                                                          AS IdFlujo,
                        gdf.Nombre                                                      AS NombreFlujo
                    FROM pla.T_PEspecificoSesion ses
                    INNER JOIN fin.T_Proveedor prov
                        ON prov.Id = ses.IdProveedor AND prov.Estado = 1
                    INNER JOIN pla.T_PEspecifico pe
                        ON pe.Id = ses.IdPEspecifico AND pe.Estado = 1
                    INNER JOIN conf.T_ClasificacionPersona cp
                        ON cp.IdTablaOriginal = prov.Id AND cp.IdTipoPersona = 4 AND cp.Estado = 1
                    INNER JOIN pla.T_GestionContacto gc
                        ON gc.IdClasificacionPersona = cp.Id AND gc.Estado = 1
                    INNER JOIN pla.T_GestionContactoDocenteFlujo gcdf
                        ON gcdf.IdGestionContacto = gc.Id AND gcdf.Estado = 1
                    INNER JOIN pla.T_GestionDocenteFlujo gdf
                        ON gdf.Id = gcdf.IdGestionDocenteFlujo AND gdf.Estado = 1
                    LEFT JOIN gp.T_Personal pers
                        ON pers.Id = gc.IdPersonal AND pers.Estado = 1
                    WHERE ses.Estado = 1";

                var resultadoDB = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                {
                    resultado = JsonConvert.DeserializeObject<List<DocenteConCursoDTO>>(resultadoDB);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene la cabecera de datos personales de un docente por su IdProveedor.
        /// </summary>
        public CabeceraDocenteAgendaDTO ObtenerCabeceraDocente(int idProveedor)
        {
            try
            {
                CabeceraDocenteAgendaDTO cabecera = null;
                string query = @"
                    SELECT TOP 1
                        prov.Id                                                         AS IdProveedor,
                        prov.NombreCompleto                                             AS NombreCompleto,
                        ISNULL(prov.Celular, '')                                        AS Celular,
                        ISNULL(prov.Email, '')                                          AS Email,
                        ISNULL(pers.Id, 0)                                              AS IdPersonalAsignado,
                        ISNULL(
                            CONCAT(
                                pers.ApellidoPaterno, ' ',
                                ISNULL(pers.ApellidoMaterno, ''), ', ',
                                pers.Nombre
                            ), ''
                        )                                                               AS PersonalAsignado,
                        ISNULL(pais.Nombre, '')                                         AS Pais,
                        ISNULL(ciu.Nombre, '')                                          AS Ciudad
                    FROM fin.T_Proveedor prov
                    INNER JOIN conf.T_ClasificacionPersona cp
                        ON cp.IdTablaOriginal = prov.Id AND cp.IdTipoPersona = 4 AND cp.Estado = 1
                    INNER JOIN pla.T_GestionContacto gc
                        ON gc.IdClasificacionPersona = cp.Id AND gc.Estado = 1
                    LEFT JOIN gp.T_Personal pers
                        ON pers.Id = gc.IdPersonal AND pers.Estado = 1
                    LEFT JOIN conf.T_Ciudad ciu
                        ON ciu.Id = prov.IdCiudad AND ciu.Estado = 1
                    LEFT JOIN conf.T_Pais pais
                        ON pais.Id = ciu.IdPais AND pais.Estado = 1
                    WHERE prov.Id = @IdProveedor AND prov.Estado = 1";

                var parametros = new { IdProveedor = idProveedor };
                var resultadoDB = _dapperRepository.QueryDapper(query, parametros);
                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                {
                    var lista = JsonConvert.DeserializeObject<List<CabeceraDocenteAgendaDTO>>(resultadoDB);
                    cabecera = lista?.Count > 0 ? lista[0] : null;
                }
                return cabecera;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene el flujo docente asociado a un registro de gestión de contacto.
        /// </summary>
        public FlujoDocenteAgendaDTO ObtenerFlujoDocente(int idGestionContacto)
        {
            try
            {
                FlujoDocenteAgendaDTO flujo = null;
                string query = @"
                    SELECT TOP 1
                        gdf.Id                          AS IdFlujo,
                        gdf.Nombre                      AS NombreFlujo,
                        ISNULL(gdf.Descripcion, '')     AS DescripcionFlujo
                    FROM pla.T_GestionContactoDocenteFlujo gcdf
                    INNER JOIN pla.T_GestionDocenteFlujo gdf
                        ON gdf.Id = gcdf.IdGestionDocenteFlujo AND gdf.Estado = 1
                    WHERE gcdf.IdGestionContacto = @IdGestionContacto AND gcdf.Estado = 1";

                var parametros = new { IdGestionContacto = idGestionContacto };
                var resultadoDB = _dapperRepository.QueryDapper(query, parametros);
                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                {
                    var lista = JsonConvert.DeserializeObject<List<FlujoDocenteAgendaDTO>>(resultadoDB);
                    flujo = lista?.Count > 0 ? lista[0] : null;
                }
                return flujo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene todos los cronogramas del docente. El idPEspecificoPriorizado aparece primero.
        /// Los demás se ordenan por FechaInicio descendente.
        /// </summary>
        public List<CronogramaDocenteDTO> ObtenerCronogramasDocente(int idProveedor, int idPEspecificoPriorizado)
        {
            try
            {
                var resultado = new List<CronogramaDocenteDTO>();
                string query = @"
                    SELECT DISTINCT
                        pe.Id                                                           AS IdPEspecifico,
                        pe.Nombre                                                       AS NombreCurso,
                        ISNULL(pe.Codigo, '')                                           AS CodigoCurso,
                        ISNULL(pe.FechaInicio, GETDATE())                               AS FechaInicio,
                        ISNULL(pe.FechaTermino, GETDATE())                              AS FechaTermino,
                        CASE WHEN pe.Id = @IdPEspecificoPriorizado THEN 1 ELSE 0 END   AS EsPriorizado,
                        ''                                                              AS EstadoCurso,
                        ''                                                              AS TipoCurso,
                        ''                                                              AS CategoriaCurso,
                        ''                                                              AS CiudadCurso
                    FROM pla.T_PEspecificoSesion ses
                    INNER JOIN pla.T_PEspecifico pe
                        ON pe.Id = ses.IdPEspecifico AND pe.Estado = 1
                    WHERE ses.IdProveedor = @IdProveedor AND ses.Estado = 1
                    ORDER BY EsPriorizado DESC, pe.FechaInicio DESC";

                var parametros = new { IdProveedor = idProveedor, IdPEspecificoPriorizado = idPEspecificoPriorizado };
                var resultadoDB = _dapperRepository.QueryDapper(query, parametros);
                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                {
                    resultado = JsonConvert.DeserializeObject<List<CronogramaDocenteDTO>>(resultadoDB);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene las sesiones de un docente para un programa específico, ordenadas por FechaHoraInicio.
        /// </summary>
        public List<SesionCronogramaDTO> ObtenerSesionesPorCronograma(int idProveedor, int idPEspecifico)
        {
            try
            {
                var resultado = new List<SesionCronogramaDTO>();
                string query = @"
                    SELECT
                        ses.Id                          AS IdSesion,
                        ses.FechaHoraInicio             AS FechaHoraInicio,
                        ISNULL(ses.Duracion, 0)         AS Duracion,
                        ISNULL(ses.Grupo, 1)            AS Grupo,
                        ses.Comentario                  AS Comentario
                    FROM pla.T_PEspecificoSesion ses
                    WHERE ses.IdProveedor = @IdProveedor
                      AND ses.IdPEspecifico = @IdPEspecifico
                      AND ses.Estado = 1
                    ORDER BY ses.FechaHoraInicio ASC";

                var parametros = new { IdProveedor = idProveedor, IdPEspecifico = idPEspecifico };
                var resultadoDB = _dapperRepository.QueryDapper(query, parametros);
                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                {
                    resultado = JsonConvert.DeserializeObject<List<SesionCronogramaDTO>>(resultadoDB);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
