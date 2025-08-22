using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class FeedbackConfigurarDTO
    {

            public int? Id { get; set; }
            public int IdFeedbackTipo { get; set; }
            public string Nombre { get; set; }

            public List<FeedbackConfigurarDetalleDTO> FeedbackConfigurarDetalles { get; set; }
        
    }
    public class FeedbackConfigurarFiltroDTO
    {
        public int Id { get; set; }
        public int IdFeedbackTipo { get; set; }
        public string NombreFeedbackTipo { get; set; }
        public string Nombre { get; set; }
    }
}
