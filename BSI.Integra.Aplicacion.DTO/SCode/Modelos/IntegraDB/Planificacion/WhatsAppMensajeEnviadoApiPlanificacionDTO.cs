using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using System.Collections.Generic;

namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion
{
    /// Autor: Lolo Zaa
    /// Fecha: 06/03/2026
    /// <summary>
    /// DTOs para el envio de mensajes WhatsApp desde Planificacion.
    /// Patron basado en WhatsAppMensajeEnviadoApiComercialDTO (Comercial)
    /// pero usando IdProveedor en lugar de IdAlumno.
    /// </summary>
    public class WhatsAppMensajeEnviadoApiPlanificacionDTO
{
    public class WhatsAppMensajeTextoPlaDTO
    {
        public string WaTo { get; set; }
        public string WaBody { get; set; }
        public int IdPais { get; set; }
        public int IdProveedor { get; set; }
        public int? IdPersonal { get; set; }
    }

    public class WhatsAppMensajePlantillaPlaDTO
    {
        public string WaTo { get; set; }
        public string WaCaption { get; set; }
        public string WaBody { get; set; }
        public int WaTypeMensaje { get; set; }
        public int IdPlantilla { get; set; }
        public int IdPais { get; set; }
        public int IdProveedor { get; set; }
        public int? IdPersonal { get; set; }
        public List<DatosPlantillaWhatsAppDTO> DatosPlantillaWhatsApp { get; set; }
    }

    public class WhatsAppMensajeArchivoPlaDTO
    {
        public string WaTo { get; set; }
        public string WaType { get; set; }
        public string WaLink { get; set; }
        public string WaFileName { get; set; }
        public int IdPais { get; set; }
        public int IdProveedor { get; set; }
        public int? IdPersonal { get; set; }
    }

    public class WhatsAppEnviarMensajePlaDTO
    {
        public int Id { get; set; }
        public string WaTo { get; set; }
        public string WaId { get; set; }
        public string WaType { get; set; }
        public int WaTypeMensaje { get; set; }
        public string WaRecipientType { get; set; }
        public string WaBody { get; set; }
        public string WaFile { get; set; }
        public string WaFileName { get; set; }
        public string WaMimeType { get; set; }
        public string WaSha256 { get; set; }
        public string WaLink { get; set; }
        public string WaCaption { get; set; }
        public int IdPais { get; set; }
        public bool EsMigracion { get; set; }
        public int IdMigracion { get; set; }
        public int IdPersonal { get; set; }
        public int IdProveedor { get; set; }
        public string usuario { get; set; }
        public List<DatosPlantillaWhatsAppDTO> DatosPlantillaWhatsApp { get; set; }
        public List<BotonDTO> botones { get; set; }
        public string imagen { get; set; }
    }

    public class RespuestaMensajeWhatsappPlaDTO
    {
        public string Mensaje { get; set; }
        public bool Estado { get; set; }
    }
}
}
