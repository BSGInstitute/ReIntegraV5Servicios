using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class CrucigramaProgramaCapacitacionDetalle : BaseIntegraEntity
    {
        public int IdCrucigramaProgramaCapacitacionDetalle { get; set; }
        public int NumeroPalabra { get; set; }
        public string Palabra { get; set; }
        public string Definicion { get; set; }
        public int Tipo { get; set; }
        public int ColumnaInicio { get; set; }
        public int FilaInicio { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
