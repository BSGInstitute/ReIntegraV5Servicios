using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

// =============================================
// Repositorio: MatriculaConfiguracionComunicacionAsesorRepository
// Ubicación: BSI.Integra.Repositorio\Repository\Implementation\
// Autor: Miguel Valdivia
// Fecha: 20/11/2025
// =============================================

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// <summary>
    /// Repositorio para gestionar configuración de comunicación académica desde sistema asesor
    /// </summary>
    public class MatriculaConfiguracionComunicacionAsesorRepository : IMatriculaConfiguracionComunicacionAsesorRepository
    {
        protected internal readonly IConnectionFactory _connectionFactory;
        protected internal readonly IDapperRepository _dapperRepository;

        public MatriculaConfiguracionComunicacionAsesorRepository(
            IConnectionFactory connectionFactory,
            IDapperRepository dapperRepository)
        {
            _connectionFactory = connectionFactory;
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
                    new { IdAlumno = idAlumno }
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

                var resultado = _dapperRepository.QuerySPDapper(
                    "pla.SP_ObtenerCatalogoHorariosContacto",
                    null
                );

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

                var resultado = _dapperRepository.QuerySPDapper(
                    "pla.SP_ObtenerConfiguracionComunicacion",
                    new
                    {
                        IdMatriculaCabecera = idMatriculaCabecera,
                        IdPGeneral = idPGeneral
                    }
                );

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
                if (listaDatos == null || !listaDatos.Any())
                    return false;

                foreach (var dato in listaDatos)
                {
                    var resultado = _dapperRepository.QuerySPDapper(
                        "pla.SP_MatriculaConfiguracionComunicacion_Insertar",
                        new
                        {
                            IdMatriculaCabecera = idMatriculaCabecera,
                            IdPGeneral = idPGeneral,
                            IdHorarioContacto = dato.IdHorarioContacto,
                            MedioWhatsApp = dato.MedioWhatsApp,
                            MedioLlamada = dato.MedioLlamada,
                            MedioCorreo = dato.MedioCorreo,
                            Estado = dato.Estado,
                            Usuario = usuario
                        }
                    );
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
                if (listaDatos == null || !listaDatos.Any())
                    return false;

                foreach (var dato in listaDatos)
                {
                    var resultado = _dapperRepository.QuerySPDapper(
                        "pla.SP_MatriculaConfiguracionComunicacion_Actualizar",
                        new
                        {
                            Id = dato.Id,
                            IdMatriculaCabecera = dato.IdMatriculaCabecera,
                            IdPGeneral = dato.IdPGeneral,
                            IdHorarioContacto = dato.IdHorarioContacto,
                            MedioWhatsApp = dato.MedioWhatsApp,
                            MedioLlamada = dato.MedioLlamada,
                            MedioCorreo = dato.MedioCorreo,
                            Estado = dato.Estado,
                            Usuario = usuario
                        }
                    );
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
