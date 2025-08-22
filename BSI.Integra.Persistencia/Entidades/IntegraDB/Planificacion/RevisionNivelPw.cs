using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class RevisionNivelPw : BaseIntegraEntity
    {
        public string Nombre { get; set; } = null!;
        public int Prioridad { get; set; }
        public int IdTipoRevisionPw { get; set; }
        public int IdRevisionPw { get; set; }
    }
}
