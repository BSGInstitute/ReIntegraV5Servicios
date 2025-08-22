using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class AsignacionAutomaticaConfiguracion : BaseIntegraEntity
    {


        public int IdFaseOportunidad { get; set; }

        public int? IdTipoDato { get; set; }

        public int? IdOrigen { get; set; }

        public bool Inclusivo { get; set; }

        public bool Habilitado { get; set; }


    }
}
