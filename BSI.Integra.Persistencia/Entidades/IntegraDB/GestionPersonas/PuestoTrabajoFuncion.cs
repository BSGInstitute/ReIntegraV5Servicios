using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas
{
    public class PuestoTrabajoFuncion : BaseIntegraEntity
    {
        public int IdPerfilPuestoTrabajo { get; set; }
        public int NroOrden { get; set; }
        public string Nombre { get; set; }
        public int IdPersonalTipoFuncion { get; set; }
        public int IdFrecuenciaPuestoTrabajo { get; set; }
        public int? IdMigracion { get; set; }
    }
}
