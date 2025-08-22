using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Sendingblue.IntegracionConIntegraDB
{
    public class T_SendinblueRelacionAlmunoSBDTO
    {
        public class CrearSendinblueRelacionAlmunoSB : BaseIntegraEntity
        {
            public int Id { get; set; }
            public int IdAlumno { get; set; }
            public int IdSendinblue { get; set; }
        }
    }
}
