using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Comercial
{
    public class CriterioCalificacionLlamada : BaseIntegraEntity
    {

        public int IdFaseCalificacion { get; set; }
        public string NombreCriterio { get; set; } = null!;
        public int? Orden { get; set; }
        public string? Descripcion { get; set; }
    }
}
