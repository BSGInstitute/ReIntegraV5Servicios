using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Comercial
{

    public class LineamientoCalificacion : BaseIntegraEntity
    {

        public int IdCriterioCalificacionLlamada { get; set; }
        public int IdCriticidadCalificacion { get; set; }
        public string NombreLineamiento { get; set; } = null!;
        public int? Orden { get; set; }
        public string? Descripcion { get; set; }
        public string? HerramientaAnalisis { get; set; }
        public int? Version { get; set; }
        public bool? EsVigente { get; set; }
        public DateTime? FechaVigenciaInicio { get; set; }
        public DateTime? FechaVigenciaFin { get; set; }
    }
}
