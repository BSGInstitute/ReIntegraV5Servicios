using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class SolicitudSubCategoriaDTO
    {
        public int Id { get; set; } 
        public string Nombre { get; set; } = null!;
        public int? IdSolicitudCategoria { get; set; } 
        public bool Estado { get; set; } 
        public string UsuarioCreacion { get; set; } = null!; 
        public string UsuarioModificacion { get; set; } = null!; 
        public DateTime FechaCreacion { get; set; } 
        public DateTime FechaModificacion { get; set; } 
        public byte[] RowVersion { get; set; } = null!; 
        public int? IdMigracion { get; set; } 
    }
    public class SolicitudSubCategoriaEntradaDTO
    {
        public int? Id { get; set; }
        public string Nombre { get; set; } = null!;
        public int? IdSolicitudCategoria { get; set; }
        public string Usuario { get; set; }
    }
    public class SolicitudProblemaEntradaDTO
    {
        public int? Id { get; set; }
        public string Descripcion { get; set; } = null!;
        public string DescripcionSolucion { get; set; } = null!;

        public string Prioridad { get; set; } = null!;
        public int IdSolicitudCategoria { get; set; }
        public int IdPersonalRevision { get; set; }
        public int IdPersonalSolucion { get; set; }
        public string Usuario { get; set; }
    }
    public class ComboSolicitudDTO
    {
        public int Id { get; set; }
        public int IdSolicitudTipoReporte { get; set; }
        public string Nombre { get; set; }
    }
    public class ComboSubCategoriaDTO
    {
        public int Id { get; set; }
        public int IdSolicitudCategoria { get; set; }
        public string Nombre { get; set; }
        public string DescripcionSolucion { get; set; }
    }
}
