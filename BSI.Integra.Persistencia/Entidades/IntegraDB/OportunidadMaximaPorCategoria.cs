using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class OportunidadMaximaPorCategoria : BaseIntegraEntity
    {
        public int IdPersonal { get; set; }
        public int IdTipoCategoriaOrigen { get; set; }
        public int IdPais { get; set; }
        public int OportunidadesMaximas { get; set; }
        public int OportunidadesSinGenerarIs { get; set; }
        public int Meta { get; set; }
        [StringLength(100)]
        public string Grupo { get; set; } = null!;
    }
}
