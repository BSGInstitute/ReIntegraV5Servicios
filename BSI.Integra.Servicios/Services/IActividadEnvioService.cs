using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using System.Threading.Tasks;

namespace BSI.Integra.Servicios.Services
{
    /// <summary>
    /// Encapsula el flujo completo de envio de una actividad automatica congelada:
    /// resolucion del docente, generacion de plantilla y envio por canal (Email o WhatsApp).
    /// Vive en la capa de Servicios porque necesita acceder a BSI.Integra.Aplicacion.Marketing
    /// y BSI.Integra.Aplicacion.Transversal al mismo tiempo, lo cual no es posible desde
    /// BSI.Integra.Aplicacion.Planificacion debido a la dependencia circular con Marketing.
    /// </summary>
    public interface IActividadEnvioService
    {
        /// <summary>
        /// Ejecuta el envio de una actividad automatica (Email o WhatsApp).
        /// Resuelve el docente, genera la plantilla y envia por el canal
        /// correspondiente segun IdPlantillaBase (2 = Email, 8 = WhatsApp).
        /// </summary>
        /// <param name="actividad">Datos de la actividad pendiente a ejecutar</param>
        /// <param name="idPersonal">Id del personal remitente (asesor sistema o logueado)</param>
        /// <param name="usuario">Identificador del proceso que ejecuta ("HANGFIRE" o "MANUAL")</param>
        /// <returns>Mensaje descriptivo del resultado del envio</returns>
        Task<string> EnviarActividadAutomaticaAsync(ActividadPendienteDTO actividad, int idPersonal, string usuario);
    }
}
