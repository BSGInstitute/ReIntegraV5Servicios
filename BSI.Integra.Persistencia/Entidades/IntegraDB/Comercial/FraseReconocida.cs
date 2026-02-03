using BSI.Integra.Aplicacion.Base;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Comercial
{
    public class FraseReconocida : BaseIntegraEntity
    {
        public int IdTranscripcionLlamada { get; set; }
        public string? RecognitionStatus { get; set; }
        public int? Channel { get; set; }
        public string? Speaker { get; set; }
        public string? Offset { get; set; }
        public string? Duration { get; set; }
        public long? OffsetInTicks { get; set; }
        public long? DurationInTicks { get; set; }
        public int? DurationMilliseconds { get; set; }
        public int? OffsetMilliseconds { get; set; }
        public List<DetalleFraseReconocida> DetalleFraseReconocida { get; set; }
    }
}
