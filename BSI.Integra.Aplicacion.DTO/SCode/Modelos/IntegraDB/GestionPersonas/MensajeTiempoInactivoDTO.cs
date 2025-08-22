using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas
{
    public class MensajeTiempoInactivoDTO
    {
        public int Id { get; set; }
        public int? MinutoInactivo { get; set; }
        public string? Mensaje { get; set; }
    }
}
