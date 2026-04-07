using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class SolicitudSubCategoria : BaseIntegraEntity
    {
        [StringLength(50)]
        public string Nombre { get; set; } = null!;
        public int? IdSolicitudCategoria { get; set; }
        public string? Titulo { get; set; }
    }
}
