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
        public string? Nombre { get; set; } = null!;
        public int IdPEspecificoSesionEstadoObservacion { get; set; }
        public int Orden { get; set; }
    }

    public class PEspecificoSesionEstadoObservacionQueryDTO
    {
        public int Id { get; set; }
        public string Descripcion { get; set; } = null!;
        public int IdPEspecificoSesionEstado { get; set; }

        public int? DetalleId { get; set; }
        public string? DetalleNombre { get; set; }
        public int? IdPEspecificoSesionEstadoObservacion { get; set; }
        public int? Orden { get; set; }
    }
}
