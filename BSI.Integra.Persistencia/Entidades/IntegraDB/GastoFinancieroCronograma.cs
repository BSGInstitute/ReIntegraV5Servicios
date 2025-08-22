using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class GastoFinancieroCronograma : BaseIntegraEntity
    {
        public string Nombre { get; set; } = null!;
        public int IdEntidadFinanciera { get; set; }
        public int IdMoneda { get; set; }
        public decimal CapitalTotal { get; set; }
        public decimal InteresTotal { get; set; }
        public DateTime FechaInicio { get; set; }
    }
}
