
using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas
{
    public class EtapaProcesoSeleccionCalificadoDTO
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
    public class EtapaExamenesPorPostulanteDTO
    {
        public int Id { get; set; }
        public int IdProcesoSeleccionEtapa { get; set; }
        public int IdPostulante { get; set; }
        public bool EsEtapaAprobada { get; set; }
        public int IdEstadoEtapaProcesoSeleccion { get; set; }
        public bool EsContactado { get; set; }
        public int IdProcesoSeleccion { get; set; }
        public string Nombre { get; set; }
        public bool EsCalificadoPorPostulante { get; set; }
        public int NroOrden { get; set; }
    }




    public class EtapaProcesoSeleccionCalificadoActualizarDTO
    {
        public int Id { get; set; }
        public int IdProcesoSeleccionEtapa { get; set; }
        public int IdPostulante { get; set; }
        public bool EsEtapaAprobada { get; set; }
        public decimal? NotaCalculada { get; set; }
        public int? IdMigracion { get; set; }
        public int? IdEstadoEtapaProcesoSeleccion { get; set; }
        public bool? EsEtapaActual { get; set; }
        public bool? EsContactado { get; set; }
        public string UsuarioModificacion { get; set; }
    }
}
