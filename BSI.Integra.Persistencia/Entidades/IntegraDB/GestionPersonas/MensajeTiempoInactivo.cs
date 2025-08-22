using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas
{
    public class MensajeTiempoInactivo : BaseIntegraEntity
    {
        public int Id { get; set; }
        public int? MinutoInactivo { get; set; }
        public string? Mensaje { get; set; }
    }
}
