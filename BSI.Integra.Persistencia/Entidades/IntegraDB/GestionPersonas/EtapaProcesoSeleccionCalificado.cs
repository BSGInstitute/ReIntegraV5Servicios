using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas
{
    public class EtapaProcesoSeleccionCalificado : BaseIntegraEntity
    {
        public int IdProcesoSeleccionEtapa { get; set; }
        public int IdPostulante { get; set; }
        public bool EsEtapaAprobada { get; set; }
        public decimal? NotaCalculada { get; set; }
        public int? IdMigracion { get; set; }
        public int? IdEstadoEtapaProcesoSeleccion { get; set; }
        public bool? EsEtapaActual { get; set; }
        public bool? EsContactado { get; set; }
    }
}
