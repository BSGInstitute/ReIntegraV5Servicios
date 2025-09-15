using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class LineamientoCalificacionFaseDTO
    {
        public int? Id { get; set; }
        public int IdCriterioAnalisisCambioFase { get; set; }
        public int Orden { get; set; }
        public int IdCriticidadLineamiento { get; set; }
        public string NombreLineamiento { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string Usuario { get; set; } = null!;
    }
}