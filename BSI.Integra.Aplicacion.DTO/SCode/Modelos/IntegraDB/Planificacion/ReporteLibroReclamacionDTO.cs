using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class ReporteLibroReclamacionDTO
    {
        public int Id { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime FechaResponder { get; set; }
        public string Nombre { get; set; }
        public string Domicilio { get; set; }
        public string Dni { get; set; }
        public string Celular { get; set; }
        public string CorreoElectronico { get; set; }
        public string TipoServicio { get; set; }
        public string BienServicio { get; set; }
        public string TipoReclamo { get; set; }
        public string DetalleReclamo { get; set; }
        public string PedidoReclamo { get; set; }
        public string Referente { get; set; }
    }
    public class ReporteLibroReclamacionFiltroDTO
    {
        public string? Nombre { get; set; }
        public string? Dni { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }
}
