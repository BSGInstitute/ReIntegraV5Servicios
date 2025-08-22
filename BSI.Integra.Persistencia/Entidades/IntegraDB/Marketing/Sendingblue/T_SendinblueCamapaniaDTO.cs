using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Sendingblue.IntegracionConIntegraDB
{
    public class T_SendinblueCamapaniaDTO
    {
        public class CrearSendinblueCamapaniaDTO : BaseIntegraEntity
        {
            public int Id { get; set; }
            public string Nombre { get; set; } = null!;
            public string? ContenidoHtml { get; set; }
            public int? IdPlantilla { get; set; }
            public string? HoraProgramada { get; set; }
            public string? Asunto { get; set; }
            public string? Receptor { get; set; }
            public string? Campo { get; set; }
            public int? IdSendinblueRemitente { get; set; }
            public bool PruebaAb { get; set; }
            public string AsuntoA { get; set; } = null!;
            public string AsuntoB { get; set; } = null!;
            public int ReglaDivision { get; set; }
            public string GanadorCriterio { get; set; } = null!;
            public int GanadorTiempoAtraso { get; set; }
            public string Respuesta { get; set; } = null!;
            public bool EstadoGuardado { get; set; }
        }
    }
}
