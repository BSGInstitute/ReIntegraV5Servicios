using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class CodigoCiiuIndustria :BaseIntegraEntity
    {
        [StringLength(300)]
        public string Ciiu { get; set; } = null!;
        [StringLength(300)]
        public string Nombre { get; set; } = null!; 
        public int IdIndustria { get; set; }
    }
}
