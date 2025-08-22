using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class GrupoFiltroProgramaCriticoPorAsesor : BaseIntegraEntity
    {
        public int? IdGrupoFiltroProgramaCritico { get; set; }
        public int IdPersonal { get; set; }
        public Guid? IdMigracion { get; set; }

    }
}

