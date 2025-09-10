using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BSI.Integra.Aplicacion.DTO.SCode.Modelos.Calidad.TranscriptionDTO;
using static BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Comercial.TranscriptionDTO;

namespace BSI.Integra.Aplicacion.Comercial.SCode.Service.Interface
{
    public interface ITranscriptionService 
    {
        Task InsertTranscriptionDataAsync(TranscriptionWebhookPayloadDTO payload);
    }
}
