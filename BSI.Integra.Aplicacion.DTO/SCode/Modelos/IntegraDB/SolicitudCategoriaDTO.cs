using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class SolicitudCategoriaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public int? IdSolicitudTipoReporte { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; } = null!;
        public int? IdMigracion { get; set; }
    }
    public class SolicitudCategoriaEntradaDTO
    {
        public int? Id { get; set; }
        public int? IdSolicitudTipoReporte { get; set; }
        public string Nombre { get; set; } = null!;
        public string Usuario { get; set; }
    }
    public class TipoReporteCategoriaDTO
    {
        public int idCategoria { get; set; }
        public string nombreCategoria { get; set; }
        public int idTipoReporte { get; set; }

        public string nombreReporte { get; set; }
    }
    public class TipoReporteSubCategoriaDTO
    {
        public int idCategoria { get; set; }
        public string nombreCategoria { get; set; }
        public int idTipoReporte { get; set; }
        public string nombreReporte { get; set; }
        public int idProblema { get; set; }
        public string nombreProblema { get; set; }
        public string descripcionSolucion { get; set; }
        public string prioridad { get; set; }
        public int idAreaRevision { get; set; }
        public string areaRevisión { get; set; }
        public int idPersonalRevision { get; set; }
        public string personalRevision { get; set; }
        public int idAreaSolucion { get; set; }
        public string areaSolucion { get; set; }
        public int idPersonalSolucion { get; set; }
        public string personalSolución { get; set; }
    }
}
