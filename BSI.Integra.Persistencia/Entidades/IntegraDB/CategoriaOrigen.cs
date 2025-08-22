using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class CategoriaOrigen : BaseIntegraEntity
    {
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public int IdTipoDato { get; set; }
        public int IdTipoCategoriaOrigen { get; set; }
        public int Meta { get; set; }
        public int? IdProveedorCampaniaIntegra { get; set; }
        public int? IdFormularioProcedencia { get; set; }
        public bool Considerar { get; set; }
        public string CodigoOrigen { get; set; } = null!;
        public string? CodigoPublicidad { get; set; }
    }
}
