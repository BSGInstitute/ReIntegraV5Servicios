using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas
{
    public class PuestoTrabajoCaracteristicaPersonal : BaseIntegraEntity
    {
        public int IdPerfilPuestoTrabajo { get; set; }
        public int? EdadMinima { get; set; }
        public int? EdadMaxima { get; set; }
        public int IdSexo { get; set; }
        public int IdEstadoCivil { get; set; }
        public int? IdMigracion { get; set; }
    }
}
