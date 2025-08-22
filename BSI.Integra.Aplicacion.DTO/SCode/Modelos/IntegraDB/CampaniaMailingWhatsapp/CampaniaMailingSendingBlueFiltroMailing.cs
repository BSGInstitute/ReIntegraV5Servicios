using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaMailingWhatsapp
{
    public class CampaniaMailingSendingBlueFiltroMailing
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string email { get; set; }
        public string Telefono { get; set; }
    }
    public class CampaniaWhatsAppFiltroWhatsApp
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string Telefono { get; set; }
    }
}
