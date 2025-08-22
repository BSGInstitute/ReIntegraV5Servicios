using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class PGeneralMontoPagoPanelDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdArea { get; set; }
        public int IdSubArea { get; set; }
        public int IdCategoria { get; set; }
    }
}
