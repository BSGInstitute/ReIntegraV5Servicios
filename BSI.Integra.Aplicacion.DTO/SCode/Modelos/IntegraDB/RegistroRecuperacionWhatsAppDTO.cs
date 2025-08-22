using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class RegistroRecuperacionWhatsAppDTO : BaseIntegraEntity
    {
        ///Propiedades		                            Significado
        ///-------------	                            -----------------------
        ///IdCampaniaGeneral							Id de la campania general (PK de la tabla mkt.T_CampaniaGeneral)
        ///IdCampaniaGeneralDetalle					    Id del detalle de la campania general (PK de la tabla mkt.T_CampaniaGeneralDetalle)
        ///IdCampaniaGeneralDetalleResponsable			Id del detalle de responsable de la campania general (PK de la tabla mkt.T_CampaniaGeneralDetalleResponsable)
        ///IdPersonal									Id del personal (PK de la tabla gp.T_Personal)
        ///IdPlantilla									Id de la plantilla (PK de la tabla mkt.T_Plantilla)
        ///IdWhatsAppConfiguracionEnvio				    Id de la configuracion de envio de WhatsApp (PK de la tabla mkt.T_WhatsAppConfiguracionEnvio)
        ///Dia											Dia actual
        ///Dia1										    Cantidad del dia 01
        ///Dia2										    Cantidad del dia 02
        ///Dia3										    Cantidad del dia 03
        ///Dia4										    Cantidad del dia 04
        ///Dia5										    Cantidad del dia 05
        ///FechaInicioEnvioWhatsapp					    Fecha de inicio del envio de WhatsApp
        ///FechaFinEnvioWhatsapp						Fecha de fin del envio de WhatsApp
        ///HoraEnvio									Hora de envio de WhatsApp
        ///Completado									Flag para determinar si el envio se ha completado en su totalidad
        ///IdMigracion									Id de la migracion (campo nullable)

        public int IdCampaniaGeneral { get; set; }
        public int IdCampaniaGeneralDetalle { get; set; }
        public int IdCampaniaGeneralDetalleResponsable { get; set; }
        public int IdPersonal { get; set; }
        public int IdPlantilla { get; set; }
        public int IdWhatsAppConfiguracionEnvio { get; set; }
        public int Dia { get; set; }
        public int Dia1 { get; set; }
        public int Dia2 { get; set; }
        public int Dia3 { get; set; }
        public int Dia4 { get; set; }
        public int Dia5 { get; set; }
        public DateTime FechaInicioEnvioWhatsapp { get; set; }
        public DateTime FechaFinEnvioWhatsapp { get; set; }
        public TimeSpan HoraEnvio { get; set; }
        public bool Completado { get; set; }
        public bool? Fallido { get; set; }
        public bool? RecuperacionEnProceso { get; set; }
        public int? IdMigracion { get; set; }
    }
}
