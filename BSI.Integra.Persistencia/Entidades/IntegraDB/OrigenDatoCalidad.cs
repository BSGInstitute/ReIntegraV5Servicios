using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class OrigenDatoCalidad : BaseIntegraEntity
    {

        public int IdProveedorCampaniaIntegra { get; set; }

        public int IdCategoriaOrigen { get; set; }

        public int IdOrigenSector { get; set; }

        public bool Agrupar { get; set; }

        public bool AgruparCategoriaOrigen { get; set; }

    }
}
