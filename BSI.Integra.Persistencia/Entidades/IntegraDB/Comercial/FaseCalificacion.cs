using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Comercial
{
    public class FaseCalificacion : BaseIntegraEntity
    {

        public string Nombre { get; set; } = null!;
        public int? Orden { get; set; }
        public string? Descripcion { get; set; }
    }
}
