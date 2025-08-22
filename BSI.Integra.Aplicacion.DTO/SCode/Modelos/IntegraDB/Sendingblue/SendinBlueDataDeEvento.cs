using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue
{
    public class SendinBlueDataDeEventoDTO
    {
        public int Id { get; set; }
        public int IdSendinBlueCampania { get; set; }
        public string EmailContacto { get; set; } = null!;
        public int IdSendinBlueEventoWebHook { get; set; }
        public DateTime FechaEnvio { get; set; }
        public DateTime FechaDeEvento { get; set; }
        public string? UrlEvento { get; set; }
        public string JsonResponse { get; set; } = null!;
    }
    public class SendinBlueDataDeEvento : BaseIntegraEntity
    {
        public int Id { get; set; }
        public int IdSendinBlueCampania { get; set; }
        public string EmailContacto { get; set; } = null!;
        public int IdSendinBlueEventoWebHook { get; set; }
        public DateTime FechaEnvio { get; set; }
        public DateTime FechaDeEvento { get; set; }
        public string? UrlEvento { get; set; }
        public string JsonResponse { get; set; } = null!;
    }
}
