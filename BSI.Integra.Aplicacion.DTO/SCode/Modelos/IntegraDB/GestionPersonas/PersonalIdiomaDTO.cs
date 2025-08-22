using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas
{
    public  class PersonalIdiomaDTO
    {
        public int Id { get; set; }
        public int IdCentroEstudio { get; set; }
        public int IdIdioma { get; set; }
        public int IdNivelIdioma { get; set; }
        public int IdPersonal { get; set; }
        public int? IdPersonalArchivo { get; set; }
    }
}
