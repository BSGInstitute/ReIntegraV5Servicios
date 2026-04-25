using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

namespace BSI.Integra.Aplicacion.Comercial.Service.Interface
{
    /// <summary>
    /// Servicio para el procesamiento consolidado de Speech de Bienvenida y Despedida
    /// Implementa el flujo completo: obtención de datos, filtrado por país, y reemplazo de etiquetas
    /// Autor: Joseph Llanque
    /// Fecha: 05/01/2026
    /// </summary>
    public interface ISpeechBienvenidaService
    {
        /// <summary>
        /// Procesa el speech de bienvenida y despedida de forma consolidada
        /// Realiza todo el flujo:
        /// 1. Obtiene información de oportunidad y alumno
        /// 2. Obtiene cabecera del programa (speech header)
        /// 3. Obtiene plantillas por fase de oportunidad
        /// 4. Obtiene IDs de plantillas de bienvenida y despedida
        /// 5. Filtra la plantilla correcta considerando país del alumno
        /// 6. Reemplaza todas las etiquetas con datos reales
        /// 7. Retorna speech procesado listo para usar
        /// </summary>
        /// <param name="idActividadDetalle">ID de la actividad detalle</param>
        /// <returns>Speech de bienvenida y despedida procesado con etiquetas reemplazadas</returns>
        SpeechBienvenidaProcesadoDTO ObtenerSpeechBienvenidaProcesado(int idActividadDetalle);

        /// <summary>
        /// Reemplaza las etiquetas en un template de texto
        /// Maneja etiquetas de formato: {tTabla.Campo}
        /// Ejemplos: {tAlumnos.nombre1}, {tPersonal.nombres}, {tPLA_PGeneral.Nombre}
        /// </summary>
        /// <param name="template">Template con etiquetas a reemplazar</param>
        /// <param name="datosReemplazo">Datos para el reemplazo de etiquetas</param>
        /// <returns>Texto con etiquetas reemplazadas</returns>
        string ReemplazarEtiquetas(string template, DatosReemplazoEtiquetaDTO datosReemplazo);

        /// <summary>
        /// Obtiene y prepara todos los datos necesarios para el reemplazo de etiquetas
        /// a partir del ID de actividad detalle
        /// </summary>
        /// <param name="idActividadDetalle">ID de la actividad detalle</param>
        /// <returns>Datos consolidados para reemplazo de etiquetas</returns>
        DatosReemplazoEtiquetaDTO ObtenerDatosReemplazo(int idActividadDetalle);
    }
}
