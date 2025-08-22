using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class Frecuencia : BaseIntegraEntity
    {
        public string Nombre { get; set; } = null!;
        public int NumDias { get; set; }
    }
}
