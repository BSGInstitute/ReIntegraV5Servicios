using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Planificacion.SCode.Service.Interface
{
    /// <summary>
    /// Orquesta el polling de clasificacion de respuestas docentes:
    /// 1. Obtiene disparadores ejecutados desde BD (SP).
    /// 2. Por cada uno llama al servicio externo Python (localhost:8000).
    /// 3. Si el Python responde CLASIFICADO → marca la ocurrencia en BD.
    /// 4. Si responde ESPERANDO → no toca BD, el Job vuelve en 1 minuto.
    /// </summary>
    public interface IClasificacionRespuestaService
    {
        Task<List<DisparadorPendienteClasificacionDTO>> ObtenerDisparadoresPendientesAsync();
        Task<string> ProcesarDisparadorAsync(DisparadorPendienteClasificacionDTO disparador);
    }
}
