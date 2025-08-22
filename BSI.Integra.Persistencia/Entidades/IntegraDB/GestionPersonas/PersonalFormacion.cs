using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas
{
    public class PersonalFormacion :BaseIntegraEntity
    {
        public int IdPersonal { get; set; }
        public int IdCentroEstudio { get; set; }
        public int IdTipoEstudio { get; set; }
        public int? IdAreaFormacion { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public bool? AlaActualidad { get; set; }
        public int? IdEstadoEstudio { get; set; }
        public string Logro { get; set; }
        public int? IdPersonalArchivo { get; set; }
    }
}
