using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas
{
    public class PersonalCeseDTO
    {

        public int? Id { get; set; }

        public int? IdMotivoCese { get; set; }
        public DateTime FechaCese { get; set; }

    }
    public class DatosPersonalCeseDTO
    {
        public int? IdContratoEstado { get; set; }
        public int? IdMotivoCese { get; set; }
        public DateTime? FechaCese { get; set; }
        public bool EsModificado { get; set; }
    }
}
