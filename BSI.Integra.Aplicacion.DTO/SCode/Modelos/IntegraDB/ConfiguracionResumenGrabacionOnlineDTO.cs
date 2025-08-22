using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ConfiguracionResumenGrabacionOnlineDTO
    {
        public int Id { get; set; }
        public int IdPEspecificoSesion { get; set; }
        public int IdResumenGrabacionOnline { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
    }

    public class EliminarConfiguracionResumenGrabacionOnlineDTO
    {
        public List<int> ListadoIds { get; set; }
        public string Usuario { get; set; }
    }

    public class ConfiguracionResumenGrabacionOnlineEntradaDTO
    {
        public int IdPEspecificoSesion { get; set; }
        public int IdResumenGrabacionOnline { get; set; }
        public bool Estado { get; set; }
        public string Usuario { get; set; }
    }

    public class IniciarProcesoResumenGrabacionesDTO
    {
        public int IdPEspecifico { get; set; }
        public int IdPEspecificoSesion { get; set; }
        public List<int> TipoResumenGrabacionOnline { get; set; }
        public string Sesion { get; set; }
        public string UrlVideo { get; set; }
        public string Usuario { get; set; }
    }

    public class ConfiguracionResumenGrabacionesEnvioCorreoDTO
    {
        public int IdMatriculaCabecera { get; set; }
        public string NombreEspecifico { get; set; }
        public string NombreGeneral { get; set; }
        public string NombreResumen { get; set; }
        public string Nombre1 { get; set; }
        public string Correo { get; set; }
        public string RegistroUrl { get; set; }
        public string NombreAsesorCompleto { get; set; }
        public string RolAsesor { get; set; }
    }

    public class ConfiguracionResumenGrabacionesEnvioWhatsAppDTO
    {
        public int IdOportunidad { get; set; }
        public int IdResumenGrabacionOnline { get; set; }
        public int IdProcesamientoTipoGenerar { get; set; }
        public int IdAlumno { get; set; }
        public int IdCodigoPais { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public string NombreEspecifico { get; set; }
        public string NombreResumen { get; set; }
        public string Celular { get; set; }
        public string RegistroUrl { get; set; }
    }

    public class RegistroProcesamientoSesionOnlineDTO
    {
        public int Id { get; set; }
    }

    public class TextoTranscripcionDTO
    {
        public string TextoTranscripcion { get; set; }
    }

    public class TextoGuionAudioDTO
    {
        public string TextoGuionAudio { get; set; }
    }

}
