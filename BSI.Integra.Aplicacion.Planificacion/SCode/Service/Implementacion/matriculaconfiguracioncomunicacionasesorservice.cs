using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Repositorio.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

// =============================================
// Service: MatriculaConfiguracionComunicacionAsesorService
// Ubicación: BSI.Integra.Aplicacion.Planificacion\SCode\Service\Implementacion\
// Autor: Miguel Valdivia
// Fecha: 20/11/2025
// =============================================

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    /// Service: MatriculaConfiguracionComunicacionAsesorService
    /// Autor: Miguel Valdivia
    /// Fecha: 20/11/2025
    /// Versión: 1.0
    /// <summary>
    /// Gestión de configuración de comunicación académica desde sistema asesor
    /// </summary>
    public class MatriculaConfiguracionComunicacionAsesorService : IMatriculaConfiguracionComunicacionAsesorService
    {
        private readonly IMatriculaConfiguracionComunicacionAsesorRepository _repository;

        public MatriculaConfiguracionComunicacionAsesorService(
            IMatriculaConfiguracionComunicacionAsesorRepository repository)
        {
            _repository = repository;
        }

        /// Autor: Miguel Valdivia
        /// Fecha: 20/11/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el catálogo de horarios de contacto disponibles
        /// </summary>
        /// <returns>Lista de horarios</returns>
        public IEnumerable<CatalogoHorarioContactoAsesorDTO> ObtenerCatalogoHorariosContacto()
        {
            try
            {
                return _repository.ObtenerCatalogoHorariosContacto();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Miguel Valdivia
        /// Fecha: 20/11/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la configuración de comunicación de un alumno usando su IdAlumno
        /// </summary>
        /// <param name="idAlumno">ID del alumno</param>
        /// <returns>ResponseConfiguracionComunicacionAsesorDTO con los datos o error</returns>
        public ResponseConfiguracionComunicacionAsesorDTO ObtenerConfiguracionPorAlumno(int idAlumno)
        {
            try
            {
                // 1. Obtener TODAS las matrículas activas del alumno
                var matriculasActivas = _repository.ObtenerMatriculasActivasPorAlumno(idAlumno).ToList();

                if (matriculasActivas == null || !matriculasActivas.Any())
                {
                    return new ResponseConfiguracionComunicacionAsesorDTO
                    {
                        Success = false,
                        Message = "El alumno no tiene matrículas activas.",
                        Datos = null
                    };
                }

                // 2. Buscar configuración en CUALQUIERA de las matrículas
                // (Si existe, debería ser la misma en todas, así que tomamos la primera que tenga datos)
                List<ConfiguracionComunicacionAsesorDTO> configuraciones = new List<ConfiguracionComunicacionAsesorDTO>();

                foreach (var matricula in matriculasActivas)
                {
                    var configMatricula = _repository.ObtenerConfiguracionComunicacion(
                        matricula.IdMatriculaCabecera,
                        matricula.IdPGeneral
                    ).ToList();

                    if (configMatricula != null && configMatricula.Any())
                    {
                        configuraciones = configMatricula;
                        break; 
                    }
                }

                return new ResponseConfiguracionComunicacionAsesorDTO
                {
                    Success = true,
                    Message = "Configuración obtenida exitosamente.",
                    Datos = new
                    {
                        Matriculas = matriculasActivas,
                        Configuraciones = configuraciones
                    }
                };
            }
            catch (Exception ex)
            {
                return new ResponseConfiguracionComunicacionAsesorDTO
                {
                    Success = false,
                    Message = $"Error al obtener configuración: {ex.Message}",
                    Datos = null
                };
            }
        }

        /// Autor: Miguel Valdivia
        /// Fecha: 20/11/2025
        /// Versión: 1.0
        /// <summary>
        /// Guarda la configuración de comunicación de un alumno en TODAS sus matrículas activas
        /// Inserta o actualiza según corresponda
        /// </summary>
        /// <param name="idAlumno">ID del alumno</param>
        /// <param name="listaDatos">Lista de configuraciones</param>
        /// <param name="usuario">Usuario asesor que realiza la acción</param>
        /// <returns>ResponseConfiguracionComunicacionAsesorDTO con resultado</returns>
        public ResponseConfiguracionComunicacionAsesorDTO GuardarConfiguracionPorAlumno(
            int idAlumno,
            List<GuardarConfiguracionComunicacionAsesorDTO> listaDatos,
            string usuario)
        {
            try
            {
                // 1. Validar que haya datos
                if (listaDatos == null || !listaDatos.Any())
                {
                    return new ResponseConfiguracionComunicacionAsesorDTO
                    {
                        Success = false,
                        Message = "La lista de configuraciones está vacía.",
                        Datos = null
                    };
                }

                // 2. Obtener TODAS las matrículas activas del alumno
                var matriculasActivas = _repository.ObtenerMatriculasActivasPorAlumno(idAlumno).ToList();

                if (matriculasActivas == null || !matriculasActivas.Any())
                {
                    return new ResponseConfiguracionComunicacionAsesorDTO
                    {
                        Success = false,
                        Message = "El alumno no tiene matrículas activas.",
                        Datos = null
                    };
                }

                // 3. Validación MAÑANA/TARDE
                var horariosMañana = listaDatos.Where(h =>
                    h.IdHorarioContacto >= 1 &&
                    h.IdHorarioContacto <= 4 &&
                    h.Estado == true
                ).ToList();

                var horariosTarde = listaDatos.Where(h =>
                    h.IdHorarioContacto >= 5 &&
                    h.IdHorarioContacto <= 9 &&
                    h.Estado == true
                ).ToList();

                if (horariosMañana.Any() && horariosTarde.Any())
                {
                    return new ResponseConfiguracionComunicacionAsesorDTO
                    {
                        Success = false,
                        Message = "No se pueden seleccionar horarios de mañana y tarde simultáneamente.",
                        Datos = null
                    };
                }

                // 4. GUARDAR EN TODAS LAS MATRÍCULAS ACTIVAS
                int matriculasActualizadas = 0;
                int matriculasInsertadas = 0;

                foreach (var matricula in matriculasActivas)
                {
                    // Verificar si ya existe configuración para esta matrícula
                    var configuracionExistente = _repository.ObtenerConfiguracionComunicacion(
                        matricula.IdMatriculaCabecera,
                        matricula.IdPGeneral
                    ).ToList();

                    bool resultado;

                    if (configuracionExistente != null && configuracionExistente.Any())
                    {
                        // ACTUALIZAR
                        var datosParaActualizar = configuracionExistente.Select(cfg =>
                        {
                            var datoNuevo = listaDatos.FirstOrDefault(d => d.IdHorarioContacto == cfg.IdHorarioContacto);
                            if (datoNuevo != null)
                            {
                                cfg.MedioWhatsApp = datoNuevo.MedioWhatsApp;
                                cfg.MedioLlamada = datoNuevo.MedioLlamada;
                                cfg.MedioCorreo = datoNuevo.MedioCorreo;
                                cfg.Estado = datoNuevo.Estado;
                            }
                            return cfg;
                        }).ToList();

                        resultado = _repository.ActualizarConfiguracionComunicacion(datosParaActualizar, usuario);
                        if (resultado) matriculasActualizadas++;
                    }
                    else
                    {
                        // INSERTAR
                        resultado = _repository.InsertarConfiguracionComunicacion(
                            matricula.IdMatriculaCabecera,
                            matricula.IdPGeneral,
                            listaDatos,
                            usuario
                        );
                        if (resultado) matriculasInsertadas++;
                    }
                }

                return new ResponseConfiguracionComunicacionAsesorDTO
                {
                    Success = true,
                    Message = $"Configuración guardada exitosamente en {matriculasActivas.Count} matrícula(s) activa(s).",
                    Datos = new
                    {
                        TotalMatriculas = matriculasActivas.Count,
                        MatriculasActualizadas = matriculasActualizadas,
                        MatriculasInsertadas = matriculasInsertadas
                    }
                };
            }
            catch (Exception ex)
            {
                return new ResponseConfiguracionComunicacionAsesorDTO
                {
                    Success = false,
                    Message = $"Error al guardar configuración: {ex.Message}",
                    Datos = null
                };
            }
        }
    }
}