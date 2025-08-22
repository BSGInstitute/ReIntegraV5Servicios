using BSI.Integra.Aplicacion.Base;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos
{
    public class PreWhatsAppResultadoConjuntoListaDTO : BaseIntegraEntity
    {
        public int IdWhatsappMensajePublicidad { get; set; }
        public int IdConjuntoListaResultado { get; set; }
        public int? IdPrioridadMailChimpListaCorreo { get; set; }
        public int IdAlumno { get; set; }
        public string Celular { get; set; }
        public int IdCodigoPais { get; set; }
        public int NroEjecucion { get; set; }
        public bool Validado { get; set; }
        public string Plantilla { get; set; }
        public int? IdPersonal { get; set; }
        public int? IdPgeneral { get; set; }
        public int? IdPlantilla { get; set; }
        public int? IdWhatsAppEstadoValidacion { get; set; }
        public int? IdWhatsAppConfiguracionEnvio { get; set; }
        public bool? Prevalidado { get; set; }
        public List<DatoPlantillaWhatsAppDTO> objetoplantilla { get; set; }
    }

    public class WhatsAppPrimeraListaCampaniaGeneralDTO : BaseIntegraEntity
    {
        public int IdPrioridadMailChimpListaCorreo { get; set; }
        public int IdAlumno { get; set; }
        public string Celular { get; set; }
        public int IdCodigoPais { get; set; }
        public int IdPgeneral { get; set; }
        public bool Prevalidado { get; set; }
    }

    public class WhatsAppResultadoCampaniaGeneralDTO : BaseIntegraEntity
    {
        public int IdAlumno { get; set; }
        public string Celular { get; set; }
        public bool Validado { get; set; }
        public string Plantilla { get; set; }
        public int IdCodigoPais { get; set; }
        public int IdPersonal { get; set; }
        public int IdWhatsappMensajePublicidad { get; set; }
        public int IdPrioridadMailChimpListaCorreo { get; set; }
        public List<DatoPlantillaWhatsAppDTO> ListaObjetoPlantilla { get; set; }
    }

    public class WhatsAppResultadoConjuntoListaDTO : BaseIntegraEntity
    {
        public int IdPre { get; set; }
        public int IdConjuntoListaResultado { get; set; }
        public int? IdPrioridadMailChimpListaCorreo { get; set; }
        public int IdAlumno { get; set; }
        public string Celular { get; set; }
        public int IdCodigoPais { get; set; }
        public int NroEjecucion { get; set; }
        public bool Validado { get; set; }
        public string Plantilla { get; set; }
        public int? IdPersonal { get; set; }
        public int? IdPgeneral { get; set; }
        public int? IdPlantilla { get; set; }
        public List<DatoPlantillaWhatsAppDTO> objetoplantilla { get; set; }
    }


    public class WhatsAppResultadoPostulanteDTO : BaseIntegraEntity
    {
        public int IdPostulante { get; set; }
        public string Celular { get; set; }
        public int IdCodigoPais { get; set; }
        public string Plantilla { get; set; }
        public int IdPersonal { get; set; }
    }
}
