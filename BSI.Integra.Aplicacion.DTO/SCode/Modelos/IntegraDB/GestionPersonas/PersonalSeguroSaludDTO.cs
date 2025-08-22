using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas
{
    public class PersonalSeguroSaludDTO
    {
        public int? IdEntidadSeguroSalud { get; set; }
        public bool EsModificado { get; set; }
        public bool Activo { get; set; }
        public int Id {  get; set; }
    }
}
