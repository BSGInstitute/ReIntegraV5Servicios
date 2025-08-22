using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaMailingWhatsapp
{
    public class CampaniaMailingWhatsAppFiltradoDTO
    {
        public class CampaniaMailingFiltrado
        {
            public int IdcampaniaGeneral { get; set; }
            public string usuario { get; set; }
            public int IdFiltroSegmento { get; set; }
        }
        public class CampaniaWhatsAppFiltrado
        {
            public int IdcampaniaGeneral { get; set; }
            public string usuario { get; set; }
            public int cantidadDeDias { get; set; }
        }
    }
}
