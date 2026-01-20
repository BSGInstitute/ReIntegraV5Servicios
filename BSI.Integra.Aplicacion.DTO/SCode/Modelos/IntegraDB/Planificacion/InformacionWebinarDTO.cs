using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    internal class InformacionWebinarDTO
    {
    }
    public class WebinarDetalleSesionDTO
    {
        public int IdPEspecificoSesion { get; set; }
        public string EstadoSesion { get; set; }
        public DateTime Fecha { get; set; }
        public string Hora { get; set; }
        public string NombrePrograma { get; set; }
        public string NombreWebinar { get; set; }
        public string EsWebinarConfirmado { get; set; }
        public string EsCancelado { get; set; }
        public int IdCentroCosto { get; set; }
        public string CentroCosto { get; set; }
        public string Configuracion { get; set; }
        public string EstadoEnvioCorreo { get; set; }
        public string EstadoEnvioWhatsApp { get; set; }
        public int TotalParticipantes { get; set; }
        public int TotalParticipantesConfirmados { get; set; }
    }
    public class WebinarReporteFiltroDTO
    {
        public List<int>? ListaPGeneral { get; set; }
        public List<int>? ListaPEspecifico { get; set; }
        public string? EstadoSesion { get; set; }
        public DateTime? Fecha { get; set; } 
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string? FechaPorDefecto { get; set; }
        public string? CodigoMatricula { get; set; }
        public int? IdCentroCosto { get; set; }

    }
}
