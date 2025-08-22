using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas
{
    public class WhatsAppMensajeRecibidoPostulanteDTO
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
    public class WhatsAppHistorialPostulanteMensajesDTO
    {
        public string Numero { get; set; }
        public int Tipo { get; set; }
        public string Mensaje { get; set; }
        public int? IdPersonal { get; set; }
        public int? IdPostulante { get; set; }
        public int IdPais { get; set; }
        public int? Registro { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string NombrePersonal { get; set; }
        public int? EstadoMensaje { get; set; }
        public DateTime? FechaEstado { get; set; }
    }


    public class WhatsAppMensajePostulantePlantillaComDTO
    {
        public string WaTo { get; set; }
        public string WaCaption { get; set; }
        public string WaBody { get; set; }
        public int WaTypeMensaje { get; set; }
        public int IdPlantilla { get; set; }
        public int IdPais { get; set; }
        public int IdPostulante { get; set; }
        public int? IdPersonal { get; set; }
        public string Usuario { get; set; }
        public List<DatosPlantillaWhatsAppDTO> DatosPlantillaWhatsApp { get; set; }
    }

    public class PlantillaWhatsAppPostulante
    {
        public string Descripcion { get; set; }
        public string Plantilla { get; set; } = "";
        public List<DatosPlantillaWhatsAppDTO> ListaEtiquetas { get; set; } = new List<DatosPlantillaWhatsAppDTO>();
    }

    public class WhatsAppMensajeTextoPostulanteDTO
    {
        public string WaTo { get; set; }
        public string WaBody { get; set; }
        public int IdPais { get; set; }
        public int IdPostulante { get; set; }
        public int? IdPersonal { get; set; }
        public string Usuario { get; set; }
    }

    public class WhatsAppMensajeArchivoPostulanteDTO
    {
        public string WaTo { get; set; }
        public string WaType { get; set; }
        public string WaLink { get; set; }
        public string WaFileName { get; set; }
        public int IdPais { get; set; }
        public int IdPostulante { get; set; }
        public int? IdPersonal { get; set; }
    }
}
