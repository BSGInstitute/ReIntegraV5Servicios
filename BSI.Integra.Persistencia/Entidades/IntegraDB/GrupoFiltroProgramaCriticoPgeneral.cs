using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class GrupoFiltroProgramaCriticoPgeneral : BaseIntegraEntity
    {
        public int IdGrupoFiltroProgramaCritico { get; set; }
        public int IdPgeneral { get; set; }
        public int? IdMigracion
        {
            get; set;
        }
    }
}

