using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Comercial
{
    public class RecomendacionTranscripcion : BaseIntegraEntity
    {
        public int IdTranscripcionLlamada { get; set; }
        public string Recomendacion { get; set; } = null!;
    }
}
