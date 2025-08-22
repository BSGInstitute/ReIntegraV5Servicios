using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ReporteEstadoCuentaProveedorFiltroDTO
    {
        public string? Empresa { get; set; }
        public bool? Estado { get; set; }
        public string? Ciudad { get; set; }
        public int? Proveedor { get; set; }
        public string? CuentaContable { get; set; }
        public string? Comprobante { get; set; }
        public string? FechaInicio { get; set; }
        public string? FechaFin { get; set; }
    }
}
