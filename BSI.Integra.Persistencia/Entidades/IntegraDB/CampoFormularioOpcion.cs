using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class CampoFormularioOpcion: BaseIntegraEntity
    {
        public int Valor { get; set; }
        public string? Descripcion { get; set; }
        public int IdCampoFormulario { get; set; }
    }
}
