namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{

    public class DatosProgramaGeneralDTO
    {
        public PGeneralAlternoDTO ProgramaGeneral { get; set; }
        public DetallesProgramasDTO DetallesProgramaGeneral { get; set; }
        //public List<int> Modalidades { get; set; }
        public string Usuario { get; set; }
        public int FechaAsincronicaNueva { get; set; } 
    }
    public class CorreoProveedorPersonalDTO
    {
        public int IdProveedor { get; set; }
        public string Nombre { get; set; }
        public string ProveedorEmail { get; set; }
        public int? IdPersonalAsignado { get; set; }
        public string PersonalEmail { get; set; }
        public string UrlFirmaPersonal { get; set; }
    }
    public class ActulizarFechaInicioAonlinelDTO
    {
        public int ActulizarFechaInicioAonline { get; set; }

    }

    public class ProgramaGeneralPuntoCorteAEliminarDTO
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
    }

    public class PersonalAsignadoDocenteDTO
    {
        public int IdDocente { get; set; }
        public string Docente { get; set; }
        public int IdPersonalAsignado { get; set; }
        public string PersonalAsignado { get; set; }
    }

    public class AsignarDocenteDTO
    {
        public int IdForo { get; set; }
        public int IdProveedor { get; set; }
    }

}
