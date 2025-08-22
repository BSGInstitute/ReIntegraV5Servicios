using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas
{
    public class SedeTrabajo : BaseIntegraEntity
    {
        public string Nombre { get; set; } = null!;
        public string? IpCentral { get; set; }
        public string? Comentarios { get; set; }
        public int? IdPais { get; set; }
    }
}
