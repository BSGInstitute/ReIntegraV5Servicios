using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    internal class TipoPagoDTO
    {
    }
    public class TipoPagoComboDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Cuotas { get; set; }
    }
}
