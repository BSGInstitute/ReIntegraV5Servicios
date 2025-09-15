using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class TransicionCalificacionFaseDTO
    {
        public int? Id { get; set; }
        public string Nombre { get; set; }
        public int IdFaseOportunidadOrigen { get; set; }
        public int IdFaseOportunidadDestino { get; set; }
        public List<CriterioCalificacionFaseOportunidadDTO>? CriterioCalificacionFaseOportunidad { get; set; }
    }

}