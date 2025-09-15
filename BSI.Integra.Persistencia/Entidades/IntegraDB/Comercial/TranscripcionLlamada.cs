using BSI.Integra.Aplicacion.Base;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Comercial
{
    public class TranscripcionLlamada : BaseIntegraEntity
    {
        public int IdLlamadaWebphoneCruceCentralTresCx { get; set; }
        public string Source { get; set; } = null!;
        public DateTime Timestamp { get; set; }
        public long? DurationInTicks { get; set; }
        public int? DurationMilliseconds { get; set; }
        public string? Duration { get; set; }
        public string? Summary { get; set; }
        public bool? OcurrenciaConsistente { get; set; }
        public string? ComentarioConsistenciaOcurrencia { get; set; }
        public List<FraseCombinada> FraseCombinada { get; set; }
        public List<FraseReconocida> FraseReconocida { get; set; }
        public List<RecomendacionTranscripcion> RecomendacionTranscripcions { get; set; }
    }
}
