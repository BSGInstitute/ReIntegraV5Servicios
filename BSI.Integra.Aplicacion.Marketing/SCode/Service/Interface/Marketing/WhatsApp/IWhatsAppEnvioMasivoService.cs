using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Marketing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Marketing.Service.Interface.Marketing.WhatsApp
{
    public interface IWhatsAppEnvioMasivoService
    {
        /// <summary>
        /// Obtiene el historial de oportunidades de un alumno
        /// mediante el SP mkt.SP_OportunidadHistorialPorAlumno.
        /// </summary>
        List<HistorialOportunidadMasivoDTO> ObtenerHistorialOportunidadesPorAlumno(int idAlumno);

        /// <summary>
        /// Inicia una calificacion batch V2 con perfil_lead e historial_oportunidades.
        /// </summary>
        Task<string> IniciarCalificacionBatchV2(CalificacionBatchV2RequestDTO request);

        /// <summary>
        /// Consulta el estado de una calificacion batch V2 por su llamadaId.
        /// </summary>
        Task<string> ObtenerEstadoCalificacionV2(string llamadaId);

        /// <summary>
        /// Obtiene los resultados de una calificacion batch V2 por su llamadaId.
        /// </summary>
        Task<string> ObtenerResultadosCalificacionV2(string llamadaId);
    }
}
