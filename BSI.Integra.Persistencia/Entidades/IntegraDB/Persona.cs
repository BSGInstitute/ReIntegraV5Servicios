using BSI.Integra.Aplicacion.Base;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class Persona : BaseIntegraEntity
    {
        public string? Email1 { get; set; }
        public int Id { get; set; }
        public int? IdMigracion { get; set; }
        public ClasificacionPersona ClasificacionPersona { get; set; }
    }
}
