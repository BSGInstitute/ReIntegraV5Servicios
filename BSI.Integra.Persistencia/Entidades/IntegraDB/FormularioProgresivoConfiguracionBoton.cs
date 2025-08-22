using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class FormularioProgresivoConfiguracionBoton : BaseIntegraEntity
    {
        public int IdFormularioProgresivo { get; set; }
        public int IdFormularioProgresivoSeccionPortal { get; set; }
        public int IdFormularioProgresivoAccionBoton { get; set; }
        public int? IdRegistroArchivoStorage { get; set; }
        public long IdentificadorFilaGrilla { get; set; }
        public string TextoBoton { get; set; }
    }
}
