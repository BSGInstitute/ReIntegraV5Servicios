using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

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
    public class GestionDocenteAgendaRepository : GenericRepository<TProveedor>, IGestionDocenteAgendaRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly IDapperRepository _dapperRepository;

        public GestionDocenteAgendaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository)
            : base(context, connectionFactory, dapperRepository)
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
                List<DocenteConCursoDTO> lista = new List<DocenteConCursoDTO>();
                string query = @"
                        SELECT DISTINCT
                            P.Id AS IdProveedor,
                            CASE
                                WHEN LEN(CONCAT(P.Nombre1, ' ', P.Nombre2, ' ', P.ApePaterno, ' ', P.ApeMaterno)) = 0
                                    THEN P.RazonSocial
                                ELSE CONCAT(P.Nombre1, ' ', P.Nombre2, ' ', P.ApePaterno, ' ', P.ApeMaterno)
                            END AS NombreDocente,
                            PE.Id AS IdPEspecifico,
                            PE.Nombre AS NombreCurso,
                            PE.Codigo AS CodigoCurso,
                            P.IdPersonal_Asignado AS IdPersonalAsignado,
                            ISNULL(CONCAT(PER.Apellidos, ', ', PER.Nombres), '') AS PersonalAsignado,
                            GC.Id AS IdGestionContacto,
                            GDF.Id AS IdFlujo,
                            GDF.Nombre AS NombreFlujo
                        FROM pla.T_PEspecificoSesion S
                        INNER JOIN fin.T_Proveedor P ON S.IdProveedor = P.Id
                        INNER JOIN pla.T_PEspecifico PE ON S.IdPEspecifico = PE.Id
                        LEFT JOIN gp.T_Personal PER ON P.IdPersonal_Asignado = PER.Id
                        INNER JOIN conf.T_ClasificacionPersona CP ON CP.IdTablaOriginal = P.Id AND CP.IdTipoPersona = 4 AND CP.Estado = 1
                        INNER JOIN pla.T_GestionContacto GC ON GC.IdClasificacionPersona = CP.Id AND GC.Estado = 1
                        INNER JOIN pla.T_GestionContactoDocenteFlujo GCDF ON GCDF.IdGestionContacto = GC.Id AND GCDF.Estado = 1
                        INNER JOIN pla.T_GestionDocenteFlujo GDF ON GCDF.IdGestionDocenteFlujo = GDF.Id AND GDF.Estado = 1
                        WHERE S.Estado = 1 AND P.Estado = 1 AND PE.Estado = 1
                        ORDER BY NombreDocente, NombreCurso";

                var resultadoDB = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<DocenteConCursoDTO>>(resultadoDB);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DocenteAgendaCabeceraDTO ObtenerCabeceraDocente(int idProveedor)
        {
            try
            {
                DocenteAgendaCabeceraDTO cabecera = null;
                string query = @"
                        SELECT
                            P.Id AS IdProveedor,
                            CASE
                                WHEN LEN(CONCAT(P.Nombre1, ' ', P.Nombre2, ' ', P.ApePaterno, ' ', P.ApeMaterno)) = 0
                                    THEN P.RazonSocial
                                ELSE CONCAT(P.Nombre1, ' ', P.Nombre2, ' ', P.ApePaterno, ' ', P.ApeMaterno)
                            END AS NombreCompleto,
                            P.Celular1 AS Celular,
                            P.Email,
                            P.IdPersonal_Asignado AS IdPersonalAsignado,
                            ISNULL(CONCAT(PER.Apellidos, ', ', PER.Nombres), '') AS PersonalAsignado,
                            ISNULL(PA.NombrePais, '') AS Pais,
                            ISNULL(C.Nombre, '') AS Ciudad
                        FROM fin.T_Proveedor P
                        LEFT JOIN gp.T_Personal PER ON P.IdPersonal_Asignado = PER.Id
                        LEFT JOIN conf.T_Ciudad C ON P.IdCiudad = C.Id
                        LEFT JOIN conf.T_Pais PA ON C.IdPais = PA.Id
                        WHERE P.Id = @IdProveedor AND P.Estado = 1";

                var resultadoDB = _dapperRepository.QueryDapper(query, new { IdProveedor = idProveedor });
                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                {
                    var lista = JsonConvert.DeserializeObject<List<DocenteAgendaCabeceraDTO>>(resultadoDB);
                    cabecera = lista?.FirstOrDefault();
                }
                return cabecera;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DocenteAgendaFlujoDTO ObtenerFlujoDocente(int idGestionContacto)
        {
            try
            {
                DocenteAgendaFlujoDTO flujo = null;
                string query = @"
                        SELECT
                            GDF.Id AS IdFlujo,
                            GDF.Nombre AS NombreFlujo,
                            GDF.Descripcion AS DescripcionFlujo
                        FROM pla.T_GestionContactoDocenteFlujo GCDF
                        INNER JOIN pla.T_GestionDocenteFlujo GDF ON GCDF.IdGestionDocenteFlujo = GDF.Id
                        WHERE GCDF.IdGestionContacto = @IdGestionContacto
                            AND GCDF.Estado = 1
                            AND GDF.Estado = 1";

                var resultadoDB = _dapperRepository.QueryDapper(query, new { IdGestionContacto = idGestionContacto });
                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                {
                    var lista = JsonConvert.DeserializeObject<List<DocenteAgendaFlujoDTO>>(resultadoDB);
                    flujo = lista?.FirstOrDefault();
                }
                return flujo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DocenteAgendaCronogramaDTO> ObtenerCronogramasDocente(int idProveedor, int idPEspecificoPrioridad)
        {
            try
            {
                List<DocenteAgendaCronogramaDTO> lista = new List<DocenteAgendaCronogramaDTO>();
                string query = @"
                        SELECT DISTINCT
                            PE.Id AS IdPEspecifico,
                            PE.Nombre AS NombreCurso,
                            PE.Codigo AS CodigoCurso,
                            PE.EstadoP AS EstadoCurso,
                            PE.Tipo AS TipoCurso,
                            PE.Categoria AS CategoriaCurso,
                            PE.Ciudad AS CiudadCurso,
                            PE.FechaInicio,
                            PE.FechaTermino,
                            CASE WHEN PE.Id = @IdPEspecificoPrioridad THEN 1 ELSE 0 END AS EsPriorizado
                        FROM pla.T_PEspecificoSesion S
                        INNER JOIN pla.T_PEspecifico PE ON S.IdPEspecifico = PE.Id
                        WHERE S.IdProveedor = @IdProveedor AND S.Estado = 1 AND PE.Estado = 1
                        ORDER BY EsPriorizado DESC, PE.FechaInicio DESC";

                var resultadoDB = _dapperRepository.QueryDapper(query, new { IdProveedor = idProveedor, IdPEspecificoPrioridad = idPEspecificoPrioridad });
                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<DocenteAgendaCronogramaDTO>>(resultadoDB);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DocenteAgendaSesionDTO> ObtenerSesionesPorCursoYDocente(int idProveedor, int idPEspecifico)
        {
            try
            {
                List<DocenteAgendaSesionDTO> lista = new List<DocenteAgendaSesionDTO>();
                string query = @"
                        SELECT
                            S.Id AS IdSesion,
                            S.FechaHoraInicio,
                            S.Duracion,
                            S.Grupo,
                            S.Comentario
                        FROM pla.T_PEspecificoSesion S
                        WHERE S.IdProveedor = @IdProveedor
                            AND S.IdPEspecifico = @IdPEspecifico
                            AND S.Estado = 1
                        ORDER BY S.FechaHoraInicio";

                var resultadoDB = _dapperRepository.QueryDapper(query, new { IdProveedor = idProveedor, IdPEspecifico = idPEspecifico });
                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<DocenteAgendaSesionDTO>>(resultadoDB);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
