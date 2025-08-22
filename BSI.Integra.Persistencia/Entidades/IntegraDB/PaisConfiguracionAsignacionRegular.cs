using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class PaisConfiguracionAsignacionRegular : BaseIntegraEntity
    {

        public int IdPaisAsignacionRegular { get; set; }

        public bool EsProporcionManual { get; set; }

        public int ProporcionManual { get; set; }


        public int ProporcionPorPais { get; set; }

        public int IdAsignacionRegular { get; set; }


    }
}
