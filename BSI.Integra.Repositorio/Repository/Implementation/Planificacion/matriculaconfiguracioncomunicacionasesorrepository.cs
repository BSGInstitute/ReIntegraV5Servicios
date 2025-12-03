using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

// =============================================
// Repositorio: MatriculaConfiguracionComunicacionAsesorRepository
// Ubicación: BSI.Integra.Repositorio\Repository\Implementation\Planificacion\
// Autor: Miguel Valdivia
// Fecha: 20/11/2025
// =============================================

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// <summary>
    /// Repositorio para gestionar configuración de comunicación académica desde sistema asesor
    /// </summary>
    public class MatriculaConfiguracionComunicacionAsesorRepository : IMatriculaConfiguracionComunicacionAsesorRepository
    {
        private readonly IDapperRepository _dapperRepository;

        public MatriculaConfiguracionComunicacionAsesorRepository(IDapperRepository dapperRepository)
        {
            _dapperRepository = dapperRepository;
        }

        /// <summary>
        /// Obtiene todas las matrículas activas de un alumno
        /// </summary>
        /// <param name="idAlumno">ID del alumno</param>
        /// <returns>Lista de matrículas activas</returns>
        public IEnumerable<MatriculaActivaAsesorDTO> ObtenerMatriculasActivasPorAlumno(int idAlumno)
        {
            try
            {
                List<MatriculaActivaAsesorDTO> matriculas = new List<MatriculaActivaAsesorDTO>();

                var resultado = _dapperRepository.QuerySPDapper(
                    "pla.SP_ObtenerMatriculasActivasPorAlumno",
                    new { idAlumno }
                );

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    matriculas = JsonConvert.DeserializeObject<List<MatriculaActivaAsesorDTO>>(resultado);
                }

                return matriculas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene el catálogo de horarios de contacto disponibles
        /// </summary>
        /// <returns>Lista de horarios</returns>
        public IEnumerable<CatalogoHorarioContactoAsesorDTO> ObtenerCatalogoHorariosContacto()
        {
            try
            {
                List<CatalogoHorarioContactoAsesorDTO> horarios = new List<CatalogoHorarioContactoAsesorDTO>();

                var query = @"SELECT IdHorarioContacto, TipoHorario, HoraInicio, HoraFin, 
                            IntervaloTexto, OrdenVisualizacion, Estado
                            FROM pla.T_HorarioContacto
                            WHERE Estado = 1
                            ORDER BY OrdenVisualizacion";

                var resultado = _dapperRepository.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    horarios = JsonConvert.DeserializeObject<List<CatalogoHorarioContactoAsesorDTO>>(resultado);
                }

                return horarios;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene la configuración de comunicación de una matrícula específica
        /// </summary>
        /// <param name="idMatriculaCabecera">ID de la matrícula</param>
        /// <param name="idPGeneral">ID del programa general</param>
        /// <returns>Lista de configuraciones</returns>
        public IEnumerable<ConfiguracionComunicacionAsesorDTO> ObtenerConfiguracionComunicacion(
            int idMatriculaCabecera,
            int idPGeneral)
        {
            try
            {
                List<ConfiguracionComunicacionAsesorDTO> configuraciones = new List<ConfiguracionComunicacionAsesorDTO>();

                var query = @"SELECT 
                                c.Id, 
                                c.IdMatriculaCabecera, 
                                c.IdPGeneral, 
                                c.IdHorarioContacto,
                                h.TipoHorario,
                                h.HoraInicio,
                                h.HoraFin,
                                h.IntervaloTexto,
                                h.OrdenVisualizacion,
                                c.MedioWhatsApp, 
                                c.MedioLlamada, 
                                c.MedioCorreo, 
                                c.Estado,
                                c.UsuarioCreacion, 
                                c.FechaCreacion, 
                                c.UsuarioModificacion, 
                                c.FechaModificacion
                            FROM pla.T_MatriculaConfiguracionComunicacion c
                            INNER JOIN pla.T_HorarioContacto h ON c.IdHorarioContacto = h.IdHorarioContacto
                            WHERE c.IdMatriculaCabecera = @idMatriculaCabecera 
                              AND c.IdPGeneral = @idPGeneral
                            ORDER BY h.OrdenVisualizacion";

                var resultado = _dapperRepository.QueryDapper(query, new { idMatriculaCabecera, idPGeneral });

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    configuraciones = JsonConvert.DeserializeObject<List<ConfiguracionComunicacionAsesorDTO>>(resultado);
                }

                return configuraciones;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Inserta configuración de comunicación para una matrícula
        /// </summary>
        /// <param name="idMatriculaCabecera">ID de la matrícula</param>
        /// <param name="idPGeneral">ID del programa</param>
        /// <param name="listaDatos">Configuraciones a insertar</param>
        /// <param name="usuario">Usuario que realiza la acción</param>
        /// <returns>True si fue exitoso</returns>
        public bool InsertarConfiguracionComunicacion(
            int idMatriculaCabecera,
            int idPGeneral,
            List<GuardarConfiguracionComunicacionAsesorDTO> listaDatos,
            string usuario)
        {
            try
            {
                string spInsertar = "pla.SP_MatriculaConfiguracionComunicacion_Insertar";

                foreach (var datos in listaDatos)
                {
                    var resultado = _dapperRepository.QuerySPDapper(spInsertar, new
                    {
                        IdMatriculaCabecera = idMatriculaCabecera,
                        IdPGeneral = idPGeneral,
                        IdHorarioContacto = datos.IdHorarioContacto,
                        MedioWhatsApp = datos.MedioWhatsApp,
                        MedioLlamada = datos.MedioLlamada,
                        MedioCorreo = datos.MedioCorreo,
                        Estado = datos.Estado,
                        Usuario = usuario
                    });

                    if (string.IsNullOrEmpty(resultado) || resultado.Contains("[]"))
                    {
                        return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Actualiza configuración de comunicación existente
        /// </summary>
        /// <param name="listaDatos">Configuraciones a actualizar</param>
        /// <param name="usuario">Usuario que realiza la acción</param>
        /// <returns>True si fue exitoso</returns>
        public bool ActualizarConfiguracionComunicacion(
            List<ConfiguracionComunicacionAsesorDTO> listaDatos,
            string usuario)
        {
            try
            {
                string spActualizar = "pla.SP_MatriculaConfiguracionComunicacion_Actualizar";

                foreach (var datos in listaDatos)
                {
                    var resultado = _dapperRepository.QuerySPDapper(spActualizar, new
                    {
                        Id = datos.Id,
                        MedioWhatsApp = datos.MedioWhatsApp,
                        MedioLlamada = datos.MedioLlamada,
                        MedioCorreo = datos.MedioCorreo,
                        Estado = datos.Estado,
                        Usuario = usuario
                    });

                    if (string.IsNullOrEmpty(resultado) || resultado.Contains("[]"))
                    {
                        return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}