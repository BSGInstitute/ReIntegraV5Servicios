namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class PespecificoCronogramaGrupoDTO
    {
        public int? Id { get; set; }
        public int IdPespecifico { get; set; }
        public int IdPais { get; set; }
        public string UrlDocumentoCronogramaGrupo { get; set; } = null!;
    }
    public class CronogramaGrupoDTO
    {
        public int Id { get; set; }
        public DateTime FechaHoraInicio { get; set; }
        public double Duracion { get; set; }
        public string DuracionTotal { get; set; }
        public string Curso { get; set; }
        public int? IdExpositor { get; set; }
        public int? IdProveedor { get; set; }
        public int? IdAmbiente { get; set; }
        public int? IdCiudad { get; set; }
        public int PEspecificoHijoId { get; set; }
        public string Tipo { get; set; }
        public int IdModalidadCurso { get; set; }
        public string Comentario { get; set; }
        public bool EsSesionInicio { get; set; }
        public int IdCentroCosto { get; set; }
        public int Grupo { get; set; }
        public int? GrupoSesion { get; set; }
        public int? TieneFur { get; set; }
        public bool? MostrarPortalWeb { get; set; }
        public int? IdCiclo { get; set; }
        public int? IdPeriodoLectivo { get; set; }
    }
}