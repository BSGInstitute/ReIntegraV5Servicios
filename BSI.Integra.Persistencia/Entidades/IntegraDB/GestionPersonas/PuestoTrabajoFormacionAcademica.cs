using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas
{
    public class PuestoTrabajoFormacionAcademica : BaseIntegraEntity
    {
        public int IdPerfilPuestoTrabajo { get; set; }
        public string IdTipoFormacion { get; set; }
        public string IdNivelEstudio { get; set; }
        public string IdAreaFormacion { get; set; }
        public string IdCentroEstudio { get; set; }
        public string IdGradoEstudio { get; set; }
        public int? IdMigracion { get; set; }
    }
}
