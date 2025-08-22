using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class OrigenDatoCalidadDetalle : BaseIntegraEntity
    {
        public int IdOrigenDatoCalidad { get; set; }

        public bool DatosCalidad { get; set; }

        public bool MuyAltaAr { get; set; }

        public bool MuyAltaAd { get; set; }

        public bool AltaAd { get; set; }

        public bool AltaAr { get; set; }

        public bool MediaAd { get; set; }

        public bool MediaAr { get; set; }


    }
}
