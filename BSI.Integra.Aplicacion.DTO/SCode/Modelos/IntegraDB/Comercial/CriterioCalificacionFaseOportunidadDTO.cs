using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{

    public class CriterioCalificacionFaseOportunidadDTO
    {
        public int? Id { get; set; }
        public int IdTransicionCalificacionFase { get; set; }
        public int Orden { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public List<LineamientoCalificacionFaseDTO>? LineamientoCalificacionFase { get; set; }
    }
}