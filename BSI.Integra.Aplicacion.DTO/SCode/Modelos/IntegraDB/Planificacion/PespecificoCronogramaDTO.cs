namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class PespecificoCronogramaDTO
    {
        public int? Id { get; set; }
        public int IdPespecifico { get; set; }
        public int IdPais { get; set; }
        public string UrlDocumentoCronograma { get; set; } = null!;
    }
    public class PEspecificoCronogramaGrupalGrupoDTO
    {
        public int Grupo { get; set; }
        public IEnumerable<PEspecificoCronogramaGrupalDTO> Lista { get; set; }
    }
    public class PEspecificoCronogramaGrupalDTO
    {
        public int Id { get; set; }
        public DateTime FechaHoraInicio { get; set; }
        public int? IdPEspecifico { get; set; }
        public double Duracion { get; set; }
        public string DuracionTotal { get; set; }
        public string Curso { get; set; }
        public string Tipo { get; set; }
        public int Grupo { get; set; }
        public string ModalidadSesion { get; set; }
    }
    public class PEspecificoSesionGrupoAnteriorDTO
    {
        public int Id { get; set; }
        public string Curso { get; set; }
        public int IdPEspecifico { get; set; }
        public decimal? Duracion { get; set; }
        public decimal? DuracionTotal { get; set; }
        public int? IdExpositor { get; set; }
        public DateTime FechaHoraInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Tipo { get; set; }
        public string NombrePrograma { get; set; }
        public string CentroCosto { get; set; }
        public int Grupo { get; set; }
        public string ModalidadSesion { get; set; }
    }
}