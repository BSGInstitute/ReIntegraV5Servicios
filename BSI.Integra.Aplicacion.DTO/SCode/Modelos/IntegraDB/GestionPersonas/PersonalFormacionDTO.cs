using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas
{
    public class PersonalFormacionDTO
    {
        public bool? AlaActualidad { get; set; }
        public DateTime? FechaFin { get; set; }
        public DateTime? FechaInicio { get; set; }
        public int Id { get; set; }
        public int? IdAreaFormacion { get; set; }
        public int IdCentroEstudio { get; set; }
        public int? IdEstadoEstudio { get; set; }
        public int IdPersonal { get; set; }
        public int IdTipoEstudio { get; set; }
        public string Logro { get; set; }
        public int? IdPersonalArchivo { get; set; }
    }
}
