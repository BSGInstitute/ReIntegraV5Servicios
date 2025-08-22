using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class AdwordsApiVolumenBusquedum : BaseIntegraEntity
    {
        public int IdAdwordsApiPalabraClave { get; set; }

        public int PromedioBusqueda { get; set; }

        public int Mes { get; set; }

        public int Anho { get; set; }

        public int IdPais { get; set; }


    }
}
