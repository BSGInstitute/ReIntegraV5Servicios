using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class PEspecificoSesionEstadoObservacionDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public int IdPEspecificoSesionEstado { get; set; }
    }
}
