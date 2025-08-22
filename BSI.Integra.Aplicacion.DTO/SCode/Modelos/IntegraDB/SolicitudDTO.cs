using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class SolicitudDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public int IdSolicitudSubCategoria { get; set; }
        public int IdPersonal { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; } = null!;
        public int? IdMigracion { get; set; }
    }
    public class SolicitudEntradaDTO
    {
        public int? Id { get; set; }
        public string Nombre { get; set; }
        public string Prioridad { get; set; } = null!;
        public int IdSolicitudSubCategoria { get; set; }
        public int IdPersonalRevision { get; set; }
        public int IdPersonalSolucion { get; set; }
        public string Usuario { get; set; }
    }
    public class ReporteSolicitudDTO
    {
        public int idCategoria { get; set; }
        public string nombreCategoria { get; set; }
        public int idTipoReporte { get; set; }
        public string nombreReporte { get; set; }
        public int idSubCategoria { get; set; }
        public string nombreSubCategoria { get; set; }
        public int idSolicitud { get; set; }
        public string nombreSolicitud { get; set; }
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
    public class HistorialSolicitudAlumnoDTO
    {
        public int id { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public string CodigoMatricula { get; set; }
        public string NombreAlumno { get; set; }

        public int IdPEspecifico { get; set; }
        public string NombrePEspecifico { get; set; }
        public int IdCentroCosto { get; set; }
        public string CentroCosto { get; set; }
        public int IdPGeneral { get; set; }
        public string PGeneral { get; set; }
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
        public int IdAreaSolicitante { get; set; }
        public string AreaSolicitante { get; set; }

        public int IdAreaRevision { get; set; }
        public string AreaRevision { get; set; }
        public int IdPersonalRevision { get; set; }
        public string PersonalRevision { get; set; }
        public string NombreArchivoSolicitante { get; set; }

        public int IdAreaSolucion { get; set; }
        public string AreaSolucion { get; set; }
        public int IdPersonalSolucion { get; set; }
        public string PersonalSolucion { get; set; }
        public string NombreArchivoSolucion { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string ComentarioSolucion { get; set; }
        public int IdEstadoSolicitud { get; set; }
        public string EstadoSolicitud { get; set; }
    }
    public class EstadoSolicitudDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

    }
}
