using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class AsignacionAutomaticaOrigen : BaseIntegraEntity
    {



        public string Nombre { get; set; }
        public byte[] RowVersion { get; set; }

        public const int Scoring = 1;
        public const int PortalWeb = 2;
        public const int CargaMasiva = 3;

        public AsignacionAutomaticaOrigen()
        {
        }
    }
}

