using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp
{
    public class ListaAlumnoMailingDTO
    {
        public int IdAlumno { get; set; }
        public string NombreAlumno { get; set; }
        public string Email1 { get; set; }
        public string EmailCoordinador { get; set; }
    }
}
