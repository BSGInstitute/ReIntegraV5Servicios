using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.SCode.Service.Interface;
using BSI.Integra.Aplicacion.Comercial.Service.Implementacion;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Comercial;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Comercial;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using System.Text.RegularExpressions;
using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// <summary>
    /// Servicio para el procesamiento consolidado de Speech de Bienvenida y Despedida
    /// Implementa el flujo completo: obtención de datos, filtrado por país, y reemplazo de etiquetas
    /// Autor: Joseph Llanque
    /// Fecha: 05/01/2026
    /// </summary>
    public class SpeechBienvenidaService : ISpeechBienvenidaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;


        public SpeechBienvenidaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

        }

        /// <summary>
        /// Procesa el speech de bienvenida y despedida de forma consolidada
        /// Reutiliza el método existente ObtenerPlantillaConvenioDeVoz y agrega procesamiento de etiquetas
        /// Flujo completo según documento MD:
        /// 1. Obtiene plantilla usando método existente en AgendaActividadService
        /// 2. Obtiene datos del alumno, personal y programa para reemplazo
        /// 3. Reemplaza todas las etiquetas con datos reales
        /// 4. Retorna speech procesado listo para usar
        /// </summary>
        public SpeechBienvenidaProcesadoDTO ObtenerSpeechBienvenidaProcesado(int idActividadDetalle)
        {
            try
            {
                // Validaciones
                if (idActividadDetalle <= 0)
                    throw new ArgumentException("El ID de actividad detalle debe ser mayor a 0", nameof(idActividadDetalle));

                // Paso 1: Obtener información básica de actividad detalle
                var actividadDetalle = _unitOfWork.ActividadDetalleRepository.ObtenerPorId(idActividadDetalle);
                if (actividadDetalle == null)
                    throw new InvalidOperationException($"No se encontró la actividad detalle con ID {idActividadDetalle}");

                var idOportunidad = actividadDetalle.IdOportunidad;

                // Obtener oportunidad para extraer fase
                var oportunidad = _unitOfWork.OportunidadRepository.ObtenerPorId(idOportunidad ?? 0);
                if (oportunidad == null)
                    throw new InvalidOperationException($"No se encontró la oportunidad con ID {idOportunidad}");

                var idFaseOportunidad = oportunidad.IdFaseOportunidad;

                // Paso 2: Obtener plantilla de Convenio de Voz usando método existente
                var agendaActividadService = new AgendaActividadService(_unitOfWork);

                var plantillaBienvenida = agendaActividadService.ObtenerPlantillaConvenioDeVoz(idFaseOportunidad, idActividadDetalle);

                if (plantillaBienvenida == null)
                    throw new InvalidOperationException($"No se encontró plantilla de bienvenida para actividad detalle {idActividadDetalle}");

                // Paso 3: Obtener plantilla de despedida (si existe)
                string templateDespedida = "";
                int idPlantillaDespedida = 0;
                try
                {
                    var plantillaBaseService = new PlantillaBaseService(_unitOfWork);
                    var idPlantillaBaseSpeechDespedida = 4; // Constante PlantillaBase.SpeechDespedida
                    var speechDespedidaDTO = plantillaBaseService.ObtenerIdPlantillaSpeechDespedida(idActividadDetalle, idPlantillaBaseSpeechDespedida);

                    if (speechDespedidaDTO != null && speechDespedidaDTO.IdPlantillaDespedida > 0)
                    {
                        idPlantillaDespedida = speechDespedidaDTO.IdPlantillaDespedida;
                        var plantillaClaveValorService = new PlantillaClaveValorService(_unitOfWork);
                        var plantillas = plantillaClaveValorService.ObtenerPlantillasPorIdFaseOportunidad(idFaseOportunidad);
                        var plantillaDespedida = plantillas.FirstOrDefault(p => p.IdPlantilla == idPlantillaDespedida);
                        templateDespedida = plantillaDespedida?.Valor ?? "";
                    }
                }
                catch
                {
                    // Si no se encuentra plantilla de despedida, continuar sin ella
                    templateDespedida = "";
                }

                // Paso 4: Obtener datos para reemplazo de etiquetas
                var datosReemplazo = ObtenerDatosReemplazo(idActividadDetalle);

                // Paso 5: Reemplazar etiquetas en templates
                var speechBienvenidaProcesado = ReemplazarEtiquetas(plantillaBienvenida.Valor ?? "", datosReemplazo);
                var speechDespedidaProcesado = !string.IsNullOrEmpty(templateDespedida)
                    ? ReemplazarEtiquetas(templateDespedida, datosReemplazo)
                    : "";

                // Paso 6: Retornar resultado
                return new SpeechBienvenidaProcesadoDTO
                {
                    SpeechBienvenida = speechBienvenidaProcesado,
                    SpeechDespedida = speechDespedidaProcesado,
                    IdPlantillaBienvenida = plantillaBienvenida.IdPlantilla,
                    IdPlantillaDespedida = idPlantillaDespedida,
                    IdFaseOportunidad = idFaseOportunidad,
                    IdCodigoPais = datosReemplazo.IdCodigoPais
                };
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al procesar speech de bienvenida para actividad detalle {idActividadDetalle}", ex);
            }
        }

        /// <summary>
        /// Reemplaza las etiquetas en un template de texto
        /// Maneja etiquetas de formato: {tTabla.Campo}
        /// Ejemplos: {tAlumnos.nombre1}, {tPersonal.nombres}, {tPLA_PGeneral.Nombre}
        /// </summary>
        public string ReemplazarEtiquetas(string template, DatosReemplazoEtiquetaDTO datosReemplazo)
        {
            if (string.IsNullOrWhiteSpace(template))
                return template;

            if (datosReemplazo == null)
                throw new ArgumentNullException(nameof(datosReemplazo));

            // Crear diccionario de reemplazos
            var diccionarioReemplazos = new Dictionary<string, string>
            {
                // Etiquetas de Alumno
                { "tAlumnos.nombre1", datosReemplazo.Nombre1 },
                { "tAlumnos.nombre2", datosReemplazo.Nombre2 },
                { "tAlumnos.apepaterno", datosReemplazo.ApellidoPaterno },
                { "tAlumnos.apematerno", datosReemplazo.ApellidoMaterno },
                { "tAlumnos.email1", datosReemplazo.Email1 },

                // Etiquetas de Personal
                { "tPersonal.nombres", datosReemplazo.NombresPersonal },
                { "tPersonal.apellidos", datosReemplazo.ApellidosPersonal },

                // Etiquetas de Programa
                { "tPLA_PGeneral.Nombre", datosReemplazo.ProgramaGeneral }
            };

            // Patrón para encontrar todas las etiquetas: {Cualquier.Cosa}
            var regex = new Regex(@"\{([^}]+)\}");
            var matches = regex.Matches(template);

            string resultado = template;

            foreach (Match match in matches)
            {
                var etiquetaCompleta = match.Value; // {tAlumnos.nombre1}
                var etiqueta = match.Groups[1].Value; // tAlumnos.nombre1

                string valorReemplazo = "";

                // Procesar según tipo de etiqueta
                if (etiqueta.Contains("TemplateV2.Duracion y Horarios"))
                {
                    // Etiqueta especial que se remueve
                    valorReemplazo = "";
                }
                else if (etiqueta.Contains("TemplateV2"))
                {
                    // Templates V2 - requiere consulta adicional (por ahora se deja vacío)
                    // TODO: Implementar lógica de TemplateV2 si es necesario
                    valorReemplazo = "";
                }
                else if (etiqueta.Contains("Template") && !etiqueta.Contains("V2"))
                {
                    // Templates antiguos (por ahora se deja vacío)
                    valorReemplazo = "";
                }
                else if (etiqueta.Contains("NoTabla.Lista"))
                {
                    // Etiqueta especial NoTabla.Lista (requiere datos adicionales)
                    valorReemplazo = "";
                }
                else
                {
                    // Buscar en diccionario de reemplazos estándar
                    diccionarioReemplazos.TryGetValue(etiqueta, out valorReemplazo);
                }

                // Reemplazar en el texto (manejar null)
                resultado = resultado.Replace(etiquetaCompleta, valorReemplazo ?? "");
            }

            // Limpiar posibles valores "undefined" o "null"
            resultado = resultado.Replace("undefined", "")
                                 .Replace("null", "");

            return resultado;
        }

        /// <summary>
        /// Obtiene y prepara todos los datos necesarios para el reemplazo de etiquetas
        /// Sigue el mismo patrón que ReporteSeguimientoOportunidades/ObtenerInformacionOportunidad
        /// para consolidar toda la información en un solo lugar
        /// </summary>
        public DatosReemplazoEtiquetaDTO ObtenerDatosReemplazo(int idActividadDetalle)
        {
            try
            {
                if (idActividadDetalle <= 0)
                    throw new ArgumentException("El ID de actividad detalle debe ser mayor a 0", nameof(idActividadDetalle));

                // Paso 1: Obtener actividad detalle y oportunidad
                var actividadDetalle = _unitOfWork.ActividadDetalleRepository.ObtenerPorId(idActividadDetalle);
                if (actividadDetalle == null)
                    throw new InvalidOperationException($"No se encontró la actividad detalle con ID {idActividadDetalle}");

                var idOportunidad = actividadDetalle.IdOportunidad;

                var oportunidadService = new OportunidadService(_unitOfWork);
                var oportunidad = oportunidadService.ObtenerPorId(idOportunidad ?? 0);
                if (oportunidad == null)
                    throw new InvalidOperationException($"No se encontró la oportunidad con ID {idOportunidad}");

                // Paso 2: Obtener alumno completo (más eficiente que por actividad detalle)
                var idClasificacionPersona = oportunidad.IdClasificacionPersona ?? 0;
                if (idClasificacionPersona == 0)
                    throw new InvalidOperationException($"La oportunidad {idOportunidad} no tiene clasificación de persona");

                var alumnoService = new AlumnoService(_unitOfWork);
                var alumno = alumnoService.ObtenerPorIdClasificacionPersona(idClasificacionPersona);
                if (alumno == null)
                    throw new InvalidOperationException($"No se encontró alumno con clasificación persona {idClasificacionPersona}");

                // Paso 3: Obtener personal asignado
                var personalService = new PersonalService(_unitOfWork);
                var idPersonalAsignado = oportunidad.IdPersonalAsignado ?? 0;
                string nombresPersonal = "";
                string apellidosPersonal = "";

                if (idPersonalAsignado > 0)
                {
                    var personal = personalService.ObtenerPorId(idPersonalAsignado);
                    if (personal != null)
                    {
                        nombresPersonal = personal.Nombres ?? "";
                        apellidosPersonal = personal.Apellidos ?? "";
                    }
                }

                // Paso 4: Obtener datos del programa
                var idCentroCosto = oportunidad.IdCentroCosto ?? 0;
                var pGeneralService = new PGeneralService(_unitOfWork);
                var cabeceraSpeech = pGeneralService.ObtenerCabeceraSpeechAgenda(idOportunidad ?? 0, idCentroCosto);

                // Paso 5: Retornar datos consolidados
                return new DatosReemplazoEtiquetaDTO
                {
                    // Datos del alumno
                    Nombre1 = alumno.Nombre1 ?? "",
                    Nombre2 = alumno.Nombre2 ?? "",
                    ApellidoPaterno = alumno.ApellidoPaterno ?? "",
                    ApellidoMaterno = alumno.ApellidoMaterno ?? "",
                    Email1 = alumno.Email1 ?? "",
                    IdCodigoPais = alumno.IdCodigoPais,

                    // Datos del personal
                    NombresPersonal = nombresPersonal,
                    ApellidosPersonal = apellidosPersonal,

                    // Datos del programa
                    ProgramaGeneral = cabeceraSpeech?.ProgramaGeneral ?? "",

                    // Datos adicionales
                    IdCentroCosto = idCentroCosto,
                    IdOportunidad = idOportunidad,
                    IdAlumno = alumno.Id
                };
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener datos de reemplazo para actividad detalle {idActividadDetalle}", ex);
            }
        }
    }
}
