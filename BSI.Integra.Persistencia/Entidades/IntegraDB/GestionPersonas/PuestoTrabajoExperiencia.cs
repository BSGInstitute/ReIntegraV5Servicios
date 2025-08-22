using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas
{
    public class PuestoTrabajoExperiencia : BaseIntegraEntity
    {
        public int IdPerfilPuestoTrabajo { get; set; }
        public int IdExperiencia { get; set; }
        public int IdTipoExperiencia { get; set; }
        public int NumeroMinimo { get; set; }
        public string Periodo { get; set; }
        public int? IdMigracion { get; set; }
    }
}
