using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class ControlSolicitudOrigen : BaseIntegraEntity
    {
        [StringLength(70)]
        public string Nombre { get; set; } = null!;
        [StringLength(500)]
        public string? Descripcion { get; set; }
    }
}
