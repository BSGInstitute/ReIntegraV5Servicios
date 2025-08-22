using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class BandejaPendientePw : BaseIntegraEntity
    {
        public int? IdDocumentoPw { get; set; }
        public int IdRevisionNivelPw { get; set; }
        public int Secuencia { get; set; }
        public int EsFinal { get; set; }
        public int EsInicio { get; set; }
        public int IdPersonal { get; set; }
        public int EstadoRevisar { get; set; }
        public string Comentario { get; set; }
        public string ComentarioRechazar { get; set; }
    }
}
