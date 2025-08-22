using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB
{
    public class PrecioFinalProgramaAlumnoDTO
    {
        public int? IdCronograma { get; set; } = null;
        public double? SumatoriaMatriculas { get; set; } = null;
        public int? NroCuotas { get; set; } = null;
        public double? ValorMenorCuota { get; set; } = null;

    }
}
