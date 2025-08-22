using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class FeedbackGrupoPreguntaProgramaEspecificoDTO 
    {
        public int Id { get; set; }
        //public int IdFeedbackConfigurarGrupoPregunta { get; set; }
        public int IdProgramaGeneral { get; set; }
        public string Nombre { get; set; }
    }
}
