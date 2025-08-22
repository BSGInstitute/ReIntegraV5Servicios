using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class SolicitudInternaDTO
    {
        public int Id { get; set; }
        public int IdSolicitud { get; set; }
        public int IdEstadoSolicitud { get; set; }
        public int IdPersonal { get; set; }
        public string? DetalleSolicitud { get; set; }
        public string? ContentTypeSolicitante { get; set; }
        public string? NombreArchivoSolicitante { get; set; }
        public string? ContentTypeSolucion { get; set; }
        public string? NombreArchivoSolucion { get; set; }
        public string? ComentarioSolucion { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; } = null!;
        public int? IdMigracion { get; set; }
    }
    public class SolicitudInternaEntradaDTO
    {
        public int IdSolicitud { get; set; }
        public int IdEstadoSolicitud { get; set; }
        public int IdPersonal { get; set; }
        public string? DetalleSolicitud { get; set; }
        public string? ContentTypeSolicitante { get; set; }
        public string? NombreArchivoSolicitante { get; set; }
        public string? ContentTypeSolucion { get; set; }
        public string? NombreArchivoSolucion { get; set; }
        public string? ComentarioSolucion { get; set; }
        public IList<IFormFile>? Files { get; set; }
        public string? Usuario { get; set; }
    }

    public class SolicitudInternaFiltradaDTO
    {
        public int id { get; set; }
        public string DetalleSolicitud { get; set; }
        public string Prioridad { get; set; }
        public int IdSolicitud { get; set; }
        public string NombreSolicitud { get; set; }
        public int IdTipoReporte { get; set; }
        public string NombreTipoReporte { get; set; }
        public int IdSolicitudCategoria { get; set; }
        public string NombreSolicitudCategoria { get; set; }
        public int IdSubCategoria { get; set; }
        public string NombreSubCategoria { get; set; }
        public int IdSolicitante { get; set; }
        public string NombreSolicitante { get; set; }
        public string CorreoSolicitante { get; set; }
        public int IdAreaSolicitante { get; set; }
        public string AreaSolicitante { get; set; }
        public int IdAreaRevision { get; set; }
        public string AreaRevision { get; set; }
        public string NombreArchivoSolicitante { get; set; }
        public int IdPersonalRevision { get; set; }
        public string PersonalRevision { get; set; }
        public int IdAreaSolucion { get; set; }
        public string AreaSolucion { get; set; }
        public int IdPersonalSolucion { get; set; }
        public string PersonalSolucion { get; set; }
        public string FechaRegistro { get; set; }
        public string ComentarioSolucion { get; set; }
        public string NombreArchivoSolucion { get; set; }
        public int IdEstadoSolicitud { get; set; }
        public string EstadoSolicitud { get; set; }


    }
    public class FiltroSolicitudesInternasDTO
    {
        public int IdPersonal { get; set; }
        public int TipoFiltro { get; set; }
        public int? Filtro1 { get; set; }
        public string? Filtro2 { get; set; }
    } 
}
