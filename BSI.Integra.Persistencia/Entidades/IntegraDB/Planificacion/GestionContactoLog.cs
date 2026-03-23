using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    /// <summary>
    /// Entidad de Negocio para el historial (Log) de Gestión de Contacto
    /// </summary>
    public class GestionContactoLog : BaseIntegraEntity
    {
        public int? IdGestionContacto { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? IdPersonalAsignado { get; set; }
        public int? IdClasificacionPersona { get; set; }
        public int? IdOrigen { get; set; }
        public int? IdEstadoGestionContacto { get; set; }
        public bool? EstadoSeguimientoWhatsApp { get; set; }
        public int? IdFaseGestionContactoAnterior { get; set; }
        public int? IdFaseGestionContacto { get; set; }
        public DateTime? FechaLog { get; set; }
        public string? Comentario { get; set; }
        public int? IdPersonalAsignadoAnterior { get; set; }
        public int? IdCentroCostoAnterior { get; set; }
        public DateTime? FechaFinLog { get; set; }
        public DateTime? FechaCambioFaseContacto { get; set; }
        public bool? CambioFaseContacto { get; set; }
        public DateTime? FechaCambioAsesor { get; set; }
        public DateTime? FechaCambioAsesorAnterior { get; set; }
    }
}
