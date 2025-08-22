using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class CongelamientoProyeccionFur : BaseIntegraEntity
    {
        public int IdCabeceraFurConfiguracionAutomatica { get; set; }
        public int IdArea { get; set; }
        public string Configuracion { get; set; } = null!;
        public string DetalleFurConfiguracionAutomatica { get; set; } = null!;
    }
}
