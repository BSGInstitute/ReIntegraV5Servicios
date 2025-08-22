using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class WhatsAppDesuscrito : BaseIntegraEntity
    {
        [StringLength(100)]
        public string? NumeroTelefono { get; set; }
        [StringLength(250)]
        public string? Descripcion { get; set; } 
        public bool? EsActivo { get; set; } 
        public bool? EsMigracion { get; set; }
    }
}
