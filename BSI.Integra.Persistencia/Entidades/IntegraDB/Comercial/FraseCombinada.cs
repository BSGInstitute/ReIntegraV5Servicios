using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Comercial
{
    public class FraseCombinada : BaseIntegraEntity
    {
        public int IdTranscripcionLlamada { get; set; }
        public int? Channel { get; set; }
        public string? Lexical { get; set; }
        public string? Itn { get; set; }
        public string? MaskedItn { get; set; }
        public string? Display { get; set; }
    }
}
