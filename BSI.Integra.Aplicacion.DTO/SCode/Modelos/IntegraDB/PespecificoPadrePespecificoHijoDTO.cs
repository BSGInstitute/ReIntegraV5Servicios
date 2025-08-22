namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class PespecificoPadrePespecificoHijoDTO
    {
        public int Id { get; set; }
        public int PEspecificoPadreId { get; set; }
        public int PEspecificoHijoId { get; set; }
    }
    public class DatosPEspecificoHijoDTO
    {
        public int PEspecificoHijoId { get; set; }
        public int PEspecificoPadreId { get; set; }
        public int IdProgramaGeneral { get; set; }
        public string Nombre { get; set; }
        public string Duracion { get; set; }
        public int? IdCiudad { get; set; }
        public string TipoAmbiente { get; set; }
    }
    public class InformacionPespecificoHijoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Duracion { get; set; }
        public int? IdCiudad { get; set; }
        public string TipoAmbiente { get; set; }
        public int? IdAmbiente { get; set; }
        public int? IdExpositor_Referencia { get; set; }
        public int? IdProgramaGeneral { get; set; }
        public DateTime? FechaHoraInicio { get; set; }
        public int IdCentroCosto { get; set; }
        public int? IdProveedor { get; set; }
        public int? IdEstadoPEspecifico { get; set; }
        public int? IdModalidadCurso { get; set; }
        public int? IdCursoMoodle { get; set; }
        public int? IdCursoMoodlePrueba { get; set; }
        public string Codigo { get; set; }
        public int? IdPeriodoLectivo { get; set; }
        public int? IdCiclo { get; set; }
        public IEnumerable<ComboDTO> Grupos { get; set; }
        public IEnumerable<ComboDTO> GruposEdicion { get; set; }
    }
}
