using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp
{
    public class FiltradoDeDatosPorPrioridadWhatsAppDTO : BaseIntegraEntity
    {
        public int Id { get; set; }
        public string Movil { get; set; } = null!;
        public int IdAlumno { get; set; }
        public int? IdPais { get; set; }
        public int? IdAreaCapacitacion { get; set; }
        public int? IdProgramaGeneral { get; set; }
        public int? IdAreaFormacion { get; set; }
        public int? IdAreaTrabajo { get; set; }
        public int? IdCargo { get; set; }
        public int? IdIndustria { get; set; }
        public int? IdcampaniaGeneral { get; set; }
        public int? Prioridad { get; set; }
        public int IdCampaniaGeneralDetalle { get; set; }
        public bool EsValidoParaWhatsApp { get; set; }
    }
    public class HelperToSendWhatsAppApiValidatorDTO
    {
        public int idpais { get; set; }
        public string url { get; set; }
        public string ip { get; set; }
        public List<FiltradoDeDatosPorPrioridadWhatsAppDTO> DataParaValidar { get; set; }
    }
}
