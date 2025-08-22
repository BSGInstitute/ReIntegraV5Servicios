using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class EstructuraEspecifica : BaseIntegraEntity
    {
        public int IdMatriculaCabecera { get; set; }
        public int IdPgeneralPadre { get; set; }
        public int IdPgeneralHijo { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }

    }
}
