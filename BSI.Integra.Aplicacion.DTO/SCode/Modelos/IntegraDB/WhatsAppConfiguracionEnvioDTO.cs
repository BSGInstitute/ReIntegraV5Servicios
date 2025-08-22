using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class WhatsAppConfiguracionEnvioDTO : BaseIntegraEntity
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdPersonal { get; set; }
        public int IdPlantilla { get; set; }
        public int IdConjuntoListaDetalle { get; set; }
        public DateTime? FechaDesactivacion { get; set; }
        public bool Activo { get; set; }
        public int? IdCampaniaGeneralDetalle { get; set; }
        public int? IdMigracion { get; set; }
    }
}
