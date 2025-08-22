using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class AsignacionRegular : BaseIntegraEntity
    {

        public int IdGrupoFiltroProgramaCritico { get; set; }

        public int IdGrupoFiltroProgramaCriticoPgeneral { get; set; }

        public int IdGrupoFiltroProgramaCriticoPorAsesor { get; set; }

        public int IdPgeneral { get; set; }

        public int IdPersonal { get; set; }

        public int IdPersonalJefe { get; set; }

        public int Prioridad { get; set; }

        public bool DatoCalidad { get; set; }

        public bool AplicaProporcionPorPais { get; set; }

        public bool EsLimiteCola { get; set; }

        public int LimiteCola { get; set; }

        public int PorcentajeTolerancia { get; set; }


    }
}
