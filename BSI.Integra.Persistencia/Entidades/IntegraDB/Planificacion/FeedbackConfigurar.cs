using BSI.Integra.Aplicacion.Base;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class FeedbackConfigurar : BaseIntegraEntity
    {
        public int IdFeedbackTipo { get; set; }
        public string Nombre { get; set; }


        public List<FeedbackConfigurarDetalle> FeedbackConfigurarDetalles { get; set; }    
    }
}
