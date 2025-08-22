using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class GrupoFiltroProgramaCritico : BaseIntegraEntity
    {
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;

        public List<GrupoFiltroProgramaCriticoPorAsesor> GrupoFiltroProgramaCriticoPorAsesor { get; set; }
    }
}
