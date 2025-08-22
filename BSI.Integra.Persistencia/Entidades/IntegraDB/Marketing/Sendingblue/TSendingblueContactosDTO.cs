using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBSI.Integra.Persistencia.Entidades.IntegraDB.Sendingblue
{
    public class TSendingblueContactosDTO
    {
        public class CrearSendingblueContactos : BaseIntegraEntity
        {
            public long Id { get; set; }
            public string Email { get; set; } = null!;
           
            public bool ListaNegraCorreo { get; set; }
         
            public bool ListaNegroMensaje { get; set; }
     
            public string FechaCreacionSendinblue { get; set; } = null!;

            public string FechaModificacionSendinblue { get; set; } = null!;

            public string? IdLista { get; set; }

            public string Atributo { get; set; } = null!;
 
            public string Respuesta { get; set; } = null!;

            public bool EstadoGuardado { get; set; }
        }
    }
}
