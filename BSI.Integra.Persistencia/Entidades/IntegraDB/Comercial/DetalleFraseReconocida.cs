using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Comercial
{
    public class DetalleFraseReconocida : BaseIntegraEntity
    {
        public int IdFraseReconocida { get; set; }
        public decimal? Confidence { get; set; }
        public string? Lexical { get; set; }
        public string? Itn { get; set; }
        public string? MaskedItn { get; set; }
        public string? Display { get; set; }
        public string? Sentiment { get; set; }
    }
}
