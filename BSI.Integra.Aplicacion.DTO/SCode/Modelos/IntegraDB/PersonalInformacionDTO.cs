namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class PersonalInformacionCorreoDTO
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Email { get; set; }
    }
    public class PersonalInformacionAgendaDTO
    {
        public PersonalDatosAgendaDTO DatosPersonal { get; set; }
        public List<PersonalAsignadoDTO> Asignados { get; set; }
    }
    
}
