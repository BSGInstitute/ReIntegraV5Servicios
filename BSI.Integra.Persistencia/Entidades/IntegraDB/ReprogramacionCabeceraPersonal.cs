using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class ReprogramacionCabeceraPersonal : BaseIntegraEntity
    {
        public int IdActividadCabecera { get; set; }
        public int? IdCategoriaOrigen { get; set; }
        public int ReproDia { get; set; }
        public DateTime FechaReprogramacion { get; set; }
        public int? IdPersonal { get; set; }
    }
}
