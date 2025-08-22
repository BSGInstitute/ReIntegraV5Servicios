using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class OportunidadMarcadorDTO
    {
        public int IdOportunidad { get; set; }
        public int IdActividadDetalle { get; set; }

    }
    public class ValorOpcionalDecimalDTO
    {
        public decimal? Valor { get; set; }
    }
    public class ValorDecimalDTO
    {
        public decimal Valor { get; set; }
    }
    public class ValorDateTimeDTO
    {
        public DateTime Valor { get; set; }
    }
    public class ValorEnteroDTO
    {
        public int Valor { get; set; }
    }
}
