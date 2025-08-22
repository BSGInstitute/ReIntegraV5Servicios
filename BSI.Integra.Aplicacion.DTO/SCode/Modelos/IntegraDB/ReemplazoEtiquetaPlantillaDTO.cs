using BSI.Integra.Persistencia.Entidades.IntegraDB;
using Mandrill.Models;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ReemplazoEtiquetaPlantillaDTO
    {
        public int IdPlantilla { get; set; }
        public int IdPlantillaBase { get; set; }
        public int IdOportunidad { get; set; }
        public int Grupo { get; set; }
        public int IdMaterialPEspecificoDetalle { get; set; }
        public List<int> ListaIdMaterialPEspecificoDetalle { get; set; }
        public int IdPostulanteProcesoSeleccion { get; set; }
        public DateTime? FechaGP { get; set; }
        public Personal Personal { get; set; }
        public int IncrementoZonaHoraria { get; set; }
        public int FechaDinamicaRegularizar { get; set; }
        public string NombrePais { get; set; }
        public int? IdPEspecificoSesion { get; set; }
        public int? IdMatriculaCabecera { get; set; }
        public int IdProveedor { get; set; }
        public int IdPEspecificoWebinar { get; set; }
        public int IdPEspecifico { get; set; }
        public int? IdResumenGrabacionOnline { get; set; }
        public int? IdProcesamientoTipoGenerar { get; set; }
        
        public PlantillaEmailMandrillDTO? EmailReemplazado { get; set; } 

        public PlantillaWhatsAppCalculadoDTO? WhatsAppReemplazado { get; set; }
        public PlantillaSmsCalculadoDTO? SmsReemplazado { get; set; }
    }
   
}
