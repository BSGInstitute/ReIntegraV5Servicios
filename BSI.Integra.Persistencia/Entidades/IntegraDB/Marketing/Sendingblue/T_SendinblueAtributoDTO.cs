using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Sendingblue.IntegracionConIntegraDB
{
    public class T_SendinblueAtributoDTO
    {
        public class CrearSendinblueAtributo : BaseIntegraEntity
        {
            public int Id { get; set; }
            public string Nombre { get; set; } = null!;
            public string Valor { get; set; } = null!;
            public string Tipo { get; set; } = null!;
            public string Categoria { get; set; } = null!;
            public string Respuesta { get; set; } = null!;
            public bool EstadoGuardado { get; set; }

        }
    }
}
