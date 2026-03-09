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
        public string Descripcion { get; set; } = null!;
        public int IdPEspecificoSesionEstado { get; set; }
        public List<PEspecificoSesionEstadoObservacionDetalleDTO> Observaciones { get; set; } = new();
    }

    public class PEspecificoSesionEstadoObservacionDetalleDTO
    {
        public int Id { get; set; }
        public string Contenido { get; set; } = null!;
        public int Orden { get; set; }
    }
}
