using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class LineamientoCalificacionFase : BaseIntegraEntity
    {
        public int IdCriterioCalificacionFaseOportunidad { get; set; }
        public int Orden { get; set; }
        public int IdCriticidadCalificacion { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
    }

}