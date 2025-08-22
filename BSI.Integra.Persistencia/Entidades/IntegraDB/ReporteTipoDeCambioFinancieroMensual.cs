using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class ReporteTipoDeCambioFinancieroMensual : BaseIntegraEntity
    {
        public int Id { get; set; }

        public string MonedaAdolar { get; set; }

        public string DolarAmoneda { get; set; }

        public int Mes { get; set; }

        public int Anio { get; set; }

        public int IdMoneda { get; set; }
    }
}
