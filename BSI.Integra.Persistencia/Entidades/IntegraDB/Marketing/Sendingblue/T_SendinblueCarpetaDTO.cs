using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Sendingblue.IntegracionConIntegraDB
{
    public class T_SendinblueCarpetaDTO
    {
        public class CrearSendinblueCarpeta : BaseIntegraEntity
        {
            public int Id { get; set; }
            public string Nombre { get; set; } = null!;
            public int ListaNegra { get; set; }
            public int IdListaSendinblue { get; set; }

            public int TotalSuscrito { get; set; }
            public int SuscritoUnico { get; set; }
            public string Respuesta { get; set; } = null!;
            public bool EstadoGuardado { get; set; }
        }
    }
}
