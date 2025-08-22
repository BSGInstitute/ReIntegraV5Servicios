using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Operaciones.Service.Interface
{
    public interface IConfiguracionResumenGrabacionOnlineService
    {
        List<ConfiguracionResumenGrabacionOnline> Add(List<ConfiguracionResumenGrabacionOnline> listadoEntidad);
        bool ActualizaActivo(IEnumerable<int> listadoIds, int idPEspecificoSesion, string usuario);
        bool ActualizaInactivo(IEnumerable<int> listadoIds, int idPEspecificoSesion, string usuario);
        IEnumerable<ConfiguracionResumenGrabacionOnlineDTO> ObtenerConfiguracionResumenGrabacionOnlinePorSesion(int idPEspecificoSesion);
        Task<(string resultado, HttpStatusCode statusCode)> GenerarResumenGrabaciones(IniciarProcesoResumenGrabacionesDTO datos);
        Task<bool> EnvioResumenGrabaciones(int idPEspecificoSesion);
        IEnumerable<ConfiguracionResumenGrabacionesEnvioCorreoDTO> ObtenerConfiguracionResumenGrabacionesEnvioCorreo(int idPEspecificoSesion);
        IEnumerable<ConfiguracionResumenGrabacionesEnvioWhatsAppDTO> ObtenerConfiguracionResumenGrabacionesEnvioWhatsApp(int idPEspecificoSesion);
        IEnumerable<RegistroProcesamientoSesionOnlineDTO> ConsultaRegistroResumenPordPEspecificoSesion(int idPEspecificoSesion);
        bool EliminaProcesamientoSesionOnlinePorIdPEspecificoSesion(int id, string usuario);
        bool EliminaPProcesamientoTipoGenerarPorIdPEspecificoSesion(int idProcesamientoSesionOnline, string usuario);
        string ObtenerTextoTranscripcionPorId(int id);
        string ObtenerTextoGuionAudioPorId(int id);
    }
}
