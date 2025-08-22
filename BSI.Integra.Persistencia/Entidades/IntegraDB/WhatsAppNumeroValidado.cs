using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class WhatsAppNumeroValidado : BaseIntegraEntity
    {
        public int IdAlumno { get; set; }
        [StringLength(25)]
        public string NumeroCelular { get; set; } = null!; 
        public int IdPais { get; set; }
    }
}
