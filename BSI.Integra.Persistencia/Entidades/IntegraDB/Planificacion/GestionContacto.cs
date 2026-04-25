using BSI.Integra.Aplicacion.Base;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    /// <summary>
    /// Entidad de Negocio para la Gestión de Contacto (Planificación Docente)
    /// </summary>
    public class GestionContacto : BaseIntegraEntity
    {
        public int? IdCentroCosto { get; set; }
        public int? IdPersonalAsignado { get; set; }
        public int? IdClasificacionPersona { get; set; }
        public int IdFaseGestionContacto { get; set; }
        public int? IdOrigen { get; set; }
        public string? UltimoComentario { get; set; }
        public int IdEstadoGestionContacto { get; set; }
        public bool? EstadoSeguimientoWhatsApp { get; set; }

        public List<ActividadDetalleGestionContacto> ListaActividadDetalle { get; set; }
        public List<GestionContactoLog> ListaGestionContactoLog { get; set; }

        public GestionContacto()
        {
            ListaActividadDetalle = new List<ActividadDetalleGestionContacto>();
            ListaGestionContactoLog = new List<GestionContactoLog>();
        }
    }
}
