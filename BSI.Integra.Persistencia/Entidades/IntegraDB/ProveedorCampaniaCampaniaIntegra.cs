using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class ProveedorCampaniaIntegra : BaseIntegraEntity
    {
        public string Nombre { get; set; } = null!;
        public bool PorDefecto { get; set; } 

    }
}
