using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas
{
    public class WhatsAppMensajeRecibidoPostulante : BaseIntegraEntity
    {
        public int Id { get; set; }
        public string? WaFrom { get; set; }
        public string? WaId { get; set; }
        public string? WaTimeStamp { get; set; }
        public string? WaType { get; set; }
        public int? WaTypeMensaje { get; set; }
        public string? WaIdTypeMensaje { get; set; }
        public string? WaBody { get; set; }
        public string? WaFile { get; set; }
        public string? WaFileName { get; set; }
        public string? WaMimeType { get; set; }
        public string? WaSha256 { get; set; }
        public string? WaCaption { get; set; }
        public int IdPais { get; set; }
        public int IdPersonal { get; set; }
        public int? IdPostulante { get; set; }
    }
}
