using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class PEspecificoConsumoDTO
    {
        public int IdCentroCosto { get; set; }
        public int IdPespecificoSesion { get; set; }
        public int IdPespecifico { get; set; }
        public int IdHistoricoProductoProveedor { get; set; }
        public int IdProducto { get; set; }
        public int IdProveedor { get; set; }
        public decimal Cantidad { get; set; }
        public string Factor { get; set; }
        public int? Semana { get; set; }
        public int AreaTrabajo { get; set; }
        public DateTime FechaHoraInicio { get; set; }
        public int Ciudad { get; set; }
        public int IdEmpresa { get; set; }
    }
}
