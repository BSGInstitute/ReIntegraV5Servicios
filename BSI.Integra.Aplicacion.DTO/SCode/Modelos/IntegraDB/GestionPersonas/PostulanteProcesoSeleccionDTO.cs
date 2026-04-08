
using System.Security.Policy;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas
{
    public class PostulanteProcesoSeleccionDTO
    {
        public int Id { get; set; }
        public int IdPostulante { get; set; }
        public int IdProcesoSeleccion { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int? IdMigracion { get; set; }
        public int? IdEstadoProcesoSeleccion { get; set; }
        public int? IdPostulanteNivelPotencial { get; set; }
        public int? IdProveedor { get; set; }
        public int? IdPersonalOperadorProceso { get; set; }
        public int? IdConvocatoriaPersonal { get; set; }
    }
    public class PostulanteAccesoProcesoSeleccionDTO
    {
        public int Id { get; set; }
        public int IdPostulante { get; set; }
        public string Postulante { get; set; }
        public string Dni { get; set; }
        public string Email { get; set; }
        public string ProcesoSeleccion { get; set; }
        public string Token { get; set; }
        public Guid? GuidAccess { get; set; }
        public int? IdPais { get; set; }
        public string Celular { get; set; }
    }
    public class PostulanteProcesoSeleccionIdDTO
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
    }

    public class PostulanteProcesoNuevoDTO
    {
        public int? IdProcesoSeleccionOrigen { get; set; }
        public int? IdProcesoSeleccionDestino { get; set; }
        public int IdPostulante { get; set; }
        public List<string>? IdsExamenAsignado { get; set; }
        public string? Usuario { get; set; }
        public int? IdPersonal { get; set; }
        public List<int>? IdsProcesoSeleccionEtapa { get; set; }
    }

    public class ComparacionProcesosSeleccionDTO
    {
        public int IdProcesoSeleccionEtapaDestino { get; set; }
        public string NombreProcesoSeleccionEtapaDestino { get; set; }
        public int IdProcesoSeleccionEtapaOrigen { get; set; }
        public string NombreProcesoSeleccionEtapaOrigen { get; set; }
        public bool EsEtapaAprobada { get; set; }
        public int Convalida { get; set; }
    }
}
