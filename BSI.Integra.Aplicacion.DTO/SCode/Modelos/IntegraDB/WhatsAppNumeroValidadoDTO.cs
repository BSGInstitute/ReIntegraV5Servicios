using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class WhatsAppNumeroValidadoDTO
    {
        public int Id { get; set; }
        public int IdAlumno { get; set; }
        public string NumeroCelular { get; set; }
        public int IdPais { get; set; }
        public string Usuario { get; set; }
    }
}
