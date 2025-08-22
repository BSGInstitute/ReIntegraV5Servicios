namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class CombosPendienteDTO
    {
        public List<ComboDTO> ListaModalidades { get; set; }
        public List<FiltroDTO> ListaPeriodo { get; set; }
        public List<DatoPersonalCoordinadorDTO> ListaCoordinador { get; set; }

        public List<PersonalAsignadoDTO> AsistentesActivos { get; set; }
        public List<PersonalAsignadoDTO> AsistentesInactivos { get; set; }
        public List<PersonalAsignadoDTO> AsistentesTotales { get; set; }
    }
    public class DatoPersonalCoordinadorDTO
    {
        public string Usuario { get; set; }
        public string NombreCompleto { get; set; }
    }
}
