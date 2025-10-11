using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    /// <summary>
    /// Entidad: Vinculación explícita entre oportunidades y formularios de Google
    /// Tabla: mkt.T_OportunidadGoogleLead
    /// Autor: Sistema
    /// Fecha: 2025-10-04
    /// </summary>
    [Table("T_OportunidadGoogleLead", Schema = "mkt")]
    public class OportunidadGoogleLead : BaseIntegraEntity
    {
        public int IdOportunidad { get; set; }
        public int IdGoogleFormularioLeadgen { get; set; }
        public int IdAlumno { get; set; }
        public DateTime FechaVinculacion { get; set; }
    }
}
