using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class CabeceraFurConfiguracionAutomatica : BaseIntegraEntity
    {
        public string Nombre { get; set; } = null!;
        public string Codigo { get; set; } = null!;
        public int? IdConfiguracionProyeccionFur { get; set; }
        public int IdEstadoProyeccionFur { get; set; }
        public int IdArea { get; set; }
        public string? Observacion { get; set; }
    }

    
}
