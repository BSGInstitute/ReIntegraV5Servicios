using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas
{
    public class PersonalHistorialMedicoDTO
    {
        public string DetalleEnfermedad { get; set; }
        public string Enfermedad { get; set; }
        public int Id { get; set; }
        public int IdPersonal { get; set; }
        public string? Periodo { get; set; }
    }
}
