using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class Solicitud : BaseIntegraEntity
    {
        [StringLength(50)]
        public string Nombre { get; set; } = null!;
        [StringLength(20)]
        public string Prioridad { get; set; } = null!;
        public int IdSolicitudSubCategoria { get; set; }
        public int IdPersonalRevision { get; set; }
        public int IdPersonalSolucion { get; set; }
    }
}
