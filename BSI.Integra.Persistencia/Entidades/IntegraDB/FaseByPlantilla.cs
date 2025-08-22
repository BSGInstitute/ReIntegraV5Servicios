using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class FaseByPlantilla : BaseIntegraEntity
    {
        public int idPlantilla { get; set; }
        public int idFaseOrigen { get; set; }
        public string NombreFase { get; set; }

    }
}
