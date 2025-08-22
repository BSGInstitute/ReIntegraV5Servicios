using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Sendingblue.IntegracionConIntegraDB
{
    public class T_SendingblueListaDTO
    {
        public class CrearSendingblueListaDTO : BaseIntegraEntity
        {
            public int Id { get; set; }
            public int IdSendinblueLista { get; set; }

            public string Nombre { get; set; } = null!;
            public int TotalSuscrito { get; set; }
            public int TotalExcluido { get; set; }
            public int UnicoSuscrito { get; set; }
            public int IdSendinblueCarpeta { get; set; }
            public string Respuesta { get; set; } = null!;
            public bool EstadoGuardado { get; set; }
        }
    }
}
