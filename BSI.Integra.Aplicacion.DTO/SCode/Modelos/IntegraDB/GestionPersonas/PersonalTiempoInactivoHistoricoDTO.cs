using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas
{
    public class PersonalTiempoInactivoHistoricoDTO
    {
        public int Id { get; set; }
        public int IdMotivoInactividad { get; set; }
        public string MotivoInactividad { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public bool Estado { get; set; }
    }
}
