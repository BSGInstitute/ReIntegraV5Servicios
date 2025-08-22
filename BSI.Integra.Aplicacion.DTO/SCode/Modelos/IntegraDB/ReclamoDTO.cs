namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ReclamoDTO
    {
        public int Id { get; set; }
        public int IdMatricula { get; set; }
        public string Descripcion { get; set; }
        public int IdReclamoEstado { get; set; }
        public int IdOrigen { get; set; }
        public int IdTipoReclamoAlumno { get; set; }
        public string Usuario { get; set; }
    }
    public class ReclamoSolucionDTO
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public string Comentario { get; set; }
    }
    public class ReclamoAreasDTO
    {
        public int Id { get; set; }
        public string CodigoMatricula { get; set; }
        public string Descripcion { get; set; }
        public int IdReclamoEstado { get; set; }
        public int IdArea { get; set; }
        public int IdOrigen { get; set; }
        public int IdTipoReclamoAlumno { get; set; }
        public string Usuario { get; set; }
        public string ComentarioSolucion { get; set; }
        public string FechaSolucion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public bool Estado { get; set; }

    }
    public class ReclamoAreasEntradaDTO
    {
        public string CodigoMatricula { get; set; }
        public string Descripcion { get; set; }
        public int IdArea { get; set; }
        public int IdReclamoEstado { get; set; }
        public int IdOrigen { get; set; }
        public int IdTipoReclamoAlumno { get; set; }
        public string Usuario { get; set; }
    }
    public class ReclamoFiltroDTO
    {
        public int? idMatricula { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
    }
    
    public class ListarReclamosDTO
    {
        public int Id { get; set; }
        public int IdReclamoEstado { get; set; }
        public int IdMatricula { get; set; }
        public string CodigoMatricula { get; set; }
        public string DNI { get; set; }
        public string NombreAlumno { get; set; }
        public string PersonalAsignado { get; set; }
        public string Descripcion { get; set; }
        public string Origen { get; set; }
        public int IdOrigen { get; set; }
        public string CentroCosto { get; set; }
        public string EstadoMatricula { get; set; }
        public string ReclamoEstado { get; set; }
        public int IdEstadoReclamo { get; set; }
        public DateTime? FechaUltimaLlamada { get; set; }
        public DateTime? FechaUltimoCorreo { get; set; }
        public DateTime? FechaUltimoWapp { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public int IdTipoReclamoAlumno { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string TipoReclamoAlumno { get; set; }
        public string ComentarioSolucion { get; set; }

    }
    public class registroTipoReclamoAlumnoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
    public class ListarReclamosAreasDTO
    {
        public int Id { get; set; }
        public int IdReclamoEstado { get; set; }
        public string ReclamoEstado { get; set; }
        public string Usuario { get; set; }
        public int IdOrigen { get; set; }
        public string Origen { get; set; }
        public string Area { get; set; }
        public int IdTipoReclamo { get; set; }
        public string TipoReclamo { get; set; }
        public string CodigoMatricula { get; set; }
        public string Descripcion { get; set; }
        public string ComentarioSolucion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaSolucion { get; set; }


    }
    public class FiltroReclamoDTO{
        public int Id { get; set; }
        public string Usuario { get; set; }
        public string Comentario { get; set; }
    }
}
