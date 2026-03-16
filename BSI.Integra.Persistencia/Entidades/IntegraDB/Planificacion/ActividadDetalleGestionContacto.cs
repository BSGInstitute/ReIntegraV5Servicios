
using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    /// <summary>
    /// Entidad de Negocio para el detalle de actividades de Gestión de Contacto
    /// </summary>
    public class ActividadDetalleGestionContacto : BaseIntegraEntity
    {
        public int? IdActividadCabecera { get; set; }
        public int? IdGestionContacto { get; set; }
        public DateTime? FechaProgramada { get; set; }
        public DateTime? FechaReal { get; set; }
        public int? DuracionReal { get; set; }
        public int IdEstadoActividadDetalle { get; set; }
        public string? Comentario { get; set; }
        public int? IdOcurrenciaAlterno { get; set; }
        public int? IdOcurrenciaActividadAlterno { get; set; }
        public bool? EstadoSeguimientoWhatsApp { get; set; }

    }
}
