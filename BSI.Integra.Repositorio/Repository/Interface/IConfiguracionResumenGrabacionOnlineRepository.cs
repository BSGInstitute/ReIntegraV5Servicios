using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IConfiguracionResumenGrabacionOnlineRepository : IGenericRepository<TConfiguracionResumenGrabacionOnline>
    {
        IEnumerable<TConfiguracionResumenGrabacionOnline> Add(IEnumerable<ConfiguracionResumenGrabacionOnline> listadoEntidad);
        bool ActualizaActivo(IEnumerable<int> listadoIds, int IdPEspecificoSesion, string usuario);
        bool ActualizaInactivo(IEnumerable<int> listadoIds, int idPEspecificoSesion, string usuario);
        IEnumerable<ConfiguracionResumenGrabacionOnlineDTO> ObtenerConfiguracionResumenGrabacionOnlinePorSesion(int idPEspecificoSesion);
        IEnumerable<ConfiguracionResumenGrabacionesEnvioCorreoDTO> ObtenerConfiguracionResumenGrabacionesEnvioCorreo(int idPEspecificoSesion);
        IEnumerable<ConfiguracionResumenGrabacionesEnvioWhatsAppDTO> ObtenerConfiguracionResumenGrabacionesEnvioWhatsApp(int idPEspecificoSesion);
        IEnumerable<RegistroProcesamientoSesionOnlineDTO> ConsultaRegistroResumenPordPEspecificoSesion(int idPEspecificoSesion);
        bool EliminaProcesamientoSesionOnlinePorIdPEspecificoSesion(int id, string usuario);
        bool EliminaPProcesamientoTipoGenerarPorIdPEspecificoSesion(int idProcesamientoSesionOnline, string usuario);
        string ObtenerTextoTranscripcionPorId(int id);
        string ObtenerTextoGuionAudioPorId(int id);
    }
}
