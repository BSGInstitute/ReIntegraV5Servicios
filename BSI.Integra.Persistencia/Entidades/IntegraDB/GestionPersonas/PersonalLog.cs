using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas
{
    public class PersonalLog :BaseIntegraEntity
    {
        public int IdPersonal { get; set; }
        public string Rol { get; set; }
        public string TipoPersonal { get; set; }
        public int? IdJefe { get; set; }
        public bool EstadoRol { get; set; }
        public bool Estado { get; set; }
        public bool EstadoTipoPersonal { get; set; }
        public bool EstadoIdJefe { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int? IdMigracion { get; set; }
        public int? IdCerrador { get; set; }
        public bool? EsCerrador { get; set; }
        public bool? EstadoCerrador { get; set; }
        public int? IdPuestoTrabajoNivel { get; set; }

    }
}
