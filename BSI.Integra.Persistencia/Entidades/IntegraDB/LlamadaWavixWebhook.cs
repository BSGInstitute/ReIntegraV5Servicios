using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class LlamadaWavixWebhook: BaseIntegraEntity
    {
        public string? Uuid { get; set; }
        public string? AnsweredBy { get; set; }
        public string? Charge { get; set; }
        public DateTime? Date { get; set; }
        public string? Destination { get; set; }
        public string? Disposition { get; set; }
        public int? Duration { get; set; }
        public string? From { get; set; }
        public string? PerMinute { get; set; }
        public string? RecordingUrl { get; set; }
        public string? SipTrunk { get; set; }
        public string? To { get; set; }
        public string? Transcription { get; set; }
        public string? Tipo { get; set; }

    }
}
