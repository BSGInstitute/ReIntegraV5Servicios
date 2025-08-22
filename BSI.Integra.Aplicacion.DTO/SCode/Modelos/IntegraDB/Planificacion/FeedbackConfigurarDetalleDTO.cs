using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class FeedbackConfigurarDetalleDTO
    {
        public int? Id { get; set; }
        public int IdFeedbackConfigurar { get; set; }
        public int IdSexo { get; set; }
        public int Puntaje { get; set; }
        public string NombreVideo { get; set; }
  
        
    }
}
